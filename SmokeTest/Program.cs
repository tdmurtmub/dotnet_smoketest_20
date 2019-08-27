using System;
using System.IO;
using System.Collections.Generic;
using System.Reflection;
using System.Diagnostics;
using System.Windows.Forms;

namespace SmokeTest
{
	static class Program
	{
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Launch(args);
        }

        internal interface IProxy
        {
            string ExecutingAssemblyDirectory { get; }
            ProcessorArchitecture ProcessArchitecture { get; }
            ProcessorArchitecture GetAssemblyProcessorArchitecture(string path);
            bool LaunchProcess(Process process);
            void NotifyOperator(string who, string what, string why, string todo, MessageBoxIcon icon = MessageBoxIcon.Information);
            void Run(string path);
        }

        internal class Proxy : IProxy
        {
            public void Run(string path)
            {
                Application.Run(new MainForm(path));
            }

            public void NotifyOperator(string who, string what, string why, string todo, MessageBoxIcon icon = MessageBoxIcon.Information)
            {
                MessageBox.Show(
                    what.ToUpper() + Environment.NewLine + Environment.NewLine +
                    who + Environment.NewLine + Environment.NewLine +
                    why + Environment.NewLine + Environment.NewLine +
                    todo,
                    String.Format("{0} ({1}) | {2}",
                        Application.ProductName,
                        Application.ProductVersion,
                        AssemblyCopyright),
                    MessageBoxButtons.OK, icon);
            }

            public ProcessorArchitecture GetAssemblyProcessorArchitecture(string path)
            {
                return AssemblyName.GetAssemblyName(path).ProcessorArchitecture;
            }

            public ProcessorArchitecture ProcessArchitecture
            {
                get 
                {
                    return AssemblyName.GetAssemblyName(Assembly.GetExecutingAssembly().Location).ProcessorArchitecture;
                }
            }

            public string ExecutingAssemblyDirectory
            {
                get
                {
                    return new FileInfo(Assembly.GetExecutingAssembly().Location).Directory.FullName;
                }
            }

            public bool LaunchProcess(Process process)
            {
                Debug.Assert(process != null);
                return process.Start();
            }
        }

        /// <summary>
        /// Launch the application for the given input arguments.
        /// </summary>
        /// <param name="args"></param>
        internal static void Launch(string[] args)
        {
            if (args.Length > 0)
            {
                FileInfo inputInfo = null;
                try
                {
                    inputInfo = new FileInfo(args[0]);
                    Assembly assembly = Assembly.ReflectionOnlyLoadFrom(inputInfo.FullName);
                    Process process = null;
                    try
                    {
                        process = CreateProcess(inputInfo.FullName);
                        if (process == null)
                        {
                            proxy.Run(args[0]);
                        }
                        else
                        {
                            proxy.LaunchProcess(process);
                        }
                    }
                    catch (UnsupportedArchitectureException ex)
                    {
                        proxy.NotifyOperator(inputInfo.FullName, "UNSUPPORTED architecture", ex.Message, "Reinstall the program and try again.", MessageBoxIcon.Error);
                    }
                    catch (Exception ex)
                    {
                        proxy.NotifyOperator(process.StartInfo.FileName, "unexpected error", ex.Message, "Reinstall the program and try again.", MessageBoxIcon.Error);
                    }
                }
                catch (BadImageFormatException)
                {
                    proxy.NotifyOperator(inputInfo.Name, "UNSUPPORTED FILE TYPE", String.Format("The above file, found under {0}, is not a managed Assembly file (DLL or EXE) or targets a .NET runtime newer than 2.0.", inputInfo.DirectoryName), "Try again with a valid managed Assembly file or upgrade to a newer version of SmokeTest.", MessageBoxIcon.Exclamation);
                }
                catch (FileNotFoundException)
                {
                    proxy.NotifyOperator(inputInfo.Name, "FILE NOT FOUND", "The above file was not found.", "Select a valid managed assembly file (DLL or EXE) as input.", MessageBoxIcon.Exclamation);
                }
                catch (Exception exception)
                {
                    proxy.NotifyOperator(inputInfo.Name, "ERROR", exception.Message, "Try again with a valid managed Assembly file.", MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                proxy.Run(null);
            }
        }

        public static string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        [Serializable]
        internal class UnsupportedArchitectureException : Exception
        {
            public UnsupportedArchitectureException(string message)
                : base(message)
            {

            }
        }

        /// <summary>
        /// Create a Process if [inputPath] identifies a platform target other than "Any CPU" otherwise return a null.
        /// </summary>
        /// <param name="inputPath"></param>
        /// <returns></returns>
        internal static Process CreateProcess(string inputPath)
        {
            var architecture = proxy.GetAssemblyProcessorArchitecture(inputPath);
            if (proxy.ProcessArchitecture == architecture) { return null; }
            var process = new System.Diagnostics.Process();
            process.StartInfo.FileName = proxy.ExecutingAssemblyDirectory;
            switch (architecture)
            {
                case ProcessorArchitecture.MSIL:
                    return null;
                case ProcessorArchitecture.X86:
                    process.StartInfo.FileName += @"\x86";
                    break;
                case ProcessorArchitecture.IA64:
                case ProcessorArchitecture.Amd64:
                    process.StartInfo.FileName += @"\x64";
                    break;
                default:
                    throw new UnsupportedArchitectureException(String.Format("ProcessorArchitecture {0} is not supported.", architecture));
            }
            process.StartInfo.FileName += @"\smoketest.exe";
            process.StartInfo.Arguments = inputPath;
            return process;
        }

        internal static IProxy proxy = new Proxy();
    }
}
