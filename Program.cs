using System.Runtime.InteropServices;

namespace SimpleShutdownHandler;

internal static class Program
{
    public static void Main(string[] args)
    {
        WaitForPosixShutdownSignal();
    }

    private static void WaitForPosixShutdownSignal()
    {
        using var reg = PosixSignalRegistration.Create(
            PosixSignal.SIGINT,
            handler: context =>
            {
                Console.WriteLine($"ShutdownHandler: 'Started after {context.Signal} signal received!'");
                Thread.Sleep(1000);
                Console.WriteLine("ShutdownHandler: 'Completed handler'");
            });
        
        Thread.Sleep(-1);
    }

    private static void AppDomainProcessExit()
    {
        AppDomain.CurrentDomain.ProcessExit += (sender, args) =>
        {
            Console.WriteLine("Exiting");
            Thread.Sleep(1000);
            Console.WriteLine("Completed handler");
        };
    }
}