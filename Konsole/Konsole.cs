namespace Konsole
{
    public class Konsole
    {
        public Konsole(int width = -1, int height = -1)
        {
            if (width != -1 && height != -1)
            {
                Renderer.GetCursorOffset();
                Renderer.width = width;
                Renderer.height = height;
                Renderer.isReady = true;
            }
            else
            {
                Renderer.GetSize();
            }
        }

        public void Run()
        {
            Renderer.ResetColor();
            Renderer.Clear();

            while (true)
            {
                Input.GetInput();

                if (Input.wasPressedThisFrame)
                {
                    if (Input.lastKey == ConsoleKey.Escape)
                    {
                        break;
                    }
                }

                Time.CalculateDeltaTime();

                // Update
                foreach (GameObject gameObject in SceneManager.scene.gameObjects.Values)
                {
                    if (gameObject.Update.isEnabled)
                    {
                        gameObject.Update.action.Invoke();
                    }
                    gameObject.transform.CalculateOffset(); // ???
                    gameObject.transform.UpdateChildren();
                }
                // =====

                Renderer.ResetColor();
                Renderer.Clear();

                // Render
                foreach (GameObject gameObject in SceneManager.scene.gameObjects.Values)
                {
                    if (gameObject.Draw.isEnabled)
                    {
                        gameObject.Draw.action.Invoke();
                    }
                    gameObject.Render();
                }
                // ====

                // Late Update
                foreach (GameObject gameObject in SceneManager.scene.gameObjects.Values)
                {
                    if (gameObject.LateUpdate.isEnabled)
                    {
                        gameObject.LateUpdate.action.Invoke();
                    }
                }
                // ====

                DrawDebugUI();

                Time.WaitForNextFrame();
            }

            Renderer.ResetColor();
            Renderer.WriteAt(Renderer.width.ToString() + "," + Renderer.height.ToString(), 0, Renderer.height - 2);
            Renderer.WriteAt("Press any key to continue...", 0, Renderer.height - 1);
            Console.ReadKey();
        }

        void DrawDebugUI()
        {
            Renderer.SetColor(ConsoleColor.Black);
            Renderer.SetBackgroundColor(ConsoleColor.DarkGreen);
            Renderer.WriteAt("Input:" + Input.lastKey.ToString(), 0, Renderer.height - 4);
            Renderer.WriteAt("T" + Time.targetFrametime.ToString(), 0, Renderer.height - 3);
            Renderer.WriteAt("D" + Time.deltaTime.ToString(), 0, Renderer.height - 2);
            Renderer.WriteAt("S" + Time.sleepTime.ToString(), 0, Renderer.height - 1);
        }
    }
}
