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
        static int CompanyName;
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

        public static async void GetAllInitData()
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Loading...");
                CompanyName = Convert.ToInt32(RememberMe.Get("CmpID"));
                CompaniesRequestDTO companiesRequestDTO = new CompaniesRequestDTO()
                {
                    IsActive = true,
                };
                var result = await webServiceManager.GetCompaniesbyAppIdAsync(companiesRequestDTO).ConfigureAwait(true);
                if (result.IsSuccess)
                {
                    if (result.Data.Companies.Count > 0)
                    {
                        CmpList = new List<Company>();
                        foreach (var data in result.Data.Companies)
                        {
                            CmpList.Add(data);
                        }
                    }
                }
                CommonRequestDTO BrandRequestDTO = new CommonRequestDTO()
                {
                    CompanyId = CompanyName
                };
                var resultBrand = await webServiceManager.GetBrandsbyCmpIdAsync(BrandRequestDTO).ConfigureAwait(true);
                if (resultBrand.IsSuccess)
                {
                    if (resultBrand.Data.Brand.Count > 0)
                    {
                        BrandList = new List<Brand>();
                        foreach (var data in resultBrand.Data.Brand)
                        {
                            BrandList.Add(data);
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
