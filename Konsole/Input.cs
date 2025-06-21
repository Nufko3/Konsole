namespace Konsole
{
    public class Input
    {
        public static bool wasPressedThisFrame = false;
        public static ConsoleKey lastKey;

        public static void GetInput()
        {
            wasPressedThisFrame = Console.KeyAvailable;
            if (wasPressedThisFrame)
            {
                lastKey = Console.ReadKey().Key;
            }
        }
    }
}
