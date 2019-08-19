﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Inspection.Resouces.DTO.Request;
using Inspection.Resouces.DTO.Response;
using Newtonsoft.Json;

namespace InspectionApp.WebServices
{
  public class WebServiceManager : ServiceConfigrations
  {
    public IWebService webService;
    public WebServiceManager()
    {
      webService = new WebService();
    }

    public async Task<WebServiceResult<CompaniesResponseDTO>> GetCompaniesbyAppIdAsync(CompaniesRequestDTO companiesRequestDTO)
    {
      UTF8Encoding encoder = new UTF8Encoding();
      byte[] data = encoder.GetBytes(JsonConvert.SerializeObject(companiesRequestDTO));
      string utfString = Encoding.UTF8.GetString(data, 0, data.Length);

      var result = await webService.PostAsync<CompaniesResponseDTO>("Company/GetCompaniesbyAppId", utfString);
      return result;
    }
    public async Task<WebServiceResult<UsersResponseDTO>> RegistrationAsync(UsersRequestDTO companiesRequestDTO)
    {
      UTF8Encoding encoder = new UTF8Encoding();
      byte[] data = encoder.GetBytes(JsonConvert.SerializeObject(companiesRequestDTO));
      string utfString = Encoding.UTF8.GetString(data, 0, data.Length);

      var result = await webService.PostAsync<UsersResponseDTO>("User/InsertUpdateUser", utfString);
      return result;
    }
    public async Task<WebServiceResult<UsersResponseDTO>> LoginAsync(UsersRequestDTO userRequestDTO)
    {
      UTF8Encoding encoder = new UTF8Encoding();
      byte[] data = encoder.GetBytes(JsonConvert.SerializeObject(userRequestDTO));
      string utfString = Encoding.UTF8.GetString(data, 0, data.Length);

      var result = await webService.PostAsync<UsersResponseDTO>("User/LoginByUserNamePassword", utfString);
      return result;
    }
    public async Task<WebServiceResult<UsersResponseDTO>> userTbsRequestDTOAsync(UsersRequestDTO userRequestDTO)
    {
      UTF8Encoding encoder = new UTF8Encoding();
      byte[] data = encoder.GetBytes(JsonConvert.SerializeObject(userRequestDTO));
      string utfString = Encoding.UTF8.GetString(data, 0, data.Length);

      var result = await webService.PostAsync<UsersResponseDTO>("User/GetUserListByUserIdAppIdCompIdRoleId", utfString);
      return result;
    }
    public async Task<WebServiceResult<SendMailResponseDTO>> ProcessAuthEmail(SendMailRequestDTO sendMailRequestDTO)
    {
      UTF8Encoding encoder = new UTF8Encoding();
      byte[] data = encoder.GetBytes(JsonConvert.SerializeObject(sendMailRequestDTO));
      string utfString = Encoding.UTF8.GetString(data, 0, data.Length);

      var result = await webService.PostAsync<SendMailResponseDTO>("Email/ProcessAuthEmail", utfString);
      return result;
    }

    #region status api
    public async Task<WebServiceResult<StatusResponseDTO>> GetStatusListByAppIdCompIdAsync(StatusRequestDTO statusRequestDTO)
    {
      UTF8Encoding encoder = new UTF8Encoding();
      byte[] data = encoder.GetBytes(JsonConvert.SerializeObject(statusRequestDTO));
      string utfString = Encoding.UTF8.GetString(data, 0, data.Length);

      var result = await webService.PostAsync<StatusResponseDTO>("Status/GetStatusListByAppIdCompId", utfString);
      return result;
    }
    public async Task<WebServiceResult<StatusResponseDTO>> InsertUpdateStatusAsync(StatusRequestDTO statusRequestDTO)
    {
      UTF8Encoding encoder = new UTF8Encoding();
      byte[] data = encoder.GetBytes(JsonConvert.SerializeObject(statusRequestDTO));
      string utfString = Encoding.UTF8.GetString(data, 0, data.Length);

      var result = await webService.PostAsync<StatusResponseDTO>("Status/InsertUpdateStatus", utfString);
      return result;
    }
    public async Task<WebServiceResult<StatusResponseDTO>> DeleteStatusbyStatusId(StatusRequestDTO freightTbsRequestDTO)
    {
      UTF8Encoding encoder = new UTF8Encoding();
      byte[] data = encoder.GetBytes(JsonConvert.SerializeObject(freightTbsRequestDTO));
      string utfString = Encoding.UTF8.GetString(data, 0, data.Length);

      var result = await webService.PostAsync<StatusResponseDTO>("Status/DeleteStatusbyStatusId", utfString);
      return result;
    }
    #endregion
    #region warehouse
    public async Task<WebServiceResult<WarehousesResponseDTO>> GetWarehousesListByAppIdCompIdAsync(WarehousesRequestDTO warehousesRequestDTO)
    {
      UTF8Encoding encoder = new UTF8Encoding();
      byte[] data = encoder.GetBytes(JsonConvert.SerializeObject(warehousesRequestDTO));
      string utfString = Encoding.UTF8.GetString(data, 0, data.Length);

      var result = await webService.PostAsync<WarehousesResponseDTO>("Warehouse/GetWarehousesListByAppIdCompId", utfString);
      return result;
    }
    public async Task<WebServiceResult<WarehousesResponseDTO>> InsertUpdateWharehosueAsync(WarehousesRequestDTO statusRequestDTO)
    {
      UTF8Encoding encoder = new UTF8Encoding();
      byte[] data = encoder.GetBytes(JsonConvert.SerializeObject(statusRequestDTO));
      string utfString = Encoding.UTF8.GetString(data, 0, data.Length);

      var result = await webService.PostAsync<WarehousesResponseDTO>("Warehouse/InsertUpdateWarehouse", utfString);
      return result;
    }
    public async Task<WebServiceResult<WarehousesResponseDTO>> DeleteWharehousebyStatusId(WarehousesRequestDTO freightTbsRequestDTO)
    {
      UTF8Encoding encoder = new UTF8Encoding();
      byte[] data = encoder.GetBytes(JsonConvert.SerializeObject(freightTbsRequestDTO));
      string utfString = Encoding.UTF8.GetString(data, 0, data.Length);

      var result = await webService.PostAsync<WarehousesResponseDTO>("Warehouse/DeleteWarehousebyId", utfString);
      return result;
    }
    #endregion

    #region truck
    public async Task<WebServiceResult<TruckResponseDTO>> GetTruckbyID(TruckRequestDTO warehousesRequestDTO)
    {
      UTF8Encoding encoder = new UTF8Encoding();
      byte[] data = encoder.GetBytes(JsonConvert.SerializeObject(warehousesRequestDTO));
      string utfString = Encoding.UTF8.GetString(data, 0, data.Length);

      var result = await webService.PostAsync<TruckResponseDTO>("Truck/GetTruckbyID", utfString);
      return result;
    }
    #endregion
  }
}