using ModLoader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace LoadDLL
{
    public class LoadMods
    {
        private List<IMod> LoadedMods = new();


        public List<IMod> GetExternObjs()
        {
            LoadFileNames();
            return LoadedMods;
        }

        private void LoadFileNames()
        {
            var dllFiles = GetDlls();
            List<Assembly> assemblies = new();
            foreach(var name in dllFiles)
            {
                var assembly = Assembly.LoadFile(name);
                assemblies.Add(assembly);
            }

            CreateInstances(assemblies);

        }

        private void CreateInstances(List<Assembly> assemblies)
        {
            foreach(var dll in assemblies)
            {
                var name = dll.GetName().Name;
                IMod modObj = dll.CreateInstance(name + ".Main") as IMod;

                if (modObj is null)
                    continue;

                LoadedMods.Add(modObj);

            }
        }

        private IEnumerable<string> GetDlls()
        {
            var pwd = Directory.GetCurrentDirectory();
            var modFile = File.ReadAllLines(Directory.GetCurrentDirectory() + "\\mods.txt");
            var mods = modFile.Where(line => line.StartsWith("mod=")).Select(line => Directory.GetCurrentDirectory() + "\\mods\\" + line.Split("mod=")[1] + ".dll").ToList();
            bool done = false;
            int i = 0;
            while (!done)
            {
                string current = mods[i];
                if (!FileExists(current))
                    mods.Remove(current);
                if (mods.Count == i + 1)
                    done = !done;
            }
            return mods;
        }

        private bool FileExists(string file)
        {
            var exists = File.Exists(file);
            return exists;

        }

    

       
    }


}
