using System;
using InspectionApp.Database;
using SQLite;

namespace InspectionApp.Model
{
  public class InspectionHeaderTable : Entity
  {

    public int CompanyId
    {
      get;
      set;
    }

    public DateTime InspectionDate
    {
      get;
      set;
    }

    public int UserId
    {
      get;
      set;
    }


    public string Invoice
    {
      get;
      set;
    }

    public int ProducerId
    {
      get;
      set;
    }

    public int VarietyId
    {
      get;
      set;
    }

    public int BrandId
    {
      get;
      set;
    }

    public int CountryofOriginId
    {
      get;
      set;
    }


    public int TotalBoxQuantities
    {
      get;
      set;
    }

    public decimal TempOnCaja
    {
      get;
      set;
    }

    public decimal TempOnTermografo
    {
      get;
      set;
    }

    public int PalletizingConditionId
    {
      get;
      set;
    }
  }
}
