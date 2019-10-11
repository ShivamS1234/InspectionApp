using System;
using InspectionApp.Database;
using SQLite;

namespace InspectionApp.Model
{
  public class SizeTb : Entity
  {

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
