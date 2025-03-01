using SchoolApp.Models;

namespace SchoolApp;

public partial class MemberDetailsPage : ContentPage
{
	public MemberDetailsPage()
	{
		InitializeComponent();
	}

    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var member = (Member)BindingContext;
        await App.Database.SaveMemberAsync(member);
        await Navigation.PopAsync();
    }
}