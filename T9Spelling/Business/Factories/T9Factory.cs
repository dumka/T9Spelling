using T9Spelling.Business.Interfaces;

namespace T9Spelling.Business.Factories
{
    public class T9Factory : IT9Factory
    {
        public IT9TextConverter CreateT9TextConverter()
        {
            return new T9TextConverter();
        }

        public IT9LineConverter CreateT9LineConverter()
        {
            return new T9LineConverter();
        }
    }
}
