using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;

namespace Kalabean.Domain.Helper
{ 
    public static class PersionDate
    {
        /// <summary>
        /// تبدیل تاریخ میلادی به شمسی
        /// </summary>
        /// <param name="MildadiDate"></param>
        /// <returns></returns>
        public static string GetShamsi(DateTime MildadiDate, bool withTime = false)
        {
            try
            {
                DateTime TmpDate;
                bool IsValid = DateTime.TryParse(MildadiDate.ToString(), out TmpDate);
                if (IsValid == false)
                {
                    return null;
                }
                PersianCalendar pc = new PersianCalendar();
                int year = pc.GetYear(MildadiDate);
                int month = pc.GetMonth(MildadiDate);
                int day = pc.GetDayOfMonth(MildadiDate);
                if (withTime)
                {
                    return string.Format("{0} {1}", FormatShamsiDate(year, month.ToString(), day.ToString()),MildadiDate.ToString("HH:mm:ss"));
                }
                else
                {
                    return FormatShamsiDate(year, month.ToString(), day.ToString());
                }
            }
            catch (Exception ex)
            {
                throw new Exception("GetShamsi", ex);
            }
        }
        /// <summary>
        /// تاریخ امروز
        /// </summary>
        /// <returns></returns>
        public static string GetShamsiToday()
        {
            try
            {
                DateTime MildadiDate = DateTime.Now;
                DateTime TmpDate;
                bool IsValid = DateTime.TryParse(MildadiDate.ToString(), out TmpDate);
                if (IsValid == false)
                {
                    return null;
                }
                PersianCalendar pc = new PersianCalendar();
                int year = pc.GetYear(MildadiDate);
                int month = pc.GetMonth(MildadiDate);
                int day = pc.GetDayOfMonth(MildadiDate);
                return FormatShamsiDate(year, month.ToString(), day.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("GetShamsiToday", ex);
            }
        }
        /// <summary>
        /// تبدیل تاریخ شمسی به میلادی
        /// </summary>
        /// <param name="ShamsiDate"></param>
        /// <returns></returns>
        public static DateTime? GetMiladi(string ShamsiDate, string Time = "")
        {
            try
            {
                if (ShamsiDate == null) return null;
                if (IsShamsi(ShamsiDate) == false)
                {
                    throw new Exception("تاریخ وارد شده غیر مجاز می باشد");
                }
                PersianCalendar pc = new PersianCalendar();
                var split = ShamsiDate.Split(char.Parse("/"));
                DateTime t;
                DateTime Date;
                if (DateTime.TryParse(Time, out t))
                {
                    Date = pc.ToDateTime(split[0].ToInt(), split[1].ToInt(), split[2].ToInt(), t.Hour, t.Minute, t.Second, t.Millisecond);
                }
                else
                {
                    Date = pc.ToDateTime(split[0].ToInt(), split[1].ToInt(), split[2].ToInt(), 0, 0, 0, 0);
                }
                return Date;
            }
            catch (Exception ex)
            {
                throw new Exception("GetMiladi", ex);
            }
        }
        /// <summary>
        /// چک کردن اینکه آیا تاریخ شمسی وارد شده درست است یا نه
        /// </summary>
        /// <param name="ShamsiDate"></param>
        /// <returns></returns>
        public static bool IsShamsi(string ShamsiDate)
        {
            if (ShamsiDate.Length != 10)
            {
                return false;
            }
            PersianCalendar pc = new PersianCalendar();
            var split = ShamsiDate.Split(char.Parse("/"));
            try
            {
                var Date = pc.ToDateTime(split[0].ToInt(), split[1].ToInt(), split[2].ToInt(), 0, 0, 0, 0);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static string GetTimeFromDate(DateTime date)
        {
            try
            {
                return date.ToString("HH:mm:ss");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// تاریخ شمسی را به رشته استاندارد تاریخ شمسی برمیگرداند
        /// </summary>
        /// <param name="ShamsiDate"></param>
        /// <returns></returns>
        private static string FormatShamsiDate(string ShamsiDate)
        {
            try
            {
                if (ShamsiDate.Length != 10)
                {
                    return "";
                }

                var split = ShamsiDate.Split(char.Parse("/"));
                if (split.Length != 2)
                {
                    return "";
                }
                string Ret = "";
                string Month, Day = "";
                Month = split[1];
                Day = split[2];
                Month = Month.Length != 2 ? Month : "0" + Month;
                Day = Day.Length != 2 ? Day : "0" + Day;
                Ret = split[0] + "/" + Month + "/" + Day;
                return Ret;
            }
            catch (Exception ex)
            {
                throw new Exception("FormatShamsiDate", ex);
            }
        }
        /// <summary>
        /// تاریخ شمسی را به رشته استاندارد تاریخ شمسی برمیگرداند
        /// </summary>
        /// <param name="Year"></param>
        /// <param name="Month"></param>
        /// <param name="Day"></param>
        /// <returns></returns>
        private static string FormatShamsiDate(int Year, string Month, string Day)
        {
            try
            {
                if (Year == 0 || Month.ToInt() == 0 || Day.ToInt() == 0)
                {
                    return "";
                }

                return string.Format("{0}/{1}/{2}",
                                     Year,
                                    (Month = Month.ToInt() <= 9 ? "0" + Month : Month),
                                    (Day = Day.ToInt() <= 9 ? "0" + Day : Day));
            }
            catch (Exception ex)
            {
                throw new Exception("FormatShamsiDate", ex);
            }
        }
    }
}