using System;

namespace AppFinancas.Shared.Common;

// Extensions Methods.
public static class DateTimeExtension
{
    public static DateTime GetFirstDay(this DateTime dateTime, int? year = null, int? month = null)
    {
        return new(year ?? dateTime.Year, month ?? dateTime.Month, 1);
    }

    public static DateTime GetLastDay(this DateTime date, int? year = null, int? month = null)
    {
        return new DateTime(year ?? date.Year, month ?? date.Month, 1)
                   .AddMonths(1)
                   .AddDays(-1);
    }
}
