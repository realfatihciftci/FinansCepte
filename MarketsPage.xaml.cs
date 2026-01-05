using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using FinansCepte.Models;

namespace FinansCepte;

public partial class MarketsPage : ContentPage
{
    ObservableCollection<MarketItem> MyList = new ObservableCollection<MarketItem>();

    public MarketsPage()
    {
        InitializeComponent();
        MarketsList.ItemsSource = MyList; 
       
        LoadDoviz();
    }
    
    private void OnDovizClicked(object sender, EventArgs e)
    {
        SetActiveButton(BtnDoviz);
        LoadDoviz();
    }

    private void OnMadenClicked(object sender, EventArgs e)
    {
        SetActiveButton(BtnMaden);
        LoadMaden();
    }

    private void OnBorsaClicked(object sender, EventArgs e)
    {
        SetActiveButton(BtnBorsa);
        LoadBorsa();
    }
    
    void LoadDoviz()
    {
        MyList.Clear(); 
        MyList.Add(new MarketItem { Name = "ABD Doları", BuyPrice = "34.10", SellPrice = "34.50" });
        MyList.Add(new MarketItem { Name = "Euro", BuyPrice = "37.05", SellPrice = "37.40" });
        MyList.Add(new MarketItem { Name = "Sterlin", BuyPrice = "43.20", SellPrice = "43.80" });
    }

    void LoadMaden()
    {
        MyList.Clear();
        MyList.Add(new MarketItem { Name = "Gram Altın", BuyPrice = "2900", SellPrice = "2950" });
        MyList.Add(new MarketItem { Name = "Çeyrek Altın", BuyPrice = "4800", SellPrice = "4950" });
        MyList.Add(new MarketItem { Name = "Gümüş (Gram)", BuyPrice = "32.50", SellPrice = "33.10" });
    }

    void LoadBorsa()
    {
        MyList.Clear();
        MyList.Add(new MarketItem { Name = "THY (THYAO)", BuyPrice = "270.00", SellPrice = "270.50" });
        MyList.Add(new MarketItem { Name = "Aselsan", BuyPrice = "64.10", SellPrice = "64.20" });
        MyList.Add(new MarketItem { Name = "Garanti BBVA", BuyPrice = "112.50", SellPrice = "112.80" });
    }
    
    void SetActiveButton(Button activeButton)
    {
        BtnDoviz.BackgroundColor = Colors.Transparent;
        BtnDoviz.TextColor = Colors.Gray;

        BtnMaden.BackgroundColor = Colors.Transparent;
        BtnMaden.TextColor = Colors.Gray;

        BtnBorsa.BackgroundColor = Colors.Transparent;
        BtnBorsa.TextColor = Colors.Gray;
        
        activeButton.BackgroundColor = Color.FromArgb("#2D3447"); 
        activeButton.TextColor = Colors.White;
    }
}
