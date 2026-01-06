using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinansCepte.Data;
using FinansCepte.Models;
using System.Linq;

namespace FinansCepte;

public partial class DashboardPage : ContentPage
{
    private readonly LocalDbService _dbService;
    public DashboardPage(LocalDbService dbService)
    {
        InitializeComponent();
        _dbService = dbService;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadData();
    }

    async Task LoadData()
    {
        var transactions = await _dbService.GetTransactionsAsync();

        if (CvTransactions != null)
        {
            CvTransactions.ItemsSource = transactions.OrderByDescending(t => t.Date).ToList();
        }
        
        
        decimal totalIncome = transactions
            .Where((t => t.Type == "Gelir"))
            .Sum(t => t.Amount);
        
        decimal totalExpense = transactions
            .Where(t => t.Type == "Gider")
            .Sum(t => t.Amount);
        
        decimal balance = totalIncome - totalExpense;

        LblTotalIncome.Text = totalIncome.ToString("N2") + "₺";
        LblTotalExpense.Text = totalExpense.ToString("N2") + "₺";
        LblTotalBalance.Text = balance.ToString("N2") + "₺";

        if (balance < 0) 
            LblTotalBalance.TextColor = Colors.Red;
        else 
            LblTotalBalance.TextColor = Colors.Green;

    }
}