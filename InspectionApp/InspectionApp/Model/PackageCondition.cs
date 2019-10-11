using System;
using InspectionApp.Database;
using SQLite;

namespace InspectionApp.Model
{
  public class PackageCondition : Entity
  {
    public string PackageConditionName
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
