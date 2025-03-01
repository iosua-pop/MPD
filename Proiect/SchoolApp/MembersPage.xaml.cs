using SchoolApp.Models;

namespace SchoolApp;

public partial class MembersPage : ContentPage
{

    private string userRole;
    private int userId;

    public MembersPage()
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
        var students = allMembers.Where(m => m.Role == "Student").ToList();

        membersListView.ItemsSource = students;
    }

    async void OnAddMemberClicked(object sender, EventArgs e)
    {
        if (userRole != "Admin")
        {
            await DisplayAlert("Acces Restrictionat", "Doar Adminul poate adauga membri.", "OK");
            return;
        }

        await Navigation.PushAsync(new MemberDetailsPage
        {
            BindingContext = new Member()
        });
    }

    async void OnEditMemberClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var selectedMember = button.BindingContext as Member;

        if (selectedMember == null) return;

        if (userRole != "Admin" && selectedMember.ID != userId)
        {
            await DisplayAlert("Acces Restrictionat", "Poti edita doar propriul profil.", "OK");
            return;
        }

        await Navigation.PushAsync(new MemberDetailsPage
        {
            BindingContext = selectedMember
        });
    }

    async void OnDeleteMemberClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var selectedMember = button.BindingContext as Member;

        if (selectedMember == null) return;

        if (userRole != "Admin" && selectedMember.ID != userId)
        {
            await DisplayAlert("Acces Restrictionat", "Doar Adminul poate sterge membri.", "OK");
            return;
        }

        bool confirm = await DisplayAlert("Stergere", $"Sigur vrei sa stergi {selectedMember.FullName}?", "Da", "Nu");

        if (confirm)
        {
            await App.Database.DeleteMemberAsync(selectedMember);
            OnAppearing();

            if(selectedMember.ID == userId)
            {
                Preferences.Remove("UserEmail");
                Preferences.Remove("UserRole");
                Preferences.Remove("ID");

                App.Current.MainPage = new NavigationPage(new LoginPage());
            }
        }
    }

}