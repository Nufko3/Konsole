namespace Konsole
{
    public class Script : Component
    {
        public Action action;

        public Script(Action action)
        {
            this.action = action;
        }
    }
}
