using Maui.eCommerce.ViewModels;

namespace Maui.eCommerce.Views;

public partial class ShoppingManagementView : ContentPage
{
	public ShoppingManagementView()
	{
		InitializeComponent();
		BindingContext = new ShoppingManagementViewModel();
	}

    private void AddtoCartClicked(object sender, EventArgs e)
    {
		(BindingContext as ShoppingManagementViewModel).PurchaseItem();
    }

    private void RemoveFromCartClicked(object sender, EventArgs e)
    {
        (BindingContext as ShoppingManagementViewModel).ReturnItem();
    }

    private void GoBackClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }

    private void CheckoutClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Checkout");
    }

    private void InLineAddClicked(object sender, EventArgs e)
    {
        (BindingContext as ShoppingManagementViewModel).RefreshUX();
    }
}