using System;
using InspectionApp.Database;
using SQLite;

namespace InspectionApp.Model
{
  public class OpeningApperence : Entity
  {
    public string ApperenceDescription
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
