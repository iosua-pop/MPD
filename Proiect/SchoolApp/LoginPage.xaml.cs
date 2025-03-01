namespace SchoolApp;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}

    async void OnLoginClicked(object sender, EventArgs e)
    {
        var user = await App.Database.LoginUserAsync(emailEntry.Text, passwordEntry.Text);

        if (user != null)
        {
            Preferences.Set("UserEmail", user.Email);
            Preferences.Set("UserRole", user.Role);
            Preferences.Set("ID", user.ID);

            await ((App)Application.Current).CheckAndSendNotifications();
            App.Current.MainPage = new AppShell();
        }
        else
        {
            await DisplayAlert("Eroare", "Email sau parola incorecta", "OK");
        }
    }

    async void OnRegisterClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RegisterPage());
    }
}