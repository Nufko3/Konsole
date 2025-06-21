namespace Konsole
{
    public class Transform : Component
    {
        public Vector2 position;

        public Transform? parent;
        public List<Transform> children = new List<Transform>();
        public Vector2? offset;

        public Transform(int x, int y)
        {
            position = new Vector2(x, y);
            isEnabled = true;
        }

        public Transform(Vector2 position)
        {
            this.position = position;
            isEnabled = true;
        }

        public void UpdateChildren()
        {
            if (children.Count > 0)
            {
                foreach (Transform child in children)
                {
                    if (child.offset != null)
                    {
                        child.position = position + child.offset;
                    }
                }
            }
        }

        public void AddChild(Transform child)
        {
            child.parent = this;
            child.CalculateOffset();
            children.Add(child);
        }

        public void CalculateOffset()
        {
            if (parent != null)
            {
                offset = position - parent.position;
            }
        }
    }
}
