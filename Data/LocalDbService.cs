using SQLitePCL;
using FinansCepte.Models;
using SQLite;

namespace FinansCepte.Data;

public class LocalDbService
{
    private const string DB_NAME = "FinansCepte";
    private readonly SQLiteAsyncConnection _connection;

    public LocalDbService()
    {
        string dbPath = Path.Combine(FileSystem.AppDataDirectory, DB_NAME);
        _connection = new SQLiteAsyncConnection(dbPath);
        _connection.CreateTableAsync<Transaction>();
    }

    public async Task<List<Transaction>> GetTransactionsAsync()
    {
        return await _connection.Table<Transaction>().ToListAsync();
    }

    public async Task CreateTransactionAsync(Transaction transaction)
    {
        await _connection.InsertAsync(transaction);
    }
}