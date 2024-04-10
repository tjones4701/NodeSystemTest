// See https://aka.ms/new-console-template for more information
using ConsoleApp1.src;
Console.WriteLine("Starting Application");

TestScenario test = new TestScenario();
test.Setup();




bool hasStarted = false;
while (hasStarted == false)
{
    Console.WriteLine("Press Space to start ...");
    ConsoleKeyInfo keyPressed = Console.ReadKey();
    if (keyPressed.Key == ConsoleKey.Spacebar)
    {
        hasStarted = true;
    }
}




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
