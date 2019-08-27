using System;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using System.Windows.Forms;
using SmokeTest;
using SmokeTest.Properties;
using NUnit.Framework;

class ProgramProxy : Program.IProxy
{
    public void Run(string path)
    {
    }

    public Process LaunchProcess_process { get; set; }
    public Exception LaunchProcess_throw = null;

    public bool LaunchProcess(Process process)
    {
        this.LaunchProcess_process = process;
        if (LaunchProcess_throw != null)
        {
            throw LaunchProcess_throw;
        }
        return true;
    }

    public Exception GetProcessorArchitecture_throw;
    public ProcessorArchitecture GetProcessorArchitecture_ = ProcessorArchitecture.MSIL;

    public ProcessorArchitecture GetAssemblyProcessorArchitecture(string path)
    {
        if (GetProcessorArchitecture_throw != null)
        {
            throw GetProcessorArchitecture_throw;
        }
        return GetProcessorArchitecture_;
    }

    public ProcessorArchitecture ProcessArchitecture_ = ProcessorArchitecture.MSIL;

    public ProcessorArchitecture ProcessArchitecture
    {
        get
        {
            return ProcessArchitecture_;
        }
    }

    public string NotifyOperator_notice { get; set; }

    public void NotifyOperator(string who, string what, string why, string todo, MessageBoxIcon icon = MessageBoxIcon.Information)
    {
        NotifyOperator_notice = who + what + why + todo;
    }

    public string ExecutingAssemblyDirectory
    {
        get
        {
            return @"P:\MyDirectory";
        }
    }
}

[TestFixture]
public class ProgramTestFixture
{
    ProgramProxy proxy = new ProgramProxy();

    [TestFixtureSetUp]
    public void TestFixtureSetUp()
    {
        Program.proxy = proxy;
    }

    [SetUp]
    public void Setup()
    {
        proxy.GetProcessorArchitecture_throw = null;
        proxy.GetProcessorArchitecture_ = ProcessorArchitecture.MSIL;
        proxy.ProcessArchitecture_ = ProcessorArchitecture.MSIL;
    }

    [Test]
    public void LaunchSmokeTestPlatformExecutableWithFullPath()
    {
        proxy.GetProcessorArchitecture_ = ProcessorArchitecture.X86;
        Assert.AreEqual(@"P:\MyDirectory\x86\smoketest.exe", Program.CreateProcess("smoketest.exe").StartInfo.FileName);
    }

    [Test]
    public void DoNotLaunchSmokeTestProcessIfExecutingArchitectureSameAsInput()
    {
        proxy.ProcessArchitecture_ = ProcessorArchitecture.X86;
        proxy.GetProcessorArchitecture_ = ProcessorArchitecture.X86;
        Assert.IsNull(Program.CreateProcess("smoketest.exe"));
    }

    [Test]
    [ExpectedException(typeof(Program.UnsupportedArchitectureException))]
    public void WhenProcesserArchitectureNotSupported()
    {
        proxy.GetProcessorArchitecture_ = ProcessorArchitecture.None;
        Program.CreateProcess("smoketest.exe");
    }
}
