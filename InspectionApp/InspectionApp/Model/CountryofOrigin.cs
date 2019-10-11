using System;
using InspectionApp.Database;
using SQLite;

namespace InspectionApp.Model
{
  public class CountryofOrigin : Entity
  {
    public string CountryName
    {
      get;
      set;
    }

    public int CompanyId
    {
      get;
      set;
    }
  }
}
