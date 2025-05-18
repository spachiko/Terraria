namespace Terraria
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Audio;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;

    public class Main : Game
    {
        public static int[] availableRecipe = new int[Recipe.maxRecipes];
        public static float[] availableRecipeY = new float[Recipe.maxRecipes];
        public static int background = 0;
        public static int[] backgroundHeight = new int[3];
        public static Texture2D[] backgroundTexture = new Texture2D[3];
        public static int[] backgroundWidth = new int[3];
        public static Texture2D blackTileTexture;
        public const float bottomWorld = 40000f;
        public static Texture2D cursorTexture;
        public const double dayLength = 40000.0;
        public static bool dayTime = true;
        public static bool debugMode = false;
        public static bool dumbAI = false;
        public static Dust[] dust = new Dust[0x3e8];
        public static Texture2D dustTexture;
        public static int focusRecipe;
        public static SpriteFont fontDeathText;
        public static SpriteFont fontItemStack;
        public static SpriteFont fontMouseText;
        public static bool godMode = false;
        private GraphicsDeviceManager graphics;
        public static Texture2D heartTexture;
        public static float[] hotbarScale = new float[]
        {
            1f,
            0.75f,
            0.75f,
            0.75f,
            0.75f,
            0.75f,
            0.75f,
            0.75f,
            0.75f,
            0.75f,
        };
        public static Texture2D hotbarTexture;
        public static Texture2D inventoryBackTexture;
        private static float inventoryScale = 0.75f;
        public static Item[] item = new Item[0x3e8];
        public static Texture2D[] itemTexture = new Texture2D[0x1b];
        public static KeyboardState keyState = Keyboard.GetState();
        public const float leftWorld = 0f;
        public const int maxBackgrounds = 3;
        public const int maxDust = 0x3e8;
        public const int maxInventory = 40;
        public const int maxItems = 0x3e8;
        public const int maxItemSounds = 2;
        public const int maxItemTypes = 0x1b;
        public const int maxNPCHitSounds = 1;
        public const int maxNPCKilledSounds = 1;
        public const int maxNPCs = 0x3e8;
        public const int maxNPCTypes = 2;
        public const int maxPlayers = 0x10;
        public const int maxTileSets = 12;
        public const int maxTilesX = 0x1389;
        public const int maxTilesY = 0x9c5;
        public const int maxWallTypes = 2;
        public static int moonPhase = 0;
        public static Texture2D moonTexture;
        public static Item mouseItem = new Item();
        public static bool mouseLeftRelease = false;
        public static MouseState mouseState = Mouse.GetState();
        public static byte mouseTextColor = 0;
        public static int mouseTextColorChange = 1;
        public static int myPlayer = 0;
        public const double nightLength = 30000.0;
        public static NPC[] npc = new NPC[0x3e9];
        public static int[] npcFrameCount = new int[] { 1, 2 };
        public static Texture2D[] npcTexture = new Texture2D[2];
        public static int numAvailableRecipes;
        public static MouseState oldMouseState = Mouse.GetState();
        public static Player[] player = new Player[0x10];
        public static Texture2D playerBodyTexture;
        public static Texture2D playerHeadTexture;
        public static bool playerInventory = false;
        public static Texture2D playerLegTexture;
        public static Random rand = new Random();
        public static Recipe[] recipe = new Recipe[Recipe.maxRecipes];
        public const float rightWorld = 80000f;
        public static int screenHeight = 600;
        public static Vector2 screenPosition;
        public static int screenWidth = 800;
        public static SoundEffect[] soundDig = new SoundEffect[3];
        public static SoundEffect soundDoorClosed;
        public static SoundEffect soundDoorOpen;
        public static SoundEffect soundGrab;
        public static SoundEffect soundGrass;
        public static SoundEffectInstance[] soundInstanceDig = new SoundEffectInstance[3];
        public static SoundEffectInstance soundInstanceDoorClosed;
        public static SoundEffectInstance soundInstanceDoorOpen;
        public static SoundEffectInstance soundInstanceGrab;
        public static SoundEffectInstance soundInstanceGrass;
        public static SoundEffectInstance[] soundInstanceItem = new SoundEffectInstance[3];
        public static SoundEffectInstance soundInstanceMenuTick;
        public static SoundEffectInstance[] soundInstanceNPCHit = new SoundEffectInstance[2];
        public static SoundEffectInstance[] soundInstanceNPCKilled = new SoundEffectInstance[2];
        public static SoundEffectInstance[] soundInstancePlayerHit = new SoundEffectInstance[3];
        public static SoundEffectInstance soundInstancePlayerKilled;
        public static SoundEffect[] soundItem = new SoundEffect[3];
        public static SoundEffect soundMenuClose;
        public static SoundEffect soundMenuOpen;
        public static SoundEffect soundMenuTick;
        public static SoundEffect[] soundNPCHit = new SoundEffect[2];
        public static SoundEffect[] soundNPCKilled = new SoundEffect[2];
        public static SoundEffect[] soundPlayerHit = new SoundEffect[3];
        public static SoundEffect soundPlayerKilled;
        public static int spawnTileX;
        public static int spawnTileY;
        private SpriteBatch spriteBatch;
        public static Texture2D sunTexture;
        public static Tile[,] tile = new Tile[0x1389, 0x9c5];
        public static Color tileColor;
        public static bool[] tileNoFail = new bool[12];
        public static bool[] tileSolid = new bool[12];
        public static Texture2D[] tileTexture = new Texture2D[12];
        public static double time = 10000.0;
        public bool toggleFullscreen;
        public const float topWorld = 0f;
        public static Texture2D treeBranchTexture;
        public static Texture2D treeTopTexture;
        public static Texture2D[] wallTexture = new Texture2D[2];
        public static double worldSurface;

        public Main()
        {
            this.graphics = new GraphicsDeviceManager(this);
            base.Content.RootDirectory = "Content";
        }

        public static double CalculateDamage(int Damage, int Defense)
        {
            return (((double)Damage) / (Defense * 0.1));
        }

        protected override void Draw(GameTime gameTime)
        {
            player[myPlayer].mouseInterface = false;
            if (base.IsActive)
            {
                int num20;
                int num27;
                float num45;
                string name;
                Vector2 vector;
                Color color15;
                object obj2;
                bool flag = false;
                if (!debugMode)
                {
                    int x = mouseState.X;
                    int screenHeight = mouseState.Y;
                    if (x < 0)
                    {
                        x = 0;
                    }
                    if (x > screenWidth)
                    {
                        x = screenWidth;
                    }
                    if (screenHeight < 0)
                    {
                        screenHeight = 0;
                    }
                    if (screenHeight > Main.screenHeight)
                    {
                        screenHeight = Main.screenHeight;
                    }
                    screenPosition.X =
                        (player[myPlayer].position.X + (player[myPlayer].width * 0.5f))
                        - (screenWidth * 0.5f);
                    screenPosition.Y =
                        (player[myPlayer].position.Y + (player[myPlayer].height * 0.5f))
                        - (Main.screenHeight * 0.5f);
                }
                screenPosition.X = (int)screenPosition.X;
                screenPosition.Y = (int)screenPosition.Y;
                if (screenPosition.X < 0f)
                {
                    screenPosition.X = 0f;
                }
                else if ((screenPosition.X + screenWidth) > 80000f)
                {
                    screenPosition.X = 80000f - screenWidth;
                }
                if (screenPosition.Y < 0f)
                {
                    screenPosition.Y = 0f;
                }
                else if ((screenPosition.Y + Main.screenHeight) > 40000f)
                {
                    screenPosition.Y = 40000f - Main.screenHeight;
                }
                base.GraphicsDevice.Clear(Color.Black);
                base.Draw(gameTime);
                this.spriteBatch.Begin();
                double num3 = 0.5;
                int num4 =
                    (
                        (int)
                            -Math.IEEERemainder(
                                screenPosition.X * num3,
                                (double)backgroundWidth[background]
                            )
                    ) - (backgroundWidth[background] / 2);
                int num5 = (screenWidth / backgroundWidth[background]) + 2;
                int num6 = 0;
                int num7 = 0;
                int y = (int)(
                    (((double)-screenPosition.Y) / ((worldSurface * 16.0) - Main.screenHeight))
                    * (backgroundHeight[background] - Main.screenHeight)
                );
                Color white = Color.White;
                int num9 =
                    ((int)((time / 40000.0) * (screenWidth + (sunTexture.Width * 2))))
                    - sunTexture.Width;
                int num10 = 0;
                Color color = Color.White;
                float scale = 1f;
                float rotation = (((float)(time / 40000.0)) * 2f) - 7.3f;
                int num14 =
                    ((int)((time / 30000.0) * (screenWidth + (moonTexture.Width * 2))))
                    - moonTexture.Width;
                int num15 = 0;
                Color color3 = Color.White;
                float num16 = 1f;
                float num17 = (((float)(time / 30000.0)) * 2f) - 7.3f;
                float num19 = 0f;
                if (dayTime)
                {
                    double num13;
                    if (time < 20000.0)
                    {
                        num13 = Math.Pow(1.0 - ((time / 40000.0) * 2.0), 2.0);
                        num10 = (int)((y + (num13 * 250.0)) + 180.0);
                    }
                    else
                    {
                        num13 = Math.Pow(((time / 40000.0) - 0.5) * 2.0, 2.0);
                        num10 = (int)((y + (num13 * 250.0)) + 180.0);
                    }
                    scale = (float)(1.2 - (num13 * 0.4));
                }
                else
                {
                    double num18;
                    if (time < 15000.0)
                    {
                        num18 = Math.Pow(1.0 - ((time / 30000.0) * 2.0), 2.0);
                        num15 = (int)((y + (num18 * 250.0)) + 180.0);
                    }
                    else
                    {
                        num18 = Math.Pow(((time / 30000.0) - 0.5) * 2.0, 2.0);
                        num15 = (int)((y + (num18 * 250.0)) + 180.0);
                    }
                    num16 = (float)(1.2 - (num18 * 0.4));
                }
                if (dayTime)
                {
                    if (time < 10000.0)
                    {
                        num19 = (float)(time / 10000.0);
                        color.R = (byte)((num19 * 200f) + 55f);
                        color.G = (byte)((num19 * 180f) + 75f);
                        color.B = (byte)((num19 * 250f) + 5f);
                        white.R = (byte)((num19 * 200f) + 55f);
                        white.G = (byte)((num19 * 200f) + 55f);
                        white.B = (byte)((num19 * 200f) + 55f);
                    }
                    if (time > 34000.0)
                    {
                        num19 = (float)(1.0 - (((time / 40000.0) - 0.85) * 6.666666666666667));
                        color.R = (byte)((num19 * 120f) + 55f);
                        color.G = (byte)((num19 * 100f) + 25f);
                        color.B = (byte)((num19 * 120f) + 55f);
                        white.R = (byte)((num19 * 200f) + 55f);
                        white.G = (byte)((num19 * 85f) + 55f);
                        white.B = (byte)((num19 * 135f) + 55f);
                    }
                    else if (time > 28000.0)
                    {
                        num19 = (float)(1.0 - (((time / 40000.0) - 0.7) * 6.666666666666667));
                        color.R = (byte)((num19 * 80f) + 175f);
                        color.G = (byte)((num19 * 130f) + 125f);
                        color.B = (byte)((num19 * 100f) + 155f);
                        white.R = (byte)((num19 * 0f) + 255f);
                        white.G = (byte)((num19 * 115f) + 140f);
                        white.B = (byte)((num19 * 75f) + 180f);
                    }
                }
                if (!dayTime)
                {
                    if (time < 15000.0)
                    {
                        num19 = (float)(1.0 - (time / 15000.0));
                        color3.R = (byte)((num19 * 10f) + 205f);
                        color3.G = (byte)((num19 * 70f) + 155f);
                        color3.B = (byte)((num19 * 100f) + 155f);
                        white.R = (byte)((num19 * 40f) + 15f);
                        white.G = (byte)((num19 * 40f) + 15f);
                        white.B = (byte)((num19 * 40f) + 15f);
                    }
                    else if (time >= 15000.0)
                    {
                        num19 = (float)(((time / 30000.0) - 0.5) * 2.0);
                        color3.R = (byte)((num19 * 50f) + 205f);
                        color3.G = (byte)((num19 * 100f) + 155f);
                        color3.B = (byte)((num19 * 100f) + 155f);
                        white.R = (byte)((num19 * 40f) + 15f);
                        white.G = (byte)((num19 * 40f) + 15f);
                        white.B = (byte)((num19 * 40f) + 15f);
                    }
                }
                tileColor.A = 0xff;
                tileColor.R = (byte)(((white.R + white.B) + white.G) / 3);
                tileColor.G = (byte)(((white.R + white.B) + white.G) / 3);
                tileColor.B = (byte)(((white.R + white.B) + white.G) / 3);
                for (num20 = 0; num20 < num5; num20++)
                {
                    this.spriteBatch.Draw(
                        backgroundTexture[background],
                        new Rectangle(
                            num4 + (backgroundWidth[background] * num20),
                            y,
                            backgroundWidth[background],
                            backgroundHeight[background]
                        ),
                        white
                    );
                }
                if (dayTime)
                {
                    this.spriteBatch.Draw(
                        sunTexture,
                        new Vector2((float)num9, (float)num10),
                        new Rectangle(0, 0, sunTexture.Width, sunTexture.Height),
                        color,
                        rotation,
                        new Vector2((float)(sunTexture.Width / 2), (float)(sunTexture.Height / 2)),
                        scale,
                        SpriteEffects.None,
                        0f
                    );
                }
                if (!dayTime)
                {
                    this.spriteBatch.Draw(
                        moonTexture,
                        new Vector2((float)num14, (float)num15),
                        new Rectangle(
                            0,
                            moonTexture.Width * moonPhase,
                            moonTexture.Width,
                            moonTexture.Width
                        ),
                        color3,
                        num17,
                        new Vector2((float)(moonTexture.Width / 2), (float)(moonTexture.Width / 2)),
                        num16,
                        SpriteEffects.None,
                        0f
                    );
                }
                int firstX = (int)((screenPosition.X / 16f) - 1f);
                int lastX = ((int)((screenPosition.X + screenWidth) / 16f)) + 2;
                int firstY = (int)((screenPosition.Y / 16f) - 1f);
                int lastY = ((int)((screenPosition.Y + Main.screenHeight) / 16f)) + 2;
                if (firstX < 0)
                {
                    firstX = 0;
                }
                if (lastX > 0x1389)
                {
                    lastX = 0x1389;
                }
                if (firstY < 0)
                {
                    firstY = 0;
                }
                if (lastY > 0x9c5)
                {
                    lastY = 0x9c5;
                }
                Lighting.LightTiles(firstX, lastX, firstY, lastY);
                Color color4 = Color.White;
                int height = 0x10;
                int width = 0x10;
                num3 = 1.0;
                num4 =
                    ((int)-Math.IEEERemainder(screenPosition.X * num3, (double)backgroundWidth[1]))
                    - (backgroundWidth[1] / 2);
                num5 = (screenWidth / backgroundWidth[1]) + 2;
                y = (int)(
                    (((((int)worldSurface) * 0x10) - backgroundHeight[1]) - screenPosition.Y) + 16f
                );
                for (num20 = 0; num20 < num5; num20++)
                {
                    num27 = 0;
                    while (num27 < 6)
                    {
                        int num28 = (int)(
                            (
                                (
                                    (
                                        ((num4 + (backgroundWidth[1] * num20)) + screenPosition.X)
                                        + (num27 * 0x10)
                                    ) / 16f
                                ) - firstX
                            ) + 21f
                        );
                        int num29 = (int)((((y + screenPosition.Y) / 16f) - firstY) + 21f);
                        if (num28 < 0)
                        {
                            num28 = 0;
                        }
                        if (num28 >= (((screenWidth / 0x10) + 0x2a) + 10))
                        {
                            num28 = (((screenWidth / 0x10) + 0x2a) + 10) - 1;
                        }
                        if (num29 < 0)
                        {
                            num29 = 0;
                        }
                        if (num29 >= (((Main.screenHeight / 0x10) + 0x2a) + 10))
                        {
                            num29 = (((Main.screenHeight / 0x10) + 0x2a) + 10) - 1;
                        }
                        Color color5 = Lighting.color[num28, num29];
                        this.spriteBatch.Draw(
                            backgroundTexture[1],
                            new Vector2(
                                (float)((num4 + (backgroundWidth[1] * num20)) + (0x10 * num27)),
                                (float)y
                            ),
                            new Rectangle(0x10 * num27, 0, 0x10, 0x10),
                            color5
                        );
                        num27++;
                    }
                }
                y = (int)(((((int)worldSurface) * 0x10) - screenPosition.Y) + 16f);
                if ((worldSurface * 16.0) <= (screenPosition.Y + Main.screenHeight))
                {
                    num3 = 1.0;
                    num4 =
                        (
                            (int)
                                -Math.IEEERemainder(
                                    100.0 + (screenPosition.X * num3),
                                    (double)backgroundWidth[2]
                                )
                        ) - (backgroundWidth[2] / 2);
                    num5 = (screenWidth / backgroundWidth[2]) + 2;
                    if ((worldSurface * 16.0) < screenPosition.Y)
                    {
                        num6 =
                            ((int)Math.IEEERemainder((double)y, (double)backgroundHeight[2]))
                            - backgroundHeight[2];
                        num7 = (Main.screenHeight / backgroundHeight[2]) + 2;
                    }
                    else
                    {
                        num6 = y;
                        num7 = ((Main.screenHeight - y) / backgroundHeight[2]) + 1;
                    }
                    num20 = 0;
                    while (num20 < num5)
                    {
                        for (num27 = 0; num27 < num7; num27++)
                        {
                            this.spriteBatch.Draw(
                                backgroundTexture[2],
                                new Rectangle(
                                    num4 + (backgroundWidth[2] * num20),
                                    num6 + (backgroundHeight[2] * num27),
                                    backgroundWidth[2],
                                    backgroundHeight[2]
                                ),
                                Color.White
                            );
                        }
                        num20++;
                    }
                }
                num27 = firstY;
                while (num27 < (lastY + 4))
                {
                    num20 = firstX - 2;
                    while (num20 < (lastX + 2))
                    {
                        if (
                            (
                                Lighting.color[(num20 - firstX) + 0x15, (num27 - firstY) + 0x15].R
                                < (tileColor.R - 10)
                            ) || (num27 > worldSurface)
                        )
                        {
                            int num30 =
                                0xff
                                - Lighting
                                    .color[(num20 - firstX) + 0x15, (num27 - firstY) + 0x15]
                                    .R;
                            if (num30 < 0)
                            {
                                num30 = 0;
                            }
                            if (num30 > 0xff)
                            {
                                num30 = 0xff;
                            }
                            color4.A = (byte)num30;
                            vector = new Vector2();
                            this.spriteBatch.Draw(
                                blackTileTexture,
                                new Vector2(
                                    (float)((num20 * 0x10) - ((int)screenPosition.X)),
                                    (float)((num27 * 0x10) - ((int)screenPosition.Y))
                                ),
                                new Rectangle(
                                    tile[num20, num27].frameX,
                                    tile[num20, num27].frameY,
                                    0x10,
                                    0x10
                                ),
                                color4,
                                0f,
                                vector,
                                (float)1f,
                                SpriteEffects.None,
                                0f
                            );
                        }
                        if (tile[num20, num27].wall > 0)
                        {
                            vector = new Vector2();
                            this.spriteBatch.Draw(
                                wallTexture[tile[num20, num27].wall],
                                new Vector2(
                                    (float)(((num20 * 0x10) - ((int)screenPosition.X)) - 8),
                                    (float)(((num27 * 0x10) - ((int)screenPosition.Y)) - 8)
                                ),
                                new Rectangle(
                                    tile[num20, num27].wallFrameX * 2,
                                    tile[num20, num27].wallFrameY * 2,
                                    0x20,
                                    0x20
                                ),
                                Lighting.color[(num20 - firstX) + 0x15, (num27 - firstY) + 0x15],
                                0f,
                                vector,
                                (float)1f,
                                SpriteEffects.None,
                                0f
                            );
                        }
                        num20++;
                    }
                    num27++;
                }
                for (num27 = firstY; num27 < (lastY + 4); num27++)
                {
                    num20 = firstX - 2;
                    while (num20 < (lastX + 2))
                    {
                        if (tile[num20, num27].active)
                        {
                            if (
                                ((tile[num20, num27].type == 3) || (tile[num20, num27].type == 4))
                                || (tile[num20, num27].type == 5)
                            )
                            {
                                height = 20;
                            }
                            else
                            {
                                height = 0x10;
                            }
                            if ((tile[num20, num27].type == 4) || (tile[num20, num27].type == 5))
                            {
                                width = 20;
                            }
                            else
                            {
                                width = 0x10;
                            }
                            if ((tile[num20, num27].type == 4) && (rand.Next(40) == 0))
                            {
                                if (tile[num20, num27].frameX == 0x16)
                                {
                                    color15 = new Color();
                                    Dust.NewDust(
                                        new Vector2(
                                            (float)((num20 * 0x10) + 6),
                                            (float)(num27 * 0x10)
                                        ),
                                        4,
                                        4,
                                        6,
                                        0f,
                                        0f,
                                        100,
                                        color15
                                    );
                                }
                                if (tile[num20, num27].frameX == 0x2c)
                                {
                                    color15 = new Color();
                                    Dust.NewDust(
                                        new Vector2(
                                            (float)((num20 * 0x10) + 2),
                                            (float)(num27 * 0x10)
                                        ),
                                        4,
                                        4,
                                        6,
                                        0f,
                                        0f,
                                        100,
                                        color15
                                    );
                                }
                                else
                                {
                                    color15 = new Color();
                                    Dust.NewDust(
                                        new Vector2(
                                            (float)((num20 * 0x10) + 4),
                                            (float)(num27 * 0x10)
                                        ),
                                        4,
                                        4,
                                        6,
                                        0f,
                                        0f,
                                        100,
                                        color15
                                    );
                                }
                            }
                            if (
                                (
                                    (tile[num20, num27].type == 5)
                                    && (tile[num20, num27].frameY >= 0xc6)
                                ) && (tile[num20, num27].frameX >= 0x16)
                            )
                            {
                                int num31 = 0;
                                if (tile[num20, num27].frameX == 0x16)
                                {
                                    if (tile[num20, num27].frameY == 220)
                                    {
                                        num31 = 1;
                                    }
                                    else if (tile[num20, num27].frameY == 0xf2)
                                    {
                                        num31 = 2;
                                    }
                                    vector = new Vector2();
                                    this.spriteBatch.Draw(
                                        treeTopTexture,
                                        new Vector2(
                                            (float)(
                                                ((num20 * 0x10) - ((int)screenPosition.X)) - 0x20
                                            ),
                                            (float)(
                                                ((num27 * 0x10) - ((int)screenPosition.Y)) - 0x40
                                            )
                                        ),
                                        new Rectangle(num31 * 0x52, 0, 80, 80),
                                        Lighting.color[
                                            (num20 - firstX) + 0x15,
                                            (num27 - firstY) + 0x15
                                        ],
                                        0f,
                                        vector,
                                        (float)1f,
                                        SpriteEffects.None,
                                        0f
                                    );
                                }
                                else if (tile[num20, num27].frameX == 0x2c)
                                {
                                    if (tile[num20, num27].frameY == 220)
                                    {
                                        num31 = 1;
                                    }
                                    else if (tile[num20, num27].frameY == 0xf2)
                                    {
                                        num31 = 2;
                                    }
                                    vector = new Vector2();
                                    this.spriteBatch.Draw(
                                        treeBranchTexture,
                                        new Vector2(
                                            (float)(
                                                ((num20 * 0x10) - ((int)screenPosition.X)) - 0x18
                                            ),
                                            (float)(((num27 * 0x10) - ((int)screenPosition.Y)) - 12)
                                        ),
                                        new Rectangle(0, num31 * 0x2a, 40, 40),
                                        Lighting.color[
                                            (num20 - firstX) + 0x15,
                                            (num27 - firstY) + 0x15
                                        ],
                                        0f,
                                        vector,
                                        (float)1f,
                                        SpriteEffects.None,
                                        0f
                                    );
                                }
                                else if (tile[num20, num27].frameX == 0x42)
                                {
                                    if (tile[num20, num27].frameY == 220)
                                    {
                                        num31 = 1;
                                    }
                                    else if (tile[num20, num27].frameY == 0xf2)
                                    {
                                        num31 = 2;
                                    }
                                    vector = new Vector2();
                                    this.spriteBatch.Draw(
                                        treeBranchTexture,
                                        new Vector2(
                                            (float)((num20 * 0x10) - ((int)screenPosition.X)),
                                            (float)(((num27 * 0x10) - ((int)screenPosition.Y)) - 12)
                                        ),
                                        new Rectangle(0x2a, num31 * 0x2a, 40, 40),
                                        Lighting.color[
                                            (num20 - firstX) + 0x15,
                                            (num27 - firstY) + 0x15
                                        ],
                                        0f,
                                        vector,
                                        (float)1f,
                                        SpriteEffects.None,
                                        0f
                                    );
                                }
                            }
                            vector = new Vector2();
                            this.spriteBatch.Draw(
                                tileTexture[tile[num20, num27].type],
                                new Vector2(
                                    ((num20 * 0x10) - ((int)screenPosition.X))
                                        - ((width - 16f) / 2f),
                                    (float)((num27 * 0x10) - ((int)screenPosition.Y))
                                ),
                                new Rectangle(
                                    tile[num20, num27].frameX,
                                    tile[num20, num27].frameY,
                                    width,
                                    height
                                ),
                                Lighting.color[(num20 - firstX) + 0x15, (num27 - firstY) + 0x15],
                                0f,
                                vector,
                                (float)1f,
                                SpriteEffects.None,
                                0f
                            );
                        }
                        num20++;
                    }
                }
                SpriteEffects none = SpriteEffects.None;
                for (num20 = 0; num20 < 0x10; num20++)
                {
                    if (player[num20].active)
                    {
                        if (player[num20].direction == -1)
                        {
                            none = SpriteEffects.FlipHorizontally;
                        }
                        else
                        {
                            none = SpriteEffects.None;
                        }
                        Color newColor = Lighting.color[
                            Lighting.LightingX(
                                (
                                    (
                                        (
                                            (int)(
                                                player[num20].position.X
                                                + (player[num20].width * 0.5)
                                            )
                                        ) / 0x10
                                    ) - firstX
                                ) + 0x15
                            ),
                            Lighting.LightingY(
                                (
                                    (
                                        (
                                            (int)(
                                                player[num20].position.Y
                                                + (player[num20].height * 0.25)
                                            )
                                        ) / 0x10
                                    ) - firstY
                                ) + 0x15
                            )
                        ];
                        this.spriteBatch.Draw(
                            playerHeadTexture,
                            (
                                new Vector2(
                                    (float)(
                                        (
                                            ((int)(player[num20].position.X - screenPosition.X))
                                            - (player[num20].headFrame.Width / 2)
                                        ) + (player[num20].width / 2)
                                    ),
                                    (float)(
                                        (int)(
                                            (
                                                (
                                                    (player[num20].position.Y - screenPosition.Y)
                                                    + player[num20].height
                                                ) - player[num20].headFrame.Height
                                            ) + 2f
                                        )
                                    )
                                ) + player[num20].headPosition
                            ) + new Vector2(16f, 14f),
                            new Rectangle?(player[num20].headFrame),
                            player[num20].GetImmuneAlpha(newColor),
                            player[num20].headRotation,
                            new Vector2(16f, 14f),
                            (float)1f,
                            none,
                            0f
                        );
                        newColor = Lighting.color[
                            Lighting.LightingX(
                                (
                                    (
                                        (
                                            (int)(
                                                player[num20].position.X
                                                + (player[num20].width * 0.5)
                                            )
                                        ) / 0x10
                                    ) - firstX
                                ) + 0x15
                            ),
                            Lighting.LightingY(
                                (
                                    (
                                        (
                                            (int)(
                                                player[num20].position.Y
                                                + (player[num20].height * 0.5)
                                            )
                                        ) / 0x10
                                    ) - firstY
                                ) + 0x15
                            )
                        ];
                        if (
                            (
                                (player[num20].itemAnimation > 0)
                                || (
                                    player[num20].inventory[player[num20].selectedItem].holdStyle
                                    > 0
                                )
                            ) && (player[num20].inventory[player[num20].selectedItem].type > 0)
                        )
                        {
                            this.spriteBatch.Draw(
                                itemTexture[
                                    player[num20].inventory[player[num20].selectedItem].type
                                ],
                                new Vector2(
                                    (float)((int)(player[num20].itemLocation.X - screenPosition.X)),
                                    (float)((int)(player[num20].itemLocation.Y - screenPosition.Y))
                                ),
                                new Rectangle(
                                    0,
                                    0,
                                    itemTexture[
                                        player[num20].inventory[player[num20].selectedItem].type
                                    ].Width,
                                    itemTexture[
                                        player[num20].inventory[player[num20].selectedItem].type
                                    ].Height
                                ),
                                player[num20]
                                    .inventory[player[num20].selectedItem]
                                    .GetAlpha(newColor),
                                player[num20].itemRotation,
                                new Vector2(
                                    (
                                        itemTexture[
                                            player[num20].inventory[player[num20].selectedItem].type
                                        ].Width * 0.5f
                                    )
                                        - (
                                            (
                                                itemTexture[
                                                    player[num20]
                                                        .inventory[player[num20].selectedItem]
                                                        .type
                                                ].Width * 0.5f
                                            ) * player[num20].direction
                                        ),
                                    (float)
                                        itemTexture[
                                            player[num20].inventory[player[num20].selectedItem].type
                                        ].Height
                                ),
                                player[num20].inventory[player[num20].selectedItem].scale,
                                none,
                                0f
                            );
                            color15 = new Color();
                            if (
                                player[num20].inventory[player[num20].selectedItem].color != color15
                            )
                            {
                                this.spriteBatch.Draw(
                                    itemTexture[
                                        player[num20].inventory[player[num20].selectedItem].type
                                    ],
                                    new Vector2(
                                        (float)(
                                            (int)(player[num20].itemLocation.X - screenPosition.X)
                                        ),
                                        (float)(
                                            (int)(player[num20].itemLocation.Y - screenPosition.Y)
                                        )
                                    ),
                                    new Rectangle(
                                        0,
                                        0,
                                        itemTexture[
                                            player[num20].inventory[player[num20].selectedItem].type
                                        ].Width,
                                        itemTexture[
                                            player[num20].inventory[player[num20].selectedItem].type
                                        ].Height
                                    ),
                                    player[num20]
                                        .inventory[player[num20].selectedItem]
                                        .GetColor(newColor),
                                    player[num20].itemRotation,
                                    new Vector2(
                                        (
                                            itemTexture[
                                                player[num20]
                                                    .inventory[player[num20].selectedItem]
                                                    .type
                                            ].Width * 0.5f
                                        )
                                            - (
                                                (
                                                    itemTexture[
                                                        player[num20]
                                                            .inventory[player[num20].selectedItem]
                                                            .type
                                                    ].Width * 0.5f
                                                ) * player[num20].direction
                                            ),
                                        (float)
                                            itemTexture[
                                                player[num20]
                                                    .inventory[player[num20].selectedItem]
                                                    .type
                                            ].Height
                                    ),
                                    player[num20].inventory[player[num20].selectedItem].scale,
                                    none,
                                    0f
                                );
                            }
                        }
                        this.spriteBatch.Draw(
                            playerBodyTexture,
                            (
                                new Vector2(
                                    (float)(
                                        (
                                            ((int)(player[num20].position.X - screenPosition.X))
                                            - (player[num20].bodyFrame.Width / 2)
                                        ) + (player[num20].width / 2)
                                    ),
                                    (float)(
                                        (int)(
                                            (
                                                (
                                                    (player[num20].position.Y - screenPosition.Y)
                                                    + player[num20].height
                                                ) - player[num20].bodyFrame.Height
                                            ) + 2f
                                        )
                                    )
                                ) + player[num20].bodyPosition
                            ) + new Vector2(16f, 28f),
                            new Rectangle?(player[num20].bodyFrame),
                            player[num20].GetImmuneAlpha(newColor),
                            player[num20].bodyRotation,
                            new Vector2(16f, 28f),
                            (float)1f,
                            none,
                            0f
                        );
                        newColor = Lighting.color[
                            Lighting.LightingX(
                                (
                                    (
                                        (
                                            (int)(
                                                player[num20].position.X
                                                + (player[num20].width * 0.5)
                                            )
                                        ) / 0x10
                                    ) - firstX
                                ) + 0x15
                            ),
                            Lighting.LightingY(
                                (
                                    (
                                        (
                                            (int)(
                                                player[num20].position.Y
                                                + (player[num20].height * 0.75)
                                            )
                                        ) / 0x10
                                    ) - firstY
                                ) + 0x15
                            )
                        ];
                        this.spriteBatch.Draw(
                            playerLegTexture,
                            (
                                new Vector2(
                                    (float)(
                                        (
                                            ((int)(player[num20].position.X - screenPosition.X))
                                            - (player[num20].legFrame.Width / 2)
                                        ) + (player[num20].width / 2)
                                    ),
                                    (float)(
                                        (int)(
                                            (
                                                (
                                                    (player[num20].position.Y - screenPosition.Y)
                                                    + player[num20].height
                                                ) - player[num20].legFrame.Height
                                            ) + 2f
                                        )
                                    )
                                ) + player[num20].legPosition
                            ) + new Vector2(16f, 40f),
                            new Rectangle?(player[num20].legFrame),
                            player[num20].GetImmuneAlpha(newColor),
                            player[num20].legRotation,
                            new Vector2(16f, 40f),
                            (float)1f,
                            none,
                            0f
                        );
                    }
                }
                Rectangle rectangle = new Rectangle(
                    (int)screenPosition.X,
                    (int)screenPosition.Y,
                    screenWidth,
                    Main.screenHeight
                );
                for (num20 = 0; num20 < 0x3e8; num20++)
                {
                    if (
                        rectangle.Intersects(
                            new Rectangle(
                                (int)npc[num20].position.X,
                                (int)npc[num20].position.Y,
                                npc[num20].width,
                                npc[num20].height
                            )
                        )
                    )
                    {
                        Color color7 = Lighting.color[
                            (
                                (((int)(npc[num20].position.X + (npc[num20].width * 0.5))) / 0x10)
                                - firstX
                            ) + 0x15,
                            (
                                (((int)(npc[num20].position.Y + (npc[num20].height * 0.5))) / 0x10)
                                - firstY
                            ) + 0x15
                        ];
                        if (npc[num20].active && (npc[num20].type > 0))
                        {
                            vector = new Vector2();
                            this.spriteBatch.Draw(
                                npcTexture[npc[num20].type],
                                new Vector2(
                                    (
                                        (npc[num20].position.X - screenPosition.X)
                                        + (npc[num20].width / 2)
                                    )
                                        - (
                                            (npcTexture[npc[num20].type].Width * npc[num20].scale)
                                            / 2f
                                        ),
                                    (
                                        (
                                            (npc[num20].position.Y - screenPosition.Y)
                                            + npc[num20].height
                                        )
                                        - (
                                            (npcTexture[npc[num20].type].Height * npc[num20].scale)
                                            / ((float)npcFrameCount[npc[num20].type])
                                        )
                                    ) + 4f
                                ),
                                new Rectangle?(npc[num20].frame),
                                npc[num20].GetAlpha(color7),
                                0f,
                                vector,
                                npc[num20].scale,
                                SpriteEffects.None,
                                0f
                            );
                            color15 = new Color();
                            if (npc[num20].color != color15)
                            {
                                vector = new Vector2();
                                this.spriteBatch.Draw(
                                    npcTexture[npc[num20].type],
                                    new Vector2(
                                        (
                                            (npc[num20].position.X - screenPosition.X)
                                            + (npc[num20].width / 2)
                                        )
                                            - (
                                                (
                                                    npcTexture[npc[num20].type].Width
                                                    * npc[num20].scale
                                                ) / 2f
                                            ),
                                        (
                                            (
                                                (npc[num20].position.Y - screenPosition.Y)
                                                + npc[num20].height
                                            )
                                            - (
                                                (
                                                    npcTexture[npc[num20].type].Height
                                                    * npc[num20].scale
                                                ) / ((float)npcFrameCount[npc[num20].type])
                                            )
                                        ) + 4f
                                    ),
                                    new Rectangle?(npc[num20].frame),
                                    npc[num20].GetColor(color7),
                                    0f,
                                    vector,
                                    npc[num20].scale,
                                    SpriteEffects.None,
                                    0f
                                );
                            }
                        }
                    }
                }
                for (num20 = 0; num20 < 0x3e8; num20++)
                {
                    if (item[num20].active && (item[num20].type > 0))
                    {
                        int lightX =
                            (
                                (((int)(item[num20].position.X + (item[num20].width * 0.5))) / 0x10)
                                - firstX
                            ) + 0x15;
                        int lightY =
                            (
                                (
                                    ((int)(item[num20].position.Y + (item[num20].height * 0.5)))
                                    / 0x10
                                ) - firstY
                            ) + 0x15;
                        Color color8 = Lighting.color[
                            Lighting.LightingX(lightX),
                            Lighting.LightingY(lightY)
                        ];
                        vector = new Vector2();
                        this.spriteBatch.Draw(
                            itemTexture[item[num20].type],
                            new Vector2(
                                (
                                    (item[num20].position.X - screenPosition.X)
                                    + (item[num20].width / 2)
                                ) - (itemTexture[item[num20].type].Width / 2),
                                (
                                    (item[num20].position.Y - screenPosition.Y)
                                    + (item[num20].height / 2)
                                ) - (itemTexture[item[num20].type].Height / 2)
                            ),
                            new Rectangle(
                                0,
                                0,
                                itemTexture[item[num20].type].Width,
                                itemTexture[item[num20].type].Height
                            ),
                            item[num20].GetAlpha(color8),
                            0f,
                            vector,
                            (float)1f,
                            SpriteEffects.None,
                            0f
                        );
                        color15 = new Color();
                        if (item[num20].color != color15)
                        {
                            vector = new Vector2();
                            this.spriteBatch.Draw(
                                itemTexture[item[num20].type],
                                new Vector2(
                                    (
                                        (item[num20].position.X - screenPosition.X)
                                        + (item[num20].width / 2)
                                    ) - (itemTexture[item[num20].type].Width / 2),
                                    (
                                        (item[num20].position.Y - screenPosition.Y)
                                        + (item[num20].height / 2)
                                    ) - (itemTexture[item[num20].type].Height / 2)
                                ),
                                new Rectangle(
                                    0,
                                    0,
                                    itemTexture[item[num20].type].Width,
                                    itemTexture[item[num20].type].Height
                                ),
                                item[num20].GetColor(color8),
                                0f,
                                vector,
                                (float)1f,
                                SpriteEffects.None,
                                0f
                            );
                        }
                    }
                }
                for (num20 = 0; num20 < 0x3e8; num20++)
                {
                    if (dust[num20].active)
                    {
                        Color color9 = Lighting.color[
                            Lighting.LightingX(
                                ((((int)(dust[num20].position.X + 4.0)) / 0x10) - firstX) + 0x15
                            ),
                            Lighting.LightingY(
                                ((((int)(dust[num20].position.Y + 4.0)) / 0x10) - firstY) + 0x15
                            )
                        ];
                        if (dust[num20].type == 6)
                        {
                            color9 = Color.White;
                        }
                        this.spriteBatch.Draw(
                            dustTexture,
                            dust[num20].position - screenPosition,
                            new Rectangle?(dust[num20].frame),
                            dust[num20].GetAlpha(color9),
                            dust[num20].rotation,
                            new Vector2(4f, 4f),
                            dust[num20].scale,
                            SpriteEffects.None,
                            0f
                        );
                        color15 = new Color();
                        if (dust[num20].color != color15)
                        {
                            this.spriteBatch.Draw(
                                dustTexture,
                                dust[num20].position - screenPosition,
                                new Rectangle?(dust[num20].frame),
                                dust[num20].GetColor(color9),
                                dust[num20].rotation,
                                new Vector2(4f, 4f),
                                dust[num20].scale,
                                SpriteEffects.None,
                                0f
                            );
                        }
                    }
                }
                int num34 = 20;
                num20 = 1;
                while (num20 < ((player[myPlayer].statLifeMax / num34) + 1))
                {
                    int r = 0xff;
                    float num36 = 1f;
                    if (player[myPlayer].statLife >= (num20 * num34))
                    {
                        r = 0xff;
                    }
                    else
                    {
                        float num37 =
                            ((float)(player[myPlayer].statLife - ((num20 - 1) * num34)))
                            / ((float)num34);
                        r = (int)(30f + (225f * num37));
                        if (r < 30)
                        {
                            r = 30;
                        }
                        num36 = (num37 / 4f) + 0.75f;
                        if (num36 < 0.75)
                        {
                            num36 = 0.75f;
                        }
                    }
                    int num38 = 0;
                    int num39 = 0;
                    if (num20 > 10)
                    {
                        num38 -= 260;
                        num39 += 0x1a;
                    }
                    vector = new Vector2();
                    this.spriteBatch.Draw(
                        heartTexture,
                        new Vector2(
                            (float)((500 + (0x1a * (num20 - 1))) + num38),
                            (32f + ((heartTexture.Height - (heartTexture.Height * num36)) / 2f))
                                + num39
                        ),
                        new Rectangle(0, 0, heartTexture.Width, heartTexture.Height),
                        new Color(r, r, r, r),
                        0f,
                        vector,
                        num36,
                        SpriteEffects.None,
                        0f
                    );
                    num20++;
                }
                string text = "";
                if (playerInventory)
                {
                    int num42;
                    int num43;
                    Color color10;
                    Item mouseItem;
                    double num46;
                    inventoryScale = 0.75f;
                    for (int i = 0; i < 10; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            num42 = (int)(20f + ((i * 0x38) * inventoryScale));
                            num43 = (int)(20f + ((j * 0x38) * inventoryScale));
                            num20 = i + (j * 10);
                            color10 = new Color(0xff, 0xff, 0xff, 0xff);
                            if (
                                (
                                    (
                                        (mouseState.X >= num42)
                                        && (
                                            mouseState.X
                                            <= (num42 + (hotbarTexture.Width * inventoryScale))
                                        )
                                    ) && (mouseState.Y >= num43)
                                )
                                && (
                                    mouseState.Y
                                    <= (num43 + (hotbarTexture.Height * inventoryScale))
                                )
                            )
                            {
                                player[myPlayer].mouseInterface = true;
                                if (
                                    (
                                        mouseLeftRelease
                                        && (mouseState.LeftButton == ButtonState.Pressed)
                                    )
                                    && (
                                        (player[myPlayer].selectedItem != num20)
                                        || (player[myPlayer].itemAnimation <= 0)
                                    )
                                )
                                {
                                    mouseItem = Main.mouseItem;
                                    Main.mouseItem = player[myPlayer].inventory[num20];
                                    player[myPlayer].inventory[num20] = mouseItem;
                                    if (
                                        (player[myPlayer].inventory[num20].type == 0)
                                        || (player[myPlayer].inventory[num20].stack < 1)
                                    )
                                    {
                                        player[myPlayer].inventory[num20] = new Item();
                                    }
                                    if (
                                        Main.mouseItem.IsTheSameAs(
                                            player[myPlayer].inventory[num20]
                                        )
                                        && (
                                            (
                                                player[myPlayer].inventory[num20].stack
                                                != player[myPlayer].inventory[num20].maxStack
                                            ) && (Main.mouseItem.stack != Main.mouseItem.maxStack)
                                        )
                                    )
                                    {
                                        if (
                                            (
                                                Main.mouseItem.stack
                                                + player[myPlayer].inventory[num20].stack
                                            ) <= Main.mouseItem.maxStack
                                        )
                                        {
                                            Item item1 = player[myPlayer].inventory[num20];
                                            item1.stack += Main.mouseItem.stack;
                                            Main.mouseItem.stack = 0;
                                        }
                                        else
                                        {
                                            int num44 =
                                                Main.mouseItem.maxStack
                                                - player[myPlayer].inventory[num20].stack;
                                            Item item2 = player[myPlayer].inventory[num20];
                                            item2.stack += num44;
                                            Main.mouseItem.stack -= num44;
                                        }
                                    }
                                    if ((Main.mouseItem.type == 0) || (Main.mouseItem.stack < 1))
                                    {
                                        Main.mouseItem = new Item();
                                    }
                                    if (
                                        (Main.mouseItem.type > 0)
                                        || (player[myPlayer].inventory[num20].type > 0)
                                    )
                                    {
                                        Recipe.FindRecipes();
                                        soundInstanceGrab.Stop();
                                        soundInstanceGrab = soundGrab.CreateInstance();
                                        soundInstanceGrab.Play();
                                    }
                                }
                                text = player[myPlayer].inventory[num20].name;
                                if (player[myPlayer].inventory[num20].stack > 1)
                                {
                                    obj2 = text;
                                    text = string.Concat(
                                        new object[]
                                        {
                                            obj2,
                                            " (",
                                            player[myPlayer].inventory[num20].stack,
                                            ")",
                                        }
                                    );
                                }
                            }
                            vector = new Vector2();
                            this.spriteBatch.Draw(
                                hotbarTexture,
                                new Vector2((float)num42, (float)num43),
                                new Rectangle(0, 0, hotbarTexture.Width, hotbarTexture.Height),
                                color10,
                                0f,
                                vector,
                                inventoryScale,
                                SpriteEffects.None,
                                0f
                            );
                            vector = new Vector2();
                            this.spriteBatch.Draw(
                                inventoryBackTexture,
                                new Vector2((float)num42, (float)num43),
                                new Rectangle(
                                    0,
                                    0,
                                    inventoryBackTexture.Width,
                                    inventoryBackTexture.Height
                                ),
                                new Color(200, 200, 200, 200),
                                0f,
                                vector,
                                inventoryScale,
                                SpriteEffects.None,
                                0f
                            );
                            color10 = Color.White;
                            if (
                                (player[myPlayer].inventory[num20].type > 0)
                                && (player[myPlayer].inventory[num20].stack > 0)
                            )
                            {
                                num45 = 1f;
                                if (
                                    (
                                        itemTexture[player[myPlayer].inventory[num20].type].Width
                                        > 0x20
                                    )
                                    || (
                                        itemTexture[player[myPlayer].inventory[num20].type].Height
                                        > 0x20
                                    )
                                )
                                {
                                    if (
                                        itemTexture[player[myPlayer].inventory[num20].type].Width
                                        > itemTexture[player[myPlayer].inventory[num20].type].Height
                                    )
                                    {
                                        num45 =
                                            32f
                                            / (
                                                (float)
                                                    itemTexture[
                                                        player[myPlayer].inventory[num20].type
                                                    ].Width
                                            );
                                    }
                                    else
                                    {
                                        num45 =
                                            32f
                                            / (
                                                (float)
                                                    itemTexture[
                                                        player[myPlayer].inventory[num20].type
                                                    ].Height
                                            );
                                    }
                                }
                                num45 *= inventoryScale;
                                vector = new Vector2();
                                this.spriteBatch.Draw(
                                    itemTexture[player[myPlayer].inventory[num20].type],
                                    new Vector2(
                                        (num42 + (26f * inventoryScale))
                                            - (
                                                (
                                                    itemTexture[
                                                        player[myPlayer].inventory[num20].type
                                                    ].Width * 0.5f
                                                ) * num45
                                            ),
                                        (num43 + (26f * inventoryScale))
                                            - (
                                                (
                                                    itemTexture[
                                                        player[myPlayer].inventory[num20].type
                                                    ].Height * 0.5f
                                                ) * num45
                                            )
                                    ),
                                    new Rectangle(
                                        0,
                                        0,
                                        itemTexture[player[myPlayer].inventory[num20].type].Width,
                                        itemTexture[player[myPlayer].inventory[num20].type].Height
                                    ),
                                    player[myPlayer].inventory[num20].GetAlpha(color10),
                                    0f,
                                    vector,
                                    num45,
                                    SpriteEffects.None,
                                    0f
                                );
                                color15 = new Color();
                                if (player[myPlayer].inventory[num20].color != color15)
                                {
                                    vector = new Vector2();
                                    this.spriteBatch.Draw(
                                        itemTexture[player[myPlayer].inventory[num20].type],
                                        new Vector2(
                                            (num42 + (26f * inventoryScale))
                                                - (
                                                    (
                                                        itemTexture[
                                                            player[myPlayer].inventory[num20].type
                                                        ].Width * 0.5f
                                                    ) * num45
                                                ),
                                            (num43 + (26f * inventoryScale))
                                                - (
                                                    (
                                                        itemTexture[
                                                            player[myPlayer].inventory[num20].type
                                                        ].Height * 0.5f
                                                    ) * num45
                                                )
                                        ),
                                        new Rectangle(
                                            0,
                                            0,
                                            itemTexture[
                                                player[myPlayer].inventory[num20].type
                                            ].Width,
                                            itemTexture[
                                                player[myPlayer].inventory[num20].type
                                            ].Height
                                        ),
                                        player[myPlayer].inventory[num20].GetColor(color10),
                                        0f,
                                        vector,
                                        num45,
                                        SpriteEffects.None,
                                        0f
                                    );
                                }
                                if (player[myPlayer].inventory[num20].stack > 1)
                                {
                                    vector = new Vector2();
                                    this.spriteBatch.DrawString(
                                        fontItemStack,
                                        string.Concat(player[myPlayer].inventory[num20].stack),
                                        new Vector2(
                                            num42 + (10f * inventoryScale),
                                            num43 + (26f * inventoryScale)
                                        ),
                                        color10,
                                        0f,
                                        vector,
                                        num45,
                                        SpriteEffects.None,
                                        0f
                                    );
                                }
                            }
                        }
                    }
                    for (num20 = 0; num20 < 3; num20++)
                    {
                        num42 = 330;
                        num43 = (int)(238f + ((num20 * 0x38) * inventoryScale));
                        color10 = new Color(0xff, 0xff, 0xff, 0xff);
                        if (
                            (
                                (
                                    (mouseState.X >= num42)
                                    && (
                                        mouseState.X
                                        <= (num42 + (hotbarTexture.Width * inventoryScale))
                                    )
                                ) && (mouseState.Y >= num43)
                            ) && (mouseState.Y <= (num43 + (hotbarTexture.Height * inventoryScale)))
                        )
                        {
                            player[myPlayer].mouseInterface = true;
                            if (
                                (mouseLeftRelease && (mouseState.LeftButton == ButtonState.Pressed))
                                && (
                                    (
                                        (
                                            (Main.mouseItem.type == 0)
                                            || ((Main.mouseItem.headSlot > 0) && (num20 == 0))
                                        ) || ((Main.mouseItem.bodySlot > 0) && (num20 == 1))
                                    ) || ((Main.mouseItem.legSlot > 0) && (num20 == 2))
                                )
                            )
                            {
                                mouseItem = Main.mouseItem;
                                Main.mouseItem = player[myPlayer].armor[num20];
                                player[myPlayer].armor[num20] = mouseItem;
                                if (
                                    (player[myPlayer].armor[num20].type == 0)
                                    || (player[myPlayer].armor[num20].stack < 1)
                                )
                                {
                                    player[myPlayer].armor[num20] = new Item();
                                }
                                if ((Main.mouseItem.type == 0) || (Main.mouseItem.stack < 1))
                                {
                                    Main.mouseItem = new Item();
                                }
                                if (
                                    (Main.mouseItem.type > 0)
                                    || (player[myPlayer].armor[num20].type > 0)
                                )
                                {
                                    Recipe.FindRecipes();
                                    soundInstanceGrab.Stop();
                                    soundInstanceGrab = soundGrab.CreateInstance();
                                    soundInstanceGrab.Play();
                                }
                            }
                            text = player[myPlayer].armor[num20].name;
                            if (player[myPlayer].armor[num20].stack > 1)
                            {
                                obj2 = text;
                                text = string.Concat(
                                    new object[]
                                    {
                                        obj2,
                                        " (",
                                        player[myPlayer].armor[num20].stack,
                                        ")",
                                    }
                                );
                            }
                        }
                        vector = new Vector2();
                        this.spriteBatch.Draw(
                            hotbarTexture,
                            new Vector2((float)num42, (float)num43),
                            new Rectangle(0, 0, hotbarTexture.Width, hotbarTexture.Height),
                            color10,
                            0f,
                            vector,
                            inventoryScale,
                            SpriteEffects.None,
                            0f
                        );
                        vector = new Vector2();
                        this.spriteBatch.Draw(
                            inventoryBackTexture,
                            new Vector2((float)num42, (float)num43),
                            new Rectangle(
                                0,
                                0,
                                inventoryBackTexture.Width,
                                inventoryBackTexture.Height
                            ),
                            new Color(200, 200, 200, 200),
                            0f,
                            vector,
                            inventoryScale,
                            SpriteEffects.None,
                            0f
                        );
                        color10 = Color.White;
                        if (
                            (player[myPlayer].armor[num20].type > 0)
                            && (player[myPlayer].armor[num20].stack > 0)
                        )
                        {
                            num45 = 1f;
                            if (
                                (itemTexture[player[myPlayer].armor[num20].type].Width > 0x20)
                                || (itemTexture[player[myPlayer].armor[num20].type].Height > 0x20)
                            )
                            {
                                if (
                                    itemTexture[player[myPlayer].armor[num20].type].Width
                                    > itemTexture[player[myPlayer].armor[num20].type].Height
                                )
                                {
                                    num45 =
                                        32f
                                        / (
                                            (float)
                                                itemTexture[
                                                    player[myPlayer].armor[num20].type
                                                ].Width
                                        );
                                }
                                else
                                {
                                    num45 =
                                        32f
                                        / (
                                            (float)
                                                itemTexture[
                                                    player[myPlayer].armor[num20].type
                                                ].Height
                                        );
                                }
                            }
                            num45 *= inventoryScale;
                            vector = new Vector2();
                            this.spriteBatch.Draw(
                                itemTexture[player[myPlayer].armor[num20].type],
                                new Vector2(
                                    (num42 + (26f * inventoryScale))
                                        - (
                                            (
                                                itemTexture[
                                                    player[myPlayer].armor[num20].type
                                                ].Width * 0.5f
                                            ) * num45
                                        ),
                                    (num43 + (26f * inventoryScale))
                                        - (
                                            (
                                                itemTexture[
                                                    player[myPlayer].armor[num20].type
                                                ].Height * 0.5f
                                            ) * num45
                                        )
                                ),
                                new Rectangle(
                                    0,
                                    0,
                                    itemTexture[player[myPlayer].armor[num20].type].Width,
                                    itemTexture[player[myPlayer].armor[num20].type].Height
                                ),
                                player[myPlayer].armor[num20].GetAlpha(color10),
                                0f,
                                vector,
                                num45,
                                SpriteEffects.None,
                                0f
                            );
                            color15 = new Color();
                            if (player[myPlayer].armor[num20].color != color15)
                            {
                                vector = new Vector2();
                                this.spriteBatch.Draw(
                                    itemTexture[player[myPlayer].armor[num20].type],
                                    new Vector2(
                                        (num42 + (26f * inventoryScale))
                                            - (
                                                (
                                                    itemTexture[
                                                        player[myPlayer].armor[num20].type
                                                    ].Width * 0.5f
                                                ) * num45
                                            ),
                                        (num43 + (26f * inventoryScale))
                                            - (
                                                (
                                                    itemTexture[
                                                        player[myPlayer].armor[num20].type
                                                    ].Height * 0.5f
                                                ) * num45
                                            )
                                    ),
                                    new Rectangle(
                                        0,
                                        0,
                                        itemTexture[player[myPlayer].armor[num20].type].Width,
                                        itemTexture[player[myPlayer].armor[num20].type].Height
                                    ),
                                    player[myPlayer].armor[num20].GetColor(color10),
                                    0f,
                                    vector,
                                    num45,
                                    SpriteEffects.None,
                                    0f
                                );
                            }
                            if (player[myPlayer].armor[num20].stack > 1)
                            {
                                vector = new Vector2();
                                this.spriteBatch.DrawString(
                                    fontItemStack,
                                    string.Concat(player[myPlayer].inventory[num20].stack),
                                    new Vector2(
                                        num42 + (10f * inventoryScale),
                                        num43 + (26f * inventoryScale)
                                    ),
                                    color10,
                                    0f,
                                    vector,
                                    num45,
                                    SpriteEffects.None,
                                    0f
                                );
                            }
                        }
                    }
                    for (num20 = 0; num20 < Recipe.maxRecipes; num20++)
                    {
                        inventoryScale = 100f / (Math.Abs(availableRecipeY[num20]) + 100f);
                        if (inventoryScale < 0.75)
                        {
                            inventoryScale = 0.75f;
                        }
                        if (availableRecipeY[num20] < ((num20 - focusRecipe) * 0x41))
                        {
                            if (availableRecipeY[num20] == 0f)
                            {
                                soundInstanceMenuTick.Play();
                            }
                            availableRecipeY[num20] += 6.5f;
                        }
                        else if (availableRecipeY[num20] > ((num20 - focusRecipe) * 0x41))
                        {
                            if (availableRecipeY[num20] == 0f)
                            {
                                soundInstanceMenuTick.Play();
                            }
                            availableRecipeY[num20] -= 6.5f;
                        }
                        if (
                            (num20 < numAvailableRecipes)
                            && (Math.Abs(availableRecipeY[num20]) <= 250f)
                        )
                        {
                            num42 = (int)(46f - (26f * inventoryScale));
                            num43 = (int)(
                                (400f + (availableRecipeY[num20] * inventoryScale))
                                - (30f * inventoryScale)
                            );
                            num46 = 255.0;
                            if (Math.Abs(availableRecipeY[num20]) > 150f)
                            {
                                num46 =
                                    (255f * (100f - (Math.Abs(availableRecipeY[num20]) - 150f)))
                                    * 0.01;
                            }
                            color10 = Color.White;
                            color10.R = (byte)num46;
                            color10.G = (byte)num46;
                            color10.B = (byte)num46;
                            color10.A = (byte)num46;
                            if (
                                (
                                    (
                                        (mouseState.X >= num42)
                                        && (
                                            mouseState.X
                                            <= (num42 + (hotbarTexture.Width * inventoryScale))
                                        )
                                    ) && (mouseState.Y >= num43)
                                )
                                && (
                                    mouseState.Y
                                    <= (num43 + (hotbarTexture.Height * inventoryScale))
                                )
                            )
                            {
                                player[myPlayer].mouseInterface = true;
                                if (
                                    mouseLeftRelease
                                    && (mouseState.LeftButton == ButtonState.Pressed)
                                )
                                {
                                    if (focusRecipe == num20)
                                    {
                                        if (
                                            (Main.mouseItem.type == 0)
                                            || (
                                                Main.mouseItem.IsTheSameAs(
                                                    recipe[availableRecipe[num20]].createItem
                                                )
                                                && (
                                                    (
                                                        Main.mouseItem.stack
                                                        + recipe[availableRecipe[num20]]
                                                            .createItem
                                                            .stack
                                                    ) <= Main.mouseItem.maxStack
                                                )
                                            )
                                        )
                                        {
                                            int stack = Main.mouseItem.stack;
                                            Main.mouseItem = (Item)
                                                recipe[availableRecipe[num20]].createItem.Clone();
                                            Main.mouseItem.stack += stack;
                                            recipe[availableRecipe[num20]].Create();
                                            if (
                                                (Main.mouseItem.type > 0)
                                                || (
                                                    recipe[availableRecipe[num20]].createItem.type
                                                    > 0
                                                )
                                            )
                                            {
                                                soundInstanceGrab.Stop();
                                                soundInstanceGrab = soundGrab.CreateInstance();
                                                soundInstanceGrab.Play();
                                            }
                                        }
                                    }
                                    else
                                    {
                                        focusRecipe = num20;
                                    }
                                }
                                text = recipe[availableRecipe[num20]].createItem.name;
                                if (recipe[availableRecipe[num20]].createItem.stack > 1)
                                {
                                    obj2 = text;
                                    text = string.Concat(
                                        new object[]
                                        {
                                            obj2,
                                            " (",
                                            recipe[availableRecipe[num20]].createItem.stack,
                                            ")",
                                        }
                                    );
                                }
                            }
                            vector = new Vector2();
                            this.spriteBatch.Draw(
                                hotbarTexture,
                                new Vector2((float)num42, (float)num43),
                                new Rectangle(0, 0, hotbarTexture.Width, hotbarTexture.Height),
                                color10,
                                0f,
                                vector,
                                inventoryScale,
                                SpriteEffects.None,
                                0f
                            );
                            num46 -= 50.0;
                            if (num46 < 0.0)
                            {
                                num46 = 0.0;
                            }
                            vector = new Vector2();
                            this.spriteBatch.Draw(
                                inventoryBackTexture,
                                new Vector2((float)num42, (float)num43),
                                new Rectangle(
                                    0,
                                    0,
                                    inventoryBackTexture.Width,
                                    inventoryBackTexture.Height
                                ),
                                new Color((byte)num46, (byte)num46, (byte)num46, (byte)num46),
                                0f,
                                vector,
                                inventoryScale,
                                SpriteEffects.None,
                                0f
                            );
                            if (
                                (recipe[availableRecipe[num20]].createItem.type > 0)
                                && (recipe[availableRecipe[num20]].createItem.stack > 0)
                            )
                            {
                                num45 = 1f;
                                if (
                                    (
                                        itemTexture[
                                            recipe[availableRecipe[num20]].createItem.type
                                        ].Width > 0x20
                                    )
                                    || (
                                        itemTexture[
                                            recipe[availableRecipe[num20]].createItem.type
                                        ].Height > 0x20
                                    )
                                )
                                {
                                    if (
                                        itemTexture[
                                            recipe[availableRecipe[num20]].createItem.type
                                        ].Width
                                        > itemTexture[
                                            recipe[availableRecipe[num20]].createItem.type
                                        ].Height
                                    )
                                    {
                                        num45 =
                                            32f
                                            / (
                                                (float)
                                                    itemTexture[
                                                        recipe[availableRecipe[num20]]
                                                            .createItem
                                                            .type
                                                    ].Width
                                            );
                                    }
                                    else
                                    {
                                        num45 =
                                            32f
                                            / (
                                                (float)
                                                    itemTexture[
                                                        recipe[availableRecipe[num20]]
                                                            .createItem
                                                            .type
                                                    ].Height
                                            );
                                    }
                                }
                                num45 *= inventoryScale;
                                vector = new Vector2();
                                this.spriteBatch.Draw(
                                    itemTexture[recipe[availableRecipe[num20]].createItem.type],
                                    new Vector2(
                                        (num42 + (26f * inventoryScale))
                                            - (
                                                (
                                                    itemTexture[
                                                        recipe[availableRecipe[num20]]
                                                            .createItem
                                                            .type
                                                    ].Width * 0.5f
                                                ) * num45
                                            ),
                                        (num43 + (26f * inventoryScale))
                                            - (
                                                (
                                                    itemTexture[
                                                        recipe[availableRecipe[num20]]
                                                            .createItem
                                                            .type
                                                    ].Height * 0.5f
                                                ) * num45
                                            )
                                    ),
                                    new Rectangle(
                                        0,
                                        0,
                                        itemTexture[
                                            recipe[availableRecipe[num20]].createItem.type
                                        ].Width,
                                        itemTexture[
                                            recipe[availableRecipe[num20]].createItem.type
                                        ].Height
                                    ),
                                    recipe[availableRecipe[num20]].createItem.GetAlpha(color10),
                                    0f,
                                    vector,
                                    num45,
                                    SpriteEffects.None,
                                    0f
                                );
                                color15 = new Color();
                                if (recipe[availableRecipe[num20]].createItem.color != color15)
                                {
                                    vector = new Vector2();
                                    this.spriteBatch.Draw(
                                        itemTexture[recipe[availableRecipe[num20]].createItem.type],
                                        new Vector2(
                                            (num42 + (26f * inventoryScale))
                                                - (
                                                    (
                                                        itemTexture[
                                                            recipe[availableRecipe[num20]]
                                                                .createItem
                                                                .type
                                                        ].Width * 0.5f
                                                    ) * num45
                                                ),
                                            (num43 + (26f * inventoryScale))
                                                - (
                                                    (
                                                        itemTexture[
                                                            recipe[availableRecipe[num20]]
                                                                .createItem
                                                                .type
                                                        ].Height * 0.5f
                                                    ) * num45
                                                )
                                        ),
                                        new Rectangle(
                                            0,
                                            0,
                                            itemTexture[
                                                recipe[availableRecipe[num20]].createItem.type
                                            ].Width,
                                            itemTexture[
                                                recipe[availableRecipe[num20]].createItem.type
                                            ].Height
                                        ),
                                        recipe[availableRecipe[num20]].createItem.GetColor(color10),
                                        0f,
                                        vector,
                                        num45,
                                        SpriteEffects.None,
                                        0f
                                    );
                                }
                                if (recipe[availableRecipe[num20]].createItem.stack > 1)
                                {
                                    vector = new Vector2();
                                    this.spriteBatch.DrawString(
                                        fontItemStack,
                                        string.Concat(
                                            Main.recipe[Main.availableRecipe[num20]]
                                                .createItem
                                                .stack
                                        ),
                                        new Vector2(
                                            num42 + (10f * inventoryScale),
                                            num43 + (26f * inventoryScale)
                                        ),
                                        color10,
                                        0f,
                                        vector,
                                        num45,
                                        SpriteEffects.None,
                                        0f
                                    );
                                }
                            }
                        }
                    }
                    if (numAvailableRecipes > 0)
                    {
                        for (num20 = 0; num20 < Recipe.maxRequirements; num20++)
                        {
                            if (recipe[availableRecipe[focusRecipe]].requiredItem[num20].type == 0)
                            {
                                break;
                            }
                            num42 = 80 + (num20 * 40);
                            num43 = 380;
                            num46 = 255.0;
                            color10 = Color.White;
                            num46 = 255f - (Math.Abs(availableRecipeY[focusRecipe]) * 3f);
                            if (num46 < 0.0)
                            {
                                num46 = 0.0;
                            }
                            color10.R = (byte)num46;
                            color10.G = (byte)num46;
                            color10.B = (byte)num46;
                            color10.A = (byte)num46;
                            inventoryScale = 0.6f;
                            if (num46 == 0.0)
                            {
                                break;
                            }
                            if (
                                (
                                    (
                                        (mouseState.X >= num42)
                                        && (
                                            mouseState.X
                                            <= (num42 + (hotbarTexture.Width * inventoryScale))
                                        )
                                    ) && (mouseState.Y >= num43)
                                )
                                && (
                                    mouseState.Y
                                    <= (num43 + (hotbarTexture.Height * inventoryScale))
                                )
                            )
                            {
                                player[myPlayer].mouseInterface = true;
                                text = recipe[availableRecipe[focusRecipe]]
                                    .requiredItem[num20]
                                    .name;
                                if (
                                    recipe[availableRecipe[focusRecipe]].requiredItem[num20].stack
                                    > 1
                                )
                                {
                                    obj2 = text;
                                    text = string.Concat(
                                        new object[]
                                        {
                                            obj2,
                                            " (",
                                            recipe[availableRecipe[focusRecipe]]
                                                .requiredItem[num20]
                                                .stack,
                                            ")",
                                        }
                                    );
                                }
                            }
                            vector = new Vector2();
                            this.spriteBatch.Draw(
                                hotbarTexture,
                                new Vector2((float)num42, (float)num43),
                                new Rectangle(0, 0, hotbarTexture.Width, hotbarTexture.Height),
                                color10,
                                0f,
                                vector,
                                inventoryScale,
                                SpriteEffects.None,
                                0f
                            );
                            num46 -= 50.0;
                            if (num46 < 0.0)
                            {
                                num46 = 0.0;
                            }
                            vector = new Vector2();
                            this.spriteBatch.Draw(
                                inventoryBackTexture,
                                new Vector2((float)num42, (float)num43),
                                new Rectangle(
                                    0,
                                    0,
                                    inventoryBackTexture.Width,
                                    inventoryBackTexture.Height
                                ),
                                new Color((byte)num46, (byte)num46, (byte)num46, (byte)num46),
                                0f,
                                vector,
                                inventoryScale,
                                SpriteEffects.None,
                                0f
                            );
                            if (
                                (recipe[availableRecipe[focusRecipe]].requiredItem[num20].type > 0)
                                && (
                                    recipe[availableRecipe[focusRecipe]].requiredItem[num20].stack
                                    > 0
                                )
                            )
                            {
                                num45 = 1f;
                                if (
                                    (
                                        itemTexture[
                                            recipe[availableRecipe[focusRecipe]]
                                                .requiredItem[num20]
                                                .type
                                        ].Width > 0x20
                                    )
                                    || (
                                        itemTexture[
                                            recipe[availableRecipe[focusRecipe]]
                                                .requiredItem[num20]
                                                .type
                                        ].Height > 0x20
                                    )
                                )
                                {
                                    if (
                                        itemTexture[
                                            recipe[availableRecipe[focusRecipe]]
                                                .requiredItem[num20]
                                                .type
                                        ].Width
                                        > itemTexture[
                                            recipe[availableRecipe[focusRecipe]]
                                                .requiredItem[num20]
                                                .type
                                        ].Height
                                    )
                                    {
                                        num45 =
                                            32f
                                            / (
                                                (float)
                                                    itemTexture[
                                                        recipe[availableRecipe[focusRecipe]]
                                                            .requiredItem[num20]
                                                            .type
                                                    ].Width
                                            );
                                    }
                                    else
                                    {
                                        num45 =
                                            32f
                                            / (
                                                (float)
                                                    itemTexture[
                                                        recipe[availableRecipe[focusRecipe]]
                                                            .requiredItem[num20]
                                                            .type
                                                    ].Height
                                            );
                                    }
                                }
                                num45 *= inventoryScale;
                                vector = new Vector2();
                                this.spriteBatch.Draw(
                                    itemTexture[
                                        recipe[availableRecipe[focusRecipe]]
                                            .requiredItem[num20]
                                            .type
                                    ],
                                    new Vector2(
                                        (num42 + (26f * inventoryScale))
                                            - (
                                                (
                                                    itemTexture[
                                                        recipe[availableRecipe[focusRecipe]]
                                                            .requiredItem[num20]
                                                            .type
                                                    ].Width * 0.5f
                                                ) * num45
                                            ),
                                        (num43 + (26f * inventoryScale))
                                            - (
                                                (
                                                    itemTexture[
                                                        recipe[availableRecipe[focusRecipe]]
                                                            .requiredItem[num20]
                                                            .type
                                                    ].Height * 0.5f
                                                ) * num45
                                            )
                                    ),
                                    new Rectangle(
                                        0,
                                        0,
                                        itemTexture[
                                            recipe[availableRecipe[focusRecipe]]
                                                .requiredItem[num20]
                                                .type
                                        ].Width,
                                        itemTexture[
                                            recipe[availableRecipe[focusRecipe]]
                                                .requiredItem[num20]
                                                .type
                                        ].Height
                                    ),
                                    recipe[availableRecipe[focusRecipe]]
                                        .requiredItem[num20]
                                        .GetAlpha(color10),
                                    0f,
                                    vector,
                                    num45,
                                    SpriteEffects.None,
                                    0f
                                );
                                color15 = new Color();
                                if (
                                    recipe[availableRecipe[focusRecipe]].requiredItem[num20].color
                                    != color15
                                )
                                {
                                    vector = new Vector2();
                                    this.spriteBatch.Draw(
                                        itemTexture[
                                            recipe[availableRecipe[focusRecipe]]
                                                .requiredItem[num20]
                                                .type
                                        ],
                                        new Vector2(
                                            (num42 + (26f * inventoryScale))
                                                - (
                                                    (
                                                        itemTexture[
                                                            recipe[availableRecipe[focusRecipe]]
                                                                .requiredItem[num20]
                                                                .type
                                                        ].Width * 0.5f
                                                    ) * num45
                                                ),
                                            (num43 + (26f * inventoryScale))
                                                - (
                                                    (
                                                        itemTexture[
                                                            recipe[availableRecipe[focusRecipe]]
                                                                .requiredItem[num20]
                                                                .type
                                                        ].Height * 0.5f
                                                    ) * num45
                                                )
                                        ),
                                        new Rectangle(
                                            0,
                                            0,
                                            itemTexture[
                                                recipe[availableRecipe[focusRecipe]]
                                                    .requiredItem[num20]
                                                    .type
                                            ].Width,
                                            itemTexture[
                                                recipe[availableRecipe[focusRecipe]]
                                                    .requiredItem[num20]
                                                    .type
                                            ].Height
                                        ),
                                        recipe[availableRecipe[focusRecipe]]
                                            .requiredItem[num20]
                                            .GetColor(color10),
                                        0f,
                                        vector,
                                        num45,
                                        SpriteEffects.None,
                                        0f
                                    );
                                }
                                if (
                                    recipe[availableRecipe[focusRecipe]].requiredItem[num20].stack
                                    > 1
                                )
                                {
                                    vector = new Vector2();
                                    this.spriteBatch.DrawString(
                                        fontItemStack,
                                        string.Concat(
                                            Main.recipe[Main.availableRecipe[num20]]
                                                .createItem
                                                .stack
                                        ),
                                        new Vector2(
                                            num42 + (10f * inventoryScale),
                                            num43 + (26f * inventoryScale)
                                        ),
                                        color10,
                                        0f,
                                        vector,
                                        num45,
                                        SpriteEffects.None,
                                        0f
                                    );
                                }
                            }
                        }
                    }
                }
                if (!playerInventory)
                {
                    int num48 = 20;
                    num45 = 1f;
                    for (num20 = 0; num20 < 10; num20++)
                    {
                        if (num20 == player[myPlayer].selectedItem)
                        {
                            if (hotbarScale[num20] < 1f)
                            {
                                hotbarScale[num20] += 0.05f;
                            }
                        }
                        else if (hotbarScale[num20] > 0.75)
                        {
                            hotbarScale[num20] -= 0.05f;
                        }
                        int num49 = (int)(20f + (22f * (1f - hotbarScale[num20])));
                        int a = (int)(75f + (150f * hotbarScale[num20]));
                        Color color11 = new Color(0xff, 0xff, 0xff, a);
                        vector = new Vector2();
                        this.spriteBatch.Draw(
                            hotbarTexture,
                            new Vector2((float)num48, (float)num49),
                            new Rectangle(0, 0, hotbarTexture.Width, hotbarTexture.Height),
                            color11,
                            0f,
                            vector,
                            hotbarScale[num20],
                            SpriteEffects.None,
                            0f
                        );
                        if (
                            (
                                (
                                    (mouseState.X >= num48)
                                    && (
                                        mouseState.X
                                        <= (num48 + (hotbarTexture.Width * hotbarScale[num20]))
                                    )
                                ) && (mouseState.Y >= num49)
                            )
                            && (
                                mouseState.Y
                                <= (num49 + (hotbarTexture.Height * hotbarScale[num20]))
                            )
                        )
                        {
                            player[myPlayer].mouseInterface = true;
                            if (mouseState.LeftButton == ButtonState.Pressed)
                            {
                                player[myPlayer].changeItem = num20;
                            }
                            player[myPlayer].showItemIcon = false;
                            text = player[myPlayer].inventory[num20].name;
                            if (player[myPlayer].inventory[num20].stack > 1)
                            {
                                obj2 = text;
                                text = string.Concat(
                                    new object[]
                                    {
                                        obj2,
                                        " (",
                                        player[myPlayer].inventory[num20].stack,
                                        ")",
                                    }
                                );
                            }
                        }
                        if (
                            (player[myPlayer].inventory[num20].type > 0)
                            && (player[myPlayer].inventory[num20].stack > 0)
                        )
                        {
                            num45 = 1f;
                            if (
                                (itemTexture[player[myPlayer].inventory[num20].type].Width > 0x20)
                                || (
                                    itemTexture[player[myPlayer].inventory[num20].type].Height
                                    > 0x20
                                )
                            )
                            {
                                if (
                                    itemTexture[player[myPlayer].inventory[num20].type].Width
                                    > itemTexture[player[myPlayer].inventory[num20].type].Height
                                )
                                {
                                    num45 =
                                        32f
                                        / (
                                            (float)
                                                itemTexture[
                                                    player[myPlayer].inventory[num20].type
                                                ].Width
                                        );
                                }
                                else
                                {
                                    num45 =
                                        32f
                                        / (
                                            (float)
                                                itemTexture[
                                                    player[myPlayer].inventory[num20].type
                                                ].Height
                                        );
                                }
                            }
                            num45 *= hotbarScale[num20];
                            vector = new Vector2();
                            this.spriteBatch.Draw(
                                itemTexture[player[myPlayer].inventory[num20].type],
                                new Vector2(
                                    (num48 + (26f * hotbarScale[num20]))
                                        - (
                                            (
                                                itemTexture[
                                                    player[myPlayer].inventory[num20].type
                                                ].Width * 0.5f
                                            ) * num45
                                        ),
                                    (num49 + (26f * hotbarScale[num20]))
                                        - (
                                            (
                                                itemTexture[
                                                    player[myPlayer].inventory[num20].type
                                                ].Height * 0.5f
                                            ) * num45
                                        )
                                ),
                                new Rectangle(
                                    0,
                                    0,
                                    itemTexture[player[myPlayer].inventory[num20].type].Width,
                                    itemTexture[player[myPlayer].inventory[num20].type].Height
                                ),
                                player[myPlayer].inventory[num20].GetAlpha(color11),
                                0f,
                                vector,
                                num45,
                                SpriteEffects.None,
                                0f
                            );
                            color15 = new Color();
                            if (player[myPlayer].inventory[num20].color != color15)
                            {
                                vector = new Vector2();
                                this.spriteBatch.Draw(
                                    itemTexture[player[myPlayer].inventory[num20].type],
                                    new Vector2(
                                        (num48 + (26f * hotbarScale[num20]))
                                            - (
                                                (
                                                    itemTexture[
                                                        player[myPlayer].inventory[num20].type
                                                    ].Width * 0.5f
                                                ) * num45
                                            ),
                                        (num49 + (26f * hotbarScale[num20]))
                                            - (
                                                (
                                                    itemTexture[
                                                        player[myPlayer].inventory[num20].type
                                                    ].Height * 0.5f
                                                ) * num45
                                            )
                                    ),
                                    new Rectangle(
                                        0,
                                        0,
                                        itemTexture[player[myPlayer].inventory[num20].type].Width,
                                        itemTexture[player[myPlayer].inventory[num20].type].Height
                                    ),
                                    player[myPlayer].inventory[num20].GetColor(color11),
                                    0f,
                                    vector,
                                    num45,
                                    SpriteEffects.None,
                                    0f
                                );
                            }
                            if (player[myPlayer].inventory[num20].stack > 1)
                            {
                                vector = new Vector2();
                                this.spriteBatch.DrawString(
                                    fontItemStack,
                                    string.Concat(player[myPlayer].inventory[num20].stack),
                                    new Vector2(
                                        num48 + (10f * hotbarScale[num20]),
                                        num49 + (26f * hotbarScale[num20])
                                    ),
                                    color11,
                                    0f,
                                    vector,
                                    num45,
                                    SpriteEffects.None,
                                    0f
                                );
                            }
                        }
                        num48 += ((int)(hotbarTexture.Width * hotbarScale[num20])) + 4;
                    }
                }
                if (((text != null) && (text != "")) && (Main.mouseItem.type == 0))
                {
                    player[myPlayer].showItemIcon = false;
                    vector = new Vector2();
                    this.spriteBatch.DrawString(
                        fontMouseText,
                        text,
                        new Vector2((float)(mouseState.X + 10), (float)(mouseState.Y + 10)),
                        new Color(mouseTextColor, mouseTextColor, mouseTextColor, mouseTextColor),
                        0f,
                        vector,
                        (float)1f,
                        SpriteEffects.None,
                        0f
                    );
                    flag = true;
                }
                if (player[myPlayer].dead)
                {
                    string str2 = player[myPlayer].name + " was slain...";
                    vector = new Vector2();
                    this.spriteBatch.DrawString(
                        fontDeathText,
                        str2,
                        new Vector2(
                            (float)((screenWidth / 2) - (str2.Length * 10)),
                            (float)((Main.screenHeight / 2) - 20)
                        ),
                        player[myPlayer].GetDeathAlpha(new Color(0, 0, 0, 0)),
                        0f,
                        vector,
                        (float)1f,
                        SpriteEffects.None,
                        0f
                    );
                }
                vector = new Vector2();
                this.spriteBatch.Draw(
                    cursorTexture,
                    new Vector2((float)mouseState.X, (float)mouseState.Y),
                    new Rectangle(0, 0, cursorTexture.Width, cursorTexture.Height),
                    Color.White,
                    0f,
                    vector,
                    (float)1f,
                    SpriteEffects.None,
                    0f
                );
                if ((Main.mouseItem.type > 0) && (Main.mouseItem.stack > 0))
                {
                    player[myPlayer].showItemIcon = false;
                    player[myPlayer].showItemIcon2 = 0;
                    flag = true;
                    num45 = 1f;
                    if (
                        (itemTexture[Main.mouseItem.type].Width > 0x20)
                        || (itemTexture[Main.mouseItem.type].Height > 0x20)
                    )
                    {
                        if (
                            itemTexture[Main.mouseItem.type].Width
                            > itemTexture[Main.mouseItem.type].Height
                        )
                        {
                            num45 = 32f / ((float)itemTexture[Main.mouseItem.type].Width);
                        }
                        else
                        {
                            num45 = 32f / ((float)itemTexture[Main.mouseItem.type].Height);
                        }
                    }
                    float num51 = 1f;
                    Color color12 = Color.White;
                    num45 *= num51;
                    vector = new Vector2();
                    this.spriteBatch.Draw(
                        itemTexture[Main.mouseItem.type],
                        new Vector2(
                            (mouseState.X + (26f * num51))
                                - ((itemTexture[Main.mouseItem.type].Width * 0.5f) * num45),
                            (mouseState.Y + (26f * num51))
                                - ((itemTexture[Main.mouseItem.type].Height * 0.5f) * num45)
                        ),
                        new Rectangle(
                            0,
                            0,
                            itemTexture[Main.mouseItem.type].Width,
                            itemTexture[Main.mouseItem.type].Height
                        ),
                        Main.mouseItem.GetAlpha(color12),
                        0f,
                        vector,
                        num45,
                        SpriteEffects.None,
                        0f
                    );
                    color15 = new Color();
                    if (Main.mouseItem.color != color15)
                    {
                        vector = new Vector2();
                        this.spriteBatch.Draw(
                            itemTexture[Main.mouseItem.type],
                            new Vector2(
                                (mouseState.X + (26f * num51))
                                    - ((itemTexture[Main.mouseItem.type].Width * 0.5f) * num45),
                                (mouseState.Y + (26f * num51))
                                    - ((itemTexture[Main.mouseItem.type].Height * 0.5f) * num45)
                            ),
                            new Rectangle(
                                0,
                                0,
                                itemTexture[Main.mouseItem.type].Width,
                                itemTexture[Main.mouseItem.type].Height
                            ),
                            Main.mouseItem.GetColor(color12),
                            0f,
                            vector,
                            num45,
                            SpriteEffects.None,
                            0f
                        );
                    }
                    if (Main.mouseItem.stack > 1)
                    {
                        vector = new Vector2();
                        this.spriteBatch.DrawString(
                            fontItemStack,
                            string.Concat(Main.mouseItem.stack),
                            new Vector2(mouseState.X + (10f * num51), mouseState.Y + (26f * num51)),
                            color12,
                            0f,
                            vector,
                            num45,
                            SpriteEffects.None,
                            0f
                        );
                    }
                }
                Rectangle rectangle2 = new Rectangle(
                    mouseState.X + ((int)screenPosition.X),
                    mouseState.Y + ((int)screenPosition.Y),
                    1,
                    1
                );
                mouseTextColor = (byte)(mouseTextColor + ((byte)mouseTextColorChange));
                if (mouseTextColor >= 250)
                {
                    mouseTextColorChange = -4;
                }
                if (mouseTextColor <= 0xaf)
                {
                    mouseTextColorChange = 4;
                }
                if (!flag)
                {
                    int num52 = (0x1a * player[myPlayer].statLifeMax) / num34;
                    int num53 = 0;
                    if (player[myPlayer].statLifeMax > 200)
                    {
                        num52 -= 260;
                        num53 += 0x1a;
                    }
                    if (
                        (
                            ((mouseState.X > 500) && (mouseState.X < (500 + num52)))
                            && (mouseState.Y > 0x20)
                        ) && (mouseState.Y < ((0x20 + heartTexture.Height) + num53))
                    )
                    {
                        player[myPlayer].showItemIcon = false;
                        name = string.Concat(
                            new object[]
                            {
                                "Life: ",
                                player[myPlayer].statLife,
                                "/",
                                player[myPlayer].statLifeMax,
                            }
                        );
                        vector = new Vector2();
                        this.spriteBatch.DrawString(
                            fontMouseText,
                            name,
                            new Vector2((float)(mouseState.X + 10), (float)(mouseState.Y + 10)),
                            new Color(
                                mouseTextColor,
                                mouseTextColor,
                                mouseTextColor,
                                mouseTextColor
                            ),
                            0f,
                            vector,
                            (float)1f,
                            SpriteEffects.None,
                            0f
                        );
                        flag = true;
                    }
                }
                if (!flag)
                {
                    for (num20 = 0; num20 < 0x3e8; num20++)
                    {
                        if (item[num20].active)
                        {
                            Rectangle rectangle3 = new Rectangle(
                                (int)(
                                    (item[num20].position.X + (item[num20].width * 0.5))
                                    - (itemTexture[item[num20].type].Width * 0.5)
                                ),
                                (((int)item[num20].position.Y) + item[num20].height)
                                    - itemTexture[item[num20].type].Height,
                                itemTexture[item[num20].type].Width,
                                itemTexture[item[num20].type].Height
                            );
                            if (rectangle2.Intersects(rectangle3))
                            {
                                player[myPlayer].showItemIcon = false;
                                name = item[num20].name;
                                if (item[num20].stack > 1)
                                {
                                    obj2 = name;
                                    name = string.Concat(
                                        new object[] { obj2, " (", item[num20].stack, ")" }
                                    );
                                }
                                vector = new Vector2();
                                this.spriteBatch.DrawString(
                                    fontMouseText,
                                    name,
                                    new Vector2(
                                        (float)(mouseState.X + 10),
                                        (float)(mouseState.Y + 10)
                                    ),
                                    new Color(
                                        mouseTextColor,
                                        mouseTextColor,
                                        mouseTextColor,
                                        mouseTextColor
                                    ),
                                    0f,
                                    vector,
                                    (float)1f,
                                    SpriteEffects.None,
                                    0f
                                );
                                flag = true;
                                break;
                            }
                        }
                    }
                }
                if (!flag)
                {
                    for (num20 = 0; num20 < 0x3e8; num20++)
                    {
                        if (npc[num20].active)
                        {
                            Rectangle rectangle4 = new Rectangle(
                                (int)(
                                    (npc[num20].position.X + (npc[num20].width * 0.5))
                                    - (npcTexture[npc[num20].type].Width * 0.5)
                                ),
                                (((int)npc[num20].position.Y) + npc[num20].height)
                                    - (
                                        npcTexture[npc[num20].type].Height
                                        / npcFrameCount[npc[num20].type]
                                    ),
                                npcTexture[npc[num20].type].Width,
                                npcTexture[npc[num20].type].Height / npcFrameCount[npc[num20].type]
                            );
                            if (rectangle2.Intersects(rectangle4))
                            {
                                player[myPlayer].showItemIcon = false;
                                name = string.Concat(
                                    new object[]
                                    {
                                        npc[num20].name,
                                        ": ",
                                        npc[num20].life,
                                        "/",
                                        npc[num20].lifeMax,
                                    }
                                );
                                vector = new Vector2();
                                this.spriteBatch.DrawString(
                                    fontMouseText,
                                    name,
                                    new Vector2(
                                        (float)(mouseState.X + 10),
                                        (float)(mouseState.Y + 10)
                                    ),
                                    new Color(
                                        mouseTextColor,
                                        mouseTextColor,
                                        mouseTextColor,
                                        mouseTextColor
                                    ),
                                    0f,
                                    vector,
                                    (float)1f,
                                    SpriteEffects.None,
                                    0f
                                );
                                break;
                            }
                        }
                    }
                }
                if (
                    player[myPlayer].showItemIcon
                    && (
                        (player[myPlayer].inventory[player[myPlayer].selectedItem].type > 0)
                        || (player[myPlayer].showItemIcon2 > 0)
                    )
                )
                {
                    int type = player[myPlayer].inventory[player[myPlayer].selectedItem].type;
                    Color alpha = player[myPlayer]
                        .inventory[player[myPlayer].selectedItem]
                        .GetAlpha(Color.White);
                    Color color14 = player[myPlayer]
                        .inventory[player[myPlayer].selectedItem]
                        .GetColor(Color.White);
                    if (player[myPlayer].showItemIcon2 > 0)
                    {
                        type = player[myPlayer].showItemIcon2;
                        alpha = Color.White;
                        color14 = new Color();
                    }
                    vector = new Vector2();
                    this.spriteBatch.Draw(
                        itemTexture[type],
                        new Vector2((float)(mouseState.X + 10), (float)(mouseState.Y + 10)),
                        new Rectangle(0, 0, itemTexture[type].Width, itemTexture[type].Height),
                        alpha,
                        0f,
                        vector,
                        (float)1f,
                        SpriteEffects.None,
                        0f
                    );
                    if (
                        (player[myPlayer].showItemIcon2 == 0)
                        && (
                            player[myPlayer].inventory[player[myPlayer].selectedItem].color
                            != new Color()
                        )
                    )
                    {
                        this.spriteBatch.Draw(
                            itemTexture[
                                player[myPlayer].inventory[player[myPlayer].selectedItem].type
                            ],
                            new Vector2((float)(mouseState.X + 10), (float)(mouseState.Y + 10)),
                            new Rectangle(
                                0,
                                0,
                                itemTexture[
                                    player[myPlayer].inventory[player[myPlayer].selectedItem].type
                                ].Width,
                                itemTexture[
                                    player[myPlayer].inventory[player[myPlayer].selectedItem].type
                                ].Height
                            ),
                            color14,
                            0f,
                            new Vector2(),
                            (float)1f,
                            SpriteEffects.None,
                            0f
                        );
                    }
                }
                player[myPlayer].showItemIcon = false;
                player[myPlayer].showItemIcon2 = 0;
                this.spriteBatch.End();
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    mouseLeftRelease = false;
                }
                else
                {
                    mouseLeftRelease = true;
                }
            }
        }

        protected override void Initialize()
        {
            int num;
            base.Window.Title = "Terraria: Dig Peon, Dig!";
            for (num = 0; num < 0x1389; num++)
            {
                for (int i = 0; i < 0x9c5; i++)
                {
                    tile[num, i] = new Tile();
                }
            }
            tileSolid[0] = true;
            tileSolid[1] = true;
            tileSolid[2] = true;
            tileSolid[3] = false;
            tileNoFail[3] = true;
            tileSolid[4] = false;
            tileNoFail[4] = true;
            tileSolid[5] = false;
            tileSolid[6] = true;
            tileSolid[7] = true;
            tileSolid[8] = true;
            tileSolid[9] = true;
            tileSolid[10] = true;
            tileSolid[11] = false;
            for (num = 0; num < 0x3e8; num++)
            {
                dust[num] = new Dust();
            }
            for (num = 0; num < 0x3e8; num++)
            {
                item[num] = new Item();
            }
            for (num = 0; num < 0x3e8; num++)
            {
                npc[num] = new NPC();
            }
            Player.SetupPlayers();
            for (num = 0; num < Recipe.maxRecipes; num++)
            {
                recipe[num] = new Recipe();
                availableRecipeY[num] = 0x41 * num;
            }
            Recipe.SetupRecipes();
            this.graphics.PreferredBackBufferWidth = screenWidth;
            this.graphics.PreferredBackBufferHeight = screenHeight;
            this.graphics.ApplyChanges();
            WorldGen.generateWorld();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            int num;
            this.spriteBatch = new SpriteBatch(base.GraphicsDevice);
            for (num = 0; num < 12; num++)
            {
                tileTexture[num] = base.Content.Load<Texture2D>(@"Images\Tiles_" + num);
            }
            for (num = 1; num < 2; num++)
            {
                wallTexture[num] = base.Content.Load<Texture2D>(@"Images\Wall_" + num);
            }
            for (num = 0; num < 3; num++)
            {
                backgroundTexture[num] = base.Content.Load<Texture2D>(@"Images\Background_" + num);
                backgroundWidth[num] = backgroundTexture[num].Width;
                backgroundHeight[num] = backgroundTexture[num].Height;
            }
            for (num = 0; num < 0x1b; num++)
            {
                itemTexture[num] = base.Content.Load<Texture2D>(@"Images\Item_" + num);
            }
            for (num = 0; num < 2; num++)
            {
                npcTexture[num] = base.Content.Load<Texture2D>(@"Images\NPC_" + num);
            }
            hotbarTexture = base.Content.Load<Texture2D>(@"Images\Hotbar");
            dustTexture = base.Content.Load<Texture2D>(@"Images\Dust");
            sunTexture = base.Content.Load<Texture2D>(@"Images\Sun");
            moonTexture = base.Content.Load<Texture2D>(@"Images\Moon");
            blackTileTexture = base.Content.Load<Texture2D>(@"Images\Black_Tile");
            heartTexture = base.Content.Load<Texture2D>(@"Images\Heart");
            cursorTexture = base.Content.Load<Texture2D>(@"Images\Cursor");
            treeTopTexture = base.Content.Load<Texture2D>(@"Images\Tree_Tops");
            treeBranchTexture = base.Content.Load<Texture2D>(@"Images\Tree_Branches");
            inventoryBackTexture = base.Content.Load<Texture2D>(@"Images\Inventory_Back");
            playerHeadTexture = base.Content.Load<Texture2D>(@"Images\Character_Heads");
            playerBodyTexture = base.Content.Load<Texture2D>(@"Images\Character_Bodies");
            playerLegTexture = base.Content.Load<Texture2D>(@"Images\Character_Legs");
            soundGrab = base.Content.Load<SoundEffect>(@"Sounds\Grab");
            soundInstanceGrab = soundGrab.CreateInstance();
            soundDig[0] = base.Content.Load<SoundEffect>(@"Sounds\Dig_0");
            soundInstanceDig[0] = soundDig[0].CreateInstance();
            soundDig[1] = base.Content.Load<SoundEffect>(@"Sounds\Dig_1");
            soundInstanceDig[1] = soundDig[1].CreateInstance();
            soundDig[2] = base.Content.Load<SoundEffect>(@"Sounds\Dig_2");
            soundInstanceDig[2] = soundDig[2].CreateInstance();
            soundPlayerHit[0] = base.Content.Load<SoundEffect>(@"Sounds\Player_Hit_0");
            soundInstancePlayerHit[0] = soundPlayerHit[0].CreateInstance();
            soundPlayerHit[1] = base.Content.Load<SoundEffect>(@"Sounds\Player_Hit_1");
            soundInstancePlayerHit[1] = soundPlayerHit[1].CreateInstance();
            soundPlayerHit[2] = base.Content.Load<SoundEffect>(@"Sounds\Player_Hit_2");
            soundInstancePlayerHit[2] = soundPlayerHit[2].CreateInstance();
            soundPlayerKilled = base.Content.Load<SoundEffect>(@"Sounds\Player_Killed");
            soundInstancePlayerKilled = soundPlayerKilled.CreateInstance();
            soundGrass = base.Content.Load<SoundEffect>(@"Sounds\Grass");
            soundInstanceGrass = soundGrass.CreateInstance();
            soundDoorOpen = base.Content.Load<SoundEffect>(@"Sounds\Door_Opened");
            soundInstanceDoorOpen = soundDoorOpen.CreateInstance();
            soundDoorClosed = base.Content.Load<SoundEffect>(@"Sounds\Door_Closed");
            soundInstanceDoorClosed = soundDoorClosed.CreateInstance();
            soundMenuTick = base.Content.Load<SoundEffect>(@"Sounds\Menu_Tick");
            soundInstanceMenuTick = soundMenuTick.CreateInstance();
            soundMenuOpen = base.Content.Load<SoundEffect>(@"Sounds\Menu_Open");
            soundMenuClose = base.Content.Load<SoundEffect>(@"Sounds\Menu_Close");
            for (num = 1; num < 3; num++)
            {
                soundItem[num] = base.Content.Load<SoundEffect>(@"Sounds\Item_" + num);
                soundInstanceItem[num] = soundItem[num].CreateInstance();
            }
            for (num = 1; num < 2; num++)
            {
                soundNPCHit[num] = base.Content.Load<SoundEffect>(@"Sounds\NPC_Hit_" + num);
                soundInstanceNPCHit[num] = soundNPCHit[num].CreateInstance();
            }
            for (num = 1; num < 2; num++)
            {
                soundNPCKilled[num] = base.Content.Load<SoundEffect>(@"Sounds\NPC_Killed_" + num);
                soundInstanceNPCKilled[num] = soundNPCKilled[num].CreateInstance();
            }
            fontItemStack = base.Content.Load<SpriteFont>(@"Fonts\Item_Stack");
            fontMouseText = base.Content.Load<SpriteFont>(@"Fonts\Mouse_Text");
            fontDeathText = base.Content.Load<SpriteFont>(@"Fonts\Death_Text");
        }

        protected override void UnloadContent() { }

        protected override void Update(GameTime gameTime)
        {
            if (!base.IsActive)
            {
                base.IsMouseVisible = true;
                player[myPlayer].delayUseItem = true;
                mouseLeftRelease = false;
            }
            else
            {
                int num;
                base.IsMouseVisible = false;
                if (
                    keyState.IsKeyDown(Keys.LeftAlt)
                    || (keyState.IsKeyDown(Keys.RightAlt) && keyState.IsKeyDown(Keys.Enter))
                )
                {
                    if (this.toggleFullscreen)
                    {
                        this.graphics.ToggleFullScreen();
                    }
                    this.toggleFullscreen = false;
                }
                else
                {
                    this.toggleFullscreen = true;
                }
                oldMouseState = mouseState;
                mouseState = Mouse.GetState();
                keyState = Keyboard.GetState();
                if (debugMode)
                {
                    UpdateDebug();
                }
                for (num = 0; num < 0x10; num++)
                {
                    player[num].UpdatePlayer(num);
                }
                NPC.SpawnNPC();
                for (num = 0; num < 0x10; num++)
                {
                    player[num].activeNPCs = 0;
                }
                for (num = 0; num < 0x3e8; num++)
                {
                    npc[num].UpdateNPC(num);
                }
                for (num = 0; num < 0x3e8; num++)
                {
                    item[num].UpdateItem(num);
                }
                Dust.UpdateDust();
                UpdateTime();
                WorldGen.UpdateWorld();
                base.Update(gameTime);
            }
        }

        private static void UpdateDebug()
        {
            if (keyState.IsKeyDown(Keys.Left))
            {
                screenPosition.X -= 8f;
            }
            if (keyState.IsKeyDown(Keys.Right))
            {
                screenPosition.X += 8f;
            }
            if (keyState.IsKeyDown(Keys.Up))
            {
                screenPosition.Y -= 8f;
            }
            if (keyState.IsKeyDown(Keys.Down))
            {
                screenPosition.Y += 8f;
            }
            int i = 0;
            int j = 0;
            i = (int)((mouseState.X + screenPosition.X) / 16f);
            j = (int)((mouseState.Y + screenPosition.Y) / 16f);
            if (
                ((mouseState.X < screenWidth) && (mouseState.Y < screenHeight))
                && ((((i >= 0) && (j >= 0)) && (i < 0x1389)) && (j < 0x9c5))
            )
            {
                if (
                    (mouseState.RightButton == ButtonState.Pressed)
                    && (mouseState.LeftButton == ButtonState.Pressed)
                )
                {
                    if (player[myPlayer].releaseUseItem)
                    {
                        int index = NPC.NewNPC(
                            mouseState.X + ((int)screenPosition.X),
                            mouseState.Y + ((int)screenPosition.Y),
                            1
                        );
                        dayTime = true;
                        npc[index].name = "Yellow Slime";
                        npc[index].scale = 1.2f;
                        npc[index].damage = 15;
                        npc[index].defense = 15;
                        npc[index].life = 50;
                        npc[index].lifeMax = npc[index].life;
                        npc[index].color = new Color(0xff, 200, 0, 100);
                    }
                }
                else if (mouseState.RightButton == ButtonState.Pressed)
                {
                    if (!tile[i, j].active)
                    {
                        WorldGen.PlaceTile(i, j, 4, false);
                    }
                }
                else if (mouseState.LeftButton == ButtonState.Pressed) { }
            }
        }

        private static void UpdateTime()
        {
            time++;
            if (!dayTime)
            {
                if (time > 30000.0)
                {
                    time = 0.0;
                    dayTime = true;
                    moonPhase++;
                    if (moonPhase >= 8)
                    {
                        moonPhase = 0;
                    }
                }
            }
            else if (time > 40000.0)
            {
                time = 0.0;
                dayTime = false;
            }
        }
    }
}
