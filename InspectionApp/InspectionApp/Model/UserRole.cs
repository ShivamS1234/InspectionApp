using System;
using InspectionApp.Database;
using SQLite;

namespace InspectionApp.Model
{

  public class UserRole : Entity
  {

    public string UserRoleName
    {
      get;
      set;
    }

    public bool IsActive
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
