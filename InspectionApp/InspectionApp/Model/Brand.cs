using System;
using InspectionApp.Database;
using SQLite;

namespace InspectionApp.Model
{
    public class Brand : Entity
    {
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
