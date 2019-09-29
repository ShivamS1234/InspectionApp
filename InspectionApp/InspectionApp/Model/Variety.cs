using System;
using SQLite;

namespace InspectionApp.Model
{
    public class Variety
    {
        [PrimaryKey]
        public int Id
        {
            get;
            set;
        }

        public string VarietyName
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

    }
}
