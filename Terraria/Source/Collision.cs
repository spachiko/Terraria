namespace Terraria
{
    using System;
    using Microsoft.Xna.Framework;

    public class Collision
    {
        public static bool EmptyTile(int i, int j)
        {
            int num;
            Rectangle rectangle = new Rectangle(i * 0x10, j * 0x10, 0x10, 0x10);
            if (Main.tile[i, j].active)
            {
                return false;
            }
            for (num = 0; num < 0x10; num++)
            {
                if (
                    Main.player[num].active
                    && rectangle.Intersects(
                        new Rectangle(
                            (int)Main.player[num].position.X,
                            (int)Main.player[num].position.Y,
                            Main.player[num].width,
                            Main.player[num].height
                        )
                    )
                )
                {
                    return false;
                }
            }
            for (num = 0; num < 0x3e8; num++)
            {
                if (
                    Main.item[num].active
                    && rectangle.Intersects(
                        new Rectangle(
                            (int)Main.item[num].position.X,
                            (int)Main.item[num].position.Y,
                            Main.item[num].width,
                            Main.item[num].height
                        )
                    )
                )
                {
                    return false;
                }
            }
            for (num = 0; num < 0x3e8; num++)
            {
                if (
                    Main.npc[num].active
                    && rectangle.Intersects(
                        new Rectangle(
                            (int)Main.npc[num].position.X,
                            (int)Main.npc[num].position.Y,
                            Main.npc[num].width,
                            Main.npc[num].height
                        )
                    )
                )
                {
                    return false;
                }
            }
            return true;
        }

        public static Vector2 TileCollision(
            Vector2 Position,
            Vector2 Velocity,
            int Width,
            int Height
        )
        {
            Vector2 vector = Velocity;
            Vector2 vector2 = Velocity;
            Vector2 vector4 = Position + Velocity;
            Vector2 vector5 = Position;
            int num = ((int)(Position.X / 16f)) - 1;
            int num2 = ((int)((Position.X + Width) / 16f)) + 2;
            int num3 = ((int)(Position.Y / 16f)) - 1;
            int num4 = ((int)((Position.Y + Height) / 16f)) + 2;
            int num5 = -1;
            int num6 = -1;
            int num7 = -1;
            int num8 = -1;
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
            for (int i = num; i < num2; i++)
            {
                for (int j = num3; j < num4; j++)
                {
                    if (Main.tile[i, j].active && Main.tileSolid[Main.tile[i, j].type])
                    {
                        Vector2 vector3;
                        vector3.X = i * 0x10;
                        vector3.Y = j * 0x10;
                        if (
                            (
                                (
                                    ((vector4.X + Width) > vector3.X)
                                    && (vector4.X < (vector3.X + 16f))
                                ) && ((vector4.Y + Height) > vector3.Y)
                            ) && (vector4.Y < (vector3.Y + 16f))
                        )
                        {
                            if ((vector5.Y + Height) <= vector3.Y)
                            {
                                num7 = i;
                                num8 = j;
                                if (num7 != num5)
                                {
                                    vector.Y = vector3.Y - (vector5.Y + Height);
                                }
                            }
                            else if ((vector5.X + Width) <= vector3.X)
                            {
                                num5 = i;
                                num6 = j;
                                if (num6 != num8)
                                {
                                    vector.X = vector3.X - (vector5.X + Width);
                                }
                                if (num7 == num5)
                                {
                                    vector.Y = vector2.Y;
                                }
                            }
                            else if (vector5.X >= (vector3.X + 16f))
                            {
                                num5 = i;
                                num6 = j;
                                if (num6 != num8)
                                {
                                    vector.X = (vector3.X + 16f) - vector5.X;
                                }
                                if (num7 == num5)
                                {
                                    vector.Y = vector2.Y;
                                }
                            }
                            else if (vector5.Y >= (vector3.Y + 16f))
                            {
                                num7 = i;
                                num8 = j;
                                vector.Y = (vector3.Y + 16f) - vector5.Y;
                                if (num8 == num6)
                                {
                                    vector.X = vector2.X + 0.01f;
                                }
                            }
                        }
                    }
                }
            }
            return vector;
        }
    }
}
