using System;
using System.IO;
using T9Spelling.Business.Interfaces;
using T9Spelling.Properties;

namespace T9Spelling.Business
{
    internal class T9TextConverter : IT9TextConverter
    {
        internal T9TextConverter()
        {
            t9LineConverter = Registry.Get<IT9Factory>().CreateT9LineConverter();
        }

        public void ConvertText(TextReader source, TextWriter destination)
        {
            int linesCounter = 0;
            try
            {
                int linesCount = Convert.ToInt32(source.ReadLine());
                    
                if (linesCount < minLinesCount || linesCount > maxLinesCount)
                    throw new IndexOutOfRangeException(
                        String.Format(Resources.NumberOfLinesInSourceFileMustBeBetween, minLinesCount, maxLinesCount));

                String lineBreak = String.Empty;
                
                while (linesCounter < linesCount)
                {
                    string sourceLine = source.ReadLine();
                    if (sourceLine == null) break;

                    linesCounter++;
                    destination.Write(
                        String.Format(Resources.Case, lineBreak, linesCounter, GetResultLine(sourceLine)));
                    
                    lineBreak = "\r\n";
                }
                
                if (linesCounter < minLinesCount)
                    throw new IndexOutOfRangeException(
                        String.Format(Resources.NumberOfLinesInSourceFileIsLessThanMinimumAllowedValue));

            }
            
            catch (FormatException)
            {
                throw new FormatException(
                    Resources.InputStringHasIncorrectFormat + " " + Resources.ProcessingIsStopped);
            }
        }

        private string GetResultLine(string sourceLine)
        {
            string result;
            try
            {
                result = t9LineConverter.Convert(sourceLine);
            }
            catch (Exception e)
            {
                result = String.Format(Resources.Error, e.Message);
            }
            return result;
        }

        private const byte minLinesCount = 1;
        private const byte maxLinesCount = 100;

        private readonly IT9LineConverter t9LineConverter;
    }
}
