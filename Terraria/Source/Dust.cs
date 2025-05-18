namespace Terraria
{
    using System;
    using System.Runtime.InteropServices;
    using Microsoft.Xna.Framework;

    public class Dust
    {
        public bool active = false;
        public int alpha;
        public Color color;
        public Rectangle frame;
        public Vector2 position;
        public float rotation;
        public float scale;
        public int type = 0;
        public Vector2 velocity;

        public Color GetAlpha(Color newColor)
        {
            int r = newColor.R - this.alpha;
            int g = newColor.G - this.alpha;
            int b = newColor.B - this.alpha;
            int a = newColor.A - this.alpha;
            if (a < 0)
            {
                a = 0;
            }
            if (a > 0xff)
            {
                a = 0xff;
            }
            return new Color(r, g, b, a);
        }

        public Color GetColor(Color newColor)
        {
            int r = this.color.R - (0xff - newColor.R);
            int g = this.color.G - (0xff - newColor.G);
            int b = this.color.B - (0xff - newColor.B);
            int a = this.color.A - (0xff - newColor.A);
            if (r < 0)
            {
                r = 0;
            }
            if (r > 0xff)
            {
                r = 0xff;
            }
            if (g < 0)
            {
                g = 0;
            }
            if (g > 0xff)
            {
                g = 0xff;
            }
            if (b < 0)
            {
                b = 0;
            }
            if (b > 0xff)
            {
                b = 0xff;
            }
            if (a < 0)
            {
                a = 0;
            }
            if (a > 0xff)
            {
                a = 0xff;
            }
            return new Color(r, g, b, a);
        }

        public static void NewDust(
            Vector2 Position,
            int Width,
            int Height,
            int Type,
            float SpeedX = 0f,
            float SpeedY = 0f,
            int Alpha = 0,
            Color newColor = new Color()
        )
        {
            for (int i = 0; i < 0x3e8; i++)
            {
                if (!Main.dust[i].active)
                {
                    Main.dust[i].active = true;
                    Main.dust[i].type = Type;
                    Main.dust[i].color = newColor;
                    Main.dust[i].alpha = Alpha;
                    Main.dust[i].position.X = (Position.X + Main.rand.Next(Width - 4)) + 4f;
                    Main.dust[i].position.Y = (Position.Y + Main.rand.Next(Height - 4)) + 4f;
                    Main.dust[i].velocity.X = (Main.rand.Next(-20, 0x15) * 0.1f) + SpeedX;
                    Main.dust[i].velocity.Y = (Main.rand.Next(-20, 0x15) * 0.1f) + SpeedY;
                    Main.dust[i].frame.X = 10 * Type;
                    Main.dust[i].frame.Y = 10 * Main.rand.Next(3);
                    Main.dust[i].frame.Width = 8;
                    Main.dust[i].frame.Height = 8;
                    Main.dust[i].rotation = 0f;
                    Main.dust[i].scale = 1f + (Main.rand.Next(-20, 0x15) * 0.01f);
                    if (Main.dust[i].type == 6)
                    {
                        Main.dust[i].velocity.Y = Main.rand.Next(-10, 6) * 0.1f;
                        Main.dust[i].velocity.X *= 0.3f;
                        Dust dust1 = Main.dust[i];
                        dust1.scale *= 0.7f;
                    }
                    break;
                }
            }
        }

        public static void UpdateDust()
        {
            for (int i = 0; i < 0x3e8; i++)
            {
                if (Main.dust[i].active)
                {
                    Dust dust1 = Main.dust[i];
                    dust1.position += Main.dust[i].velocity;
                    if (Main.dust[i].type == 6)
                    {
                        Main.dust[i].velocity.Y += 0.05f;
                    }
                    else
                    {
                        Main.dust[i].velocity.Y += 0.1f;
                    }
                    Main.dust[i].velocity.X *= 0.99f;
                    Dust dust2 = Main.dust[i];
                    dust2.rotation += Main.dust[i].velocity.X * 0.5f;
                    Dust dust3 = Main.dust[i];
                    dust3.scale -= 0.01f;
                    if (Main.dust[i].position.Y > (Main.screenPosition.Y + Main.screenHeight))
                    {
                        Main.dust[i].active = false;
                    }
                    if (Main.dust[i].scale < 0.1)
                    {
                        Main.dust[i].active = false;
                    }
                }
            }
        }
    }
}
