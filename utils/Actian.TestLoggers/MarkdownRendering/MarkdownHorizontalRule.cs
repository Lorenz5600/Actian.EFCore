using System.IO;

namespace Actian.TestLoggers.MarkdownRendering
{
    partial class MarkdownDocument
    {
        public static MarkdownHorizontalRule HorizontalRule => MarkdownHorizontalRule.Default;
    }

    public class MarkdownHorizontalRule : MarkdownBase
    {
        public static readonly MarkdownHorizontalRule Default = new MarkdownHorizontalRule();

        public override void Render(TextWriter writer)
        {
            writer.WriteLine("---");
            writer.WriteLine();
        }
    }
}
