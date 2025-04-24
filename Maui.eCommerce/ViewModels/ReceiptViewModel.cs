using System.ComponentModel;
using Library.eCommerce.Models;
using Library.eCommerce.Util;
using Newtonsoft.Json;

public class ReceiptViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    private ReceiptDataContainer _receiptData;
    public ReceiptDataContainer ReceiptData
    {
        get => _receiptData;
        set
        {
            _receiptData = value;
            OnPropertyChanged(nameof(ReceiptData));
        }
    }

    public ReceiptViewModel()
    {
        LoadReceiptData();
    }

    public async void LoadReceiptData()
    {
        var receipt = await FinalizePurchase();
        ReceiptData = receipt;
    }

    private async Task<ReceiptDataContainer> FinalizePurchase()
    {
        var response = await new WebRequestHandler().Post("/Receipt/purchase", null);
        return JsonConvert.DeserializeObject<ReceiptDataContainer>(response);
    }

    protected void OnPropertyChanged(string name) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}