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


        decimal income = transactions
            .Where((t => t.Type == "Gelir"))
            .Sum(t => t.Amount);

        decimal expense = transactions
            .Where(t => t.Type == "Gider")
            .Sum(t => t.Amount);
        decimal investment = transactions.Where(t => t.Type == "Yatırım").Sum(t => t.Amount);

        decimal balance = (income -(expense + investment));

        LblTotalBalance.Text = balance.ToString("N2") + "₺";
        LblTotalIncome.Text = income.ToString("N2") + "₺";
        LblTotalExpense.Text = expense.ToString("N2") + "₺";

        if (balance < 0)
            LblTotalBalance.TextColor = Colors.Red;
        else
            LblTotalBalance.TextColor = Colors.Green;

    }
    
    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var transaction = button?.CommandParameter as Transaction;

        if (transaction != null)
        {
            bool answer = await DisplayAlert("Sil", "Bu kaydı silmek istiyor musunuz?", "Evet", "Hayır");

            if (answer)
            {
                await _dbService.DeleteTransactionAsync(transaction);
                await LoadData();
            }
        }
    }
}