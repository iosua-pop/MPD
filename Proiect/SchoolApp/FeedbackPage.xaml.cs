using SchoolApp.Models;

namespace SchoolApp;

public partial class FeedbackPage : ContentPage
{
	public FeedbackPage()
	{
		InitializeComponent();
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        listView.ItemsSource = await App.Database.GetFeedbacksAsync();
    }

    async void OnFeedbackSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem == null)
            return;

        var selectedFeedback = e.SelectedItem as Feedback;
        await Navigation.PushAsync(new FeedbackDetailsPage
        {
            BindingContext = selectedFeedback
        });

        listView.SelectedItem = null;
    }

    async void OnAddFeedbackClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new FeedbackDetailsPage
        {
            BindingContext = new Feedback()
        });
    }
}