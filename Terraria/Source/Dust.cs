using Microsoft.Xna.Framework;

namespace Terraria;

public class Dust
{
    private int _alpha;
    private Vector2 _velocity;
    public bool Active;
    public Color Color;
    public Rectangle Frame;
    public Vector2 Position;
    public float Rotation;
    public float Scale;
    public int Type;

    public Color GetAlpha(Color newColor)
    {
        var r = newColor.R - _alpha;
        var g = newColor.G - _alpha;
        var b = newColor.B - _alpha;
        var a = newColor.A - _alpha;
        if (a < 0)
            a = 0;

        if (a > 0xff)
            a = 0xff;

        return new Color(r, g, b, a);
    }

    public Color GetColor(Color newColor)
    {
        var r = Color.R - (0xff - newColor.R);
        var g = Color.G - (0xff - newColor.G);
        var b = Color.B - (0xff - newColor.B);
        var a = Color.A - (0xff - newColor.A);
        if (r < 0)
            r = 0;

        if (g < 0)
            g = 0;

        if (b < 0)
            b = 0;

        if (a < 0)
            a = 0;

        return new Color(r, g, b, a);
    }

    public static void NewDust(
        Vector2 position,
        int width,
        int height,
        int type,
        float speedX = 0f,
        float speedY = 0f,
        int alpha = 0,
        Color newColor = new Color()
    )
    {
        for (var i = 0; i < 0x3e8; i++)
            if (!Main.Dust[i].Active)
            {
                Main.Dust[i].Active = true;
                Main.Dust[i].Type = type;
                Main.Dust[i].Color = newColor;
                Main.Dust[i]._alpha = alpha;
                Main.Dust[i].Position.X = position.X + Main.Rand.Next(width - 4) + 4f;
                Main.Dust[i].Position.Y = position.Y + Main.Rand.Next(height - 4) + 4f;
                Main.Dust[i]._velocity.X = Main.Rand.Next(-20, 0x15) * 0.1f + speedX;
                Main.Dust[i]._velocity.Y = Main.Rand.Next(-20, 0x15) * 0.1f + speedY;
                Main.Dust[i].Frame.X = 10 * type;
                Main.Dust[i].Frame.Y = 10 * Main.Rand.Next(3);
                Main.Dust[i].Frame.Width = 8;
                Main.Dust[i].Frame.Height = 8;
                Main.Dust[i].Rotation = 0f;
                Main.Dust[i].Scale = 1f + Main.Rand.Next(-20, 0x15) * 0.01f;
                if (Main.Dust[i].Type == 6)
                {
                    Main.Dust[i]._velocity.Y = Main.Rand.Next(-10, 6) * 0.1f;
                    Main.Dust[i]._velocity.X *= 0.3f;
                    Dust dust1 = Main.Dust[i];
                    dust1.Scale *= 0.7f;
                }

                break;
            }
    }

    public static void UpdateDust()
    {
        for (var i = 0; i < 0x3e8; i++)
            if (Main.Dust[i].Active)
            {
                Dust dust1 = Main.Dust[i];
                dust1.Position += Main.Dust[i]._velocity;
                if (Main.Dust[i].Type == 6)
                    Main.Dust[i]._velocity.Y += 0.05f;
                else
                    Main.Dust[i]._velocity.Y += 0.1f;

                Main.Dust[i]._velocity.X *= 0.99f;
                Dust dust2 = Main.Dust[i];
                dust2.Rotation += Main.Dust[i]._velocity.X * 0.5f;
                Dust dust3 = Main.Dust[i];
                dust3.Scale -= 0.01f;
                if (Main.Dust[i].Position.Y > Main.screenPosition.Y + Main.ScreenHeight)
                    Main.Dust[i].Active = false;

                if (Main.Dust[i].Scale < 0.1)
                    Main.Dust[i].Active = false;
            }
    }
}
