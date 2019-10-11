﻿using System;
using InspectionApp.ViewModel;
using InspectionApp.WebServices;
using Inspection.Resouces.DTO.Request;
using System.Collections.Generic;
using Inspection.Resouces.Entites.CustomModel;
using Acr.UserDialogs;
using Inspection.Resouces.Entites;
using InspectionApp.Database;
using System.Collections.ObjectModel;
using InspectionApp.Model;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace InspectionApp.Helpers
{
  public static class InitData
  {
    #region propertise
    static int CompanyId;
    static WebServiceManager webServiceManager = new WebServiceManager();
    public static List<WarehouseDetails> WarehouseList
    {
      get;
      set;
    }
    public static List<ClientDetails> ClientList
    {
      get;
      set;
    }
    public static List<UserDetails> UserList
    {
      get;
      set;
    }
    public static IDatabaseService<Model.Company> CmpRepo => new InspectionAppDatabase<Model.Company>(App1.DBPath);
    public static ObservableCollection<InspectionApp.Model.Company> CmpList
    {
      get;
      set;
    }
    public static IDatabaseService<Model.Brand> BranRepo => new InspectionAppDatabase<Model.Brand>(App1.DBPath);
    public static ObservableCollection<Model.Brand> BrandList
    {
      get;
      set;
    }
    public static IDatabaseService<Model.CountryofOrigin> CountryOriginRepo => new InspectionAppDatabase<Model.CountryofOrigin>(App1.DBPath);
    public static ObservableCollection<Model.CountryofOrigin> CountryofOriginList
    {
      get;
      set;
    }
    public static IDatabaseService<Model.OpeningApperence> OpeningApperenceRepo => new InspectionAppDatabase<Model.OpeningApperence>(App1.DBPath);
    public static ObservableCollection<Model.OpeningApperence> OpeningApperenceList
    {
      get;
      set;
    }
    public static IDatabaseService<Model.PackageCondition> PackageConditionRepo => new InspectionAppDatabase<Model.PackageCondition>(App1.DBPath);
    public static ObservableCollection<Model.PackageCondition> PackageConditionList
    {
      get;
      set;
    }
    public static IDatabaseService<Model.PalletCondition> PalletRepo => new InspectionAppDatabase<Model.PalletCondition>(App1.DBPath);
    public static ObservableCollection<Model.PalletCondition> PalletConditionList
    {
      get;
      set;
    }
    public static IDatabaseService<Model.Producer> ProducerRepo => new InspectionAppDatabase<Model.Producer>(App1.DBPath);
    public static ObservableCollection<Model.Producer> ProducerList
    {
      get;
      set;
    }
    public static IDatabaseService<Model.Product> ProductRepo => new InspectionAppDatabase<Model.Product>(App1.DBPath);
    public static ObservableCollection<Model.Product> ProductList
    {
      get;
      set;
    }
    public static IDatabaseService<Model.SizeTb> SizeRepo => new InspectionAppDatabase<Model.SizeTb>(App1.DBPath);
    public static ObservableCollection<Model.SizeTb> SizeTbList
    {
      get;
      set;
    }
    public static IDatabaseService<Model.Variety> VarietyRepo => new InspectionAppDatabase<Model.Variety>(App1.DBPath);
    public static ObservableCollection<Model.Variety> VarietyList
    {
      get;
      set;
    }
    public static IDatabaseService<InspectionHeaderTable> HeaderRepo => new InspectionAppDatabase<InspectionHeaderTable>(App1.DBPath);
    public static ObservableCollection<InspectionHeaderTable> InspectionHeaderList
    {
      get;
      set;
    }
    public static IDatabaseService<InspectionDetailTable> DetailsRepo => new InspectionAppDatabase<InspectionDetailTable>(App1.DBPath);
    public static ObservableCollection<InspectionDetailTable> InspectionDetailsList
    {
      get;
      set;
    }
    #endregion

    #region method

    public static async void GetAllInitData()
    {
      try
      {
        UserDialogs.Instance.ShowLoading("Going for Databse Setup...");
        CmpList = new ObservableCollection<Model.Company>();
        BrandList = new ObservableCollection<Model.Brand>();
        CountryofOriginList = new ObservableCollection<Model.CountryofOrigin>();
        OpeningApperenceList = new ObservableCollection<Model.OpeningApperence>();
        PackageConditionList = new ObservableCollection<Model.PackageCondition>();
        PalletConditionList = new ObservableCollection<Model.PalletCondition>();
        ProducerList = new ObservableCollection<Model.Producer>();
        ProductList = new ObservableCollection<Model.Product>();
        SizeTbList = new ObservableCollection<Model.SizeTb>();
        VarietyList = new ObservableCollection<Model.Variety>();
        InspectionHeaderList = new ObservableCollection<InspectionHeaderTable>();
        InspectionDetailsList = new ObservableCollection<InspectionDetailTable>();

        if (ConfigurationCommon.App_Online)
        {
          #region for_cmp
          CompaniesRequestDTO companiesRequestDTO = new CompaniesRequestDTO()
          {
            IsActive = true
          };
          var resultApplication = await webServiceManager.GetCompaniesbyAppIdAsync(companiesRequestDTO).ConfigureAwait(true);
          foreach (var item in resultApplication.Data.Companies)
          {
            CmpList.Add(new Model.Company
            {
              Id = item.Id,
              CompanyAddress = item.CompanyAddress,
              CompanyEmail = item.CompanyEmail,
              CompanyName = item.CompanyName,
              IsActive = item.IsActive,
            });
          }
          await CmpRepo.DeleteAllAsync<InspectionApp.Model.Company>();
          await CmpRepo.InsertAll(CmpList); // for cmp data saving
          #endregion

          CompanyId = Convert.ToInt32(RememberMe.Get("CmpID"));
          CommonRequestDTO commonRequestDTO = new CommonRequestDTO()
          {
            CompanyId = CompanyId
          };
          var result = await webServiceManager.GetAllDatabyCmpIdAsync(commonRequestDTO).ConfigureAwait(true);
          if (result.IsSuccess)
          {
            if (result.Data.Brand.Count > 0)
            {
              foreach (var data in result.Data.Brand)
              {
                BrandList.Add(new Model.Brand
                {
                  Id = data.Id,
                  BrandName = data.BrandName,
                  CompanyId = data.CompanyId,
                  ProductID = data.ProductID,
                  VarietyId = data.VarietyId,

                });
              }
              await BranRepo.DeleteAllAsync<InspectionApp.Model.Brand>();
              await BranRepo.InsertAll(BrandList);
            }
            if (result.Data.CountryofOrigin.Count > 0)
            {
              foreach (var data in result.Data.CountryofOrigin)
              {
                CountryofOriginList.Add(new Model.CountryofOrigin
                {
                  Id = data.Id,
                  CompanyId = data.CompanyId,
                  CountryName = data.CountryName
                });
              }
              await BranRepo.DeleteAllAsync<InspectionApp.Model.Brand>();
              await BranRepo.InsertAll(BrandList);
            }
            if (result.Data.OpeningApperence.Count > 0)
            {
              foreach (var data in result.Data.OpeningApperence)
              {
                OpeningApperenceList.Add(new Model.OpeningApperence
                {
                  Id = data.Id,
                  ApperenceDescription = data.ApperenceDescription,
                  CompanyId = data.CompanyId
                });
              }
              await OpeningApperenceRepo.DeleteAllAsync<Model.OpeningApperence>();
              await OpeningApperenceRepo.InsertAll(OpeningApperenceList);
            }
            if (result.Data.PackageCondition.Count > 0)
            {
              foreach (var data in result.Data.PackageCondition)
              {
                PackageConditionList.Add(new Model.PackageCondition
                {
                  Id = data.Id,
                  PackageConditionName = data.PackageConditionName,
                  CompanyId = data.CompanyId
                });
              }
              await PackageConditionRepo.DeleteAllAsync<Model.PackageCondition>();
              await PackageConditionRepo.InsertAll(PackageConditionList);
            }
            if (result.Data.PackageCondition.Count > 0)
            {
              foreach (var data in result.Data.PalletCondition)
              {
                PalletConditionList.Add(new Model.PalletCondition
                {
                  Id = data.Id,
                  CompanyId = data.CompanyId,
                  PalletConditionName = data.PalletConditionName
                }
                  );
                await PalletRepo.DeleteAllAsync<Model.PalletCondition>();
                await PalletRepo.InsertAll(PalletConditionList);
              }
            }
            if (result.Data.PackageCondition.Count > 0)
            {
              foreach (var data in result.Data.Producer)
              {
                ProducerList.Add(new Model.Producer
                {
                  Id = data.Id,
                  CompanyId = data.CompanyId,
                  ProducerName = data.ProducerName
                });
              }
              await ProducerRepo.DeleteAllAsync<Model.Producer>();
              await ProducerRepo.InsertAll(ProducerList);
            }
            if (result.Data.PackageCondition.Count > 0)
            {
              foreach (var data in result.Data.Product)
              {
                ProductList.Add(new Model.Product
                {
                  Id = data.Id,
                  CompanyId = data.CompanyId,
                  ProductName = data.ProductName
                });
              }
              await ProductRepo.DeleteAllAsync<Model.Product>();
              await ProductRepo.InsertAll(ProductList);
            }
            if (result.Data.PackageCondition.Count > 0)
            {
              foreach (var data in result.Data.SizeTb)
              {
                SizeTbList.Add(new Model.SizeTb
                {
                  Id = data.Id,
                  CompanyId = data.CompanyId,
                  SizeDescription = data.SizeDescription
                });
              }
              await SizeRepo.DeleteAllAsync<Model.SizeTb>();
              await SizeRepo.InsertAll(SizeTbList);
            }
            if (result.Data.Variety.Count > 0)
            {
              foreach (var data in result.Data.Variety)
              {
                VarietyList.Add(new Model.Variety
                {
                  Id = data.Id,
                  CompanyId = data.CompanyId,
                  ProductID = data.ProductID,
                  VarietyName = data.VarietyName
                });
              }
              await VarietyRepo.DeleteAllAsync<Model.Variety>();
              await VarietyRepo.InsertAll(VarietyList);
            }
          }
          await SyncHeaderDetails();
        }
        else
        {
          var cmpdata = await CmpRepo.Get<Model.Company>();
          if (cmpdata != null)
          {
            CmpList = cmpdata;
          }

          var Producerdata = await ProducerRepo.Get<Model.Producer>();
          if (Producerdata != null)
          {
            ProducerList = Producerdata;
          }

          var Productdata = await ProductRepo.Get<Model.Product>();
          if (Productdata != null)
          {
            ProductList = Productdata;
          }

          var Branddata = await BranRepo.Get<Model.Brand>();
          if (Branddata != null)
          {
            BrandList = Branddata;
          }

          var CountryOrigindata = await CountryOriginRepo.Get<Model.CountryofOrigin>();
          if (CountryOrigindata != null)
          {
            CountryofOriginList = CountryOrigindata;
          }

          var OpeningApperencedata = await OpeningApperenceRepo.Get<Model.OpeningApperence>();
          if (OpeningApperencedata != null)
          {
            OpeningApperenceList = OpeningApperencedata;
          }

          var PackageConditiondata = await PackageConditionRepo.Get<Model.PackageCondition>();
          if (PackageConditiondata != null)
          {
            PackageConditionList = PackageConditiondata;
          }

          var Palletdata = await PalletRepo.Get<Model.PalletCondition>();
          if (Palletdata != null)
          {
            PalletConditionList = Palletdata;
          }

          var SizeTbdata = await SizeRepo.Get<Model.SizeTb>();
          if (SizeTbdata != null)
          {
            SizeTbList = SizeTbdata;
          }

          var Varietydata = await VarietyRepo.Get<Model.Variety>();
          if (Varietydata != null)
          {
            VarietyList = Varietydata;
          }
        }
        RememberMe.Set("isDatabaseUpdate", true);
        UserDialogs.Instance.HideLoading();
      }
      catch (Exception ex)
      {
        UserDialogs.Instance.HideLoading();
        Console.WriteLine("Error in Master Data Sync: " + ex.Message);
        throw ex;
      }
    }

    #region Save_HeaderAndDetails_Data
    static async Task SyncHeaderDetails()
    {
      InspectionHeadersRequestDTO headerRequestDTO = new InspectionHeadersRequestDTO()
      {
        CompanyId = Convert.ToInt32(RememberMe.Get("CmpID")),
        UserId = Convert.ToInt32(RememberMe.Get("userID")),
      };
      var result = await webServiceManager.GetHeaderbyID(headerRequestDTO).ConfigureAwait(true);
      if (result.IsSuccess)
      {
        if (result.Data.InspectionHeader != null && result.Data.InspectionHeader.Count > 0)
        {
          InspectionHeaderList = new ObservableCollection<InspectionHeaderTable>();
          foreach (var data in result.Data.InspectionHeader)
          {
            InspectionHeaderList.Add(new InspectionHeaderTable
            {
              Id = data.Id,
              CompanyId = data.CompanyId,
              InspectionDate = data.InspectionDate,
              UserId = data.UserId,
              Invoice = data.Invoice,
              ProducerId = data.ProducerId,
              VarietyId = data.VarietyId,
              BrandId = data.BrandId,
              CountryofOriginId = data.CountryofOriginId,
              TotalBoxQuantities = data.TotalBoxQuantities,
              TempOnCaja = data.TempOnCaja,
              TempOnTermografo = data.TempOnTermografo,
              PalletizingConditionId = data.PalletizingConditionId,
            });
          }
        }
        await HeaderRepo.DeleteAllAsync<InspectionHeaderTable>();
        await HeaderRepo.InsertAll(InspectionHeaderList);

        var resultDetails = await webServiceManager.GetHeaderDetailsAll().ConfigureAwait(true);
        if (resultDetails.IsSuccess)
        {
          if (resultDetails.Data.InspectionDetail != null && resultDetails.Data.InspectionDetail.Count > 0)
          {
            InspectionDetailsList = new ObservableCollection<InspectionDetailTable>();
            foreach (var data in resultDetails.Data.InspectionDetail)
            {
              InspectionDetailsList.Add(new InspectionDetailTable
              {
                Id = data.Id,
                InspectionHeaderId = data.InspectionHeaderId,
                SizeId = data.SizeId,
                SampleSize = data.SampleSize,
                Weight = data.Weight,
                PhysicalCount = data.PhysicalCount,
                OpeningApperenceId = data.OpeningApperenceId,
                Temperature = data.Temperature,
                Brix = data.Brix,
                Firmness = data.Firmness,
                SkinDamage = data.SkinDamage,
                Color = data.Color,
                PackageConditionId = data.PackageConditionId,
                Comment = data.Comment,
                QualityScore = data.QualityScore,
              });
            }
          }
          await DetailsRepo.DeleteAllAsync<InspectionDetailTable>();
          await DetailsRepo.InsertAll(InspectionDetailsList);
        }
      }
      else
      {
        Console.WriteLine("Prolem in header and details sync");
      }
    }
    #endregion

    #endregion
  }
}
