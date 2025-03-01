using AppMaui.Models;

namespace AppMaui;

public partial class ListPage : ContentPage
{
    public ListPage()
    {
        InitializeComponent();
    }

    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var slist = (ShopList)BindingContext;
        slist.Date = DateTime.UtcNow;
        Shop selectedShop = (ShopPicker.SelectedItem as Shop);
        slist.ShopID = selectedShop.ID;
        await App.Database.SaveShopListAsync(slist);
        await Navigation.PopAsync();
    }

    async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        var slist = (ShopList)BindingContext;
        await App.Database.DeleteShopListAsync(slist);
        await Navigation.PopAsync();
    }

    async void OnChooseButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ProductPage((ShopList)this.BindingContext)
        {
            BindingContext = new Product()
        });
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var items = await App.Database.GetShopsAsync();

        ShopPicker.ItemsSource = items;
        ShopPicker.ItemDisplayBinding = new Binding("ShopDetails");

        var shopl = (ShopList)BindingContext;
        var selectedShop = items.FirstOrDefault(s => s.ID == shopl.ShopID);
        ShopPicker.SelectedItem = selectedShop;

        listView.ItemsSource = await App.Database.GetListProductsAsync(shopl.ID);
    }


    async void OnDeleteItemButtonClicked(object sender, EventArgs e)
    {
        Product p;
        if (listView.SelectedItem != null)
        {
            p = listView.SelectedItem as Product;
            var shopl = (ShopList)BindingContext;

            await App.Database.DeleteListProductAsync(shopl.ID, p.ID);
            listView.ItemsSource = await App.Database.GetListProductsAsync(shopl.ID);

            //await Navigation.PopAsync();
        }
    }
}