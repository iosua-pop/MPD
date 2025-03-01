using System.Net;
using Plugin.LocalNotification;
using SchoolApp.Data;
using SchoolApp.Models;

namespace SchoolApp
{
    public partial class App : Application
    {
        static MusicSchoolDatabase database;

        public static MusicSchoolDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new MusicSchoolDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MusicSchool.db3"));
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();

            //MainPage = new AppShell();

            if (Preferences.ContainsKey("UserEmail"))
            {
                Task.Run(async () => await CheckAndSendNotifications());
                MainPage = new AppShell();
            }
            else
            {
                MainPage = new NavigationPage(new LoginPage());
            }
        }

        public async Task CheckAndSendNotifications()
        {
            string userRole = Preferences.Get("UserRole", "User");
            int userId = Preferences.Get("ID", -1);

            if (userRole == "Admin") return;

            var allProgramari = await App.Database.GetProgramariAsync();
            var now = DateTime.Now;
            var in24h = now.AddHours(24);

            List<Programare> programariViitoare;

            if (userRole == "Student")
            {
                programariViitoare = allProgramari.Where(p => p.StudentID == userId && p.OraProgramarii >= now && p.OraProgramarii <= in24h).ToList();
            }
            else if (userRole == "Profesor")
            {
                programariViitoare = allProgramari.Where(p => p.TeacherID == userId && p.OraProgramarii >= now && p.OraProgramarii <= in24h).ToList();
            }
            else
            {
                return;
            }

            foreach (var programare in programariViitoare)
            {
                string mesaj = $"Ai o lecție programată pe {programare.OraProgramarii:dd/MM/yyyy HH:mm} la {programare.AdresaProgramarii}";

                var notification = new NotificationRequest
                {
                    NotificationId = programare.ID,
                    Title = "Reminder Lecție",
                    Description = mesaj,
                    Schedule = new NotificationRequestSchedule
                    {
                        NotifyTime = DateTime.Now.AddSeconds(1)
                    }
                };

                await LocalNotificationCenter.Current.Show(notification);

            }
        }

    }
}
