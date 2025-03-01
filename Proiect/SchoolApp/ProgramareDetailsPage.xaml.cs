using SchoolApp.Models;

namespace SchoolApp;

public partial class ProgramareDetailsPage : ContentPage
{

    public ProgramareDetailsPage()
	{
		InitializeComponent();
        
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        dataPicker.MinimumDate = DateTime.Today;
        var profesori = await App.Database.GetMembersByRoleAsync("Profesor");
        teacherPicker.ItemsSource = profesori;
        teacherPicker.ItemDisplayBinding = new Binding("FullName");
    }

    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var programare = (Programare)BindingContext;

        if (teacherPicker.SelectedItem == null)
        {
            await DisplayAlert("Eroare", "Selecteaza un profesor!", "OK");
            return;
        }

        if (string.IsNullOrWhiteSpace(adresaEntry.Text))
        {
            await DisplayAlert("Eroare", "Introdu o adresa valida!", "OK");
            return;
        }

        DateTime selectedDateTime = dataPicker.Date.Add(timePicker.Time);
        if (selectedDateTime < DateTime.Now)
        {
            await DisplayAlert("Eroare", "Programarea nu poate fi în trecut!", "OK");
            return;
        }
        int userId = Preferences.Get("ID", -1);
        programare.StudentID = userId;
        programare.TeacherID = ((Member)teacherPicker.SelectedItem).ID;
        programare.OraProgramarii = selectedDateTime;
        programare.AdresaProgramarii = adresaEntry.Text;

        await App.Database.SaveProgramareAsync(programare);
        await Navigation.PopAsync();
    }
}