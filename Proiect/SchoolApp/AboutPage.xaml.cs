namespace SchoolApp;

public partial class AboutPage : ContentPage
{
    public AboutPage()
    {
        InitializeComponent();
    }

    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        bool confirm = await DisplayAlert("Logout", "Esti sigur ca vrei sa te deconectezi?", "Da", "Nu");

        if (confirm)
        {
            Preferences.Remove("UserEmail");
            Preferences.Remove("UserRole");
            Preferences.Remove("ID");

            App.Current.MainPage = new NavigationPage(new LoginPage());
        }
    }
}