using System;
using InspectionApp.Database;
using SQLite;

namespace InspectionApp.Model
{
  public class Company : Entity
  {

    public string CompanyName
    {
      get;
      set;
    }

    public string CompanyAddress
    {
      get;
      set;
    }

    public string CompanyEmail
    {
      get;
      set;
    }

    public bool IsActive
    {
      get;
      set;
    }
  }
}
