﻿using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Util;

namespace AdventOfCode.Y2020
{
    public class Day13 : PuzzleSolutionWithParsedInput<Day13.Input>
    {

        public record Input(int CurTime, List<int> Ids);

        public Day13() : base(13, 2020) { }

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
            var schedules = input.Ids.Select((busId, idx)=> (busId, idx)).Where(x => x.busId > 0).ToArray();

            const long start = 100_000_000_000_000;

            long timestamp = start - start % input.Ids[0];
            long increment = input.Ids[0];

            foreach (var bus in schedules.Skip(1))
            {
                while((timestamp+bus.idx) % bus.busId != 0)
                    timestamp += increment;

                increment *= bus.busId;
            }

            return timestamp;
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