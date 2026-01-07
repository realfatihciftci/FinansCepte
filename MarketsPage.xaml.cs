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
        MyList.Add(new MarketItem { Name = "USD - ABD Doları", BuyPrice = "34.18", SellPrice = "34.22" });
        MyList.Add(new MarketItem { Name = "EUR - Euro", BuyPrice = "37.45", SellPrice = "37.52" });
        MyList.Add(new MarketItem { Name = "GBP - İngiliz Sterlini", BuyPrice = "43.10", SellPrice = "43.25" });
        MyList.Add(new MarketItem { Name = "CHF - İsviçre Frangı", BuyPrice = "38.50", SellPrice = "38.65" });
        MyList.Add(new MarketItem { Name = "CAD - Kanada Doları", BuyPrice = "25.10", SellPrice = "25.20" });
        MyList.Add(new MarketItem { Name = "JPY - Japon Yeni", BuyPrice = "0.22", SellPrice = "0.23" });
        MyList.Add(new MarketItem { Name = "SAR - S. Arabistan Riyali", BuyPrice = "9.10", SellPrice = "9.15" });
        MyList.Add(new MarketItem { Name = "AUD - Avustralya Doları", BuyPrice = "22.40", SellPrice = "22.50" });
        MyList.Add(new MarketItem { Name = "DKK - Danimarka Kronu", BuyPrice = "5.02", SellPrice = "5.05" });
        MyList.Add(new MarketItem { Name = "SEK - İsveç Kronu", BuyPrice = "3.25", SellPrice = "3.28" });
        MyList.Add(new MarketItem { Name = "RUB - Rus Rublesi", BuyPrice = "0.35", SellPrice = "0.37" });
    }

    void LoadMaden()
    {
        MyList.Clear();
        MyList.Add(new MarketItem { Name = "Gram Altın", BuyPrice = "2.950", SellPrice = "2.965" });
        MyList.Add(new MarketItem { Name = "Çeyrek Altın", BuyPrice = "4.800", SellPrice = "4.950" });
        MyList.Add(new MarketItem { Name = "Yarım Altın", BuyPrice = "9.600", SellPrice = "9.900" });
        MyList.Add(new MarketItem { Name = "Tam Altın", BuyPrice = "19.200", SellPrice = "19.500" });
        MyList.Add(new MarketItem { Name = "Cumhuriyet Altını", BuyPrice = "20.100", SellPrice = "20.500" });
        MyList.Add(new MarketItem { Name = "Ata Altın", BuyPrice = "20.300", SellPrice = "20.700" });
        MyList.Add(new MarketItem { Name = "Ons Altın ($)", BuyPrice = "2.650", SellPrice = "2.655" });
        MyList.Add(new MarketItem { Name = "Has Altın", BuyPrice = "2.940", SellPrice = "2.955" });
        MyList.Add(new MarketItem { Name = "22 Ayar Bilezik", BuyPrice = "2.750", SellPrice = "2.850" });
        MyList.Add(new MarketItem { Name = "Gümüş (Gram)", BuyPrice = "33.50", SellPrice = "34.10" });
    }

    void LoadBorsa()
    {
        MyList.Clear();
        MyList.Add(new MarketItem { Name = "BIST 100", BuyPrice = "9.850", SellPrice = "9.850" });
        MyList.Add(new MarketItem { Name = "THY (THYAO)", BuyPrice = "275.50", SellPrice = "276.00" });
        MyList.Add(new MarketItem { Name = "ASELSAN (ASELS)", BuyPrice = "64.10", SellPrice = "64.20" });
        MyList.Add(new MarketItem { Name = "GARANTİ BBVA", BuyPrice = "112.50", SellPrice = "112.80" });
        MyList.Add(new MarketItem { Name = "SASA Polyester", BuyPrice = "42.50", SellPrice = "42.60" });
        MyList.Add(new MarketItem { Name = "EREGLI Demir Çelik", BuyPrice = "52.30", SellPrice = "52.40" });
        MyList.Add(new MarketItem { Name = "TUPRAS", BuyPrice = "165.20", SellPrice = "165.80" });
        MyList.Add(new MarketItem { Name = "FORD OTOSAN", BuyPrice = "1050.00", SellPrice = "1055.00" });
        MyList.Add(new MarketItem { Name = "ŞİŞECAM", BuyPrice = "48.90", SellPrice = "49.00" });
        MyList.Add(new MarketItem { Name = "AKBANK", BuyPrice = "62.10", SellPrice = "62.40" });
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
