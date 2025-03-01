using SchoolApp.Models;

namespace SchoolApp;

public partial class InstrumentDetailsPage : ContentPage
{
	public InstrumentDetailsPage()
	{
		InitializeComponent();
	}

    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var instrument = (Instrument)BindingContext;
        await App.Database.SaveInstrumentAsync(instrument);
        await Navigation.PopAsync();
    }

    async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        var instrument = (Instrument)BindingContext;
        await App.Database.DeleteInstrumentAsync(instrument);
        await Navigation.PopAsync();
    }
}