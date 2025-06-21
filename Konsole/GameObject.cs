namespace Konsole
{
    public class GameObject
    {
        public int id;
        public string name = "GameObject";
        public List<string> tags = new List<string>();

        public Transform transform = new Transform(0, 0);
        public Sprite sprite = new Sprite();

        public Script Start;
        public Script Update;
        public Script Draw;
        public Script LateUpdate;

        public GameObject(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                this.name = name;
            }
            else
            {
                throw new ArgumentNullException();
            }

            Action empty = new Action(() => { });
            Start = new Script(empty);
            Update = new Script(empty);
            Draw = new Script(empty);
            LateUpdate = new Script(empty);
        }

        public void Render()
        {
            if (sprite.isEnabled)
            {
                if (sprite.overrideColor)
                {
                    Renderer.SetColor(sprite.color);
                    Renderer.SetBackgroundColor(sprite.backgroundColor);
                }
                Renderer.WriteAt(sprite.sprite, transform.position.x, transform.position.y);
            }
        }
    }
}
