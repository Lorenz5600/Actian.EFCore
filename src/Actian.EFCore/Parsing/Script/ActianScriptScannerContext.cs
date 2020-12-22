using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Actian.EFCore.Parsing.Script
{
    public class ActianScriptScannerContext
    {
        public ActianScriptScannerContext(string input, string directory)
        {
            Input = input;
            Directory = directory;
        }

        public string Input { get; }
        public string Directory { get; }
        public int Position { get; private set; } = 0;
        public bool Eof => Position >= Input.Length;
        public char Char => Eof ? '\0' : Input[Position];
        public string Rest => Eof ? "" : Input.Substring(Position);

        public void NextChar()
        {
            if (!Eof)
            {
                Position += 1;
            }
        }

        public void Skip(char c)
        {
            if (Char == c)
            {
                NextChar();
            }
        }

        public void SkipWhiteSpace()
        {
            while (char.IsWhiteSpace(Char))
            {
                NextChar();
            }
        }

        public void SkipWhile(Func<char, bool> predicate)
        {
            GetWhile(predicate);
        }

        public void SkipWhile(Func<string, bool> predicate)
        {
            GetWhile(predicate);
        }

        public void SkipWhile(Regex re)
        {
            GetWhile(re);
        }

        public void SkipWhile(params char[] chars)
        {
            GetWhile(cc => chars.Any(c => cc == c));
        }

        public void SkipWhile(string str, StringComparison comparisonType = StringComparison.Ordinal)
        {
            GetWhile(cstr => cstr.StartsWith(str, comparisonType));
        }

        public void SkipUntil(Func<char, bool> predicate)
        {
            GetUntil(predicate);
        }

        public void SkipUntil(Func<string, bool> predicate)
        {
            GetUntil(predicate);
        }

        public void SkipUntil(Regex re)
        {
            GetUntil(re);
        }

        public void SkipUntil(params char[] chars)
        {
            GetUntil(cc => chars.Any(c => cc == c));
        }

        public void SkipUntil(string str, StringComparison comparisonType = StringComparison.Ordinal)
        {
            GetUntil(cstr => cstr.StartsWith(str, comparisonType));
        }

        public string Get(int count)
        {
            var start = Position;
            while (!Eof && count > 0)
            {
                NextChar();
                count -= 1;
            }
            return Input.Substring(start, Position - start);
        }

        public string GetWhile(Func<char, bool> predicate)
        {
            var start = Position;
            while (!Eof && predicate(Char))
            {
                NextChar();
            }
            return Input.Substring(start, Position - start);
        }

        public string GetWhile(Func<string, bool> predicate)
        {
            var start = Position;
            while (!Eof && predicate(Rest))
            {
                NextChar();
            }
            return Input.Substring(start, Position - start);
        }

        public string GetWhile(Regex re)
        {
            return GetWhile(re.IsMatch);
        }

        public string GetWhile(params char[] chars)
        {
            return GetWhile(cc => chars.Any(c => cc == c));
        }

        public string GetWhile(string str, StringComparison comparisonType = StringComparison.Ordinal)
        {
            return GetWhile(cstr => cstr.StartsWith(str, comparisonType));
        }

        public string GetUntil(Func<char, bool> predicate)
        {
            var start = Position;
            while (!Eof && !predicate(Char))
            {
                NextChar();
            }
            return Input.Substring(start, Position - start);
        }

        public string GetUntil(Func<string, bool> predicate)
        {
            var start = Position;
            while (!Eof && !predicate(Rest))
            {
                NextChar();
            }
            return Input.Substring(start, Position - start);
        }

        public string GetUntil(Regex re)
        {
            return GetWhile(rest => !re.IsMatch(rest));
        }

        public string GetUntil(params char[] chars)
        {
            return GetUntil(cc => chars.Any(c => cc == c));
        }

        public string GetUntil(string str, StringComparison comparisonType = StringComparison.Ordinal)
        {
            return GetUntil(cstr => cstr.StartsWith(str, comparisonType));
        }

        public string GetRest()
        {
            var start = Position;
            Position = Input.Length;
            return Input.Substring(start, Position - start);
        }
    }
}
