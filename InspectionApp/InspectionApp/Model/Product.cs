using System;
using InspectionApp.Database;
using SQLite;

namespace InspectionApp.Model
{
  public class Product : Entity
  {

    public string ProductName
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
