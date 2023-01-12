using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DAL;

namespace BLL
{
    public class BusinessLogicLayer
    {
        DataAccessLayer dal = new DataAccessLayer();


        //USERS
        public string Registration(User user)
        {
            return dal.Registration(user);
        }

        public string Login(User user)
        {
            return dal.Login(user);
        }



        //MODULES
        public double CalculateSelftStudyHrs(Module module)
        {
            return dal.CalculateSelftStudyHrs(module);
        }

        public int StoreModule(Module module)
        {
            return dal.StoreModule(module);
        }

        public DataTable GetModules(Module module)
        {
            return dal.GetModules(module);
        }



        //WORKING-HRS
        public int StoreWorkingHrs(WorkingHrs workinghrs)
        {
            return dal.StoreWorkingHrs(workinghrs);
        }

        public DataTable GetWorkingHrs(WorkingHrs workinghrs)
        {
            return dal.GetWorkingHrs(workinghrs);
        }
    }
}
