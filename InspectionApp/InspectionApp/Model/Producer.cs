using System;
using SQLite;

namespace InspectionApp.Model
{
    public class Producer
    {
        [PrimaryKey]
        public int Id
        {
            get;
            set;
        }

        public string ProducerName
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
