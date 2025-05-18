namespace Terraria
{
    using System;
    using Microsoft.Xna.Framework;

    public class Lighting
    {
        public static Color[,] color = new Color[
            ((Main.screenWidth / 0x10) + 0x2a) + 10,
            ((Main.screenHeight / 0x10) + 0x2a) + 10
        ];
        private static int firstTileX;
        private static int firstTileY;
        private static int firstToLightX;
        private static int firstToLightY;
        private static int lastTileX;
        private static int lastTileY;
        private static int lastToLightX;
        private static int lastToLightY;
        private static Color lightColor = Color.Black;
        private static int maxTempLights = 100;
        public const int offScreenTiles = 0x15;
        private static int[] tempLight = new int[maxTempLights];
        private static int tempLightCount;
        private static int[] tempLightX = new int[maxTempLights];
        private static int[] tempLightY = new int[maxTempLights];

        public static void addLight(int i, int j, byte Lightness)
        {
            if (
                (tempLightCount != maxTempLights)
                && (
                    (
                        (
                            (((i - firstTileX) + 0x15) >= 0)
                            && (
                                ((i - firstTileX) + 0x15)
                                < (((Main.screenWidth / 0x10) + 0x2a) + 10)
                            )
                        ) && (((j - firstTileY) + 0x15) >= 0)
                    ) && (((j - firstTileY) + 0x15) < (((Main.screenHeight / 0x10) + 0x2a) + 10))
                )
            )
            {
                tempLightX[tempLightCount] = i;
                tempLightY[tempLightCount] = j;
                tempLight[tempLightCount] = Lightness;
                tempLightCount++;
            }
        }

        private static void LightColor(int i, int j)
        {
            int num = i - firstToLightX;
            int num2 = j - firstToLightY;
            try
            {
                int num3;
                if (color[num, num2].R > lightColor.R)
                {
                    lightColor = color[num, num2];
                }
                else
                {
                    color[num, num2] = lightColor;
                }
                if (!(Main.tile[i, j].active && Main.tileSolid[Main.tile[i, j].type]))
                {
                    num3 = 10;
                }
                else
                {
                    num3 = 40;
                }
                int num4 = lightColor.R - num3;
                if (num4 < 0)
                {
                    num4 = 0;
                }
                if (num4 > 0xff)
                {
                    num4 = 0xff;
                }
                lightColor.R = (byte)num4;
                lightColor.B = lightColor.R;
                lightColor.G = lightColor.R;
                if (
                    (
                        (lightColor.R > 0)
                        && (!Main.tile[i, j].active || !Main.tileSolid[Main.tile[i, j].type])
                    ) && (j < Main.worldSurface)
                )
                {
                    Main.tile[i, j].lighted = true;
                }
            }
            catch { }
        }

        public static int LightingX(int lightX)
        {
            if (lightX < 0)
            {
                return 0;
            }
            if (lightX >= (((Main.screenWidth / 0x10) + 0x2a) + 10))
            {
                return ((((Main.screenWidth / 0x10) + 0x2a) + 10) - 1);
            }
            return lightX;
        }

        public static int LightingY(int lightY)
        {
            if (lightY < 0)
            {
                return 0;
            }
            if (lightY >= (((Main.screenHeight / 0x10) + 0x2a) + 10))
            {
                return ((((Main.screenHeight / 0x10) + 0x2a) + 10) - 1);
            }
            return lightY;
        }

        public static void LightTiles(int firstX, int lastX, int firstY, int lastY)
        {
            int firstToLightX;
            int firstToLightY;
            firstTileX = firstX;
            lastTileX = lastX;
            firstTileY = firstY;
            lastTileY = lastY;
            Lighting.firstToLightX = firstTileX - 0x15;
            Lighting.firstToLightY = firstTileY - 0x15;
            lastToLightX = lastTileX + 0x15;
            lastToLightY = lastTileY + 0x15;
            for (
                firstToLightX = 0;
                firstToLightX < (((Main.screenWidth / 0x10) + 0x2a) + 10);
                firstToLightX++
            )
            {
                firstToLightY = 0;
                while (firstToLightY < (((Main.screenHeight / 0x10) + 0x2a) + 10))
                {
                    color[firstToLightX, firstToLightY] = Color.Black;
                    firstToLightY++;
                }
            }
            for (firstToLightX = 0; firstToLightX < tempLightCount; firstToLightX++)
            {
                if (
                    (
                        (
                            (((tempLightX[firstToLightX] - firstTileX) + 0x15) >= 0)
                            && (
                                ((tempLightX[firstToLightX] - firstTileX) + 0x15)
                                < (((Main.screenWidth / 0x10) + 0x2a) + 10)
                            )
                        ) && (((tempLightY[firstToLightX] - firstTileY) + 0x15) >= 0)
                    )
                    && (
                        ((tempLightY[firstToLightX] - firstTileY) + 0x15)
                        < (((Main.screenHeight / 0x10) + 0x2a) + 10)
                    )
                )
                {
                    color[
                        (tempLightX[firstToLightX] - firstTileX) + 0x15,
                        (tempLightY[firstToLightX] - firstTileY) + 0x15
                    ] = new Color(
                        tempLight[firstToLightX],
                        tempLight[firstToLightX],
                        tempLight[firstToLightX],
                        tempLight[firstToLightX]
                    );
                }
            }
            tempLightCount = 0;
            firstToLightX = Lighting.firstToLightX;
            while (firstToLightX < lastToLightX)
            {
                firstToLightY = Lighting.firstToLightY;
                while (firstToLightY < lastToLightY)
                {
                    if (
                        (
                            (
                                ((firstToLightX >= 0) && (firstToLightX < 0x1389))
                                && (firstToLightY >= 0)
                            ) && (firstToLightY < 0x9c5)
                        )
                        && (
                            (
                                !Main.tile[firstToLightX, firstToLightY].active
                                || !Main.tileSolid[Main.tile[firstToLightX, firstToLightY].type]
                            )
                            && (
                                Main.tile[firstToLightX, firstToLightY].lighted
                                || (Main.tile[firstToLightX, firstToLightY].type == 4)
                            )
                        )
                    )
                    {
                        if (
                            (
                                (
                                    color[
                                        firstToLightX - Lighting.firstToLightX,
                                        firstToLightY - Lighting.firstToLightY
                                    ].R < Main.tileColor.R
                                )
                                && (
                                    Main.tileColor.R
                                    > color[
                                        firstToLightX - Lighting.firstToLightX,
                                        firstToLightY - Lighting.firstToLightY
                                    ].R
                                )
                            ) && (Main.tile[firstToLightX, firstToLightY].wall == 0)
                        )
                        {
                            color[
                                firstToLightX - Lighting.firstToLightX,
                                firstToLightY - Lighting.firstToLightY
                            ] = Main.tileColor;
                        }
                        if (Main.tile[firstToLightX, firstToLightY].type == 4)
                        {
                            color[
                                firstToLightX - Lighting.firstToLightX,
                                firstToLightY - Lighting.firstToLightY
                            ] = Color.White;
                        }
                    }
                    firstToLightY++;
                }
                firstToLightX++;
            }
            for (int i = 0; i < 2; i++)
            {
                firstToLightX = Lighting.firstToLightX;
                while (firstToLightX < lastToLightX)
                {
                    lightColor = Color.Black;
                    firstToLightY = Lighting.firstToLightY;
                    while (firstToLightY < lastToLightY)
                    {
                        LightColor(firstToLightX, firstToLightY);
                        firstToLightY++;
                    }
                    firstToLightX++;
                }
                firstToLightX = Lighting.firstToLightX;
                while (firstToLightX < lastToLightX)
                {
                    lightColor = Color.Black;
                    firstToLightY = lastToLightY;
                    while (firstToLightY >= Lighting.firstToLightY)
                    {
                        LightColor(firstToLightX, firstToLightY);
                        firstToLightY--;
                    }
                    firstToLightX++;
                }
                firstToLightY = Lighting.firstToLightY;
                while (firstToLightY < lastToLightY)
                {
                    lightColor = Color.Black;
                    firstToLightX = Lighting.firstToLightX;
                    while (firstToLightX < lastToLightX)
                    {
                        LightColor(firstToLightX, firstToLightY);
                        firstToLightX++;
                    }
                    firstToLightY++;
                }
                for (
                    firstToLightY = Lighting.firstToLightY;
                    firstToLightY < lastToLightY;
                    firstToLightY++
                )
                {
                    lightColor = Color.Black;
                    for (
                        firstToLightX = lastToLightX;
                        firstToLightX >= Lighting.firstToLightX;
                        firstToLightX--
                    )
                    {
                        LightColor(firstToLightX, firstToLightY);
                    }
                }
            }
        }
    }
}
