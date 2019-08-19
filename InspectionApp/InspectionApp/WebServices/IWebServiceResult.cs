using System;
namespace InspectionApp.WebServices
{
  public interface IWebServiceResult
  {
    string Status
    {
      get;
      set;
    }
    string Message
    {
      get;
      set;
    }
    bool IsSuccess
    {
      get;

    }
  }
  public interface IWebServiceResult<TData> : IWebServiceResult
  {
    TData Data
    {
      get;
      set;
    }
  }
}
