using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LR9
{
    /// <summary>
    /// Для того чтобы распределить файлы по папкам в зависимости от того к какой неделе они относятся
    /// </summary>
    internal class WeekCalculate
    {
        public static DateTime GetFirstDayOfWeek(int year, int weekNumber)
        {
            // Создаем экземпляр класса Calendar для текущей культуры
            Calendar calendar = CultureInfo.CurrentCulture.Calendar;

            // Получаем первый день указанной недели (воскресенье)
            DateTime firstDayOfWeek = new DateTime(year, 1, 1).AddDays((weekNumber - 1) * 7);
            while (calendar.GetWeekOfYear(firstDayOfWeek, CalendarWeekRule.FirstDay, DayOfWeek.Sunday) != weekNumber)
            {
                firstDayOfWeek = firstDayOfWeek.AddDays(1);
            }

            return firstDayOfWeek;
        }

        public static DateTime GetLastDayOfWeek(int year, int weekNumber)
        {
            // Получаем начало недели
            DateTime firstDayOfWeek = GetFirstDayOfWeek(year, weekNumber);

            // Получаем конец недели (суббота)
            DateTime lastDayOfWeek = firstDayOfWeek.AddDays(6);

            return lastDayOfWeek;
        }
    }
}
