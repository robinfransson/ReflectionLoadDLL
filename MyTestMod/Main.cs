using ModLoader;
using System;

namespace MyTestMod
{
    public class Main : IMod
    {
        public string ModName { get; } = "MyTestMod"; 

        public void Test()
        {
            Console.WriteLine("Test invoked!");
        }

        public void OnKeyPress(KeyboardEvent e)
        {
            if (e.Key == ConsoleKey.A)
                Console.WriteLine(AddNumbers(5, 7));

            else if(e.Key == ConsoleKey.M)
                Console.WriteLine("Funkar också");

            else if(e.Key == ConsoleKey.Y)
                Console.WriteLine(AnotherClass.AnotherMethod());
        }

        public int AddNumbers(int a, int b)
        {
            return a + b;
        }

        public void OnLoad(IHelper helper)
        {
            Console.WriteLine($"Loaded {ModName}");
            helper.OnKeyPress += OnKeyPress;
        }
    }
}
