using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class WorkingHrs
    {
        public string UserID { get; set; }
        public string ModuleName { get; set; }
        public int HoursSpent { get; set; }
        public string Date { get; set; }
        public int SelfStudyHrs { get; set; }
    }
}
