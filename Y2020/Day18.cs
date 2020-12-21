using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Util;
#pragma warning disable 8509

namespace AdventOfCode.Y2020
{
    public class Day18 : PuzzleSolutionWithLinesInput
    {
        public Day18(): base(18, 2020){}
        public override object SolvePart1(string[] input)
        {
            return input.Select(line => ExecuteRPN(ShuntingYard(Parse(line), PrecedencePart1))).Sum();
        }

        public override object SolvePart2(string[] input)
        {            
            return input.Select(line => ExecuteRPN(ShuntingYard(Parse(line), PrecedencePart2))).Sum();
        }

        private record Token(TokenType Type, char Value);

        private enum TokenType
        {
            Number,
            Parenthesis,
            Operator
        }

        private IEnumerable<Token> Parse(string line)
            => from chr in line
               where chr != ' '
               select chr switch
               {
                   '+' or '*' => new Token(TokenType.Operator, chr),
                   '(' or ')' => new Token(TokenType.Parenthesis, chr),
                   _ => new Token(TokenType.Number, chr)
               };


        private static readonly Dictionary<char, int> PrecedencePart1 = new Dictionary<char, int>
        {
            {'+', 1 },
            {'*', 1 }
        };

        private static readonly Dictionary<char, int> PrecedencePart2 = new Dictionary<char, int>
        {
            {'+', 2 },
            {'*', 1 }
        };

        private long ExecuteRPN(IEnumerable<Token> tokens)
        {
            Stack<long> tokenStack = new();

            foreach(var (tokenType, value) in tokens)
            {
                switch (tokenType)
                {
                    case TokenType.Number:
                        tokenStack.Push(value - '0');
                        break;
                    case TokenType.Operator:
                        tokenStack.Push(ApplyOperator(tokenStack.Pop(), value, tokenStack.Pop()));
                        break;
                    default:
                        return -1;
                }
            }

            return tokenStack.Pop();
        }

        private long ApplyOperator(long v1, char value, long v2)
        {
            return value switch
            {
                '+' => v1 + v2,
                '*' => v1 * v2
            };
        }

        private IEnumerable<Token> ShuntingYard(IEnumerable<Token> tokens, Dictionary<char, int> precedence)
        {
            var stack = new Stack<Token>();
            foreach (var token in tokens)
            {
                switch (token.Type)
                {
                    case TokenType.Number:
                        yield return token;
                        break;
                    case TokenType.Operator:
                        while (stack.Any() && stack.Peek().Type == TokenType.Operator && precedence[token.Value] <= precedence[stack.Peek().Value])
                            yield return stack.Pop();
                        stack.Push(token);
                        break;
                    case TokenType.Parenthesis:
                        if (token.Value == '(')
                            stack.Push(token);
                        else
                        {
                            while (stack.Peek().Value != '(')
                                yield return stack.Pop();
                            stack.Pop();
                        }
                        break;
                    default:
                        throw new ArgumentException("Wrong expression", nameof(tokens));
                }
            }
            while (stack.Any())
            {
                var tok = stack.Pop();
                if (tok.Type == TokenType.Parenthesis)
                    throw new ArgumentException("Wrong expression", nameof(tokens));
                yield return tok;
            }
        }
    }
}
