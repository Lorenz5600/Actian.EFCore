using System.IO;

namespace Actian.TestLoggers.MarkdownRendering
{
    partial class MarkdownDocument
    {
        public static MarkdownParagraph Paragraph(object contents)
        {
            return new MarkdownParagraph(new MarkdownDocument(contents, "\n"));
        }

        public static MarkdownParagraph Paragraph(MarkdownBase contents)
        {
            return new MarkdownParagraph(contents);
        }
    }

    public class MarkdownParagraph : MarkdownBase
    {
        public MarkdownParagraph(MarkdownBase contents)
        {
            Contents = contents;
        }

        public MarkdownBase Contents { get; }

        public MarkdownParagraph With(
            MarkdownBase Contents = null
            )
        {
            return new MarkdownParagraph(
                Contents ?? this.Contents
            );
        }

        public MarkdownParagraph WithContents(MarkdownBase contents)
            => new MarkdownParagraph(contents);

        public override void Render(TextWriter writer)
        {
            if (Contents is null)
                return;

            Contents.Render(writer);
            writer.WriteLine();
            writer.WriteLine();
        }
    }
}
