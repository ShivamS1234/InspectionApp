using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Inspection.Resouces.Entites;
using InspectionApp.Model;
using SQLite;

namespace InspectionApp.Database
{
  public class InspectionAppDatabase
  {
    private static SQLiteAsyncConnection _connectionAsync;
    private static SQLiteConnection _connection;
    public InspectionAppDatabase(string dbPath)
    {
      try
      {
        _connectionAsync = new SQLiteAsyncConnection(dbPath);
        _connectionAsync.CreateTableAsync<Company>().Wait();
        _connectionAsync.CreateTableAsync<InspectionApp.Model.UserRole>().Wait();
        _connectionAsync.CreateTableAsync<InspectionApp.Model.User>().Wait();
        _connectionAsync.CreateTableAsync<InspectionApp.Model.Producer>().Wait();
        _connectionAsync.CreateTableAsync<InspectionApp.Model.Product>().Wait();
        _connectionAsync.CreateTableAsync<InspectionApp.Model.Variety>().Wait();
        _connectionAsync.CreateTableAsync<InspectionApp.Model.Brand>().Wait();
        _connectionAsync.CreateTableAsync<InspectionApp.Model.CountryofOrigin>().Wait();
        _connectionAsync.CreateTableAsync<InspectionApp.Model.PalletCondition>().Wait();
        _connectionAsync.CreateTableAsync<InspectionApp.Model.InspectionHeaderTable>().Wait();
        _connectionAsync.CreateTableAsync<InspectionApp.Model.InspectionDetailTable>().Wait();
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }
    }

    #region save
    public async static Task SaveCompanies(IList<Company> Companys)
    {
      foreach (var account in Companys)
      {
        var result = await _connection.Table<Company>().Where(x => x.Id == account.Id).ToListAsync();
        if (result != null && result.Count > 0)
        {
          _connection.UpdateAsync(account);
        }
        else
        {
          _connection.InsertAsync(account);
        }
      }
    }
    public static void BulkSaveCompanies(IList<Company> Companys)
    {
      try
      {
        var count = Accounts.Select(a => a.AccountId).Distinct().Count();
        //var DistData= Accounts.Select(a => a.AccountId).Distinct();
        //var GropData = Accounts.GroupBy(a => a.AccountId).Select( b => b.Key);

        var gropdata = Accounts.GroupBy(info => info.AccountId).Select(group => new
        {
          AccountId = group.Key,
          Count = group.Count()
        }).Where(x => x.Count > 1);

        _connection.InsertAll(Accounts);

      }
    #endregion
      public Task<List<InspectionHeaderModel>> GetItemsAsync()
      {
        return database.Table<InspectionHeaderModel>().ToListAsync();
      }

      public Task<List<InspectionHeaderModel>> GetItemsNotDoneAsync()
      {
        return database.QueryAsync<InspectionHeaderModel>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
      }

      public Task<InspectionHeaderModel> GetItemAsync(int id)
      {
        return database.Table<InspectionHeaderModel>().Where(i => i.Id == id).FirstOrDefaultAsync();
      }

      public Task<int> SaveItemAsync(InspectionHeaderModel item)
      {
        if (item.Id != 0)
        {
          return database.UpdateAsync(item);
        }
        else
        {
          return database.InsertAsync(item);
        }
      }

      public Task<int> DeleteItemAsync(InspectionHeaderModel item)
      {
        return database.DeleteAsync(item);
      }
    }
  }