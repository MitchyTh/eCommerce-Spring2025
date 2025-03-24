using Maui.eCommerce.ViewModels;

namespace Maui.eCommerce.Views;

public partial class CheckoutView : ContentPage
{
	public CheckoutView()
	{
		BindingContext = new ShoppingManagementViewModel();
		InitializeComponent();
	}
}