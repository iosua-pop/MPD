namespace SchoolApp;

public partial class RegisterPage : ContentPage
{
	public RegisterPage()
	{
		InitializeComponent();
	}

    async void OnRegisterClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(numeEntry.Text) ||
            string.IsNullOrWhiteSpace(prenumeEntry.Text) ||
            string.IsNullOrWhiteSpace(emailEntry.Text) ||
            string.IsNullOrWhiteSpace(telefonEntry.Text) ||
            string.IsNullOrWhiteSpace(adresaEntry.Text) ||
            string.IsNullOrWhiteSpace(passwordEntry.Text) ||
            string.IsNullOrWhiteSpace(confirmPasswordEntry.Text))
        {
            await DisplayAlert("Eroare", "Toate câmpurile sunt obligatorii.", "OK");
            return;
        }

        if (passwordEntry.Text != confirmPasswordEntry.Text)
        {
            await DisplayAlert("Eroare", "Parolele nu coincid!", "OK");
            return;
        }

        var success = await App.Database.RegisterUserAsync(
            emailEntry.Text, passwordEntry.Text, numeEntry.Text, prenumeEntry.Text, telefonEntry.Text, adresaEntry.Text
        );

        if (success)
        {
            await DisplayAlert("Succes", "Inregistrare completa! Acum te poti autentifica.", "OK");
            await Navigation.PopAsync();
        }
        else
        {
            await DisplayAlert("Eroare", "Inregistrare esuata!", "OK");
        }
    }
}