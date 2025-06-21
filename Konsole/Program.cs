namespace Konsole
{
    internal class Program
    {
        static GameObject player;

        static void Main(string[] args)
        {
            int w = -1;
            int h = -1;
            if (args.Length >= 2)
            {
                w = int.Parse(args[0]);
                h = int.Parse(args[1]);
            }

            Konsole konsole = new Konsole(w, h);
            Scene menuScene = new Scene("Menu");
            Scene scene = new Scene("Game");


            player = new GameObject("Player");
            player.tags.Add("Player");

            player.Start = new Script(new Action(PlayerStart));
            player.Start.isEnabled = true;
            player.Update = new Script(new Action(PlayerUpdate));
            player.Update.isEnabled = true;

            Sprite playerSprite = new Sprite();
            playerSprite.sprite = "#";
            playerSprite.overrideColor = true;
            playerSprite.color = ConsoleColor.Black;
            playerSprite.backgroundColor = ConsoleColor.DarkGreen;
            playerSprite.isEnabled = true;
            player.sprite = playerSprite;

            Sprite playerChildSprite = new Sprite();
            playerChildSprite.sprite = "@";
            playerChildSprite.overrideColor = true;
            playerChildSprite.color = ConsoleColor.Black;
            playerChildSprite.backgroundColor = ConsoleColor.DarkYellow;
            playerChildSprite.isEnabled = true;

            GameObject playerChild10 = new GameObject("PlayerChild10");
            playerChild10.transform = new Transform(1, 0);
            playerChild10.sprite = playerChildSprite;
            player.transform.AddChild(playerChild10.transform);

            GameObject playerChild01 = new GameObject("PlayerChild01");
            playerChild01.transform = new Transform(0, 1);
            playerChild01.sprite = playerChildSprite;
            player.transform.AddChild(playerChild01.transform);

            GameObject playerChild11 = new GameObject("PlayerChild11");
            playerChild11.transform = new Transform(1, 1);
            playerChild11.sprite = playerChildSprite;
            player.transform.AddChild(playerChild11.transform);

            GameObject playerChild22 = new GameObject("PlayerChild22");
            playerChild22.transform = new Transform(2, 2);
            playerChild22.sprite = playerChildSprite;
            playerChild11.transform.AddChild(playerChild22.transform);

            GameObject border = new GameObject("Border");
            border.Start = new Script(new Action(BorderStart));
            border.Start.isEnabled = true;
            border.LateUpdate = new Script(new Action(BorderDraw));
            border.LateUpdate.isEnabled = true;

            scene.AddGameObject(player);
            scene.AddGameObject(playerChild10);
            scene.AddGameObject(playerChild01);
            scene.AddGameObject(playerChild11);
            scene.AddGameObject(playerChild22);
            scene.AddGameObject(border);


            GameObject title = new GameObject("Title");
            Sprite titleSprite = new Sprite();
            titleSprite.sprite = "Welcome to Konsole!";
            titleSprite.isEnabled = true;
            title.sprite = titleSprite;
            menuScene.AddGameObject(title);

            title.Start = new Script(new Action(MenuStart));
            title.Start.isEnabled = true;
            title.Update = new Script(new Action(MenuUpdate));
            title.Update.isEnabled = true;

            GameObject credits = new GameObject("Credits");
            Sprite creditsSprite = new Sprite();
            creditsSprite.sprite = "A little console game engine made by Nufko3";
            creditsSprite.isEnabled = true;
            credits.sprite = creditsSprite;
            credits.transform = new Transform(-credits.sprite.sprite.Length / 4, 1);
            title.transform.AddChild(credits.transform);
            menuScene.AddGameObject(credits);

            GameObject hint = new GameObject("Hint");
            Sprite hintSprite = new Sprite();
            hintSprite.sprite = "Press Enter to Play";
            hintSprite.isEnabled = true;
            hint.sprite = hintSprite;
            hint.transform = new Transform(0, 3);
            title.transform.AddChild(hint.transform);
            menuScene.AddGameObject(hint);

            GameObject hint2 = new GameObject("Hint2");
            Sprite hint2Sprite = new Sprite();
            hint2Sprite.sprite = "Press Esc to Quit";
            hint2Sprite.isEnabled = true;
            hint2.sprite = hint2Sprite;
            hint2.transform = new Transform(0, 4);
            title.transform.AddChild(hint2.transform);
            menuScene.AddGameObject(hint2);


            SceneManager.AddScene(menuScene);
            SceneManager.AddScene(scene);

            SceneManager.LoadScene(menuScene);

            konsole.Run();
        }

        public static void PlayerStart()
        {
            player.transform.position.x = Renderer.width / 2;
            player.transform.position.y = Renderer.height / 2;
        }

        static int dirX = 1;
        static int dirY = 1;

        public static void PlayerUpdate()
        {
            player.transform.position.x += dirX;
            if (player.transform.position.x >= Renderer.width - 1)
            {
                player.transform.position.x = Renderer.width - 2;
                dirX = -1;
            }
            else if (player.transform.position.x <= 1)
            {
                player.transform.position.x = 1;
                dirX = 1;
            }

            player.transform.position.y += dirY;
            if (player.transform.position.y >= Renderer.height - 1)
            {
                player.transform.position.y = Renderer.height - 2;
                dirY = -1;
            }
            else if (player.transform.position.y <= 1)
            {
                player.transform.position.y = 1;
                dirY = 1;
            }
        }

        static string borderX = string.Empty;

        public static void BorderStart()
        {
            borderX = new string('#', Renderer.width);
        }

        public static void BorderDraw()
        {
            Renderer.ResetColor();
            Renderer.WriteAt(borderX, 0, 0);
            Renderer.WriteAt(borderX, 0, Renderer.height - 1);
            for (int i = 1; i < Renderer.height - 1; i++)
            {
                Renderer.WriteAt("#", 0, i);
                Renderer.WriteAt("#", Renderer.width - 1, i);
            }
        }

        public static void MenuStart()
        {
            if (SceneManager.scene.name == "Menu")
            {
                GameObject title = SceneManager.scene.gameObjects["Title"];
                title.transform.position.x = Renderer.width / 2 - title.sprite.sprite.Length / 2;
                title.transform.position.y = 10;
            }
        }

        public static void MenuUpdate()
        {
            if (Input.wasPressedThisFrame && Input.lastKey == ConsoleKey.Enter)
            {
                if (SceneManager.scene.name == "Menu")
                {
                    SceneManager.LoadScene("Game");
                }
            }
        }

        public static void DrawBlueScreen(string exception, string msg)
        {
            Renderer.SetColor(ConsoleColor.White);
            Renderer.SetBackgroundColor(ConsoleColor.DarkBlue);
            Renderer.Clear();

            Renderer.SetBackgroundColor(ConsoleColor.White);
            Renderer.WriteAt("####", 4, 2);
            Renderer.WriteAt("####", 4, 3);
            Renderer.WriteAt("####", 4, 8);
            Renderer.WriteAt("####", 4, 9);
            Renderer.WriteAt("######", 12, 2);
            Renderer.WriteAt("##", 12, 3);
            Renderer.WriteAt("##", 12, 4);
            Renderer.WriteAt("##", 12, 5);
            Renderer.WriteAt("##", 12, 6);
            Renderer.WriteAt("##", 12, 7);
            Renderer.WriteAt("##", 12, 8);
            Renderer.WriteAt("######", 12, 9);

            Renderer.SetBackgroundColor(ConsoleColor.DarkBlue);
            Renderer.WriteAt(exception, 4, 12);
            Renderer.WriteAt(msg, 4, 14);

            Renderer.WriteAt("Press Esc to Quit", 0, Renderer.height - 2);
            Renderer.WriteAt("Press any key to continue...", 0, Renderer.height - 1);
            Renderer.ResetColor();

            ConsoleKey key = Console.ReadKey().Key;
            if (key == ConsoleKey.Escape)
            {
                Environment.Exit(1);
            }
        }
    }
}
