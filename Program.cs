// See https://aka.ms/new-console-template for more information
using ConsoleApp1.src;

Console.WriteLine("Hello, World!");

TestScenario test = new TestScenario();
test.Setup();
bool isExit = false;
while (isExit == false)
{
    Console.WriteLine("Press key to trigger a tick...");

    ConsoleKeyInfo keyPressed = Console.ReadKey();
    if (keyPressed.Key == ConsoleKey.Escape)
    {
        isExit = true;
    }

    test.Update();
}
