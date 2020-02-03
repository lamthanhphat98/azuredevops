using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureDevops.Ultilities
{
    public class ConvertToDay
    {
        public static String ConvertIntToDay(int dayOfWeek)
        {
            if(dayOfWeek==1)
            {
                return "Sunday";
            }
            if (dayOfWeek == 2)
            {
                return "Monday";
            }
            if (dayOfWeek == 3)
            {
                return "Tuesday";
            }
            if (dayOfWeek == 4)
            {
                return "Wednesday";
            }
            if (dayOfWeek == 5)
            {
                return "Thursday";
            }
            if (dayOfWeek == 6)
            {
                return "Friday";
            }
            if (dayOfWeek == 7)
            {
                return "Saturday";
            }
            return null;
        }
    }
}
