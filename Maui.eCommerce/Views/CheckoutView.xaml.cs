using Maui.eCommerce.ViewModels;

namespace Maui.eCommerce.Views;

public partial class CheckoutView : ContentPage
{
	public CheckoutView()
	{
        InitializeComponent();
        BindingContext = new ReceiptViewModel();
	}
}