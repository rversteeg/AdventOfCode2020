using Sprache;

namespace AdventOfCode.Util;

public abstract class PuzzleSolutionWithParser<TInput> : PuzzleSolutionBase
{
    private readonly Parser<TInput> _parser;

    protected PuzzleSolutionWithParser(Parser<TInput> parser)
    {
        _parser = parser;
    }

    protected abstract object SolvePart1(TInput input);
    protected abstract object SolvePart2(TInput input);

    public override object SolvePart1()
    {
        return SolvePart1(_parser.Parse(ReadAllInputText()));
    }

    public override object SolvePart2()
    {
        return SolvePart2(_parser.Parse(ReadAllInputText()));
    }
}