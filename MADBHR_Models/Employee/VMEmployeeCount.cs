using System;
using System.Collections.Generic;
using System.Text;

namespace MADBHR_Models.Employee
{
    public class VMEmployeeCount
    {
        public string StateDivisionCode { get; set; }
        public string StateDivision { get; set; }
        public int TownshipCount { get; set; }
        public int EmployeeCount { get; set; }
        public int TotalUpperRank { get; set; }
        public int TotalLowerRank { get; set; }
    }
}
