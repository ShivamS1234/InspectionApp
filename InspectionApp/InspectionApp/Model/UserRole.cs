using System;
using SQLite;

namespace InspectionApp.Model
{

    public class UserRole
    {
        [PrimaryKey]
        public int Id
        {
            get;
            set;
        }

        public string UserRoleName
        {
            get;
            set;
        }

        public bool IsActive
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
