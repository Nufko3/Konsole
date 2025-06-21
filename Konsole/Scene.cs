namespace Konsole
{
    public class Scene
    {
        public int id;
        public string name = "Scene";
        public Dictionary<string, GameObject> gameObjects;

        int gameObjectCount = 0;

        public Scene(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                this.name = name;
            }
            else
            {
                throw new ArgumentNullException();
            }

            gameObjects = new Dictionary<string, GameObject>();
        }

        public void AddGameObject(GameObject gameObject)
        {
            gameObject.id = gameObjectCount;
            gameObjects.Add(gameObject.name, gameObject);
            gameObjectCount++;
        }

        public void RemoveGameObject(GameObject gameObject)
        {
            gameObjects.Remove(gameObject.name);
        }
    }
}
