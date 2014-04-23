using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using T9Spelling.Business;
using T9Spelling.Business.Interfaces;

namespace T9SpellingTest
{
    [TestClass]
    public class T9LineConverterTest : BaseT9Test
    {
        private IT9LineConverter t9LineConverter;

        [TestInitialize]
        public void Initialize()
        {
            t9LineConverter = Registry.Get<IT9Factory>().CreateT9LineConverter();
        }

        [TestMethod]
        public void ConvertSimpleSequence()
        {
            string result = t9LineConverter.Convert("yes");
            Assert.AreEqual(result, "999337777");
        }

        [TestMethod]
        public void ConvertSequenceWithPause()
        {
            string result = t9LineConverter.Convert("hi");
            Assert.AreEqual(result, "44 444");
        }

        [TestMethod]
        public void ConvertSequenceWithSpaces()
        {
            string result = t9LineConverter.Convert("foo  bar");
            Assert.AreEqual(result, "333666 6660 022 2777");
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ExpectAnExceptionByIncorrectSymbols()
        {
            t9LineConverter.Convert("fooTEST 123тест!@#$%^&*()№;:?\" bar");
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ExpectAnExceptionByOverflow()
        {
            t9LineConverter.Convert(new String('a', 2000));
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ExpectAnExceptionByEmptyString()
        {
            t9LineConverter.Convert(String.Empty);
        }
    }
}
