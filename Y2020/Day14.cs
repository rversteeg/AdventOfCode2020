using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using AdventOfCode.Util;

namespace AdventOfCode.Y2020
{
    public class Day14 : PuzzleSolutionWithParsedInput<Day14.Command[]>
    {
        public Day14() : base(14, 2020) {}

        public override object SolvePart1(Command[] input)
        {
            var result = input.Aggregate(new Computer(ImmutableDictionary<long, long>.Empty, ""),
                (computer, command) => command switch
            {
                SetMemCommand memCommand => new(computer.Memory.SetItem(memCommand.Address, ApplyMask(memCommand.Value, computer.Mask)), computer.Mask),
                SetMaskCommand maskCommand => new(computer.Memory, maskCommand.Mask),
                _ => computer
            });

            return result.Memory.Sum(x => x.Value);
        }

        private static long ApplyMask(long value, string mask)
        {
            long andMask = Convert.ToInt64(new string(mask.Select(x => x == '0' ? '0' : '1').ToArray()), 2);
            long orMask = Convert.ToInt64(new string(mask.Select(x=> x == '1'? '1' : '0').ToArray()), 2);

            return (value & andMask) | orMask;
        }        

        public override object SolvePart2(Command[] input)
        {
            var result = input.Aggregate(new Computer(ImmutableDictionary<long, long>.Empty, ""),
                (computer, command) => command switch
                {
                    SetMemCommand memCommand => new(computer.Memory.SetItems(GetAddresses(memCommand.Address, computer.Mask).Select(x=>KeyValuePair.Create(x, memCommand.Value))), computer.Mask),
                    SetMaskCommand maskCommand => new(computer.Memory, maskCommand.Mask),
                    _ => computer
                });

            return result.Memory.Sum(x => x.Value);
        }

        private static IEnumerable<long> GetAddresses(long address, string mask, int index = 0)
            => index >= mask.Length
                ? new[] { address }
                : mask[mask.Length - index - 1] switch
                {
                    '0' => GetAddresses(address, mask, index + 1),
                    '1' => GetAddresses(SetBit(address, index), mask, index + 1),
                    'X' => GetAddresses(address, mask, index + 1).Concat(GetAddresses(SwapBit(address, index), mask, index + 1)),
                    _ => throw new ArgumentException()
                };

        private static long SwapBit(long address, int bitNum)
            => address ^ (1L << bitNum);

        private static long SetBit(long address, int bitNum)
            => address | (1L << bitNum);

        public record Command(string Name);
        public record SetMaskCommand(string Mask) : Command("mask");
        public record SetMemCommand(long Address, long Value) : Command("mem");
        public record Computer(ImmutableDictionary<long, long> Memory, string Mask);

        protected override Command[] Parse()
            => ReadAllInputLines().Select(line =>
                    line.StartsWith("mask")
                        ? (Command)new SetMaskCommand(line.Substring(line.IndexOf('=') + 2))
                        : (Command)new SetMemCommand(
                        long.Parse(line.Substring(line.IndexOf('[') + 1, line.IndexOf(']') - line.IndexOf('[') - 1)),
                            long.Parse(line.Substring(line.IndexOf('=') + 2)))
                            ).ToArray();
    }
}