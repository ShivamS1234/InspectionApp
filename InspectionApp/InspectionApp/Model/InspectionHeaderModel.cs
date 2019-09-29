using System;
using System.Linq;
using Inspection.Resouces.Entites;
using InspectionApp.Helpers;
using SQLite;

namespace InspectionApp.Model
{
    public class InspectionHeaderModel : InspectionHeader
    {
        public InspectionHeaderModel()
        {
        }
        private string _CmpName;
        [Ignore]
        public string CmpName
        {
            get
            {
                if (CompanyId > 0)
                {
                    return InitData.CmpList.Where(x => x.Id == CompanyId).FirstOrDefault().CompanyName;
                }
                return null;
            }
        }
        private string _VarietyName;
        [Ignore]
        public string VarietyName
        {
            get
            {
                if (VarietyId > 0)
                {
                    return InitData.VarietyList.Where(x => x.Id == VarietyId).FirstOrDefault().VarietyName;
                }
                return null;
            }
        }
        private string _BrandName;
        [Ignore]
        public string BrandName
        {
            get
            {
                if (BrandId > 0)
                {
                    return InitData.BrandList.Where(x => x.Id == BrandId).FirstOrDefault().BrandName;
                }
                return null;
            }
        }
        private string _PalletName;
        [Ignore]
        public string PalletName
        {
            get
            {
                if (PalletizingConditionId > 0)
                {
                    return InitData.PalletConditionList.Where(x => x.Id == PalletizingConditionId).FirstOrDefault().PalletConditionName;
                }
                return null;
            }
        }
    }
}
