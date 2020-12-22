using System.IO;
using System.Text;

namespace TestConsoleApp
{
    public class CompositeTextWriter : TextWriter
    {
        private readonly TextWriter[] _writers;

        public CompositeTextWriter(params TextWriter[] writers)
        {
            _writers = writers;
        }

        public override Encoding Encoding => Encoding.UTF8;

        public override void Write(char value)
        {
            foreach (var writer in _writers)
            {
                writer.Write(value);
            }
        }
    }
}
