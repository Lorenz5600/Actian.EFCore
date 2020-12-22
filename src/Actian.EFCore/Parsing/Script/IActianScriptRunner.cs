namespace Actian.EFCore.Parsing.Script
{
    public interface IActianScriptRunner
    {
        void Execute(SqlStatement statement, bool ignoreErrors);
    }
}
