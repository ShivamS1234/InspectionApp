using System;
using InspectionApp.Database;
using SQLite;

namespace InspectionApp.Model
{
  public class PalletCondition : Entity
  {
    public string PalletConditionName
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
