using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2020.Util;

namespace AdventOfCode2020
{
    public class Day13 : PuzzleSolutionWithParsedInput<Day13.Input>
    {

        public record Input(int CurTime, List<int> Ids);

        public Day13() : base(13) { }

        public override object SolvePart1(Input input)
        {
            var result =
                
                (from busId in input.Ids
                 where busId > 0
                    let timeToWait = busId - input.CurTime % busId
                    orderby timeToWait
                    select busId * timeToWait).First();

            return result;
        }

        public override object SolvePart2(Input input)
        {
            var schedules = input.Ids.Select((busId, idx)=> (busId: busId, idx)).Where(x => x.busId > 0).ToArray();

            long timestamp = 100_000_000_000 - 100_000_000_000 % input.Ids[0];
            long increment = 1;

            foreach (var bus in schedules)
            {
                while((timestamp+bus.idx) % bus.busId != 0)
                    timestamp += increment;

                increment *= bus.busId;
            }

            return timestamp;
        }

        private void WriteSchedule(in long time, (int busId, int idx)[] schedule)
        {
            Console.WriteLine($"{time}: {String.Join(", ", schedule.Select(x=>x.busId.ToString()))}");
        }

        protected override Input Parse()
        {
            var lines = ReadAllInputLines();

            return new Input(
                int.Parse(lines[0])
                , lines[1].Split(',').Select(part => part == "x" ? -1 : int.Parse(part)).ToList());
        }
    }
}