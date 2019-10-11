using System;
using InspectionApp.Database;
using SQLite;

namespace InspectionApp.Model
{
  public class User : Entity
  {

    public int? CompanyId
    {
      get;
      set;
    }

    public int? UserRoleId
    {
      get;
      set;
    }

    public string UserName
    {
      get;
      set;
    }

    public string UserPwd
    {
      get;
      set;
    }

    public string Email
    {
      get;
      set;
    }

    public string PhoneNumber
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