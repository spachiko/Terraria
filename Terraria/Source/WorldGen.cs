namespace Terraria
{
    using System;
    using System.Runtime.InteropServices;
    using Microsoft.Xna.Framework;

    internal class WorldGen
    {
        private static bool destroyObject = false;
        private static bool mergeDown = false;
        private static bool mergeLeft = false;
        private static bool mergeRight = false;
        private static bool mergeUp = false;

        public static void AddPlants()
        {
            for (int i = 0; i < 0x1389; i++)
            {
                for (int j = 1; j < 0x9c5; j++)
                {
                    if (
                        ((Main.tile[i, j].type == 2) && Main.tile[i, j].active)
                        && !Main.tile[i, j - 1].active
                    )
                    {
                        PlaceTile(i, j - 1, 3, true);
                    }
                }
            }
        }

        public static void AddTrees()
        {
            for (int i = 1; i < 0x1388; i++)
            {
                for (int j = 20; j < Main.worldSurface; j++)
                {
                    if (
                        (Main.tile[i, j].active && (Main.tile[i, j].type == 2))
                        && (
                            (
                                (
                                    (Main.tile[i - 1, j].active && (Main.tile[i - 1, j].type == 2))
                                    && Main.tile[i + 1, j].active
                                ) && (Main.tile[i + 1, j].type == 2)
                            ) && EmptyTileCheck(i - 2, i + 2, j - 14, j - 1)
                        )
                    )
                    {
                        int num3;
                        bool flag = false;
                        bool flag2 = false;
                        int num5 = Main.rand.Next(5, 15);
                        for (int k = j - num5; k < j; k++)
                        {
                            Main.tile[i, k].frameNumber = (byte)Main.rand.Next(3);
                            Main.tile[i, k].active = true;
                            Main.tile[i, k].type = 5;
                            num3 = Main.rand.Next(3);
                            int num4 = Main.rand.Next(10);
                            if ((k == (j - 1)) || (k == (j - num5)))
                            {
                                num4 = 0;
                            }
                            goto Label_0184;
                            Label_0175:
                            num4 = Main.rand.Next(10);
                            Label_0184:
                            ;
                            if (
                                (((num4 == 5) || (num4 == 7)) && flag)
                                || (((num4 == 6) || (num4 == 7)) && flag2)
                            )
                            {
                                goto Label_0175;
                            }
                            flag = false;
                            flag2 = false;
                            if ((num4 == 5) || (num4 == 7))
                            {
                                flag = true;
                            }
                            if ((num4 == 6) || (num4 == 7))
                            {
                                flag2 = true;
                            }
                            if (num4 == 1)
                            {
                                switch (num3)
                                {
                                    case 0:
                                        Main.tile[i, k].frameX = 0;
                                        Main.tile[i, k].frameY = 0x42;
                                        break;

                                    case 1:
                                        Main.tile[i, k].frameX = 0;
                                        Main.tile[i, k].frameY = 0x58;
                                        break;

                                    case 2:
                                        Main.tile[i, k].frameX = 0;
                                        Main.tile[i, k].frameY = 110;
                                        break;
                                }
                            }
                            else if (num4 == 2)
                            {
                                switch (num3)
                                {
                                    case 0:
                                        Main.tile[i, k].frameX = 0x16;
                                        Main.tile[i, k].frameY = 0;
                                        break;

                                    case 1:
                                        Main.tile[i, k].frameX = 0x16;
                                        Main.tile[i, k].frameY = 0x16;
                                        break;

                                    case 2:
                                        Main.tile[i, k].frameX = 0x16;
                                        Main.tile[i, k].frameY = 0x2c;
                                        break;
                                }
                            }
                            else if (num4 == 3)
                            {
                                switch (num3)
                                {
                                    case 0:
                                        Main.tile[i, k].frameX = 0x2c;
                                        Main.tile[i, k].frameY = 0x42;
                                        break;

                                    case 1:
                                        Main.tile[i, k].frameX = 0x2c;
                                        Main.tile[i, k].frameY = 0x58;
                                        break;

                                    case 2:
                                        Main.tile[i, k].frameX = 0x2c;
                                        Main.tile[i, k].frameY = 110;
                                        break;
                                }
                            }
                            else if (num4 == 4)
                            {
                                switch (num3)
                                {
                                    case 0:
                                        Main.tile[i, k].frameX = 0x16;
                                        Main.tile[i, k].frameY = 0x42;
                                        break;

                                    case 1:
                                        Main.tile[i, k].frameX = 0x16;
                                        Main.tile[i, k].frameY = 0x58;
                                        break;

                                    case 2:
                                        Main.tile[i, k].frameX = 0x16;
                                        Main.tile[i, k].frameY = 110;
                                        break;
                                }
                            }
                            else if (num4 == 5)
                            {
                                switch (num3)
                                {
                                    case 0:
                                        Main.tile[i, k].frameX = 0x58;
                                        Main.tile[i, k].frameY = 0;
                                        break;

                                    case 1:
                                        Main.tile[i, k].frameX = 0x58;
                                        Main.tile[i, k].frameY = 0x16;
                                        break;

                                    case 2:
                                        Main.tile[i, k].frameX = 0x58;
                                        Main.tile[i, k].frameY = 0x2c;
                                        break;
                                }
                            }
                            else if (num4 == 6)
                            {
                                switch (num3)
                                {
                                    case 0:
                                        Main.tile[i, k].frameX = 0x42;
                                        Main.tile[i, k].frameY = 0x42;
                                        break;

                                    case 1:
                                        Main.tile[i, k].frameX = 0x42;
                                        Main.tile[i, k].frameY = 0x58;
                                        break;

                                    case 2:
                                        Main.tile[i, k].frameX = 0x42;
                                        Main.tile[i, k].frameY = 110;
                                        break;
                                }
                            }
                            else if (num4 == 7)
                            {
                                switch (num3)
                                {
                                    case 0:
                                        Main.tile[i, k].frameX = 110;
                                        Main.tile[i, k].frameY = 0x42;
                                        break;

                                    case 1:
                                        Main.tile[i, k].frameX = 110;
                                        Main.tile[i, k].frameY = 0x58;
                                        break;

                                    case 2:
                                        Main.tile[i, k].frameX = 110;
                                        Main.tile[i, k].frameY = 110;
                                        break;
                                }
                            }
                            else
                            {
                                switch (num3)
                                {
                                    case 0:
                                        Main.tile[i, k].frameX = 0;
                                        Main.tile[i, k].frameY = 0;
                                        break;

                                    case 1:
                                        Main.tile[i, k].frameX = 0;
                                        Main.tile[i, k].frameY = 0x16;
                                        break;

                                    case 2:
                                        Main.tile[i, k].frameX = 0;
                                        Main.tile[i, k].frameY = 0x2c;
                                        break;
                                }
                            }
                            if ((num4 == 5) || (num4 == 7))
                            {
                                Main.tile[i - 1, k].active = true;
                                Main.tile[i - 1, k].type = 5;
                                num3 = Main.rand.Next(3);
                                if (Main.rand.Next(3) < 2)
                                {
                                    switch (num3)
                                    {
                                        case 0:
                                            Main.tile[i - 1, k].frameX = 0x2c;
                                            Main.tile[i - 1, k].frameY = 0xc6;
                                            break;

                                        case 1:
                                            Main.tile[i - 1, k].frameX = 0x2c;
                                            Main.tile[i - 1, k].frameY = 220;
                                            break;

                                        case 2:
                                            Main.tile[i - 1, k].frameX = 0x2c;
                                            Main.tile[i - 1, k].frameY = 0xf2;
                                            break;
                                    }
                                }
                                else
                                {
                                    switch (num3)
                                    {
                                        case 0:
                                            Main.tile[i - 1, k].frameX = 0x42;
                                            Main.tile[i - 1, k].frameY = 0;
                                            break;

                                        case 1:
                                            Main.tile[i - 1, k].frameX = 0x42;
                                            Main.tile[i - 1, k].frameY = 0x16;
                                            break;

                                        case 2:
                                            Main.tile[i - 1, k].frameX = 0x42;
                                            Main.tile[i - 1, k].frameY = 0x2c;
                                            break;
                                    }
                                }
                            }
                            if ((num4 == 6) || (num4 == 7))
                            {
                                Main.tile[i + 1, k].active = true;
                                Main.tile[i + 1, k].type = 5;
                                num3 = Main.rand.Next(3);
                                if (Main.rand.Next(3) < 2)
                                {
                                    switch (num3)
                                    {
                                        case 0:
                                            Main.tile[i + 1, k].frameX = 0x42;
                                            Main.tile[i + 1, k].frameY = 0xc6;
                                            break;

                                        case 1:
                                            Main.tile[i + 1, k].frameX = 0x42;
                                            Main.tile[i + 1, k].frameY = 220;
                                            break;

                                        case 2:
                                            Main.tile[i + 1, k].frameX = 0x42;
                                            Main.tile[i + 1, k].frameY = 0xf2;
                                            break;
                                    }
                                }
                                else
                                {
                                    switch (num3)
                                    {
                                        case 0:
                                            Main.tile[i + 1, k].frameX = 0x58;
                                            Main.tile[i + 1, k].frameY = 0x42;
                                            break;

                                        case 1:
                                            Main.tile[i + 1, k].frameX = 0x58;
                                            Main.tile[i + 1, k].frameY = 0x58;
                                            break;

                                        case 2:
                                            Main.tile[i + 1, k].frameX = 0x58;
                                            Main.tile[i + 1, k].frameY = 110;
                                            break;
                                    }
                                }
                            }
                        }
                        int num7 = Main.rand.Next(3);
                        switch (num7)
                        {
                            case 0:
                            case 1:
                                Main.tile[i + 1, j - 1].active = true;
                                Main.tile[i + 1, j - 1].type = 5;
                                num3 = Main.rand.Next(3);
                                if (num3 == 0)
                                {
                                    Main.tile[i + 1, j - 1].frameX = 0x16;
                                    Main.tile[i + 1, j - 1].frameY = 0x84;
                                }
                                if (num3 == 1)
                                {
                                    Main.tile[i + 1, j - 1].frameX = 0x16;
                                    Main.tile[i + 1, j - 1].frameY = 0x9a;
                                }
                                if (num3 == 2)
                                {
                                    Main.tile[i + 1, j - 1].frameX = 0x16;
                                    Main.tile[i + 1, j - 1].frameY = 0xb0;
                                }
                                break;
                        }
                        if ((num7 == 0) || (num7 == 2))
                        {
                            Main.tile[i - 1, j - 1].active = true;
                            Main.tile[i - 1, j - 1].type = 5;
                            num3 = Main.rand.Next(3);
                            switch (num3)
                            {
                                case 0:
                                    Main.tile[i - 1, j - 1].frameX = 0x2c;
                                    Main.tile[i - 1, j - 1].frameY = 0x84;
                                    break;

                                case 1:
                                    Main.tile[i - 1, j - 1].frameX = 0x2c;
                                    Main.tile[i - 1, j - 1].frameY = 0x9a;
                                    break;

                                case 2:
                                    Main.tile[i - 1, j - 1].frameX = 0x2c;
                                    Main.tile[i - 1, j - 1].frameY = 0xb0;
                                    break;
                            }
                        }
                        num3 = Main.rand.Next(3);
                        if (num7 == 0)
                        {
                            switch (num3)
                            {
                                case 0:
                                    Main.tile[i, j - 1].frameX = 0x58;
                                    Main.tile[i, j - 1].frameY = 0x84;
                                    break;

                                case 1:
                                    Main.tile[i, j - 1].frameX = 0x58;
                                    Main.tile[i, j - 1].frameY = 0x9a;
                                    break;

                                case 2:
                                    Main.tile[i, j - 1].frameX = 0x58;
                                    Main.tile[i, j - 1].frameY = 0xb0;
                                    break;
                            }
                        }
                        else if (num7 == 1)
                        {
                            switch (num3)
                            {
                                case 0:
                                    Main.tile[i, j - 1].frameX = 0;
                                    Main.tile[i, j - 1].frameY = 0x84;
                                    break;

                                case 1:
                                    Main.tile[i, j - 1].frameX = 0;
                                    Main.tile[i, j - 1].frameY = 0x9a;
                                    break;

                                case 2:
                                    Main.tile[i, j - 1].frameX = 0;
                                    Main.tile[i, j - 1].frameY = 0xb0;
                                    break;
                            }
                        }
                        else if (num7 == 2)
                        {
                            switch (num3)
                            {
                                case 0:
                                    Main.tile[i, j - 1].frameX = 0x42;
                                    Main.tile[i, j - 1].frameY = 0x84;
                                    break;

                                case 1:
                                    Main.tile[i, j - 1].frameX = 0x42;
                                    Main.tile[i, j - 1].frameY = 0x9a;
                                    break;

                                case 2:
                                    Main.tile[i, j - 1].frameX = 0x42;
                                    Main.tile[i, j - 1].frameY = 0xb0;
                                    break;
                            }
                        }
                        if (Main.rand.Next(3) < 2)
                        {
                            switch (Main.rand.Next(3))
                            {
                                case 0:
                                    Main.tile[i, j - num5].frameX = 0x16;
                                    Main.tile[i, j - num5].frameY = 0xc6;
                                    break;

                                case 1:
                                    Main.tile[i, j - num5].frameX = 0x16;
                                    Main.tile[i, j - num5].frameY = 220;
                                    break;

                                case 2:
                                    Main.tile[i, j - num5].frameX = 0x16;
                                    Main.tile[i, j - num5].frameY = 0xf2;
                                    break;
                            }
                        }
                        else
                        {
                            switch (Main.rand.Next(3))
                            {
                                case 0:
                                    Main.tile[i, j - num5].frameX = 0;
                                    Main.tile[i, j - num5].frameY = 0xc6;
                                    break;

                                case 1:
                                    Main.tile[i, j - num5].frameX = 0;
                                    Main.tile[i, j - num5].frameY = 220;
                                    break;

                                case 2:
                                    Main.tile[i, j - num5].frameX = 0;
                                    Main.tile[i, j - num5].frameY = 0xf2;
                                    break;
                            }
                        }
                    }
                }
            }
        }

        public static void CloseDoor(int i, int j)
        {
            int num = 0;
            int num2 = i;
            int num3 = j;
            int frameX = Main.tile[i, j].frameX;
            int frameY = Main.tile[i, j].frameY;
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
            int num6 = num2;
            if (num == -1)
            {
                num6 = num2 - 1;
            }
            for (int k = num6; k < (num6 + 2); k++)
            {
                for (int m = num3; m < (num3 + 3); m++)
                {
                    if (k == num2)
                    {
                        Main.tile[k, m].type = 10;
                        Main.tile[k, m].frameX = (short)(Main.rand.Next(3) * 0x12);
                    }
                    else
                    {
                        Main.tile[k, m].active = false;
                    }
                }
            }
            Main.soundInstanceDoorClosed.Stop();
            Main.soundInstanceDoorClosed = Main.soundDoorClosed.CreateInstance();
            Main.soundInstanceDoorClosed.Play();
        }

        public static bool EmptyTileCheck(int startX, int endX, int startY, int endY)
        {
            if (startX < 0)
            {
                return false;
            }
            if (endX >= 0x1389)
            {
                return false;
            }
            if (startY < 0)
            {
                return false;
            }
            if (endY >= 0x9c5)
            {
                return false;
            }
            for (int i = startX; i < (endX + 1); i++)
            {
                for (int j = startY; j < (endY + 1); j++)
                {
                    if (Main.tile[i, j].active)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static void EveryTileFrame()
        {
            for (int i = 0; i < 0x1389; i++)
            {
                for (int j = 0; j < 0x9c5; j++)
                {
                    TileFrame(i, j, true);
                }
            }
        }

        public static void generateWorld()
        {
            int num10;
            int num11;
            int num13;
            int num7 = 0;
            int num8 = 0;
            double num = 875.34999999999991;
            num *= Main.rand.Next(90, 110) * 0.01;
            double num4 = num + 375.15;
            num4 *= Main.rand.Next(90, 110) * 0.01;
            double num2 = num;
            double num3 = num;
            double num5 = num4;
            double num6 = num4;
            int num9 = 0;
            while (num9 < 0x1389)
            {
                if (num < num2)
                {
                    num2 = num;
                }
                if (num > num3)
                {
                    num3 = num;
                }
                if (num4 < num5)
                {
                    num5 = num4;
                }
                if (num4 > num6)
                {
                    num6 = num4;
                }
                if (num8 <= 0)
                {
                    num7 = Main.rand.Next(0, 5);
                    num8 = Main.rand.Next(5, 40);
                    if (num7 == 0)
                    {
                        num8 *= (int)(Main.rand.Next(15, 30) * 0.1);
                    }
                }
                num8--;
                switch (num7)
                {
                    case 0:
                        while (Main.rand.Next(0, 7) == 0)
                        {
                            num += Main.rand.Next(-1, 2);
                        }
                        break;

                    case 1:
                        while (Main.rand.Next(0, 4) == 0)
                        {
                            num--;
                        }
                        while (Main.rand.Next(0, 10) == 0)
                        {
                            num++;
                        }
                        break;

                    case 2:
                        while (Main.rand.Next(0, 4) == 0)
                        {
                            num++;
                        }
                        while (Main.rand.Next(0, 10) == 0)
                        {
                            num--;
                        }
                        break;

                    case 3:
                        while (Main.rand.Next(0, 2) == 0)
                        {
                            num--;
                        }
                        while (Main.rand.Next(0, 6) == 0)
                        {
                            num++;
                        }
                        break;

                    case 4:
                        while (Main.rand.Next(0, 2) == 0)
                        {
                            num++;
                        }
                        while (Main.rand.Next(0, 5) == 0)
                        {
                            num--;
                        }
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
                while (Main.rand.Next(0, 3) == 0)
                {
                    num4 += Main.rand.Next(-2, 3);
                }
                if (num4 < (num + 125.05000000000001))
                {
                    num4++;
                }
                if (num4 > (num + 625.25))
                {
                    num4--;
                }
                num10 = 0;
                while (num10 < num)
                {
                    Main.tile[num9, num10].active = false;
                    Main.tile[num9, num10].lighted = true;
                    num10++;
                }
                num10 = (int)num;
                while (num10 < 0x9c5)
                {
                    if (num10 < num4)
                    {
                        Main.tile[num9, num10].active = true;
                        Main.tile[num9, num10].type = 0;
                        Main.tile[num9, num10].frameX = -1;
                        Main.tile[num9, num10].frameY = -1;
                    }
                    else
                    {
                        Main.tile[num9, num10].active = true;
                        Main.tile[num9, num10].type = 1;
                        Main.tile[num9, num10].frameX = -1;
                        Main.tile[num9, num10].frameY = -1;
                    }
                    num10++;
                }
                num9++;
            }
            Main.worldSurface = num3;
            for (num11 = 0; num11 < 0x61b7; num11++)
            {
                TileRunner(
                    Main.rand.Next(0, 0x1389),
                    Main.rand.Next((int)num2, ((int)num6) + 1),
                    (double)Main.rand.Next(2, 7),
                    Main.rand.Next(2, 0x17),
                    1,
                    false,
                    0f,
                    0f
                );
            }
            for (num11 = 0; num11 < 0xf449; num11++)
            {
                TileRunner(
                    Main.rand.Next(0, 0x1389),
                    Main.rand.Next((int)num5, 0x9c5),
                    (double)Main.rand.Next(2, 6),
                    Main.rand.Next(2, 40),
                    0,
                    false,
                    0f,
                    0f
                );
            }
            for (num11 = 0; num11 < 0x30db; num11++)
            {
                TileRunner(
                    Main.rand.Next(0, 0x1389),
                    Main.rand.Next((int)num3, 0x9c5),
                    (double)Main.rand.Next(2, 5),
                    Main.rand.Next(2, 20),
                    -1,
                    false,
                    0f,
                    0f
                );
            }
            for (num11 = 0; num11 < 0x30db; num11++)
            {
                TileRunner(
                    Main.rand.Next(0, 0x1389),
                    Main.rand.Next((int)num3, 0x9c5),
                    (double)Main.rand.Next(8, 15),
                    Main.rand.Next(7, 30),
                    -1,
                    false,
                    0f,
                    0f
                );
            }
            for (num11 = 0; num11 < 0x9c5; num11++)
            {
                if (num6 <= 2501.0)
                {
                    TileRunner(
                        Main.rand.Next(0, 0x1389),
                        Main.rand.Next((int)num6, 0x9c5),
                        (double)Main.rand.Next(10, 20),
                        Main.rand.Next(50, 300),
                        -1,
                        false,
                        0f,
                        0f
                    );
                }
            }
            int i = 0;
            for (num11 = 0; num11 < 0x19; num11++)
            {
                i = Main.rand.Next(0, 0x1389);
                num13 = 0;
                while (num13 < num3)
                {
                    if (Main.tile[i, num13].active)
                    {
                        TileRunner(
                            i,
                            num13,
                            (double)Main.rand.Next(3, 6),
                            Main.rand.Next(5, 50),
                            -1,
                            false,
                            Main.rand.Next(-10, 11) * 0.1f,
                            1f
                        );
                        break;
                    }
                    num13++;
                }
            }
            for (num11 = 0; num11 < 40; num11++)
            {
                i = Main.rand.Next(0, 0x1389);
                num13 = 0;
                while (num13 < num3)
                {
                    if (Main.tile[i, num13].active)
                    {
                        TileRunner(
                            i,
                            num13,
                            (double)Main.rand.Next(10, 15),
                            Main.rand.Next(50, 130),
                            -1,
                            false,
                            Main.rand.Next(-10, 11) * 0.1f,
                            1f
                        );
                        break;
                    }
                    num13++;
                }
            }
            for (num11 = 0; num11 < 10; num11++)
            {
                i = Main.rand.Next(0, 0x1389);
                num13 = 0;
                while (num13 < num3)
                {
                    if (Main.tile[i, num13].active)
                    {
                        TileRunner(
                            i,
                            num13,
                            (double)Main.rand.Next(12, 0x19),
                            Main.rand.Next(100, 400),
                            -1,
                            false,
                            Main.rand.Next(-10, 11) * 0.1f,
                            3f
                        );
                        TileRunner(
                            i,
                            num13,
                            (double)Main.rand.Next(8, 0x11),
                            Main.rand.Next(60, 200),
                            -1,
                            false,
                            Main.rand.Next(-10, 11) * 0.1f,
                            2f
                        );
                        TileRunner(
                            i,
                            num13,
                            (double)Main.rand.Next(5, 13),
                            Main.rand.Next(40, 170),
                            -1,
                            false,
                            Main.rand.Next(-10, 11) * 0.1f,
                            2f
                        );
                        break;
                    }
                    num13++;
                }
            }
            for (num11 = 0; num11 < 0x30db; num11++)
            {
                int num14 = Main.rand.Next(1, 0x1388);
                int num15 = Main.rand.Next((int)num2, (int)num3);
                if (num15 >= 0x9c5)
                {
                    num15 = 0x9c3;
                }
                if (
                    (
                        (
                            (
                                Main.tile[num14 - 1, num15].active
                                && (Main.tile[num14 - 1, num15].type == 0)
                            )
                            && (
                                Main.tile[num14 + 1, num15].active
                                && (Main.tile[num14 + 1, num15].type == 0)
                            )
                        )
                        && (
                            (
                                Main.tile[num14, num15 - 1].active
                                && (Main.tile[num14, num15 - 1].type == 0)
                            ) && Main.tile[num14, num15 + 1].active
                        )
                    ) && (Main.tile[num14, num15 + 1].type == 0)
                )
                {
                    Main.tile[num14, num15].active = true;
                    Main.tile[num14, num15].type = 2;
                }
            }
            for (num11 = 0; num11 < 0x271; num11++)
            {
                TileRunner(
                    Main.rand.Next(0, 0x1389),
                    Main.rand.Next((int)num2, (int)num3),
                    (double)Main.rand.Next(3, 4),
                    Main.rand.Next(2, 5),
                    7,
                    false,
                    0f,
                    0f
                );
            }
            for (num11 = 0; num11 < 750; num11++)
            {
                TileRunner(
                    Main.rand.Next(0, 0x1389),
                    Main.rand.Next((int)num3, (int)num6),
                    (double)Main.rand.Next(3, 6),
                    Main.rand.Next(3, 6),
                    7,
                    false,
                    0f,
                    0f
                );
            }
            for (num11 = 0; num11 < 0xea8; num11++)
            {
                TileRunner(
                    Main.rand.Next(0, 0x1389),
                    Main.rand.Next((int)num5, 0x9c5),
                    (double)Main.rand.Next(4, 9),
                    Main.rand.Next(4, 8),
                    7,
                    false,
                    0f,
                    0f
                );
            }
            for (num11 = 0; num11 < 500; num11++)
            {
                TileRunner(
                    Main.rand.Next(0, 0x1389),
                    Main.rand.Next((int)num2, (int)num3),
                    (double)Main.rand.Next(3, 4),
                    Main.rand.Next(2, 5),
                    6,
                    false,
                    0f,
                    0f
                );
            }
            for (num11 = 0; num11 < 0x271; num11++)
            {
                TileRunner(
                    Main.rand.Next(0, 0x1389),
                    Main.rand.Next((int)num3, (int)num6),
                    (double)Main.rand.Next(3, 6),
                    Main.rand.Next(3, 6),
                    6,
                    false,
                    0f,
                    0f
                );
            }
            for (num11 = 0; num11 < 0x9c5; num11++)
            {
                TileRunner(
                    Main.rand.Next(0, 0x1389),
                    Main.rand.Next((int)num5, 0x9c5),
                    (double)Main.rand.Next(4, 9),
                    Main.rand.Next(4, 8),
                    6,
                    false,
                    0f,
                    0f
                );
            }
            for (num11 = 0; num11 < 0x7d; num11++)
            {
                TileRunner(
                    Main.rand.Next(0, 0x1389),
                    Main.rand.Next((int)num2, (int)num3),
                    (double)Main.rand.Next(3, 4),
                    Main.rand.Next(2, 5),
                    9,
                    false,
                    0f,
                    0f
                );
            }
            for (num11 = 0; num11 < 250; num11++)
            {
                TileRunner(
                    Main.rand.Next(0, 0x1389),
                    Main.rand.Next((int)num3, (int)num6),
                    (double)Main.rand.Next(3, 6),
                    Main.rand.Next(3, 6),
                    9,
                    false,
                    0f,
                    0f
                );
            }
            for (num11 = 0; num11 < 0x4e2; num11++)
            {
                TileRunner(
                    Main.rand.Next(0, 0x1389),
                    Main.rand.Next((int)num5, 0x9c5),
                    (double)Main.rand.Next(4, 9),
                    Main.rand.Next(4, 8),
                    9,
                    false,
                    0f,
                    0f
                );
            }
            for (num11 = 0; num11 < 0x3e; num11++)
            {
                TileRunner(
                    Main.rand.Next(0, 0x1389),
                    Main.rand.Next((int)num2, (int)num3),
                    (double)Main.rand.Next(3, 4),
                    Main.rand.Next(2, 5),
                    8,
                    false,
                    0f,
                    0f
                );
            }
            for (num11 = 0; num11 < 0x7d; num11++)
            {
                TileRunner(
                    Main.rand.Next(0, 0x1389),
                    Main.rand.Next((int)num3, (int)num6),
                    (double)Main.rand.Next(3, 6),
                    Main.rand.Next(3, 6),
                    8,
                    false,
                    0f,
                    0f
                );
            }
            for (num11 = 0; num11 < 0x271; num11++)
            {
                TileRunner(
                    Main.rand.Next(0, 0x1389),
                    Main.rand.Next((int)num5, 0x9c5),
                    (double)Main.rand.Next(4, 9),
                    Main.rand.Next(4, 8),
                    8,
                    false,
                    0f,
                    0f
                );
            }
            for (num11 = 0; num11 < 0x1389; num11++)
            {
                i = num11;
                for (num13 = 0; num13 < (Main.worldSurface - 1.0); num13++)
                {
                    if (Main.tile[i, num13].active)
                    {
                        if (Main.tile[i, num13].type == 0)
                        {
                            SpreadGrass(i, num13, 0, 2, true);
                        }
                        break;
                    }
                }
            }
            for (num10 = 0; num10 < 0x9c5; num10++)
            {
                num9 = 0x9c4;
                if (Main.tile[num9, num10].active)
                {
                    Main.spawnTileX = num9;
                    Main.spawnTileY = num10;
                    Main.player[0].Spawn();
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
                (
                    ((((i >= 0) && (j >= 0)) && (i < 0x1389)) && (j < 0x9c5))
                    && Main.tile[i, j].active
                )
                && (
                    (
                        (
                            ((j < 1) || !Main.tile[i, j - 1].active)
                            || (Main.tile[i, j - 1].type != 5)
                        ) || (Main.tile[i, j].type == 5)
                    )
                    || (
                        (
                            (
                                (
                                    (Main.tile[i, j - 1].frameX == 0x42)
                                    && (Main.tile[i, j - 1].frameY >= 0)
                                ) && (Main.tile[i, j - 1].frameY <= 0x2c)
                            )
                            || (
                                (
                                    (Main.tile[i, j - 1].frameX == 0x58)
                                    && (Main.tile[i, j - 1].frameY >= 0x42)
                                ) && (Main.tile[i, j - 1].frameY <= 110)
                            )
                        ) || (Main.tile[i, j - 1].frameY >= 0xc6)
                    )
                )
            )
            {
                if (Main.tile[i, j].type == 3)
                {
                    Main.soundInstanceGrass.Stop();
                    Main.soundInstanceGrass = Main.soundGrass.CreateInstance();
                    Main.soundInstanceGrass.Play();
                    if (Main.tile[i, j].frameX == 0x90)
                    {
                        Item.NewItem(i * 0x10, j * 0x10, 0x10, 0x10, 5);
                    }
                }
                else
                {
                    int index = Main.rand.Next(3);
                    Main.soundInstanceDig[index].Stop();
                    Main.soundInstanceDig[index] = Main.soundDig[index].CreateInstance();
                    Main.soundInstanceDig[index].Play();
                }
                int num2 = 10;
                if (fail)
                {
                    num2 = 3;
                }
                for (int k = 0; k < num2; k++)
                {
                    int type = 0;
                    if (Main.tile[i, j].type == 0)
                    {
                        type = 0;
                    }
                    if (Main.tile[i, j].type == 1)
                    {
                        type = 1;
                    }
                    if (Main.tile[i, j].type == 4)
                    {
                        type = 6;
                    }
                    if (
                        ((Main.tile[i, j].type == 5) || (Main.tile[i, j].type == 10))
                        || (Main.tile[i, j].type == 11)
                    )
                    {
                        type = 7;
                    }
                    if (Main.tile[i, j].type == 2)
                    {
                        if (Main.rand.Next(2) == 0)
                        {
                            type = 0;
                        }
                        else
                        {
                            type = 2;
                        }
                    }
                    if (Main.tile[i, j].type == 6)
                    {
                        type = 8;
                    }
                    if (Main.tile[i, j].type == 7)
                    {
                        type = 9;
                    }
                    if (Main.tile[i, j].type == 8)
                    {
                        type = 10;
                    }
                    if (Main.tile[i, j].type == 9)
                    {
                        type = 11;
                    }
                    if (Main.tile[i, j].type == 3)
                    {
                        type = 3;
                    }
                    Color newColor = new Color();
                    Dust.NewDust(
                        new Vector2((float)(i * 0x10), (float)(j * 0x10)),
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
                    if (Main.tile[i, j].type == 2)
                    {
                        Main.tile[i, j].type = 0;
                    }
                    SquareTileFrame(i, j, true);
                }
                else
                {
                    int num5 = 0;
                    if ((Main.tile[i, j].type == 0) || (Main.tile[i, j].type == 2))
                    {
                        num5 = 2;
                    }
                    else if (Main.tile[i, j].type == 1)
                    {
                        num5 = 3;
                    }
                    else if (Main.tile[i, j].type == 4)
                    {
                        num5 = 8;
                    }
                    else if (Main.tile[i, j].type == 5)
                    {
                        num5 = 9;
                    }
                    else if (Main.tile[i, j].type == 6)
                    {
                        num5 = 11;
                    }
                    else if (Main.tile[i, j].type == 7)
                    {
                        num5 = 12;
                    }
                    else if (Main.tile[i, j].type == 8)
                    {
                        num5 = 13;
                    }
                    else if (Main.tile[i, j].type == 9)
                    {
                        num5 = 14;
                    }
                    if (num5 > 0)
                    {
                        Item.NewItem(i * 0x10, j * 0x10, 0x10, 0x10, num5);
                    }
                    Main.tile[i, j].active = false;
                    Main.tile[i, j].lighted = false;
                    Main.tile[i, j].frameX = -1;
                    Main.tile[i, j].frameY = -1;
                    Main.tile[i, j].frameNumber = 0;
                    Main.tile[i, j].type = 0;
                    SquareTileFrame(i, j, true);
                }
            }
        }

        public static void KillWall(int i, int j, bool fail = false)
        {
            if (
                ((((i >= 0) && (j >= 0)) && (i < 0x1389)) && (j < 0x9c5))
                && (Main.tile[i, j].wall > 0)
            )
            {
                int index = Main.rand.Next(3);
                Main.soundInstanceDig[index].Stop();
                Main.soundInstanceDig[index] = Main.soundDig[index].CreateInstance();
                Main.soundInstanceDig[index].Play();
                int num2 = 10;
                if (fail)
                {
                    num2 = 3;
                }
                for (int k = 0; k < num2; k++)
                {
                    int type = 0;
                    if (Main.tile[i, j].wall == 1)
                    {
                        type = 1;
                    }
                    Color newColor = new Color();
                    Dust.NewDust(
                        new Vector2((float)(i * 0x10), (float)(j * 0x10)),
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
                    SquareWallFrame(i, j, true);
                }
                else
                {
                    int num5 = 0;
                    if (Main.tile[i, j].wall == 1)
                    {
                        num5 = 0x1a;
                    }
                    if (num5 > 0)
                    {
                        Item.NewItem(i * 0x10, j * 0x10, 0x10, 0x10, num5);
                    }
                    Main.tile[i, j].wall = 0;
                    SquareWallFrame(i, j, true);
                }
            }
        }

        public static void OpenDoor(int i, int j, int direction)
        {
            int num3;
            int num = 0;
            if (
                (Main.tile[i, j - 1].frameY == 0)
                && (Main.tile[i, j - 1].type == Main.tile[i, j].type)
            )
            {
                num = j - 1;
            }
            else if (
                (Main.tile[i, j - 2].frameY == 0)
                && (Main.tile[i, j - 2].type == Main.tile[i, j].type)
            )
            {
                num = j - 2;
            }
            else if (
                (Main.tile[i, j + 1].frameY == 0)
                && (Main.tile[i, j + 1].type == Main.tile[i, j].type)
            )
            {
                num = j + 1;
            }
            else
            {
                num = j;
            }
            int num2 = i;
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
            bool flag = true;
            for (int k = num; k < (num + 3); k++)
            {
                if (Main.tile[num3, k].active)
                {
                    if (Main.tile[num3, k].type == 3)
                    {
                        KillTile(num3, k, false);
                    }
                    else
                    {
                        flag = false;
                        break;
                    }
                }
            }
            if (flag)
            {
                Main.soundInstanceDoorOpen.Stop();
                Main.soundInstanceDoorOpen = Main.soundDoorOpen.CreateInstance();
                Main.soundInstanceDoorOpen.Play();
                Main.tile[num2, num].active = true;
                Main.tile[num2, num].type = 11;
                Main.tile[num2, num].frameY = 0;
                Main.tile[num2, num].frameX = num4;
                Main.tile[num2 + 1, num].active = true;
                Main.tile[num2 + 1, num].type = 11;
                Main.tile[num2 + 1, num].frameY = 0;
                Main.tile[num2 + 1, num].frameX = (short)(num4 + 0x12);
                Main.tile[num2, num + 1].active = true;
                Main.tile[num2, num + 1].type = 11;
                Main.tile[num2, num + 1].frameY = 0x12;
                Main.tile[num2, num + 1].frameX = num4;
                Main.tile[num2 + 1, num + 1].active = true;
                Main.tile[num2 + 1, num + 1].type = 11;
                Main.tile[num2 + 1, num + 1].frameY = 0x12;
                Main.tile[num2 + 1, num + 1].frameX = (short)(num4 + 0x12);
                Main.tile[num2, num + 2].active = true;
                Main.tile[num2, num + 2].type = 11;
                Main.tile[num2, num + 2].frameY = 0x24;
                Main.tile[num2, num + 2].frameX = num4;
                Main.tile[num2 + 1, num + 2].active = true;
                Main.tile[num2 + 1, num + 2].type = 11;
                Main.tile[num2 + 1, num + 2].frameY = 0x24;
                Main.tile[num2 + 1, num + 2].frameX = (short)(num4 + 0x12);
            }
        }

        public static bool PlaceDoor(int i, int j, int type)
        {
            try
            {
                if (
                    (
                        (Main.tile[i, j - 2].active && Main.tileSolid[Main.tile[i, j - 2].type])
                        && Main.tile[i, j + 2].active
                    ) && Main.tileSolid[Main.tile[i, j + 2].type]
                )
                {
                    Main.tile[i, j - 1].active = true;
                    Main.tile[i, j - 1].type = 10;
                    Main.tile[i, j - 1].frameY = 0;
                    Main.tile[i, j - 1].frameX = (short)(Main.rand.Next(3) * 0x12);
                    Main.tile[i, j].active = true;
                    Main.tile[i, j].type = 10;
                    Main.tile[i, j].frameY = 0x12;
                    Main.tile[i, j].frameX = (short)(Main.rand.Next(3) * 0x12);
                    Main.tile[i, j + 1].active = true;
                    Main.tile[i, j + 1].type = 10;
                    Main.tile[i, j + 1].frameY = 0x24;
                    Main.tile[i, j + 1].frameX = (short)(Main.rand.Next(3) * 0x12);
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public static void PlaceTile(int i, int j, int type, bool mute = false)
        {
            if (
                ((((i >= 0) && (j >= 0)) && (i < 0x1389)) && (j < 0x9c5))
                && Collision.EmptyTile(i, j)
            )
            {
                Main.tile[i, j].frameY = 0;
                Main.tile[i, j].frameX = 0;
                if (type == 3)
                {
                    if (
                        (((j + 1) < 0x9c5) && (Main.tile[i, j + 1].type == 2))
                        && Main.tile[i, j + 1].active
                    )
                    {
                        if (Main.rand.Next(50) == 0)
                        {
                            Main.tile[i, j].active = true;
                            Main.tile[i, j].type = 3;
                            Main.tile[i, j].frameX = 0x90;
                        }
                        else if (Main.rand.Next(0x23) == 0)
                        {
                            Main.tile[i, j].active = true;
                            Main.tile[i, j].type = 3;
                            Main.tile[i, j].frameX = (short)((Main.rand.Next(2) * 0x12) + 0x6c);
                        }
                        else
                        {
                            Main.tile[i, j].active = true;
                            Main.tile[i, j].type = 3;
                            Main.tile[i, j].frameX = (short)(Main.rand.Next(6) * 0x12);
                        }
                    }
                }
                else if (type == 4)
                {
                    if (
                        (
                            (
                                Main.tile[i - 1, j].active
                                && (
                                    Main.tileSolid[Main.tile[i - 1, j].type]
                                    || (
                                        (
                                            (Main.tile[i - 1, j].type == 5)
                                            && (Main.tile[i - 1, j - 1].type == 5)
                                        ) && (Main.tile[i - 1, j + 1].type == 5)
                                    )
                                )
                            )
                            || (
                                Main.tile[i + 1, j].active
                                && (
                                    Main.tileSolid[Main.tile[i + 1, j].type]
                                    || (
                                        (
                                            (Main.tile[i + 1, j].type == 5)
                                            && (Main.tile[i + 1, j - 1].type == 5)
                                        ) && (Main.tile[i + 1, j + 1].type == 5)
                                    )
                                )
                            )
                        )
                        || (Main.tile[i, j + 1].active && Main.tileSolid[Main.tile[i, j + 1].type])
                    )
                    {
                        Main.tile[i, j].active = true;
                        Main.tile[i, j].type = (byte)type;
                        SquareTileFrame(i, j, true);
                    }
                }
                else if (type == 10)
                {
                    if (
                        (
                            (Main.tile[i, j - 1].active || Main.tile[i, j - 2].active)
                            || !Main.tile[i, j - 3].active
                        ) || !Main.tileSolid[Main.tile[i, j - 3].type]
                    )
                    {
                        if (
                            (
                                (Main.tile[i, j + 1].active || Main.tile[i, j + 2].active)
                                || !Main.tile[i, j + 3].active
                            ) || !Main.tileSolid[Main.tile[i, j + 3].type]
                        )
                        {
                            return;
                        }
                        PlaceDoor(i, j + 1, type);
                        SquareTileFrame(i, j, true);
                    }
                    else
                    {
                        PlaceDoor(i, j - 1, type);
                        SquareTileFrame(i, j, true);
                    }
                }
                else
                {
                    Main.tile[i, j].active = true;
                    Main.tile[i, j].type = (byte)type;
                    SquareTileFrame(i, j, true);
                }
                if (!(!Main.tile[i, j].active || mute))
                {
                    int index = Main.rand.Next(3);
                    Main.soundInstanceDig[index].Stop();
                    Main.soundInstanceDig[index] = Main.soundDig[index].CreateInstance();
                    Main.soundInstanceDig[index].Play();
                }
            }
        }

        public static void PlaceWall(int i, int j, int type, bool mute = false)
        {
            if (Main.tile[i, j].wall != type)
            {
                Main.tile[i, j].wall = (byte)type;
                SquareWallFrame(i, j, true);
                if (!mute)
                {
                    int index = Main.rand.Next(3);
                    Main.soundInstanceDig[index].Stop();
                    Main.soundInstanceDig[index] = Main.soundDig[index].CreateInstance();
                    Main.soundInstanceDig[index].Play();
                }
            }
        }

        public static void PlantCheck(int i, int j)
        {
            int num = -1;
            int num2 = -1;
            int num3 = -1;
            int num4 = -1;
            int num5 = -1;
            int num6 = -1;
            int num7 = -1;
            int num8 = -1;
            int type = Main.tile[i, j].type;
            if ((i - 1) < 0)
            {
                num = type;
                num4 = type;
                num6 = type;
            }
            if ((i + 1) >= 0x1389)
            {
                num3 = type;
                num5 = type;
                num8 = type;
            }
            if ((j - 1) < 0)
            {
                num = type;
                num2 = type;
                num3 = type;
            }
            if ((j + 1) >= 0x9c5)
            {
                num6 = type;
                num7 = type;
                num8 = type;
            }
            if (((i - 1) >= 0) && Main.tile[i - 1, j].active)
            {
                num4 = Main.tile[i - 1, j].type;
            }
            if (((i + 1) < 0x1389) && Main.tile[i + 1, j].active)
            {
                num5 = Main.tile[i + 1, j].type;
            }
            if (((j - 1) >= 0) && Main.tile[i, j - 1].active)
            {
                num2 = Main.tile[i, j - 1].type;
            }
            if (((j + 1) < 0x9c5) && Main.tile[i, j + 1].active)
            {
                num7 = Main.tile[i, j + 1].type;
            }
            if ((((i - 1) >= 0) && ((j - 1) >= 0)) && Main.tile[i - 1, j - 1].active)
            {
                num = Main.tile[i - 1, j - 1].type;
            }
            if ((((i + 1) < 0x1389) && ((j - 1) >= 0)) && Main.tile[i + 1, j - 1].active)
            {
                num3 = Main.tile[i + 1, j - 1].type;
            }
            if ((((i - 1) >= 0) && ((j + 1) < 0x9c5)) && Main.tile[i - 1, j + 1].active)
            {
                num6 = Main.tile[i - 1, j + 1].type;
            }
            if ((((i + 1) < 0x1389) && ((j + 1) < 0x9c5)) && Main.tile[i + 1, j + 1].active)
            {
                num8 = Main.tile[i + 1, j + 1].type;
            }
            if (num7 != 2)
            {
                KillTile(i, j, false);
            }
        }

        public static void SpreadGrass(
            int i,
            int j,
            int dirt = 0,
            int grass = 2,
            bool repeat = true
        )
        {
            if (
                ((Main.tile[i, j].type == dirt) && Main.tile[i, j].active)
                && (j < Main.worldSurface)
            )
            {
                int num5;
                int num6;
                int num = i - 1;
                int num2 = i + 2;
                int num3 = j - 1;
                int num4 = j + 2;
                if (num < 0)
                {
                    num = 0;
                }
                if (num2 > 0x1389)
                {
                    num2 = 0x1389;
                }
                if (num3 < 0)
                {
                    num3 = 0;
                }
                if (num4 > 0x9c5)
                {
                    num4 = 0x9c5;
                }
                bool flag = true;
                for (num5 = num; num5 < num2; num5++)
                {
                    num6 = num3;
                    while (num6 < num4)
                    {
                        if (!Main.tile[num5, num6].active)
                        {
                            flag = false;
                            goto Label_00E4;
                        }
                        num6++;
                    }
                    Label_00E4:
                    ;
                }
                if (!flag)
                {
                    Main.tile[i, j].type = (byte)grass;
                    for (num5 = num; num5 < num2; num5++)
                    {
                        for (num6 = num3; num6 < num4; num6++)
                        {
                            if (
                                (
                                    Main.tile[num5, num6].active
                                    && (Main.tile[num5, num6].type == dirt)
                                ) && repeat
                            )
                            {
                                SpreadGrass(num5, num6, dirt, grass, true);
                            }
                        }
                    }
                }
            }
        }

        public static void SquareTileFrame(int i, int j, bool resetFrame = true)
        {
            TileFrame(i - 1, j - 1, false);
            TileFrame(i - 1, j, false);
            TileFrame(i - 1, j + 1, false);
            TileFrame(i, j - 1, false);
            TileFrame(i, j, resetFrame);
            TileFrame(i, j + 1, false);
            TileFrame(i + 1, j - 1, false);
            TileFrame(i + 1, j, false);
            TileFrame(i + 1, j + 1, false);
        }

        public static void SquareWallFrame(int i, int j, bool resetFrame = true)
        {
            WallFrame(i - 1, j - 1, false);
            WallFrame(i - 1, j, false);
            WallFrame(i - 1, j + 1, false);
            WallFrame(i, j - 1, false);
            WallFrame(i, j, resetFrame);
            WallFrame(i, j + 1, false);
            WallFrame(i + 1, j - 1, false);
            WallFrame(i + 1, j, false);
            WallFrame(i + 1, j + 1, false);
        }

        public static void TileFrame(int i, int j, bool resetFrame = false)
        {
            if ((((i >= 0) && (j >= 0)) && ((i < 0x1389) && (j < 0x9c5))) && Main.tile[i, j].active)
            {
                Rectangle rectangle;
                int num = -1;
                int num2 = -1;
                int num3 = -1;
                int index = -1;
                int num5 = -1;
                int num6 = -1;
                int num7 = -1;
                int num8 = -1;
                int type = Main.tile[i, j].type;
                int frameX = Main.tile[i, j].frameX;
                int frameY = Main.tile[i, j].frameY;
                rectangle.X = -1;
                rectangle.Y = -1;
                if (type == 3)
                {
                    PlantCheck(i, j);
                }
                else
                {
                    int num12;
                    int num13;
                    bool flag;
                    int num21;
                    WorldGen.mergeUp = false;
                    WorldGen.mergeDown = false;
                    WorldGen.mergeLeft = false;
                    WorldGen.mergeRight = false;
                    if ((i - 1) < 0)
                    {
                        num = type;
                        index = type;
                        num6 = type;
                    }
                    if ((i + 1) >= 0x1389)
                    {
                        num3 = type;
                        num5 = type;
                        num8 = type;
                    }
                    if ((j - 1) < 0)
                    {
                        num = type;
                        num2 = type;
                        num3 = type;
                    }
                    if ((j + 1) >= 0x9c5)
                    {
                        num6 = type;
                        num7 = type;
                        num8 = type;
                    }
                    if (((i - 1) >= 0) && Main.tile[i - 1, j].active)
                    {
                        index = Main.tile[i - 1, j].type;
                    }
                    if (((i + 1) < 0x1389) && Main.tile[i + 1, j].active)
                    {
                        num5 = Main.tile[i + 1, j].type;
                    }
                    if (((j - 1) >= 0) && Main.tile[i, j - 1].active)
                    {
                        num2 = Main.tile[i, j - 1].type;
                    }
                    if (((j + 1) < 0x9c5) && Main.tile[i, j + 1].active)
                    {
                        num7 = Main.tile[i, j + 1].type;
                    }
                    if ((((i - 1) >= 0) && ((j - 1) >= 0)) && Main.tile[i - 1, j - 1].active)
                    {
                        num = Main.tile[i - 1, j - 1].type;
                    }
                    if ((((i + 1) < 0x1389) && ((j - 1) >= 0)) && Main.tile[i + 1, j - 1].active)
                    {
                        num3 = Main.tile[i + 1, j - 1].type;
                    }
                    if ((((i - 1) >= 0) && ((j + 1) < 0x9c5)) && Main.tile[i - 1, j + 1].active)
                    {
                        num6 = Main.tile[i - 1, j + 1].type;
                    }
                    if ((((i + 1) < 0x1389) && ((j + 1) < 0x9c5)) && Main.tile[i + 1, j + 1].active)
                    {
                        num8 = Main.tile[i + 1, j + 1].type;
                    }
                    switch (type)
                    {
                        case 4:
                            if ((num7 >= 0) && Main.tileSolid[num7])
                            {
                                Main.tile[i, j].frameX = 0;
                            }
                            else if (
                                ((index >= 0) && Main.tileSolid[index])
                                || (((index == 5) && (num == 5)) && (num6 == 5))
                            )
                            {
                                Main.tile[i, j].frameX = 0x16;
                            }
                            else if (
                                ((num5 >= 0) && Main.tileSolid[num5])
                                || (((num5 == 5) && (num3 == 5)) && (num8 == 5))
                            )
                            {
                                Main.tile[i, j].frameX = 0x2c;
                            }
                            else
                            {
                                KillTile(i, j, false);
                            }
                            return;

                        case 10:
                            if (!destroyObject)
                            {
                                num12 = Main.tile[i, j].frameY;
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
                                        Main.tile[i, num13 - 1].active
                                        && Main.tileSolid[Main.tile[i, num13 - 1].type]
                                    )
                                )
                                {
                                    flag = true;
                                }
                                if (
                                    !(
                                        Main.tile[i, num13 + 3].active
                                        && Main.tileSolid[Main.tile[i, num13 + 3].type]
                                    )
                                )
                                {
                                    flag = true;
                                }
                                if (
                                    !(
                                        Main.tile[i, num13].active
                                        && (Main.tile[i, num13].type == type)
                                    )
                                )
                                {
                                    flag = true;
                                }
                                if (
                                    !(
                                        Main.tile[i, num13 + 1].active
                                        && (Main.tile[i, num13 + 1].type == type)
                                    )
                                )
                                {
                                    flag = true;
                                }
                                if (
                                    !(
                                        Main.tile[i, num13 + 2].active
                                        && (Main.tile[i, num13 + 2].type == type)
                                    )
                                )
                                {
                                    flag = true;
                                }
                                if (flag)
                                {
                                    destroyObject = true;
                                    KillTile(i, num13, false);
                                    KillTile(i, num13 + 1, false);
                                    KillTile(i, num13 + 2, false);
                                    Item.NewItem(i * 0x10, j * 0x10, 0x10, 0x10, 0x19);
                                }
                                destroyObject = false;
                            }
                            return;

                        case 11:
                            if (!destroyObject)
                            {
                                int num14 = 0;
                                int num15 = i;
                                num13 = j;
                                int num16 = Main.tile[i, j].frameX;
                                num12 = Main.tile[i, j].frameY;
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
                                        (
                                            (
                                                Main.tile[num15, num13 - 1].active
                                                && Main.tileSolid[Main.tile[num15, num13 - 1].type]
                                            ) && Main.tile[num15, num13 + 3].active
                                        ) && Main.tileSolid[Main.tile[num15, num13 + 3].type]
                                    )
                                )
                                {
                                    flag = true;
                                    destroyObject = true;
                                    Item.NewItem(i * 0x10, j * 0x10, 0x10, 0x10, 0x19);
                                }
                                int num17 = num15;
                                if (num14 == -1)
                                {
                                    num17 = num15 - 1;
                                }
                                for (int k = num17; k < (num17 + 2); k++)
                                {
                                    for (int m = num13; m < (num13 + 3); m++)
                                    {
                                        if (
                                            !flag
                                            && !(
                                                (Main.tile[k, m].type == 11)
                                                && Main.tile[k, m].active
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
                                        {
                                            KillTile(k, m, false);
                                        }
                                    }
                                }
                                destroyObject = false;
                            }
                            return;
                    }
                    if (type == 5)
                    {
                        if ((num7 != type) && (num7 != -1))
                        {
                            if (
                                (
                                    (
                                        (
                                            (Main.tile[i, j].frameX >= 0x16)
                                            && (Main.tile[i, j].frameX <= 0x2c)
                                        ) && (Main.tile[i, j].frameY >= 0x84)
                                    ) && (Main.tile[i, j].frameY <= 0xb0)
                                ) && ((index != type) && (num5 != type))
                            )
                            {
                                KillTile(i, j, false);
                            }
                        }
                        else if (
                            (
                                (
                                    (
                                        (
                                            (Main.tile[i, j].frameX == 0x58)
                                            && (Main.tile[i, j].frameY >= 0)
                                        ) && (Main.tile[i, j].frameY <= 0x2c)
                                    )
                                    || (
                                        (
                                            (Main.tile[i, j].frameX == 0x42)
                                            && (Main.tile[i, j].frameY >= 0x42)
                                        ) && (Main.tile[i, j].frameY <= 130)
                                    )
                                )
                                || (
                                    (
                                        (Main.tile[i, j].frameX == 110)
                                        && (Main.tile[i, j].frameY >= 0x42)
                                    ) && (Main.tile[i, j].frameY <= 110)
                                )
                            )
                            || (
                                ((Main.tile[i, j].frameX == 0x84) && (Main.tile[i, j].frameY >= 0))
                                && (Main.tile[i, j].frameY <= 0xb0)
                            )
                        )
                        {
                            if ((index == type) && (num5 == type))
                            {
                                if (Main.tile[i, j].frameNumber == 0)
                                {
                                    Main.tile[i, j].frameX = 110;
                                    Main.tile[i, j].frameY = 0x42;
                                }
                                if (Main.tile[i, j].frameNumber == 1)
                                {
                                    Main.tile[i, j].frameX = 110;
                                    Main.tile[i, j].frameY = 0x58;
                                }
                                if (Main.tile[i, j].frameNumber == 2)
                                {
                                    Main.tile[i, j].frameX = 110;
                                    Main.tile[i, j].frameY = 110;
                                }
                            }
                            else if (index == type)
                            {
                                if (Main.tile[i, j].frameNumber == 0)
                                {
                                    Main.tile[i, j].frameX = 0x58;
                                    Main.tile[i, j].frameY = 0;
                                }
                                if (Main.tile[i, j].frameNumber == 1)
                                {
                                    Main.tile[i, j].frameX = 0x58;
                                    Main.tile[i, j].frameY = 0x16;
                                }
                                if (Main.tile[i, j].frameNumber == 2)
                                {
                                    Main.tile[i, j].frameX = 0x58;
                                    Main.tile[i, j].frameY = 0x2c;
                                }
                            }
                            else if (num5 == type)
                            {
                                if (Main.tile[i, j].frameNumber == 0)
                                {
                                    Main.tile[i, j].frameX = 0x42;
                                    Main.tile[i, j].frameY = 0x42;
                                }
                                if (Main.tile[i, j].frameNumber == 1)
                                {
                                    Main.tile[i, j].frameX = 0x42;
                                    Main.tile[i, j].frameY = 0x58;
                                }
                                if (Main.tile[i, j].frameNumber == 2)
                                {
                                    Main.tile[i, j].frameX = 0x42;
                                    Main.tile[i, j].frameY = 110;
                                }
                            }
                            else
                            {
                                if (Main.tile[i, j].frameNumber == 0)
                                {
                                    Main.tile[i, j].frameX = 0;
                                    Main.tile[i, j].frameY = 0x16;
                                }
                                if (Main.tile[i, j].frameNumber == 1)
                                {
                                    Main.tile[i, j].frameX = 0;
                                    Main.tile[i, j].frameY = 0x16;
                                }
                                if (Main.tile[i, j].frameNumber == 2)
                                {
                                    Main.tile[i, j].frameX = 0;
                                    Main.tile[i, j].frameY = 0x16;
                                }
                            }
                        }
                        if (
                            ((Main.tile[i, j].frameY >= 0x84) && (Main.tile[i, j].frameY <= 0xb0))
                            && (
                                ((Main.tile[i, j].frameX == 0) || (Main.tile[i, j].frameX == 0x42))
                                || (Main.tile[i, j].frameX == 0x58)
                            )
                        )
                        {
                            if ((index != type) && (num5 != type))
                            {
                                if (Main.tile[i, j].frameNumber == 0)
                                {
                                    Main.tile[i, j].frameX = 0;
                                    Main.tile[i, j].frameY = 0;
                                }
                                if (Main.tile[i, j].frameNumber == 1)
                                {
                                    Main.tile[i, j].frameX = 0;
                                    Main.tile[i, j].frameY = 0x16;
                                }
                                if (Main.tile[i, j].frameNumber == 2)
                                {
                                    Main.tile[i, j].frameX = 0;
                                    Main.tile[i, j].frameY = 0x2c;
                                }
                            }
                            else if (index != type)
                            {
                                if (Main.tile[i, j].frameNumber == 0)
                                {
                                    Main.tile[i, j].frameX = 0;
                                    Main.tile[i, j].frameY = 0x84;
                                }
                                if (Main.tile[i, j].frameNumber == 1)
                                {
                                    Main.tile[i, j].frameX = 0;
                                    Main.tile[i, j].frameY = 0x9a;
                                }
                                if (Main.tile[i, j].frameNumber == 2)
                                {
                                    Main.tile[i, j].frameX = 0;
                                    Main.tile[i, j].frameY = 0xb0;
                                }
                            }
                            else if (num5 != type)
                            {
                                if (Main.tile[i, j].frameNumber == 0)
                                {
                                    Main.tile[i, j].frameX = 0x42;
                                    Main.tile[i, j].frameY = 0x84;
                                }
                                if (Main.tile[i, j].frameNumber == 1)
                                {
                                    Main.tile[i, j].frameX = 0x42;
                                    Main.tile[i, j].frameY = 0x9a;
                                }
                                if (Main.tile[i, j].frameNumber == 2)
                                {
                                    Main.tile[i, j].frameX = 0x42;
                                    Main.tile[i, j].frameY = 0xb0;
                                }
                            }
                            else
                            {
                                if (Main.tile[i, j].frameNumber == 0)
                                {
                                    Main.tile[i, j].frameX = 0x58;
                                    Main.tile[i, j].frameY = 0x84;
                                }
                                if (Main.tile[i, j].frameNumber == 1)
                                {
                                    Main.tile[i, j].frameX = 0x58;
                                    Main.tile[i, j].frameY = 0x9a;
                                }
                                if (Main.tile[i, j].frameNumber == 2)
                                {
                                    Main.tile[i, j].frameX = 0x58;
                                    Main.tile[i, j].frameY = 0xb0;
                                }
                            }
                        }
                        if (
                            (
                                (
                                    (
                                        (Main.tile[i, j].frameX == 0x42)
                                        && (
                                            (
                                                (Main.tile[i, j].frameY == 0)
                                                || (Main.tile[i, j].frameY == 0x16)
                                            ) || (Main.tile[i, j].frameY == 0x2c)
                                        )
                                    )
                                    || (
                                        (Main.tile[i, j].frameX == 0x58)
                                        && (
                                            (
                                                (Main.tile[i, j].frameY == 0x42)
                                                || (Main.tile[i, j].frameY == 0x58)
                                            ) || (Main.tile[i, j].frameY == 110)
                                        )
                                    )
                                )
                                || (
                                    (Main.tile[i, j].frameX == 0x2c)
                                    && (
                                        (
                                            (Main.tile[i, j].frameY == 0xc6)
                                            || (Main.tile[i, j].frameY == 220)
                                        ) || (Main.tile[i, j].frameY == 0xf2)
                                    )
                                )
                            )
                            || (
                                (Main.tile[i, j].frameX == 0x42)
                                && (
                                    (
                                        (Main.tile[i, j].frameY == 0xc6)
                                        || (Main.tile[i, j].frameY == 220)
                                    ) || (Main.tile[i, j].frameY == 0xf2)
                                )
                            )
                        )
                        {
                            if ((index != type) && (num5 != type))
                            {
                                KillTile(i, j, false);
                            }
                        }
                        else if (num7 == -1)
                        {
                            KillTile(i, j, false);
                        }
                        else if (
                            ((num2 != type) && (Main.tile[i, j].frameY < 0xc6))
                            && (
                                (
                                    (Main.tile[i, j].frameX != 0x16)
                                    && (Main.tile[i, j].frameX != 0x2c)
                                ) || (Main.tile[i, j].frameY < 0x84)
                            )
                        )
                        {
                            if ((index == type) || (num5 == type))
                            {
                                if (num7 == type)
                                {
                                    if ((index == type) && (num5 == type))
                                    {
                                        if (Main.tile[i, j].frameNumber == 0)
                                        {
                                            Main.tile[i, j].frameX = 0x84;
                                            Main.tile[i, j].frameY = 0x84;
                                        }
                                        if (Main.tile[i, j].frameNumber == 1)
                                        {
                                            Main.tile[i, j].frameX = 0x84;
                                            Main.tile[i, j].frameY = 0x9a;
                                        }
                                        if (Main.tile[i, j].frameNumber == 2)
                                        {
                                            Main.tile[i, j].frameX = 0x84;
                                            Main.tile[i, j].frameY = 0xb0;
                                        }
                                    }
                                    else if (index == type)
                                    {
                                        if (Main.tile[i, j].frameNumber == 0)
                                        {
                                            Main.tile[i, j].frameX = 0x84;
                                            Main.tile[i, j].frameY = 0;
                                        }
                                        if (Main.tile[i, j].frameNumber == 1)
                                        {
                                            Main.tile[i, j].frameX = 0x84;
                                            Main.tile[i, j].frameY = 0x16;
                                        }
                                        if (Main.tile[i, j].frameNumber == 2)
                                        {
                                            Main.tile[i, j].frameX = 0x84;
                                            Main.tile[i, j].frameY = 0x2c;
                                        }
                                    }
                                    else if (num5 == type)
                                    {
                                        if (Main.tile[i, j].frameNumber == 0)
                                        {
                                            Main.tile[i, j].frameX = 0x84;
                                            Main.tile[i, j].frameY = 0x42;
                                        }
                                        if (Main.tile[i, j].frameNumber == 1)
                                        {
                                            Main.tile[i, j].frameX = 0x84;
                                            Main.tile[i, j].frameY = 0x58;
                                        }
                                        if (Main.tile[i, j].frameNumber == 2)
                                        {
                                            Main.tile[i, j].frameX = 0x84;
                                            Main.tile[i, j].frameY = 110;
                                        }
                                    }
                                }
                                else if ((index == type) && (num5 == type))
                                {
                                    if (Main.tile[i, j].frameNumber == 0)
                                    {
                                        Main.tile[i, j].frameX = 0x9a;
                                        Main.tile[i, j].frameY = 0x84;
                                    }
                                    if (Main.tile[i, j].frameNumber == 1)
                                    {
                                        Main.tile[i, j].frameX = 0x9a;
                                        Main.tile[i, j].frameY = 0x9a;
                                    }
                                    if (Main.tile[i, j].frameNumber == 2)
                                    {
                                        Main.tile[i, j].frameX = 0x9a;
                                        Main.tile[i, j].frameY = 0xb0;
                                    }
                                }
                                else if (index == type)
                                {
                                    if (Main.tile[i, j].frameNumber == 0)
                                    {
                                        Main.tile[i, j].frameX = 0x9a;
                                        Main.tile[i, j].frameY = 0;
                                    }
                                    if (Main.tile[i, j].frameNumber == 1)
                                    {
                                        Main.tile[i, j].frameX = 0x9a;
                                        Main.tile[i, j].frameY = 0x16;
                                    }
                                    if (Main.tile[i, j].frameNumber == 2)
                                    {
                                        Main.tile[i, j].frameX = 0x9a;
                                        Main.tile[i, j].frameY = 0x2c;
                                    }
                                }
                                else if (num5 == type)
                                {
                                    if (Main.tile[i, j].frameNumber == 0)
                                    {
                                        Main.tile[i, j].frameX = 0x9a;
                                        Main.tile[i, j].frameY = 0x42;
                                    }
                                    if (Main.tile[i, j].frameNumber == 1)
                                    {
                                        Main.tile[i, j].frameX = 0x9a;
                                        Main.tile[i, j].frameY = 0x58;
                                    }
                                    if (Main.tile[i, j].frameNumber == 2)
                                    {
                                        Main.tile[i, j].frameX = 0x9a;
                                        Main.tile[i, j].frameY = 110;
                                    }
                                }
                            }
                            else
                            {
                                if (Main.tile[i, j].frameNumber == 0)
                                {
                                    Main.tile[i, j].frameX = 110;
                                    Main.tile[i, j].frameY = 0;
                                }
                                if (Main.tile[i, j].frameNumber == 1)
                                {
                                    Main.tile[i, j].frameX = 110;
                                    Main.tile[i, j].frameY = 0x16;
                                }
                                if (Main.tile[i, j].frameNumber == 2)
                                {
                                    Main.tile[i, j].frameX = 110;
                                    Main.tile[i, j].frameY = 0x2c;
                                }
                            }
                        }
                        rectangle.X = Main.tile[i, j].frameX;
                        rectangle.Y = Main.tile[i, j].frameY;
                    }
                    int frameNumber = 0;
                    if (resetFrame)
                    {
                        frameNumber = Main.rand.Next(0, 3);
                        Main.tile[i, j].frameNumber = (byte)frameNumber;
                    }
                    else
                    {
                        frameNumber = Main.tile[i, j].frameNumber;
                    }
                    if (type == 0)
                    {
                        for (num21 = 0; num21 < 12; num21++)
                        {
                            switch (num21)
                            {
                                case 1:
                                case 6:
                                case 7:
                                case 8:
                                case 9:
                                    if (num2 == num21)
                                    {
                                        TileFrame(i, j - 1, false);
                                        if (WorldGen.mergeDown)
                                        {
                                            num2 = type;
                                        }
                                    }
                                    if (num7 == num21)
                                    {
                                        TileFrame(i, j + 1, false);
                                        if (WorldGen.mergeUp)
                                        {
                                            num7 = type;
                                        }
                                    }
                                    if (index == num21)
                                    {
                                        TileFrame(i - 1, j, false);
                                        if (WorldGen.mergeRight)
                                        {
                                            index = type;
                                        }
                                    }
                                    if (num5 == num21)
                                    {
                                        TileFrame(i + 1, j, false);
                                        if (WorldGen.mergeLeft)
                                        {
                                            num5 = type;
                                        }
                                    }
                                    if (num == num21)
                                    {
                                        num = type;
                                    }
                                    if (num3 == num21)
                                    {
                                        num3 = type;
                                    }
                                    if (num6 == num21)
                                    {
                                        num6 = type;
                                    }
                                    if (num8 == num21)
                                    {
                                        num8 = type;
                                    }
                                    break;
                            }
                        }
                        if (num2 == 2)
                        {
                            num2 = type;
                        }
                        if (num7 == 2)
                        {
                            num7 = type;
                        }
                        if (index == 2)
                        {
                            index = type;
                        }
                        if (num5 == 2)
                        {
                            num5 = type;
                        }
                        if (num == 2)
                        {
                            num = type;
                        }
                        if (num3 == 2)
                        {
                            num3 = type;
                        }
                        if (num6 == 2)
                        {
                            num6 = type;
                        }
                        if (num8 == 2)
                        {
                            num8 = type;
                        }
                    }
                    if (
                        (((type == 1) || (type == 6)) || ((type == 7) || (type == 8)))
                        || (type == 9)
                    )
                    {
                        for (num21 = 0; num21 < 12; num21++)
                        {
                            switch (num21)
                            {
                                case 1:
                                case 6:
                                case 7:
                                case 8:
                                case 9:
                                    if (num2 == 0)
                                    {
                                        num2 = -2;
                                    }
                                    if (num7 == 0)
                                    {
                                        num7 = -2;
                                    }
                                    if (index == 0)
                                    {
                                        index = -2;
                                    }
                                    if (num5 == 0)
                                    {
                                        num5 = -2;
                                    }
                                    if (num == 0)
                                    {
                                        num = -2;
                                    }
                                    if (num3 == 0)
                                    {
                                        num3 = -2;
                                    }
                                    if (num6 == 0)
                                    {
                                        num6 = -2;
                                    }
                                    if (num8 == 0)
                                    {
                                        num8 = -2;
                                    }
                                    break;
                            }
                        }
                    }
                    if (type == 2)
                    {
                        int num22 = 0;
                        if (
                            ((num2 != type) && (num2 != num22))
                            && ((num7 == type) || (num7 == num22))
                        )
                        {
                            if ((index == num22) && (num5 == type))
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
                            else if ((index == type) && (num5 == num22))
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
                            ((num7 != type) && (num7 != num22))
                            && ((num2 == type) || (num2 == num22))
                        )
                        {
                            if ((index == num22) && (num5 == type))
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
                            else if ((index == type) && (num5 == num22))
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
                            ((index != type) && (index != num22))
                            && ((num5 == type) || (num5 == num22))
                        )
                        {
                            if ((num2 == num22) && (num7 == type))
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
                            else if ((num7 == type) && (num5 == num2))
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
                            ((num5 != type) && (num5 != num22))
                            && ((index == type) || (index == num22))
                        )
                        {
                            if ((num2 == num22) && (num7 == type))
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
                            else if ((num7 == type) && (num5 == num2))
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
                            (((num2 == type) && (num7 == type)) && (index == type))
                            && (num5 == type)
                        )
                        {
                            if (
                                (((num != type) && (num3 != type)) && (num6 != type))
                                && (num8 != type)
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
                            else if ((num != type) && (num8 != type))
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
                            else if ((num3 != type) && (num6 != type))
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
                                (((num != type) && (num3 == type)) && (num6 == type))
                                && (num8 == type)
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
                                (((num == type) && (num3 != type)) && (num6 == type))
                                && (num8 == type)
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
                                (((num == type) && (num3 == type)) && (num6 != type))
                                && (num8 == type)
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
                                (((num == type) && (num3 == type)) && (num6 == type))
                                && (num8 != type)
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
                            (
                                (
                                    ((num2 == type) && (num7 == num22))
                                    && ((index == type) && (num5 == type))
                                ) && (num == -1)
                            ) && (num3 == -1)
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
                            (
                                (
                                    ((num2 == num22) && (num7 == type))
                                    && ((index == type) && (num5 == type))
                                ) && (num6 == -1)
                            ) && (num8 == -1)
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
                            (
                                (
                                    ((num2 == type) && (num7 == type))
                                    && ((index == num22) && (num5 == type))
                                ) && (num3 == -1)
                            ) && (num8 == -1)
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
                            (
                                (
                                    ((num2 == type) && (num7 == type))
                                    && ((index == type) && (num5 == num22))
                                ) && (num == -1)
                            ) && (num6 == -1)
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
                            (((num2 == type) && (num7 == num22)) && (index == type))
                            && (num5 == type)
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
                            (((num2 == num22) && (num7 == type)) && (index == type))
                            && (num5 == type)
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
                            (((num2 == type) && (num7 == type)) && (index == type))
                            && (num5 == num22)
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
                            (((num2 == type) && (num7 == type)) && (index == num22))
                            && (num5 == type)
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
                                (
                                    (
                                        ((num2 == num22) && (num7 == type))
                                        && ((index == type) && (num5 == type))
                                    )
                                    || (
                                        ((num2 == type) && (num7 == num22))
                                        && ((index == type) && (num5 == type))
                                    )
                                )
                                || (
                                    ((num2 == type) && (num7 == type))
                                    && ((index == num22) && (num5 == type))
                                )
                            )
                            || (
                                (((num2 == type) && (num7 == type)) && (index == type))
                                && (num5 == num22)
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
                            (
                                (
                                    ((num2 == type) || (num2 == num22))
                                    && ((num7 == type) || (num7 == num22))
                                ) && ((index == type) || (index == num22))
                            ) && ((num5 == type) || (num5 == num22))
                        )
                        {
                            if (
                                (
                                    (
                                        ((num != type) && (num != num22))
                                        && ((num3 == type) || (num3 == num22))
                                    ) && ((num6 == type) || (num6 == num22))
                                ) && ((num8 == type) || (num8 == num22))
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
                                (
                                    (
                                        ((num3 != type) && (num3 != num22))
                                        && ((num == type) || (num == num22))
                                    ) && ((num6 == type) || (num6 == num22))
                                ) && ((num8 == type) || (num8 == num22))
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
                                (
                                    (
                                        ((num6 != type) && (num6 != num22))
                                        && ((num == type) || (num == num22))
                                    ) && ((num3 == type) || (num3 == num22))
                                ) && ((num8 == type) || (num8 == num22))
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
                                (
                                    (
                                        ((num8 != type) && (num8 != num22))
                                        && ((num == type) || (num == num22))
                                    ) && ((num6 == type) || (num6 == num22))
                                ) && ((num3 == type) || (num3 == num22))
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
                            (
                                (
                                    ((num2 != num22) && (num2 != type))
                                    && ((num7 == type) && (index != num22))
                                ) && (((index != type) && (num5 == type)) && (num8 != num22))
                            ) && (num8 != type)
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
                            (
                                (
                                    ((num2 != num22) && (num2 != type))
                                    && ((num7 == type) && (index == type))
                                ) && (((num5 != num22) && (num5 != type)) && (num6 != num22))
                            ) && (num6 != type)
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
                            (
                                (
                                    ((num7 != num22) && (num7 != type))
                                    && ((num2 == type) && (index != num22))
                                ) && (((index != type) && (num5 == type)) && (num3 != num22))
                            ) && (num3 != type)
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
                            (
                                (
                                    ((num7 != num22) && (num7 != type))
                                    && ((num2 == type) && (index == type))
                                ) && (((num5 != num22) && (num5 != type)) && (num != num22))
                            ) && (num != type)
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
                            (
                                (
                                    ((num2 != type) && (num2 != num22))
                                    && ((num7 == type) && (index == type))
                                )
                                && (
                                    ((num5 == type) && (num6 != type))
                                    && ((num6 != num22) && (num8 != type))
                                )
                            ) && (num8 != num22)
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
                            (
                                (
                                    ((num7 != type) && (num7 != num22))
                                    && ((num2 == type) && (index == type))
                                )
                                && (
                                    ((num5 == type) && (num != type))
                                    && ((num != num22) && (num3 != type))
                                )
                            ) && (num3 != num22)
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
                            (
                                (
                                    ((index != type) && (index != num22))
                                    && ((num7 == type) && (num2 == type))
                                )
                                && (
                                    ((num5 == type) && (num3 != type))
                                    && ((num3 != num22) && (num8 != type))
                                )
                            ) && (num8 != num22)
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
                            (
                                (
                                    ((num5 != type) && (num5 != num22))
                                    && ((num7 == type) && (num2 == type))
                                )
                                && (
                                    ((index == type) && (num != type))
                                    && ((num != num22) && (num6 != type))
                                )
                            ) && (num6 != num22)
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
                            (
                                (
                                    ((num2 != num22) && (num2 != type))
                                    && ((num7 == num22) || (num7 == type))
                                ) && (index == num22)
                            ) && (num5 == num22)
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
                            (
                                (
                                    ((num7 != num22) && (num7 != type))
                                    && ((num2 == num22) || (num2 == type))
                                ) && (index == num22)
                            ) && (num5 == num22)
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
                            (
                                (
                                    ((index != num22) && (index != type))
                                    && ((num5 == num22) || (num5 == type))
                                ) && (num2 == num22)
                            ) && (num7 == num22)
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
                            (
                                (
                                    ((num5 != num22) && (num5 != type))
                                    && ((index == num22) || (index == type))
                                ) && (num2 == num22)
                            ) && (num7 == num22)
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
                            (((num2 == type) && (num7 == num22)) && (index == num22))
                            && (num5 == num22)
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
                            (((num2 == num22) && (num7 == type)) && (index == num22))
                            && (num5 == num22)
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
                            (((num2 == num22) && (num7 == num22)) && (index == type))
                            && (num5 == num22)
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
                            (((num2 == num22) && (num7 == num22)) && (index == num22))
                            && (num5 == type)
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
                            (
                                ((num2 != type) && (num2 != num22))
                                && ((num7 == type) && (index == type))
                            ) && (num5 == type)
                        )
                        {
                            if (
                                (((num6 == num22) || (num6 == type)) && (num8 != num22))
                                && (num8 != type)
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
                                (((num8 == num22) || (num8 == type)) && (num6 != num22))
                                && (num6 != type)
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
                            (
                                ((num7 != type) && (num7 != num22))
                                && ((num2 == type) && (index == type))
                            ) && (num5 == type)
                        )
                        {
                            if (
                                (((num == num22) || (num == type)) && (num3 != num22))
                                && (num3 != type)
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
                                (((num3 == num22) || (num3 == type)) && (num != num22))
                                && (num != type)
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
                            (
                                ((index != type) && (index != num22))
                                && ((num2 == type) && (num7 == type))
                            ) && (num5 == type)
                        )
                        {
                            if (
                                (((num3 == num22) || (num3 == type)) && (num8 != num22))
                                && (num8 != type)
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
                                (((num8 == num22) || (num8 == type)) && (num3 != num22))
                                && (num3 != type)
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
                            (
                                ((num5 != type) && (num5 != num22))
                                && ((num2 == type) && (num7 == type))
                            ) && (index == type)
                        )
                        {
                            if (
                                (((num == num22) || (num == type)) && (num6 != num22))
                                && (num6 != type)
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
                                (((num6 == num22) || (num6 == type)) && (num != num22))
                                && (num != type)
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
                            (
                                (
                                    (
                                        ((num2 == type) || (num2 == num22))
                                        && ((num7 == type) || (num7 == num22))
                                    )
                                    && (
                                        ((index == type) || (index == num22))
                                        && ((num5 == type) || (num5 == num22))
                                    )
                                ) && (((num != -1) && (num3 != -1)) && (num6 != -1))
                            ) && (num8 != -1)
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
                        {
                            num2 = -2;
                        }
                        if (num7 == num22)
                        {
                            num7 = -2;
                        }
                        if (index == num22)
                        {
                            index = -2;
                        }
                        if (num5 == num22)
                        {
                            num5 = -2;
                        }
                        if (num == num22)
                        {
                            num = -2;
                        }
                        if (num3 == num22)
                        {
                            num3 = -2;
                        }
                        if (num6 == num22)
                        {
                            num6 = -2;
                        }
                        if (num8 == num22)
                        {
                            num8 = -2;
                        }
                    }
                    if (
                        (
                            (((type == 1) || (type == 2)) || ((type == 6) || (type == 7)))
                            && (rectangle.X == -1)
                        ) && (rectangle.Y == -1)
                    )
                    {
                        if ((num2 >= 0) && (num2 != type))
                        {
                            num2 = -1;
                        }
                        if ((num7 >= 0) && (num7 != type))
                        {
                            num7 = -1;
                        }
                        if ((index >= 0) && (index != type))
                        {
                            index = -1;
                        }
                        if ((num5 >= 0) && (num5 != type))
                        {
                            num5 = -1;
                        }
                        if ((((num2 != -1) && (num7 != -1)) && (index != -1)) && (num5 != -1))
                        {
                            if (
                                (((num2 == -2) && (num7 == type)) && (index == type))
                                && (num5 == type)
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
                                WorldGen.mergeUp = true;
                            }
                            else if (
                                (((num2 == type) && (num7 == -2)) && (index == type))
                                && (num5 == type)
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
                                WorldGen.mergeDown = true;
                            }
                            else if (
                                (((num2 == type) && (num7 == type)) && (index == -2))
                                && (num5 == type)
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
                                WorldGen.mergeLeft = true;
                            }
                            else if (
                                (((num2 == type) && (num7 == type)) && (index == type))
                                && (num5 == -2)
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
                                WorldGen.mergeRight = true;
                            }
                            else if (
                                (((num2 == -2) && (num7 == type)) && (index == -2))
                                && (num5 == type)
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
                                WorldGen.mergeUp = true;
                                WorldGen.mergeLeft = true;
                            }
                            else if (
                                (((num2 == -2) && (num7 == type)) && (index == type))
                                && (num5 == -2)
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
                                WorldGen.mergeUp = true;
                                WorldGen.mergeRight = true;
                            }
                            else if (
                                (((num2 == type) && (num7 == -2)) && (index == -2))
                                && (num5 == type)
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
                                WorldGen.mergeDown = true;
                                WorldGen.mergeLeft = true;
                            }
                            else if (
                                (((num2 == type) && (num7 == -2)) && (index == type))
                                && (num5 == -2)
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
                                WorldGen.mergeDown = true;
                                WorldGen.mergeRight = true;
                            }
                            else if (
                                (((num2 == type) && (num7 == type)) && (index == -2))
                                && (num5 == -2)
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
                                WorldGen.mergeLeft = true;
                                WorldGen.mergeRight = true;
                            }
                            else if (
                                (((num2 == -2) && (num7 == -2)) && (index == type))
                                && (num5 == type)
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
                                WorldGen.mergeUp = true;
                                WorldGen.mergeDown = true;
                            }
                            else if (
                                (((num2 == -2) && (num7 == type)) && (index == -2)) && (num5 == -2)
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
                                WorldGen.mergeUp = true;
                                WorldGen.mergeLeft = true;
                                WorldGen.mergeRight = true;
                            }
                            else if (
                                (((num2 == type) && (num7 == -2)) && (index == -2)) && (num5 == -2)
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
                                WorldGen.mergeDown = true;
                                WorldGen.mergeLeft = true;
                                WorldGen.mergeRight = true;
                            }
                            else if (
                                (((num2 == -2) && (num7 == -2)) && (index == type)) && (num5 == -2)
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
                                WorldGen.mergeUp = true;
                                WorldGen.mergeDown = true;
                                WorldGen.mergeRight = true;
                            }
                            else if (
                                (((num2 == -2) && (num7 == -2)) && (index == -2)) && (num5 == type)
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
                                WorldGen.mergeUp = true;
                                WorldGen.mergeDown = true;
                                WorldGen.mergeLeft = true;
                            }
                            else if (
                                (((num2 == -2) && (num7 == -2)) && (index == -2)) && (num5 == -2)
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
                                WorldGen.mergeUp = true;
                                WorldGen.mergeDown = true;
                                WorldGen.mergeLeft = true;
                                WorldGen.mergeRight = true;
                            }
                            else if (
                                (((num2 == type) && (num7 == type)) && (index == type))
                                && (num5 == type)
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
                            (((num2 != -1) && (num7 != -1)) && (index == -1)) && (num5 == type)
                        )
                        {
                            if ((num2 == -2) && (num7 == type))
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
                                WorldGen.mergeUp = true;
                            }
                            else if ((num7 == -2) && (num2 == type))
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
                                WorldGen.mergeDown = true;
                            }
                        }
                        else if (
                            (((num2 != -1) && (num7 != -1)) && (index == type)) && (num5 == -1)
                        )
                        {
                            if ((num2 == -2) && (num7 == type))
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
                                WorldGen.mergeUp = true;
                            }
                            else if ((num7 == -2) && (num2 == type))
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
                                WorldGen.mergeDown = true;
                            }
                        }
                        else if (
                            (((num2 == -1) && (num7 == type)) && (index != -1)) && (num5 != -1)
                        )
                        {
                            if ((index == -2) && (num5 == type))
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
                                WorldGen.mergeLeft = true;
                            }
                            else if ((num5 == -2) && (index == type))
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
                                WorldGen.mergeRight = true;
                            }
                        }
                        else if (
                            (((num2 == type) && (num7 == -1)) && (index != -1)) && (num5 != -1)
                        )
                        {
                            if ((index == -2) && (num5 == type))
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
                                WorldGen.mergeLeft = true;
                            }
                            else if ((num5 == -2) && (index == type))
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
                                WorldGen.mergeRight = true;
                            }
                        }
                        else if ((((num2 != -1) && (num7 != -1)) && (index == -1)) && (num5 == -1))
                        {
                            if ((num2 == -2) && (num7 == -2))
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
                                WorldGen.mergeUp = true;
                                WorldGen.mergeDown = true;
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
                                WorldGen.mergeUp = true;
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
                                WorldGen.mergeDown = true;
                            }
                        }
                        else if ((((num2 == -1) && (num7 == -1)) && (index != -1)) && (num5 != -1))
                        {
                            if ((index == -2) && (num5 == -2))
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
                                WorldGen.mergeLeft = true;
                                WorldGen.mergeRight = true;
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
                                WorldGen.mergeLeft = true;
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
                                WorldGen.mergeRight = true;
                            }
                        }
                        else if ((((num2 == -2) && (num7 == -1)) && (index == -1)) && (num5 == -1))
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
                            WorldGen.mergeUp = true;
                        }
                        else if ((((num2 == -1) && (num7 == -2)) && (index == -1)) && (num5 == -1))
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
                            WorldGen.mergeDown = true;
                        }
                        else if ((((num2 == -1) && (num7 == -1)) && (index == -2)) && (num5 == -1))
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
                            WorldGen.mergeLeft = true;
                        }
                        else if ((((num2 == -1) && (num7 == -1)) && (index == -1)) && (num5 == -2))
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
                            WorldGen.mergeRight = true;
                        }
                    }
                    if ((rectangle.X < 0) || (rectangle.Y < 0))
                    {
                        if (type == 2)
                        {
                            if (num2 == -2)
                            {
                                num2 = type;
                            }
                            if (num7 == -2)
                            {
                                num7 = type;
                            }
                            if (index == -2)
                            {
                                index = type;
                            }
                            if (num5 == -2)
                            {
                                num5 = type;
                            }
                            if (num == -2)
                            {
                                num = type;
                            }
                            if (num3 == -2)
                            {
                                num3 = type;
                            }
                            if (num6 == -2)
                            {
                                num6 = type;
                            }
                            if (num8 == -2)
                            {
                                num8 = type;
                            }
                        }
                        if (
                            ((num2 == type) && (num7 == type)) && ((index == type) & (num5 == type))
                        )
                        {
                            if ((num != type) && (num3 != type))
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
                            else if ((num6 != type) && (num8 != type))
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
                            else if ((num != type) && (num6 != type))
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
                            else if ((num3 != type) && (num8 != type))
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
                            ((num2 != type) && (num7 == type)) && ((index == type) & (num5 == type))
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
                            ((num2 == type) && (num7 != type)) && ((index == type) & (num5 == type))
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
                            ((num2 == type) && (num7 == type)) && ((index != type) & (num5 == type))
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
                            ((num2 == type) && (num7 == type)) && ((index == type) & (num5 != type))
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
                            ((num2 != type) && (num7 == type)) && ((index != type) & (num5 == type))
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
                            ((num2 != type) && (num7 == type)) && ((index == type) & (num5 != type))
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
                            ((num2 == type) && (num7 != type)) && ((index != type) & (num5 == type))
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
                            ((num2 == type) && (num7 != type)) && ((index == type) & (num5 != type))
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
                            ((num2 == type) && (num7 == type)) && ((index != type) & (num5 != type))
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
                            ((num2 != type) && (num7 != type)) && ((index == type) & (num5 == type))
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
                            ((num2 != type) && (num7 == type)) && ((index != type) & (num5 != type))
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
                            ((num2 == type) && (num7 != type)) && ((index != type) & (num5 != type))
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
                            ((num2 != type) && (num7 != type)) && ((index != type) & (num5 == type))
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
                            ((num2 != type) && (num7 != type)) && ((index == type) & (num5 != type))
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
                            ((num2 != type) && (num7 != type)) && ((index != type) & (num5 != type))
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
                    if ((rectangle.X <= -1) || (rectangle.Y <= -1))
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
                    Main.tile[i, j].frameX = (short)rectangle.X;
                    Main.tile[i, j].frameY = (short)rectangle.Y;
                    if (
                        (((rectangle.X != frameX) && (rectangle.Y != frameY)) && (frameX >= 0))
                        && (frameY >= 0)
                    )
                    {
                        bool mergeUp = WorldGen.mergeUp;
                        bool mergeDown = WorldGen.mergeDown;
                        bool mergeLeft = WorldGen.mergeLeft;
                        bool mergeRight = WorldGen.mergeRight;
                        TileFrame(i - 1, j, false);
                        TileFrame(i + 1, j, false);
                        TileFrame(i, j - 1, false);
                        TileFrame(i, j + 1, false);
                        WorldGen.mergeUp = mergeUp;
                        WorldGen.mergeDown = mergeDown;
                        WorldGen.mergeLeft = mergeLeft;
                        WorldGen.mergeRight = mergeRight;
                    }
                }
            }
        }

        public static void TileRunner(
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
            double num5 = strength;
            float num6 = steps;
            vector.X = i;
            vector.Y = j;
            vector2.X = Main.rand.Next(-10, 11) * 0.1f;
            vector2.Y = Main.rand.Next(-10, 11) * 0.1f;
            if ((speedX != 0f) || (speedY != 0f))
            {
                vector2.X = speedX;
                vector2.Y = speedY;
            }
            while ((num5 > 0.0) && (num6 > 0f))
            {
                num5 = strength * (num6 / ((float)steps));
                num6--;
                int num = (int)(vector.X - (num5 * 0.5));
                int num3 = (int)(vector.X + (num5 * 0.5));
                int num2 = (int)(vector.Y - (num5 * 0.5));
                int num4 = (int)(vector.Y + (num5 * 0.5));
                if (num < 0)
                {
                    num = 0;
                }
                if (num3 > 0x1389)
                {
                    num3 = 0x1389;
                }
                if (num2 < 0)
                {
                    num2 = 0;
                }
                if (num4 > 0x9c5)
                {
                    num4 = 0x9c5;
                }
                for (int k = num; k < num3; k++)
                {
                    for (int m = num2; m < num4; m++)
                    {
                        if (
                            (Math.Abs((float)(k - vector.X)) + Math.Abs((float)(m - vector.Y)))
                            < ((strength * 0.5) * (1.0 + (Main.rand.Next(-10, 11) * 0.015)))
                        )
                        {
                            if (type == -1)
                            {
                                Main.tile[k, m].active = false;
                            }
                            else
                            {
                                if (addTile)
                                {
                                    Main.tile[k, m].active = true;
                                }
                                Main.tile[k, m].type = (byte)type;
                            }
                        }
                    }
                }
                vector += vector2;
                vector2.X += Main.rand.Next(-10, 11) * 0.05f;
                vector2.Y += Main.rand.Next(-10, 11) * 0.05f;
                if (vector2.X > 1f)
                {
                    vector2.X = 1f;
                }
                if (vector2.X < -1f)
                {
                    vector2.X = -1f;
                }
                if (vector2.Y > 1f)
                {
                    vector2.Y = 1f;
                }
                if (vector2.Y < -1f)
                {
                    vector2.Y = -1f;
                }
            }
        }

        public static void UpdateWorld()
        {
            float num = 0.0002f;
            for (int i = 0; i < (1.25075E+07f * num); i++)
            {
                int num3 = Main.rand.Next(0x1389);
                int num4 = Main.rand.Next(((int)Main.worldSurface) - 1);
                int num5 = num3 - 1;
                int num6 = num3 + 2;
                int j = num4 - 1;
                int num8 = num4 + 2;
                if (num5 < 0)
                {
                    num5 = 0;
                }
                if (num6 > 0x1389)
                {
                    num6 = 0x1389;
                }
                if (j < 0)
                {
                    j = 0;
                }
                if (num8 > 0x9c5)
                {
                    num8 = 0x9c5;
                }
                if (Main.tile[num3, num4].active && (Main.tile[num3, num4].type == 2))
                {
                    if (!(Main.tile[num3, j].active || (Main.rand.Next(10) != 0)))
                    {
                        PlaceTile(num3, j, 3, true);
                    }
                    for (int k = num5; k < num6; k++)
                    {
                        for (int m = j; m < num8; m++)
                        {
                            if (
                                ((num3 != k) || (num4 != m))
                                && (Main.tile[k, m].active && (Main.tile[k, m].type == 0))
                            )
                            {
                                SpreadGrass(k, m, 0, 2, false);
                                if (Main.tile[k, m].type == 2)
                                {
                                    SquareTileFrame(k, m, true);
                                }
                            }
                        }
                    }
                }
            }
        }

        public static void WallFrame(int i, int j, bool resetFrame = false)
        {
            int num = -1;
            int num2 = -1;
            int num3 = -1;
            int num4 = -1;
            int num5 = -1;
            int num6 = -1;
            int num7 = -1;
            int num8 = -1;
            int wall = Main.tile[i, j].wall;
            if (wall != 0)
            {
                Rectangle rectangle;
                int wallFrameX = Main.tile[i, j].wallFrameX;
                int wallFrameY = Main.tile[i, j].wallFrameY;
                rectangle.X = -1;
                rectangle.Y = -1;
                if ((i - 1) < 0)
                {
                    num = wall;
                    num4 = wall;
                    num6 = wall;
                }
                if ((i + 1) >= 0x1389)
                {
                    num3 = wall;
                    num5 = wall;
                    num8 = wall;
                }
                if ((j - 1) < 0)
                {
                    num = wall;
                    num2 = wall;
                    num3 = wall;
                }
                if ((j + 1) >= 0x9c5)
                {
                    num6 = wall;
                    num7 = wall;
                    num8 = wall;
                }
                if ((i - 1) >= 0)
                {
                    num4 = Main.tile[i - 1, j].wall;
                }
                if ((i + 1) < 0x1389)
                {
                    num5 = Main.tile[i + 1, j].wall;
                }
                if ((j - 1) >= 0)
                {
                    num2 = Main.tile[i, j - 1].wall;
                }
                if ((j + 1) < 0x9c5)
                {
                    num7 = Main.tile[i, j + 1].wall;
                }
                if (((i - 1) >= 0) && ((j - 1) >= 0))
                {
                    num = Main.tile[i - 1, j - 1].wall;
                }
                if (((i + 1) < 0x1389) && ((j - 1) >= 0))
                {
                    num3 = Main.tile[i + 1, j - 1].wall;
                }
                if (((i - 1) >= 0) && ((j + 1) < 0x9c5))
                {
                    num6 = Main.tile[i - 1, j + 1].wall;
                }
                if (((i + 1) < 0x1389) && ((j + 1) < 0x9c5))
                {
                    num8 = Main.tile[i + 1, j + 1].wall;
                }
                int wallFrameNumber = 0;
                if (resetFrame)
                {
                    wallFrameNumber = Main.rand.Next(0, 3);
                    Main.tile[i, j].wallFrameNumber = (byte)wallFrameNumber;
                }
                else
                {
                    wallFrameNumber = Main.tile[i, j].wallFrameNumber;
                }
                if ((rectangle.X < 0) || (rectangle.Y < 0))
                {
                    if (((num2 == wall) && (num7 == wall)) && ((num4 == wall) & (num5 == wall)))
                    {
                        if ((num != wall) && (num3 != wall))
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
                        else if ((num6 != wall) && (num8 != wall))
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
                        else if ((num != wall) && (num6 != wall))
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
                        else if ((num3 != wall) && (num8 != wall))
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
                        ((num2 != wall) && (num7 == wall)) && ((num4 == wall) & (num5 == wall))
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
                        ((num2 == wall) && (num7 != wall)) && ((num4 == wall) & (num5 == wall))
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
                        ((num2 == wall) && (num7 == wall)) && ((num4 != wall) & (num5 == wall))
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
                        ((num2 == wall) && (num7 == wall)) && ((num4 == wall) & (num5 != wall))
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
                        ((num2 != wall) && (num7 == wall)) && ((num4 != wall) & (num5 == wall))
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
                        ((num2 != wall) && (num7 == wall)) && ((num4 == wall) & (num5 != wall))
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
                        ((num2 == wall) && (num7 != wall)) && ((num4 != wall) & (num5 == wall))
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
                        ((num2 == wall) && (num7 != wall)) && ((num4 == wall) & (num5 != wall))
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
                        ((num2 == wall) && (num7 == wall)) && ((num4 != wall) & (num5 != wall))
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
                        ((num2 != wall) && (num7 != wall)) && ((num4 == wall) & (num5 == wall))
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
                        ((num2 != wall) && (num7 == wall)) && ((num4 != wall) & (num5 != wall))
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
                        ((num2 == wall) && (num7 != wall)) && ((num4 != wall) & (num5 != wall))
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
                        ((num2 != wall) && (num7 != wall)) && ((num4 != wall) & (num5 == wall))
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
                        ((num2 != wall) && (num7 != wall)) && ((num4 == wall) & (num5 != wall))
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
                        ((num2 != wall) && (num7 != wall)) && ((num4 != wall) & (num5 != wall))
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
                if ((rectangle.X <= -1) || (rectangle.Y <= -1))
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
                Main.tile[i, j].wallFrameX = (byte)rectangle.X;
                Main.tile[i, j].wallFrameY = (byte)rectangle.Y;
                if (
                    (
                        ((rectangle.X != wallFrameX) && (rectangle.Y != wallFrameY))
                        && (wallFrameX >= 0)
                    ) && (wallFrameY >= 0)
                ) { }
            }
        }
    }
}
