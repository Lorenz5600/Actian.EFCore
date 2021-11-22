using System.IO;
using Actian.TestLoggers.MarkdownRendering;

namespace Actian.TestLoggers
{
    public static class TextWriterExtensions
    {
        public static void WriteMD(this TextWriter writer, IMarkdown markdown)
        {
            markdown?.Render(writer);
        }
    }
}
