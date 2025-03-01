using SchoolApp.Models;

namespace SchoolApp;

public partial class ProgramariPage : ContentPage
{
    private string userRole;
    private int userId;

    public ProgramariPage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        userRole = Preferences.Get("UserRole", "Student");
        userId = Preferences.Get("ID", -1);

        var allProgramari = await App.Database.GetProgramariAsync();
        List<Programare> myProgramari = new List<Programare>();

        if (userRole == "Student")
        {
            myProgramari = allProgramari.Where(p => p.StudentID == userId).ToList();
        }
        else if (userRole == "Profesor")
        {
            myProgramari = allProgramari.Where(p => p.TeacherID == userId).ToList();
        }
        else if (userRole == "Admin")
        {
            myProgramari = allProgramari;
        }

        foreach (var p in myProgramari)
        {
            if (userRole == "Profesor")
            {
                var student = await App.Database.GetMemberByIdAsync(p.StudentID);
                p.DisplayName = student != null ? student.FullName : "Necunoscut";
            }
            else
            {
                var profesor = await App.Database.GetMemberByIdAsync(p.TeacherID);
                p.DisplayName = profesor != null ? profesor.FullName : "Necunoscut";
            }
        }

        programariListView.ItemsSource = myProgramari;
    }

    async void OnAddProgramareClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ProgramareDetailsPage
        {
            BindingContext = new Programare()
        });
    }

    async void OnEditProgramareClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var selectedProgramare = button.BindingContext as Programare;

        if (selectedProgramare == null) return;

        await Navigation.PushAsync(new ProgramareDetailsPage
        {
            BindingContext = selectedProgramare
        });
    }

    async void OnDeleteProgramareClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var selectedProgramare = button.BindingContext as Programare;

        if (selectedProgramare == null) return;

        bool confirm = await DisplayAlert("Stergere", $"Sigur vrei sa stergi aceasta programare?", "Da", "Nu");

        if (confirm)
        {
            await App.Database.DeleteProgramareAsync(selectedProgramare);
            OnAppearing();
        }
    }
}