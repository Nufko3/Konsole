namespace Konsole
{
    public class SceneManager
    {
        public static Scene scene;
        public static Dictionary<string, Scene> scenes = new Dictionary<string, Scene>();

        static int sceneCount = 0;

        public static void LoadScene(Scene newScene)
        {
            LoadScene(newScene.name);
        }

        public static void LoadScene(int id)
        {
            foreach (Scene _scene in scenes.Values)
            {
                if (_scene.id == id)
                {
                    LoadScene(_scene.name);
                    break;
                }
            }
        }

        public static void LoadScene(string name)
        {
            scene = scenes[name];
            RunStart();
        }

        public static void AddScene(Scene newScene)
        {
            newScene.id = sceneCount;
            scenes.Add(newScene.name, newScene);
            sceneCount++;
        }

        public static void RunStart()
        {
            foreach (GameObject gameObject in scene.gameObjects.Values)
            {
                if (gameObject.Start.isEnabled)
                {
                    gameObject.Start.action.Invoke();
                }
            }
        }
    }
}
