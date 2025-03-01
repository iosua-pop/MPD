using SchoolApp.Models;

namespace SchoolApp;

public partial class FeedbackDetailsPage : ContentPage
{
	public FeedbackDetailsPage()
	{
		InitializeComponent();
	}

    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var feedback = (Feedback)BindingContext;
        await App.Database.SaveFeedbackAsync(feedback);
        await Navigation.PopAsync();
    }

    async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        var feedback = (Feedback)BindingContext;
        await App.Database.DeleteFeedbackAsync(feedback);
        await Navigation.PopAsync();
    }
}