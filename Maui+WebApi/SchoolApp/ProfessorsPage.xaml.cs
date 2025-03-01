using SchoolApp.Models;

namespace SchoolApp;

public partial class ProfessorsPage : ContentPage
{
    private string userRole;
    private int userId;

    public ProfessorsPage()
    {
        InitializeComponent();

        userRole = Preferences.Get("UserRole", "Student");
        userId = Preferences.Get("ID", -1);

        addButton.IsVisible = userRole == "Admin";
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var allMembers = await App.Database.GetMembersAsync();
        var professors = allMembers.Where(m => m.Role == "Profesor").ToList();

        listView.ItemsSource = professors;
    }

    async void OnAddProfessorClicked(object sender, EventArgs e)
    {
        if (userRole != "Admin")
        {
            await DisplayAlert("Acces Restrictionat", "Doar Adminul poate adauga profesori.", "OK");
            return;
        }

        await Navigation.PushAsync(new MemberDetailsPage
        {
            BindingContext = new Member { Role = "Profesor" }
        });
    }

    async void OnEditProfessorClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var selectedProfessor = button.BindingContext as Member;

        if (selectedProfessor == null) return;

        if (userRole != "Admin" && selectedProfessor.ID != userId)
        {
            await DisplayAlert("Acces Restrictionat", "Poti edita doar propriul profil.", "OK");
            return;
        }

        await Navigation.PushAsync(new MemberDetailsPage
        {
            BindingContext = selectedProfessor
        });
    }

    async void OnDeleteProfessorClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var selectedProfessor = button.BindingContext as Member;

        if (selectedProfessor == null) return;

        if (userRole != "Admin" || selectedProfessor.ID != userId)
        {
            await DisplayAlert("Acces Restrictionat", "Doar Adminul poate sterge profesori.", "OK");
            return;
        }

        bool confirm = await DisplayAlert("Stergere", $"Sigur vrei sã stergi {selectedProfessor.FullName}?", "Da", "Nu");

        if (confirm)
        {
            await App.Database.DeleteMemberAsync(selectedProfessor);
            OnAppearing();

            if (selectedProfessor.ID == userId)
            {
                Preferences.Remove("UserEmail");
                Preferences.Remove("UserRole");
                Preferences.Remove("ID");

                App.Current.MainPage = new NavigationPage(new LoginPage());
            }
        }
    }
}