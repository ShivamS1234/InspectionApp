using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Inspection.Resouces.Entites;
using InspectionApp.Model;
using SQLite;
using System.Linq;
using InspectionApp.WebServices;
using System.Linq.Expressions;
using System.Collections.ObjectModel;

namespace InspectionApp.Database
{
  public class InspectionAppDatabase<T> : IDatabaseService<T> where T : class, new()
  {
    #region propertise
    private static SQLiteAsyncConnection _connectionAsync;
    private static SQLiteConnection _connection;
    #endregion

    #region constructore
    public InspectionAppDatabase(string dbPath)
    {
      try
      {
        _connectionAsync = new SQLiteAsyncConnection(dbPath); // for the async type
        _connection = new SQLiteConnection(dbPath); // for the non aysnc
        //master table
        _connectionAsync.CreateTableAsync<Model.Company>().Wait();
        _connectionAsync.CreateTableAsync<Model.UserRole>().Wait();
        _connectionAsync.CreateTableAsync<Model.User>().Wait();
        _connectionAsync.CreateTableAsync<Model.Producer>().Wait();
        _connectionAsync.CreateTableAsync<Model.Product>().Wait();
        _connectionAsync.CreateTableAsync<Model.Variety>().Wait();
        _connectionAsync.CreateTableAsync<Model.Brand>().Wait();
        _connectionAsync.CreateTableAsync<Model.CountryofOrigin>().Wait();
        _connectionAsync.CreateTableAsync<Model.OpeningApperence>().Wait();
        _connectionAsync.CreateTableAsync<Model.PackageCondition>().Wait();
        _connectionAsync.CreateTableAsync<Model.PalletCondition>().Wait();
        _connectionAsync.CreateTableAsync<Model.SizeTb>().Wait();
        _connectionAsync.CreateTableAsync<Model.Variety>().Wait();
        //transaction table
        _connectionAsync.CreateTableAsync<InspectionHeaderTable>().Wait();
        _connectionAsync.CreateTableAsync<InspectionDetailTable>().Wait();
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }
    }
    #endregion

    #region save
    public async static Task SaveCompanies(IList<InspectionApp.Model.Company> Companys)
    {
      foreach (var account in Companys)
      {
        var result = _connection.Table<InspectionApp.Model.Company>().Where(x => x.Id == account.Id).ToList();
        if (result != null && result.Count > 0)
        {
          _connection.Update(account);
        }
        else
        {
          _connection.Insert(account);
        }
      }
    }
    public void BulkSaveCompanies(IList<InspectionApp.Model.Company> Companys)
    {
      try
      {
        //var gropdata = Accounts.GroupBy(info => info.AccountId).Select(group => new
        //{
        //    AccountId = group.Key,
        //    Count = group.Count()
        //}).Where(x => x.Count > 1);
        var data = _connection.Table<InspectionApp.Model.Company>().ToList();
        _connection.InsertAll(Companys);

      }
      catch (Exception ex)
      {
        Console.WriteLine("Error in Cmp: " + ex.Message);
      }
    }

    public async Task InsertAll(IList<T> instance)
    {
      try
      {
        //var gropdata = Accounts.GroupBy(info => info.AccountId).Select(group => new
        //{
        //    AccountId = group.Key,
        //    Count = group.Count()
        //}).Where(x => x.Count > 1);
        _connection.InsertAll(instance);

      }
      catch (Exception ex)
      {
        Console.WriteLine("Error in Brand: " + ex.Message);
      }
    }

    public async Task<int> Update(T entity)
    {
      return await _connectionAsync.UpdateAsync(entity);
    }
    #endregion

    #region delete
    //public Task<int> DeleteAll(bool company = true, bool Brand = true, bool user = true, bool userRole = true,
    //    bool producer = true, bool product = true, bool variety = true, bool countryofOrigin = true,
    //    bool header = true, bool detailsHeader = true)
    //{
    //    if (company)
    //        _connectionAsync.DeleteAllAsync<Company>();
    //    else if (Brand)
    //        _connectionAsync.DeleteAllAsync<InspectionApp.Model.Brand>();
    //    else if (user)
    //        _connectionAsync.DeleteAllAsync<InspectionApp.Model.User>();
    //    else if (userRole)
    //        _connectionAsync.DeleteAllAsync<InspectionApp.Model.UserRole>();
    //    else if (producer)
    //        _connectionAsync.DeleteAllAsync<InspectionApp.Model.Producer>();
    //    else if (product)
    //        _connectionAsync.DeleteAllAsync<InspectionApp.Model.Product>();
    //    else if (variety)
    //        _connectionAsync.DeleteAllAsync<InspectionApp.Model.Variety>();
    //    else if (countryofOrigin)
    //        _connectionAsync.DeleteAllAsync<InspectionApp.Model.CountryofOrigin>();
    //}

    public async Task<int> DeleteAllAsync<T>()
    {
      var result = await _connectionAsync.DeleteAllAsync<T>();
      return result;
    }

    #endregion

    #region Get
    public AsyncTableQuery<T> AsQueryable()
    {
      return _connectionAsync.Table<T>();
    }
    //public AsyncTableQuery<T> AsQueryable() =>
    //        _connectionAsync.Table<T>();

    public async Task<int> Count(Expression<Func<T, bool>> predicate = null)
    {
      var query = _connectionAsync.Table<T>();
      if (predicate != null)
      {
        query = query.Where(predicate);
      }
      return await query.CountAsync();
    }

    public async Task<List<T>> Get()
    {
      return await _connectionAsync.Table<T>().ToListAsync();
    }

    public async Task<T> Get(Expression<Func<T, bool>> predicate)
    {
      return await _connectionAsync.FindAsync<T>(predicate);
    }

    public async Task<T> Get(int id)
    {
      return await _connectionAsync.FindAsync<T>(id);
    }

    public async Task<ObservableCollection<T>> Get<TValue>(Expression<Func<T, bool>> predicate = null, Expression<Func<T, TValue>> orderBy = null)
    {
      var query = _connectionAsync.Table<T>();

      if (predicate != null)
      {
        query = query.Where(predicate);
      }
      if (orderBy != null)
      {
        query = query.OrderBy<TValue>(orderBy);
      }

      var collection = new ObservableCollection<T>();
      var items = await query.ToListAsync();
      foreach (var item in items)
      {
        collection.Add(item);
      }

      return collection;
    }

    public Task<InspectionHeaderModel> GetItemAsync(int id)
    {
      return _connectionAsync.Table<InspectionHeaderModel>().Where(i => i.Id == id).FirstOrDefaultAsync();
    }
    #endregion


    public Task<int> SaveItemAsync(InspectionHeaderModel item)
    {
      if (item.Id != 0)
      {
        return _connectionAsync.UpdateAsync(item);
      }
      else
      {
        return _connectionAsync.InsertAsync(item);
      }
    }

    public Task<int> DeleteItemAsync(InspectionHeaderModel item)
    {
      return _connectionAsync.DeleteAsync(item);
    }
  }
}