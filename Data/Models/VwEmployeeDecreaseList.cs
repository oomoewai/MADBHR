using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MADBHR_Data.Models
{
    public partial class VwEmployeeDecreaseList
    {
        public int EmployeePkid { get; set; }
        public string CurrentRank { get; set; }
        public string Name { get; set; }
        public string JobAddress { get; set; }
        public string DateOfBirth { get; set; }
        public string JoinDate { get; set; }
        public string CurrentRankDate { get; set; }
        public string EmployeeDecreaseDateForTransfer { get; set; }
        public string EmployeeDecreaseDateForResign { get; set; }
        public string EmployeeDecreaseDateForPension { get; set; }
        public string EmployeeDecreaseDateForRemove { get; set; }
        public string EmployeeDecreaseDateForDismiss { get; set; }
        public string EmployeeDecreaseDateForDead { get; set; }
        public string EmployeeDecreaseDateForArrive { get; set; }
        public string Remark { get; set; }
        public int IsActive { get; set; }
        public string SerialNumberMyan { get; set; }
        public string SerialNumber { get; set; }
    }
}
