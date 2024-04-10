// See https://aka.ms/new-console-template for more information
using ConsoleApp1.src;
Console.WriteLine("Starting Application");

TestScenario test = new TestScenario();
test.Setup();


Console.WriteLine("Press key to trigger a tick...");

Thread thread = new Thread(() => test.Run());
thread.Start();


bool isExit = false;
while (isExit == false)
{
    ConsoleKeyInfo keyPressed = Console.ReadKey();
    if (keyPressed.Key == ConsoleKey.Escape)
    {
        isExit = true;
        thread.Interrupt();
    }
}
