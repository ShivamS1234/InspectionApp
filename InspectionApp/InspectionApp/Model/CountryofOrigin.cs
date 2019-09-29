using System;
using SQLite;

namespace InspectionApp.Model
{
    public class CountryofOrigin
    {
        [PrimaryKey]
        public int Id
        {
            get;
            set;
        }

        public string CountryName
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
