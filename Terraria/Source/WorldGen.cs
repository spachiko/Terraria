using System;
using Microsoft.Xna.Framework;

namespace Terraria;

internal abstract class WorldGen
{
    private static bool destroyObject;
    private static bool mergeDown;
    private static bool mergeLeft;
    private static bool mergeRight;
    private static bool mergeUp;

    private static void AddPlants()
    {
        for (var i = 0; i < 0x1389; i++)
        for (var j = 1; j < 0x9c5; j++)
            if (
                Main.Tile[i, j].Type == 2 && Main.Tile[i, j].Active
                                          && !Main.Tile[i, j - 1].Active
            )
                PlaceTile(i, j - 1, 3, true);
    }

    private static void AddTrees()
    {
        for (var i = 1; i < 0x1388; i++)
        for (var j = 20; j < Main.worldSurface; j++)
            if (
                Main.Tile[i, j].Active && Main.Tile[i, j].Type == 2
                                       && Main.Tile[i - 1, j].Active && Main.Tile[i - 1, j].Type == 2
                                       && Main.Tile[i + 1, j].Active && Main.Tile[i + 1, j].Type == 2 &&
                                       EmptyTileCheck(i - 2, i + 2, j - 14, j - 1)
            )
            {
                int num3;
                var flag = false;
                var flag2 = false;
                var num5 = Main.Rand.Next(5, 15);
                for (var k = j - num5; k < j; k++)
                {
                    Main.Tile[i, k].FrameNumber = (byte)Main.Rand.Next(3);
                    Main.Tile[i, k].Active = true;
                    Main.Tile[i, k].Type = 5;
                    num3 = Main.Rand.Next(3);
                    var num4 = Main.Rand.Next(10);
                    if (k == j - 1 || k == j - num5)
                        num4 = 0;

                    goto Label_0184;
                    Label_0175:
                    num4 = Main.Rand.Next(10);
                    Label_0184: ;
                    if (
                        ((num4 == 5 || num4 == 7) && flag)
                        || ((num4 == 6 || num4 == 7) && flag2)
                    )
                        goto Label_0175;

                    flag = false;
                    flag2 = false;
                    if (num4 == 5 || num4 == 7)
                        flag = true;

                    if (num4 == 6 || num4 == 7)
                        flag2 = true;

                    if (num4 == 1)
                    {
                        switch (num3)
                        {
                            case 0:
                                Main.Tile[i, k].FrameX = 0;
                                Main.Tile[i, k].FrameY = 0x42;
                                break;

                            case 1:
                                Main.Tile[i, k].FrameX = 0;
                                Main.Tile[i, k].FrameY = 0x58;
                                break;

                            case 2:
                                Main.Tile[i, k].FrameX = 0;
                                Main.Tile[i, k].FrameY = 110;
                                break;
                        }
                    }
                    else if (num4 == 2)
                    {
                        switch (num3)
                        {
                            case 0:
                                Main.Tile[i, k].FrameX = 0x16;
                                Main.Tile[i, k].FrameY = 0;
                                break;

                            case 1:
                                Main.Tile[i, k].FrameX = 0x16;
                                Main.Tile[i, k].FrameY = 0x16;
                                break;

                            case 2:
                                Main.Tile[i, k].FrameX = 0x16;
                                Main.Tile[i, k].FrameY = 0x2c;
                                break;
                        }
                    }
                    else if (num4 == 3)
                    {
                        switch (num3)
                        {
                            case 0:
                                Main.Tile[i, k].FrameX = 0x2c;
                                Main.Tile[i, k].FrameY = 0x42;
                                break;

                            case 1:
                                Main.Tile[i, k].FrameX = 0x2c;
                                Main.Tile[i, k].FrameY = 0x58;
                                break;

                            case 2:
                                Main.Tile[i, k].FrameX = 0x2c;
                                Main.Tile[i, k].FrameY = 110;
                                break;
                        }
                    }
                    else if (num4 == 4)
                    {
                        switch (num3)
                        {
                            case 0:
                                Main.Tile[i, k].FrameX = 0x16;
                                Main.Tile[i, k].FrameY = 0x42;
                                break;

                            case 1:
                                Main.Tile[i, k].FrameX = 0x16;
                                Main.Tile[i, k].FrameY = 0x58;
                                break;

                            case 2:
                                Main.Tile[i, k].FrameX = 0x16;
                                Main.Tile[i, k].FrameY = 110;
                                break;
                        }
                    }
                    else if (num4 == 5)
                    {
                        switch (num3)
                        {
                            case 0:
                                Main.Tile[i, k].FrameX = 0x58;
                                Main.Tile[i, k].FrameY = 0;
                                break;

                            case 1:
                                Main.Tile[i, k].FrameX = 0x58;
                                Main.Tile[i, k].FrameY = 0x16;
                                break;

                            case 2:
                                Main.Tile[i, k].FrameX = 0x58;
                                Main.Tile[i, k].FrameY = 0x2c;
                                break;
                        }
                    }
                    else if (num4 == 6)
                    {
                        switch (num3)
                        {
                            case 0:
                                Main.Tile[i, k].FrameX = 0x42;
                                Main.Tile[i, k].FrameY = 0x42;
                                break;

                            case 1:
                                Main.Tile[i, k].FrameX = 0x42;
                                Main.Tile[i, k].FrameY = 0x58;
                                break;

                            case 2:
                                Main.Tile[i, k].FrameX = 0x42;
                                Main.Tile[i, k].FrameY = 110;
                                break;
                        }
                    }
                    else if (num4 == 7)
                    {
                        switch (num3)
                        {
                            case 0:
                                Main.Tile[i, k].FrameX = 110;
                                Main.Tile[i, k].FrameY = 0x42;
                                break;

                            case 1:
                                Main.Tile[i, k].FrameX = 110;
                                Main.Tile[i, k].FrameY = 0x58;
                                break;

                            case 2:
                                Main.Tile[i, k].FrameX = 110;
                                Main.Tile[i, k].FrameY = 110;
                                break;
                        }
                    }
                    else
                    {
                        switch (num3)
                        {
                            case 0:
                                Main.Tile[i, k].FrameX = 0;
                                Main.Tile[i, k].FrameY = 0;
                                break;

                            case 1:
                                Main.Tile[i, k].FrameX = 0;
                                Main.Tile[i, k].FrameY = 0x16;
                                break;

                            case 2:
                                Main.Tile[i, k].FrameX = 0;
                                Main.Tile[i, k].FrameY = 0x2c;
                                break;
                        }
                    }

                    if (num4 == 5 || num4 == 7)
                    {
                        Main.Tile[i - 1, k].Active = true;
                        Main.Tile[i - 1, k].Type = 5;
                        num3 = Main.Rand.Next(3);
                        if (Main.Rand.Next(3) < 2)
                        {
                            switch (num3)
                            {
                                case 0:
                                    Main.Tile[i - 1, k].FrameX = 0x2c;
                                    Main.Tile[i - 1, k].FrameY = 0xc6;
                                    break;

                                case 1:
                                    Main.Tile[i - 1, k].FrameX = 0x2c;
                                    Main.Tile[i - 1, k].FrameY = 220;
                                    break;

                                case 2:
                                    Main.Tile[i - 1, k].FrameX = 0x2c;
                                    Main.Tile[i - 1, k].FrameY = 0xf2;
                                    break;
                            }
                        }
                        else
                        {
                            switch (num3)
                            {
                                case 0:
                                    Main.Tile[i - 1, k].FrameX = 0x42;
                                    Main.Tile[i - 1, k].FrameY = 0;
                                    break;

                                case 1:
                                    Main.Tile[i - 1, k].FrameX = 0x42;
                                    Main.Tile[i - 1, k].FrameY = 0x16;
                                    break;

                                case 2:
                                    Main.Tile[i - 1, k].FrameX = 0x42;
                                    Main.Tile[i - 1, k].FrameY = 0x2c;
                                    break;
                            }
                        }
                    }

                    if (num4 == 6 || num4 == 7)
                    {
                        Main.Tile[i + 1, k].Active = true;
                        Main.Tile[i + 1, k].Type = 5;
                        num3 = Main.Rand.Next(3);
                        if (Main.Rand.Next(3) < 2)
                        {
                            switch (num3)
                            {
                                case 0:
                                    Main.Tile[i + 1, k].FrameX = 0x42;
                                    Main.Tile[i + 1, k].FrameY = 0xc6;
                                    break;

                                case 1:
                                    Main.Tile[i + 1, k].FrameX = 0x42;
                                    Main.Tile[i + 1, k].FrameY = 220;
                                    break;

                                case 2:
                                    Main.Tile[i + 1, k].FrameX = 0x42;
                                    Main.Tile[i + 1, k].FrameY = 0xf2;
                                    break;
                            }
                        }
                        else
                        {
                            switch (num3)
                            {
                                case 0:
                                    Main.Tile[i + 1, k].FrameX = 0x58;
                                    Main.Tile[i + 1, k].FrameY = 0x42;
                                    break;

                                case 1:
                                    Main.Tile[i + 1, k].FrameX = 0x58;
                                    Main.Tile[i + 1, k].FrameY = 0x58;
                                    break;

                                case 2:
                                    Main.Tile[i + 1, k].FrameX = 0x58;
                                    Main.Tile[i + 1, k].FrameY = 110;
                                    break;
                            }
                        }
                    }
                }

                var num7 = Main.Rand.Next(3);
                switch (num7)
                {
                    case 0:
                    case 1:
                        Main.Tile[i + 1, j - 1].Active = true;
                        Main.Tile[i + 1, j - 1].Type = 5;
                        num3 = Main.Rand.Next(3);
                        if (num3 == 0)
                        {
                            Main.Tile[i + 1, j - 1].FrameX = 0x16;
                            Main.Tile[i + 1, j - 1].FrameY = 0x84;
                        }

                        if (num3 == 1)
                        {
                            Main.Tile[i + 1, j - 1].FrameX = 0x16;
                            Main.Tile[i + 1, j - 1].FrameY = 0x9a;
                        }

                        if (num3 == 2)
                        {
                            Main.Tile[i + 1, j - 1].FrameX = 0x16;
                            Main.Tile[i + 1, j - 1].FrameY = 0xb0;
                        }

                        break;
                }

                if (num7 == 0 || num7 == 2)
                {
                    Main.Tile[i - 1, j - 1].Active = true;
                    Main.Tile[i - 1, j - 1].Type = 5;
                    num3 = Main.Rand.Next(3);
                    switch (num3)
                    {
                        case 0:
                            Main.Tile[i - 1, j - 1].FrameX = 0x2c;
                            Main.Tile[i - 1, j - 1].FrameY = 0x84;
                            break;

                        case 1:
                            Main.Tile[i - 1, j - 1].FrameX = 0x2c;
                            Main.Tile[i - 1, j - 1].FrameY = 0x9a;
                            break;

                        case 2:
                            Main.Tile[i - 1, j - 1].FrameX = 0x2c;
                            Main.Tile[i - 1, j - 1].FrameY = 0xb0;
                            break;
                    }
                }

                num3 = Main.Rand.Next(3);
                if (num7 == 0)
                {
                    switch (num3)
                    {
                        case 0:
                            Main.Tile[i, j - 1].FrameX = 0x58;
                            Main.Tile[i, j - 1].FrameY = 0x84;
                            break;

                        case 1:
                            Main.Tile[i, j - 1].FrameX = 0x58;
                            Main.Tile[i, j - 1].FrameY = 0x9a;
                            break;

                        case 2:
                            Main.Tile[i, j - 1].FrameX = 0x58;
                            Main.Tile[i, j - 1].FrameY = 0xb0;
                            break;
                    }
                }
                else if (num7 == 1)
                {
                    switch (num3)
                    {
                        case 0:
                            Main.Tile[i, j - 1].FrameX = 0;
                            Main.Tile[i, j - 1].FrameY = 0x84;
                            break;

                        case 1:
                            Main.Tile[i, j - 1].FrameX = 0;
                            Main.Tile[i, j - 1].FrameY = 0x9a;
                            break;

                        case 2:
                            Main.Tile[i, j - 1].FrameX = 0;
                            Main.Tile[i, j - 1].FrameY = 0xb0;
                            break;
                    }
                }
                else if (num7 == 2)
                {
                    switch (num3)
                    {
                        case 0:
                            Main.Tile[i, j - 1].FrameX = 0x42;
                            Main.Tile[i, j - 1].FrameY = 0x84;
                            break;

                        case 1:
                            Main.Tile[i, j - 1].FrameX = 0x42;
                            Main.Tile[i, j - 1].FrameY = 0x9a;
                            break;

                        case 2:
                            Main.Tile[i, j - 1].FrameX = 0x42;
                            Main.Tile[i, j - 1].FrameY = 0xb0;
                            break;
                    }
                }

                if (Main.Rand.Next(3) < 2)
                {
                    switch (Main.Rand.Next(3))
                    {
                        case 0:
                            Main.Tile[i, j - num5].FrameX = 0x16;
                            Main.Tile[i, j - num5].FrameY = 0xc6;
                            break;

                        case 1:
                            Main.Tile[i, j - num5].FrameX = 0x16;
                            Main.Tile[i, j - num5].FrameY = 220;
                            break;

                        case 2:
                            Main.Tile[i, j - num5].FrameX = 0x16;
                            Main.Tile[i, j - num5].FrameY = 0xf2;
                            break;
                    }
                }
                else
                {
                    switch (Main.Rand.Next(3))
                    {
                        case 0:
                            Main.Tile[i, j - num5].FrameX = 0;
                            Main.Tile[i, j - num5].FrameY = 0xc6;
                            break;

                        case 1:
                            Main.Tile[i, j - num5].FrameX = 0;
                            Main.Tile[i, j - num5].FrameY = 220;
                            break;

                        case 2:
                            Main.Tile[i, j - num5].FrameX = 0;
                            Main.Tile[i, j - num5].FrameY = 0xf2;
                            break;
                    }
                }
            }
    }

    public static void CloseDoor(int i, int j)
    {
        var num = 0;
        var num2 = i;
        var num3 = j;
        int frameX = Main.Tile[i, j].FrameX;
        int frameY = Main.Tile[i, j].FrameY;
        if (frameX == 0)
        {
            num2 = i;
            num = 1;
        }
        else if (frameX == 0x12)
        {
            num2 = i - 1;
            num = 1;
        }
        else if (frameX == 0x24)
        {
            num2 = i + 1;
            num = -1;
        }
        else if (frameX == 0x36)
        {
            num2 = i;
            num = -1;
        }

        switch (frameY)
        {
            case 0:
                num3 = j;
                break;

            case 0x12:
                num3 = j - 1;
                break;

            case 0x24:
                num3 = j - 2;
                break;
        }

        var num6 = num2;
        if (num == -1)
            num6 = num2 - 1;

        for (var k = num6; k < num6 + 2; k++)
        for (var m = num3; m < num3 + 3; m++)
            if (k == num2)
            {
                Main.Tile[k, m].Type = 10;
                Main.Tile[k, m].FrameX = (short)(Main.Rand.Next(3) * 0x12);
            }
            else
                Main.Tile[k, m].Active = false;

        Main.soundInstanceDoorClosed.Stop();
        Main.soundInstanceDoorClosed = Main.soundDoorClosed.CreateInstance();
        Main.soundInstanceDoorClosed.Play();
    }

    private static bool EmptyTileCheck(int startX, int endX, int startY, int endY)
    {
        if (startX < 0)
            return false;

        if (endX >= 0x1389)
            return false;

        if (startY < 0)
            return false;

        if (endY >= 0x9c5)
            return false;

        for (var i = startX; i < endX + 1; i++)
        for (var j = startY; j < endY + 1; j++)
            if (Main.Tile[i, j].Active)
                return false;

        return true;
    }

    private static void EveryTileFrame()
    {
        for (var i = 0; i < 0x1389; i++)
        for (var j = 0; j < 0x9c5; j++)
            TileFrame(i, j, true);
    }

    public static void GenerateWorld()
    {
        int num10;
        int num11;
        int num13;
        var num7 = 0;
        var num8 = 0;
        var num = 875.34999999999991;
        num *= Main.Rand.Next(90, 110) * 0.01;
        var num4 = num + 375.15;
        num4 *= Main.Rand.Next(90, 110) * 0.01;
        var num2 = num;
        var num3 = num;
        var num5 = num4;
        var num6 = num4;
        var num9 = 0;
        while (num9 < 0x1389)
        {
            if (num < num2)
                num2 = num;

            if (num > num3)
                num3 = num;

            if (num4 < num5)
                num5 = num4;

            if (num4 > num6)
                num6 = num4;

            if (num8 <= 0)
            {
                num7 = Main.Rand.Next(0, 5);
                num8 = Main.Rand.Next(5, 40);
                if (num7 == 0)
                    num8 *= (int)(Main.Rand.Next(15, 30) * 0.1);
            }

            num8--;
            switch (num7)
            {
                case 0:
                    while (Main.Rand.Next(0, 7) == 0)
                        num += Main.Rand.Next(-1, 2);

                    break;

                case 1:
                    while (Main.Rand.Next(0, 4) == 0)
                        num--;

                    while (Main.Rand.Next(0, 10) == 0)
                        num++;

                    break;

                case 2:
                    while (Main.Rand.Next(0, 4) == 0)
                        num++;

                    while (Main.Rand.Next(0, 10) == 0)
                        num--;

                    break;

                case 3:
                    while (Main.Rand.Next(0, 2) == 0)
                        num--;

                    while (Main.Rand.Next(0, 6) == 0)
                        num++;

                    break;

                case 4:
                    while (Main.Rand.Next(0, 2) == 0)
                        num++;

                    while (Main.Rand.Next(0, 5) == 0)
                        num--;

                    break;
            }

            if (num < 125.05000000000001)
            {
                num = 125.05000000000001;
                num8 = 0;
            }
            else if (num > 2375.95)
            {
                num = 2375.95;
                num8 = 0;
            }

            while (Main.Rand.Next(0, 3) == 0)
                num4 += Main.Rand.Next(-2, 3);

            if (num4 < num + 125.05000000000001)
                num4++;

            if (num4 > num + 625.25)
                num4--;

            num10 = 0;
            while (num10 < num)
            {
                Main.Tile[num9, num10].Active = false;
                Main.Tile[num9, num10].Lighted = true;
                num10++;
            }

            num10 = (int)num;
            while (num10 < 0x9c5)
            {
                if (num10 < num4)
                {
                    Main.Tile[num9, num10].Active = true;
                    Main.Tile[num9, num10].Type = 0;
                    Main.Tile[num9, num10].FrameX = -1;
                    Main.Tile[num9, num10].FrameY = -1;
                }
                else
                {
                    Main.Tile[num9, num10].Active = true;
                    Main.Tile[num9, num10].Type = 1;
                    Main.Tile[num9, num10].FrameX = -1;
                    Main.Tile[num9, num10].FrameY = -1;
                }

                num10++;
            }

            num9++;
        }

        Main.worldSurface = num3;
        for (num11 = 0; num11 < 0x61b7; num11++)
            TileRunner(
                Main.Rand.Next(0, 0x1389),
                Main.Rand.Next((int)num2, (int)num6 + 1),
                Main.Rand.Next(2, 7),
                Main.Rand.Next(2, 0x17),
                1
            );

        for (num11 = 0; num11 < 0xf449; num11++)
            TileRunner(
                Main.Rand.Next(0, 0x1389),
                Main.Rand.Next((int)num5, 0x9c5),
                Main.Rand.Next(2, 6),
                Main.Rand.Next(2, 40),
                0
            );

        for (num11 = 0; num11 < 0x30db; num11++)
            TileRunner(
                Main.Rand.Next(0, 0x1389),
                Main.Rand.Next((int)num3, 0x9c5),
                Main.Rand.Next(2, 5),
                Main.Rand.Next(2, 20),
                -1
            );

        for (num11 = 0; num11 < 0x30db; num11++)
            TileRunner(
                Main.Rand.Next(0, 0x1389),
                Main.Rand.Next((int)num3, 0x9c5),
                Main.Rand.Next(8, 15),
                Main.Rand.Next(7, 30),
                -1
            );

        for (num11 = 0; num11 < 0x9c5; num11++)
            if (num6 <= 2501.0)
            {
                TileRunner(
                    Main.Rand.Next(0, 0x1389),
                    Main.Rand.Next((int)num6, 0x9c5),
                    Main.Rand.Next(10, 20),
                    Main.Rand.Next(50, 300),
                    -1
                );
            }

        var i = 0;
        for (num11 = 0; num11 < 0x19; num11++)
        {
            i = Main.Rand.Next(0, 0x1389);
            num13 = 0;
            while (num13 < num3)
            {
                if (Main.Tile[i, num13].Active)
                {
                    TileRunner(
                        i,
                        num13,
                        Main.Rand.Next(3, 6),
                        Main.Rand.Next(5, 50),
                        -1,
                        false,
                        Main.Rand.Next(-10, 11) * 0.1f,
                        1f
                    );

                    break;
                }

                num13++;
            }
        }

        for (num11 = 0; num11 < 40; num11++)
        {
            i = Main.Rand.Next(0, 0x1389);
            num13 = 0;
            while (num13 < num3)
            {
                if (Main.Tile[i, num13].Active)
                {
                    TileRunner(
                        i,
                        num13,
                        Main.Rand.Next(10, 15),
                        Main.Rand.Next(50, 130),
                        -1,
                        false,
                        Main.Rand.Next(-10, 11) * 0.1f,
                        1f
                    );

                    break;
                }

                num13++;
            }
        }

        for (num11 = 0; num11 < 10; num11++)
        {
            i = Main.Rand.Next(0, 0x1389);
            num13 = 0;
            while (num13 < num3)
            {
                if (Main.Tile[i, num13].Active)
                {
                    TileRunner(
                        i,
                        num13,
                        Main.Rand.Next(12, 0x19),
                        Main.Rand.Next(100, 400),
                        -1,
                        false,
                        Main.Rand.Next(-10, 11) * 0.1f,
                        3f
                    );

                    TileRunner(
                        i,
                        num13,
                        Main.Rand.Next(8, 0x11),
                        Main.Rand.Next(60, 200),
                        -1,
                        false,
                        Main.Rand.Next(-10, 11) * 0.1f,
                        2f
                    );

                    TileRunner(
                        i,
                        num13,
                        Main.Rand.Next(5, 13),
                        Main.Rand.Next(40, 170),
                        -1,
                        false,
                        Main.Rand.Next(-10, 11) * 0.1f,
                        2f
                    );

                    break;
                }

                num13++;
            }
        }

        for (num11 = 0; num11 < 0x30db; num11++)
        {
            var num14 = Main.Rand.Next(1, 0x1388);
            var num15 = Main.Rand.Next((int)num2, (int)num3);
            if (num15 >= 0x9c5)
                num15 = 0x9c3;

            if (
                Main.Tile[num14 - 1, num15].Active
                && Main.Tile[num14 - 1, num15].Type == 0
                && Main.Tile[num14 + 1, num15].Active
                && Main.Tile[num14 + 1, num15].Type == 0
                && Main.Tile[num14, num15 - 1].Active
                && Main.Tile[num14, num15 - 1].Type == 0 && Main.Tile[num14, num15 + 1].Active &&
                Main.Tile[num14, num15 + 1].Type == 0
            )
            {
                Main.Tile[num14, num15].Active = true;
                Main.Tile[num14, num15].Type = 2;
            }
        }

        for (num11 = 0; num11 < 0x271; num11++)
            TileRunner(
                Main.Rand.Next(0, 0x1389),
                Main.Rand.Next((int)num2, (int)num3),
                Main.Rand.Next(3, 4),
                Main.Rand.Next(2, 5),
                7
            );

        for (num11 = 0; num11 < 750; num11++)
            TileRunner(
                Main.Rand.Next(0, 0x1389),
                Main.Rand.Next((int)num3, (int)num6),
                Main.Rand.Next(3, 6),
                Main.Rand.Next(3, 6),
                7
            );

        for (num11 = 0; num11 < 0xea8; num11++)
            TileRunner(
                Main.Rand.Next(0, 0x1389),
                Main.Rand.Next((int)num5, 0x9c5),
                Main.Rand.Next(4, 9),
                Main.Rand.Next(4, 8),
                7
            );

        for (num11 = 0; num11 < 500; num11++)
            TileRunner(
                Main.Rand.Next(0, 0x1389),
                Main.Rand.Next((int)num2, (int)num3),
                Main.Rand.Next(3, 4),
                Main.Rand.Next(2, 5),
                6
            );

        for (num11 = 0; num11 < 0x271; num11++)
            TileRunner(
                Main.Rand.Next(0, 0x1389),
                Main.Rand.Next((int)num3, (int)num6),
                Main.Rand.Next(3, 6),
                Main.Rand.Next(3, 6),
                6
            );

        for (num11 = 0; num11 < 0x9c5; num11++)
            TileRunner(
                Main.Rand.Next(0, 0x1389),
                Main.Rand.Next((int)num5, 0x9c5),
                Main.Rand.Next(4, 9),
                Main.Rand.Next(4, 8),
                6
            );

        for (num11 = 0; num11 < 0x7d; num11++)
            TileRunner(
                Main.Rand.Next(0, 0x1389),
                Main.Rand.Next((int)num2, (int)num3),
                Main.Rand.Next(3, 4),
                Main.Rand.Next(2, 5),
                9
            );

        for (num11 = 0; num11 < 250; num11++)
            TileRunner(
                Main.Rand.Next(0, 0x1389),
                Main.Rand.Next((int)num3, (int)num6),
                Main.Rand.Next(3, 6),
                Main.Rand.Next(3, 6),
                9
            );

        for (num11 = 0; num11 < 0x4e2; num11++)
            TileRunner(
                Main.Rand.Next(0, 0x1389),
                Main.Rand.Next((int)num5, 0x9c5),
                Main.Rand.Next(4, 9),
                Main.Rand.Next(4, 8),
                9
            );

        for (num11 = 0; num11 < 0x3e; num11++)
            TileRunner(
                Main.Rand.Next(0, 0x1389),
                Main.Rand.Next((int)num2, (int)num3),
                Main.Rand.Next(3, 4),
                Main.Rand.Next(2, 5),
                8
            );

        for (num11 = 0; num11 < 0x7d; num11++)
            TileRunner(
                Main.Rand.Next(0, 0x1389),
                Main.Rand.Next((int)num3, (int)num6),
                Main.Rand.Next(3, 6),
                Main.Rand.Next(3, 6),
                8
            );

        for (num11 = 0; num11 < 0x271; num11++)
            TileRunner(
                Main.Rand.Next(0, 0x1389),
                Main.Rand.Next((int)num5, 0x9c5),
                Main.Rand.Next(4, 9),
                Main.Rand.Next(4, 8),
                8
            );

        for (num11 = 0; num11 < 0x1389; num11++)
        {
            i = num11;
            for (num13 = 0; num13 < Main.worldSurface - 1.0; num13++)
                if (Main.Tile[i, num13].Active)
                {
                    if (Main.Tile[i, num13].Type == 0)
                        SpreadGrass(i, num13);

                    break;
                }
        }

        for (num10 = 0; num10 < 0x9c5; num10++)
        {
            num9 = 0x9c4;
            if (Main.Tile[num9, num10].Active)
            {
                Main.spawnTileX = num9;
                Main.spawnTileY = num10;
                Main.Player[0].Spawn();
                break;
            }
        }

        AddTrees();
        EveryTileFrame();
        AddPlants();
    }

    public static void KillTile(int i, int j, bool fail = false)
    {
        if (
            i >= 0 && j >= 0 && i < 0x1389 && j < 0x9c5
            && Main.Tile[i, j].Active
            && (
                j < 1 || !Main.Tile[i, j - 1].Active
                      || Main.Tile[i, j - 1].Type != 5 || Main.Tile[i, j].Type == 5
                      || (
                          Main.Tile[i, j - 1].FrameX == 0x42
                          && Main.Tile[i, j - 1].FrameY >= 0 && Main.Tile[i, j - 1].FrameY <= 0x2c
                      )
                      || (
                          Main.Tile[i, j - 1].FrameX == 0x58
                          && Main.Tile[i, j - 1].FrameY >= 0x42 && Main.Tile[i, j - 1].FrameY <= 110
                      ) || Main.Tile[i, j - 1].FrameY >= 0xc6
            )
        )
        {
            if (Main.Tile[i, j].Type == 3)
            {
                Main.soundInstanceGrass.Stop();
                Main.soundInstanceGrass = Main.soundGrass.CreateInstance();
                Main.soundInstanceGrass.Play();
                if (Main.Tile[i, j].FrameX == 0x90)
                    Item.NewItem(i * 0x10, j * 0x10, 0x10, 0x10, 5);
            }
            else
            {
                var index = Main.Rand.Next(3);
                Main.SoundInstanceDig[index].Stop();
                Main.SoundInstanceDig[index] = Main.SoundDig[index].CreateInstance();
                Main.SoundInstanceDig[index].Play();
            }

            var num2 = 10;
            if (fail)
                num2 = 3;

            for (var k = 0; k < num2; k++)
            {
                var type = 0;
                if (Main.Tile[i, j].Type == 0)
                    type = 0;

                if (Main.Tile[i, j].Type == 1)
                    type = 1;

                if (Main.Tile[i, j].Type == 4)
                    type = 6;

                if (
                    Main.Tile[i, j].Type == 5 || Main.Tile[i, j].Type == 10
                                              || Main.Tile[i, j].Type == 11
                )
                    type = 7;

                if (Main.Tile[i, j].Type == 2)
                    type = Main.Rand.Next(2) == 0 ? 0 : 2;

                if (Main.Tile[i, j].Type == 6)
                    type = 8;

                if (Main.Tile[i, j].Type == 7)
                    type = 9;

                if (Main.Tile[i, j].Type == 8)
                    type = 10;

                if (Main.Tile[i, j].Type == 9)
                    type = 11;

                if (Main.Tile[i, j].Type == 3)
                    type = 3;

                Color newColor = new Color();
                Dust.NewDust(
                    new Vector2(i * 0x10, j * 0x10),
                    0x10,
                    0x10,
                    type,
                    0f,
                    0f,
                    0,
                    newColor
                );
            }

            if (fail)
            {
                if (Main.Tile[i, j].Type == 2)
                    Main.Tile[i, j].Type = 0;

                SquareTileFrame(i, j);
            }
            else
            {
                var num5 = 0;
                if (Main.Tile[i, j].Type == 0 || Main.Tile[i, j].Type == 2)
                    num5 = 2;
                else if (Main.Tile[i, j].Type == 1)
                    num5 = 3;
                else if (Main.Tile[i, j].Type == 4)
                    num5 = 8;
                else if (Main.Tile[i, j].Type == 5)
                    num5 = 9;
                else if (Main.Tile[i, j].Type == 6)
                    num5 = 11;
                else if (Main.Tile[i, j].Type == 7)
                    num5 = 12;
                else if (Main.Tile[i, j].Type == 8)
                    num5 = 13;
                else if (Main.Tile[i, j].Type == 9)
                    num5 = 14;

                if (num5 > 0)
                    Item.NewItem(i * 0x10, j * 0x10, 0x10, 0x10, num5);

                Main.Tile[i, j].Active = false;
                Main.Tile[i, j].Lighted = false;
                Main.Tile[i, j].FrameX = -1;
                Main.Tile[i, j].FrameY = -1;
                Main.Tile[i, j].FrameNumber = 0;
                Main.Tile[i, j].Type = 0;
                SquareTileFrame(i, j);
            }
        }
    }

    public static void KillWall(int i, int j, bool fail = false)
    {
        if (
            i >= 0 && j >= 0 && i < 0x1389 && j < 0x9c5
            && Main.Tile[i, j].Wall > 0
        )
        {
            var index = Main.Rand.Next(3);
            Main.SoundInstanceDig[index].Stop();
            Main.SoundInstanceDig[index] = Main.SoundDig[index].CreateInstance();
            Main.SoundInstanceDig[index].Play();
            var num2 = 10;
            if (fail)
                num2 = 3;

            for (var k = 0; k < num2; k++)
            {
                var type = 0;
                if (Main.Tile[i, j].Wall == 1)
                    type = 1;

                Color newColor = new Color();
                Dust.NewDust(
                    new Vector2(i * 0x10, j * 0x10),
                    0x10,
                    0x10,
                    type,
                    0f,
                    0f,
                    0,
                    newColor
                );
            }

            if (fail)
                SquareWallFrame(i, j);
            else
            {
                var num5 = 0;
                if (Main.Tile[i, j].Wall == 1)
                    num5 = 0x1a;

                if (num5 > 0)
                    Item.NewItem(i * 0x10, j * 0x10, 0x10, 0x10, num5);

                Main.Tile[i, j].Wall = 0;
                SquareWallFrame(i, j);
            }
        }
    }

    public static void OpenDoor(int i, int j, int direction)
    {
        int num3;
        var num = 0;
        if (
            Main.Tile[i, j - 1].FrameY == 0
            && Main.Tile[i, j - 1].Type == Main.Tile[i, j].Type
        )
            num = j - 1;
        else if (
            Main.Tile[i, j - 2].FrameY == 0
            && Main.Tile[i, j - 2].Type == Main.Tile[i, j].Type
        )
            num = j - 2;
        else if (
            Main.Tile[i, j + 1].FrameY == 0
            && Main.Tile[i, j + 1].Type == Main.Tile[i, j].Type
        )
            num = j + 1;
        else
            num = j;

        var num2 = i;
        short num4 = 0;
        if (direction == -1)
        {
            num2 = i - 1;
            num4 = 0x24;
            num3 = i - 1;
        }
        else
        {
            num2 = i;
            num3 = i + 1;
        }

        var flag = true;
        for (var k = num; k < num + 3; k++)
            if (Main.Tile[num3, k].Active)
            {
                if (Main.Tile[num3, k].Type == 3)
                    KillTile(num3, k);
                else
                {
                    flag = false;
                    break;
                }
            }

        if (flag)
        {
            Main.soundInstanceDoorOpen.Stop();
            Main.soundInstanceDoorOpen = Main.soundDoorOpen.CreateInstance();
            Main.soundInstanceDoorOpen.Play();
            Main.Tile[num2, num].Active = true;
            Main.Tile[num2, num].Type = 11;
            Main.Tile[num2, num].FrameY = 0;
            Main.Tile[num2, num].FrameX = num4;
            Main.Tile[num2 + 1, num].Active = true;
            Main.Tile[num2 + 1, num].Type = 11;
            Main.Tile[num2 + 1, num].FrameY = 0;
            Main.Tile[num2 + 1, num].FrameX = (short)(num4 + 0x12);
            Main.Tile[num2, num + 1].Active = true;
            Main.Tile[num2, num + 1].Type = 11;
            Main.Tile[num2, num + 1].FrameY = 0x12;
            Main.Tile[num2, num + 1].FrameX = num4;
            Main.Tile[num2 + 1, num + 1].Active = true;
            Main.Tile[num2 + 1, num + 1].Type = 11;
            Main.Tile[num2 + 1, num + 1].FrameY = 0x12;
            Main.Tile[num2 + 1, num + 1].FrameX = (short)(num4 + 0x12);
            Main.Tile[num2, num + 2].Active = true;
            Main.Tile[num2, num + 2].Type = 11;
            Main.Tile[num2, num + 2].FrameY = 0x24;
            Main.Tile[num2, num + 2].FrameX = num4;
            Main.Tile[num2 + 1, num + 2].Active = true;
            Main.Tile[num2 + 1, num + 2].Type = 11;
            Main.Tile[num2 + 1, num + 2].FrameY = 0x24;
            Main.Tile[num2 + 1, num + 2].FrameX = (short)(num4 + 0x12);
        }
    }

    private static void PlaceDoor(int i, int j)
    {
        if (
            Main.Tile[i, j - 2].Active && Main.TileSolid[Main.Tile[i, j - 2].Type]
                                       && Main.Tile[i, j + 2].Active && Main.TileSolid[Main.Tile[i, j + 2].Type]
        )
        {
            Main.Tile[i, j - 1].Active = true;
            Main.Tile[i, j - 1].Type = 10;
            Main.Tile[i, j - 1].FrameY = 0;
            Main.Tile[i, j - 1].FrameX = (short)(Main.Rand.Next(3) * 0x12);
            Main.Tile[i, j].Active = true;
            Main.Tile[i, j].Type = 10;
            Main.Tile[i, j].FrameY = 0x12;
            Main.Tile[i, j].FrameX = (short)(Main.Rand.Next(3) * 0x12);
            Main.Tile[i, j + 1].Active = true;
            Main.Tile[i, j + 1].Type = 10;
            Main.Tile[i, j + 1].FrameY = 0x24;
            Main.Tile[i, j + 1].FrameX = (short)(Main.Rand.Next(3) * 0x12);
        }
    }

    public static void PlaceTile(int i, int j, int type, bool mute = false)
    {
        if (
            i >= 0 && j >= 0 && i < 0x1389 && j < 0x9c5
            && Collision.EmptyTile(i, j)
        )
        {
            Main.Tile[i, j].FrameY = 0;
            Main.Tile[i, j].FrameX = 0;
            if (type == 3)
            {
                if (
                    j + 1 < 0x9c5 && Main.Tile[i, j + 1].Type == 2
                                  && Main.Tile[i, j + 1].Active
                )
                {
                    if (Main.Rand.Next(50) == 0)
                    {
                        Main.Tile[i, j].Active = true;
                        Main.Tile[i, j].Type = 3;
                        Main.Tile[i, j].FrameX = 0x90;
                    }
                    else if (Main.Rand.Next(0x23) == 0)
                    {
                        Main.Tile[i, j].Active = true;
                        Main.Tile[i, j].Type = 3;
                        Main.Tile[i, j].FrameX = (short)(Main.Rand.Next(2) * 0x12 + 0x6c);
                    }
                    else
                    {
                        Main.Tile[i, j].Active = true;
                        Main.Tile[i, j].Type = 3;
                        Main.Tile[i, j].FrameX = (short)(Main.Rand.Next(6) * 0x12);
                    }
                }
            }
            else if (type == 4)
            {
                if (
                    (
                        Main.Tile[i - 1, j].Active
                        && (
                            Main.TileSolid[Main.Tile[i - 1, j].Type]
                            || (
                                Main.Tile[i - 1, j].Type == 5
                                && Main.Tile[i - 1, j - 1].Type == 5 && Main.Tile[i - 1, j + 1].Type == 5
                            )
                        )
                    )
                    || (
                        Main.Tile[i + 1, j].Active
                        && (
                            Main.TileSolid[Main.Tile[i + 1, j].Type]
                            || (
                                Main.Tile[i + 1, j].Type == 5
                                && Main.Tile[i + 1, j - 1].Type == 5 && Main.Tile[i + 1, j + 1].Type == 5
                            )
                        )
                    )
                    || (Main.Tile[i, j + 1].Active && Main.TileSolid[Main.Tile[i, j + 1].Type])
                )
                {
                    Main.Tile[i, j].Active = true;
                    Main.Tile[i, j].Type = (byte)type;
                    SquareTileFrame(i, j);
                }
            }
            else if (type == 10)
            {
                if (
                    Main.Tile[i, j - 1].Active || Main.Tile[i, j - 2].Active
                                               || !Main.Tile[i, j - 3].Active || !Main.TileSolid[Main.Tile[i, j - 3].Type]
                )
                {
                    if (
                        Main.Tile[i, j + 1].Active || Main.Tile[i, j + 2].Active
                                                   || !Main.Tile[i, j + 3].Active || !Main.TileSolid[Main.Tile[i, j + 3].Type]
                    )
                        return;

                    PlaceDoor(i, j + 1);
                    SquareTileFrame(i, j);
                }
                else
                {
                    PlaceDoor(i, j - 1);
                    SquareTileFrame(i, j);
                }
            }
            else
            {
                Main.Tile[i, j].Active = true;
                Main.Tile[i, j].Type = (byte)type;
                SquareTileFrame(i, j);
            }

            if (!(!Main.Tile[i, j].Active || mute))
            {
                var index = Main.Rand.Next(3);
                Main.SoundInstanceDig[index].Stop();
                Main.SoundInstanceDig[index] = Main.SoundDig[index].CreateInstance();
                Main.SoundInstanceDig[index].Play();
            }
        }
    }

    public static void PlaceWall(int i, int j, int type, bool mute = false)
    {
        if (Main.Tile[i, j].Wall != type)
        {
            Main.Tile[i, j].Wall = (byte)type;
            SquareWallFrame(i, j);
            if (!mute)
            {
                var index = Main.Rand.Next(3);
                Main.SoundInstanceDig[index].Stop();
                Main.SoundInstanceDig[index] = Main.SoundDig[index].CreateInstance();
                Main.SoundInstanceDig[index].Play();
            }
        }
    }

    private static void PlantCheck(int i, int j)
    {
        var num7 = -1;
        int type = Main.Tile[i, j].Type;

        if (j + 1 >= 0x9c5)
            num7 = type;

        if (j + 1 < 0x9c5 && Main.Tile[i, j + 1].Active)
            num7 = Main.Tile[i, j + 1].Type;

        if (num7 != 2)
            KillTile(i, j);
    }

    private static void SpreadGrass(
        int i,
        int j,
        int dirt = 0,
        int grass = 2,
        bool repeat = true
    )
    {
        if (
            Main.Tile[i, j].Type == dirt && Main.Tile[i, j].Active
                                         && j < Main.worldSurface
        )
        {
            int num5;
            int num6;
            var num = i - 1;
            var num2 = i + 2;
            var num3 = j - 1;
            var num4 = j + 2;
            if (num < 0)
                num = 0;

            if (num2 > 0x1389)
                num2 = 0x1389;

            if (num3 < 0)
                num3 = 0;

            if (num4 > 0x9c5)
                num4 = 0x9c5;

            var flag = true;
            for (num5 = num; num5 < num2; num5++)
            {
                num6 = num3;
                while (num6 < num4)
                {
                    if (!Main.Tile[num5, num6].Active)
                    {
                        flag = false;
                        goto Label_00E4;
                    }

                    num6++;
                }

                Label_00E4: ;
            }

            if (!flag)
            {
                Main.Tile[i, j].Type = (byte)grass;
                for (num5 = num; num5 < num2; num5++)
                for (num6 = num3; num6 < num4; num6++)
                    if (
                        Main.Tile[num5, num6].Active
                        && Main.Tile[num5, num6].Type == dirt && repeat
                    )
                        SpreadGrass(num5, num6, dirt, grass);
            }
        }
    }

    public static void SquareTileFrame(int i, int j, bool resetFrame = true)
    {
        TileFrame(i - 1, j - 1);
        TileFrame(i - 1, j);
        TileFrame(i - 1, j + 1);
        TileFrame(i, j - 1);
        TileFrame(i, j, resetFrame);
        TileFrame(i, j + 1);
        TileFrame(i + 1, j - 1);
        TileFrame(i + 1, j);
        TileFrame(i + 1, j + 1);
    }

    private static void SquareWallFrame(int i, int j, bool resetFrame = true)
    {
        WallFrame(i - 1, j - 1);
        WallFrame(i - 1, j);
        WallFrame(i - 1, j + 1);
        WallFrame(i, j - 1);
        WallFrame(i, j, resetFrame);
        WallFrame(i, j + 1);
        WallFrame(i + 1, j - 1);
        WallFrame(i + 1, j);
        WallFrame(i + 1, j + 1);
    }

    private static void TileFrame(int i, int j, bool resetFrame = false)
    {
        if (i >= 0 && j >= 0 && i < 0x1389 && j < 0x9c5 && Main.Tile[i, j].Active)
        {
            Rectangle rectangle = default;
            var num = -1;
            var num2 = -1;
            var num3 = -1;
            var index = -1;
            var num5 = -1;
            var num6 = -1;
            var num7 = -1;
            var num8 = -1;
            int type = Main.Tile[i, j].Type;
            int frameX = Main.Tile[i, j].FrameX;
            int frameY = Main.Tile[i, j].FrameY;
            rectangle.X = -1;
            rectangle.Y = -1;
            if (type == 3)
                PlantCheck(i, j);
            else
            {
                int num12;
                int num13;
                bool flag;
                int num21;
                mergeUp = false;
                mergeDown = false;
                mergeLeft = false;
                mergeRight = false;
                if (i - 1 < 0)
                {
                    num = type;
                    index = type;
                    num6 = type;
                }

                if (i + 1 >= 0x1389)
                {
                    num3 = type;
                    num5 = type;
                    num8 = type;
                }

                if (j - 1 < 0)
                {
                    num = type;
                    num2 = type;
                    num3 = type;
                }

                if (j + 1 >= 0x9c5)
                {
                    num6 = type;
                    num7 = type;
                    num8 = type;
                }

                if (i - 1 >= 0 && Main.Tile[i - 1, j].Active)
                    index = Main.Tile[i - 1, j].Type;

                if (i + 1 < 0x1389 && Main.Tile[i + 1, j].Active)
                    num5 = Main.Tile[i + 1, j].Type;

                if (j - 1 >= 0 && Main.Tile[i, j - 1].Active)
                    num2 = Main.Tile[i, j - 1].Type;

                if (j + 1 < 0x9c5 && Main.Tile[i, j + 1].Active)
                    num7 = Main.Tile[i, j + 1].Type;

                if (i - 1 >= 0 && j - 1 >= 0 && Main.Tile[i - 1, j - 1].Active)
                    num = Main.Tile[i - 1, j - 1].Type;

                if (i + 1 < 0x1389 && j - 1 >= 0 && Main.Tile[i + 1, j - 1].Active)
                    num3 = Main.Tile[i + 1, j - 1].Type;

                if (i - 1 >= 0 && j + 1 < 0x9c5 && Main.Tile[i - 1, j + 1].Active)
                    num6 = Main.Tile[i - 1, j + 1].Type;

                if (i + 1 < 0x1389 && j + 1 < 0x9c5 && Main.Tile[i + 1, j + 1].Active)
                    num8 = Main.Tile[i + 1, j + 1].Type;

                switch (type)
                {
                    case 4:
                        if (num7 >= 0 && Main.TileSolid[num7])
                            Main.Tile[i, j].FrameX = 0;
                        else if (
                            (index >= 0 && Main.TileSolid[index])
                            || (index == 5 && num == 5 && num6 == 5)
                        )
                            Main.Tile[i, j].FrameX = 0x16;
                        else if (
                            (num5 >= 0 && Main.TileSolid[num5])
                            || (num5 == 5 && num3 == 5 && num8 == 5)
                        )
                            Main.Tile[i, j].FrameX = 0x2c;
                        else
                            KillTile(i, j);

                        return;

                    case 10:
                        if (!destroyObject)
                        {
                            num12 = Main.Tile[i, j].FrameY;
                            num13 = j;
                            flag = false;
                            switch (num12)
                            {
                                case 0:
                                    num13 = j;
                                    break;

                                case 0x12:
                                    num13 = j - 1;
                                    break;

                                case 0x24:
                                    num13 = j - 2;
                                    break;
                            }

                            if (
                                !(
                                    Main.Tile[i, num13 - 1].Active
                                    && Main.TileSolid[Main.Tile[i, num13 - 1].Type]
                                )
                            )
                                flag = true;

                            if (
                                !(
                                    Main.Tile[i, num13 + 3].Active
                                    && Main.TileSolid[Main.Tile[i, num13 + 3].Type]
                                )
                            )
                                flag = true;

                            if (
                                !(
                                    Main.Tile[i, num13].Active
                                    && Main.Tile[i, num13].Type == type
                                )
                            )
                                flag = true;

                            if (
                                !(
                                    Main.Tile[i, num13 + 1].Active
                                    && Main.Tile[i, num13 + 1].Type == type
                                )
                            )
                                flag = true;

                            if (
                                !(
                                    Main.Tile[i, num13 + 2].Active
                                    && Main.Tile[i, num13 + 2].Type == type
                                )
                            )
                                flag = true;

                            if (flag)
                            {
                                destroyObject = true;
                                KillTile(i, num13);
                                KillTile(i, num13 + 1);
                                KillTile(i, num13 + 2);
                                Item.NewItem(i * 0x10, j * 0x10, 0x10, 0x10, 0x19);
                            }

                            destroyObject = false;
                        }

                        return;

                    case 11:
                        if (!destroyObject)
                        {
                            var num14 = 0;
                            var num15 = i;
                            num13 = j;
                            int num16 = Main.Tile[i, j].FrameX;
                            num12 = Main.Tile[i, j].FrameY;
                            flag = false;
                            if (num16 == 0)
                            {
                                num15 = i;
                                num14 = 1;
                            }
                            else if (num16 == 0x12)
                            {
                                num15 = i - 1;
                                num14 = 1;
                            }
                            else if (num16 == 0x24)
                            {
                                num15 = i + 1;
                                num14 = -1;
                            }
                            else if (num16 == 0x36)
                            {
                                num15 = i;
                                num14 = -1;
                            }

                            switch (num12)
                            {
                                case 0:
                                    num13 = j;
                                    break;

                                case 0x12:
                                    num13 = j - 1;
                                    break;

                                case 0x24:
                                    num13 = j - 2;
                                    break;
                            }

                            if (
                                !(
                                    Main.Tile[num15, num13 - 1].Active
                                    && Main.TileSolid[Main.Tile[num15, num13 - 1].Type] && Main.Tile[num15, num13 + 3].Active &&
                                    Main.TileSolid[Main.Tile[num15, num13 + 3].Type]
                                )
                            )
                            {
                                flag = true;
                                destroyObject = true;
                                Item.NewItem(i * 0x10, j * 0x10, 0x10, 0x10, 0x19);
                            }

                            var num17 = num15;
                            if (num14 == -1)
                                num17 = num15 - 1;

                            for (var k = num17; k < num17 + 2; k++)
                            for (var m = num13; m < num13 + 3; m++)
                            {
                                if (
                                    !flag
                                    && !(
                                        Main.Tile[k, m].Type == 11
                                        && Main.Tile[k, m].Active
                                    )
                                )
                                {
                                    destroyObject = true;
                                    Item.NewItem(i * 0x10, j * 0x10, 0x10, 0x10, 0x19);
                                    flag = true;
                                    k = num17;
                                    m = num13;
                                }

                                if (flag)
                                    KillTile(k, m);
                            }

                            destroyObject = false;
                        }

                        return;
                }

                if (type == 5)
                {
                    if (num7 != type && num7 != -1)
                    {
                        if (
                            Main.Tile[i, j].FrameX >= 0x16
                            && Main.Tile[i, j].FrameX <= 0x2c && Main.Tile[i, j].FrameY >= 0x84 &&
                            Main.Tile[i, j].FrameY <= 0xb0 && index != type && num5 != type
                        )
                            KillTile(i, j);
                    }
                    else if (
                        (
                            Main.Tile[i, j].FrameX == 0x58
                            && Main.Tile[i, j].FrameY >= 0 && Main.Tile[i, j].FrameY <= 0x2c
                        )
                        || (
                            Main.Tile[i, j].FrameX == 0x42
                            && Main.Tile[i, j].FrameY >= 0x42 && Main.Tile[i, j].FrameY <= 130
                        )
                        || (
                            Main.Tile[i, j].FrameX == 110
                            && Main.Tile[i, j].FrameY >= 0x42 && Main.Tile[i, j].FrameY <= 110
                        )
                        || (
                            Main.Tile[i, j].FrameX == 0x84 && Main.Tile[i, j].FrameY >= 0
                                                           && Main.Tile[i, j].FrameY <= 0xb0
                        )
                    )
                    {
                        if (index == type && num5 == type)
                        {
                            if (Main.Tile[i, j].FrameNumber == 0)
                            {
                                Main.Tile[i, j].FrameX = 110;
                                Main.Tile[i, j].FrameY = 0x42;
                            }

                            if (Main.Tile[i, j].FrameNumber == 1)
                            {
                                Main.Tile[i, j].FrameX = 110;
                                Main.Tile[i, j].FrameY = 0x58;
                            }

                            if (Main.Tile[i, j].FrameNumber == 2)
                            {
                                Main.Tile[i, j].FrameX = 110;
                                Main.Tile[i, j].FrameY = 110;
                            }
                        }
                        else if (index == type)
                        {
                            if (Main.Tile[i, j].FrameNumber == 0)
                            {
                                Main.Tile[i, j].FrameX = 0x58;
                                Main.Tile[i, j].FrameY = 0;
                            }

                            if (Main.Tile[i, j].FrameNumber == 1)
                            {
                                Main.Tile[i, j].FrameX = 0x58;
                                Main.Tile[i, j].FrameY = 0x16;
                            }

                            if (Main.Tile[i, j].FrameNumber == 2)
                            {
                                Main.Tile[i, j].FrameX = 0x58;
                                Main.Tile[i, j].FrameY = 0x2c;
                            }
                        }
                        else if (num5 == type)
                        {
                            if (Main.Tile[i, j].FrameNumber == 0)
                            {
                                Main.Tile[i, j].FrameX = 0x42;
                                Main.Tile[i, j].FrameY = 0x42;
                            }

                            if (Main.Tile[i, j].FrameNumber == 1)
                            {
                                Main.Tile[i, j].FrameX = 0x42;
                                Main.Tile[i, j].FrameY = 0x58;
                            }

                            if (Main.Tile[i, j].FrameNumber == 2)
                            {
                                Main.Tile[i, j].FrameX = 0x42;
                                Main.Tile[i, j].FrameY = 110;
                            }
                        }
                        else
                        {
                            if (Main.Tile[i, j].FrameNumber == 0)
                            {
                                Main.Tile[i, j].FrameX = 0;
                                Main.Tile[i, j].FrameY = 0x16;
                            }

                            if (Main.Tile[i, j].FrameNumber == 1)
                            {
                                Main.Tile[i, j].FrameX = 0;
                                Main.Tile[i, j].FrameY = 0x16;
                            }

                            if (Main.Tile[i, j].FrameNumber == 2)
                            {
                                Main.Tile[i, j].FrameX = 0;
                                Main.Tile[i, j].FrameY = 0x16;
                            }
                        }
                    }

                    if (
                        Main.Tile[i, j].FrameY >= 0x84 && Main.Tile[i, j].FrameY <= 0xb0
                                                       && (
                                                           Main.Tile[i, j].FrameX == 0 || Main.Tile[i, j].FrameX == 0x42
                                                           || Main.Tile[i, j].FrameX == 0x58
                                                       )
                    )
                    {
                        if (index != type && num5 != type)
                        {
                            if (Main.Tile[i, j].FrameNumber == 0)
                            {
                                Main.Tile[i, j].FrameX = 0;
                                Main.Tile[i, j].FrameY = 0;
                            }

                            if (Main.Tile[i, j].FrameNumber == 1)
                            {
                                Main.Tile[i, j].FrameX = 0;
                                Main.Tile[i, j].FrameY = 0x16;
                            }

                            if (Main.Tile[i, j].FrameNumber == 2)
                            {
                                Main.Tile[i, j].FrameX = 0;
                                Main.Tile[i, j].FrameY = 0x2c;
                            }
                        }
                        else if (index != type)
                        {
                            if (Main.Tile[i, j].FrameNumber == 0)
                            {
                                Main.Tile[i, j].FrameX = 0;
                                Main.Tile[i, j].FrameY = 0x84;
                            }

                            if (Main.Tile[i, j].FrameNumber == 1)
                            {
                                Main.Tile[i, j].FrameX = 0;
                                Main.Tile[i, j].FrameY = 0x9a;
                            }

                            if (Main.Tile[i, j].FrameNumber == 2)
                            {
                                Main.Tile[i, j].FrameX = 0;
                                Main.Tile[i, j].FrameY = 0xb0;
                            }
                        }
                        else if (num5 != type)
                        {
                            if (Main.Tile[i, j].FrameNumber == 0)
                            {
                                Main.Tile[i, j].FrameX = 0x42;
                                Main.Tile[i, j].FrameY = 0x84;
                            }

                            if (Main.Tile[i, j].FrameNumber == 1)
                            {
                                Main.Tile[i, j].FrameX = 0x42;
                                Main.Tile[i, j].FrameY = 0x9a;
                            }

                            if (Main.Tile[i, j].FrameNumber == 2)
                            {
                                Main.Tile[i, j].FrameX = 0x42;
                                Main.Tile[i, j].FrameY = 0xb0;
                            }
                        }
                        else
                        {
                            if (Main.Tile[i, j].FrameNumber == 0)
                            {
                                Main.Tile[i, j].FrameX = 0x58;
                                Main.Tile[i, j].FrameY = 0x84;
                            }

                            if (Main.Tile[i, j].FrameNumber == 1)
                            {
                                Main.Tile[i, j].FrameX = 0x58;
                                Main.Tile[i, j].FrameY = 0x9a;
                            }

                            if (Main.Tile[i, j].FrameNumber == 2)
                            {
                                Main.Tile[i, j].FrameX = 0x58;
                                Main.Tile[i, j].FrameY = 0xb0;
                            }
                        }
                    }

                    if (
                        (
                            Main.Tile[i, j].FrameX == 0x42
                            && (
                                Main.Tile[i, j].FrameY == 0
                                || Main.Tile[i, j].FrameY == 0x16 || Main.Tile[i, j].FrameY == 0x2c
                            )
                        )
                        || (
                            Main.Tile[i, j].FrameX == 0x58
                            && (
                                Main.Tile[i, j].FrameY == 0x42
                                || Main.Tile[i, j].FrameY == 0x58 || Main.Tile[i, j].FrameY == 110
                            )
                        )
                        || (
                            Main.Tile[i, j].FrameX == 0x2c
                            && (
                                Main.Tile[i, j].FrameY == 0xc6
                                || Main.Tile[i, j].FrameY == 220 || Main.Tile[i, j].FrameY == 0xf2
                            )
                        )
                        || (
                            Main.Tile[i, j].FrameX == 0x42
                            && (
                                Main.Tile[i, j].FrameY == 0xc6
                                || Main.Tile[i, j].FrameY == 220 || Main.Tile[i, j].FrameY == 0xf2
                            )
                        )
                    )
                    {
                        if (index != type && num5 != type)
                            KillTile(i, j);
                    }
                    else if (num7 == -1)
                        KillTile(i, j);
                    else if (
                        num2 != type && Main.Tile[i, j].FrameY < 0xc6
                                     && (
                                         (
                                             Main.Tile[i, j].FrameX != 0x16
                                             && Main.Tile[i, j].FrameX != 0x2c
                                         ) || Main.Tile[i, j].FrameY < 0x84
                                     )
                    )
                    {
                        if (index == type || num5 == type)
                        {
                            if (num7 == type)
                            {
                                if (index == type && num5 == type)
                                {
                                    if (Main.Tile[i, j].FrameNumber == 0)
                                    {
                                        Main.Tile[i, j].FrameX = 0x84;
                                        Main.Tile[i, j].FrameY = 0x84;
                                    }

                                    if (Main.Tile[i, j].FrameNumber == 1)
                                    {
                                        Main.Tile[i, j].FrameX = 0x84;
                                        Main.Tile[i, j].FrameY = 0x9a;
                                    }

                                    if (Main.Tile[i, j].FrameNumber == 2)
                                    {
                                        Main.Tile[i, j].FrameX = 0x84;
                                        Main.Tile[i, j].FrameY = 0xb0;
                                    }
                                }
                                else if (index == type)
                                {
                                    if (Main.Tile[i, j].FrameNumber == 0)
                                    {
                                        Main.Tile[i, j].FrameX = 0x84;
                                        Main.Tile[i, j].FrameY = 0;
                                    }

                                    if (Main.Tile[i, j].FrameNumber == 1)
                                    {
                                        Main.Tile[i, j].FrameX = 0x84;
                                        Main.Tile[i, j].FrameY = 0x16;
                                    }

                                    if (Main.Tile[i, j].FrameNumber == 2)
                                    {
                                        Main.Tile[i, j].FrameX = 0x84;
                                        Main.Tile[i, j].FrameY = 0x2c;
                                    }
                                }
                                else if (num5 == type)
                                {
                                    if (Main.Tile[i, j].FrameNumber == 0)
                                    {
                                        Main.Tile[i, j].FrameX = 0x84;
                                        Main.Tile[i, j].FrameY = 0x42;
                                    }

                                    if (Main.Tile[i, j].FrameNumber == 1)
                                    {
                                        Main.Tile[i, j].FrameX = 0x84;
                                        Main.Tile[i, j].FrameY = 0x58;
                                    }

                                    if (Main.Tile[i, j].FrameNumber == 2)
                                    {
                                        Main.Tile[i, j].FrameX = 0x84;
                                        Main.Tile[i, j].FrameY = 110;
                                    }
                                }
                            }
                            else if (index == type && num5 == type)
                            {
                                if (Main.Tile[i, j].FrameNumber == 0)
                                {
                                    Main.Tile[i, j].FrameX = 0x9a;
                                    Main.Tile[i, j].FrameY = 0x84;
                                }

                                if (Main.Tile[i, j].FrameNumber == 1)
                                {
                                    Main.Tile[i, j].FrameX = 0x9a;
                                    Main.Tile[i, j].FrameY = 0x9a;
                                }

                                if (Main.Tile[i, j].FrameNumber == 2)
                                {
                                    Main.Tile[i, j].FrameX = 0x9a;
                                    Main.Tile[i, j].FrameY = 0xb0;
                                }
                            }
                            else if (index == type)
                            {
                                if (Main.Tile[i, j].FrameNumber == 0)
                                {
                                    Main.Tile[i, j].FrameX = 0x9a;
                                    Main.Tile[i, j].FrameY = 0;
                                }

                                if (Main.Tile[i, j].FrameNumber == 1)
                                {
                                    Main.Tile[i, j].FrameX = 0x9a;
                                    Main.Tile[i, j].FrameY = 0x16;
                                }

                                if (Main.Tile[i, j].FrameNumber == 2)
                                {
                                    Main.Tile[i, j].FrameX = 0x9a;
                                    Main.Tile[i, j].FrameY = 0x2c;
                                }
                            }
                            else if (num5 == type)
                            {
                                if (Main.Tile[i, j].FrameNumber == 0)
                                {
                                    Main.Tile[i, j].FrameX = 0x9a;
                                    Main.Tile[i, j].FrameY = 0x42;
                                }

                                if (Main.Tile[i, j].FrameNumber == 1)
                                {
                                    Main.Tile[i, j].FrameX = 0x9a;
                                    Main.Tile[i, j].FrameY = 0x58;
                                }

                                if (Main.Tile[i, j].FrameNumber == 2)
                                {
                                    Main.Tile[i, j].FrameX = 0x9a;
                                    Main.Tile[i, j].FrameY = 110;
                                }
                            }
                        }
                        else
                        {
                            if (Main.Tile[i, j].FrameNumber == 0)
                            {
                                Main.Tile[i, j].FrameX = 110;
                                Main.Tile[i, j].FrameY = 0;
                            }

                            if (Main.Tile[i, j].FrameNumber == 1)
                            {
                                Main.Tile[i, j].FrameX = 110;
                                Main.Tile[i, j].FrameY = 0x16;
                            }

                            if (Main.Tile[i, j].FrameNumber == 2)
                            {
                                Main.Tile[i, j].FrameX = 110;
                                Main.Tile[i, j].FrameY = 0x2c;
                            }
                        }
                    }

                    rectangle.X = Main.Tile[i, j].FrameX;
                    rectangle.Y = Main.Tile[i, j].FrameY;
                }

                var frameNumber = 0;
                if (resetFrame)
                {
                    frameNumber = Main.Rand.Next(0, 3);
                    Main.Tile[i, j].FrameNumber = (byte)frameNumber;
                }
                else
                    frameNumber = Main.Tile[i, j].FrameNumber;

                if (type == 0)
                {
                    for (num21 = 0; num21 < 12; num21++)
                        switch (num21)
                        {
                            case 1:
                            case 6:
                            case 7:
                            case 8:
                            case 9:
                                if (num2 == num21)
                                {
                                    TileFrame(i, j - 1);
                                    if (mergeDown)
                                        num2 = type;
                                }

                                if (num7 == num21)
                                {
                                    TileFrame(i, j + 1);
                                    if (mergeUp)
                                        num7 = type;
                                }

                                if (index == num21)
                                {
                                    TileFrame(i - 1, j);
                                    if (mergeRight)
                                        index = type;
                                }

                                if (num5 == num21)
                                {
                                    TileFrame(i + 1, j);
                                    if (mergeLeft)
                                        num5 = type;
                                }

                                if (num == num21)
                                    num = type;

                                if (num3 == num21)
                                    num3 = type;

                                if (num6 == num21)
                                    num6 = type;

                                if (num8 == num21)
                                    num8 = type;

                                break;
                        }

                    if (num2 == 2)
                        num2 = type;

                    if (num7 == 2)
                        num7 = type;

                    if (index == 2)
                        index = type;

                    if (num5 == 2)
                        num5 = type;

                    if (num == 2)
                        num = type;

                    if (num3 == 2)
                        num3 = type;

                    if (num6 == 2)
                        num6 = type;

                    if (num8 == 2)
                        num8 = type;
                }

                if (
                    type == 1 || type == 6 || type == 7 || type == 8
                    || type == 9
                )
                {
                    for (num21 = 0; num21 < 12; num21++)
                        switch (num21)
                        {
                            case 1:
                            case 6:
                            case 7:
                            case 8:
                            case 9:
                                if (num2 == 0)
                                    num2 = -2;

                                if (num7 == 0)
                                    num7 = -2;

                                if (index == 0)
                                    index = -2;

                                if (num5 == 0)
                                    num5 = -2;

                                if (num == 0)
                                    num = -2;

                                if (num3 == 0)
                                    num3 = -2;

                                if (num6 == 0)
                                    num6 = -2;

                                if (num8 == 0)
                                    num8 = -2;

                                break;
                        }
                }

                if (type == 2)
                {
                    var num22 = 0;
                    if (
                        num2 != type && num2 != num22
                                     && (num7 == type || num7 == num22)
                    )
                    {
                        if (index == num22 && num5 == type)
                        {
                            switch (frameNumber)
                            {
                                case 0:
                                    rectangle.X = 0;
                                    rectangle.Y = 0xc6;
                                    break;

                                case 1:
                                    rectangle.X = 0x12;
                                    rectangle.Y = 0xc6;
                                    break;

                                case 2:
                                    rectangle.X = 0x24;
                                    rectangle.Y = 0xc6;
                                    break;
                            }
                        }
                        else if (index == type && num5 == num22)
                        {
                            switch (frameNumber)
                            {
                                case 0:
                                    rectangle.X = 0x36;
                                    rectangle.Y = 0xc6;
                                    break;

                                case 1:
                                    rectangle.X = 0x48;
                                    rectangle.Y = 0xc6;
                                    break;

                                case 2:
                                    rectangle.X = 90;
                                    rectangle.Y = 0xc6;
                                    break;
                            }
                        }
                    }
                    else if (
                        num7 != type && num7 != num22
                                     && (num2 == type || num2 == num22)
                    )
                    {
                        if (index == num22 && num5 == type)
                        {
                            switch (frameNumber)
                            {
                                case 0:
                                    rectangle.X = 0;
                                    rectangle.Y = 0xd8;
                                    break;

                                case 1:
                                    rectangle.X = 0x12;
                                    rectangle.Y = 0xd8;
                                    break;

                                case 2:
                                    rectangle.X = 0x24;
                                    rectangle.Y = 0xd8;
                                    break;
                            }
                        }
                        else if (index == type && num5 == num22)
                        {
                            switch (frameNumber)
                            {
                                case 0:
                                    rectangle.X = 0x36;
                                    rectangle.Y = 0xd8;
                                    break;

                                case 1:
                                    rectangle.X = 0x48;
                                    rectangle.Y = 0xd8;
                                    break;

                                case 2:
                                    rectangle.X = 90;
                                    rectangle.Y = 0xd8;
                                    break;
                            }
                        }
                    }
                    else if (
                        index != type && index != num22
                                      && (num5 == type || num5 == num22)
                    )
                    {
                        if (num2 == num22 && num7 == type)
                        {
                            switch (frameNumber)
                            {
                                case 0:
                                    rectangle.X = 0x48;
                                    rectangle.Y = 0x90;
                                    break;

                                case 1:
                                    rectangle.X = 0x48;
                                    rectangle.Y = 0xa2;
                                    break;

                                case 2:
                                    rectangle.X = 0x48;
                                    rectangle.Y = 180;
                                    break;
                            }
                        }
                        else if (num7 == type && num5 == num2)
                        {
                            switch (frameNumber)
                            {
                                case 0:
                                    rectangle.X = 0x48;
                                    rectangle.Y = 90;
                                    break;

                                case 1:
                                    rectangle.X = 0x48;
                                    rectangle.Y = 0x6c;
                                    break;

                                case 2:
                                    rectangle.X = 0x48;
                                    rectangle.Y = 0x7e;
                                    break;
                            }
                        }
                    }
                    else if (
                        num5 != type && num5 != num22
                                     && (index == type || index == num22)
                    )
                    {
                        if (num2 == num22 && num7 == type)
                        {
                            switch (frameNumber)
                            {
                                case 0:
                                    rectangle.X = 90;
                                    rectangle.Y = 0x90;
                                    break;

                                case 1:
                                    rectangle.X = 90;
                                    rectangle.Y = 0xa2;
                                    break;

                                case 2:
                                    rectangle.X = 90;
                                    rectangle.Y = 180;
                                    break;
                            }
                        }
                        else if (num7 == type && num5 == num2)
                        {
                            switch (frameNumber)
                            {
                                case 0:
                                    rectangle.X = 90;
                                    rectangle.Y = 90;
                                    break;

                                case 1:
                                    rectangle.X = 90;
                                    rectangle.Y = 0x6c;
                                    break;

                                case 2:
                                    rectangle.X = 90;
                                    rectangle.Y = 0x7e;
                                    break;
                            }
                        }
                    }
                    else if (
                        num2 == type && num7 == type && index == type
                        && num5 == type
                    )
                    {
                        if (
                            num != type && num3 != type && num6 != type
                            && num8 != type
                        )
                        {
                            if (num8 == num22)
                            {
                                switch (frameNumber)
                                {
                                    case 0:
                                        rectangle.X = 0x6c;
                                        rectangle.Y = 0x144;
                                        break;

                                    case 1:
                                        rectangle.X = 0x7e;
                                        rectangle.Y = 0x144;
                                        break;

                                    case 2:
                                        rectangle.X = 0x90;
                                        rectangle.Y = 0x144;
                                        break;
                                }
                            }
                            else if (num3 == num22)
                            {
                                switch (frameNumber)
                                {
                                    case 0:
                                        rectangle.X = 0x6c;
                                        rectangle.Y = 0x156;
                                        break;

                                    case 1:
                                        rectangle.X = 0x7e;
                                        rectangle.Y = 0x156;
                                        break;

                                    case 2:
                                        rectangle.X = 0x90;
                                        rectangle.Y = 0x156;
                                        break;
                                }
                            }
                            else if (num6 == num22)
                            {
                                switch (frameNumber)
                                {
                                    case 0:
                                        rectangle.X = 0x6c;
                                        rectangle.Y = 360;
                                        break;

                                    case 1:
                                        rectangle.X = 0x7e;
                                        rectangle.Y = 360;
                                        break;

                                    case 2:
                                        rectangle.X = 0x90;
                                        rectangle.Y = 360;
                                        break;
                                }
                            }
                            else if (num == num22)
                            {
                                switch (frameNumber)
                                {
                                    case 0:
                                        rectangle.X = 0x6c;
                                        rectangle.Y = 0x17a;
                                        break;

                                    case 1:
                                        rectangle.X = 0x7e;
                                        rectangle.Y = 0x17a;
                                        break;

                                    case 2:
                                        rectangle.X = 0x90;
                                        rectangle.Y = 0x17a;
                                        break;
                                }
                            }
                            else
                            {
                                switch (frameNumber)
                                {
                                    case 0:
                                        rectangle.X = 0x90;
                                        rectangle.Y = 0xea;
                                        break;

                                    case 1:
                                        rectangle.X = 0xc6;
                                        rectangle.Y = 0xea;
                                        break;

                                    case 2:
                                        rectangle.X = 0xfc;
                                        rectangle.Y = 0xea;
                                        break;
                                }
                            }
                        }
                        else if (num != type && num8 != type)
                        {
                            switch (frameNumber)
                            {
                                case 0:
                                    rectangle.X = 0x24;
                                    rectangle.Y = 0x132;
                                    break;

                                case 1:
                                    rectangle.X = 0x36;
                                    rectangle.Y = 0x132;
                                    break;

                                case 2:
                                    rectangle.X = 0x48;
                                    rectangle.Y = 0x132;
                                    break;
                            }
                        }
                        else if (num3 != type && num6 != type)
                        {
                            switch (frameNumber)
                            {
                                case 0:
                                    rectangle.X = 90;
                                    rectangle.Y = 0x132;
                                    break;

                                case 1:
                                    rectangle.X = 0x6c;
                                    rectangle.Y = 0x132;
                                    break;

                                case 2:
                                    rectangle.X = 0x7e;
                                    rectangle.Y = 0x132;
                                    break;
                            }
                        }
                        else if (
                            num != type && num3 == type && num6 == type
                            && num8 == type
                        )
                        {
                            switch (frameNumber)
                            {
                                case 0:
                                    rectangle.X = 0x36;
                                    rectangle.Y = 0x6c;
                                    break;

                                case 1:
                                    rectangle.X = 0x36;
                                    rectangle.Y = 0x90;
                                    break;

                                case 2:
                                    rectangle.X = 0x36;
                                    rectangle.Y = 180;
                                    break;
                            }
                        }
                        else if (
                            num == type && num3 != type && num6 == type
                            && num8 == type
                        )
                        {
                            switch (frameNumber)
                            {
                                case 0:
                                    rectangle.X = 0x24;
                                    rectangle.Y = 0x6c;
                                    break;

                                case 1:
                                    rectangle.X = 0x24;
                                    rectangle.Y = 0x90;
                                    break;

                                case 2:
                                    rectangle.X = 0x24;
                                    rectangle.Y = 180;
                                    break;
                            }
                        }
                        else if (
                            num == type && num3 == type && num6 != type
                            && num8 == type
                        )
                        {
                            switch (frameNumber)
                            {
                                case 0:
                                    rectangle.X = 0x36;
                                    rectangle.Y = 90;
                                    break;

                                case 1:
                                    rectangle.X = 0x36;
                                    rectangle.Y = 0x7e;
                                    break;

                                case 2:
                                    rectangle.X = 0x36;
                                    rectangle.Y = 0xa2;
                                    break;
                            }
                        }
                        else if (
                            num == type && num3 == type && num6 == type
                            && num8 != type
                        )
                        {
                            switch (frameNumber)
                            {
                                case 0:
                                    rectangle.X = 0x24;
                                    rectangle.Y = 90;
                                    break;

                                case 1:
                                    rectangle.X = 0x24;
                                    rectangle.Y = 0x7e;
                                    break;

                                case 2:
                                    rectangle.X = 0x24;
                                    rectangle.Y = 0xa2;
                                    break;
                            }
                        }
                    }
                    else if (
                        num2 == type && num7 == num22
                                     && index == type && num5 == type && num == -1 && num3 == -1
                    )
                    {
                        switch (frameNumber)
                        {
                            case 0:
                                rectangle.X = 0x6c;
                                rectangle.Y = 0x12;
                                break;

                            case 1:
                                rectangle.X = 0x7e;
                                rectangle.Y = 0x12;
                                break;

                            case 2:
                                rectangle.X = 0x90;
                                rectangle.Y = 0x12;
                                break;
                        }
                    }
                    else if (
                        num2 == num22 && num7 == type
                                      && index == type && num5 == type && num6 == -1 && num8 == -1
                    )
                    {
                        switch (frameNumber)
                        {
                            case 0:
                                rectangle.X = 0x6c;
                                rectangle.Y = 0x24;
                                break;

                            case 1:
                                rectangle.X = 0x7e;
                                rectangle.Y = 0x24;
                                break;

                            case 2:
                                rectangle.X = 0x90;
                                rectangle.Y = 0x24;
                                break;
                        }
                    }
                    else if (
                        num2 == type && num7 == type
                                     && index == num22 && num5 == type && num3 == -1 && num8 == -1
                    )
                    {
                        switch (frameNumber)
                        {
                            case 0:
                                rectangle.X = 0xc6;
                                rectangle.Y = 0;
                                break;

                            case 1:
                                rectangle.X = 0xc6;
                                rectangle.Y = 0x12;
                                break;

                            case 2:
                                rectangle.X = 0xc6;
                                rectangle.Y = 0x24;
                                break;
                        }
                    }
                    else if (
                        num2 == type && num7 == type
                                     && index == type && num5 == num22 && num == -1 && num6 == -1
                    )
                    {
                        switch (frameNumber)
                        {
                            case 0:
                                rectangle.X = 180;
                                rectangle.Y = 0;
                                break;

                            case 1:
                                rectangle.X = 180;
                                rectangle.Y = 0x12;
                                break;

                            case 2:
                                rectangle.X = 180;
                                rectangle.Y = 0x24;
                                break;
                        }
                    }
                    else if (
                        num2 == type && num7 == num22 && index == type
                        && num5 == type
                    )
                    {
                        if (num3 == -1)
                        {
                            if (num != -1)
                            {
                                switch (frameNumber)
                                {
                                    case 0:
                                        rectangle.X = 0x24;
                                        rectangle.Y = 0x6c;
                                        break;

                                    case 1:
                                        rectangle.X = 0x24;
                                        rectangle.Y = 0x90;
                                        break;

                                    case 2:
                                        rectangle.X = 0x24;
                                        rectangle.Y = 180;
                                        break;
                                }
                            }
                        }
                        else
                        {
                            switch (frameNumber)
                            {
                                case 0:
                                    rectangle.X = 0x36;
                                    rectangle.Y = 0x6c;
                                    break;

                                case 1:
                                    rectangle.X = 0x36;
                                    rectangle.Y = 0x90;
                                    break;

                                case 2:
                                    rectangle.X = 0x36;
                                    rectangle.Y = 180;
                                    break;
                            }
                        }
                    }
                    else if (
                        num2 == num22 && num7 == type && index == type
                        && num5 == type
                    )
                    {
                        if (num8 == -1)
                        {
                            if (num6 != -1)
                            {
                                switch (frameNumber)
                                {
                                    case 0:
                                        rectangle.X = 0x24;
                                        rectangle.Y = 90;
                                        break;

                                    case 1:
                                        rectangle.X = 0x24;
                                        rectangle.Y = 0x7e;
                                        break;

                                    case 2:
                                        rectangle.X = 0x24;
                                        rectangle.Y = 0xa2;
                                        break;
                                }
                            }
                        }
                        else
                        {
                            switch (frameNumber)
                            {
                                case 0:
                                    rectangle.X = 0x36;
                                    rectangle.Y = 90;
                                    break;

                                case 1:
                                    rectangle.X = 0x36;
                                    rectangle.Y = 0x7e;
                                    break;

                                case 2:
                                    rectangle.X = 0x36;
                                    rectangle.Y = 0xa2;
                                    break;
                            }
                        }
                    }
                    else if (
                        num2 == type && num7 == type && index == type
                        && num5 == num22
                    )
                    {
                        if (num == -1)
                        {
                            if (num6 != -1)
                            {
                                switch (frameNumber)
                                {
                                    case 0:
                                        rectangle.X = 0x36;
                                        rectangle.Y = 0x6c;
                                        break;

                                    case 1:
                                        rectangle.X = 0x36;
                                        rectangle.Y = 0x90;
                                        break;

                                    case 2:
                                        rectangle.X = 0x36;
                                        rectangle.Y = 180;
                                        break;
                                }
                            }
                        }
                        else
                        {
                            switch (frameNumber)
                            {
                                case 0:
                                    rectangle.X = 0x36;
                                    rectangle.Y = 90;
                                    break;

                                case 1:
                                    rectangle.X = 0x36;
                                    rectangle.Y = 0x7e;
                                    break;

                                case 2:
                                    rectangle.X = 0x36;
                                    rectangle.Y = 0xa2;
                                    break;
                            }
                        }
                    }
                    else if (
                        num2 == type && num7 == type && index == num22
                        && num5 == type
                    )
                    {
                        if (num3 == -1)
                        {
                            if (num8 != -1)
                            {
                                switch (frameNumber)
                                {
                                    case 0:
                                        rectangle.X = 0x24;
                                        rectangle.Y = 0x6c;
                                        break;

                                    case 1:
                                        rectangle.X = 0x24;
                                        rectangle.Y = 0x90;
                                        break;

                                    case 2:
                                        rectangle.X = 0x24;
                                        rectangle.Y = 180;
                                        break;
                                }
                            }
                        }
                        else
                        {
                            switch (frameNumber)
                            {
                                case 0:
                                    rectangle.X = 0x24;
                                    rectangle.Y = 90;
                                    break;

                                case 1:
                                    rectangle.X = 0x24;
                                    rectangle.Y = 0x7e;
                                    break;

                                case 2:
                                    rectangle.X = 0x24;
                                    rectangle.Y = 0xa2;
                                    break;
                            }
                        }
                    }
                    else if (
                        (
                            num2 == num22 && num7 == type
                                          && index == type && num5 == type
                        )
                        || (
                            num2 == type && num7 == num22
                                         && index == type && num5 == type
                        )
                        || (
                            num2 == type && num7 == type
                                         && index == num22 && num5 == type
                        )
                        || (
                            num2 == type && num7 == type && index == type
                            && num5 == num22
                        )
                    )
                    {
                        switch (frameNumber)
                        {
                            case 0:
                                rectangle.X = 0x12;
                                rectangle.Y = 0x12;
                                break;

                            case 1:
                                rectangle.X = 0x24;
                                rectangle.Y = 0x12;
                                break;

                            case 2:
                                rectangle.X = 0x36;
                                rectangle.Y = 0x12;
                                break;
                        }
                    }

                    if (
                        (num2 == type || num2 == num22)
                        && (num7 == type || num7 == num22) && (index == type || index == num22) && (num5 == type || num5 == num22)
                    )
                    {
                        if (
                            num != type && num != num22
                                        && (num3 == type || num3 == num22) && (num6 == type || num6 == num22) &&
                                        (num8 == type || num8 == num22)
                        )
                        {
                            switch (frameNumber)
                            {
                                case 0:
                                    rectangle.X = 0x36;
                                    rectangle.Y = 0x6c;
                                    break;

                                case 1:
                                    rectangle.X = 0x36;
                                    rectangle.Y = 0x90;
                                    break;

                                case 2:
                                    rectangle.X = 0x36;
                                    rectangle.Y = 180;
                                    break;
                            }
                        }
                        else if (
                            num3 != type && num3 != num22
                                         && (num == type || num == num22) && (num6 == type || num6 == num22) &&
                                         (num8 == type || num8 == num22)
                        )
                        {
                            switch (frameNumber)
                            {
                                case 0:
                                    rectangle.X = 0x24;
                                    rectangle.Y = 0x6c;
                                    break;

                                case 1:
                                    rectangle.X = 0x24;
                                    rectangle.Y = 0x90;
                                    break;

                                case 2:
                                    rectangle.X = 0x24;
                                    rectangle.Y = 180;
                                    break;
                            }
                        }
                        else if (
                            num6 != type && num6 != num22
                                         && (num == type || num == num22) && (num3 == type || num3 == num22) &&
                                         (num8 == type || num8 == num22)
                        )
                        {
                            switch (frameNumber)
                            {
                                case 0:
                                    rectangle.X = 0x36;
                                    rectangle.Y = 90;
                                    break;

                                case 1:
                                    rectangle.X = 0x36;
                                    rectangle.Y = 0x7e;
                                    break;

                                case 2:
                                    rectangle.X = 0x36;
                                    rectangle.Y = 0xa2;
                                    break;
                            }
                        }
                        else if (
                            num8 != type && num8 != num22
                                         && (num == type || num == num22) && (num6 == type || num6 == num22) &&
                                         (num3 == type || num3 == num22)
                        )
                        {
                            switch (frameNumber)
                            {
                                case 0:
                                    rectangle.X = 0x24;
                                    rectangle.Y = 90;
                                    break;

                                case 1:
                                    rectangle.X = 0x24;
                                    rectangle.Y = 0x7e;
                                    break;

                                case 2:
                                    rectangle.X = 0x24;
                                    rectangle.Y = 0xa2;
                                    break;
                            }
                        }
                    }

                    if (
                        num2 != num22 && num2 != type
                                      && num7 == type && index != num22 && index != type && num5 == type && num8 != num22 &&
                                      num8 != type
                    )
                    {
                        switch (frameNumber)
                        {
                            case 0:
                                rectangle.X = 90;
                                rectangle.Y = 270;
                                break;

                            case 1:
                                rectangle.X = 0x6c;
                                rectangle.Y = 270;
                                break;

                            case 2:
                                rectangle.X = 0x7e;
                                rectangle.Y = 270;
                                break;
                        }
                    }
                    else if (
                        num2 != num22 && num2 != type
                                      && num7 == type && index == type && num5 != num22 && num5 != type && num6 != num22 &&
                                      num6 != type
                    )
                    {
                        switch (frameNumber)
                        {
                            case 0:
                                rectangle.X = 0x90;
                                rectangle.Y = 270;
                                break;

                            case 1:
                                rectangle.X = 0xa2;
                                rectangle.Y = 270;
                                break;

                            case 2:
                                rectangle.X = 180;
                                rectangle.Y = 270;
                                break;
                        }
                    }
                    else if (
                        num7 != num22 && num7 != type
                                      && num2 == type && index != num22 && index != type && num5 == type && num3 != num22 &&
                                      num3 != type
                    )
                    {
                        switch (frameNumber)
                        {
                            case 0:
                                rectangle.X = 90;
                                rectangle.Y = 0x120;
                                break;

                            case 1:
                                rectangle.X = 0x6c;
                                rectangle.Y = 0x120;
                                break;

                            case 2:
                                rectangle.X = 0x7e;
                                rectangle.Y = 0x120;
                                break;
                        }
                    }
                    else if (
                        num7 != num22 && num7 != type
                                      && num2 == type && index == type && num5 != num22 && num5 != type && num != num22 &&
                                      num != type
                    )
                    {
                        switch (frameNumber)
                        {
                            case 0:
                                rectangle.X = 0x90;
                                rectangle.Y = 0x120;
                                break;

                            case 1:
                                rectangle.X = 0xa2;
                                rectangle.Y = 0x120;
                                break;

                            case 2:
                                rectangle.X = 180;
                                rectangle.Y = 0x120;
                                break;
                        }
                    }
                    else if (
                        num2 != type && num2 != num22
                                     && num7 == type && index == type
                                     && num5 == type && num6 != type
                                     && num6 != num22 && num8 != type && num8 != num22
                    )
                    {
                        switch (frameNumber)
                        {
                            case 0:
                                rectangle.X = 0x90;
                                rectangle.Y = 0xd8;
                                break;

                            case 1:
                                rectangle.X = 0xc6;
                                rectangle.Y = 0xd8;
                                break;

                            case 2:
                                rectangle.X = 0xfc;
                                rectangle.Y = 0xd8;
                                break;
                        }
                    }
                    else if (
                        num7 != type && num7 != num22
                                     && num2 == type && index == type
                                     && num5 == type && num != type
                                     && num != num22 && num3 != type && num3 != num22
                    )
                    {
                        switch (frameNumber)
                        {
                            case 0:
                                rectangle.X = 0x90;
                                rectangle.Y = 0xfc;
                                break;

                            case 1:
                                rectangle.X = 0xc6;
                                rectangle.Y = 0xfc;
                                break;

                            case 2:
                                rectangle.X = 0xfc;
                                rectangle.Y = 0xfc;
                                break;
                        }
                    }
                    else if (
                        index != type && index != num22
                                      && num7 == type && num2 == type
                                      && num5 == type && num3 != type
                                      && num3 != num22 && num8 != type && num8 != num22
                    )
                    {
                        switch (frameNumber)
                        {
                            case 0:
                                rectangle.X = 0x7e;
                                rectangle.Y = 0xea;
                                break;

                            case 1:
                                rectangle.X = 180;
                                rectangle.Y = 0xea;
                                break;

                            case 2:
                                rectangle.X = 0xea;
                                rectangle.Y = 0xea;
                                break;
                        }
                    }
                    else if (
                        num5 != type && num5 != num22
                                     && num7 == type && num2 == type
                                     && index == type && num != type
                                     && num != num22 && num6 != type && num6 != num22
                    )
                    {
                        switch (frameNumber)
                        {
                            case 0:
                                rectangle.X = 0xa2;
                                rectangle.Y = 0xea;
                                break;

                            case 1:
                                rectangle.X = 0xd8;
                                rectangle.Y = 0xea;
                                break;

                            case 2:
                                rectangle.X = 270;
                                rectangle.Y = 0xea;
                                break;
                        }
                    }
                    else if (
                        num2 != num22 && num2 != type
                                      && (num7 == num22 || num7 == type) && index == num22 && num5 == num22
                    )
                    {
                        switch (frameNumber)
                        {
                            case 0:
                                rectangle.X = 0x24;
                                rectangle.Y = 270;
                                break;

                            case 1:
                                rectangle.X = 0x36;
                                rectangle.Y = 270;
                                break;

                            case 2:
                                rectangle.X = 0x48;
                                rectangle.Y = 270;
                                break;
                        }
                    }
                    else if (
                        num7 != num22 && num7 != type
                                      && (num2 == num22 || num2 == type) && index == num22 && num5 == num22
                    )
                    {
                        switch (frameNumber)
                        {
                            case 0:
                                rectangle.X = 0x24;
                                rectangle.Y = 0x120;
                                break;

                            case 1:
                                rectangle.X = 0x36;
                                rectangle.Y = 0x120;
                                break;

                            case 2:
                                rectangle.X = 0x48;
                                rectangle.Y = 0x120;
                                break;
                        }
                    }
                    else if (
                        index != num22 && index != type
                                       && (num5 == num22 || num5 == type) && num2 == num22 && num7 == num22
                    )
                    {
                        switch (frameNumber)
                        {
                            case 0:
                                rectangle.X = 0;
                                rectangle.Y = 270;
                                break;

                            case 1:
                                rectangle.X = 0;
                                rectangle.Y = 0x120;
                                break;

                            case 2:
                                rectangle.X = 0;
                                rectangle.Y = 0x132;
                                break;
                        }
                    }
                    else if (
                        num5 != num22 && num5 != type
                                      && (index == num22 || index == type) && num2 == num22 && num7 == num22
                    )
                    {
                        switch (frameNumber)
                        {
                            case 0:
                                rectangle.X = 0x12;
                                rectangle.Y = 270;
                                break;

                            case 1:
                                rectangle.X = 0x12;
                                rectangle.Y = 0x120;
                                break;

                            case 2:
                                rectangle.X = 0x12;
                                rectangle.Y = 0x132;
                                break;
                        }
                    }
                    else if (
                        num2 == type && num7 == num22 && index == num22
                        && num5 == num22
                    )
                    {
                        switch (frameNumber)
                        {
                            case 0:
                                rectangle.X = 0xc6;
                                rectangle.Y = 0x120;
                                break;

                            case 1:
                                rectangle.X = 0xd8;
                                rectangle.Y = 0x120;
                                break;

                            case 2:
                                rectangle.X = 0xea;
                                rectangle.Y = 0x120;
                                break;
                        }
                    }
                    else if (
                        num2 == num22 && num7 == type && index == num22
                        && num5 == num22
                    )
                    {
                        switch (frameNumber)
                        {
                            case 0:
                                rectangle.X = 0xc6;
                                rectangle.Y = 270;
                                break;

                            case 1:
                                rectangle.X = 0xd8;
                                rectangle.Y = 270;
                                break;

                            case 2:
                                rectangle.X = 0xea;
                                rectangle.Y = 270;
                                break;
                        }
                    }
                    else if (
                        num2 == num22 && num7 == num22 && index == type
                        && num5 == num22
                    )
                    {
                        switch (frameNumber)
                        {
                            case 0:
                                rectangle.X = 0xc6;
                                rectangle.Y = 0x132;
                                break;

                            case 1:
                                rectangle.X = 0xd8;
                                rectangle.Y = 0x132;
                                break;

                            case 2:
                                rectangle.X = 0xea;
                                rectangle.Y = 0x132;
                                break;
                        }
                    }
                    else if (
                        num2 == num22 && num7 == num22 && index == num22
                        && num5 == type
                    )
                    {
                        switch (frameNumber)
                        {
                            case 0:
                                rectangle.X = 0x90;
                                rectangle.Y = 0x132;
                                break;

                            case 1:
                                rectangle.X = 0xa2;
                                rectangle.Y = 0x132;
                                break;

                            case 2:
                                rectangle.X = 180;
                                rectangle.Y = 0x132;
                                break;
                        }
                    }

                    if (
                        num2 != type && num2 != num22
                                     && num7 == type && index == type && num5 == type
                    )
                    {
                        if (
                            (num6 == num22 || num6 == type) && num8 != num22
                                                            && num8 != type
                        )
                        {
                            switch (frameNumber)
                            {
                                case 0:
                                    rectangle.X = 0;
                                    rectangle.Y = 0x144;
                                    break;

                                case 1:
                                    rectangle.X = 0x12;
                                    rectangle.Y = 0x144;
                                    break;

                                case 2:
                                    rectangle.X = 0x24;
                                    rectangle.Y = 0x144;
                                    break;
                            }
                        }
                        else if (
                            (num8 == num22 || num8 == type) && num6 != num22
                                                            && num6 != type
                        )
                        {
                            switch (frameNumber)
                            {
                                case 0:
                                    rectangle.X = 0x36;
                                    rectangle.Y = 0x144;
                                    break;

                                case 1:
                                    rectangle.X = 0x48;
                                    rectangle.Y = 0x144;
                                    break;

                                case 2:
                                    rectangle.X = 90;
                                    rectangle.Y = 0x144;
                                    break;
                            }
                        }
                    }
                    else if (
                        num7 != type && num7 != num22
                                     && num2 == type && index == type && num5 == type
                    )
                    {
                        if (
                            (num == num22 || num == type) && num3 != num22
                                                          && num3 != type
                        )
                        {
                            switch (frameNumber)
                            {
                                case 0:
                                    rectangle.X = 0;
                                    rectangle.Y = 0x156;
                                    break;

                                case 1:
                                    rectangle.X = 0x12;
                                    rectangle.Y = 0x156;
                                    break;

                                case 2:
                                    rectangle.X = 0x24;
                                    rectangle.Y = 0x156;
                                    break;
                            }
                        }
                        else if (
                            (num3 == num22 || num3 == type) && num != num22
                                                            && num != type
                        )
                        {
                            switch (frameNumber)
                            {
                                case 0:
                                    rectangle.X = 0x36;
                                    rectangle.Y = 0x156;
                                    break;

                                case 1:
                                    rectangle.X = 0x48;
                                    rectangle.Y = 0x156;
                                    break;

                                case 2:
                                    rectangle.X = 90;
                                    rectangle.Y = 0x156;
                                    break;
                            }
                        }
                    }
                    else if (
                        index != type && index != num22
                                      && num2 == type && num7 == type && num5 == type
                    )
                    {
                        if (
                            (num3 == num22 || num3 == type) && num8 != num22
                                                            && num8 != type
                        )
                        {
                            switch (frameNumber)
                            {
                                case 0:
                                    rectangle.X = 0x36;
                                    rectangle.Y = 360;
                                    break;

                                case 1:
                                    rectangle.X = 0x48;
                                    rectangle.Y = 360;
                                    break;

                                case 2:
                                    rectangle.X = 90;
                                    rectangle.Y = 360;
                                    break;
                            }
                        }
                        else if (
                            (num8 == num22 || num8 == type) && num3 != num22
                                                            && num3 != type
                        )
                        {
                            switch (frameNumber)
                            {
                                case 0:
                                    rectangle.X = 0;
                                    rectangle.Y = 360;
                                    break;

                                case 1:
                                    rectangle.X = 0x12;
                                    rectangle.Y = 360;
                                    break;

                                case 2:
                                    rectangle.X = 0x24;
                                    rectangle.Y = 360;
                                    break;
                            }
                        }
                    }
                    else if (
                        num5 != type && num5 != num22
                                     && num2 == type && num7 == type && index == type
                    )
                    {
                        if (
                            (num == num22 || num == type) && num6 != num22
                                                          && num6 != type
                        )
                        {
                            switch (frameNumber)
                            {
                                case 0:
                                    rectangle.X = 0;
                                    rectangle.Y = 0x17a;
                                    break;

                                case 1:
                                    rectangle.X = 0x12;
                                    rectangle.Y = 0x17a;
                                    break;

                                case 2:
                                    rectangle.X = 0x24;
                                    rectangle.Y = 0x17a;
                                    break;
                            }
                        }
                        else if (
                            (num6 == num22 || num6 == type) && num != num22
                                                            && num != type
                        )
                        {
                            switch (frameNumber)
                            {
                                case 0:
                                    rectangle.X = 0x36;
                                    rectangle.Y = 0x17a;
                                    break;

                                case 1:
                                    rectangle.X = 0x48;
                                    rectangle.Y = 0x17a;
                                    break;

                                case 2:
                                    rectangle.X = 90;
                                    rectangle.Y = 0x17a;
                                    break;
                            }
                        }
                    }

                    if (
                        (num2 == type || num2 == num22)
                        && (num7 == type || num7 == num22)
                        && (index == type || index == num22)
                        && (num5 == type || num5 == num22) && num != -1 && num3 != -1 && num6 != -1 && num8 != -1
                    )
                    {
                        switch (frameNumber)
                        {
                            case 0:
                                rectangle.X = 0x12;
                                rectangle.Y = 0x12;
                                break;

                            case 1:
                                rectangle.X = 0x24;
                                rectangle.Y = 0x12;
                                break;

                            case 2:
                                rectangle.X = 0x36;
                                rectangle.Y = 0x12;
                                break;
                        }
                    }

                    if (num2 == num22)
                        num2 = -2;

                    if (num7 == num22)
                        num7 = -2;

                    if (index == num22)
                        index = -2;

                    if (num5 == num22)
                        num5 = -2;

                    if (num == num22)
                        num = -2;

                    if (num3 == num22)
                        num3 = -2;

                    if (num6 == num22)
                        num6 = -2;

                    if (num8 == num22)
                        num8 = -2;
                }

                if (
                    (type == 1 || type == 2 || type == 6 || type == 7)
                    && rectangle is { X: -1, Y: -1 }
                )
                {
                    if (num2 >= 0 && num2 != type)
                        num2 = -1;

                    if (num7 >= 0 && num7 != type)
                        num7 = -1;

                    if (index >= 0 && index != type)
                        index = -1;

                    if (num5 >= 0 && num5 != type)
                        num5 = -1;

                    if (num2 != -1 && num7 != -1 && index != -1 && num5 != -1)
                    {
                        if (
                            num2 == -2 && num7 == type && index == type
                            && num5 == type
                        )
                        {
                            switch (frameNumber)
                            {
                                case 0:
                                    rectangle.X = 0x90;
                                    rectangle.Y = 0x6c;
                                    break;

                                case 1:
                                    rectangle.X = 0xa2;
                                    rectangle.Y = 0x6c;
                                    break;

                                case 2:
                                    rectangle.X = 180;
                                    rectangle.Y = 0x6c;
                                    break;
                            }

                            mergeUp = true;
                        }
                        else if (
                            num2 == type && num7 == -2 && index == type
                            && num5 == type
                        )
                        {
                            switch (frameNumber)
                            {
                                case 0:
                                    rectangle.X = 0x90;
                                    rectangle.Y = 90;
                                    break;

                                case 1:
                                    rectangle.X = 0xa2;
                                    rectangle.Y = 90;
                                    break;

                                case 2:
                                    rectangle.X = 180;
                                    rectangle.Y = 90;
                                    break;
                            }

                            mergeDown = true;
                        }
                        else if (
                            num2 == type && num7 == type && index == -2
                            && num5 == type
                        )
                        {
                            switch (frameNumber)
                            {
                                case 0:
                                    rectangle.X = 0xa2;
                                    rectangle.Y = 0x7e;
                                    break;

                                case 1:
                                    rectangle.X = 0xa2;
                                    rectangle.Y = 0x90;
                                    break;

                                case 2:
                                    rectangle.X = 0xa2;
                                    rectangle.Y = 0xa2;
                                    break;
                            }

                            mergeLeft = true;
                        }
                        else if (
                            num2 == type && num7 == type && index == type
                            && num5 == -2
                        )
                        {
                            switch (frameNumber)
                            {
                                case 0:
                                    rectangle.X = 0x90;
                                    rectangle.Y = 0x7e;
                                    break;

                                case 1:
                                    rectangle.X = 0x90;
                                    rectangle.Y = 0x90;
                                    break;

                                case 2:
                                    rectangle.X = 0x90;
                                    rectangle.Y = 0xa2;
                                    break;
                            }

                            mergeRight = true;
                        }
                        else if (
                            num2 == -2 && num7 == type && index == -2
                            && num5 == type
                        )
                        {
                            switch (frameNumber)
                            {
                                case 0:
                                    rectangle.X = 0x24;
                                    rectangle.Y = 90;
                                    break;

                                case 1:
                                    rectangle.X = 0x24;
                                    rectangle.Y = 0x7e;
                                    break;

                                case 2:
                                    rectangle.X = 0x24;
                                    rectangle.Y = 0xa2;
                                    break;
                            }

                            mergeUp = true;
                            mergeLeft = true;
                        }
                        else if (
                            num2 == -2 && num7 == type && index == type
                            && num5 == -2
                        )
                        {
                            switch (frameNumber)
                            {
                                case 0:
                                    rectangle.X = 0x36;
                                    rectangle.Y = 90;
                                    break;

                                case 1:
                                    rectangle.X = 0x36;
                                    rectangle.Y = 0x7e;
                                    break;

                                case 2:
                                    rectangle.X = 0x36;
                                    rectangle.Y = 0xa2;
                                    break;
                            }

                            mergeUp = true;
                            mergeRight = true;
                        }
                        else if (
                            num2 == type && num7 == -2 && index == -2
                            && num5 == type
                        )
                        {
                            switch (frameNumber)
                            {
                                case 0:
                                    rectangle.X = 0x24;
                                    rectangle.Y = 0x6c;
                                    break;

                                case 1:
                                    rectangle.X = 0x24;
                                    rectangle.Y = 0x90;
                                    break;

                                case 2:
                                    rectangle.X = 0x24;
                                    rectangle.Y = 180;
                                    break;
                            }

                            mergeDown = true;
                            mergeLeft = true;
                        }
                        else if (
                            num2 == type && num7 == -2 && index == type
                            && num5 == -2
                        )
                        {
                            switch (frameNumber)
                            {
                                case 0:
                                    rectangle.X = 0x36;
                                    rectangle.Y = 0x6c;
                                    break;

                                case 1:
                                    rectangle.X = 0x36;
                                    rectangle.Y = 0x90;
                                    break;

                                case 2:
                                    rectangle.X = 0x36;
                                    rectangle.Y = 180;
                                    break;
                            }

                            mergeDown = true;
                            mergeRight = true;
                        }
                        else if (
                            num2 == type && num7 == type && index == -2
                            && num5 == -2
                        )
                        {
                            switch (frameNumber)
                            {
                                case 0:
                                    rectangle.X = 180;
                                    rectangle.Y = 0x7e;
                                    break;

                                case 1:
                                    rectangle.X = 180;
                                    rectangle.Y = 0x90;
                                    break;

                                case 2:
                                    rectangle.X = 180;
                                    rectangle.Y = 0xa2;
                                    break;
                            }

                            mergeLeft = true;
                            mergeRight = true;
                        }
                        else if (
                            num2 == -2 && num7 == -2 && index == type
                            && num5 == type
                        )
                        {
                            switch (frameNumber)
                            {
                                case 0:
                                    rectangle.X = 0x90;
                                    rectangle.Y = 180;
                                    break;

                                case 1:
                                    rectangle.X = 0xa2;
                                    rectangle.Y = 180;
                                    break;

                                case 2:
                                    rectangle.X = 180;
                                    rectangle.Y = 180;
                                    break;
                            }

                            mergeUp = true;
                            mergeDown = true;
                        }
                        else if (
                            num2 == -2 && num7 == type && index == -2 && num5 == -2
                        )
                        {
                            switch (frameNumber)
                            {
                                case 0:
                                    rectangle.X = 0xc6;
                                    rectangle.Y = 90;
                                    break;

                                case 1:
                                    rectangle.X = 0xc6;
                                    rectangle.Y = 0x6c;
                                    break;

                                case 2:
                                    rectangle.X = 0xc6;
                                    rectangle.Y = 0x7e;
                                    break;
                            }

                            mergeUp = true;
                            mergeLeft = true;
                            mergeRight = true;
                        }
                        else if (
                            num2 == type && num7 == -2 && index == -2 && num5 == -2
                        )
                        {
                            switch (frameNumber)
                            {
                                case 0:
                                    rectangle.X = 0xc6;
                                    rectangle.Y = 0x90;
                                    break;

                                case 1:
                                    rectangle.X = 0xc6;
                                    rectangle.Y = 0xa2;
                                    break;

                                case 2:
                                    rectangle.X = 0xc6;
                                    rectangle.Y = 180;
                                    break;
                            }

                            mergeDown = true;
                            mergeLeft = true;
                            mergeRight = true;
                        }
                        else if (
                            num2 == -2 && num7 == -2 && index == type && num5 == -2
                        )
                        {
                            switch (frameNumber)
                            {
                                case 0:
                                    rectangle.X = 0xd8;
                                    rectangle.Y = 0x90;
                                    break;

                                case 1:
                                    rectangle.X = 0xd8;
                                    rectangle.Y = 0xa2;
                                    break;

                                case 2:
                                    rectangle.X = 0xd8;
                                    rectangle.Y = 180;
                                    break;
                            }

                            mergeUp = true;
                            mergeDown = true;
                            mergeRight = true;
                        }
                        else if (
                            num2 == -2 && num7 == -2 && index == -2 && num5 == type
                        )
                        {
                            switch (frameNumber)
                            {
                                case 0:
                                    rectangle.X = 0xd8;
                                    rectangle.Y = 90;
                                    break;

                                case 1:
                                    rectangle.X = 0xd8;
                                    rectangle.Y = 0x6c;
                                    break;

                                case 2:
                                    rectangle.X = 0xd8;
                                    rectangle.Y = 0x7e;
                                    break;
                            }

                            mergeUp = true;
                            mergeDown = true;
                            mergeLeft = true;
                        }
                        else if (
                            num2 == -2 && num7 == -2 && index == -2 && num5 == -2
                        )
                        {
                            switch (frameNumber)
                            {
                                case 0:
                                    rectangle.X = 0x6c;
                                    rectangle.Y = 0xc6;
                                    break;

                                case 1:
                                    rectangle.X = 0x7e;
                                    rectangle.Y = 0xc6;
                                    break;

                                case 2:
                                    rectangle.X = 0x90;
                                    rectangle.Y = 0xc6;
                                    break;
                            }

                            mergeUp = true;
                            mergeDown = true;
                            mergeLeft = true;
                            mergeRight = true;
                        }
                        else if (
                            num2 == type && num7 == type && index == type
                            && num5 == type
                        )
                        {
                            if (num == -2)
                            {
                                switch (frameNumber)
                                {
                                    case 0:
                                        rectangle.X = 0x12;
                                        rectangle.Y = 0x6c;
                                        break;

                                    case 1:
                                        rectangle.X = 0x12;
                                        rectangle.Y = 0x90;
                                        break;

                                    case 2:
                                        rectangle.X = 0x12;
                                        rectangle.Y = 180;
                                        break;
                                }
                            }

                            if (num3 == -2)
                            {
                                switch (frameNumber)
                                {
                                    case 0:
                                        rectangle.X = 0;
                                        rectangle.Y = 0x6c;
                                        break;

                                    case 1:
                                        rectangle.X = 0;
                                        rectangle.Y = 0x90;
                                        break;

                                    case 2:
                                        rectangle.X = 0;
                                        rectangle.Y = 180;
                                        break;
                                }
                            }

                            if (num6 == -2)
                            {
                                switch (frameNumber)
                                {
                                    case 0:
                                        rectangle.X = 0x12;
                                        rectangle.Y = 90;
                                        break;

                                    case 1:
                                        rectangle.X = 0x12;
                                        rectangle.Y = 0x7e;
                                        break;

                                    case 2:
                                        rectangle.X = 0x12;
                                        rectangle.Y = 0xa2;
                                        break;
                                }
                            }

                            if (num8 == -2)
                            {
                                switch (frameNumber)
                                {
                                    case 0:
                                        rectangle.X = 0;
                                        rectangle.Y = 90;
                                        break;

                                    case 1:
                                        rectangle.X = 0;
                                        rectangle.Y = 0x7e;
                                        break;

                                    case 2:
                                        rectangle.X = 0;
                                        rectangle.Y = 0xa2;
                                        break;
                                }
                            }
                        }
                    }
                    else if (
                        num2 != -1 && num7 != -1 && index == -1 && num5 == type
                    )
                    {
                        if (num2 == -2 && num7 == type)
                        {
                            switch (frameNumber)
                            {
                                case 0:
                                    rectangle.X = 0x48;
                                    rectangle.Y = 0x90;
                                    break;

                                case 1:
                                    rectangle.X = 0x48;
                                    rectangle.Y = 0xa2;
                                    break;

                                case 2:
                                    rectangle.X = 0x48;
                                    rectangle.Y = 180;
                                    break;
                            }

                            mergeUp = true;
                        }
                        else if (num7 == -2 && num2 == type)
                        {
                            switch (frameNumber)
                            {
                                case 0:
                                    rectangle.X = 0x48;
                                    rectangle.Y = 90;
                                    break;

                                case 1:
                                    rectangle.X = 0x48;
                                    rectangle.Y = 0x6c;
                                    break;

                                case 2:
                                    rectangle.X = 0x48;
                                    rectangle.Y = 0x7e;
                                    break;
                            }

                            mergeDown = true;
                        }
                    }
                    else if (
                        num2 != -1 && num7 != -1 && index == type && num5 == -1
                    )
                    {
                        if (num2 == -2 && num7 == type)
                        {
                            switch (frameNumber)
                            {
                                case 0:
                                    rectangle.X = 90;
                                    rectangle.Y = 0x90;
                                    break;

                                case 1:
                                    rectangle.X = 90;
                                    rectangle.Y = 0xa2;
                                    break;

                                case 2:
                                    rectangle.X = 90;
                                    rectangle.Y = 180;
                                    break;
                            }

                            mergeUp = true;
                        }
                        else if (num7 == -2 && num2 == type)
                        {
                            switch (frameNumber)
                            {
                                case 0:
                                    rectangle.X = 90;
                                    rectangle.Y = 90;
                                    break;

                                case 1:
                                    rectangle.X = 90;
                                    rectangle.Y = 0x6c;
                                    break;

                                case 2:
                                    rectangle.X = 90;
                                    rectangle.Y = 0x7e;
                                    break;
                            }

                            mergeDown = true;
                        }
                    }
                    else if (
                        num2 == -1 && num7 == type && index != -1 && num5 != -1
                    )
                    {
                        if (index == -2 && num5 == type)
                        {
                            switch (frameNumber)
                            {
                                case 0:
                                    rectangle.X = 0;
                                    rectangle.Y = 0xc6;
                                    break;

                                case 1:
                                    rectangle.X = 0x12;
                                    rectangle.Y = 0xc6;
                                    break;

                                case 2:
                                    rectangle.X = 0x24;
                                    rectangle.Y = 0xc6;
                                    break;
                            }

                            mergeLeft = true;
                        }
                        else if (num5 == -2 && index == type)
                        {
                            switch (frameNumber)
                            {
                                case 0:
                                    rectangle.X = 0x36;
                                    rectangle.Y = 0xc6;
                                    break;

                                case 1:
                                    rectangle.X = 0x48;
                                    rectangle.Y = 0xc6;
                                    break;

                                case 2:
                                    rectangle.X = 90;
                                    rectangle.Y = 0xc6;
                                    break;
                            }

                            mergeRight = true;
                        }
                    }
                    else if (
                        num2 == type && num7 == -1 && index != -1 && num5 != -1
                    )
                    {
                        if (index == -2 && num5 == type)
                        {
                            switch (frameNumber)
                            {
                                case 0:
                                    rectangle.X = 0;
                                    rectangle.Y = 0xd8;
                                    break;

                                case 1:
                                    rectangle.X = 0x12;
                                    rectangle.Y = 0xd8;
                                    break;

                                case 2:
                                    rectangle.X = 0x24;
                                    rectangle.Y = 0xd8;
                                    break;
                            }

                            mergeLeft = true;
                        }
                        else if (num5 == -2 && index == type)
                        {
                            switch (frameNumber)
                            {
                                case 0:
                                    rectangle.X = 0x36;
                                    rectangle.Y = 0xd8;
                                    break;

                                case 1:
                                    rectangle.X = 0x48;
                                    rectangle.Y = 0xd8;
                                    break;

                                case 2:
                                    rectangle.X = 90;
                                    rectangle.Y = 0xd8;
                                    break;
                            }

                            mergeRight = true;
                        }
                    }
                    else if (num2 != -1 && num7 != -1 && index == -1 && num5 == -1)
                    {
                        if (num2 == -2 && num7 == -2)
                        {
                            switch (frameNumber)
                            {
                                case 0:
                                    rectangle.X = 0x6c;
                                    rectangle.Y = 0xd8;
                                    break;

                                case 1:
                                    rectangle.X = 0x6c;
                                    rectangle.Y = 0xea;
                                    break;

                                case 2:
                                    rectangle.X = 0x6c;
                                    rectangle.Y = 0xfc;
                                    break;
                            }

                            mergeUp = true;
                            mergeDown = true;
                        }
                        else if (num2 == -2)
                        {
                            switch (frameNumber)
                            {
                                case 0:
                                    rectangle.X = 0x7e;
                                    rectangle.Y = 0x90;
                                    break;

                                case 1:
                                    rectangle.X = 0x7e;
                                    rectangle.Y = 0xa2;
                                    break;

                                case 2:
                                    rectangle.X = 0x7e;
                                    rectangle.Y = 180;
                                    break;
                            }

                            mergeUp = true;
                        }
                        else if (num7 == -2)
                        {
                            switch (frameNumber)
                            {
                                case 0:
                                    rectangle.X = 0x7e;
                                    rectangle.Y = 90;
                                    break;

                                case 1:
                                    rectangle.X = 0x7e;
                                    rectangle.Y = 0x6c;
                                    break;

                                case 2:
                                    rectangle.X = 0x7e;
                                    rectangle.Y = 0x7e;
                                    break;
                            }

                            mergeDown = true;
                        }
                    }
                    else if (num2 == -1 && num7 == -1 && index != -1 && num5 != -1)
                    {
                        if (index == -2 && num5 == -2)
                        {
                            switch (frameNumber)
                            {
                                case 0:
                                    rectangle.X = 0xa2;
                                    rectangle.Y = 0xc6;
                                    break;

                                case 1:
                                    rectangle.X = 180;
                                    rectangle.Y = 0xc6;
                                    break;

                                case 2:
                                    rectangle.X = 0xc6;
                                    rectangle.Y = 0xc6;
                                    break;
                            }

                            mergeLeft = true;
                            mergeRight = true;
                        }
                        else if (index == -2)
                        {
                            switch (frameNumber)
                            {
                                case 0:
                                    rectangle.X = 0;
                                    rectangle.Y = 0xfc;
                                    break;

                                case 1:
                                    rectangle.X = 0x12;
                                    rectangle.Y = 0xfc;
                                    break;

                                case 2:
                                    rectangle.X = 0x24;
                                    rectangle.Y = 0xfc;
                                    break;
                            }

                            mergeLeft = true;
                        }
                        else if (num5 == -2)
                        {
                            switch (frameNumber)
                            {
                                case 0:
                                    rectangle.X = 0x36;
                                    rectangle.Y = 0xfc;
                                    break;

                                case 1:
                                    rectangle.X = 0x48;
                                    rectangle.Y = 0xfc;
                                    break;

                                case 2:
                                    rectangle.X = 90;
                                    rectangle.Y = 0xfc;
                                    break;
                            }

                            mergeRight = true;
                        }
                    }
                    else if (num2 == -2 && num7 == -1 && index == -1 && num5 == -1)
                    {
                        switch (frameNumber)
                        {
                            case 0:
                                rectangle.X = 0x6c;
                                rectangle.Y = 0x90;
                                break;

                            case 1:
                                rectangle.X = 0x6c;
                                rectangle.Y = 0xa2;
                                break;

                            case 2:
                                rectangle.X = 0x6c;
                                rectangle.Y = 180;
                                break;
                        }

                        mergeUp = true;
                    }
                    else if (num2 == -1 && num7 == -2 && index == -1 && num5 == -1)
                    {
                        switch (frameNumber)
                        {
                            case 0:
                                rectangle.X = 0x6c;
                                rectangle.Y = 90;
                                break;

                            case 1:
                                rectangle.X = 0x6c;
                                rectangle.Y = 0x6c;
                                break;

                            case 2:
                                rectangle.X = 0x6c;
                                rectangle.Y = 0x7e;
                                break;
                        }

                        mergeDown = true;
                    }
                    else if (num2 == -1 && num7 == -1 && index == -2 && num5 == -1)
                    {
                        switch (frameNumber)
                        {
                            case 0:
                                rectangle.X = 0;
                                rectangle.Y = 0xea;
                                break;

                            case 1:
                                rectangle.X = 0x12;
                                rectangle.Y = 0xea;
                                break;

                            case 2:
                                rectangle.X = 0x24;
                                rectangle.Y = 0xea;
                                break;
                        }

                        mergeLeft = true;
                    }
                    else if (num2 == -1 && num7 == -1 && index == -1 && num5 == -2)
                    {
                        switch (frameNumber)
                        {
                            case 0:
                                rectangle.X = 0x36;
                                rectangle.Y = 0xea;
                                break;

                            case 1:
                                rectangle.X = 0x48;
                                rectangle.Y = 0xea;
                                break;

                            case 2:
                                rectangle.X = 90;
                                rectangle.Y = 0xea;
                                break;
                        }

                        mergeRight = true;
                    }
                }

                if (rectangle.X < 0 || rectangle.Y < 0)
                {
                    if (type == 2)
                    {
                        if (num2 == -2)
                            num2 = type;

                        if (num7 == -2)
                            num7 = type;

                        if (index == -2)
                            index = type;

                        if (num5 == -2)
                            num5 = type;

                        if (num == -2)
                            num = type;

                        if (num3 == -2)
                            num3 = type;

                        if (num6 == -2)
                            num6 = type;

                        if (num8 == -2)
                            num8 = type;
                    }

                    if (
                        num2 == type && num7 == type && (index == type) & (num5 == type)
                    )
                    {
                        if (num != type && num3 != type)
                        {
                            switch (frameNumber)
                            {
                                case 0:
                                    rectangle.X = 0x6c;
                                    rectangle.Y = 0x12;
                                    break;

                                case 1:
                                    rectangle.X = 0x7e;
                                    rectangle.Y = 0x12;
                                    break;

                                case 2:
                                    rectangle.X = 0x90;
                                    rectangle.Y = 0x12;
                                    break;
                            }
                        }
                        else if (num6 != type && num8 != type)
                        {
                            switch (frameNumber)
                            {
                                case 0:
                                    rectangle.X = 0x6c;
                                    rectangle.Y = 0x24;
                                    break;

                                case 1:
                                    rectangle.X = 0x7e;
                                    rectangle.Y = 0x24;
                                    break;

                                case 2:
                                    rectangle.X = 0x90;
                                    rectangle.Y = 0x24;
                                    break;
                            }
                        }
                        else if (num != type && num6 != type)
                        {
                            switch (frameNumber)
                            {
                                case 0:
                                    rectangle.X = 180;
                                    rectangle.Y = 0;
                                    break;

                                case 1:
                                    rectangle.X = 180;
                                    rectangle.Y = 0x12;
                                    break;

                                case 2:
                                    rectangle.X = 180;
                                    rectangle.Y = 0x24;
                                    break;
                            }
                        }
                        else if (num3 != type && num8 != type)
                        {
                            switch (frameNumber)
                            {
                                case 0:
                                    rectangle.X = 0xc6;
                                    rectangle.Y = 0;
                                    break;

                                case 1:
                                    rectangle.X = 0xc6;
                                    rectangle.Y = 0x12;
                                    break;

                                case 2:
                                    rectangle.X = 0xc6;
                                    rectangle.Y = 0x24;
                                    break;
                            }
                        }
                        else
                        {
                            switch (frameNumber)
                            {
                                case 0:
                                    rectangle.X = 0x12;
                                    rectangle.Y = 0x12;
                                    break;

                                case 1:
                                    rectangle.X = 0x24;
                                    rectangle.Y = 0x12;
                                    break;

                                case 2:
                                    rectangle.X = 0x36;
                                    rectangle.Y = 0x12;
                                    break;
                            }
                        }
                    }
                    else if (
                        num2 != type && num7 == type && (index == type) & (num5 == type)
                    )
                    {
                        switch (frameNumber)
                        {
                            case 0:
                                rectangle.X = 0x12;
                                rectangle.Y = 0;
                                break;

                            case 1:
                                rectangle.X = 0x24;
                                rectangle.Y = 0;
                                break;

                            case 2:
                                rectangle.X = 0x36;
                                rectangle.Y = 0;
                                break;
                        }
                    }
                    else if (
                        num2 == type && num7 != type && (index == type) & (num5 == type)
                    )
                    {
                        switch (frameNumber)
                        {
                            case 0:
                                rectangle.X = 0x12;
                                rectangle.Y = 0x24;
                                break;

                            case 1:
                                rectangle.X = 0x24;
                                rectangle.Y = 0x24;
                                break;

                            case 2:
                                rectangle.X = 0x36;
                                rectangle.Y = 0x24;
                                break;
                        }
                    }
                    else if (
                        num2 == type && num7 == type && (index != type) & (num5 == type)
                    )
                    {
                        switch (frameNumber)
                        {
                            case 0:
                                rectangle.X = 0;
                                rectangle.Y = 0;
                                break;

                            case 1:
                                rectangle.X = 0;
                                rectangle.Y = 0x12;
                                break;

                            case 2:
                                rectangle.X = 0;
                                rectangle.Y = 0x24;
                                break;
                        }
                    }
                    else if (
                        num2 == type && num7 == type && (index == type) & (num5 != type)
                    )
                    {
                        switch (frameNumber)
                        {
                            case 0:
                                rectangle.X = 0x48;
                                rectangle.Y = 0;
                                break;

                            case 1:
                                rectangle.X = 0x48;
                                rectangle.Y = 0x12;
                                break;

                            case 2:
                                rectangle.X = 0x48;
                                rectangle.Y = 0x24;
                                break;
                        }
                    }
                    else if (
                        num2 != type && num7 == type && (index != type) & (num5 == type)
                    )
                    {
                        switch (frameNumber)
                        {
                            case 0:
                                rectangle.X = 0;
                                rectangle.Y = 0x36;
                                break;

                            case 1:
                                rectangle.X = 0x24;
                                rectangle.Y = 0x36;
                                break;

                            case 2:
                                rectangle.X = 0x48;
                                rectangle.Y = 0x36;
                                break;
                        }
                    }
                    else if (
                        num2 != type && num7 == type && (index == type) & (num5 != type)
                    )
                    {
                        switch (frameNumber)
                        {
                            case 0:
                                rectangle.X = 0x12;
                                rectangle.Y = 0x36;
                                break;

                            case 1:
                                rectangle.X = 0x36;
                                rectangle.Y = 0x36;
                                break;

                            case 2:
                                rectangle.X = 90;
                                rectangle.Y = 0x36;
                                break;
                        }
                    }
                    else if (
                        num2 == type && num7 != type && (index != type) & (num5 == type)
                    )
                    {
                        switch (frameNumber)
                        {
                            case 0:
                                rectangle.X = 0;
                                rectangle.Y = 0x48;
                                break;

                            case 1:
                                rectangle.X = 0x24;
                                rectangle.Y = 0x48;
                                break;

                            case 2:
                                rectangle.X = 0x48;
                                rectangle.Y = 0x48;
                                break;
                        }
                    }
                    else if (
                        num2 == type && num7 != type && (index == type) & (num5 != type)
                    )
                    {
                        switch (frameNumber)
                        {
                            case 0:
                                rectangle.X = 0x12;
                                rectangle.Y = 0x48;
                                break;

                            case 1:
                                rectangle.X = 0x36;
                                rectangle.Y = 0x48;
                                break;

                            case 2:
                                rectangle.X = 90;
                                rectangle.Y = 0x48;
                                break;
                        }
                    }
                    else if (
                        num2 == type && num7 == type && (index != type) & (num5 != type)
                    )
                    {
                        switch (frameNumber)
                        {
                            case 0:
                                rectangle.X = 90;
                                rectangle.Y = 0;
                                break;

                            case 1:
                                rectangle.X = 90;
                                rectangle.Y = 0x12;
                                break;

                            case 2:
                                rectangle.X = 90;
                                rectangle.Y = 0x24;
                                break;
                        }
                    }
                    else if (
                        num2 != type && num7 != type && (index == type) & (num5 == type)
                    )
                    {
                        switch (frameNumber)
                        {
                            case 0:
                                rectangle.X = 0x6c;
                                rectangle.Y = 0x48;
                                break;

                            case 1:
                                rectangle.X = 0x7e;
                                rectangle.Y = 0x48;
                                break;

                            case 2:
                                rectangle.X = 0x90;
                                rectangle.Y = 0x48;
                                break;
                        }
                    }
                    else if (
                        num2 != type && num7 == type && (index != type) & (num5 != type)
                    )
                    {
                        switch (frameNumber)
                        {
                            case 0:
                                rectangle.X = 0x6c;
                                rectangle.Y = 0;
                                break;

                            case 1:
                                rectangle.X = 0x7e;
                                rectangle.Y = 0;
                                break;

                            case 2:
                                rectangle.X = 0x90;
                                rectangle.Y = 0;
                                break;
                        }
                    }
                    else if (
                        num2 == type && num7 != type && (index != type) & (num5 != type)
                    )
                    {
                        switch (frameNumber)
                        {
                            case 0:
                                rectangle.X = 0x6c;
                                rectangle.Y = 0x36;
                                break;

                            case 1:
                                rectangle.X = 0x7e;
                                rectangle.Y = 0x36;
                                break;

                            case 2:
                                rectangle.X = 0x90;
                                rectangle.Y = 0x36;
                                break;
                        }
                    }
                    else if (
                        num2 != type && num7 != type && (index != type) & (num5 == type)
                    )
                    {
                        switch (frameNumber)
                        {
                            case 0:
                                rectangle.X = 0xa2;
                                rectangle.Y = 0;
                                break;

                            case 1:
                                rectangle.X = 0xa2;
                                rectangle.Y = 0x12;
                                break;

                            case 2:
                                rectangle.X = 0xa2;
                                rectangle.Y = 0x24;
                                break;
                        }
                    }
                    else if (
                        num2 != type && num7 != type && (index == type) & (num5 != type)
                    )
                    {
                        switch (frameNumber)
                        {
                            case 0:
                                rectangle.X = 0xd8;
                                rectangle.Y = 0;
                                break;

                            case 1:
                                rectangle.X = 0xd8;
                                rectangle.Y = 0x12;
                                break;

                            case 2:
                                rectangle.X = 0xd8;
                                rectangle.Y = 0x24;
                                break;
                        }
                    }
                    else if (
                        num2 != type && num7 != type && (index != type) & (num5 != type)
                    )
                    {
                        switch (frameNumber)
                        {
                            case 0:
                                rectangle.X = 0xa2;
                                rectangle.Y = 0x36;
                                break;

                            case 1:
                                rectangle.X = 180;
                                rectangle.Y = 0x36;
                                break;

                            case 2:
                                rectangle.X = 0xc6;
                                rectangle.Y = 0x36;
                                break;
                        }
                    }
                }

                if (rectangle.X <= -1 || rectangle.Y <= -1)
                {
                    if (frameNumber <= 0)
                    {
                        rectangle.X = 0x12;
                        rectangle.Y = 0x12;
                    }

                    if (frameNumber == 1)
                    {
                        rectangle.X = 0x24;
                        rectangle.Y = 0x12;
                    }

                    if (frameNumber >= 2)
                    {
                        rectangle.X = 0x36;
                        rectangle.Y = 0x12;
                    }
                }

                Main.Tile[i, j].FrameX = (short)rectangle.X;
                Main.Tile[i, j].FrameY = (short)rectangle.Y;
                if (
                    rectangle.X != frameX && rectangle.Y != frameY && frameX >= 0
                    && frameY >= 0
                )
                {
                    var up = mergeUp;
                    var down = mergeDown;
                    var left = mergeLeft;
                    var right = mergeRight;
                    TileFrame(i - 1, j);
                    TileFrame(i + 1, j);
                    TileFrame(i, j - 1);
                    TileFrame(i, j + 1);
                    mergeUp = up;
                    mergeDown = down;
                    mergeLeft = left;
                    mergeRight = right;
                }
            }
        }
    }

    private static void TileRunner(
        int i,
        int j,
        double strength,
        int steps,
        int type,
        bool addTile = false,
        float speedX = 0f,
        float speedY = 0f
    )
    {
        Vector2 vector;
        Vector2 vector2;
        var num5 = strength;
        float num6 = steps;
        vector.X = i;
        vector.Y = j;
        vector2.X = Main.Rand.Next(-10, 11) * 0.1f;
        vector2.Y = Main.Rand.Next(-10, 11) * 0.1f;
        if (speedX != 0f || speedY != 0f)
        {
            vector2.X = speedX;
            vector2.Y = speedY;
        }

        while (num5 > 0.0 && num6 > 0f)
        {
            num5 = strength * (num6 / steps);
            num6--;
            var num = (int)(vector.X - num5 * 0.5);
            var num3 = (int)(vector.X + num5 * 0.5);
            var num2 = (int)(vector.Y - num5 * 0.5);
            var num4 = (int)(vector.Y + num5 * 0.5);
            if (num < 0)
                num = 0;

            if (num3 > 0x1389)
                num3 = 0x1389;

            if (num2 < 0)
                num2 = 0;

            if (num4 > 0x9c5)
                num4 = 0x9c5;

            for (var k = num; k < num3; k++)
            for (var m = num2; m < num4; m++)
                if (
                    Math.Abs(k - vector.X) + Math.Abs(m - vector.Y)
                    < strength * 0.5 * (1.0 + Main.Rand.Next(-10, 11) * 0.015)
                )
                {
                    if (type == -1)
                        Main.Tile[k, m].Active = false;
                    else
                    {
                        if (addTile)
                            Main.Tile[k, m].Active = true;

                        Main.Tile[k, m].Type = (byte)type;
                    }
                }

            vector += vector2;
            vector2.X += Main.Rand.Next(-10, 11) * 0.05f;
            vector2.Y += Main.Rand.Next(-10, 11) * 0.05f;
            if (vector2.X > 1f)
                vector2.X = 1f;

            if (vector2.X < -1f)
                vector2.X = -1f;

            if (vector2.Y > 1f)
                vector2.Y = 1f;

            if (vector2.Y < -1f)
                vector2.Y = -1f;
        }
    }

    public static void UpdateWorld()
    {
        var num = 0.0002f;
        for (var i = 0; i < 1.25075E+07f * num; i++)
        {
            var num3 = Main.Rand.Next(0x1389);
            var num4 = Main.Rand.Next((int)Main.worldSurface - 1);
            var num5 = num3 - 1;
            var num6 = num3 + 2;
            var j = num4 - 1;
            var num8 = num4 + 2;
            if (num5 < 0)
                num5 = 0;

            if (num6 > 0x1389)
                num6 = 0x1389;

            if (j < 0)
                j = 0;

            if (num8 > 0x9c5)
                num8 = 0x9c5;

            if (Main.Tile[num3, num4].Active && Main.Tile[num3, num4].Type == 2)
            {
                if (!(Main.Tile[num3, j].Active || Main.Rand.Next(10) != 0))
                    PlaceTile(num3, j, 3, true);

                for (var k = num5; k < num6; k++)
                for (var m = j; m < num8; m++)
                    if (
                        (num3 != k || num4 != m)
                        && Main.Tile[k, m].Active && Main.Tile[k, m].Type == 0
                    )
                    {
                        SpreadGrass(k, m, 0, 2, false);
                        if (Main.Tile[k, m].Type == 2)
                            SquareTileFrame(k, m);
                    }
            }
        }
    }

    private static void WallFrame(int i, int j, bool resetFrame = false)
    {
        var num = -1;
        var num2 = -1;
        var num3 = -1;
        var num4 = -1;
        var num5 = -1;
        var num6 = -1;
        var num7 = -1;
        var num8 = -1;
        int wall = Main.Tile[i, j].Wall;
        if (wall != 0)
        {
            Rectangle rectangle;
            int wallFrameX = Main.Tile[i, j].WallFrameX;
            int wallFrameY = Main.Tile[i, j].WallFrameY;
            rectangle.X = -1;
            rectangle.Y = -1;
            if (i - 1 < 0)
            {
                num = wall;
                num4 = wall;
                num6 = wall;
            }

            if (i + 1 >= 0x1389)
            {
                num3 = wall;
                num5 = wall;
                num8 = wall;
            }

            if (j - 1 < 0)
            {
                num = wall;
                num2 = wall;
                num3 = wall;
            }

            if (j + 1 >= 0x9c5)
            {
                num6 = wall;
                num7 = wall;
                num8 = wall;
            }

            if (i - 1 >= 0)
                num4 = Main.Tile[i - 1, j].Wall;

            if (i + 1 < 0x1389)
                num5 = Main.Tile[i + 1, j].Wall;

            if (j - 1 >= 0)
                num2 = Main.Tile[i, j - 1].Wall;

            if (j + 1 < 0x9c5)
                num7 = Main.Tile[i, j + 1].Wall;

            if (i - 1 >= 0 && j - 1 >= 0)
                num = Main.Tile[i - 1, j - 1].Wall;

            if (i + 1 < 0x1389 && j - 1 >= 0)
                num3 = Main.Tile[i + 1, j - 1].Wall;

            if (i - 1 >= 0 && j + 1 < 0x9c5)
                num6 = Main.Tile[i - 1, j + 1].Wall;

            if (i + 1 < 0x1389 && j + 1 < 0x9c5)
                num8 = Main.Tile[i + 1, j + 1].Wall;

            var wallFrameNumber = 0;
            if (resetFrame)
            {
                wallFrameNumber = Main.Rand.Next(0, 3);
                Main.Tile[i, j].WallFrameNumber = (byte)wallFrameNumber;
            }
            else
                wallFrameNumber = Main.Tile[i, j].WallFrameNumber;

            if (rectangle.X < 0 || rectangle.Y < 0)
            {
                if (num2 == wall && num7 == wall && (num4 == wall) & (num5 == wall))
                {
                    if (num != wall && num3 != wall)
                    {
                        switch (wallFrameNumber)
                        {
                            case 0:
                                rectangle.X = 0x6c;
                                rectangle.Y = 0x12;
                                break;

                            case 1:
                                rectangle.X = 0x7e;
                                rectangle.Y = 0x12;
                                break;

                            case 2:
                                rectangle.X = 0x90;
                                rectangle.Y = 0x12;
                                break;
                        }
                    }
                    else if (num6 != wall && num8 != wall)
                    {
                        switch (wallFrameNumber)
                        {
                            case 0:
                                rectangle.X = 0x6c;
                                rectangle.Y = 0x24;
                                break;

                            case 1:
                                rectangle.X = 0x7e;
                                rectangle.Y = 0x24;
                                break;

                            case 2:
                                rectangle.X = 0x90;
                                rectangle.Y = 0x24;
                                break;
                        }
                    }
                    else if (num != wall && num6 != wall)
                    {
                        switch (wallFrameNumber)
                        {
                            case 0:
                                rectangle.X = 180;
                                rectangle.Y = 0;
                                break;

                            case 1:
                                rectangle.X = 180;
                                rectangle.Y = 0x12;
                                break;

                            case 2:
                                rectangle.X = 180;
                                rectangle.Y = 0x24;
                                break;
                        }
                    }
                    else if (num3 != wall && num8 != wall)
                    {
                        switch (wallFrameNumber)
                        {
                            case 0:
                                rectangle.X = 0xc6;
                                rectangle.Y = 0;
                                break;

                            case 1:
                                rectangle.X = 0xc6;
                                rectangle.Y = 0x12;
                                break;

                            case 2:
                                rectangle.X = 0xc6;
                                rectangle.Y = 0x24;
                                break;
                        }
                    }
                    else
                    {
                        switch (wallFrameNumber)
                        {
                            case 0:
                                rectangle.X = 0x12;
                                rectangle.Y = 0x12;
                                break;

                            case 1:
                                rectangle.X = 0x24;
                                rectangle.Y = 0x12;
                                break;

                            case 2:
                                rectangle.X = 0x36;
                                rectangle.Y = 0x12;
                                break;
                        }
                    }
                }
                else if (
                    num2 != wall && num7 == wall && (num4 == wall) & (num5 == wall)
                )
                {
                    switch (wallFrameNumber)
                    {
                        case 0:
                            rectangle.X = 0x12;
                            rectangle.Y = 0;
                            break;

                        case 1:
                            rectangle.X = 0x24;
                            rectangle.Y = 0;
                            break;

                        case 2:
                            rectangle.X = 0x36;
                            rectangle.Y = 0;
                            break;
                    }
                }
                else if (
                    num2 == wall && num7 != wall && (num4 == wall) & (num5 == wall)
                )
                {
                    switch (wallFrameNumber)
                    {
                        case 0:
                            rectangle.X = 0x12;
                            rectangle.Y = 0x24;
                            break;

                        case 1:
                            rectangle.X = 0x24;
                            rectangle.Y = 0x24;
                            break;

                        case 2:
                            rectangle.X = 0x36;
                            rectangle.Y = 0x24;
                            break;
                    }
                }
                else if (
                    num2 == wall && num7 == wall && (num4 != wall) & (num5 == wall)
                )
                {
                    switch (wallFrameNumber)
                    {
                        case 0:
                            rectangle.X = 0;
                            rectangle.Y = 0;
                            break;

                        case 1:
                            rectangle.X = 0;
                            rectangle.Y = 0x12;
                            break;

                        case 2:
                            rectangle.X = 0;
                            rectangle.Y = 0x24;
                            break;
                    }
                }
                else if (
                    num2 == wall && num7 == wall && (num4 == wall) & (num5 != wall)
                )
                {
                    switch (wallFrameNumber)
                    {
                        case 0:
                            rectangle.X = 0x48;
                            rectangle.Y = 0;
                            break;

                        case 1:
                            rectangle.X = 0x48;
                            rectangle.Y = 0x12;
                            break;

                        case 2:
                            rectangle.X = 0x48;
                            rectangle.Y = 0x24;
                            break;
                    }
                }
                else if (
                    num2 != wall && num7 == wall && (num4 != wall) & (num5 == wall)
                )
                {
                    switch (wallFrameNumber)
                    {
                        case 0:
                            rectangle.X = 0;
                            rectangle.Y = 0x36;
                            break;

                        case 1:
                            rectangle.X = 0x24;
                            rectangle.Y = 0x36;
                            break;

                        case 2:
                            rectangle.X = 0x48;
                            rectangle.Y = 0x36;
                            break;
                    }
                }
                else if (
                    num2 != wall && num7 == wall && (num4 == wall) & (num5 != wall)
                )
                {
                    switch (wallFrameNumber)
                    {
                        case 0:
                            rectangle.X = 0x12;
                            rectangle.Y = 0x36;
                            break;

                        case 1:
                            rectangle.X = 0x36;
                            rectangle.Y = 0x36;
                            break;

                        case 2:
                            rectangle.X = 90;
                            rectangle.Y = 0x36;
                            break;
                    }
                }
                else if (
                    num2 == wall && num7 != wall && (num4 != wall) & (num5 == wall)
                )
                {
                    switch (wallFrameNumber)
                    {
                        case 0:
                            rectangle.X = 0;
                            rectangle.Y = 0x48;
                            break;

                        case 1:
                            rectangle.X = 0x24;
                            rectangle.Y = 0x48;
                            break;

                        case 2:
                            rectangle.X = 0x48;
                            rectangle.Y = 0x48;
                            break;
                    }
                }
                else if (
                    num2 == wall && num7 != wall && (num4 == wall) & (num5 != wall)
                )
                {
                    switch (wallFrameNumber)
                    {
                        case 0:
                            rectangle.X = 0x12;
                            rectangle.Y = 0x48;
                            break;

                        case 1:
                            rectangle.X = 0x36;
                            rectangle.Y = 0x48;
                            break;

                        case 2:
                            rectangle.X = 90;
                            rectangle.Y = 0x48;
                            break;
                    }
                }
                else if (
                    num2 == wall && num7 == wall && (num4 != wall) & (num5 != wall)
                )
                {
                    switch (wallFrameNumber)
                    {
                        case 0:
                            rectangle.X = 90;
                            rectangle.Y = 0;
                            break;

                        case 1:
                            rectangle.X = 90;
                            rectangle.Y = 0x12;
                            break;

                        case 2:
                            rectangle.X = 90;
                            rectangle.Y = 0x24;
                            break;
                    }
                }
                else if (
                    num2 != wall && num7 != wall && (num4 == wall) & (num5 == wall)
                )
                {
                    switch (wallFrameNumber)
                    {
                        case 0:
                            rectangle.X = 0x6c;
                            rectangle.Y = 0x48;
                            break;

                        case 1:
                            rectangle.X = 0x7e;
                            rectangle.Y = 0x48;
                            break;

                        case 2:
                            rectangle.X = 0x90;
                            rectangle.Y = 0x48;
                            break;
                    }
                }
                else if (
                    num2 != wall && num7 == wall && (num4 != wall) & (num5 != wall)
                )
                {
                    switch (wallFrameNumber)
                    {
                        case 0:
                            rectangle.X = 0x6c;
                            rectangle.Y = 0;
                            break;

                        case 1:
                            rectangle.X = 0x7e;
                            rectangle.Y = 0;
                            break;

                        case 2:
                            rectangle.X = 0x90;
                            rectangle.Y = 0;
                            break;
                    }
                }
                else if (
                    num2 == wall && num7 != wall && (num4 != wall) & (num5 != wall)
                )
                {
                    switch (wallFrameNumber)
                    {
                        case 0:
                            rectangle.X = 0x6c;
                            rectangle.Y = 0x36;
                            break;

                        case 1:
                            rectangle.X = 0x7e;
                            rectangle.Y = 0x36;
                            break;

                        case 2:
                            rectangle.X = 0x90;
                            rectangle.Y = 0x36;
                            break;
                    }
                }
                else if (
                    num2 != wall && num7 != wall && (num4 != wall) & (num5 == wall)
                )
                {
                    switch (wallFrameNumber)
                    {
                        case 0:
                            rectangle.X = 0xa2;
                            rectangle.Y = 0;
                            break;

                        case 1:
                            rectangle.X = 0xa2;
                            rectangle.Y = 0x12;
                            break;

                        case 2:
                            rectangle.X = 0xa2;
                            rectangle.Y = 0x24;
                            break;
                    }
                }
                else if (
                    num2 != wall && num7 != wall && (num4 == wall) & (num5 != wall)
                )
                {
                    switch (wallFrameNumber)
                    {
                        case 0:
                            rectangle.X = 0xd8;
                            rectangle.Y = 0;
                            break;

                        case 1:
                            rectangle.X = 0xd8;
                            rectangle.Y = 0x12;
                            break;

                        case 2:
                            rectangle.X = 0xd8;
                            rectangle.Y = 0x24;
                            break;
                    }
                }
                else if (
                    num2 != wall && num7 != wall && (num4 != wall) & (num5 != wall)
                )
                {
                    switch (wallFrameNumber)
                    {
                        case 0:
                            rectangle.X = 0xa2;
                            rectangle.Y = 0x36;
                            break;

                        case 1:
                            rectangle.X = 180;
                            rectangle.Y = 0x36;
                            break;

                        case 2:
                            rectangle.X = 0xc6;
                            rectangle.Y = 0x36;
                            break;
                    }
                }
            }

            if (rectangle.X <= -1 || rectangle.Y <= -1)
            {
                if (wallFrameNumber <= 0)
                {
                    rectangle.X = 0x12;
                    rectangle.Y = 0x12;
                }

                if (wallFrameNumber == 1)
                {
                    rectangle.X = 0x24;
                    rectangle.Y = 0x12;
                }

                if (wallFrameNumber >= 2)
                {
                    rectangle.X = 0x36;
                    rectangle.Y = 0x12;
                }
            }

            Main.Tile[i, j].WallFrameX = (byte)rectangle.X;
            Main.Tile[i, j].WallFrameY = (byte)rectangle.Y;
            if (
                rectangle.X != wallFrameX && rectangle.Y != wallFrameY
            )
            {
            }
        }
    }
}
