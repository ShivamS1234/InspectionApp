using System;
using InspectionApp.Database;

namespace InspectionApp.Model
{
  public class Producer : Entity
  {

    public string ProducerName
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
