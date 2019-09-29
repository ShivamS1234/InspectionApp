using System;
using SQLite;

namespace InspectionApp.Model
{
    public class InspectionDetailTable
    {
        [PrimaryKey]
        public int Id
        {
            get;
            set;
        }

        public int InspectionHeaderId
        {
            get;
            set;
        }

        public int SizeId
        {
            get;
            set;
        }


        public int SampleSize
        {
            get;
            set;
        }

        public decimal Weight
        {
            get;
            set;
        }

        public int PhysicalCount
        {
            get;
            set;
        }

        public int OpeningApperenceId
        {
            get;
            set;
        }


        public decimal Temperature
        {
            get;
            set;
        }

        public decimal Brix
        {
            get;
            set;
        }

        public decimal Firmness
        {
            get;
            set;
        }

        public decimal SkinDamage
        {
            get;
            set;
        }

        public decimal Color
        {
            get;
            set;
        }

        public int PackageConditionId
        {
            get;
            set;
        }

        public string Comment
        {
            get;
            set;
        }

        public decimal QualityScore
        {
            get;
            set;
        }
    }
}
