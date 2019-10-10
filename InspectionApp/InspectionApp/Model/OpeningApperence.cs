using System;
using SQLite;

namespace InspectionApp.Model
{
  public class OpeningApperence
  {
    [PrimaryKey]
    public int Id
    {
      get;
      set;
    }

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
