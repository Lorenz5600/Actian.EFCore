using System.IO;

namespace Actian.TestLoggers.MarkdownRendering
{
    partial class MarkdownDocument
    {
        public static MarkdownBold Bold(object contents)
        {
            return new MarkdownBold(new MarkdownDocument(contents));
        }

        public static MarkdownBold Bold(MarkdownBase contents)
        {
            return new MarkdownBold(contents);
        }
    }

    public class MarkdownBold : MarkdownBase
    {
        public MarkdownBold(MarkdownBase contents)
        {
            Contents = contents;
        }

        public MarkdownBase Contents { get; }

        public MarkdownBold With(
            MarkdownBase Contents = null
            )
        {
            return new MarkdownBold(
                Contents ?? this.Contents
            );
        }

        public MarkdownBold WithContents(MarkdownBase contents)
            => new MarkdownBold(contents);

        public override void Render(TextWriter writer)
        {
            if (Contents is null)
                return;

            writer.Write("**");
            Contents.Render(writer);
            writer.Write("**");
        }
    }
}
