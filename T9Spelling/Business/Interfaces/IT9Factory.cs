namespace T9Spelling.Business.Interfaces
{
    public interface IT9Factory
    {
        /// <summary>
        /// Creates T9 text converter.
        /// </summary>
        /// <returns>T9 text converter.</returns>
        IT9TextConverter CreateT9TextConverter();

        /// <summary>
        /// Creates T9 line converter.
        /// </summary>
        /// <returns>T9 line converter.</returns>
        IT9LineConverter CreateT9LineConverter();
    }
}
