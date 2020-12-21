using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Util;
#pragma warning disable 8509

namespace AdventOfCode.Y2020
{
    public class Day08 : PuzzleSolutionWithParsedInput<Day08.Instruction[]>
    {

        public Day08() : base(8, 2020) {}

        public override object SolvePart1(Instruction[] input)
        {
            var result = Execute(input);
            return result.Accumulator;
        }

        public override object SolvePart2(Instruction[] input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i].Operation == "acc")
                    continue;
                var result = Execute(SwapInstruction(input, i));
                if (!result.Terminated)
                    return result.Accumulator;
            }

            return -1;
        }

        private Instruction[] SwapInstruction(Instruction[] input, int i)
            => input.Select((inst, idx) => idx == i ? new Instruction(inst.Operation switch { "jmp" => "nop", "nop" => "jmp" }, inst.Argument) : inst).ToArray();

        private static ExecutionResult Execute(IReadOnlyList<Instruction> input)
        {
            int accumulator = 0;
            int operationPointer = 0;

            HashSet<int> executedInstructions = new HashSet<int>();

            while (operationPointer < input.Count)
            {
                if (executedInstructions.Contains(operationPointer))
                    return new ExecutionResult(true, accumulator, operationPointer);

                executedInstructions.Add(operationPointer);

                switch (input[operationPointer].Operation)
                {
                    case "acc":
                        accumulator += input[operationPointer++].Argument;
                        break;
                    case "nop":
                        ++operationPointer;
                        break;
                    case "jmp":
                        operationPointer += input[operationPointer].Argument;
                        break;
                }
            }

            return new ExecutionResult(operationPointer != input.Count, accumulator, operationPointer);
        }

        protected override Instruction[] Parse()
            => ReadAllInputLines().Select(x => x.Split()).Select(x => new Instruction(x[0], int.Parse(x[1]))).ToArray();

        public record Instruction(string Operation, int Argument);

        public record ExecutionResult(bool Terminated, int Accumulator, int OperationPointer);

    }
}
