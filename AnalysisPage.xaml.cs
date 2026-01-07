using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinansCepte.Data;
using FinansCepte.Models;
using System.Collections.ObjectModel;

namespace FinansCepte;

public partial class AnalysisPage : ContentPage
{
    private readonly LocalDbService _dbService;

    public class CategoryAnalysis
    {
        public string Category { get; set; }
        public decimal TotalAmount { get; set; }
        public double Percentage { get; set; } // 0.1 ile 1.0 arası
        public Color ColorCode { get; set; } // Bar rengi
    }
    
    public AnalysisPage(LocalDbService dbService)
    {
        InitializeComponent();
        _dbService = dbService;
    }
    
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadData();
    }

    private async Task LoadData()
    {
        // 1. Veritabanından verileri çek
        var transactions = await _dbService.GetTransactionsAsync();

        if (transactions.Count == 0) return;

        // 2. Gelir ve Gider Toplamları (DECIMAL olarak hesaplıyoruz)
        decimal totalIncome = transactions.Where(t => t.Type == "Gelir").Sum(t => t.Amount);
        decimal totalExpense = transactions.Where(t => t.Type == "Gider").Sum(t => t.Amount);

        // --- ÜST KART HESAPLAMALARI ---
        
        // Kalan Bütçe
        decimal remaining = totalIncome - totalExpense;
        LblRemaining.Text = $"Kalan Bütçe: {remaining:N0} ₺";

        // Yüzdelik Hesaplama (Burada (double) diyerek dönüşüm yapıyoruz)
        double usageRatio = totalIncome > 0 ? (double)(totalExpense / totalIncome) : 0;
        
        LblPercentage.Text = $"%{usageRatio * 100:N0}";
        PbLimit.Progress = usageRatio;

        // Renk Ayarı
        if (usageRatio > 1) 
        {
            LblRemaining.TextColor = Colors.Red;
            LblRemaining.Text = $"Limit aşıldı! ({remaining:N0} ₺)";
            PbLimit.ProgressColor = Colors.Red;
        }
        else
        {
            LblRemaining.TextColor = Color.FromArgb("#4CAF50"); // Yeşil
            PbLimit.ProgressColor = Color.FromArgb("#4CAF50");
        }

        // --- KATEGORİ LİSTESİ ---

        var expenseGroups = transactions
            .Where(t => t.Type == "Gider")
            .GroupBy(t => t.Category)
            .Select(g => new
            {
                Category = g.Key,
                Total = g.Sum(t => t.Amount)
            })
            .OrderByDescending(x => x.Total)
            .ToList();

        var displayList = new ObservableCollection<CategoryAnalysis>();
        
        var colors = new[] { "#64B5F6", "#FFB74D", "#BA68C8", "#E57373", "#4DB6AC", "#7986CB" };
        int colorIndex = 0;

        foreach (var item in expenseGroups)
        {
            // Yüzde hesaplarken decimal'i double'a çeviriyoruz
            double catPercentage = totalExpense > 0 ? (double)(item.Total / totalExpense) : 0;

            displayList.Add(new CategoryAnalysis
            {
                Category = item.Category,
                TotalAmount = item.Total, // Artık hata vermez, ikisi de decimal
                Percentage = catPercentage,
                ColorCode = Color.FromArgb(colors[colorIndex % colors.Length])
            });
            colorIndex++;
        }

        CvCategories.ItemsSource = displayList;

        // --- İPUCU KUTUSU ---
        
        var topCategory = expenseGroups.FirstOrDefault();
        if (topCategory != null)
        {
            LblTip.Text = $"İpucu: Bu ay en çok harcamayı {topCategory.Total:N0} ₺ ile '{topCategory.Category}' kategorisinde yaptınız.";
        }
        else
        {
            LblTip.Text = "Henüz yeterli veri yok.";
        }
    }
}