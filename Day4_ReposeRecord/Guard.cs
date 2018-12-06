using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day4_ReposeRecord
{
    class Guard
    {
        public int ID { get; set; }
        public int CheckSum { get { return MaxMinute * ID; } }
        public int MaxMinute { get { return GetMaxMinute().Key; } }
        public int MaxMinuteCount { get { return GetMaxMinute().Value; } }
        public int TotalMinutesAsleep { get { return SleepMinutes.Sum(x => x.Value); } }
        public IDictionary<int, int> SleepMinutes { get; set; }

        public Guard()
        {
            SleepMinutes = new Dictionary<int, int>();
            for (int i = 0; i < 60; i++)
                SleepMinutes.Add(i, 0);
        }

        public void PopulateSleepMinutes(DateTime sleepStart, DateTime wakeUp)
        {
            var currentDateTime = new DateTime(sleepStart.Year, sleepStart.Month, sleepStart.Day, sleepStart.Hour, sleepStart.Minute, 0);
            while(currentDateTime < wakeUp)
            {
                SleepMinutes[currentDateTime.Minute]++;
                currentDateTime = currentDateTime.AddMinutes(1);
            }
        }

        private KeyValuePair<int, int> GetMaxMinute()
        {
            var currentMaxValue = 0;
            var currentMaxKey = 0;
            KeyValuePair<int, int> maxKVP = SleepMinutes.First();
            foreach (var kvp in SleepMinutes)
                if (kvp.Value >= currentMaxValue)
                {
                    currentMaxValue = kvp.Value;
                    currentMaxKey = kvp.Key;
                    maxKVP = kvp;
                }
            return maxKVP;
        }
    }
}
