using ModLoader;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;

namespace TestModLoad
{
    class Program
    {
        static void Main(string[] args)
        {
            List<IMod> mods = new LoadMods().GetInstalledMods();

            if (!mods.Any())
                Exit();

            foreach (var mod in mods)
            {
                mod.OnLoad();
            }



            while (true)
            {
                Console.WriteLine("Enter a mod to invoke: ");
                var modToLoad = Console.ReadLine();
                var mod = mods.FirstOrDefault(x => x.ModName == modToLoad);
                if (mod is null)
                    continue;

                Console.WriteLine("Enter a method to invoke: ");
                Console.WriteLine(AvailableMethods(mod));
                var method = Console.ReadLine();
                var parameters = GetParameters(mod, method);
                if (parameters is null)
                    continue;
                if (parameters.Any())
                {
                    int stopAt = parameters.Count();
                    dynamic val = new ExpandoObject();

                    val.list = new List<dynamic>();
                    for (int i = 0; i < stopAt; i++)
                    {
                        var parameter = parameters.ElementAt(i);
                        Console.WriteLine("Enter the parameter for " + parameter.Name + " (" + parameter.ParameterType.ToString() + ")");
                        val.list.Add(Convert.ChangeType(Console.ReadLine(), parameter.ParameterType));
                    }
                    val.list = val.list.ToArray();
                    Console.WriteLine(RunFunction(mod, method, val.list));

                }
                else
                {
                    Console.WriteLine(RunFunction(mod, method, null));
                }
            }
        }

        private static void Exit()
        {
            Console.WriteLine("No mods in mod folder.. exiting");

            Environment.Exit(0);
        }

        private static string AvailableMethods(IMod mod)
        {
            var methods = mod.GetType().GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance);
            return string.Join("\n", methods.Select(x => x.Name));
        }

        private static IEnumerable<MethodParameterInfo> GetParameters(IMod mod, string method)
        {
            var typeInfo = mod.GetType();
            var methodInfo = typeInfo.GetMethod(method);

            if (AnyNull(mod, methodInfo))
                return null;
            var parameters = methodInfo.GetParameters();
            return parameters.Select(parameter => new MethodParameterInfo() 
            {
                Position = parameter.Position, 
                Name = parameter.Name, 
                ParameterType = parameter.ParameterType 
            });

        }


        private static bool AnyNull(params object[] objs)
        {
            for(int i = 0; i < objs.Length; i++)
            {
                if (objs[i] is null)
                    return true;
            }
            return false;
        }

        static string RunFunction(IMod mod, string methodName, object[] parameters)
        {

            var typeInfo = mod.GetType();
            var method = typeInfo.GetMethod(methodName);
            var returnedValue = method.Invoke(mod, parameters);
            var returnType = method.ReturnType;

            if (returnType == typeof(void))
                return "";
            return returnedValue.ToString();
        }
    }
}
