using System;
using SQLite;

namespace InspectionApp.Model
{
    public class Product
    {
        [PrimaryKey]
        public int Id
        {
            get;
            set;
        }

        public string ProductName
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
