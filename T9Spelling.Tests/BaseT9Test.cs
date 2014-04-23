using Microsoft.VisualStudio.TestTools.UnitTesting;
using T9Spelling.Business;
using T9Spelling.Business.Factories;
using T9Spelling.Business.Interfaces;

namespace T9SpellingTest
{
    [TestClass]
    public class BaseT9Test
    {
        public BaseT9Test()
        {
            Registry.Set<IT9Factory>(new T9Factory());
        }
    }
}
