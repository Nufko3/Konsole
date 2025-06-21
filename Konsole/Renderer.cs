namespace Konsole
{
    public class Renderer
    {
        public static int width = -1;
        public static int height = -1;
        public static bool isReady = false;

        static int origRow;
        static int origCol;

        public static bool WriteAt(string s, int x, int y)
        {
            if (x > width - 1 || y > height - 1 || x < 0 || y < 0)
            {
                return false;
            }

            try
            {
                Console.SetCursorPosition(origCol + x, origRow + y);
                if (!string.IsNullOrEmpty(s))
                {
                    Console.Write(s);
                }
                return true;
            }
            catch (ArgumentOutOfRangeException e)
            {
                Program.DrawBlueScreen(nameof(ArgumentOutOfRangeException), e.Message);
                return false;
            }
        }

        public static void SetColor(ConsoleColor color)
        {
            Console.ForegroundColor = color;
        }

        public static void SetBackgroundColor(ConsoleColor color)
        {
            Console.BackgroundColor = color;
        }

        public static void ResetColor()
        {
            Console.ResetColor();
        }

        public static void Clear()
        {
            Console.Clear();
        }

        public static void GetCursorOffset()
        {
            Clear();
            origRow = Console.CursorTop;
            origCol = Console.CursorLeft;
        }

        public static void GetSize()
        {
            GetCursorOffset();
            width = Console.WindowWidth;
            height = Console.WindowHeight;

            int bufferWidth = Console.BufferWidth;
            int bufferHeight = Console.BufferHeight;
            if (width != bufferWidth)
            {
                Program.DrawBlueScreen("PasstNichtException", "WindowWidth and BufferWidth do not match\nWindowWidth: " + width.ToString() + "\nBufferWidth: " + bufferWidth.ToString());
            }
            if (height != bufferHeight)
            {
                Program.DrawBlueScreen("PasstNichtException", "WindowHeight and BufferHeight do not match\nWindowHeight: " + height.ToString() + "\nBufferHeight: " + bufferHeight.ToString());
            }

            isReady = true;
        }
    }
}
