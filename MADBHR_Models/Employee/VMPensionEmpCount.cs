using System;
using System.Collections.Generic;
using System.Text;

namespace MADBHR_Models.Employee
{
    public class VMPensionEmpCount
    {
        public string StateDivisionCode { get; set; }
        public string StateDivision { get; set; }
        public int TownshipCount { get; set; }
        public int EmployeeCount { get; set; }
    }
}
