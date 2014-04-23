namespace T9Spelling.Business.Interfaces
{
    public interface IT9LineConverter
    {
        /// <summary>
        /// Converts input string into number sequence.
        /// </summary>
        /// <param name="inputString">Input string.</param>
        /// <returns>Number sequence.</returns>
        string Convert(string inputString);
    }
}
