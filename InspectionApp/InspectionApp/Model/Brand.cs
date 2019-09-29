using System;
using SQLite;

namespace InspectionApp.Model
{
    public class Brand
    {
        [PrimaryKey]
        public int Id
        {
            get;
            set;
        }

        public string BrandName
        {
            get;
            set;
        }

        public int ProductID
        {
            get;
            set;
        }

        public int CompanyId
        {
            get;
            set;
        }


        public int VarietyId
        {
            get;
            set;
        }


    }
}
