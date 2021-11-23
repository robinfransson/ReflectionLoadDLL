using ModLoader;
using System;

namespace MyTestMod
{
    public class Main : IMod
    {
        public string ModName { get; } = "MyTestMod"; 

        public void OnLoad()
        {
            Console.WriteLine($"Loaded {ModName}");
            Console.WriteLine(AnotherClass.AnotherMethod());
        }

        public void Test()
        {
            Console.WriteLine("Test invoked!");
        }

        public int AddNumbers(int a, int b)
        {
            return a + b;
        }
    }
}
