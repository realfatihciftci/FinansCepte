using SQLite;
using SQLitePCL;

namespace FinansCepte.Models;


public class Transaction
{
    [PrimaryKey, AutoIncrement]
    public int Id {get; set;}
    public string Type {get; set;}
    public string Title {get; set;}
    
    public string? Category {get; set;}
    public decimal Amount {get; set;}
    public DateTime Date {get; set;}
    
    public string? AssetName {get; set;}
    public double Quantity {get; set;}
    public decimal? UnitPrice {get; set;}
    
}