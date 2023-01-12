using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Module
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int Credits { get; set; }
        public int ClassHrsWeek { get; set; }
        public int NumOfWeeks { get; set; }
        public string StartDate { get; set; }
        public int SelfStudyHrs { get; set; }
        public string UserID { get; set; }
        public string AssignDayOfWeek { get; set; }
    }
}