namespace Terraria
{
    using System;
    using System.Runtime.InteropServices;
    using Microsoft.Xna.Framework;

    public class NPC
    {
        public bool active;
        private static int activeRangeX = (Main.screenWidth * 2);
        private static int activeRangeY = (Main.screenHeight * 2);
        private static int activeTime = 0x3e8;
        public double[] ai = new double[maxAI];
        public int aiAction = 0;
        public int aiStyle;
        public int alpha;
        public Color color;
        public int damage;
        private static int defaultMaxSpawns = 6;
        private static int defaultSpawnRate = 700;
        public int defense;
        public int direction = 1;
        public Rectangle frame;
        public double frameCounter;
        public static float gravity = 0.3f;
        public int height;
        public int[] immune = new int[0x10];
        public static int immuneTime = 20;
        public float knockBackResist = 1f;
        public int life;
        public int lifeMax;
        public static int maxAI = 10;
        public static float maxFallSpeed = 10f;
        private static int maxSpawns = defaultMaxSpawns;
        public string name;
        public Vector2 position;
        private static int safeRangeX = ((int)((Main.screenWidth / 0x10) * 0.55));
        private static int safeRangeY = ((int)((Main.screenHeight / 0x10) * 0.55));
        public float scale = 1f;
        public int soundHit;
        public int soundKilled;
        private static int spawnRangeX = ((int)((Main.screenWidth / 0x10) * 1.2));
        private static int spawnRangeY = ((int)((Main.screenHeight / 0x10) * 1.2));
        private static int spawnRate = defaultSpawnRate;
        private static int spawnSpaceX = 4;
        private static int spawnSpaceY = 4;
        public int target = -1;
        public Rectangle targetRect;
        public int timeLeft;
        public int type;
        public Vector2 velocity;
        public int width;

        public void AI()
        {
            if (this.aiStyle == 0)
            {
                this.velocity.X *= 0.93f;
                if ((this.velocity.X > -0.1) && (this.velocity.X < 0.1))
                {
                    this.velocity.X = 0f;
                }
            }
            else if (this.aiStyle == 1)
            {
                this.aiAction = 0;
                if (this.ai[2] == 0.0)
                {
                    this.ai[0] = -100.0;
                    this.ai[2] = 1.0;
                }
                if (this.velocity.Y == 0f)
                {
                    this.velocity.X *= 0.8f;
                    if ((this.velocity.X > -0.1) && (this.velocity.X < 0.1))
                    {
                        this.velocity.X = 0f;
                    }
                    this.ai[0]++;
                    if (this.ai[0] >= 0.0)
                    {
                        this.direction = this.FindTarget();
                        if (this.ai[1] == 2.0)
                        {
                            this.velocity.Y = -8f;
                            this.velocity.X += 3 * this.direction;
                            this.ai[0] = -100.0;
                            this.ai[1] = 0.0;
                        }
                        else
                        {
                            this.velocity.Y = -5.5f;
                            this.velocity.X += 2 * this.direction;
                            this.ai[0] = -60.0;
                            this.ai[1]++;
                        }
                    }
                    else if (this.ai[0] >= -30.0)
                    {
                        this.aiAction = 1;
                    }
                }
                else if (
                    (this.target >= 0)
                    && (
                        ((this.direction == 1) && (this.velocity.X < 3f))
                        || ((this.direction == -1) && (this.velocity.X > -3f))
                    )
                )
                {
                    if (
                        ((this.direction == -1) && (this.velocity.X < 0.1))
                        || ((this.direction == 1) && (this.velocity.X > -0.1))
                    )
                    {
                        this.velocity.X += 0.2f * this.direction;
                    }
                    else
                    {
                        this.velocity.X *= 0.93f;
                    }
                }
            }
        }

        public void CheckActive()
        {
            if (this.active)
            {
                bool flag = false;
                Rectangle rectangle = new Rectangle(
                    (((int)this.position.X) + (this.width / 2)) - activeRangeX,
                    (((int)this.position.Y) + (this.height / 2)) - activeRangeY,
                    activeRangeX * 2,
                    activeRangeY * 2
                );
                Rectangle rectangle2 = new Rectangle(
                    ((int)((this.position.X + (this.width / 2)) - (Main.screenWidth * 0.5)))
                        - this.width,
                    ((int)((this.position.Y + (this.height / 2)) - (Main.screenHeight * 0.5)))
                        - this.height,
                    Main.screenWidth + (this.width * 2),
                    Main.screenHeight + (this.height * 2)
                );
                for (int i = 0; i < 0x10; i++)
                {
                    if (Main.player[i].active)
                    {
                        if (
                            rectangle.Intersects(
                                new Rectangle(
                                    (int)Main.player[i].position.X,
                                    (int)Main.player[i].position.Y,
                                    Main.player[i].width,
                                    Main.player[i].height
                                )
                            )
                        )
                        {
                            flag = true;
                            Player player1 = Main.player[i];
                            player1.activeNPCs++;
                        }
                        if (
                            rectangle2.Intersects(
                                new Rectangle(
                                    (int)Main.player[i].position.X,
                                    (int)Main.player[i].position.Y,
                                    Main.player[i].width,
                                    Main.player[i].height
                                )
                            )
                        )
                        {
                            this.timeLeft = activeTime;
                        }
                    }
                }
                this.timeLeft--;
                if (this.timeLeft <= 0)
                {
                    flag = false;
                }
                if (!flag)
                {
                    this.active = false;
                }
            }
        }

        public void FindFrame()
        {
            int num = Main.npcTexture[this.type].Height / Main.npcFrameCount[this.type];
            int num2 = 0;
            if (this.aiAction == 0)
            {
                if (this.velocity.Y < 0f)
                {
                    num2 = 2;
                }
                else if (this.velocity.Y > 0f)
                {
                    num2 = 3;
                }
                else if (!(this.velocity.X == 0f))
                {
                    num2 = 1;
                }
                else
                {
                    num2 = 0;
                }
            }
            else if (this.aiAction == 1)
            {
                num2 = 4;
            }
            if (this.type == 1)
            {
                this.frameCounter++;
                if (num2 > 0)
                {
                    this.frameCounter++;
                }
                if (num2 == 4)
                {
                    this.frameCounter++;
                }
                if (this.frameCounter >= 8.0)
                {
                    this.frame.Y += num;
                    this.frameCounter = 0.0;
                }
                if (this.frame.Y >= (num * Main.npcFrameCount[this.type]))
                {
                    this.frame.Y = 0;
                }
            }
        }

        public int FindTarget()
        {
            if (this.target == -1)
            {
                int num = -1;
                for (int i = 0; i < 0x10; i++)
                {
                    if (
                        (Main.player[i].active && !Main.player[i].dead)
                        && (
                            (num == -1)
                            || (
                                (
                                    Math.Abs(
                                        (float)(
                                            (
                                                (
                                                    Main.player[i].position.X
                                                    + (Main.player[i].width / 2)
                                                ) - this.position.X
                                            ) + (this.width / 2)
                                        )
                                    )
                                    + Math.Abs(
                                        (float)(
                                            (
                                                (
                                                    Main.player[i].position.Y
                                                    + (Main.player[i].height / 2)
                                                ) - this.position.Y
                                            ) + (this.height / 2)
                                        )
                                    )
                                ) < num
                            )
                        )
                    )
                    {
                        this.target = i;
                    }
                }
            }
            if (this.target == -1)
            {
                this.target = 0;
            }
            this.targetRect = new Rectangle(
                (int)Main.player[this.target].position.X,
                (int)Main.player[this.target].position.Y,
                Main.player[this.target].width,
                Main.player[this.target].height
            );
            if (
                (this.targetRect.X + (this.targetRect.Width / 2))
                < (this.position.X + (this.width / 2))
            )
            {
                return -1;
            }
            return 1;
        }

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

        public void HitEffect(int hitDirection = 0, double dmg = 10.0)
        {
            if (this.type == 1)
            {
                int num;
                if (this.life > 0)
                {
                    for (num = 0; num < ((dmg / ((double)this.lifeMax)) * 100.0); num++)
                    {
                        Dust.NewDust(
                            this.position,
                            this.width,
                            this.height,
                            4,
                            (float)hitDirection,
                            -1f,
                            this.alpha,
                            this.color
                        );
                    }
                }
                else
                {
                    for (num = 0; num < 50; num++)
                    {
                        Dust.NewDust(
                            this.position,
                            this.width,
                            this.height,
                            4,
                            (float)(2 * hitDirection),
                            -2f,
                            this.alpha,
                            this.color
                        );
                    }
                }
            }
        }

        public static int NewNPC(int X, int Y, int Type)
        {
            int index = -1;
            for (int i = 0; i < 0x3e8; i++)
            {
                if (!Main.npc[i].active)
                {
                    index = i;
                    break;
                }
            }
            if (index >= 0)
            {
                Main.npc[index] = new NPC();
                Main.npc[index].SetDefaults(Type);
                Main.npc[index].position.X = X - (Main.npc[index].width / 2);
                Main.npc[index].position.Y = Y - Main.npc[index].height;
                Main.npc[index].active = true;
                Main.npc[index].timeLeft = activeTime;
                return index;
            }
            return 0x3e9;
        }

        public void NPCLoot()
        {
            int type = 0;
            if ((this.type == 1) && (Main.rand.Next(3) <= 1))
            {
                type = 0x17;
            }
            if (type > 0)
            {
                int index = Item.NewItem(
                    (int)this.position.X,
                    (int)this.position.Y,
                    this.width,
                    this.height,
                    type
                );
                if ((this.type == 1) && (type == 0x17))
                {
                    Main.item[index].color = this.color;
                    Main.item[index].alpha = this.alpha;
                }
            }
        }

        public void SetDefaults(int Type)
        {
            this.active = true;
            this.alpha = 0;
            this.color = new Color();
            this.frameCounter = 0.0;
            this.knockBackResist = 1f;
            this.scale = 1f;
            this.soundHit = 0;
            this.soundKilled = 0;
            this.target = -1;
            this.targetRect = new Rectangle();
            this.timeLeft = activeTime;
            this.type = Type;
            for (int i = 0; i < maxAI; i++)
            {
                this.ai[i] = 0.0;
            }
            if (this.type == 1)
            {
                this.name = "Blue Slime";
                this.width = 0x18;
                this.height = 0x12;
                this.aiStyle = 1;
                this.damage = 7;
                this.defense = 10;
                this.lifeMax = 30;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.alpha = 0xaf;
                this.color = new Color(0, 80, 0xff, 100);
            }
            this.frame = new Rectangle(
                0,
                0,
                Main.npcTexture[this.type].Width,
                Main.npcTexture[this.type].Height / Main.npcFrameCount[this.type]
            );
            this.width = (int)(this.width * this.scale);
            this.height = (int)(this.height * this.scale);
            this.life = this.lifeMax;
            if (Main.dumbAI)
            {
                this.aiStyle = 0;
            }
        }

        public static void SpawnNPC()
        {
            bool flag = false;
            int num = 0;
            int num2 = 0;
            int type = 0;
            for (int i = 0; i < 0x10; i++)
            {
                spawnRate = defaultSpawnRate;
                maxSpawns = defaultMaxSpawns;
                if (!Main.dayTime)
                {
                    spawnRate = (int)(spawnRate * 1.8);
                    maxSpawns = (int)(maxSpawns * 1.8f);
                }
                if (Main.player[i].position.Y > ((Main.worldSurface * 16.0) + Main.screenHeight))
                {
                    spawnRate = (int)(spawnRate * 1.2);
                    maxSpawns = (int)(maxSpawns * 1.2f);
                }
                if (
                    (
                        (Main.player[i].active && !Main.player[i].dead)
                        && (Main.player[i].activeNPCs < maxSpawns)
                    ) && (Main.rand.Next(spawnRate) == 0)
                )
                {
                    int minValue = ((int)(Main.player[i].position.X / 16f)) - spawnRangeX;
                    int maxValue = ((int)(Main.player[i].position.X / 16f)) + spawnRangeX;
                    int num7 = ((int)(Main.player[i].position.Y / 16f)) - spawnRangeY;
                    int num8 = ((int)(Main.player[i].position.Y / 16f)) + spawnRangeY;
                    int num9 = ((int)(Main.player[i].position.X / 16f)) - safeRangeX;
                    int num10 = ((int)(Main.player[i].position.X / 16f)) + safeRangeX;
                    int num11 = ((int)(Main.player[i].position.Y / 16f)) - safeRangeY;
                    int num12 = ((int)(Main.player[i].position.Y / 16f)) + safeRangeY;
                    if (minValue < 0)
                    {
                        minValue = 0;
                    }
                    if (maxValue > 0x1389)
                    {
                        maxValue = 0x1389;
                    }
                    if (num7 < 0)
                    {
                        num7 = 0;
                    }
                    if (num8 > 0x9c5)
                    {
                        num8 = 0x9c5;
                    }
                    for (int j = 0; j < spawnRate; j++)
                    {
                        int num14 = Main.rand.Next(minValue, maxValue);
                        int num15 = Main.rand.Next(num7, num8);
                        if (
                            !Main.tile[num14, num15].active
                            || !Main.tileSolid[Main.tile[num14, num15].type]
                        )
                        {
                            if (Main.tile[num14, num15].wall == 1)
                            {
                                goto Label_04BC;
                            }
                            int num16 = num15;
                            while (num16 < 0x9c5)
                            {
                                if (
                                    Main.tile[num14, num16].active
                                    && Main.tileSolid[Main.tile[num14, num16].type]
                                )
                                {
                                    if (
                                        (((num14 < num9) || (num14 > num10)) || (num16 < num11))
                                        || (num16 > num12)
                                    )
                                    {
                                        type = Main.tile[num14, num16].type;
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
                                int num17 = num - (spawnSpaceX / 2);
                                int num18 = num + (spawnSpaceX / 2);
                                int num19 = num2 - spawnSpaceY;
                                int num20 = num2;
                                if (num17 < 0)
                                {
                                    flag = false;
                                }
                                if (num18 > 0x1389)
                                {
                                    flag = false;
                                }
                                if (num19 < 0)
                                {
                                    flag = false;
                                }
                                if (num20 > 0x9c5)
                                {
                                    flag = false;
                                }
                                if (flag)
                                {
                                    for (int k = num17; k < num18; k++)
                                    {
                                        for (num16 = num19; num16 < num20; num16++)
                                        {
                                            if (
                                                Main.tile[k, num16].active
                                                && Main.tileSolid[Main.tile[k, num16].type]
                                            )
                                            {
                                                flag = false;
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        if (flag || flag)
                        {
                            break;
                        }
                        Label_04BC:
                        ;
                    }
                }
                if (flag)
                {
                    int num22;
                    if (num2 <= Main.worldSurface)
                    {
                        if (Main.dayTime)
                        {
                            num22 = NewNPC((num * 0x10) + 8, num2 * 0x10, 1);
                            if (Main.rand.Next(3) == 0)
                            {
                                Main.npc[num22].name = "Green Slime";
                                Main.npc[num22].scale = 0.9f;
                                Main.npc[num22].damage = 8;
                                Main.npc[num22].defense = 7;
                                Main.npc[num22].life = 0x19;
                                Main.npc[num22].knockBackResist = 1.2f;
                                Main.npc[num22].lifeMax = Main.npc[num22].life;
                                Main.npc[num22].color = new Color(0, 0xff, 30, 100);
                            }
                        }
                        else
                        {
                            num22 = NewNPC((num * 0x10) + 8, num2 * 0x10, 1);
                            if (Main.rand.Next(2) == 0)
                            {
                                Main.npc[num22].name = "Purple Slime";
                                Main.npc[num22].scale = 1.2f;
                                Main.npc[num22].damage = 13;
                                Main.npc[num22].defense = 15;
                                Main.npc[num22].life = 0x2d;
                                Main.npc[num22].knockBackResist = 0.9f;
                                Main.npc[num22].lifeMax = Main.npc[num22].life;
                                Main.npc[num22].color = new Color(200, 0, 0xff, 150);
                            }
                        }
                    }
                    else if (Main.dayTime)
                    {
                        num22 = NewNPC((num * 0x10) + 8, num2 * 0x10, 1);
                        Main.npc[num22].name = "Red Slime";
                        Main.npc[num22].damage = 12;
                        Main.npc[num22].defense = 10;
                        Main.npc[num22].life = 40;
                        Main.npc[num22].lifeMax = Main.npc[num22].life;
                        Main.npc[num22].color = new Color(0xff, 30, 0, 100);
                    }
                    else
                    {
                        num22 = NewNPC((num * 0x10) + 8, num2 * 0x10, 1);
                        Main.npc[num22].name = "Yellow Slime";
                        Main.npc[num22].scale = 1.2f;
                        Main.npc[num22].damage = 15;
                        Main.npc[num22].defense = 15;
                        Main.npc[num22].life = 50;
                        Main.npc[num22].lifeMax = Main.npc[num22].life;
                        Main.npc[num22].color = new Color(0xff, 200, 0, 100);
                    }
                    break;
                }
            }
        }

        public double StrikeNPC(int Damage, float knockBack, int hitDirection)
        {
            double dmg = Main.CalculateDamage(Damage, this.defense);
            if (dmg >= 1.0)
            {
                this.life -= (int)dmg;
                if (knockBack > 0f)
                {
                    this.velocity.Y = (-knockBack * 0.75f) * this.knockBackResist;
                    this.velocity.X = (knockBack * hitDirection) * this.knockBackResist;
                }
                this.HitEffect(hitDirection, dmg);
                if (this.soundHit > 0)
                {
                    Main.soundInstanceNPCHit[this.soundHit].Play();
                }
                if (this.life <= 0)
                {
                    if (this.soundKilled > 0)
                    {
                        Main.soundInstanceNPCKilled[this.soundKilled].Play();
                    }
                    this.NPCLoot();
                    this.active = false;
                }
                return dmg;
            }
            return 0.0;
        }

        public void UpdateNPC(int i)
        {
            if (this.active)
            {
                this.AI();
                for (int j = 0; j < 0x10; j++)
                {
                    if (this.immune[j] > 0)
                    {
                        this.immune[j]--;
                    }
                }
                this.velocity.Y += gravity;
                if (this.velocity.Y > maxFallSpeed)
                {
                    this.velocity.Y = maxFallSpeed;
                }
                if ((this.velocity.X < 0.1) && (this.velocity.X > -0.1))
                {
                    this.velocity.X = 0f;
                }
                this.velocity = Collision.TileCollision(
                    this.position,
                    this.velocity,
                    this.width,
                    this.height
                );
                this.position += this.velocity;
                this.FindFrame();
                this.CheckActive();
            }
        }
    }
}
