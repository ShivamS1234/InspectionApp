using System;
namespace InspectionApp.WebServices
{
  public class WebServiceResult : IWebServiceResult
  {
    public string Message
    {
      get;
      set;
    }
    public string Status
    {
      get;
      set;
    }

    public bool IsSuccess
    {
      get
      {
        return Status == "Success" ? true : false;
      }
    }

    public WebServiceResult()
    {

    }
  }
  public class WebServiceResult<TData> : WebServiceResult, IWebServiceResult<TData>
  {
    public TData Data
    {
      get;
      set;
    }
  }
}
