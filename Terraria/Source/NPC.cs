using Microsoft.Xna.Framework;

namespace Terraria;

public class Npc
{
    private static readonly int DefaultSpawnRate = 700;
    private static readonly int DefaultMaxSpawns = 6;
    private static int maxSpawns = DefaultMaxSpawns;
    private static int spawnRate = DefaultSpawnRate;
    private static readonly float Gravity = 0.3f;
    private static readonly float MaxFallSpeed = 10f;
    private static readonly int ActiveRangeX = Main.ScreenWidth * 2;
    private static readonly int ActiveRangeY = Main.ScreenHeight * 2;
    private static readonly int ActiveTime = 0x3e8;
    private static readonly int MaxAi = 10;
    private static readonly int SafeRangeX = (int)(Main.ScreenWidth / 0x10 * 0.55);
    private static readonly int SafeRangeY = (int)(Main.ScreenHeight / 0x10 * 0.55);
    private static readonly int SpawnRangeX = (int)(Main.ScreenWidth / 0x10 * 1.2);
    private static readonly int SpawnRangeY = (int)(Main.ScreenHeight / 0x10 * 1.2);
    private static readonly int SpawnSpaceX = 4;
    private static readonly int SpawnSpaceY = 4;
    public static int immuneTime = 20;
    private readonly double[] _ai = new double[MaxAi];
    public readonly int[] Immune = new int[0x10];
    private int _aiAction;
    private int _aiStyle;
    private int _alpha;
    private int _direction = 1;

    private double _frameCounter;
    private float _knockBackResist = 1f;
    private int _soundHit;
    private int _soundKilled;
    private int _target = -1;
    private Rectangle _targetRect;
    private int _timeLeft;
    private Vector2 _velocity;
    public bool Active;
    public Color Color;
    public int Damage;
    public int Defense;
    public Rectangle Frame;
    public int Height;
    public int Life;
    public int LifeMax;
    public string Name;
    public Vector2 Position;
    public float Scale = 1f;
    public int Type;
    public int Width;

    private void Ai()
    {
        if (_aiStyle == 0)
        {
            _velocity.X *= 0.93f;
            if (_velocity.X > -0.1 && _velocity.X < 0.1)
                _velocity.X = 0f;
        }
        else if (_aiStyle == 1)
        {
            _aiAction = 0;
            if (_ai[2] == 0.0)
            {
                _ai[0] = -100.0;
                _ai[2] = 1.0;
            }

            if (_velocity.Y == 0f)
            {
                _velocity.X *= 0.8f;
                if (_velocity.X > -0.1 && _velocity.X < 0.1)
                    _velocity.X = 0f;

                _ai[0]++;
                if (_ai[0] >= 0.0)
                {
                    _direction = FindTarget();
                    if (_ai[1] == 2.0)
                    {
                        _velocity.Y = -8f;
                        _velocity.X += 3 * _direction;
                        _ai[0] = -100.0;
                        _ai[1] = 0.0;
                    }
                    else
                    {
                        _velocity.Y = -5.5f;
                        _velocity.X += 2 * _direction;
                        _ai[0] = -60.0;
                        _ai[1]++;
                    }
                }
                else if (_ai[0] >= -30.0)
                    _aiAction = 1;
            }
            else if (
                _target >= 0
                && (
                    (_direction == 1 && _velocity.X < 3f)
                    || (_direction == -1 && _velocity.X > -3f)
                )
            )
            {
                if (
                    (_direction == -1 && _velocity.X < 0.1)
                    || (_direction == 1 && _velocity.X > -0.1)
                )
                    _velocity.X += 0.2f * _direction;
                else
                    _velocity.X *= 0.93f;
            }
        }
    }

    private void CheckActive()
    {
        if (Active)
        {
            var flag = false;
            Rectangle rectangle = new Rectangle(
                (int)Position.X + Width / 2 - ActiveRangeX,
                (int)Position.Y + Height / 2 - ActiveRangeY,
                ActiveRangeX * 2,
                ActiveRangeY * 2
            );

            Rectangle rectangle2 = new Rectangle(
                (int)(Position.X + Width / 2 - Main.ScreenWidth * 0.5)
                - Width,
                (int)(Position.Y + Height / 2 - Main.ScreenHeight * 0.5)
                - Height,
                Main.ScreenWidth + Width * 2,
                Main.ScreenHeight + Height * 2
            );

            for (var i = 0; i < 0x10; i++)
                if (Main.Player[i].Active)
                {
                    if (
                        rectangle.Intersects(
                            new Rectangle(
                                (int)Main.Player[i].Position.X,
                                (int)Main.Player[i].Position.Y,
                                Main.Player[i].Width,
                                Main.Player[i].Height
                            )
                        )
                    )
                    {
                        flag = true;
                        Player player1 = Main.Player[i];
                        player1.ActiveNpCs++;
                    }

                    if (
                        rectangle2.Intersects(
                            new Rectangle(
                                (int)Main.Player[i].Position.X,
                                (int)Main.Player[i].Position.Y,
                                Main.Player[i].Width,
                                Main.Player[i].Height
                            )
                        )
                    )
                        _timeLeft = ActiveTime;
                }

            _timeLeft--;
            if (_timeLeft <= 0)
                flag = false;

            if (!flag)
                Active = false;
        }
    }

    private void FindFrame()
    {
        var num = Main.NpcTexture[Type].Height / Main.NpcFrameCount[Type];
        var num2 = 0;
        if (_aiAction == 0)
        {
            if (_velocity.Y < 0f)
                num2 = 2;
            else if (_velocity.Y > 0f)
                num2 = 3;
            else if (_velocity.X != 0f)
                num2 = 1;
            else
                num2 = 0;
        }
        else if (_aiAction == 1)
            num2 = 4;

        if (Type == 1)
        {
            _frameCounter++;
            if (num2 > 0)
                _frameCounter++;

            if (num2 == 4)
                _frameCounter++;

            if (_frameCounter >= 8.0)
            {
                Frame.Y += num;
                _frameCounter = 0.0;
            }

            if (Frame.Y >= num * Main.NpcFrameCount[Type])
                Frame.Y = 0;
        }
    }

    private int FindTarget()
    {
        if (_target == -1)
        {
            for (var i = 0; i < 0x10; i++)
                if (
                    Main.Player[i].Active && !Main.Player[i].Dead
                )
                    _target = i;
        }

        if (_target == -1)
            _target = 0;

        _targetRect = new Rectangle(
            (int)Main.Player[_target].Position.X,
            (int)Main.Player[_target].Position.Y,
            Main.Player[_target].Width,
            Main.Player[_target].Height
        );

        if (
            _targetRect.X + _targetRect.Width / 2
            < Position.X + Width / 2
        )
            return -1;

        return 1;
    }

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

    private void HitEffect(int hitDirection = 0, double dmg = 10.0)
    {
        if (Type == 1)
        {
            int num;
            if (Life > 0)
            {
                for (num = 0; num < dmg / LifeMax * 100.0; num++)
                    Dust.NewDust(
                        Position,
                        Width,
                        Height,
                        4,
                        hitDirection,
                        -1f,
                        _alpha,
                        Color
                    );
            }
            else
            {
                for (num = 0; num < 50; num++)
                    Dust.NewDust(
                        Position,
                        Width,
                        Height,
                        4,
                        2 * hitDirection,
                        -2f,
                        _alpha,
                        Color
                    );
            }
        }
    }

    public static int NewNpc(int x, int y, int type)
    {
        var index = -1;
        for (var i = 0; i < 0x3e8; i++)
            if (!Main.Npc[i].Active)
            {
                index = i;
                break;
            }

        if (index >= 0)
        {
            Main.Npc[index] = new Npc();
            Main.Npc[index].SetDefaults(type);
            Main.Npc[index].Position.X = x - Main.Npc[index].Width / 2;
            Main.Npc[index].Position.Y = y - Main.Npc[index].Height;
            Main.Npc[index].Active = true;
            Main.Npc[index]._timeLeft = ActiveTime;
            return index;
        }

        return 0x3e9;
    }

    private void NpcLoot()
    {
        var type = 0;
        if (Type == 1 && Main.Rand.Next(3) <= 1)
            type = 0x17;

        if (type > 0)
        {
            var index = Item.NewItem(
                (int)Position.X,
                (int)Position.Y,
                Width,
                Height,
                type
            );

            if (Type == 1)
            {
                Main.Item[index].Color = Color;
                Main.Item[index].Alpha = _alpha;
            }
        }
    }

    private void SetDefaults(int type)
    {
        Active = true;
        _alpha = 0;
        Color = new Color();
        _frameCounter = 0.0;
        _knockBackResist = 1f;
        Scale = 1f;
        _soundHit = 0;
        _soundKilled = 0;
        _target = -1;
        _targetRect = new Rectangle();
        _timeLeft = ActiveTime;
        Type = type;
        for (var i = 0; i < MaxAi; i++)
            _ai[i] = 0.0;

        if (Type == 1)
        {
            Name = "Blue Slime";
            Width = 0x18;
            Height = 0x12;
            _aiStyle = 1;
            Damage = 7;
            Defense = 10;
            LifeMax = 30;
            _soundHit = 1;
            _soundKilled = 1;
            _alpha = 0xaf;
            Color = new Color(0, 80, 0xff, 100);
        }

        Frame = new Rectangle(
            0,
            0,
            Main.NpcTexture[Type].Width,
            Main.NpcTexture[Type].Height / Main.NpcFrameCount[Type]
        );

        Width = (int)(Width * Scale);
        Height = (int)(Height * Scale);
        Life = LifeMax;
        if (Main.DumbAi)
            _aiStyle = 0;
    }

    public static void SpawnNpc()
    {
        var flag = false;
        var num = 0;
        var num2 = 0;
        for (var i = 0; i < 0x10; i++)
        {
            spawnRate = DefaultSpawnRate;
            maxSpawns = DefaultMaxSpawns;
            if (!Main.dayTime)
            {
                spawnRate = (int)(spawnRate * 1.8);
                maxSpawns = (int)(maxSpawns * 1.8f);
            }

            if (Main.Player[i].Position.Y > Main.worldSurface * 16.0 + Main.ScreenHeight)
            {
                spawnRate = (int)(spawnRate * 1.2);
                maxSpawns = (int)(maxSpawns * 1.2f);
            }

            if (
                Main.Player[i].Active && !Main.Player[i].Dead
                                      && Main.Player[i].ActiveNpCs < maxSpawns && Main.Rand.Next(spawnRate) == 0
            )
            {
                var minValue = (int)(Main.Player[i].Position.X / 16f) - SpawnRangeX;
                var maxValue = (int)(Main.Player[i].Position.X / 16f) + SpawnRangeX;
                var num7 = (int)(Main.Player[i].Position.Y / 16f) - SpawnRangeY;
                var num8 = (int)(Main.Player[i].Position.Y / 16f) + SpawnRangeY;
                var num9 = (int)(Main.Player[i].Position.X / 16f) - SafeRangeX;
                var num10 = (int)(Main.Player[i].Position.X / 16f) + SafeRangeX;
                var num11 = (int)(Main.Player[i].Position.Y / 16f) - SafeRangeY;
                var num12 = (int)(Main.Player[i].Position.Y / 16f) + SafeRangeY;
                if (minValue < 0)
                    minValue = 0;

                if (maxValue > 0x1389)
                    maxValue = 0x1389;

                if (num7 < 0)
                    num7 = 0;

                if (num8 > 0x9c5)
                    num8 = 0x9c5;

                for (var j = 0; j < spawnRate; j++)
                {
                    var num14 = Main.Rand.Next(minValue, maxValue);
                    var num15 = Main.Rand.Next(num7, num8);
                    if (
                        !Main.Tile[num14, num15].Active
                        || !Main.TileSolid[Main.Tile[num14, num15].Type]
                    )
                    {
                        if (Main.Tile[num14, num15].Wall == 1)
                            goto Label_04BC;

                        var num16 = num15;
                        while (num16 < 0x9c5)
                        {
                            if (
                                Main.Tile[num14, num16].Active
                                && Main.TileSolid[Main.Tile[num14, num16].Type]
                            )
                            {
                                if (
                                    num14 < num9 || num14 > num10 || num16 < num11
                                    || num16 > num12
                                )
                                {
                                    num = num14;
                                    num2 = num16;
                                    flag = true;
                                }

                                break;
                            }

                            num16++;
                        }

                        if (flag)
                        {
                            var num17 = num - SpawnSpaceX / 2;
                            var num18 = num + SpawnSpaceX / 2;
                            var num19 = num2 - SpawnSpaceY;
                            var num20 = num2;
                            if (num17 < 0)
                                flag = false;

                            if (num18 > 0x1389)
                                flag = false;

                            if (num19 < 0)
                                flag = false;

                            if (flag)
                            {
                                for (var k = num17; k < num18; k++)
                                for (num16 = num19; num16 < num20; num16++)
                                    if (
                                        Main.Tile[k, num16].Active
                                        && Main.TileSolid[Main.Tile[k, num16].Type]
                                    )
                                    {
                                        flag = false;
                                        break;
                                    }
                            }
                        }
                    }

                    if (flag)
                        break;

                    Label_04BC: ;
                }
            }

            if (flag)
            {
                int num22;
                if (num2 <= Main.worldSurface)
                {
                    if (Main.dayTime)
                    {
                        num22 = NewNpc(num * 0x10 + 8, num2 * 0x10, 1);
                        if (Main.Rand.Next(3) == 0)
                        {
                            Main.Npc[num22].Name = "Green Slime";
                            Main.Npc[num22].Scale = 0.9f;
                            Main.Npc[num22].Damage = 8;
                            Main.Npc[num22].Defense = 7;
                            Main.Npc[num22].Life = 0x19;
                            Main.Npc[num22]._knockBackResist = 1.2f;
                            Main.Npc[num22].LifeMax = Main.Npc[num22].Life;
                            Main.Npc[num22].Color = new Color(0, 0xff, 30, 100);
                        }
                    }
                    else
                    {
                        num22 = NewNpc(num * 0x10 + 8, num2 * 0x10, 1);
                        if (Main.Rand.Next(2) == 0)
                        {
                            Main.Npc[num22].Name = "Purple Slime";
                            Main.Npc[num22].Scale = 1.2f;
                            Main.Npc[num22].Damage = 13;
                            Main.Npc[num22].Defense = 15;
                            Main.Npc[num22].Life = 0x2d;
                            Main.Npc[num22]._knockBackResist = 0.9f;
                            Main.Npc[num22].LifeMax = Main.Npc[num22].Life;
                            Main.Npc[num22].Color = new Color(200, 0, 0xff, 150);
                        }
                    }
                }
                else if (Main.dayTime)
                {
                    num22 = NewNpc(num * 0x10 + 8, num2 * 0x10, 1);
                    Main.Npc[num22].Name = "Red Slime";
                    Main.Npc[num22].Damage = 12;
                    Main.Npc[num22].Defense = 10;
                    Main.Npc[num22].Life = 40;
                    Main.Npc[num22].LifeMax = Main.Npc[num22].Life;
                    Main.Npc[num22].Color = new Color(0xff, 30, 0, 100);
                }
                else
                {
                    num22 = NewNpc(num * 0x10 + 8, num2 * 0x10, 1);
                    Main.Npc[num22].Name = "Yellow Slime";
                    Main.Npc[num22].Scale = 1.2f;
                    Main.Npc[num22].Damage = 15;
                    Main.Npc[num22].Defense = 15;
                    Main.Npc[num22].Life = 50;
                    Main.Npc[num22].LifeMax = Main.Npc[num22].Life;
                    Main.Npc[num22].Color = new Color(0xff, 200, 0, 100);
                }

                break;
            }
        }
    }

    public double StrikeNpc(int damage, float knockBack, int hitDirection)
    {
        var dmg = Main.CalculateDamage(damage, Defense);
        if (dmg >= 1.0)
        {
            Life -= (int)dmg;
            if (knockBack > 0f)
            {
                _velocity.Y = -knockBack * 0.75f * _knockBackResist;
                _velocity.X = knockBack * hitDirection * _knockBackResist;
            }

            HitEffect(hitDirection, dmg);
            if (_soundHit > 0)
                Main.SoundInstanceNpcHit[_soundHit].Play();

            if (Life <= 0)
            {
                if (_soundKilled > 0)
                    Main.SoundInstanceNpcKilled[_soundKilled].Play();

                NpcLoot();
                Active = false;
            }

            return dmg;
        }

        return 0.0;
    }

    public void UpdateNpc(int i)
    {
        if (Active)
        {
            Ai();
            for (var j = 0; j < 0x10; j++)
                if (Immune[j] > 0)
                    Immune[j]--;

            _velocity.Y += Gravity;
            if (_velocity.Y > MaxFallSpeed)
                _velocity.Y = MaxFallSpeed;

            if (_velocity.X < 0.1 && _velocity.X > -0.1)
                _velocity.X = 0f;

            _velocity = Collision.TileCollision(
                Position,
                _velocity,
                Width,
                Height
            );

            Position += _velocity;
            FindFrame();
            CheckActive();
        }
    }
}
