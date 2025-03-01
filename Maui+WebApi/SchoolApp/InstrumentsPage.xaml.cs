using SchoolApp.Models;

namespace SchoolApp;

public partial class InstrumentsPage : ContentPage
{
	public InstrumentsPage()
	{
		InitializeComponent();
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        listView.ItemsSource = await App.Database.GetInstrumentsAsync();
    }

    async void OnInstrumentSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem == null)
            return;

        var selectedInstrument = e.SelectedItem as Instrument;
        await Navigation.PushAsync(new InstrumentDetailsPage
        {
            BindingContext = selectedInstrument
        });

        listView.SelectedItem = null;
    }

    async void OnAddInstrumentClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new InstrumentDetailsPage
        {
            BindingContext = new Instrument()
        });
    }
}