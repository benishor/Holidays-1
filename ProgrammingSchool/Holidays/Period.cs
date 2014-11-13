using System;

namespace Holidays
{
    public class Period
    {
        public DateTime From { private set; get; }
        public DateTime To { private set; get; }

        public Period(DateTime from, DateTime to)
        {
            From = from;
            To = to;
        }
    }
}
