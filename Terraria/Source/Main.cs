using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Terraria;

public class Main : Game
{
    public const double DayLength = 40000.0;
    public const double NightLength = 30000.0;
    public const float BottomWorld = 40000f;
    public const float LeftWorld = 0f;
    public const float RightWorld = 80000f;
    public const float TopWorld = 0f;
    public const int MaxBackgrounds = 3;
    public const int MaxDust = 0x3e8;
    public const int MaxInventory = 40;
    public const int MaxItems = 0x3e8;
    public const int MaxItemSounds = 2;
    public const int MaxItemTypes = 0x1b;
    public const int MaxNpcHitSounds = 1;
    public const int MaxNpcKilledSounds = 1;
    public const int MaxNpCs = 0x3e8;
    public const int MaxNpcTypes = 2;
    public const int MaxPlayers = 0x10;
    public const int MaxTileSets = 12;
    public const int MaxTilesX = 0x1389;
    public const int MaxTilesY = 0x9c5;
    public const int MaxWallTypes = 2;
    private static readonly bool DebugMode = false;
    private static readonly int Background = 0;
    private static readonly int[] BackgroundHeight = new int[3];
    private static readonly int[] BackgroundWidth = new int[3];
    private static readonly Texture2D[] BackgroundTexture = new Texture2D[3];
    private static SpriteFont fontDeathText;
    private static SpriteFont fontItemStack;
    private static SpriteFont fontMouseText;
    private static Texture2D blackTileTexture;
    private static Texture2D cursorTexture;
    private static Texture2D dustTexture;
    private static Texture2D heartTexture;
    public static bool dayTime = true;
    public static int focusRecipe;
    public static readonly bool DumbAi = false;
    public static readonly bool GodMode = false;
    public static readonly Dust[] Dust = new Dust[0x3e8];
    public static readonly float[] AvailableRecipeY = new float[Terraria.Recipe.MaxRecipes];
    public static readonly int[] AvailableRecipe = new int[Terraria.Recipe.MaxRecipes];

    private static readonly float[] HotbarScale =
    [
        1f,
        0.75f,
        0.75f,
        0.75f,
        0.75f,
        0.75f,
        0.75f,
        0.75f,
        0.75f,
        0.75f
    ];

    private static Texture2D hotbarTexture;
    private static Texture2D inventoryBackTexture;
    private static float inventoryScale = 0.75f;
    public static readonly Item[] Item = new Item[0x3e8];
    public static readonly Texture2D[] ItemTexture = new Texture2D[0x1b];
    public static KeyboardState keyState = Keyboard.GetState();
    private static int moonPhase;
    private static Texture2D moonTexture;
    public static Item mouseItem = new Item();
    public static bool mouseLeftRelease;
    public static MouseState mouseState = Mouse.GetState();
    private static byte mouseTextColor;
    private static int mouseTextColorChange = 1;
    public static readonly int MyPlayer = 0;
    public static readonly Npc[] Npc = new Npc[0x3e9];
    public static readonly int[] NpcFrameCount = [1, 2];
    public static readonly Texture2D[] NpcTexture = new Texture2D[2];
    public static int numAvailableRecipes;
    public static MouseState oldMouseState = Mouse.GetState();
    public static readonly Player[] Player = new Player[0x10];
    private static Texture2D playerBodyTexture;
    private static Texture2D playerHeadTexture;
    public static bool playerInventory = false;
    private static Texture2D playerLegTexture;
    public static readonly Random Rand = new Random();
    public static readonly Recipe[] Recipe = new Recipe[Terraria.Recipe.MaxRecipes];
    public static readonly int ScreenHeight = 600;
    public static Vector2 screenPosition;
    public static readonly int ScreenWidth = 800;
    public static readonly SoundEffect[] SoundDig = new SoundEffect[3];
    public static SoundEffect soundDoorClosed;
    public static SoundEffect soundDoorOpen;
    public static SoundEffect soundGrab;
    public static SoundEffect soundGrass;
    public static readonly SoundEffectInstance[] SoundInstanceDig = new SoundEffectInstance[3];
    public static SoundEffectInstance soundInstanceDoorClosed;
    public static SoundEffectInstance soundInstanceDoorOpen;
    public static SoundEffectInstance soundInstanceGrab;
    public static SoundEffectInstance soundInstanceGrass;
    public static readonly SoundEffectInstance[] SoundInstanceItem = new SoundEffectInstance[3];
    public static SoundEffectInstance soundInstanceMenuTick;
    public static readonly SoundEffectInstance[] SoundInstanceNpcHit = new SoundEffectInstance[2];
    public static readonly SoundEffectInstance[] SoundInstanceNpcKilled = new SoundEffectInstance[2];
    public static readonly SoundEffectInstance[] SoundInstancePlayerHit = new SoundEffectInstance[3];
    public static SoundEffectInstance soundInstancePlayerKilled;
    private static readonly SoundEffect[] SoundItem = new SoundEffect[3];
    public static SoundEffect soundMenuClose;
    public static SoundEffect soundMenuOpen;
    private static SoundEffect soundMenuTick;
    private static readonly SoundEffect[] SoundNpcHit = new SoundEffect[2];
    private static readonly SoundEffect[] SoundNpcKilled = new SoundEffect[2];
    public static readonly SoundEffect[] SoundPlayerHit = new SoundEffect[3];
    public static SoundEffect soundPlayerKilled;
    public static int spawnTileX;
    public static int spawnTileY;
    private static Texture2D sunTexture;
    public static readonly Tile[,] Tile = new Tile[0x1389, 0x9c5];
    public static Color tileColor;
    public static readonly bool[] TileNoFail = new bool[12];
    public static readonly bool[] TileSolid = new bool[12];
    private static readonly Texture2D[] TileTexture = new Texture2D[12];
    private static double time = 10000.0;
    private static Texture2D treeBranchTexture;
    private static Texture2D treeTopTexture;
    private static readonly Texture2D[] WallTexture = new Texture2D[2];
    public static double worldSurface;
    private readonly GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private bool _toggleFullscreen;

    public Main()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
    }

    public static double CalculateDamage(int damage, int defense)
    {
        return damage / (defense * 0.1);
    }

    protected override void Draw(GameTime gameTime)
    {
        Player[MyPlayer].MouseInterface = false;
        if (IsActive)
        {
            int num20;
            int num27;
            float num45;
            string name;
            Vector2 vector;
            Color color15;
            object obj2;
            var flag = false;
            if (!DebugMode)
            {
                var x = mouseState.X;
                var screenHeight = mouseState.Y;
                if (x < 0)
                    x = 0;

                if (x > ScreenWidth)
                {
                }

                if (screenHeight < 0) screenHeight = 0;
                if (screenHeight > ScreenHeight)
                {
                }

                screenPosition.X =
                    Player[MyPlayer].Position.X + Player[MyPlayer].Width * 0.5f
                    - ScreenWidth * 0.5f;

                screenPosition.Y =
                    Player[MyPlayer].Position.Y + Player[MyPlayer].Height * 0.5f
                    - ScreenHeight * 0.5f;
            }

            screenPosition.X = (int)screenPosition.X;
            screenPosition.Y = (int)screenPosition.Y;
            if (screenPosition.X < 0f)
                screenPosition.X = 0f;
            else if (screenPosition.X + ScreenWidth > 80000f)
                screenPosition.X = 80000f - ScreenWidth;

            if (screenPosition.Y < 0f)
                screenPosition.Y = 0f;
            else if (screenPosition.Y + ScreenHeight > 40000f)
                screenPosition.Y = 40000f - ScreenHeight;

            GraphicsDevice.Clear(Color.Black);
            base.Draw(gameTime);
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            var num3 = 0.5;
            var num4 =
                (int)
                -Math.IEEERemainder(
                    screenPosition.X * num3,
                    BackgroundWidth[Background]
                ) - BackgroundWidth[Background] / 2;

            var num5 = ScreenWidth / BackgroundWidth[Background] + 2;
            var y = (int)(
                -screenPosition.Y / (worldSurface * 16.0 - ScreenHeight)
                * (BackgroundHeight[Background] - ScreenHeight)
            );

            Color white = Color.White;
            var num9 =
                (int)(time / 40000.0 * (ScreenWidth + sunTexture.Width * 2))
                - sunTexture.Width;

            var num10 = 0;
            Color color = Color.White;
            var scale = 1f;
            var rotation = (float)(time / 40000.0) * 2f - 7.3f;
            var num14 =
                (int)(time / 30000.0 * (ScreenWidth + moonTexture.Width * 2))
                - moonTexture.Width;

            var num15 = 0;
            Color color3 = Color.White;
            var num16 = 1f;
            var num17 = (float)(time / 30000.0) * 2f - 7.3f;
            if (dayTime)
            {
                double num13;
                if (time < 20000.0)
                {
                    num13 = Math.Pow(1.0 - time / 40000.0 * 2.0, 2.0);
                    num10 = (int)(y + num13 * 250.0 + 180.0);
                }
                else
                {
                    num13 = Math.Pow((time / 40000.0 - 0.5) * 2.0, 2.0);
                    num10 = (int)(y + num13 * 250.0 + 180.0);
                }

                scale = (float)(1.2 - num13 * 0.4);
            }
            else
            {
                double num18;
                if (time < 15000.0)
                {
                    num18 = Math.Pow(1.0 - time / 30000.0 * 2.0, 2.0);
                    num15 = (int)(y + num18 * 250.0 + 180.0);
                }
                else
                {
                    num18 = Math.Pow((time / 30000.0 - 0.5) * 2.0, 2.0);
                    num15 = (int)(y + num18 * 250.0 + 180.0);
                }

                num16 = (float)(1.2 - num18 * 0.4);
            }

            float num19;
            if (dayTime)
            {
                if (time < 10000.0)
                {
                    num19 = (float)(time / 10000.0);
                    color.R = (byte)(num19 * 200f + 55f);
                    color.G = (byte)(num19 * 180f + 75f);
                    color.B = (byte)(num19 * 250f + 5f);
                    white.R = (byte)(num19 * 200f + 55f);
                    white.G = (byte)(num19 * 200f + 55f);
                    white.B = (byte)(num19 * 200f + 55f);
                }

                if (time > 34000.0)
                {
                    num19 = (float)(1.0 - (time / 40000.0 - 0.85) * 6.666666666666667);
                    color.R = (byte)(num19 * 120f + 55f);
                    color.G = (byte)(num19 * 100f + 25f);
                    color.B = (byte)(num19 * 120f + 55f);
                    white.R = (byte)(num19 * 200f + 55f);
                    white.G = (byte)(num19 * 85f + 55f);
                    white.B = (byte)(num19 * 135f + 55f);
                }
                else if (time > 28000.0)
                {
                    num19 = (float)(1.0 - (time / 40000.0 - 0.7) * 6.666666666666667);
                    color.R = (byte)(num19 * 80f + 175f);
                    color.G = (byte)(num19 * 130f + 125f);
                    color.B = (byte)(num19 * 100f + 155f);
                    white.R = (byte)(num19 * 0f + 255f);
                    white.G = (byte)(num19 * 115f + 140f);
                    white.B = (byte)(num19 * 75f + 180f);
                }
            }

            if (!dayTime)
            {
                if (time < 15000.0)
                {
                    num19 = (float)(1.0 - time / 15000.0);
                    color3.R = (byte)(num19 * 10f + 205f);
                    color3.G = (byte)(num19 * 70f + 155f);
                    color3.B = (byte)(num19 * 100f + 155f);
                    white.R = (byte)(num19 * 40f + 15f);
                    white.G = (byte)(num19 * 40f + 15f);
                    white.B = (byte)(num19 * 40f + 15f);
                }
                else if (time >= 15000.0)
                {
                    num19 = (float)((time / 30000.0 - 0.5) * 2.0);
                    color3.R = (byte)(num19 * 50f + 205f);
                    color3.G = (byte)(num19 * 100f + 155f);
                    color3.B = (byte)(num19 * 100f + 155f);
                    white.R = (byte)(num19 * 40f + 15f);
                    white.G = (byte)(num19 * 40f + 15f);
                    white.B = (byte)(num19 * 40f + 15f);
                }
            }

            tileColor.A = 0xff;
            tileColor.R = (byte)((white.R + white.B + white.G) / 3);
            tileColor.G = (byte)((white.R + white.B + white.G) / 3);
            tileColor.B = (byte)((white.R + white.B + white.G) / 3);
            for (num20 = 0; num20 < num5; num20++)
                _spriteBatch.Draw(
                    BackgroundTexture[Background],
                    new Rectangle(
                        num4 + BackgroundWidth[Background] * num20,
                        y,
                        BackgroundWidth[Background],
                        BackgroundHeight[Background]
                    ),
                    white
                );

            if (dayTime)
            {
                _spriteBatch.Draw(
                    sunTexture,
                    new Vector2(num9, num10),
                    new Rectangle(0, 0, sunTexture.Width, sunTexture.Height),
                    color,
                    rotation,
                    new Vector2(sunTexture.Width / 2, sunTexture.Height / 2),
                    scale,
                    SpriteEffects.None,
                    0f
                );
            }

            if (!dayTime)
            {
                _spriteBatch.Draw(
                    moonTexture,
                    new Vector2(num14, num15),
                    new Rectangle(
                        0,
                        moonTexture.Width * moonPhase,
                        moonTexture.Width,
                        moonTexture.Width
                    ),
                    color3,
                    num17,
                    new Vector2(moonTexture.Width / 2, moonTexture.Width / 2),
                    num16,
                    SpriteEffects.None,
                    0f
                );
            }

            var firstX = (int)(screenPosition.X / 16f - 1f);
            var lastX = (int)((screenPosition.X + ScreenWidth) / 16f) + 2;
            var firstY = (int)(screenPosition.Y / 16f - 1f);
            var lastY = (int)((screenPosition.Y + ScreenHeight) / 16f) + 2;
            if (firstX < 0)
                firstX = 0;

            if (lastX > 0x1389)
                lastX = 0x1389;

            if (firstY < 0)
                firstY = 0;

            if (lastY > 0x9c5)
                lastY = 0x9c5;

            Lighting.LightTiles(firstX, lastX, firstY, lastY);
            Color color4 = Color.White;
            num3 = 1.0;
            num4 =
                (int)-Math.IEEERemainder(screenPosition.X * num3, BackgroundWidth[1])
                - BackgroundWidth[1] / 2;

            num5 = ScreenWidth / BackgroundWidth[1] + 2;
            y = (int)(
                (int)worldSurface * 0x10 - BackgroundHeight[1] - screenPosition.Y + 16f
            );

            for (num20 = 0; num20 < num5; num20++)
            {
                num27 = 0;
                while (num27 < 6)
                {
                    var num28 = (int)(
                        (
                            num4 + BackgroundWidth[1] * num20 + screenPosition.X
                            + num27 * 0x10
                        ) / 16f - firstX + 21f
                    );

                    var num29 = (int)((y + screenPosition.Y) / 16f - firstY + 21f);
                    if (num28 < 0)
                        num28 = 0;

                    if (num28 >= ScreenWidth / 0x10 + 0x2a + 10)
                        num28 = ScreenWidth / 0x10 + 0x2a + 10 - 1;

                    if (num29 < 0)
                        num29 = 0;

                    if (num29 >= ScreenHeight / 0x10 + 0x2a + 10)
                        num29 = ScreenHeight / 0x10 + 0x2a + 10 - 1;

                    Color color5 = Lighting.Color[num28, num29];
                    _spriteBatch.Draw(
                        BackgroundTexture[1],
                        new Vector2(
                            num4 + BackgroundWidth[1] * num20 + 0x10 * num27,
                            y
                        ),
                        new Rectangle(0x10 * num27, 0, 0x10, 0x10),
                        color5
                    );

                    num27++;
                }
            }

            y = (int)((int)worldSurface * 0x10 - screenPosition.Y + 16f);
            if (worldSurface * 16.0 <= screenPosition.Y + ScreenHeight)
            {
                num3 = 1.0;
                num4 =
                    (int)
                    -Math.IEEERemainder(
                        100.0 + screenPosition.X * num3,
                        BackgroundWidth[2]
                    ) - BackgroundWidth[2] / 2;

                num5 = ScreenWidth / BackgroundWidth[2] + 2;
                int num7;
                int num6;
                if (worldSurface * 16.0 < screenPosition.Y)
                {
                    num6 =
                        (int)Math.IEEERemainder(y, BackgroundHeight[2])
                        - BackgroundHeight[2];

                    num7 = ScreenHeight / BackgroundHeight[2] + 2;
                }
                else
                {
                    num6 = y;
                    num7 = (ScreenHeight - y) / BackgroundHeight[2] + 1;
                }

                num20 = 0;
                while (num20 < num5)
                {
                    for (num27 = 0; num27 < num7; num27++)
                        _spriteBatch.Draw(
                            BackgroundTexture[2],
                            new Rectangle(
                                num4 + BackgroundWidth[2] * num20,
                                num6 + BackgroundHeight[2] * num27,
                                BackgroundWidth[2],
                                BackgroundHeight[2]
                            ),
                            Color.White
                        );

                    num20++;
                }
            }

            num27 = firstY;
            while (num27 < lastY + 4)
            {
                num20 = firstX - 2;
                while (num20 < lastX + 2)
                {
                    if (
                        Lighting.Color[num20 - firstX + 0x15, num27 - firstY + 0x15].R
                        < tileColor.R - 10 || num27 > worldSurface
                    )
                    {
                        var num30 =
                            0xff
                            - Lighting
                                .Color[num20 - firstX + 0x15, num27 - firstY + 0x15]
                                .R;

                        if (num30 < 0)
                            num30 = 0;

                        if (num30 > 0xff)
                            num30 = 0xff;

                        color4.A = (byte)num30;
                        vector = new Vector2();
                        _spriteBatch.Draw(
                            blackTileTexture,
                            new Vector2(
                                num20 * 0x10 - (int)screenPosition.X,
                                num27 * 0x10 - (int)screenPosition.Y
                            ),
                            new Rectangle(
                                Tile[num20, num27].FrameX,
                                Tile[num20, num27].FrameY,
                                0x10,
                                0x10
                            ),
                            color4,
                            0f,
                            vector,
                            1f,
                            SpriteEffects.None,
                            0f
                        );
                    }

                    if (Tile[num20, num27].Wall > 0)
                    {
                        vector = new Vector2();
                        _spriteBatch.Draw(
                            WallTexture[Tile[num20, num27].Wall],
                            new Vector2(
                                num20 * 0x10 - (int)screenPosition.X - 8,
                                num27 * 0x10 - (int)screenPosition.Y - 8
                            ),
                            new Rectangle(
                                Tile[num20, num27].WallFrameX * 2,
                                Tile[num20, num27].WallFrameY * 2,
                                0x20,
                                0x20
                            ),
                            Lighting.Color[num20 - firstX + 0x15, num27 - firstY + 0x15],
                            0f,
                            vector,
                            1f,
                            SpriteEffects.None,
                            0f
                        );
                    }

                    num20++;
                }

                num27++;
            }

            for (num27 = firstY; num27 < lastY + 4; num27++)
            {
                num20 = firstX - 2;
                while (num20 < lastX + 2)
                {
                    if (Tile[num20, num27].Active)
                    {
                        int height;
                        if (
                            Tile[num20, num27].Type == 3 || Tile[num20, num27].Type == 4
                                                         || Tile[num20, num27].Type == 5
                        )
                            height = 20;
                        else
                            height = 0x10;

                        int width;
                        if (Tile[num20, num27].Type == 4 || Tile[num20, num27].Type == 5)
                            width = 20;
                        else
                            width = 0x10;

                        if (Tile[num20, num27].Type == 4 && Rand.Next(40) == 0)
                        {
                            if (Tile[num20, num27].FrameX == 0x16)
                            {
                                color15 = new Color();
                                Terraria.Dust.NewDust(
                                    new Vector2(
                                        num20 * 0x10 + 6,
                                        num27 * 0x10
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

                            if (Tile[num20, num27].FrameX == 0x2c)
                            {
                                color15 = new Color();
                                Terraria.Dust.NewDust(
                                    new Vector2(
                                        num20 * 0x10 + 2,
                                        num27 * 0x10
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
                                Terraria.Dust.NewDust(
                                    new Vector2(
                                        num20 * 0x10 + 4,
                                        num27 * 0x10
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
                            Tile[num20, num27].Type == 5
                            && Tile[num20, num27].FrameY >= 0xc6 && Tile[num20, num27].FrameX >= 0x16
                        )
                        {
                            var num31 = 0;
                            if (Tile[num20, num27].FrameX == 0x16)
                            {
                                if (Tile[num20, num27].FrameY == 220)
                                    num31 = 1;
                                else if (Tile[num20, num27].FrameY == 0xf2)
                                    num31 = 2;

                                vector = new Vector2();
                                _spriteBatch.Draw(
                                    treeTopTexture,
                                    new Vector2(
                                        num20 * 0x10 - (int)screenPosition.X - 0x20,
                                        num27 * 0x10 - (int)screenPosition.Y - 0x40
                                    ),
                                    new Rectangle(num31 * 0x52, 0, 80, 80),
                                    Lighting.Color[
                                        num20 - firstX + 0x15,
                                        num27 - firstY + 0x15
                                    ],
                                    0f,
                                    vector,
                                    1f,
                                    SpriteEffects.None,
                                    0f
                                );
                            }
                            else if (Tile[num20, num27].FrameX == 0x2c)
                            {
                                if (Tile[num20, num27].FrameY == 220)
                                    num31 = 1;
                                else if (Tile[num20, num27].FrameY == 0xf2)
                                    num31 = 2;

                                vector = new Vector2();
                                _spriteBatch.Draw(
                                    treeBranchTexture,
                                    new Vector2(
                                        num20 * 0x10 - (int)screenPosition.X - 0x18,
                                        num27 * 0x10 - (int)screenPosition.Y - 12
                                    ),
                                    new Rectangle(0, num31 * 0x2a, 40, 40),
                                    Lighting.Color[
                                        num20 - firstX + 0x15,
                                        num27 - firstY + 0x15
                                    ],
                                    0f,
                                    vector,
                                    1f,
                                    SpriteEffects.None,
                                    0f
                                );
                            }
                            else if (Tile[num20, num27].FrameX == 0x42)
                            {
                                if (Tile[num20, num27].FrameY == 220)
                                    num31 = 1;
                                else if (Tile[num20, num27].FrameY == 0xf2)
                                    num31 = 2;

                                vector = new Vector2();
                                _spriteBatch.Draw(
                                    treeBranchTexture,
                                    new Vector2(
                                        num20 * 0x10 - (int)screenPosition.X,
                                        num27 * 0x10 - (int)screenPosition.Y - 12
                                    ),
                                    new Rectangle(0x2a, num31 * 0x2a, 40, 40),
                                    Lighting.Color[
                                        num20 - firstX + 0x15,
                                        num27 - firstY + 0x15
                                    ],
                                    0f,
                                    vector,
                                    1f,
                                    SpriteEffects.None,
                                    0f
                                );
                            }
                        }

                        vector = new Vector2();
                        _spriteBatch.Draw(
                            TileTexture[Tile[num20, num27].Type],
                            new Vector2(
                                num20 * 0x10 - (int)screenPosition.X
                                             - (width - 16f) / 2f,
                                num27 * 0x10 - (int)screenPosition.Y
                            ),
                            new Rectangle(
                                Tile[num20, num27].FrameX,
                                Tile[num20, num27].FrameY,
                                width,
                                height
                            ),
                            Lighting.Color[num20 - firstX + 0x15, num27 - firstY + 0x15],
                            0f,
                            vector,
                            1f,
                            SpriteEffects.None,
                            0f
                        );
                    }

                    num20++;
                }
            }

            for (num20 = 0; num20 < 0x10; num20++)
                if (Player[num20].Active)
                {
                    SpriteEffects none = Player[num20].Direction == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

                    Color newColor = Lighting.Color[
                        Lighting.LightingX(
                            (int)(
                                Player[num20].Position.X
                                + Player[num20].Width * 0.5
                            ) / 0x10 - firstX + 0x15
                        ),
                        Lighting.LightingY(
                            (int)(
                                Player[num20].Position.Y
                                + Player[num20].Height * 0.25
                            ) / 0x10 - firstY + 0x15
                        )
                    ];

                    _spriteBatch.Draw(
                        playerHeadTexture,
                        new Vector2(
                            (int)(Player[num20].Position.X - screenPosition.X)
                            - Player[num20].HeadFrame.Width / 2 + Player[num20].Width / 2,
                            (int)(
                                Player[num20].Position.Y - screenPosition.Y
                                + Player[num20].Height - Player[num20].HeadFrame.Height + 2f
                            )
                        ) + Player[num20].HeadPosition + new Vector2(16f, 14f),
                        Player[num20].HeadFrame,
                        Player[num20].GetImmuneAlpha(newColor),
                        Player[num20].HeadRotation,
                        new Vector2(16f, 14f),
                        1f,
                        none,
                        0f
                    );

                    newColor = Lighting.Color[
                        Lighting.LightingX(
                            (int)(
                                Player[num20].Position.X
                                + Player[num20].Width * 0.5
                            ) / 0x10 - firstX + 0x15
                        ),
                        Lighting.LightingY(
                            (int)(
                                Player[num20].Position.Y
                                + Player[num20].Height * 0.5
                            ) / 0x10 - firstY + 0x15
                        )
                    ];

                    if (
                        (
                            Player[num20].ItemAnimation > 0
                            || Player[num20].Inventory[Player[num20].SelectedItem].HoldStyle
                            > 0
                        ) && Player[num20].Inventory[Player[num20].SelectedItem].Type > 0
                    )
                    {
                        _spriteBatch.Draw(
                            ItemTexture[
                                Player[num20].Inventory[Player[num20].SelectedItem].Type
                            ],
                            new Vector2(
                                (int)(Player[num20].ItemLocation.X - screenPosition.X),
                                (int)(Player[num20].ItemLocation.Y - screenPosition.Y)
                            ),
                            new Rectangle(
                                0,
                                0,
                                ItemTexture[
                                    Player[num20].Inventory[Player[num20].SelectedItem].Type
                                ].Width,
                                ItemTexture[
                                    Player[num20].Inventory[Player[num20].SelectedItem].Type
                                ].Height
                            ),
                            Player[num20]
                                .Inventory[Player[num20].SelectedItem]
                                .GetAlpha(newColor),
                            Player[num20].ItemRotation,
                            new Vector2(
                                ItemTexture[
                                    Player[num20].Inventory[Player[num20].SelectedItem].Type
                                ].Width * 0.5f
                                - ItemTexture[
                                    Player[num20]
                                        .Inventory[Player[num20].SelectedItem]
                                        .Type
                                ].Width * 0.5f * Player[num20].Direction,
                                ItemTexture[
                                    Player[num20].Inventory[Player[num20].SelectedItem].Type
                                ].Height
                            ),
                            Player[num20].Inventory[Player[num20].SelectedItem].Scale,
                            none,
                            0f
                        );

                        color15 = new Color();
                        if (
                            Player[num20].Inventory[Player[num20].SelectedItem].Color != color15
                        )
                        {
                            _spriteBatch.Draw(
                                ItemTexture[
                                    Player[num20].Inventory[Player[num20].SelectedItem].Type
                                ],
                                new Vector2(
                                    (int)(Player[num20].ItemLocation.X - screenPosition.X),
                                    (int)(Player[num20].ItemLocation.Y - screenPosition.Y)
                                ),
                                new Rectangle(
                                    0,
                                    0,
                                    ItemTexture[
                                        Player[num20].Inventory[Player[num20].SelectedItem].Type
                                    ].Width,
                                    ItemTexture[
                                        Player[num20].Inventory[Player[num20].SelectedItem].Type
                                    ].Height
                                ),
                                Player[num20]
                                    .Inventory[Player[num20].SelectedItem]
                                    .GetColor(newColor),
                                Player[num20].ItemRotation,
                                new Vector2(
                                    ItemTexture[
                                        Player[num20]
                                            .Inventory[Player[num20].SelectedItem]
                                            .Type
                                    ].Width * 0.5f
                                    - ItemTexture[
                                        Player[num20]
                                            .Inventory[Player[num20].SelectedItem]
                                            .Type
                                    ].Width * 0.5f * Player[num20].Direction,
                                    ItemTexture[
                                        Player[num20]
                                            .Inventory[Player[num20].SelectedItem]
                                            .Type
                                    ].Height
                                ),
                                Player[num20].Inventory[Player[num20].SelectedItem].Scale,
                                none,
                                0f
                            );
                        }
                    }

                    _spriteBatch.Draw(
                        playerBodyTexture,
                        new Vector2(
                            (int)(Player[num20].Position.X - screenPosition.X)
                            - Player[num20].BodyFrame.Width / 2 + Player[num20].Width / 2,
                            (int)(
                                Player[num20].Position.Y - screenPosition.Y
                                + Player[num20].Height - Player[num20].BodyFrame.Height + 2f
                            )
                        ) + Player[num20].BodyPosition + new Vector2(16f, 28f),
                        Player[num20].BodyFrame,
                        Player[num20].GetImmuneAlpha(newColor),
                        Player[num20].BodyRotation,
                        new Vector2(16f, 28f),
                        1f,
                        none,
                        0f
                    );

                    newColor = Lighting.Color[
                        Lighting.LightingX(
                            (int)(
                                Player[num20].Position.X
                                + Player[num20].Width * 0.5
                            ) / 0x10 - firstX + 0x15
                        ),
                        Lighting.LightingY(
                            (int)(
                                Player[num20].Position.Y
                                + Player[num20].Height * 0.75
                            ) / 0x10 - firstY + 0x15
                        )
                    ];

                    _spriteBatch.Draw(
                        playerLegTexture,
                        new Vector2(
                            (int)(Player[num20].Position.X - screenPosition.X)
                            - Player[num20].LegFrame.Width / 2 + Player[num20].Width / 2,
                            (int)(
                                Player[num20].Position.Y - screenPosition.Y
                                + Player[num20].Height - Player[num20].LegFrame.Height + 2f
                            )
                        ) + Player[num20].LegPosition + new Vector2(16f, 40f),
                        Player[num20].LegFrame,
                        Player[num20].GetImmuneAlpha(newColor),
                        Player[num20].LegRotation,
                        new Vector2(16f, 40f),
                        1f,
                        none,
                        0f
                    );
                }

            Rectangle rectangle = new Rectangle(
                (int)screenPosition.X,
                (int)screenPosition.Y,
                ScreenWidth,
                ScreenHeight
            );

            for (num20 = 0; num20 < 0x3e8; num20++)
                if (
                    rectangle.Intersects(
                        new Rectangle(
                            (int)Npc[num20].Position.X,
                            (int)Npc[num20].Position.Y,
                            Npc[num20].Width,
                            Npc[num20].Height
                        )
                    )
                )
                {
                    Color color7 = Lighting.Color[
                        (int)(Npc[num20].Position.X + Npc[num20].Width * 0.5) / 0x10
                        - firstX + 0x15,
                        (int)(Npc[num20].Position.Y + Npc[num20].Height * 0.5) / 0x10
                        - firstY + 0x15
                    ];

                    if (Npc[num20].Active && Npc[num20].Type > 0)
                    {
                        vector = new Vector2();
                        _spriteBatch.Draw(
                            NpcTexture[Npc[num20].Type],
                            new Vector2(
                                Npc[num20].Position.X - screenPosition.X
                                + Npc[num20].Width / 2
                                - NpcTexture[Npc[num20].Type].Width * Npc[num20].Scale
                                / 2f,
                                Npc[num20].Position.Y - screenPosition.Y
                                + Npc[num20].Height
                                - NpcTexture[Npc[num20].Type].Height * Npc[num20].Scale
                                / NpcFrameCount[Npc[num20].Type] + 4f
                            ),
                            Npc[num20].Frame,
                            Npc[num20].GetAlpha(color7),
                            0f,
                            vector,
                            Npc[num20].Scale,
                            SpriteEffects.None,
                            0f
                        );

                        color15 = new Color();
                        if (Npc[num20].Color != color15)
                        {
                            vector = new Vector2();
                            _spriteBatch.Draw(
                                NpcTexture[Npc[num20].Type],
                                new Vector2(
                                    Npc[num20].Position.X - screenPosition.X
                                    + Npc[num20].Width / 2
                                    - NpcTexture[Npc[num20].Type].Width
                                    * Npc[num20].Scale / 2f,
                                    Npc[num20].Position.Y - screenPosition.Y
                                    + Npc[num20].Height
                                    - NpcTexture[Npc[num20].Type].Height
                                    * Npc[num20].Scale / NpcFrameCount[Npc[num20].Type] + 4f
                                ),
                                Npc[num20].Frame,
                                Npc[num20].GetColor(color7),
                                0f,
                                vector,
                                Npc[num20].Scale,
                                SpriteEffects.None,
                                0f
                            );
                        }
                    }
                }

            for (num20 = 0; num20 < 0x3e8; num20++)
                if (Item[num20].Active && Item[num20].Type > 0)
                {
                    var lightX =
                        (int)(Item[num20].Position.X + Item[num20].Width * 0.5) / 0x10
                        - firstX + 0x15;

                    var lightY =
                        (int)(Item[num20].Position.Y + Item[num20].Height * 0.5)
                        / 0x10 - firstY + 0x15;

                    Color color8 = Lighting.Color[
                        Lighting.LightingX(lightX),
                        Lighting.LightingY(lightY)
                    ];

                    vector = new Vector2();
                    _spriteBatch.Draw(
                        ItemTexture[Item[num20].Type],
                        new Vector2(
                            Item[num20].Position.X - screenPosition.X
                            + Item[num20].Width / 2 - ItemTexture[Item[num20].Type].Width / 2,
                            Item[num20].Position.Y - screenPosition.Y
                            + Item[num20].Height / 2 - ItemTexture[Item[num20].Type].Height / 2
                        ),
                        new Rectangle(
                            0,
                            0,
                            ItemTexture[Item[num20].Type].Width,
                            ItemTexture[Item[num20].Type].Height
                        ),
                        Item[num20].GetAlpha(color8),
                        0f,
                        vector,
                        1f,
                        SpriteEffects.None,
                        0f
                    );

                    color15 = new Color();
                    if (Item[num20].Color != color15)
                    {
                        vector = new Vector2();
                        _spriteBatch.Draw(
                            ItemTexture[Item[num20].Type],
                            new Vector2(
                                Item[num20].Position.X - screenPosition.X
                                + Item[num20].Width / 2 - ItemTexture[Item[num20].Type].Width / 2,
                                Item[num20].Position.Y - screenPosition.Y
                                + Item[num20].Height / 2 - ItemTexture[Item[num20].Type].Height / 2
                            ),
                            new Rectangle(
                                0,
                                0,
                                ItemTexture[Item[num20].Type].Width,
                                ItemTexture[Item[num20].Type].Height
                            ),
                            Item[num20].GetColor(color8),
                            0f,
                            vector,
                            1f,
                            SpriteEffects.None,
                            0f
                        );
                    }
                }

            for (num20 = 0; num20 < 0x3e8; num20++)
                if (Dust[num20].Active)
                {
                    Color color9 = Lighting.Color[
                        Lighting.LightingX(
                            (int)(Dust[num20].Position.X + 4.0) / 0x10 - firstX + 0x15
                        ),
                        Lighting.LightingY(
                            (int)(Dust[num20].Position.Y + 4.0) / 0x10 - firstY + 0x15
                        )
                    ];

                    if (Dust[num20].Type == 6)
                        color9 = Color.White;

                    _spriteBatch.Draw(
                        dustTexture,
                        Dust[num20].Position - screenPosition,
                        Dust[num20].Frame,
                        Dust[num20].GetAlpha(color9),
                        Dust[num20].Rotation,
                        new Vector2(4f, 4f),
                        Dust[num20].Scale,
                        SpriteEffects.None,
                        0f
                    );

                    color15 = new Color();
                    if (Dust[num20].Color != color15)
                    {
                        _spriteBatch.Draw(
                            dustTexture,
                            Dust[num20].Position - screenPosition,
                            Dust[num20].Frame,
                            Dust[num20].GetColor(color9),
                            Dust[num20].Rotation,
                            new Vector2(4f, 4f),
                            Dust[num20].Scale,
                            SpriteEffects.None,
                            0f
                        );
                    }
                }

            var num34 = 20;
            num20 = 1;
            while (num20 < Player[MyPlayer].StatLifeMax / num34 + 1)
            {
                var num36 = 1f;
                int r;
                if (Player[MyPlayer].StatLife >= num20 * num34)
                    r = 0xff;
                else
                {
                    var num37 =
                        (Player[MyPlayer].StatLife - (num20 - 1) * num34)
                        / (float)num34;

                    r = (int)(30f + 225f * num37);
                    if (r < 30)
                        r = 30;

                    num36 = num37 / 4f + 0.75f;
                    if (num36 < 0.75)
                        num36 = 0.75f;
                }

                var num38 = 0;
                var num39 = 0;
                if (num20 > 10)
                {
                    num38 -= 260;
                    num39 += 0x1a;
                }

                vector = new Vector2();
                _spriteBatch.Draw(
                    heartTexture,
                    new Vector2(
                        500 + 0x1a * (num20 - 1) + num38,
                        32f + (heartTexture.Height - heartTexture.Height * num36) / 2f
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

            var text = "";
            if (playerInventory)
            {
                int num42;
                int num43;
                Color color10;
                Item item;
                double num46;
                inventoryScale = 0.75f;
                for (var i = 0; i < 10; i++)
                for (var j = 0; j < 4; j++)
                {
                    num42 = (int)(20f + i * 0x38 * inventoryScale);
                    num43 = (int)(20f + j * 0x38 * inventoryScale);
                    num20 = i + j * 10;
                    color10 = new Color(0xff, 0xff, 0xff, 0xff);
                    if (
                        mouseState.X >= num42
                        && mouseState.X
                        <= num42 + hotbarTexture.Width * inventoryScale && mouseState.Y >= num43
                        && mouseState.Y
                        <= num43 + hotbarTexture.Height * inventoryScale
                    )
                    {
                        Player[MyPlayer].MouseInterface = true;
                        if (
                            mouseLeftRelease
                            && mouseState.LeftButton == ButtonState.Pressed
                            && (
                                Player[MyPlayer].SelectedItem != num20
                                || Player[MyPlayer].ItemAnimation <= 0
                            )
                        )
                        {
                            item = mouseItem;
                            mouseItem = Player[MyPlayer].Inventory[num20];
                            Player[MyPlayer].Inventory[num20] = item;
                            if (
                                Player[MyPlayer].Inventory[num20].Type == 0
                                || Player[MyPlayer].Inventory[num20].Stack < 1
                            )
                                Player[MyPlayer].Inventory[num20] = new Item();

                            if (
                                mouseItem.IsTheSameAs(
                                    Player[MyPlayer].Inventory[num20]
                                )
                                && Player[MyPlayer].Inventory[num20].Stack
                                != Player[MyPlayer].Inventory[num20].MaxStack && mouseItem.Stack != mouseItem.MaxStack
                            )
                            {
                                if (
                                    mouseItem.Stack
                                    + Player[MyPlayer].Inventory[num20].Stack <= mouseItem.MaxStack
                                )
                                {
                                    Item item1 = Player[MyPlayer].Inventory[num20];
                                    item1.Stack += mouseItem.Stack;
                                    mouseItem.Stack = 0;
                                }
                                else
                                {
                                    var num44 =
                                        mouseItem.MaxStack
                                        - Player[MyPlayer].Inventory[num20].Stack;

                                    Item item2 = Player[MyPlayer].Inventory[num20];
                                    item2.Stack += num44;
                                    mouseItem.Stack -= num44;
                                }
                            }

                            if (mouseItem.Type == 0 || mouseItem.Stack < 1)
                                mouseItem = new Item();

                            if (
                                mouseItem.Type > 0
                                || Player[MyPlayer].Inventory[num20].Type > 0
                            )
                            {
                                Terraria.Recipe.FindRecipes();
                                soundInstanceGrab.Stop();
                                soundInstanceGrab = soundGrab.CreateInstance();
                                soundInstanceGrab.Play();
                            }
                        }

                        text = Player[MyPlayer].Inventory[num20].Name;
                        if (Player[MyPlayer].Inventory[num20].Stack > 1)
                        {
                            obj2 = text;
                            text = string.Concat(obj2, " (", Player[MyPlayer].Inventory[num20].Stack, ")");
                        }
                    }

                    vector = new Vector2();
                    _spriteBatch.Draw(
                        hotbarTexture,
                        new Vector2(num42, num43),
                        new Rectangle(0, 0, hotbarTexture.Width, hotbarTexture.Height),
                        color10,
                        0f,
                        vector,
                        inventoryScale,
                        SpriteEffects.None,
                        0f
                    );

                    vector = new Vector2();
                    _spriteBatch.Draw(
                        inventoryBackTexture,
                        new Vector2(num42, num43),
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
                        Player[MyPlayer].Inventory[num20].Type > 0
                        && Player[MyPlayer].Inventory[num20].Stack > 0
                    )
                    {
                        num45 = 1f;
                        if (
                            ItemTexture[Player[MyPlayer].Inventory[num20].Type].Width
                            > 0x20
                            || ItemTexture[Player[MyPlayer].Inventory[num20].Type].Height
                            > 0x20
                        )
                        {
                            if (
                                ItemTexture[Player[MyPlayer].Inventory[num20].Type].Width
                                > ItemTexture[Player[MyPlayer].Inventory[num20].Type].Height
                            )
                            {
                                num45 =
                                    32f
                                    / ItemTexture[
                                        Player[MyPlayer].Inventory[num20].Type
                                    ].Width;
                            }
                            else
                            {
                                num45 =
                                    32f
                                    / ItemTexture[
                                        Player[MyPlayer].Inventory[num20].Type
                                    ].Height;
                            }
                        }

                        num45 *= inventoryScale;
                        vector = new Vector2();
                        _spriteBatch.Draw(
                            ItemTexture[Player[MyPlayer].Inventory[num20].Type],
                            new Vector2(
                                num42 + 26f * inventoryScale
                                - ItemTexture[
                                    Player[MyPlayer].Inventory[num20].Type
                                ].Width * 0.5f * num45,
                                num43 + 26f * inventoryScale
                                - ItemTexture[
                                    Player[MyPlayer].Inventory[num20].Type
                                ].Height * 0.5f * num45
                            ),
                            new Rectangle(
                                0,
                                0,
                                ItemTexture[Player[MyPlayer].Inventory[num20].Type].Width,
                                ItemTexture[Player[MyPlayer].Inventory[num20].Type].Height
                            ),
                            Player[MyPlayer].Inventory[num20].GetAlpha(color10),
                            0f,
                            vector,
                            num45,
                            SpriteEffects.None,
                            0f
                        );

                        color15 = new Color();
                        if (Player[MyPlayer].Inventory[num20].Color != color15)
                        {
                            vector = new Vector2();
                            _spriteBatch.Draw(
                                ItemTexture[Player[MyPlayer].Inventory[num20].Type],
                                new Vector2(
                                    num42 + 26f * inventoryScale
                                    - ItemTexture[
                                        Player[MyPlayer].Inventory[num20].Type
                                    ].Width * 0.5f * num45,
                                    num43 + 26f * inventoryScale
                                    - ItemTexture[
                                        Player[MyPlayer].Inventory[num20].Type
                                    ].Height * 0.5f * num45
                                ),
                                new Rectangle(
                                    0,
                                    0,
                                    ItemTexture[
                                        Player[MyPlayer].Inventory[num20].Type
                                    ].Width,
                                    ItemTexture[
                                        Player[MyPlayer].Inventory[num20].Type
                                    ].Height
                                ),
                                Player[MyPlayer].Inventory[num20].GetColor(color10),
                                0f,
                                vector,
                                num45,
                                SpriteEffects.None,
                                0f
                            );
                        }

                        if (Player[MyPlayer].Inventory[num20].Stack > 1)
                        {
                            vector = new Vector2();
                            _spriteBatch.DrawString(
                                fontItemStack,
                                string.Concat(Player[MyPlayer].Inventory[num20].Stack),
                                new Vector2(
                                    num42 + 10f * inventoryScale,
                                    num43 + 26f * inventoryScale
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

                for (num20 = 0; num20 < 3; num20++)
                {
                    num42 = 330;
                    num43 = (int)(238f + num20 * 0x38 * inventoryScale);
                    color10 = new Color(0xff, 0xff, 0xff, 0xff);
                    if (
                        mouseState.X >= num42
                        && mouseState.X
                        <= num42 + hotbarTexture.Width * inventoryScale && mouseState.Y >= num43 &&
                        mouseState.Y <= num43 + hotbarTexture.Height * inventoryScale
                    )
                    {
                        Player[MyPlayer].MouseInterface = true;
                        if (
                            mouseLeftRelease && mouseState.LeftButton == ButtonState.Pressed
                                             && (
                                                 mouseItem.Type == 0
                                                 || (mouseItem.HeadSlot > 0 && num20 == 0) ||
                                                 (mouseItem.BodySlot > 0 && num20 == 1) ||
                                                 (mouseItem.LegSlot > 0 && num20 == 2)
                                             )
                        )
                        {
                            item = mouseItem;
                            mouseItem = Player[MyPlayer].Armor[num20];
                            Player[MyPlayer].Armor[num20] = item;
                            if (
                                Player[MyPlayer].Armor[num20].Type == 0
                                || Player[MyPlayer].Armor[num20].Stack < 1
                            )
                                Player[MyPlayer].Armor[num20] = new Item();

                            if (mouseItem.Type == 0 || mouseItem.Stack < 1)
                                mouseItem = new Item();

                            if (
                                mouseItem.Type > 0
                                || Player[MyPlayer].Armor[num20].Type > 0
                            )
                            {
                                Terraria.Recipe.FindRecipes();
                                soundInstanceGrab.Stop();
                                soundInstanceGrab = soundGrab.CreateInstance();
                                soundInstanceGrab.Play();
                            }
                        }

                        text = Player[MyPlayer].Armor[num20].Name;
                        if (Player[MyPlayer].Armor[num20].Stack > 1)
                        {
                            obj2 = text;
                            text = string.Concat(obj2, " (", Player[MyPlayer].Armor[num20].Stack, ")");
                        }
                    }

                    vector = new Vector2();
                    _spriteBatch.Draw(
                        hotbarTexture,
                        new Vector2(num42, num43),
                        new Rectangle(0, 0, hotbarTexture.Width, hotbarTexture.Height),
                        color10,
                        0f,
                        vector,
                        inventoryScale,
                        SpriteEffects.None,
                        0f
                    );

                    vector = new Vector2();
                    _spriteBatch.Draw(
                        inventoryBackTexture,
                        new Vector2(num42, num43),
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
                        Player[MyPlayer].Armor[num20].Type > 0
                        && Player[MyPlayer].Armor[num20].Stack > 0
                    )
                    {
                        num45 = 1f;
                        if (
                            ItemTexture[Player[MyPlayer].Armor[num20].Type].Width > 0x20
                            || ItemTexture[Player[MyPlayer].Armor[num20].Type].Height > 0x20
                        )
                        {
                            if (
                                ItemTexture[Player[MyPlayer].Armor[num20].Type].Width
                                > ItemTexture[Player[MyPlayer].Armor[num20].Type].Height
                            )
                            {
                                num45 =
                                    32f
                                    / ItemTexture[
                                        Player[MyPlayer].Armor[num20].Type
                                    ].Width;
                            }
                            else
                            {
                                num45 =
                                    32f
                                    / ItemTexture[
                                        Player[MyPlayer].Armor[num20].Type
                                    ].Height;
                            }
                        }

                        num45 *= inventoryScale;
                        vector = new Vector2();
                        _spriteBatch.Draw(
                            ItemTexture[Player[MyPlayer].Armor[num20].Type],
                            new Vector2(
                                num42 + 26f * inventoryScale
                                - ItemTexture[
                                    Player[MyPlayer].Armor[num20].Type
                                ].Width * 0.5f * num45,
                                num43 + 26f * inventoryScale
                                - ItemTexture[
                                    Player[MyPlayer].Armor[num20].Type
                                ].Height * 0.5f * num45
                            ),
                            new Rectangle(
                                0,
                                0,
                                ItemTexture[Player[MyPlayer].Armor[num20].Type].Width,
                                ItemTexture[Player[MyPlayer].Armor[num20].Type].Height
                            ),
                            Player[MyPlayer].Armor[num20].GetAlpha(color10),
                            0f,
                            vector,
                            num45,
                            SpriteEffects.None,
                            0f
                        );

                        color15 = new Color();
                        if (Player[MyPlayer].Armor[num20].Color != color15)
                        {
                            vector = new Vector2();
                            _spriteBatch.Draw(
                                ItemTexture[Player[MyPlayer].Armor[num20].Type],
                                new Vector2(
                                    num42 + 26f * inventoryScale
                                    - ItemTexture[
                                        Player[MyPlayer].Armor[num20].Type
                                    ].Width * 0.5f * num45,
                                    num43 + 26f * inventoryScale
                                    - ItemTexture[
                                        Player[MyPlayer].Armor[num20].Type
                                    ].Height * 0.5f * num45
                                ),
                                new Rectangle(
                                    0,
                                    0,
                                    ItemTexture[Player[MyPlayer].Armor[num20].Type].Width,
                                    ItemTexture[Player[MyPlayer].Armor[num20].Type].Height
                                ),
                                Player[MyPlayer].Armor[num20].GetColor(color10),
                                0f,
                                vector,
                                num45,
                                SpriteEffects.None,
                                0f
                            );
                        }

                        if (Player[MyPlayer].Armor[num20].Stack > 1)
                        {
                            vector = new Vector2();
                            _spriteBatch.DrawString(
                                fontItemStack,
                                string.Concat(Player[MyPlayer].Inventory[num20].Stack),
                                new Vector2(
                                    num42 + 10f * inventoryScale,
                                    num43 + 26f * inventoryScale
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

                for (num20 = 0; num20 < Terraria.Recipe.MaxRecipes; num20++)
                {
                    inventoryScale = 100f / (Math.Abs(AvailableRecipeY[num20]) + 100f);
                    if (inventoryScale < 0.75)
                        inventoryScale = 0.75f;

                    if (AvailableRecipeY[num20] < (num20 - focusRecipe) * 0x41)
                    {
                        if (AvailableRecipeY[num20] == 0f)
                            soundInstanceMenuTick.Play();

                        AvailableRecipeY[num20] += 6.5f;
                    }
                    else if (AvailableRecipeY[num20] > (num20 - focusRecipe) * 0x41)
                    {
                        if (AvailableRecipeY[num20] == 0f)
                            soundInstanceMenuTick.Play();

                        AvailableRecipeY[num20] -= 6.5f;
                    }

                    if (
                        num20 < numAvailableRecipes
                        && Math.Abs(AvailableRecipeY[num20]) <= 250f
                    )
                    {
                        num42 = (int)(46f - 26f * inventoryScale);
                        num43 = (int)(
                            400f + AvailableRecipeY[num20] * inventoryScale
                            - 30f * inventoryScale
                        );

                        num46 = 255.0;
                        if (Math.Abs(AvailableRecipeY[num20]) > 150f)
                        {
                            num46 =
                                255f * (100f - (Math.Abs(AvailableRecipeY[num20]) - 150f))
                                     * 0.01;
                        }

                        color10 = Color.White;
                        color10.R = (byte)num46;
                        color10.G = (byte)num46;
                        color10.B = (byte)num46;
                        color10.A = (byte)num46;
                        if (
                            mouseState.X >= num42
                            && mouseState.X
                            <= num42 + hotbarTexture.Width * inventoryScale && mouseState.Y >= num43
                            && mouseState.Y
                            <= num43 + hotbarTexture.Height * inventoryScale
                        )
                        {
                            Player[MyPlayer].MouseInterface = true;
                            if (
                                mouseLeftRelease
                                && mouseState.LeftButton == ButtonState.Pressed
                            )
                            {
                                if (focusRecipe == num20)
                                {
                                    if (
                                        mouseItem.Type == 0
                                        || (
                                            mouseItem.IsTheSameAs(
                                                Recipe[AvailableRecipe[num20]].CreateItem
                                            )
                                            && mouseItem.Stack
                                            + Recipe[AvailableRecipe[num20]]
                                                .CreateItem
                                                .Stack <= mouseItem.MaxStack
                                        )
                                    )
                                    {
                                        var stack = mouseItem.Stack;
                                        mouseItem = (Item)
                                            Recipe[AvailableRecipe[num20]].CreateItem.Clone();

                                        mouseItem.Stack += stack;
                                        Recipe[AvailableRecipe[num20]].Create();
                                        if (
                                            mouseItem.Type > 0
                                            || Recipe[AvailableRecipe[num20]].CreateItem.Type
                                            > 0
                                        )
                                        {
                                            soundInstanceGrab.Stop();
                                            soundInstanceGrab = soundGrab.CreateInstance();
                                            soundInstanceGrab.Play();
                                        }
                                    }
                                }
                                else
                                    focusRecipe = num20;
                            }

                            text = Recipe[AvailableRecipe[num20]].CreateItem.Name;
                            if (Recipe[AvailableRecipe[num20]].CreateItem.Stack > 1)
                            {
                                obj2 = text;
                                text = string.Concat(obj2, " (", Recipe[AvailableRecipe[num20]].CreateItem.Stack, ")");
                            }
                        }

                        vector = new Vector2();
                        _spriteBatch.Draw(
                            hotbarTexture,
                            new Vector2(num42, num43),
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
                            num46 = 0.0;

                        vector = new Vector2();
                        _spriteBatch.Draw(
                            inventoryBackTexture,
                            new Vector2(num42, num43),
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
                            Recipe[AvailableRecipe[num20]].CreateItem.Type > 0
                            && Recipe[AvailableRecipe[num20]].CreateItem.Stack > 0
                        )
                        {
                            num45 = 1f;
                            if (
                                ItemTexture[
                                    Recipe[AvailableRecipe[num20]].CreateItem.Type
                                ].Width > 0x20
                                || ItemTexture[
                                    Recipe[AvailableRecipe[num20]].CreateItem.Type
                                ].Height > 0x20
                            )
                            {
                                if (
                                    ItemTexture[
                                        Recipe[AvailableRecipe[num20]].CreateItem.Type
                                    ].Width
                                    > ItemTexture[
                                        Recipe[AvailableRecipe[num20]].CreateItem.Type
                                    ].Height
                                )
                                {
                                    num45 =
                                        32f
                                        / ItemTexture[
                                            Recipe[AvailableRecipe[num20]]
                                                .CreateItem
                                                .Type
                                        ].Width;
                                }
                                else
                                {
                                    num45 =
                                        32f
                                        / ItemTexture[
                                            Recipe[AvailableRecipe[num20]]
                                                .CreateItem
                                                .Type
                                        ].Height;
                                }
                            }

                            num45 *= inventoryScale;
                            vector = new Vector2();
                            _spriteBatch.Draw(
                                ItemTexture[Recipe[AvailableRecipe[num20]].CreateItem.Type],
                                new Vector2(
                                    num42 + 26f * inventoryScale
                                    - ItemTexture[
                                        Recipe[AvailableRecipe[num20]]
                                            .CreateItem
                                            .Type
                                    ].Width * 0.5f * num45,
                                    num43 + 26f * inventoryScale
                                    - ItemTexture[
                                        Recipe[AvailableRecipe[num20]]
                                            .CreateItem
                                            .Type
                                    ].Height * 0.5f * num45
                                ),
                                new Rectangle(
                                    0,
                                    0,
                                    ItemTexture[
                                        Recipe[AvailableRecipe[num20]].CreateItem.Type
                                    ].Width,
                                    ItemTexture[
                                        Recipe[AvailableRecipe[num20]].CreateItem.Type
                                    ].Height
                                ),
                                Recipe[AvailableRecipe[num20]].CreateItem.GetAlpha(color10),
                                0f,
                                vector,
                                num45,
                                SpriteEffects.None,
                                0f
                            );

                            color15 = new Color();
                            if (Recipe[AvailableRecipe[num20]].CreateItem.Color != color15)
                            {
                                vector = new Vector2();
                                _spriteBatch.Draw(
                                    ItemTexture[Recipe[AvailableRecipe[num20]].CreateItem.Type],
                                    new Vector2(
                                        num42 + 26f * inventoryScale
                                        - ItemTexture[
                                            Recipe[AvailableRecipe[num20]]
                                                .CreateItem
                                                .Type
                                        ].Width * 0.5f * num45,
                                        num43 + 26f * inventoryScale
                                        - ItemTexture[
                                            Recipe[AvailableRecipe[num20]]
                                                .CreateItem
                                                .Type
                                        ].Height * 0.5f * num45
                                    ),
                                    new Rectangle(
                                        0,
                                        0,
                                        ItemTexture[
                                            Recipe[AvailableRecipe[num20]].CreateItem.Type
                                        ].Width,
                                        ItemTexture[
                                            Recipe[AvailableRecipe[num20]].CreateItem.Type
                                        ].Height
                                    ),
                                    Recipe[AvailableRecipe[num20]].CreateItem.GetColor(color10),
                                    0f,
                                    vector,
                                    num45,
                                    SpriteEffects.None,
                                    0f
                                );
                            }

                            if (Recipe[AvailableRecipe[num20]].CreateItem.Stack > 1)
                            {
                                vector = new Vector2();
                                _spriteBatch.DrawString(
                                    fontItemStack,
                                    string.Concat(
                                        Recipe[AvailableRecipe[num20]]
                                            .CreateItem
                                            .Stack
                                    ),
                                    new Vector2(
                                        num42 + 10f * inventoryScale,
                                        num43 + 26f * inventoryScale
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
                    for (num20 = 0; num20 < Terraria.Recipe.MaxRequirements; num20++)
                    {
                        if (Recipe[AvailableRecipe[focusRecipe]].RequiredItem[num20].Type == 0)
                            break;

                        num42 = 80 + num20 * 40;
                        num43 = 380;
                        color10 = Color.White;
                        num46 = 255f - Math.Abs(AvailableRecipeY[focusRecipe]) * 3f;
                        if (num46 < 0.0)
                            num46 = 0.0;

                        color10.R = (byte)num46;
                        color10.G = (byte)num46;
                        color10.B = (byte)num46;
                        color10.A = (byte)num46;
                        inventoryScale = 0.6f;
                        if (num46 == 0.0)
                            break;

                        if (
                            mouseState.X >= num42
                            && mouseState.X
                            <= num42 + hotbarTexture.Width * inventoryScale && mouseState.Y >= num43
                            && mouseState.Y
                            <= num43 + hotbarTexture.Height * inventoryScale
                        )
                        {
                            Player[MyPlayer].MouseInterface = true;
                            text = Recipe[AvailableRecipe[focusRecipe]]
                                .RequiredItem[num20]
                                .Name;

                            if (
                                Recipe[AvailableRecipe[focusRecipe]].RequiredItem[num20].Stack
                                > 1
                            )
                            {
                                obj2 = text;
                                text = string.Concat(obj2, " (", Recipe[AvailableRecipe[focusRecipe]]
                                    .RequiredItem[num20]
                                    .Stack, ")");
                            }
                        }

                        vector = new Vector2();
                        _spriteBatch.Draw(
                            hotbarTexture,
                            new Vector2(num42, num43),
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
                            num46 = 0.0;

                        vector = new Vector2();
                        _spriteBatch.Draw(
                            inventoryBackTexture,
                            new Vector2(num42, num43),
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
                            Recipe[AvailableRecipe[focusRecipe]].RequiredItem[num20].Type > 0
                            && Recipe[AvailableRecipe[focusRecipe]].RequiredItem[num20].Stack
                            > 0
                        )
                        {
                            num45 = 1f;
                            if (
                                ItemTexture[
                                    Recipe[AvailableRecipe[focusRecipe]]
                                        .RequiredItem[num20]
                                        .Type
                                ].Width > 0x20
                                || ItemTexture[
                                    Recipe[AvailableRecipe[focusRecipe]]
                                        .RequiredItem[num20]
                                        .Type
                                ].Height > 0x20
                            )
                            {
                                if (
                                    ItemTexture[
                                        Recipe[AvailableRecipe[focusRecipe]]
                                            .RequiredItem[num20]
                                            .Type
                                    ].Width
                                    > ItemTexture[
                                        Recipe[AvailableRecipe[focusRecipe]]
                                            .RequiredItem[num20]
                                            .Type
                                    ].Height
                                )
                                {
                                    num45 =
                                        32f
                                        / ItemTexture[
                                            Recipe[AvailableRecipe[focusRecipe]]
                                                .RequiredItem[num20]
                                                .Type
                                        ].Width;
                                }
                                else
                                {
                                    num45 =
                                        32f
                                        / ItemTexture[
                                            Recipe[AvailableRecipe[focusRecipe]]
                                                .RequiredItem[num20]
                                                .Type
                                        ].Height;
                                }
                            }

                            num45 *= inventoryScale;
                            vector = new Vector2();
                            _spriteBatch.Draw(
                                ItemTexture[
                                    Recipe[AvailableRecipe[focusRecipe]]
                                        .RequiredItem[num20]
                                        .Type
                                ],
                                new Vector2(
                                    num42 + 26f * inventoryScale
                                    - ItemTexture[
                                        Recipe[AvailableRecipe[focusRecipe]]
                                            .RequiredItem[num20]
                                            .Type
                                    ].Width * 0.5f * num45,
                                    num43 + 26f * inventoryScale
                                    - ItemTexture[
                                        Recipe[AvailableRecipe[focusRecipe]]
                                            .RequiredItem[num20]
                                            .Type
                                    ].Height * 0.5f * num45
                                ),
                                new Rectangle(
                                    0,
                                    0,
                                    ItemTexture[
                                        Recipe[AvailableRecipe[focusRecipe]]
                                            .RequiredItem[num20]
                                            .Type
                                    ].Width,
                                    ItemTexture[
                                        Recipe[AvailableRecipe[focusRecipe]]
                                            .RequiredItem[num20]
                                            .Type
                                    ].Height
                                ),
                                Recipe[AvailableRecipe[focusRecipe]]
                                    .RequiredItem[num20]
                                    .GetAlpha(color10),
                                0f,
                                vector,
                                num45,
                                SpriteEffects.None,
                                0f
                            );

                            color15 = new Color();
                            if (
                                Recipe[AvailableRecipe[focusRecipe]].RequiredItem[num20].Color
                                != color15
                            )
                            {
                                vector = new Vector2();
                                _spriteBatch.Draw(
                                    ItemTexture[
                                        Recipe[AvailableRecipe[focusRecipe]]
                                            .RequiredItem[num20]
                                            .Type
                                    ],
                                    new Vector2(
                                        num42 + 26f * inventoryScale
                                        - ItemTexture[
                                            Recipe[AvailableRecipe[focusRecipe]]
                                                .RequiredItem[num20]
                                                .Type
                                        ].Width * 0.5f * num45,
                                        num43 + 26f * inventoryScale
                                        - ItemTexture[
                                            Recipe[AvailableRecipe[focusRecipe]]
                                                .RequiredItem[num20]
                                                .Type
                                        ].Height * 0.5f * num45
                                    ),
                                    new Rectangle(
                                        0,
                                        0,
                                        ItemTexture[
                                            Recipe[AvailableRecipe[focusRecipe]]
                                                .RequiredItem[num20]
                                                .Type
                                        ].Width,
                                        ItemTexture[
                                            Recipe[AvailableRecipe[focusRecipe]]
                                                .RequiredItem[num20]
                                                .Type
                                        ].Height
                                    ),
                                    Recipe[AvailableRecipe[focusRecipe]]
                                        .RequiredItem[num20]
                                        .GetColor(color10),
                                    0f,
                                    vector,
                                    num45,
                                    SpriteEffects.None,
                                    0f
                                );
                            }

                            if (
                                Recipe[AvailableRecipe[focusRecipe]].RequiredItem[num20].Stack
                                > 1
                            )
                            {
                                vector = new Vector2();
                                _spriteBatch.DrawString(
                                    fontItemStack,
                                    string.Concat(
                                        Recipe[AvailableRecipe[num20]]
                                            .CreateItem
                                            .Stack
                                    ),
                                    new Vector2(
                                        num42 + 10f * inventoryScale,
                                        num43 + 26f * inventoryScale
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
                var num48 = 20;
                for (num20 = 0; num20 < 10; num20++)
                {
                    if (num20 == Player[MyPlayer].SelectedItem)
                    {
                        if (HotbarScale[num20] < 1f)
                            HotbarScale[num20] += 0.05f;
                    }
                    else if (HotbarScale[num20] > 0.75)
                        HotbarScale[num20] -= 0.05f;

                    var num49 = (int)(20f + 22f * (1f - HotbarScale[num20]));
                    var a = (int)(75f + 150f * HotbarScale[num20]);
                    Color color11 = new Color(0xff, 0xff, 0xff, a);
                    vector = new Vector2();
                    _spriteBatch.Draw(
                        hotbarTexture,
                        new Vector2(num48, num49),
                        new Rectangle(0, 0, hotbarTexture.Width, hotbarTexture.Height),
                        color11,
                        0f,
                        vector,
                        HotbarScale[num20],
                        SpriteEffects.None,
                        0f
                    );

                    if (
                        mouseState.X >= num48
                        && mouseState.X
                        <= num48 + hotbarTexture.Width * HotbarScale[num20] && mouseState.Y >= num49
                        && mouseState.Y
                        <= num49 + hotbarTexture.Height * HotbarScale[num20]
                    )
                    {
                        Player[MyPlayer].MouseInterface = true;
                        if (mouseState.LeftButton == ButtonState.Pressed)
                            Player[MyPlayer].ChangeItem = num20;

                        Player[MyPlayer].ShowItemIcon = false;
                        text = Player[MyPlayer].Inventory[num20].Name;
                        if (Player[MyPlayer].Inventory[num20].Stack > 1)
                        {
                            obj2 = text;
                            text = string.Concat(obj2, " (", Player[MyPlayer].Inventory[num20].Stack, ")");
                        }
                    }

                    if (
                        Player[MyPlayer].Inventory[num20].Type > 0
                        && Player[MyPlayer].Inventory[num20].Stack > 0
                    )
                    {
                        num45 = 1f;
                        if (
                            ItemTexture[Player[MyPlayer].Inventory[num20].Type].Width > 0x20
                            || ItemTexture[Player[MyPlayer].Inventory[num20].Type].Height
                            > 0x20
                        )
                        {
                            if (
                                ItemTexture[Player[MyPlayer].Inventory[num20].Type].Width
                                > ItemTexture[Player[MyPlayer].Inventory[num20].Type].Height
                            )
                            {
                                num45 =
                                    32f
                                    / ItemTexture[
                                        Player[MyPlayer].Inventory[num20].Type
                                    ].Width;
                            }
                            else
                            {
                                num45 =
                                    32f
                                    / ItemTexture[
                                        Player[MyPlayer].Inventory[num20].Type
                                    ].Height;
                            }
                        }

                        num45 *= HotbarScale[num20];
                        vector = new Vector2();
                        _spriteBatch.Draw(
                            ItemTexture[Player[MyPlayer].Inventory[num20].Type],
                            new Vector2(
                                num48 + 26f * HotbarScale[num20]
                                - ItemTexture[
                                    Player[MyPlayer].Inventory[num20].Type
                                ].Width * 0.5f * num45,
                                num49 + 26f * HotbarScale[num20]
                                - ItemTexture[
                                    Player[MyPlayer].Inventory[num20].Type
                                ].Height * 0.5f * num45
                            ),
                            new Rectangle(
                                0,
                                0,
                                ItemTexture[Player[MyPlayer].Inventory[num20].Type].Width,
                                ItemTexture[Player[MyPlayer].Inventory[num20].Type].Height
                            ),
                            Player[MyPlayer].Inventory[num20].GetAlpha(color11),
                            0f,
                            vector,
                            num45,
                            SpriteEffects.None,
                            0f
                        );

                        color15 = new Color();
                        if (Player[MyPlayer].Inventory[num20].Color != color15)
                        {
                            vector = new Vector2();
                            _spriteBatch.Draw(
                                ItemTexture[Player[MyPlayer].Inventory[num20].Type],
                                new Vector2(
                                    num48 + 26f * HotbarScale[num20]
                                    - ItemTexture[
                                        Player[MyPlayer].Inventory[num20].Type
                                    ].Width * 0.5f * num45,
                                    num49 + 26f * HotbarScale[num20]
                                    - ItemTexture[
                                        Player[MyPlayer].Inventory[num20].Type
                                    ].Height * 0.5f * num45
                                ),
                                new Rectangle(
                                    0,
                                    0,
                                    ItemTexture[Player[MyPlayer].Inventory[num20].Type].Width,
                                    ItemTexture[Player[MyPlayer].Inventory[num20].Type].Height
                                ),
                                Player[MyPlayer].Inventory[num20].GetColor(color11),
                                0f,
                                vector,
                                num45,
                                SpriteEffects.None,
                                0f
                            );
                        }

                        if (Player[MyPlayer].Inventory[num20].Stack > 1)
                        {
                            vector = new Vector2();
                            _spriteBatch.DrawString(
                                fontItemStack,
                                string.Concat(Player[MyPlayer].Inventory[num20].Stack),
                                new Vector2(
                                    num48 + 10f * HotbarScale[num20],
                                    num49 + 26f * HotbarScale[num20]
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

                    num48 += (int)(hotbarTexture.Width * HotbarScale[num20]) + 4;
                }
            }

            if (!string.IsNullOrEmpty(text) && mouseItem.Type == 0)
            {
                Player[MyPlayer].ShowItemIcon = false;
                vector = new Vector2();
                _spriteBatch.DrawString(
                    fontMouseText,
                    text,
                    new Vector2(mouseState.X + 10, mouseState.Y + 10),
                    new Color(mouseTextColor, mouseTextColor, mouseTextColor, mouseTextColor),
                    0f,
                    vector,
                    1f,
                    SpriteEffects.None,
                    0f
                );

                flag = true;
            }

            if (Player[MyPlayer].Dead)
            {
                var str2 = Player[MyPlayer].Name + " was slain...";
                vector = new Vector2();
                _spriteBatch.DrawString(
                    fontDeathText,
                    str2,
                    new Vector2(
                        ScreenWidth / 2 - str2.Length * 10,
                        ScreenHeight / 2 - 20
                    ),
                    Player[MyPlayer].GetDeathAlpha(new Color(0, 0, 0, 0)),
                    0f,
                    vector,
                    1f,
                    SpriteEffects.None,
                    0f
                );
            }

            vector = new Vector2();
            _spriteBatch.Draw(
                cursorTexture,
                new Vector2(mouseState.X, mouseState.Y),
                new Rectangle(0, 0, cursorTexture.Width, cursorTexture.Height),
                Color.White,
                0f,
                vector,
                1f,
                SpriteEffects.None,
                0f
            );

            if (mouseItem.Type > 0 && mouseItem.Stack > 0)
            {
                Player[MyPlayer].ShowItemIcon = false;
                Player[MyPlayer].ShowItemIcon2 = 0;
                flag = true;
                num45 = 1f;
                if (
                    ItemTexture[mouseItem.Type].Width > 0x20
                    || ItemTexture[mouseItem.Type].Height > 0x20
                )
                {
                    if (
                        ItemTexture[mouseItem.Type].Width
                        > ItemTexture[mouseItem.Type].Height
                    )
                        num45 = 32f / ItemTexture[mouseItem.Type].Width;
                    else
                        num45 = 32f / ItemTexture[mouseItem.Type].Height;
                }

                var num51 = 1f;
                Color color12 = Color.White;
                num45 *= num51;
                vector = new Vector2();
                _spriteBatch.Draw(
                    ItemTexture[mouseItem.Type],
                    new Vector2(
                        mouseState.X + 26f * num51
                        - ItemTexture[mouseItem.Type].Width * 0.5f * num45,
                        mouseState.Y + 26f * num51
                        - ItemTexture[mouseItem.Type].Height * 0.5f * num45
                    ),
                    new Rectangle(
                        0,
                        0,
                        ItemTexture[mouseItem.Type].Width,
                        ItemTexture[mouseItem.Type].Height
                    ),
                    mouseItem.GetAlpha(color12),
                    0f,
                    vector,
                    num45,
                    SpriteEffects.None,
                    0f
                );

                color15 = new Color();
                if (mouseItem.Color != color15)
                {
                    vector = new Vector2();
                    _spriteBatch.Draw(
                        ItemTexture[mouseItem.Type],
                        new Vector2(
                            mouseState.X + 26f * num51
                            - ItemTexture[mouseItem.Type].Width * 0.5f * num45,
                            mouseState.Y + 26f * num51
                            - ItemTexture[mouseItem.Type].Height * 0.5f * num45
                        ),
                        new Rectangle(
                            0,
                            0,
                            ItemTexture[mouseItem.Type].Width,
                            ItemTexture[mouseItem.Type].Height
                        ),
                        mouseItem.GetColor(color12),
                        0f,
                        vector,
                        num45,
                        SpriteEffects.None,
                        0f
                    );
                }

                if (mouseItem.Stack > 1)
                {
                    vector = new Vector2();
                    _spriteBatch.DrawString(
                        fontItemStack,
                        string.Concat(mouseItem.Stack),
                        new Vector2(mouseState.X + 10f * num51, mouseState.Y + 26f * num51),
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
                mouseState.X + (int)screenPosition.X,
                mouseState.Y + (int)screenPosition.Y,
                1,
                1
            );

            mouseTextColor = (byte)(mouseTextColor + (byte)mouseTextColorChange);
            if (mouseTextColor >= 250)
                mouseTextColorChange = -4;

            if (mouseTextColor <= 0xaf)
                mouseTextColorChange = 4;

            if (!flag)
            {
                var num52 = 0x1a * Player[MyPlayer].StatLifeMax / num34;
                var num53 = 0;
                if (Player[MyPlayer].StatLifeMax > 200)
                {
                    num52 -= 260;
                    num53 += 0x1a;
                }

                if (
                    mouseState.X > 500 && mouseState.X < 500 + num52
                                       && mouseState.Y > 0x20 && mouseState.Y < 0x20 + heartTexture.Height + num53
                )
                {
                    Player[MyPlayer].ShowItemIcon = false;
                    name = string.Concat("Life: ", Player[MyPlayer].StatLife, "/", Player[MyPlayer].StatLifeMax);
                    vector = new Vector2();
                    _spriteBatch.DrawString(
                        fontMouseText,
                        name,
                        new Vector2(mouseState.X + 10, mouseState.Y + 10),
                        new Color(
                            mouseTextColor,
                            mouseTextColor,
                            mouseTextColor,
                            mouseTextColor
                        ),
                        0f,
                        vector,
                        1f,
                        SpriteEffects.None,
                        0f
                    );

                    flag = true;
                }
            }

            if (!flag)
            {
                for (num20 = 0; num20 < 0x3e8; num20++)
                    if (Item[num20].Active)
                    {
                        Rectangle rectangle3 = new Rectangle(
                            (int)(
                                Item[num20].Position.X + Item[num20].Width * 0.5
                                - ItemTexture[Item[num20].Type].Width * 0.5
                            ),
                            (int)Item[num20].Position.Y + Item[num20].Height
                            - ItemTexture[Item[num20].Type].Height,
                            ItemTexture[Item[num20].Type].Width,
                            ItemTexture[Item[num20].Type].Height
                        );

                        if (rectangle2.Intersects(rectangle3))
                        {
                            Player[MyPlayer].ShowItemIcon = false;
                            name = Item[num20].Name;
                            if (Item[num20].Stack > 1)
                            {
                                obj2 = name;
                                name = string.Concat(obj2, " (", Item[num20].Stack, ")");
                            }

                            vector = new Vector2();
                            _spriteBatch.DrawString(
                                fontMouseText,
                                name,
                                new Vector2(
                                    mouseState.X + 10,
                                    mouseState.Y + 10
                                ),
                                new Color(
                                    mouseTextColor,
                                    mouseTextColor,
                                    mouseTextColor,
                                    mouseTextColor
                                ),
                                0f,
                                vector,
                                1f,
                                SpriteEffects.None,
                                0f
                            );

                            flag = true;
                            break;
                        }
                    }
            }

            if (!flag)
            {
                for (num20 = 0; num20 < 0x3e8; num20++)
                    if (Npc[num20].Active)
                    {
                        Rectangle rectangle4 = new Rectangle(
                            (int)(
                                Npc[num20].Position.X + Npc[num20].Width * 0.5
                                - NpcTexture[Npc[num20].Type].Width * 0.5
                            ),
                            (int)Npc[num20].Position.Y + Npc[num20].Height
                            - NpcTexture[Npc[num20].Type].Height
                            / NpcFrameCount[Npc[num20].Type],
                            NpcTexture[Npc[num20].Type].Width,
                            NpcTexture[Npc[num20].Type].Height / NpcFrameCount[Npc[num20].Type]
                        );

                        if (rectangle2.Intersects(rectangle4))
                        {
                            Player[MyPlayer].ShowItemIcon = false;
                            name = string.Concat(Npc[num20].Name, ": ", Npc[num20].Life, "/", Npc[num20].LifeMax);
                            vector = new Vector2();
                            _spriteBatch.DrawString(
                                fontMouseText,
                                name,
                                new Vector2(
                                    mouseState.X + 10,
                                    mouseState.Y + 10
                                ),
                                new Color(
                                    mouseTextColor,
                                    mouseTextColor,
                                    mouseTextColor,
                                    mouseTextColor
                                ),
                                0f,
                                vector,
                                1f,
                                SpriteEffects.None,
                                0f
                            );

                            break;
                        }
                    }
            }

            if (
                Player[MyPlayer].ShowItemIcon
                && (
                    Player[MyPlayer].Inventory[Player[MyPlayer].SelectedItem].Type > 0
                    || Player[MyPlayer].ShowItemIcon2 > 0
                )
            )
            {
                var type = Player[MyPlayer].Inventory[Player[MyPlayer].SelectedItem].Type;
                Color alpha = Player[MyPlayer]
                    .Inventory[Player[MyPlayer].SelectedItem]
                    .GetAlpha(Color.White);

                Color color14 = Player[MyPlayer]
                    .Inventory[Player[MyPlayer].SelectedItem]
                    .GetColor(Color.White);

                if (Player[MyPlayer].ShowItemIcon2 > 0)
                {
                    type = Player[MyPlayer].ShowItemIcon2;
                    alpha = Color.White;
                    color14 = new Color();
                }

                vector = new Vector2();
                _spriteBatch.Draw(
                    ItemTexture[type],
                    new Vector2(mouseState.X + 10, mouseState.Y + 10),
                    new Rectangle(0, 0, ItemTexture[type].Width, ItemTexture[type].Height),
                    alpha,
                    0f,
                    vector,
                    1f,
                    SpriteEffects.None,
                    0f
                );

                if (
                    Player[MyPlayer].ShowItemIcon2 == 0
                    && Player[MyPlayer].Inventory[Player[MyPlayer].SelectedItem].Color
                    != new Color()
                )
                {
                    _spriteBatch.Draw(
                        ItemTexture[
                            Player[MyPlayer].Inventory[Player[MyPlayer].SelectedItem].Type
                        ],
                        new Vector2(mouseState.X + 10, mouseState.Y + 10),
                        new Rectangle(
                            0,
                            0,
                            ItemTexture[
                                Player[MyPlayer].Inventory[Player[MyPlayer].SelectedItem].Type
                            ].Width,
                            ItemTexture[
                                Player[MyPlayer].Inventory[Player[MyPlayer].SelectedItem].Type
                            ].Height
                        ),
                        color14,
                        0f,
                        new Vector2(),
                        1f,
                        SpriteEffects.None,
                        0f
                    );
                }
            }

            Player[MyPlayer].ShowItemIcon = false;
            Player[MyPlayer].ShowItemIcon2 = 0;
            _spriteBatch.End();
            mouseLeftRelease = mouseState.LeftButton != ButtonState.Pressed;
        }
    }

    protected override void Initialize()
    {
        int num;
        Window.Title = "Terraria: Dig Peon, Dig!";
        for (num = 0; num < 0x1389; num++)
        for (var i = 0; i < 0x9c5; i++)
            Tile[num, i] = new Tile();

        TileSolid[0] = true;
        TileSolid[1] = true;
        TileSolid[2] = true;
        TileSolid[3] = false;
        TileNoFail[3] = true;
        TileSolid[4] = false;
        TileNoFail[4] = true;
        TileSolid[5] = false;
        TileSolid[6] = true;
        TileSolid[7] = true;
        TileSolid[8] = true;
        TileSolid[9] = true;
        TileSolid[10] = true;
        TileSolid[11] = false;
        for (num = 0; num < 0x3e8; num++)
            Dust[num] = new Dust();

        for (num = 0; num < 0x3e8; num++)
            Item[num] = new Item();

        for (num = 0; num < 0x3e8; num++)
            Npc[num] = new Npc();

        Terraria.Player.SetupPlayers();
        for (num = 0; num < Terraria.Recipe.MaxRecipes; num++)
        {
            Recipe[num] = new Recipe();
            AvailableRecipeY[num] = 0x41 * num;
        }

        Terraria.Recipe.SetupRecipes();
        _graphics.PreferredBackBufferWidth = ScreenWidth;
        _graphics.PreferredBackBufferHeight = ScreenHeight;
        _graphics.ApplyChanges();
        WorldGen.GenerateWorld();
        base.Initialize();
    }

    protected override void LoadContent()
    {
        int num;
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        for (num = 0; num < 12; num++)
            TileTexture[num] = Content.Load<Texture2D>(@"Images\Tiles_" + num);

        for (num = 1; num < 2; num++)
            WallTexture[num] = Content.Load<Texture2D>(@"Images\Wall_" + num);

        for (num = 0; num < 3; num++)
        {
            BackgroundTexture[num] = Content.Load<Texture2D>(@"Images\Background_" + num);
            BackgroundWidth[num] = BackgroundTexture[num].Width;
            BackgroundHeight[num] = BackgroundTexture[num].Height;
        }

        for (num = 0; num < 0x1b; num++)
            ItemTexture[num] = Content.Load<Texture2D>(@"Images\Item_" + num);

        for (num = 0; num < 2; num++)
            NpcTexture[num] = Content.Load<Texture2D>(@"Images\NPC_" + num);

        hotbarTexture = Content.Load<Texture2D>(@"Images\Hotbar");
        dustTexture = Content.Load<Texture2D>(@"Images\Dust");
        sunTexture = Content.Load<Texture2D>(@"Images\Sun");
        moonTexture = Content.Load<Texture2D>(@"Images\Moon");
        blackTileTexture = Content.Load<Texture2D>(@"Images\Black_Tile");
        heartTexture = Content.Load<Texture2D>(@"Images\Heart");
        cursorTexture = Content.Load<Texture2D>(@"Images\Cursor");
        treeTopTexture = Content.Load<Texture2D>(@"Images\Tree_Tops");
        treeBranchTexture = Content.Load<Texture2D>(@"Images\Tree_Branches");
        inventoryBackTexture = Content.Load<Texture2D>(@"Images\Inventory_Back");
        playerHeadTexture = Content.Load<Texture2D>(@"Images\Character_Heads");
        playerBodyTexture = Content.Load<Texture2D>(@"Images\Character_Bodies");
        playerLegTexture = Content.Load<Texture2D>(@"Images\Character_Legs");
        soundGrab = Content.Load<SoundEffect>(@"Sounds\Grab");
        soundInstanceGrab = soundGrab.CreateInstance();
        SoundDig[0] = Content.Load<SoundEffect>(@"Sounds\Dig_0");
        SoundInstanceDig[0] = SoundDig[0].CreateInstance();
        SoundDig[1] = Content.Load<SoundEffect>(@"Sounds\Dig_1");
        SoundInstanceDig[1] = SoundDig[1].CreateInstance();
        SoundDig[2] = Content.Load<SoundEffect>(@"Sounds\Dig_2");
        SoundInstanceDig[2] = SoundDig[2].CreateInstance();
        SoundPlayerHit[0] = Content.Load<SoundEffect>(@"Sounds\Player_Hit_0");
        SoundInstancePlayerHit[0] = SoundPlayerHit[0].CreateInstance();
        SoundPlayerHit[1] = Content.Load<SoundEffect>(@"Sounds\Player_Hit_1");
        SoundInstancePlayerHit[1] = SoundPlayerHit[1].CreateInstance();
        SoundPlayerHit[2] = Content.Load<SoundEffect>(@"Sounds\Player_Hit_2");
        SoundInstancePlayerHit[2] = SoundPlayerHit[2].CreateInstance();
        soundPlayerKilled = Content.Load<SoundEffect>(@"Sounds\Player_Killed");
        soundInstancePlayerKilled = soundPlayerKilled.CreateInstance();
        soundGrass = Content.Load<SoundEffect>(@"Sounds\Grass");
        soundInstanceGrass = soundGrass.CreateInstance();
        soundDoorOpen = Content.Load<SoundEffect>(@"Sounds\Door_Opened");
        soundInstanceDoorOpen = soundDoorOpen.CreateInstance();
        soundDoorClosed = Content.Load<SoundEffect>(@"Sounds\Door_Closed");
        soundInstanceDoorClosed = soundDoorClosed.CreateInstance();
        soundMenuTick = Content.Load<SoundEffect>(@"Sounds\Menu_Tick");
        soundInstanceMenuTick = soundMenuTick.CreateInstance();
        soundMenuOpen = Content.Load<SoundEffect>(@"Sounds\Menu_Open");
        soundMenuClose = Content.Load<SoundEffect>(@"Sounds\Menu_Close");
        for (num = 1; num < 3; num++)
        {
            SoundItem[num] = Content.Load<SoundEffect>(@"Sounds\Item_" + num);
            SoundInstanceItem[num] = SoundItem[num].CreateInstance();
        }

        for (num = 1; num < 2; num++)
        {
            SoundNpcHit[num] = Content.Load<SoundEffect>(@"Sounds\NPC_Hit_" + num);
            SoundInstanceNpcHit[num] = SoundNpcHit[num].CreateInstance();
        }

        for (num = 1; num < 2; num++)
        {
            SoundNpcKilled[num] = Content.Load<SoundEffect>(@"Sounds\NPC_Killed_" + num);
            SoundInstanceNpcKilled[num] = SoundNpcKilled[num].CreateInstance();
        }

        fontItemStack = Content.Load<SpriteFont>(@"Fonts\Item_Stack");
        fontMouseText = Content.Load<SpriteFont>(@"Fonts\Mouse_Text");
        fontDeathText = Content.Load<SpriteFont>(@"Fonts\Death_Text");
    }

    protected override void UnloadContent()
    {
    }

    protected override void Update(GameTime gameTime)
    {
        if (!IsActive)
        {
            IsMouseVisible = true;
            Player[MyPlayer].DelayUseItem = true;
            mouseLeftRelease = false;
        }
        else
        {
            int num;
            IsMouseVisible = false;
            if (
                keyState.IsKeyDown(Keys.LeftAlt)
                || (keyState.IsKeyDown(Keys.RightAlt) && keyState.IsKeyDown(Keys.Enter))
            )
            {
                if (_toggleFullscreen)
                    _graphics.ToggleFullScreen();

                _toggleFullscreen = false;
            }
            else
                _toggleFullscreen = true;

            oldMouseState = mouseState;
            mouseState = Mouse.GetState();
            keyState = Keyboard.GetState();
            if (DebugMode)
                UpdateDebug();

            for (num = 0; num < 0x10; num++)
                Player[num].UpdatePlayer(num);

            Terraria.Npc.SpawnNpc();
            for (num = 0; num < 0x10; num++)
                Player[num].ActiveNpCs = 0;

            for (num = 0; num < 0x3e8; num++)
                Npc[num].UpdateNpc(num);

            for (num = 0; num < 0x3e8; num++)
                Item[num].UpdateItem(num);

            Terraria.Dust.UpdateDust();
            UpdateTime();
            WorldGen.UpdateWorld();
            base.Update(gameTime);
        }
    }

    private static void UpdateDebug()
    {
        if (keyState.IsKeyDown(Keys.Left))
            screenPosition.X -= 8f;

        if (keyState.IsKeyDown(Keys.Right))
            screenPosition.X += 8f;

        if (keyState.IsKeyDown(Keys.Up))
            screenPosition.Y -= 8f;

        if (keyState.IsKeyDown(Keys.Down))
            screenPosition.Y += 8f;

        var i = (int)((mouseState.X + screenPosition.X) / 16f);
        var j = (int)((mouseState.Y + screenPosition.Y) / 16f);
        if (
            mouseState.X < ScreenWidth && mouseState.Y < ScreenHeight
                                       && i >= 0 && j >= 0 && i < 0x1389 && j < 0x9c5
        )
        {
            if (
                mouseState is { RightButton: ButtonState.Pressed, LeftButton: ButtonState.Pressed }
            )
            {
                if (Player[MyPlayer].ReleaseUseItem)
                {
                    var index = Terraria.Npc.NewNpc(
                        mouseState.X + (int)screenPosition.X,
                        mouseState.Y + (int)screenPosition.Y,
                        1
                    );

                    dayTime = true;
                    Npc[index].Name = "Yellow Slime";
                    Npc[index].Scale = 1.2f;
                    Npc[index].Damage = 15;
                    Npc[index].Defense = 15;
                    Npc[index].Life = 50;
                    Npc[index].LifeMax = Npc[index].Life;
                    Npc[index].Color = new Color(0xff, 200, 0, 100);
                }
            }
            else if (mouseState.RightButton == ButtonState.Pressed)
            {
                if (!Tile[i, j].Active)
                    WorldGen.PlaceTile(i, j, 4);
            }
            else if (mouseState.LeftButton == ButtonState.Pressed)
            {
            }
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
                    moonPhase = 0;
            }
        }
        else if (time > 40000.0)
        {
            time = 0.0;
            dayTime = false;
        }
    }
}
