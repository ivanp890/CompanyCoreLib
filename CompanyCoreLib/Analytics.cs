using System;
using System.Collections.Generic;
using System.Linq;

namespace CompanyCoreLib
{
    public class Analytics
    {
        public List<DateTime> PopularMonths(List<DateTime> dates)
        {
            // Список для хранения дат и их популярности
            var dateTimeWithCounterList = new List<Tuple<DateTime, int>>();

            // Вычисляем прошлый год
            int previousYear = DateTime.Now.Year - 1;

            foreach (DateTime iterDate in dates)
            {
                // Фильтруем даты только за прошлый год
                if (iterDate.Year == previousYear)
                {
                    // Вычисляем начало месяца
                    var dateMonthStart = new DateTime(iterDate.Year, iterDate.Month, 1, 0, 0, 0);

                    // Проверяем, есть ли уже эта дата в списке
                    var index = dateTimeWithCounterList.FindIndex(item => item.Item1 == dateMonthStart);

                    if (index == -1)
                    {
                        // Если даты нет, добавляем ее с начальным счетчиком 1
                        dateTimeWithCounterList.Add(new Tuple<DateTime, int>(dateMonthStart, 1));
                    }
                    else
                    {
                        // Если дата уже есть, увеличиваем счетчик
                        dateTimeWithCounterList[index] = Tuple.Create(
                            dateTimeWithCounterList[index].Item1,
                            dateTimeWithCounterList[index].Item2 + 1
                        );
                    }
                }
            }

            // Сортируем и возвращаем список дат
            return dateTimeWithCounterList
                .OrderByDescending(item => item.Item2) // Сортируем по убыванию популярности
                .ThenBy(item => item.Item1)          // При равной популярности - по возрастанию даты
                .Select(item => item.Item1)          // Извлекаем только даты
                .ToList();                           // Преобразуем в список
        }
    }
}
