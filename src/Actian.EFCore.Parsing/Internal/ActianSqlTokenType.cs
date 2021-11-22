namespace Actian.EFCore.Parsing.Internal
{
    public enum ActianSqlTokenType
    {
        NewLine,
        WhiteSpace,
        LineComment,
        BlockComment,
        Word,
        Number,
        String,
        Symbol,
        Semicolon,
        Command,
        Unknown
    }
}
