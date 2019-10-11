using System;
using InspectionApp.Database;
using SQLite;

namespace InspectionApp.Model
{
  public class Variety : Entity
  {
    public string VarietyName
    {
      get;
      set;
    }
    public int ProductID
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
