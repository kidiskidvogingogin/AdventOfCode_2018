using System;
using System.Collections.Generic;
using System.Linq;

namespace Day4_ReposeRecord
{
    class Program
    {
        static void Main(string[] args)
        {
            //Part1();
            Part2();
        }

        private static void Part1()
        {
            string input;
            var entries = new List<(DateTime, string)>();
            var guardNaps = new Dictionary<int, Guard>();
            while ((input = Console.ReadLine()) != "end")
            {
                var logDate = DateTime.ParseExact(input.Substring(1, input.IndexOf("]") - 1), "yyyy-MM-dd HH:mm", null);
                var activity = input.Substring(input.IndexOf("]") + 2);
                entries.Add((logDate, activity));
            }
            entries.Sort((x, y) => x.Item1.CompareTo(y.Item1));

            Guard guard = null;
            for (int i = 0; i + 1 < entries.Count; i++)
            {
                var entry = entries[i];
                if (entry.Item2.Contains("Guard"))
                {
                    int guardID = Convert.ToInt32(entry.Item2.Substring(7, entry.Item2.Substring(7).IndexOf(" ")));
                    if (!guardNaps.TryGetValue(guardID, out guard))
                    {
                        guardNaps.Add(guardID, new Guard { ID = guardID });
                        guard = guardNaps[guardID];
                    }
                }
                else
                {
                    DateTime start = entries[i].Item1;
                    DateTime end = entries[i + 1].Item1;
                    guard.PopulateSleepMinutes(start, end);
                    i++;
                }
                //Console.WriteLine($"[{entries[i].Item1}] {entries[i].Item2}");
            }
            var sortedGuards = guardNaps.Values.ToList();
            sortedGuards.Sort((x, y) => y.TotalMinutesAsleep.CompareTo(x.TotalMinutesAsleep));
            sortedGuards = sortedGuards.Where(x => x.TotalMinutesAsleep == sortedGuards[0].TotalMinutesAsleep).ToList();
            sortedGuards.Sort((x, y) => y.MaxMinuteCount.CompareTo(x.MaxMinuteCount));

            Console.WriteLine($"GuardID: {sortedGuards[0].ID}, Minute: {sortedGuards[0].MaxMinute}, MinuteCount: {sortedGuards[0].MaxMinuteCount}");
            Console.WriteLine($"The answer is: {sortedGuards[0].ID * sortedGuards[0].MaxMinute}");
            Console.ReadLine();
        }

        private static void Part2()
        {
            string input;
            var entries = new List<(DateTime, string)>();
            var guardNaps = new Dictionary<int, Guard>();
            while ((input = Console.ReadLine()) != "end")
            {
                var logDate = DateTime.ParseExact(input.Substring(1, input.IndexOf("]") - 1), "yyyy-MM-dd HH:mm", null);
                var activity = input.Substring(input.IndexOf("]") + 2);
                entries.Add((logDate, activity));
            }
            entries.Sort((x, y) => x.Item1.CompareTo(y.Item1));

            Guard guard = null;
            for (int i = 0; i + 1 < entries.Count; i++)
            {
                var entry = entries[i];
                if (entry.Item2.Contains("Guard"))
                {
                    int guardID = Convert.ToInt32(entry.Item2.Substring(7, entry.Item2.Substring(7).IndexOf(" ")));
                    if (!guardNaps.TryGetValue(guardID, out guard))
                    {
                        guardNaps.Add(guardID, new Guard { ID = guardID });
                        guard = guardNaps[guardID];
                    }
                }
                else
                {
                    DateTime start = entries[i].Item1;
                    DateTime end = entries[i + 1].Item1;
                    guard.PopulateSleepMinutes(start, end);
                    i++;
                }
                //Console.WriteLine($"[{entries[i].Item1}] {entries[i].Item2}");
            }
            var sortedGuards = guardNaps.Values.ToList();
            //sortedGuards.Sort((x, y) => y.TotalMinutesAsleep.CompareTo(x.TotalMinutesAsleep));
            //sortedGuards = sortedGuards.Where(x => x.TotalMinutesAsleep == sortedGuards[0].TotalMinutesAsleep).ToList();
            sortedGuards.Sort((x, y) => y.MaxMinuteCount.CompareTo(x.MaxMinuteCount));

            Console.WriteLine($"GuardID: {sortedGuards[0].ID}, Minute: {sortedGuards[0].MaxMinute}, MinuteCount: {sortedGuards[0].MaxMinuteCount}");
            Console.WriteLine($"The answer is: {sortedGuards[0].ID * sortedGuards[0].MaxMinute}");
            Console.ReadLine();
        }
    }
}
