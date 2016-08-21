using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JTJ_Entity; // Entity Classes

namespace JTJ_Controller
{
    public class ScheduleController
    {
        public Schedule objSchedule = new Schedule();

        public void AddSchedule(string Date, string Day, string Info)
        {
            objSchedule.AddScheduleJSON(Date, Day, Info);
        }
        public void ModifySchedule(string Date, string Day, string Info)
        {
            objSchedule.ModifyScheduleJSON(Date, Day, Info);
        }
        public string ViewOnewWeekSchedule(string Date)
        {
            return objSchedule.ViewOneWeekScheduleJSON(Date);
        }
        public Schedule SearchDate(string Date)
        {
            return objSchedule.SearchDateJSON(Date);
        }
        public void CancelDate(string Date)
        {
            objSchedule.CancelDateJSON(Date);
        }
    }
}
