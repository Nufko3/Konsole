namespace Konsole
{
    public class Time
    {
        public static readonly long targetFrametime = 100;
        public static long deltaTime;
        public static int sleepTime;

        static long frameStartTime;
        static long frameEndTime;

        public static void CalculateDeltaTime()
        {
            if (frameEndTime == 0)
            {
                frameEndTime = frameStartTime;
            }
            deltaTime = frameEndTime - frameStartTime;

            frameStartTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        }

        public static void WaitForNextFrame()
        {
            frameEndTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();

            sleepTime = (int)Math.Max(targetFrametime - deltaTime, 0);
            if (sleepTime > 0)
            {
                Thread.Sleep(sleepTime);
            }
        }
    }
}
