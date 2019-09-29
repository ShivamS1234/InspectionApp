using System;
using SQLite;

namespace InspectionApp.Model
{
    public class PalletCondition
    {
        [PrimaryKey]
        public int Id
        {
            get;
            set;
        }

        public string PalletConditionName
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
