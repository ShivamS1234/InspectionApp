using System;
using SQLite;

namespace InspectionApp.Model
{
  public class SizeTb
  {
    [PrimaryKey]
    public int Id
    {
      get;
      set;
    }

    public string SizeDescription
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
