using System;
using System.Collections.Generic;
using System.Text;

namespace MADBHR_Models.Employee
{
   public class VMEmployeeInfo
    {
        public string Name { get; set; }
        public string DateOfBirth { get; set; }
        public string EducationType{ get; set; }
        public string Township { get; set; }//same department
        public string joindate { get; set; }
        public string FromDate { get; set; }
        public string FatherName { get; set; }
        public string RankType { get; set; }
        public string TownCode { get; set; }
    }
}
