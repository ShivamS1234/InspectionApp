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
    public static List<StatusDetails> StatusList
    {
      get;
      set;
    }
    public static List<UserDetails> UserList
    {
      get;
      set;
    }
    public static List<Truck> TruckList
    {
      get;
      set;
    }

    //public static async void GetAllInitData()
    //{
    //  try
    //  {
    //    UserDialogs.Instance.ShowLoading("Loading...");
    //    WarehousesRequestDTO freightTbsRequestDTO = new WarehousesRequestDTO()
    //    {
    //      ApplicationId = (int)LoginPageViewModel.UserData.ApplicationId,
    //      CompanyId = (int)LoginPageViewModel.UserData.CompanyId,
    //    };
    //    var result = await webServiceManager.GetWarehousesListByAppIdCompIdAsync(freightTbsRequestDTO).ConfigureAwait(true);
    //    if (result.IsSuccess)
    //    {
    //      if (result.Data.WarehouseDetails.Count > 0)
    //      {
    //        WarehouseList = new List<WarehouseDetails>();
    //        foreach (var data in result.Data.WarehouseDetails)
    //        {
    //          WarehouseList.Add(data);
    //        }
    //      }
    //    }
    //    ClientRequestDTO clientResponseDTO = new ClientRequestDTO()
    //    {
    //      ApplicationId = (int)LoginPageViewModel.UserData.ApplicationId,
    //      CompanyId = (int)LoginPageViewModel.UserData.CompanyId,
    //    };
    //    var resultClient = await webServiceManager.GetClientListByAppIdCompIdAsync(clientResponseDTO).ConfigureAwait(true);
    //    if (resultClient.IsSuccess)
    //    {
    //      if (resultClient.Data.ClientDetails.Count > 0)
    //      {
    //        ClientList = new List<ClientDetails>();
    //        foreach (var data in resultClient.Data.ClientDetails)
    //        {
    //          ClientList.Add(data);
    //        }
    //      }
    //    }

    //    StatusRequestDTO statusTbsRequestDTO = new StatusRequestDTO()
    //    {
    //      ApplicationId = (int)LoginPageViewModel.UserData.ApplicationId,
    //      CompanyId = (int)LoginPageViewModel.UserData.CompanyId,
    //    };
    //    var resultStatus = await webServiceManager.GetStatusListByAppIdCompIdAsync(statusTbsRequestDTO).ConfigureAwait(true);
    //    if (resultStatus.IsSuccess)
    //    {
    //      if (resultStatus.Data.LstStatusDetails.Count > 0)
    //      {
    //        StatusList = new List<StatusDetails>();
    //        foreach (var data in resultStatus.Data.LstStatusDetails)
    //        {
    //          StatusList.Add(data);
    //        }
    //      }
    //    }
    //    //load user location
    //    GetAllUserLocation();
    //    //TruckRequestDTO truckRequestDTO = new TruckRequestDTO()
    //    //{
    //    //  appid = (int)LoginPageViewModel.UserData.ApplicationId,
    //    //  companyid = (int)LoginPageViewModel.UserData.CompanyId,
    //    //  IsActive = true
    //    //};
    //    var resultTruck = await webServiceManager.webService.GetAsync<TruckResponseDTO>("Truck/GetAllTruck", "");
    //    //var resultTruck = await webServiceManager.GetTruckbyID(truckRequestDTO).ConfigureAwait(true);
    //    if (resultTruck.IsSuccess && resultTruck.Data.TruckList != null)
    //    {
    //      if (resultTruck.Data.TruckList.Count > 0)
    //      {
    //        TruckList = new List<Truck>();
    //        foreach (var data in resultTruck.Data.TruckList)
    //        {
    //          TruckList.Add(data);
    //        }
    //      }
    //    }
    //    UserDialogs.Instance.HideLoading();
    //  }
    //  catch (Exception ex)
    //  {
    //    UserDialogs.Instance.HideLoading();
    //    Console.WriteLine(ex.Message);
    //    throw ex;
    //  }
    //}
    //public static async void GetAllUserLocation()
    //{
    //  try
    //  {
    //    UsersRequestDTO userTbsRequestDTO = new UsersRequestDTO()
    //    {
    //      ApplicationId = (int)LoginPageViewModel.UserData.ApplicationId,
    //      CompanyId = (int)LoginPageViewModel.UserData.CompanyId,
    //      UserRoleId = (int)LoginPageViewModel.UserData.UserRoleId,
    //      Id = (int)LoginPageViewModel.UserData.Id,
    //    };
    //    var resultUser = await webServiceManager.userTbsRequestDTOAsync(userTbsRequestDTO).ConfigureAwait(true);
    //    if (resultUser.IsSuccess)
    //    {

    //      if (resultUser.Data.UserDetails.Count > 0)
    //      {
    //        UserList = new List<UserDetails>();
    //        foreach (var data in resultUser.Data.UserDetails)
    //        {
    //          UserList.Add(data);
    //        }
    //      }
    //    }
    //  }
    //  catch (Exception ex)
    //  {
    //    UserDialogs.Instance.HideLoading();
    //    Console.WriteLine(ex.Message);
    //    throw ex;
    //  }
    //}
  }
}
