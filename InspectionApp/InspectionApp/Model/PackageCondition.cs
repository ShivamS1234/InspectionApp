using System;
using SQLite;

namespace InspectionApp.Model
{
  public class PackageCondition
  {
    [PrimaryKey]
    public int Id
    {
      get;
      set;
    }

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
