using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinansCepte.Data;
using FinansCepte.Models;

namespace FinansCepte;

public partial class AddTransactions : ContentPage
{
    private string selectedType = "Gider";

    public AddTransactions()
    {
        InitializeComponent();
        UpdateUI();
    }

    private void OnTypeChanged(object sender, EventArgs e)
    {
        var button = sender as Button;
        if (button != null)
        {
            selectedType = button.Text;
            UpdateUI();
        }
    }

    void UpdateUI()
    {
        BtnGider.BackgroundColor = Colors.LightGray;
        BtnGider.TextColor = Colors.Gray;
        
        BtnGelir.BackgroundColor = Colors.LightGray;
        BtnGelir.TextColor = Colors.Gray;
        
        BtnYatırım.BackgroundColor = Colors.LightGray;
        BtnYatırım.TextColor = Colors.Gray;

        if (selectedType == "Gider")
        {
            BtnGider.BackgroundColor = Colors.Gray;
            BtnGider.TextColor = Colors.White;
            StkInvestment.IsVisible = false;
        }
        else if (selectedType == "Gelir")
        {
            BtnGelir.BackgroundColor = Colors.Gray;
            BtnGelir.TextColor = Colors.White;
            StkInvestment.IsVisible = false;
        }
        else if (selectedType == "Yatırım")
        {
            BtnYatırım.BackgroundColor = Colors.Gray;
            BtnYatırım.TextColor = Colors.White;
            StkInvestment.IsVisible = true;
        }
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(EntAmount.Text))
        {
            await DisplayAlert("Uyarı", "Lütfen bir tutar girin.", "Tamam");
                return;
        }

        var newItem = new Transaction
        {
            Type = selectedType,
            Title = EntTitle.Text,
            Date = DtDate.Date,
            Amount = Content.ToDecimal(EntAmount.Text)
        };

        if (selectedType == "Yatırım")
        {
            newItem.AssetName = PckAsset.SelectedItem?.ToString();

            if (!string.IsNullOrEmpty(EntQuantity.Text))
                newItem.UnitPrice = Convert.ToDecimal(EntUnitPrice.Text);
        }
        
        FakeDb.Add(newItem);
        await DisplayAlert("Başarılı", "İşlem Başarıyla Eklendi!", "Tamam");

        EntAmount.Text = "";
        EntTitle.Text = "";

        string mesaj = $"{selectedType} işlemi seçildi.\nTutar: {EntAmount.Text} TL";

        if (selectedType == "Yatırım")
        {
            mesaj += $"\nVarlık: {PckAsset.SelectedItem}";
            mesaj += $"\nMiktar: {EntQuantity.Text}";
        }
        await DisplayAlert("Başarılı ", mesaj, "Tamam");
        {
            
        }
    }
}