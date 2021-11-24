using ModLoader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ModLoader
{
    public class LoadMods
    {
        private List<IMod> LoadedMods = new();


        public List<IMod> GetInstalledMods()
        {
            LoadFileNames();
            return LoadedMods;
        }

        private void LoadFileNames()
        {
            var dllFiles = GetDlls();
            List<Assembly> assemblies = new();
            foreach (var name in dllFiles)
            {
                var assembly = Assembly.LoadFile(name);
                assemblies.Add(assembly);
            }

            CreateInstances(assemblies);

        }

        private void CreateInstances(List<Assembly> assemblies)
        {
            foreach (var dll in assemblies)
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


            if (!Directory.Exists(pwd + "\\mods"))
                Directory.CreateDirectory(pwd + "\\mods");

            var mods = Directory.GetFiles(pwd + "\\mods").Where(file => file.EndsWith(".dll")).ToList();
            for(int i = 0; i < mods.Count(); i++)
            {
                string current = mods.ElementAt(i);
                if (!File.Exists(current))
                    mods.Remove(current);
            }
            return mods;
        }


    

       
    }


}
