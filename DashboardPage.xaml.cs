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
    public DashboardPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadData();
    }

    void LoadData()
    {
        var transactions = FakeDb.Transactions;
        
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