using System;
using System.Linq;
using System.Reflection;
using System.Collections;

namespace ConsoleApp1
{
    static class Program
    {

        static MethodInfo GetRuntimeMethodsExt(this Type type, string name, params Type[] types)
        {
            // https://stackoverflow.com/questions/21307845/runtimereflectionextensions-getruntimemethod-does-not-work-as-expected
            // Find potential methods with the correct name and the right number of parameters
            // and parameter names
            var potentials = (from ele in type.GetMethods()
                              where ele.Name.Equals(name)
                              //let param = ele.GetParameters()
                              //where param.Length == types.Length
                              //&& param.Select(p => p.ParameterType.Name).SequenceEqual(types.Select(t => t.Name))
                              select ele);

            // Maybe check if we have more than 1? Or not?
            return potentials.FirstOrDefault();
        }

        static int Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("No arguments found!");
                Console.WriteLine("Program arguments: <DLL path> <Backlight Level: 0-2>");
                Console.WriteLine("Example: thinkpadbacklight.exe \"c:\\Users\\All Users\\Lenovo\\ImController\\Plugins\\ThinkKeyboardPlugin\\x86\\Keyboard_Core.dll\" 1");
                Console.WriteLine("");
                Console.WriteLine("Copyright Ligius 2017, version 0.1");
                return 1;
            }
            else
            {
                //https://stackoverflow.com/questions/4804494/p-invoking-function-via-a-mangled-name
                //https://stackoverflow.com/questions/5713577/how-to-import-a-class-from-dll

                //[DllImport(@"c:\Users\All Users\Lenovo\ImController\Plugins\ThinkKeyboardPlugin\x86\Keyboard_Core.dll")]

                Assembly myAssembly;
                //"c:\\Users\\All Users\\Lenovo\\ImController\\Plugins\\ThinkKeyboardPlugin\\x86\\Keyboard_Core.dll"
                myAssembly = Assembly.LoadFile(args[0]);

                object obj;
                Type myType = myAssembly.GetType("Keyboard_Core.KeyboardControl");
                obj = Activator.CreateInstance(myType);
                IEnumerable list = myType.GetMethods();
                //MethodInfo getKeyboardBackLightLevelInfo = myType.GetMethod("GetKeyboardBackLightLevel", new Type[] { typeof(Int32) });
                MethodInfo getKeyboardBackLightLevelInfo = GetRuntimeMethodsExt(myType, "GetKeyboardBackLightLevel", new Type[] { });
                MethodInfo setKeyboardBackLightStatusInfo = GetRuntimeMethodsExt(myType, "SetKeyboardBackLightStatus", new Type[] { });

                object[] arguments = new object[] { Int32.Parse(args[1]) };
                UInt32 output = (UInt32)setKeyboardBackLightStatusInfo.Invoke(obj, arguments);

                //UInt32 output = (UInt32)getKeyboardBackLightLevelInfo.Invoke(obj, arguments);
                Console.Write(output);
                /*o = (Keyboard_Core.KeyboardControl)Activator.CreateInstance(myType);
                int output = -999;
                o.GetKeyboardBackLightLevel(out output);
                Console.WriteLine(output);*/
                return (int)output;
            }
        }
    }
}
