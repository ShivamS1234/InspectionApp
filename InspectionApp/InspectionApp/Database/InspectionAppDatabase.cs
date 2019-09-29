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
        readonly SQLiteAsyncConnection database;

        public InspectionAppDatabase(string dbPath)
        {
            try
            {
                database = new SQLiteAsyncConnection(dbPath);
                database.CreateTableAsync<Company>().Wait();
                database.CreateTableAsync<InspectionApp.Model.UserRole>().Wait();
                database.CreateTableAsync<InspectionApp.Model.User>().Wait();
                database.CreateTableAsync<InspectionApp.Model.Producer>().Wait();
                database.CreateTableAsync<InspectionApp.Model.Product>().Wait();
                database.CreateTableAsync<InspectionApp.Model.Variety>().Wait();
                database.CreateTableAsync<InspectionApp.Model.Brand>().Wait();
                database.CreateTableAsync<InspectionApp.Model.CountryofOrigin>().Wait();
                database.CreateTableAsync<InspectionApp.Model.PalletCondition>().Wait();
                database.CreateTableAsync<InspectionApp.Model.InspectionHeaderTable>().Wait();
                database.CreateTableAsync<InspectionApp.Model.InspectionDetailTable>().Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

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