using System.IO;

namespace Actian.TestLoggers.MarkdownRendering
{
    partial class MarkdownDocument
    {
        public static MarkdownText Text(object contents)
        {
            return new MarkdownText(new MarkdownDocument(contents));
        }

        public static MarkdownText Text(MarkdownBase contents)
        {
            return new MarkdownText(contents);
        }
    }

    public partial class MarkdownText : MarkdownBase
    {
        public MarkdownText(MarkdownBase contents)
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

            Contents.Render(writer);
        }
    }
}
