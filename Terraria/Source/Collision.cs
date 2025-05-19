using Microsoft.Xna.Framework;

namespace Terraria;

public abstract class Collision
{
    public static bool EmptyTile(int i, int j)
    {
        int num;
        Rectangle rectangle = new Rectangle(i * 0x10, j * 0x10, 0x10, 0x10);
        if (Main.Tile[i, j].Active)
            return false;

        for (num = 0; num < 0x10; num++)
            if (
                Main.Player[num].Active
                && rectangle.Intersects(
                    new Rectangle(
                        (int)Main.Player[num].Position.X,
                        (int)Main.Player[num].Position.Y,
                        Main.Player[num].Width,
                        Main.Player[num].Height
                    )
                )
            )
                return false;

        for (num = 0; num < 0x3e8; num++)
            if (
                Main.Item[num].Active
                && rectangle.Intersects(
                    new Rectangle(
                        (int)Main.Item[num].Position.X,
                        (int)Main.Item[num].Position.Y,
                        Main.Item[num].Width,
                        Main.Item[num].Height
                    )
                )
            )
                return false;

        for (num = 0; num < 0x3e8; num++)
            if (
                Main.Npc[num].Active
                && rectangle.Intersects(
                    new Rectangle(
                        (int)Main.Npc[num].Position.X,
                        (int)Main.Npc[num].Position.Y,
                        Main.Npc[num].Width,
                        Main.Npc[num].Height
                    )
                )
            )
                return false;

        return true;
    }

    public static Vector2 TileCollision(
        Vector2 position,
        Vector2 velocity,
        int width,
        int height
    )
    {
        Vector2 vector = velocity;
        Vector2 vector2 = velocity;
        Vector2 vector4 = position + velocity;
        Vector2 vector5 = position;
        var num = (int)(position.X / 16f) - 1;
        var num2 = (int)((position.X + width) / 16f) + 2;
        var num3 = (int)(position.Y / 16f) - 1;
        var num4 = (int)((position.Y + height) / 16f) + 2;
        var num5 = -1;
        var num6 = -1;
        var num7 = -1;
        var num8 = -1;
        if (num < 0)
            num = 0;

        if (num2 > 0x1389)
            num2 = 0x1389;

        if (num3 < 0)
            num3 = 0;

        if (num4 > 0x9c5)
            num4 = 0x9c5;

        for (var i = num; i < num2; i++)
        for (var j = num3; j < num4; j++)
            if (Main.Tile[i, j].Active && Main.TileSolid[Main.Tile[i, j].Type])
            {
                Vector2 vector3;
                vector3.X = i * 0x10;
                vector3.Y = j * 0x10;
                if (
                    vector4.X + width > vector3.X
                    && vector4.X < vector3.X + 16f && vector4.Y + height > vector3.Y && vector4.Y < vector3.Y + 16f
                )
                {
                    if (vector5.Y + height <= vector3.Y)
                    {
                        num7 = i;
                        num8 = j;
                        if (num7 != num5)
                            vector.Y = vector3.Y - (vector5.Y + height);
                    }
                    else if (vector5.X + width <= vector3.X)
                    {
                        num5 = i;
                        num6 = j;
                        if (num6 != num8)
                            vector.X = vector3.X - (vector5.X + width);

                        if (num7 == num5)
                            vector.Y = vector2.Y;
                    }
                    else if (vector5.X >= vector3.X + 16f)
                    {
                        num5 = i;
                        num6 = j;
                        if (num6 != num8)
                            vector.X = vector3.X + 16f - vector5.X;

                        if (num7 == num5)
                            vector.Y = vector2.Y;
                    }
                    else if (vector5.Y >= vector3.Y + 16f)
                    {
                        num7 = i;
                        num8 = j;
                        vector.Y = vector3.Y + 16f - vector5.Y;
                        if (num8 == num6)
                            vector.X = vector2.X + 0.01f;
                    }
                }
            }

        return vector;
    }
}
