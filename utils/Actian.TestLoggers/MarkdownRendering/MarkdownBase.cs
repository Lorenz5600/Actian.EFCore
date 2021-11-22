using System.IO;

namespace Actian.TestLoggers.MarkdownRendering
{
    public abstract class MarkdownBase : IMarkdown
    {
        public abstract void Render(TextWriter writer);

        public override string ToString()
        {
            return this.Render();
        }
    }
}
