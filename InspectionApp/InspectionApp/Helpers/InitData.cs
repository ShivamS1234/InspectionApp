using System;
using InspectionApp.ViewModel;
using InspectionApp.WebServices;
using Inspection.Resouces.DTO.Request;
using System.Collections.Generic;
using Inspection.Resouces.Entites.CustomModel;
using Acr.UserDialogs;
using Inspection.Resouces.Entites;

namespace InspectionApp.Helpers
{
  public static class InitData
  {
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
    public static List<Company> CmpList
    {
      get;
      set;
    }
    public static List<Brand> BrandList
    {
      get;
      set;
    }
    public static List<CountryofOrigin> CountryofOriginList
    {
      get;
      set;
    }
    public static List<OpeningApperence> OpeningApperenceList
    {
      get;
      set;
    }
    public static List<PackageCondition> PackageConditionList
    {
      get;
      set;
    }
    public static List<PalletCondition> PalletConditionList
    {
      get;
      set;
    }
    public static List<Producer> ProducerList
    {
      get;
      set;
    }
    public static List<Product> ProductList
    {
      get;
      set;
    }
    public static List<SizeTb> SizeTbList
    {
      get;
      set;
    }
    public static List<Variety> VarietyList
    {
      get;
      set;
    }


    public static async void GetAllInitData()
    {
      try
      {
        UserDialogs.Instance.ShowLoading("Loading...");
        CompaniesRequestDTO companiesRequestDTO = new CompaniesRequestDTO()
        {
          IsActive = true
        };
        var resultApplication = await webServiceManager.GetCompaniesbyAppIdAsync(companiesRequestDTO).ConfigureAwait(true); ;
        CmpList = resultApplication.Data.Companies;

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
            BrandList = new List<Brand>();
            foreach (var data in result.Data.Brand)
            {
              BrandList.Add(data);
            }
          }
          if (result.Data.CountryofOrigin.Count > 0)
          {
            CountryofOriginList = new List<CountryofOrigin>();
            foreach (var data in result.Data.CountryofOrigin)
            {
              CountryofOriginList.Add(data);
            }
          }
          if (result.Data.OpeningApperence.Count > 0)
          {
            OpeningApperenceList = new List<OpeningApperence>();
            foreach (var data in result.Data.OpeningApperence)
            {
              OpeningApperenceList.Add(data);
            }
          }
          if (result.Data.PackageCondition.Count > 0)
          {
            PackageConditionList = new List<PackageCondition>();
            foreach (var data in result.Data.PackageCondition)
            {
              PackageConditionList.Add(data);
            }
          }
          if (result.Data.PackageCondition.Count > 0)
          {
            PalletConditionList = new List<PalletCondition>();
            foreach (var data in result.Data.PalletCondition)
            {
              PalletConditionList.Add(data);
            }
          }
          if (result.Data.PackageCondition.Count > 0)
          {
            ProducerList = new List<Producer>();
            foreach (var data in result.Data.Producer)
            {
              ProducerList.Add(data);
            }
          }
          if (result.Data.PackageCondition.Count > 0)
          {
            ProductList = new List<Product>();
            foreach (var data in result.Data.Product)
            {
              ProductList.Add(data);
            }
          }
          if (result.Data.PackageCondition.Count > 0)
          {
            SizeTbList = new List<SizeTb>();
            foreach (var data in result.Data.SizeTb)
            {
              SizeTbList.Add(data);
            }
          }
          if (result.Data.Variety.Count > 0)
          {
            VarietyList = new List<Variety>();
            foreach (var data in result.Data.Variety)
            {
              VarietyList.Add(data);
            }
          }
        }
        UserDialogs.Instance.HideLoading();
      }
      catch (Exception ex)
      {
        UserDialogs.Instance.HideLoading();
        Console.WriteLine(ex.Message);
        throw ex;
      }
    }

  }
}
