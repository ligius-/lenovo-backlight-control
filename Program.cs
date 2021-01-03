using System;
using System.Reflection;

namespace LenovoBacklightControl
{
    static class Program
    {
        static (MethodInfo, object) GetMethodFromAssembly(string dllFile, string objectName, string methodName)
        {
            //https://stackoverflow.com/questions/4804494/p-invoking-function-via-a-mangled-name
            //https://stackoverflow.com/questions/5713577/how-to-import-a-class-from-dll

            Type assemblyObject = Assembly.LoadFile(dllFile).GetType(objectName);
            object instance = Activator.CreateInstance(assemblyObject);
            var method = Array.Find(assemblyObject.GetMethods(), element => element.Name.Equals(methodName));
            return (method, instance);
        }

        static int Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("No valid arguments found!");
                Console.WriteLine("Program arguments: <DLL path> <Backlight Level: 0-2>");
                Console.WriteLine("Example: thinkpadbacklight.exe \"c:\\Users\\All Users\\Lenovo\\ImController\\Plugins\\ThinkKeyboardPlugin\\x86\\Keyboard_Core.dll\" 1");
                Console.WriteLine("");
                Console.WriteLine("Copyright Ligius 2021, version 0.2");
                return 1;
            }
            else
            {
                (MethodInfo method, object instance) = GetMethodFromAssembly(args[0], "Keyboard_Core.KeyboardControl", "SetKeyboardBackLightStatus");

                object[] arguments = new object[] { Int32.Parse(args[1]), null };
                uint output = (uint)method.Invoke(instance, arguments);

                Console.Write(output);
                return (int)output;
            }
        }
    }
}
