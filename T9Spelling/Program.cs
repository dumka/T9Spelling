using System;
using System.IO;
using System.Text.RegularExpressions;
using T9Spelling.Business;
using T9Spelling.Business.Factories;
using T9Spelling.Business.Interfaces;
using T9Spelling.Properties;

namespace T9Spelling
{
    /// <summary>
    /// Main program.
    /// </summary>
    class Program
    {
        public static void Main()
        {
            Registry.Set<IT9Factory>(new T9Factory());
            t9TextConverter = Registry.Get<IT9Factory>().CreateT9TextConverter();

            TestT9SpellingApplication();
        }

        private static void TestT9SpellingApplication()
        {
            string sourcePath = GetPath(Resources.PleaseInputPathToSourceFile);
            if (String.IsNullOrEmpty(sourcePath))
                return;
            
            string destinationPath = GetPath(Resources.PleaseInputPathToDestinationFile);
            if (String.IsNullOrEmpty(destinationPath))
                return;            
            
            try
            {
                using (StreamReader source = new StreamReader(sourcePath))
                {
                    using (StreamWriter destination = new StreamWriter(destinationPath))
                    {
                        t9TextConverter.ConvertText(source, destination);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(String.Format(Resources.Error, e.Message));
            }  
            finally
            {
                Console.WriteLine(Resources.WorkIsCompleted);
                Console.ReadKey();
            }
        }
        
        private static string GetPath(string message)
        {
            const string exitString = "/q";
            string path = String.Empty;
            Regex pathValidator = new Regex(@"^(([a-zA-Z]:|\\)\\)?(((\.)|(\.\.)|([^\\/:\*\?""\|<>\. ](([^\\/:\*\?""\|<>\. ])|([^\\/:\*\?""\|<>]*[^\\/:\*\?""\|<>\. ]))?))\\)*[^\\/:\*\?""\|<>\. ](([^\\/:\*\?""\|<>\. ])|([^\\/:\*\?""\|<>]*[^\\/:\*\?""\|<>\. ]))?$");
            
            while (String.IsNullOrEmpty(path))
            {
                Console.WriteLine(String.Format(Resources.InputMessageMask, message, exitString));
                path = Console.ReadLine();
                
                if (path == exitString) return null;
                
                if (!pathValidator.IsMatch(path))
                {
                    Console.WriteLine(Resources.YourPathHasIncorrectSymbols);
                    path = string.Empty;
                }
            }
            return path;
        }

        private static IT9TextConverter t9TextConverter;
    }
}
