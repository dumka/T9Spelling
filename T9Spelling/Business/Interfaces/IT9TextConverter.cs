using System.IO;

namespace T9Spelling.Business.Interfaces
{
    public interface IT9TextConverter
    {
        /// <summary>
        /// Converts text.
        /// </summary>
        /// <param name="source">Source.</param>
        /// <param name="destination">Destination.</param>
        void ConvertText(TextReader source, TextWriter destination);
    }
}
