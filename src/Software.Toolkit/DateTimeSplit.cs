using System;
using System.Collections.Generic;

namespace Software.Toolkit
{
    public static class DateTimeSplit
    {
        /// <summary>
        /// 按照给定的间隔分割时间
        /// </summary>
        /// <param name="startTime">起始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="intervalDays">间隔天数</param>
        public static List<DateTime> SplitByInterval(DateTime startTime, DateTime endTime, int intervalDays)
        {
            if (startTime > endTime)
            {
                throw new ArgumentException("起始时间大于结束时间");
            }

            List<DateTime> dateTimes = new List<DateTime>();
            DateTime startTemp = startTime;
            DateTime endTemp = startTime;
            bool first = true;
            do
            {
                if (first)
                {
                    endTemp = startTime.AddDays(intervalDays);
                    first = false;
                }
                else
                {
                    startTemp = endTemp.AddMilliseconds(1);
                    endTemp = startTemp.AddDays(intervalDays);
                }

                if (endTemp > endTime)
                {
                    endTemp = endTime;
                }

                if (startTemp > endTemp) break;

                dateTimes.Add(startTemp);
                dateTimes.Add(endTemp);

            } while (endTime > endTemp);

            return dateTimes;
        }

        /// <summary>
        /// 按照月份分割时间，分割点为每月最后一刻
        /// </summary>
        /// <param name="startTime">起始时间</param>
        /// <param name="endTime">结束时间</param>
        public static List<DateTime> SplitByMonth(DateTime startTime, DateTime endTime)
        {
            if (startTime > endTime)
            {
                throw new ArgumentException("起始时间大于结束时间");
            }

            List<DateTime> dateTimes = new List<DateTime>();
            DateTime startTemp = startTime;
            DateTime endTemp = startTime;
            bool first = true;
            do
            {
                if (first)
                {
                    endTemp = DateTime.Parse(startTime.ToString($"yyyy-MM-{DateTime.DaysInMonth(startTime.Year, startTime.Month)} 23:59:59.999"));
                    first = false;
                }
                else
                {
                    startTemp = endTemp.AddMilliseconds(1);
                    endTemp = DateTime.Parse(startTemp.ToString($"yyyy-MM-{DateTime.DaysInMonth(startTemp.Year, startTemp.Month)} 23:59:59.999"));
                }

                if (endTemp > endTime)
                {
                    endTemp = endTime;
                }

                if (startTemp > endTemp) break;

                dateTimes.Add(startTemp);
                dateTimes.Add(endTemp);

            } while (endTime > endTemp);

            return dateTimes;
        }
    }
}
