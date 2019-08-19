using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Prism.DryIoc;
using Newtonsoft.Json;
using InspectionApp.Helpers;

namespace InspectionApp.WebServices
{
  public class WebService : ServiceConfigrations, IWebService
  {
    private async Task<WebServiceResult<T>> SendRequest<T>(string action, string parameters, string method)
    {
      try
      {
        HttpClient httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri(Constants.BaseUrl);

        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        HttpResponseMessage response = null;
        WebServiceResult<T> result = new WebServiceResult<T>();
        if (method == "POST")
        {
          HttpContent Content = new StringContent(parameters, Encoding.UTF8, "application/json");
          response = await httpClient.PostAsync(action, Content);
        }
        else if (method == "GET")
        {
          HttpRequestMessage req1 = new HttpRequestMessage(HttpMethod.Get, action);
          req1.Content = new StringContent(parameters, Encoding.UTF8, "application/json");
          response = await httpClient.GetAsync(req1.RequestUri);
        }
        if (response.IsSuccessStatusCode)
        {
          var responseString = await response.Content.ReadAsStringAsync();
          if (!string.IsNullOrEmpty(responseString))
          {

            var dataResult = JsonConvert.DeserializeObject<T>(responseString);
            if (dataResult != null)
            {
              result = new WebServiceResult<T> { Data = dataResult, Message = "", Status = "Success" };
            }
          }
        }
        else
        {
          result.Status = "Unsuccess";
          result.Message = response.ReasonPhrase;
        }
        return result;
      }
      catch (Exception ex)
      {
        if (ex.InnerException.Message == "Error: ConnectFailure (Network is unreachable)")
        {
          return new WebServiceResult<T>() { Status = "Unsuccess", Message = "Please check internet connection." };
        }
        else
        {
          return new WebServiceResult<T>() { Status = "Unsuccess", Message = ex.Message };
        }

      }

    }

    public Task<WebServiceResult<T>> SendRequestAsync<T>(string action, string parameters, string method)
    {
      return Task.Run(() =>
      {
        return SendRequest<T>(action, parameters, method);
      });
    }


    public Task<WebServiceResult<T>> GetAsync<T>(string action, string objectData)
    {
      return SendRequestAsync<T>(action, objectData, "GET");
    }

    public Task<WebServiceResult<T>> PostAsync<T>(string action, string objectData)
    {
      return SendRequestAsync<T>(action, objectData, "POST");
    }

    public WebService()
    {

    }

  }
}
