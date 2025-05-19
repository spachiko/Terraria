using Microsoft.Xna.Framework;

namespace Terraria;

public abstract class Lighting
{
    public const int OffScreenTiles = 0x15;

    public static readonly Color[,] Color = new Color[
        Main.ScreenWidth / 0x10 + 0x2a + 10,
        Main.ScreenHeight / 0x10 + 0x2a + 10
    ];

    private static int firstTileX;
    private static int firstTileY;
    private static int firstToLightX;
    private static int firstToLightY;
    private static int lastTileX;
    private static int lastTileY;
    private static int lastToLightX;
    private static int lastToLightY;
    private static Color lightColor = Microsoft.Xna.Framework.Color.Black;
    private static readonly int MaxTempLights = 100;
    private static readonly int[] TempLight = new int[MaxTempLights];
    private static int tempLightCount;
    private static readonly int[] TempLightX = new int[MaxTempLights];
    private static readonly int[] TempLightY = new int[MaxTempLights];

    public static void AddLight(int i, int j, byte lightness)
    {
        if (
            tempLightCount != MaxTempLights
            && i - firstTileX + 0x15 >= 0
            && i - firstTileX + 0x15
            < Main.ScreenWidth / 0x10 + 0x2a + 10 && j - firstTileY + 0x15 >= 0 &&
            j - firstTileY + 0x15 < Main.ScreenHeight / 0x10 + 0x2a + 10
        )
        {
            TempLightX[tempLightCount] = i;
            TempLightY[tempLightCount] = j;
            TempLight[tempLightCount] = lightness;
            tempLightCount++;
        }
    }

    private static void LightColor(int i, int j)
    {
        var num = i - firstToLightX;
        var num2 = j - firstToLightY;
        try
        {
            int num3;
            if (Color[num, num2].R > lightColor.R)
                lightColor = Color[num, num2];
            else
                Color[num, num2] = lightColor;

            if (!(Main.Tile[i, j].Active && Main.TileSolid[Main.Tile[i, j].Type]))
                num3 = 10;
            else
                num3 = 40;

            var num4 = lightColor.R - num3;
            if (num4 < 0)
                num4 = 0;

            lightColor.R = (byte)num4;
            lightColor.B = lightColor.R;
            lightColor.G = lightColor.R;
            if (
                lightColor.R > 0
                && (!Main.Tile[i, j].Active || !Main.TileSolid[Main.Tile[i, j].Type]) && j < Main.worldSurface
            )
                Main.Tile[i, j].Lighted = true;
        }
        catch
        {
            // ignored
        }
    }

    public static int LightingX(int lightX)
    {
        if (lightX < 0)
            return 0;

        if (lightX >= Main.ScreenWidth / 0x10 + 0x2a + 10)
            return Main.ScreenWidth / 0x10 + 0x2a + 10 - 1;

        return lightX;
    }

    public static int LightingY(int lightY)
    {
        if (lightY < 0)
            return 0;

        if (lightY >= Main.ScreenHeight / 0x10 + 0x2a + 10)
            return Main.ScreenHeight / 0x10 + 0x2a + 10 - 1;

        return lightY;
    }

    public static void LightTiles(int firstX, int lastX, int firstY, int lastY)
    {
        int toLightX;
        int toLightY;
        firstTileX = firstX;
        lastTileX = lastX;
        firstTileY = firstY;
        lastTileY = lastY;
        firstToLightX = firstTileX - 0x15;
        firstToLightY = firstTileY - 0x15;
        lastToLightX = lastTileX + 0x15;
        lastToLightY = lastTileY + 0x15;
        for (
            toLightX = 0;
            toLightX < Main.ScreenWidth / 0x10 + 0x2a + 10;
            toLightX++
        )
        {
            toLightY = 0;
            while (toLightY < Main.ScreenHeight / 0x10 + 0x2a + 10)
            {
                Color[toLightX, toLightY] = Microsoft.Xna.Framework.Color.Black;
                toLightY++;
            }
        }

        for (toLightX = 0; toLightX < tempLightCount; toLightX++)
            if (
                TempLightX[toLightX] - firstTileX + 0x15 >= 0
                && TempLightX[toLightX] - firstTileX + 0x15
                < Main.ScreenWidth / 0x10 + 0x2a + 10 && TempLightY[toLightX] - firstTileY + 0x15 >= 0
                && TempLightY[toLightX] - firstTileY + 0x15
                < Main.ScreenHeight / 0x10 + 0x2a + 10
            )
            {
                Color[
                    TempLightX[toLightX] - firstTileX + 0x15,
                    TempLightY[toLightX] - firstTileY + 0x15
                ] = new Color(
                    TempLight[toLightX],
                    TempLight[toLightX],
                    TempLight[toLightX],
                    TempLight[toLightX]
                );
            }

        tempLightCount = 0;
        toLightX = firstToLightX;
        while (toLightX < lastToLightX)
        {
            toLightY = firstToLightY;
            while (toLightY < lastToLightY)
            {
                if (
                    toLightX is >= 0 and < 0x1389
                    && toLightY is >= 0 and < 0x9c5
                    && (
                        !Main.Tile[toLightX, toLightY].Active
                        || !Main.TileSolid[Main.Tile[toLightX, toLightY].Type]
                    )
                    && (
                        Main.Tile[toLightX, toLightY].Lighted
                        || Main.Tile[toLightX, toLightY].Type == 4
                    )
                )
                {
                    if (
                        Color[
                            toLightX - firstToLightX,
                            toLightY - firstToLightY
                        ].R < Main.tileColor.R
                        && Main.tileColor.R
                        > Color[
                            toLightX - firstToLightX,
                            toLightY - firstToLightY
                        ].R && Main.Tile[toLightX, toLightY].Wall == 0
                    )
                    {
                        Color[
                            toLightX - firstToLightX,
                            toLightY - firstToLightY
                        ] = Main.tileColor;
                    }

                    if (Main.Tile[toLightX, toLightY].Type == 4)
                    {
                        Color[
                            toLightX - firstToLightX,
                            toLightY - firstToLightY
                        ] = Microsoft.Xna.Framework.Color.White;
                    }
                }

                toLightY++;
            }

            toLightX++;
        }

        for (var i = 0; i < 2; i++)
        {
            toLightX = firstToLightX;
            while (toLightX < lastToLightX)
            {
                lightColor = Microsoft.Xna.Framework.Color.Black;
                toLightY = firstToLightY;
                while (toLightY < lastToLightY)
                {
                    LightColor(toLightX, toLightY);
                    toLightY++;
                }

                toLightX++;
            }

            toLightX = firstToLightX;
            while (toLightX < lastToLightX)
            {
                lightColor = Microsoft.Xna.Framework.Color.Black;
                toLightY = lastToLightY;
                while (toLightY >= firstToLightY)
                {
                    LightColor(toLightX, toLightY);
                    toLightY--;
                }

                toLightX++;
            }

            toLightY = firstToLightY;
            while (toLightY < lastToLightY)
            {
                lightColor = Microsoft.Xna.Framework.Color.Black;
                toLightX = firstToLightX;
                while (toLightX < lastToLightX)
                {
                    LightColor(toLightX, toLightY);
                    toLightX++;
                }

                toLightY++;
            }

            for (
                toLightY = firstToLightY;
                toLightY < lastToLightY;
                toLightY++
            )
            {
                lightColor = Microsoft.Xna.Framework.Color.Black;
                for (
                    toLightX = lastToLightX;
                    toLightX >= firstToLightX;
                    toLightX--
                )
                    LightColor(toLightX, toLightY);
            }
        }
    }
}
