using Newtonsoft.Json;

namespace Actian.TestLoggers.MarkdownRendering
{
    partial class MarkdownDocument
    {
        public static MarkdownCodeBlock Json(object contents)
            => CodeBlock(JsonConvert.SerializeObject(contents, Formatting.Indented), "json");

        public static MarkdownCodeBlock Json(object contents, JsonSerializerSettings settings)
            => CodeBlock(JsonConvert.SerializeObject(contents, Formatting.Indented, settings), "json");

        public static MarkdownCodeBlock Json(object contents, params JsonConverter[] converters)
            => CodeBlock(JsonConvert.SerializeObject(contents, Formatting.Indented, converters), "json");
    }
}
