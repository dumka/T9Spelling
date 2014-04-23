using System;
using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using T9Spelling.Business;
using T9Spelling.Business.Interfaces;

namespace T9SpellingTest
{
    [TestClass]
    public class T9TextConverterTest : BaseT9Test
    {
        private IT9TextConverter t9TextConverter;
        private readonly StringBuilder sbDestination = new StringBuilder();
        private StringReader source;
        private StringWriter destination;

        [TestInitialize]
        public void Initialize()
        {
            t9TextConverter = Registry.Get<IT9Factory>().CreateT9TextConverter();
            destination = new StringWriter(sbDestination);
        }

        [TestCleanup]
        public void CleanUp()
        {
            source.Dispose();
            destination.Dispose();
            sbDestination.Clear();
        }

        [TestMethod]
        public void ConvertShortText()
        {
            source = new StringReader("1\r\nhi");
            t9TextConverter.ConvertText(source, destination);
            Assert.AreEqual(sbDestination.ToString(), "Case #1: 44 444");
        }

        [TestMethod]
        public void ConvertLongText()
        {
            StringBuilder correctResult = new StringBuilder();
            StringBuilder sourceText = new StringBuilder("100\r\n");
            String lineBreak = string.Empty;
            for (int rowNumber = 1; rowNumber <= 100; rowNumber++)
            {
                sourceText.AppendLine("hi");
                correctResult.Append(String.Format("{0}Case #{1}: 44 444", lineBreak, rowNumber));
                lineBreak = "\r\n";
            }
            
            source = new StringReader(sourceText.ToString());
            t9TextConverter.ConvertText(source, destination);

            Assert.AreEqual(sbDestination.ToString(), correctResult.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void ConvertTextLongerThanAllowed()
        {
            StringBuilder sourceText = new StringBuilder("101\r\n");
            for (int rowNumber = 1; rowNumber <= 101; rowNumber++)
            {
                sourceText.AppendLine("hi");
            }

            source = new StringReader(sourceText.ToString());
            t9TextConverter.ConvertText(source, destination);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void ConvertTextShorterThanAllowed()
        {
            source = new StringReader("0");
            t9TextConverter.ConvertText(source, destination);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ConvertTextWithNotNumberHeader()
        {
            source = new StringReader("test");
            t9TextConverter.ConvertText(source, destination);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void ConvertTextWitIncorrectHeader()
        {
            source = new StringReader("100");
            t9TextConverter.ConvertText(source, destination);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void ExpectAnExceptionByEmptyText()
        {
            source = new StringReader("");
            t9TextConverter.ConvertText(source, destination);
        }
    }
}
