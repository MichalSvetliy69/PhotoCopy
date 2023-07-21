using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LR9
{
    public class MonthList
    {
        /// <summary>
        /// Словарь с месяцами для того чтобы числовой формат месяца переводить в стринговый (для красивого формирования названий папок с месяцами в каталоге)
        /// </summary>
        public static Dictionary<int, string> Month = new Dictionary<int, string>
        {
            {1, "January"},
            {2, "February"},
            {3, "March"},
            {4, "April"},
            {5, "May"},
            {6, "June"},
            {7, "July"},
            {8, "August"},
            {9, "September"},
            {10, "October"},
            {11, "November"},
            {12, "December"}
        };

    }
}
