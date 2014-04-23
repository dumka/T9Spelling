using System;
using System.Collections.Generic;
using System.Text;
using T9Spelling.Business.Interfaces;
using T9Spelling.Properties;

namespace T9Spelling.Business
{
    internal class T9LineConverter : IT9LineConverter 
    {
        internal T9LineConverter()
        {
            InitializeMap();
        }

        public string Convert(string inputString)
        {
            if (!IsInputStringLengthCorrect(inputString))
                throw new FormatException(String.Format(Resources.LengthOfStringMustBeBetween, minStringLength, maxStringLength));
            
            StringBuilder result = new StringBuilder();
            char lastDigit = '-';
            foreach (char symbol in inputString)
            {
                T9MapItem mapItem = GetMapItemForSymbol(symbol);
                
                if (mapItem != null)
                {
                    if (mapItem.Digit == lastDigit)
                    {
                        result.Append(pauseSymbol);
                    }

                    lastDigit = mapItem.Digit;
                    result.Append(mapItem.Digit, mapItem.RepeatCount);
                }
                
            }
            return result.ToString();
        }

        /// <summary>
        /// Initializes map.
        /// </summary>
        private void InitializeMap()
        {
            t9Map = new Dictionary<char, T9MapItem>
                        {
                            {'a', new T9MapItem {Digit = '2', RepeatCount = 1}},
                            {'b', new T9MapItem {Digit = '2', RepeatCount = 2}},
                            {'c', new T9MapItem {Digit = '2', RepeatCount = 3}},
                            {'d', new T9MapItem {Digit = '3', RepeatCount = 1}},
                            {'e', new T9MapItem {Digit = '3', RepeatCount = 2}},
                            {'f', new T9MapItem {Digit = '3', RepeatCount = 3}},
                            {'g', new T9MapItem {Digit = '4', RepeatCount = 1}},
                            {'h', new T9MapItem {Digit = '4', RepeatCount = 2}},
                            {'i', new T9MapItem {Digit = '4', RepeatCount = 3}},
                            {'j', new T9MapItem {Digit = '5', RepeatCount = 1}},
                            {'k', new T9MapItem {Digit = '5', RepeatCount = 2}},
                            {'l', new T9MapItem {Digit = '5', RepeatCount = 3}},
                            {'m', new T9MapItem {Digit = '6', RepeatCount = 1}},
                            {'n', new T9MapItem {Digit = '6', RepeatCount = 2}},
                            {'o', new T9MapItem {Digit = '6', RepeatCount = 3}},
                            {'p', new T9MapItem {Digit = '7', RepeatCount = 1}},
                            {'q', new T9MapItem {Digit = '7', RepeatCount = 2}},
                            {'r', new T9MapItem {Digit = '7', RepeatCount = 3}},
                            {'s', new T9MapItem {Digit = '7', RepeatCount = 4}},
                            {'t', new T9MapItem {Digit = '8', RepeatCount = 1}},
                            {'u', new T9MapItem {Digit = '8', RepeatCount = 2}},
                            {'v', new T9MapItem {Digit = '8', RepeatCount = 3}},
                            {'w', new T9MapItem {Digit = '9', RepeatCount = 1}},
                            {'x', new T9MapItem {Digit = '9', RepeatCount = 2}},
                            {'y', new T9MapItem {Digit = '9', RepeatCount = 3}},
                            {'z', new T9MapItem {Digit = '9', RepeatCount = 4}},
                            {' ', new T9MapItem {Digit = '0', RepeatCount = 1}}
                        };
        }

        private bool IsInputStringLengthCorrect(string inputString)
        {
            int inputStringLength = inputString.Length;
            return (inputStringLength >= minStringLength && inputStringLength <= maxStringLength);
        }

        /// <summary>
        /// Gets a map item for the symbol.
        /// </summary>
        /// <param name="inputSymbol">Input symbol.</param>
        /// <returns>Map item.</returns>
        private T9MapItem GetMapItemForSymbol(char inputSymbol)
        {
            T9MapItem result;
            if (t9Map.ContainsKey(inputSymbol))
                result = t9Map[inputSymbol];
            else
                throw new FormatException(Resources.StringContainsIncorrectSymbols);
            return result;
        }

        /// <summary>
        /// Symbol that means "Pause".
        /// </summary>
        private const char pauseSymbol = ' ';

        /// <summary>
        /// Minimum string length.
        /// </summary>
        private const int minStringLength = 1;

        /// <summary>
        /// Maximum string length.
        /// </summary>
        private const int maxStringLength = 1000;

        /// <summary>
        /// Symbols map.
        /// </summary>
        private IDictionary<char, T9MapItem> t9Map;
    }
}
