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
    private readonly LocalDbService _dbService;
    string[] GiderKategorileri = { "Market", "Fatura", "Kira", "Ulaşım", "Sağlık", "Eğlence", "Diğer" };
    string[] GelirKategorileri = { "Maaş", "Prim", "Satış", "Kira Geliri", "Borç Tahsilatı", "Diğer" };
    string[] YatırımKategorileri = { "Altın", "Döviz", "Hisse", "Fon", "Kripto" };

    
    public AddTransactions(LocalDbService dbService)
    {
        InitializeComponent();
        _dbService = dbService;
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
        }
        else if (selectedType == "Gelir")
        {
            BtnGelir.BackgroundColor = Colors.Gray;
            BtnGelir.TextColor = Colors.White;
        }
        else if (selectedType == "Yatırım")
        {
            BtnYatırım.BackgroundColor = Colors.Gray;
            BtnYatırım.TextColor = Colors.White;
        }
        UpdateCategories();
    }
    
    void UpdateCategories()
    {
        PckCategory.SelectedItem = null; 

        if (selectedType == "Gider")
        {
            PckCategory.ItemsSource = GiderKategorileri;
            PckCategory.Title = "Gider Kategorisi Seç";
        }
        else if (selectedType == "Gelir")
        {
            PckCategory.ItemsSource = GelirKategorileri;
            PckCategory.Title = "Gelir Kaynağı Seç";
        }
        else 
        {
            PckCategory.ItemsSource = YatırımKategorileri;
            PckCategory.Title = "Yatırım Türü";
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
            Amount = Convert.ToDecimal(EntAmount.Text.Replace(".",",")),
            Category = PckCategory.SelectedItem?.ToString() ?? "Diğer"
        };
        
        await _dbService.CreateTransactionAsync(newItem);
        await DisplayAlert("Başarılı", "İşlem Başarıyla Eklendi!", "Tamam");

        EntAmount.Text = string.Empty;
        EntTitle.Text = string.Empty;
        
    }
}