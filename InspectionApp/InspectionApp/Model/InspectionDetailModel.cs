using System;
using System.Linq;
using Inspection.Resouces.Entites;
using InspectionApp.Helpers;

namespace InspectionApp.Model
{
  public class InspectionDetailModel : InspectionDetail
  {
    public InspectionDetailModel()
    {
    }
    private string _SizeDescriptionName;
    public string SizeDescriptionName
    {
      get
      {
        if (SizeId > 0)
        {
          return InitData.SizeTbList.Where(x => x.Id == SizeId).FirstOrDefault().SizeDescription;
        }
        return null;
      }
    }
    private string _PalletConditionName;
    public string PalletConditionName
    {
      get
      {
        if (PackageConditionId > 0)
        {
          return InitData.PalletConditionList.Where(x => x.Id == PackageConditionId).FirstOrDefault().PalletConditionName;
        }
        return null;
      }
    }

  }
}
