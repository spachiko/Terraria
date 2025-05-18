namespace Terraria
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;

    public class Player
    {
        public bool active;
        public int activeNPCs;
        public Item[] armor = new Item[3];
        public int body = 0;
        public Rectangle bodyFrame;
        public double bodyFrameCounter;
        public Vector2 bodyPosition;
        public float bodyRotation;
        public Vector2 bodyVelocity;
        public int changeItem = -1;
        public bool controlDown;
        public bool controlJump;
        public bool controlLeft;
        public bool controlRight;
        public bool controlUp;
        public bool controlUseItem;
        public bool controlUseTile;
        public bool dead = false;
        public bool delayUseItem;
        public int direction = 1;
        private static float gravity = 0.4f;
        public int head = Main.rand.Next(2);
        public Rectangle headFrame;
        public double headFrameCounter;
        public Vector2 headPosition;
        public float headRotation;
        public Vector2 headVelocity;
        public int height = 0x2a;
        public int hitTile;
        public int hitTileX;
        public int hitTileY;
        public bool immune;
        public int immuneAlpha;
        public int immuneAlphaDirection;
        public int immuneTime;
        public Item[] inventory = new Item[40];
        public int itemAnimation;
        public int itemAnimationMax;
        private static int itemGrabRange = 0x20;
        private static float itemGrabSpeed = 0.4f;
        private static float itemGrabSpeedMax = 4f;
        public int itemHeight;
        public Vector2 itemLocation;
        public float itemRotation;
        public int itemTime;
        public int itemWidth;
        public int jump;
        private static int jumpHeight = 15;
        private static float jumpSpeed = 5.01f;
        public Rectangle legFrame;
        public double legFrameCounter;
        public Vector2 legPosition;
        public float legRotation;
        public int legs = 0;
        public Vector2 legVelocity;
        private static float maxFallSpeed = 10f;
        private static float maxRunSpeed = 3f;
        public bool mouseInterface;
        public string name = "";
        public Vector2 position;
        public bool releaseInventory;
        public bool releaseJump;
        public bool releaseUseItem;
        public bool releaseUseTile;
        public int respawnTimer;
        private static float runAcceleration = 0.08f;
        private static float runSlowdown = 0.2f;
        public int selectedItem = 0;
        public bool showItemIcon = false;
        public int showItemIcon2 = 0;
        public int statAttack = 0;
        public int statDefense = 10;
        public int statLife = 100;
        public int statLifeMax = 100;
        private static int tileRangeX = 5;
        private static int tileRangeY = 4;
        private static int tileTargetX;
        private static int tileTargetY;
        public Vector2 velocity;
        public int width = 20;

        public void DropItems()
        {
            for (int i = 10; i < 40; i++)
            {
                int index = Item.NewItem(
                    (int)this.position.X,
                    (int)this.position.Y,
                    this.width,
                    this.height,
                    this.inventory[i].type
                );
                this.inventory[i].position = Main.item[index].position;
                Main.item[index] = this.inventory[i];
                this.inventory[i] = new Item();
                this.selectedItem = 0;
                Main.item[index].velocity.Y = Main.rand.Next(-20, 1) * 0.1f;
                Main.item[index].velocity.X = Main.rand.Next(-20, 0x15) * 0.1f;
                Main.item[index].noGrabDelay = 100;
            }
        }

        public Color GetDeathAlpha(Color newColor)
        {
            int r = newColor.R + ((int)(this.immuneAlpha * 0.9));
            int g = newColor.G + ((int)(this.immuneAlpha * 0.5));
            int b = newColor.B + ((int)(this.immuneAlpha * 0.5));
            int a = newColor.A + ((int)(this.immuneAlpha * 0.4));
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

        public Color GetImmuneAlpha(Color newColor)
        {
            int r = newColor.R - this.immuneAlpha;
            int g = newColor.G - this.immuneAlpha;
            int b = newColor.B - this.immuneAlpha;
            int a = newColor.A - this.immuneAlpha;
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

        public Item GetItem(int plr, Item newItem)
        {
            Item item = newItem;
            if (newItem.noGrabDelay <= 0)
            {
                int num;
                for (num = 0; num < 40; num++)
                {
                    if (
                        (
                            (this.inventory[num].type > 0)
                            && (this.inventory[num].stack < this.inventory[num].maxStack)
                        ) && item.IsTheSameAs(this.inventory[num])
                    )
                    {
                        Main.soundInstanceGrab.Stop();
                        Main.soundInstanceGrab = Main.soundGrab.CreateInstance();
                        Main.soundInstanceGrab.Play();
                        if (
                            (item.stack + this.inventory[num].stack) <= this.inventory[num].maxStack
                        )
                        {
                            Item item1 = this.inventory[num];
                            item1.stack += item.stack;
                            if (plr == Main.myPlayer)
                            {
                                Recipe.FindRecipes();
                            }
                            return new Item();
                        }
                        item.stack -= this.inventory[num].maxStack - this.inventory[num].stack;
                        this.inventory[num].stack = this.inventory[num].maxStack;
                        if (plr == Main.myPlayer)
                        {
                            Recipe.FindRecipes();
                        }
                    }
                }
                for (num = 0; num < 40; num++)
                {
                    if (this.inventory[num].type == 0)
                    {
                        this.inventory[num] = item;
                        Main.soundInstanceGrab.Stop();
                        Main.soundInstanceGrab = Main.soundGrab.CreateInstance();
                        Main.soundInstanceGrab.Play();
                        if (plr == Main.myPlayer)
                        {
                            Recipe.FindRecipes();
                        }
                        return new Item();
                    }
                }
            }
            return item;
        }

        public void Hurt(int Damage, int hitDirection)
        {
            if (!this.immune && !Main.godMode)
            {
                double num = Main.CalculateDamage(Damage, this.statDefense);
                if (num >= 1.0)
                {
                    int num3;
                    Color color;
                    this.statLife -= (int)num;
                    this.immune = true;
                    this.immuneTime = 40;
                    this.velocity.X = 4.5f * hitDirection;
                    this.velocity.Y = -3.5f;
                    int index = Main.rand.Next(3);
                    Main.soundInstancePlayerHit[index].Stop();
                    Main.soundInstancePlayerHit[index] = Main.soundPlayerHit[index]
                        .CreateInstance();
                    Main.soundInstancePlayerHit[index].Play();
                    if (this.statLife > 0)
                    {
                        for (num3 = 0; num3 < ((num / ((double)this.statLifeMax)) * 100.0); num3++)
                        {
                            color = new Color();
                            Dust.NewDust(
                                this.position,
                                this.width,
                                this.height,
                                5,
                                (float)(2 * hitDirection),
                                -2f,
                                0,
                                color
                            );
                        }
                    }
                    else
                    {
                        this.DropItems();
                        Main.soundPlayerKilled.Play();
                        this.headVelocity.Y = Main.rand.Next(-40, -10) * 0.1f;
                        this.bodyVelocity.Y = Main.rand.Next(-40, -10) * 0.1f;
                        this.legVelocity.Y = Main.rand.Next(-40, -10) * 0.1f;
                        this.headVelocity.X =
                            (Main.rand.Next(-20, 0x15) * 0.1f) + (2 * hitDirection);
                        this.bodyVelocity.X =
                            (Main.rand.Next(-20, 0x15) * 0.1f) + (2 * hitDirection);
                        this.legVelocity.X =
                            (Main.rand.Next(-20, 0x15) * 0.1f) + (2 * hitDirection);
                        for (
                            num3 = 0;
                            num3 < (20.0 + ((num / ((double)this.statLifeMax)) * 100.0));
                            num3++
                        )
                        {
                            color = new Color();
                            Dust.NewDust(
                                this.position,
                                this.width,
                                this.height,
                                5,
                                (float)(2 * hitDirection),
                                -2f,
                                0,
                                color
                            );
                        }
                        this.dead = true;
                        this.respawnTimer = 300;
                        this.immuneAlpha = 0;
                    }
                }
            }
        }

        public void ItemCheck(int i)
        {
            if (this.inventory[this.selectedItem].autoReuse)
            {
                this.releaseUseItem = true;
            }
            if (
                ((this.controlUseItem && (this.itemAnimation == 0)) && this.releaseUseItem)
                && (this.inventory[this.selectedItem].useStyle > 0)
            )
            {
                this.itemAnimation = this.inventory[this.selectedItem].useAnimation;
                this.itemAnimationMax = this.itemAnimation;
                if (this.inventory[this.selectedItem].useSound > 0)
                {
                    Main.soundInstanceItem[this.inventory[this.selectedItem].useSound].Play();
                }
            }
            if (this.itemAnimation > 0)
            {
                this.itemHeight = Main.itemTexture[this.inventory[this.selectedItem].type].Height;
                this.itemWidth = Main.itemTexture[this.inventory[this.selectedItem].type].Width;
                this.itemAnimation--;
                if (this.inventory[this.selectedItem].useStyle == 1)
                {
                    if (
                        this.itemAnimation
                        < (this.inventory[this.selectedItem].useAnimation * 0.333)
                    )
                    {
                        this.itemLocation.X =
                            (this.position.X + (this.width * 0.5f))
                            + (
                                (
                                    (
                                        Main.itemTexture[
                                            this.inventory[this.selectedItem].type
                                        ].Width * 0.5f
                                    ) - 4f
                                ) * this.direction
                            );
                        this.itemLocation.Y = this.position.Y + 24f;
                    }
                    else if (
                        this.itemAnimation
                        < (this.inventory[this.selectedItem].useAnimation * 0.666)
                    )
                    {
                        this.itemLocation.X =
                            (this.position.X + (this.width * 0.5f))
                            + (
                                (
                                    (
                                        Main.itemTexture[
                                            this.inventory[this.selectedItem].type
                                        ].Width * 0.5f
                                    ) - 10f
                                ) * this.direction
                            );
                        this.itemLocation.Y = this.position.Y + 10f;
                    }
                    else
                    {
                        this.itemLocation.X =
                            (this.position.X + (this.width * 0.5f))
                            - (
                                (
                                    (
                                        Main.itemTexture[
                                            this.inventory[this.selectedItem].type
                                        ].Width * 0.5f
                                    ) - 4f
                                ) * this.direction
                            );
                        this.itemLocation.Y = this.position.Y + 6f;
                    }
                    this.itemRotation =
                        (
                            (
                                (
                                    (
                                        ((float)this.itemAnimation)
                                        / ((float)this.inventory[this.selectedItem].useAnimation)
                                    ) - 0.5f
                                ) * -this.direction
                            ) * 3.5f
                        ) - (this.direction * 0.3f);
                }
                else if (this.inventory[this.selectedItem].useStyle == 2)
                {
                    this.itemRotation =
                        (
                            (
                                (
                                    ((float)this.itemAnimation)
                                    / ((float)this.inventory[this.selectedItem].useAnimation)
                                ) * this.direction
                            ) * 2f
                        ) + (-1.4f * this.direction);
                    if (this.itemAnimation < (this.inventory[this.selectedItem].useAnimation * 0.5))
                    {
                        this.itemLocation.X =
                            (this.position.X + (this.width * 0.5f))
                            + (
                                (
                                    (
                                        (
                                            Main.itemTexture[
                                                this.inventory[this.selectedItem].type
                                            ].Width * 0.5f
                                        ) - 9f
                                    ) - ((this.itemRotation * 12f) * this.direction)
                                ) * this.direction
                            );
                        this.itemLocation.Y =
                            (this.position.Y + 38f) + ((this.itemRotation * this.direction) * 4f);
                    }
                    else
                    {
                        this.itemLocation.X =
                            (this.position.X + (this.width * 0.5f))
                            + (
                                (
                                    (
                                        (
                                            Main.itemTexture[
                                                this.inventory[this.selectedItem].type
                                            ].Width * 0.5f
                                        ) - 9f
                                    ) - ((this.itemRotation * 16f) * this.direction)
                                ) * this.direction
                            );
                        this.itemLocation.Y =
                            (this.position.Y + 38f) + (this.itemRotation * this.direction);
                    }
                }
                else if (this.inventory[this.selectedItem].useStyle == 3)
                {
                    if (
                        this.itemAnimation
                        > (this.inventory[this.selectedItem].useAnimation * 0.666)
                    )
                    {
                        this.itemLocation.X = -1000f;
                        this.itemLocation.Y = -1000f;
                        this.itemRotation = -1.3f * this.direction;
                    }
                    else
                    {
                        this.itemLocation.X =
                            (this.position.X + (this.width * 0.5f))
                            + (
                                (
                                    (
                                        Main.itemTexture[
                                            this.inventory[this.selectedItem].type
                                        ].Width * 0.5f
                                    ) - 4f
                                ) * this.direction
                            );
                        this.itemLocation.Y = this.position.Y + 24f;
                        float num =
                            (
                                (
                                    (
                                        (
                                            (
                                                ((float)this.itemAnimation)
                                                / (
                                                    (float)
                                                        this.inventory[
                                                            this.selectedItem
                                                        ].useAnimation
                                                )
                                            )
                                            * Main.itemTexture[
                                                this.inventory[this.selectedItem].type
                                            ].Width
                                        ) * this.direction
                                    ) * this.inventory[this.selectedItem].scale
                                ) * 1.2f
                            ) - (10 * this.direction);
                        if ((num > -4f) && (this.direction == -1))
                        {
                            num = -4f;
                        }
                        if ((num < 4f) && (this.direction == 1))
                        {
                            num = 4f;
                        }
                        this.itemLocation.X -= num;
                        this.itemRotation = 0.8f * this.direction;
                    }
                }
            }
            else if (this.inventory[this.selectedItem].holdStyle == 1)
            {
                this.itemLocation.X =
                    (this.position.X + (this.width * 0.5f))
                    + (
                        (
                            (Main.itemTexture[this.inventory[this.selectedItem].type].Width * 0.5f)
                            + 4f
                        ) * this.direction
                    );
                this.itemLocation.Y = this.position.Y + 24f;
                this.itemRotation = 0f;
            }
            if (this.inventory[this.selectedItem].type == 8)
            {
                int maxValue = 20;
                if (this.itemAnimation > 0)
                {
                    maxValue = 7;
                }
                if (this.direction == -1)
                {
                    if (Main.rand.Next(maxValue) == 0)
                    {
                        Dust.NewDust(
                            new Vector2(this.itemLocation.X - 16f, this.itemLocation.Y - 14f),
                            4,
                            4,
                            6,
                            0f,
                            0f,
                            100,
                            new Color()
                        );
                    }
                    Lighting.addLight(
                        (int)(((this.itemLocation.X - 16f) + this.velocity.X) / 16f),
                        (int)((this.itemLocation.Y - 14f) / 16f),
                        0xff
                    );
                }
                else
                {
                    Main.screenPosition.X =
                        (this.position.X + (this.width * 0.5f)) - (Main.screenWidth * 0.5f);
                    Main.screenPosition.Y =
                        (this.position.Y + (this.height * 0.5f)) - (Main.screenHeight * 0.5f);
                    if (Main.rand.Next(maxValue) == 0)
                    {
                        Dust.NewDust(
                            new Vector2(this.itemLocation.X + 6f, this.itemLocation.Y - 14f),
                            4,
                            4,
                            6,
                            0f,
                            0f,
                            100,
                            new Color()
                        );
                    }
                    Lighting.addLight(
                        (int)(((this.itemLocation.X + 6f) + this.velocity.X) / 16f),
                        (int)((this.itemLocation.Y - 14f) / 16f),
                        0xff
                    );
                }
            }
            if (this.controlUseItem)
            {
                this.releaseUseItem = false;
            }
            else
            {
                this.releaseUseItem = true;
            }
            if (this.itemTime > 0)
            {
                this.itemTime--;
            }
            if (i == Main.myPlayer)
            {
                if (
                    (
                        (
                            (this.inventory[this.selectedItem].pick > 0)
                            || (this.inventory[this.selectedItem].axe > 0)
                        ) || (this.inventory[this.selectedItem].hammer > 0)
                    )
                    && (
                        (
                            (
                                (
                                    (
                                        ((this.position.X / 16f) - tileRangeX)
                                        - this.inventory[this.selectedItem].tileBoost
                                    ) <= tileTargetX
                                )
                                && (
                                    (
                                        (
                                            (((this.position.X + this.width) / 16f) + tileRangeX)
                                            + this.inventory[this.selectedItem].tileBoost
                                        ) - 1f
                                    ) >= tileTargetX
                                )
                            )
                            && (
                                (
                                    ((this.position.Y / 16f) - tileRangeY)
                                    - this.inventory[this.selectedItem].tileBoost
                                ) <= tileTargetY
                            )
                        )
                        && (
                            (
                                (
                                    (((this.position.Y + this.height) / 16f) + tileRangeY)
                                    + this.inventory[this.selectedItem].tileBoost
                                ) - 2f
                            ) >= tileTargetY
                        )
                    )
                )
                {
                    this.showItemIcon = true;
                    if (
                        Main.tile[tileTargetX, tileTargetY].active
                        && (
                            ((this.itemTime == 0) && (this.itemAnimation > 0))
                            && this.controlUseItem
                        )
                    )
                    {
                        if ((this.hitTileX != tileTargetX) || (this.hitTileY != tileTargetY))
                        {
                            this.hitTile = 0;
                            this.hitTileX = tileTargetX;
                            this.hitTileY = tileTargetY;
                        }
                        if (Main.tileNoFail[Main.tile[tileTargetX, tileTargetY].type])
                        {
                            this.hitTile = 100;
                        }
                        if (
                            (
                                (Main.tile[tileTargetX, tileTargetY].type == 5)
                                || (Main.tile[tileTargetX, tileTargetY].type == 10)
                            ) || (Main.tile[tileTargetX, tileTargetY].type == 11)
                        )
                        {
                            this.hitTile += this.inventory[this.selectedItem].axe;
                            if (this.inventory[this.selectedItem].axe > 0)
                            {
                                if (this.hitTile >= 100)
                                {
                                    this.hitTile = 0;
                                    WorldGen.KillTile(tileTargetX, tileTargetY, false);
                                }
                                else
                                {
                                    WorldGen.KillTile(tileTargetX, tileTargetY, true);
                                }
                                this.itemTime = this.inventory[this.selectedItem].useTime;
                            }
                        }
                        else if (this.inventory[this.selectedItem].pick > 0)
                        {
                            this.hitTile += this.inventory[this.selectedItem].pick;
                            if (this.hitTile >= 100)
                            {
                                this.hitTile = 0;
                                WorldGen.KillTile(tileTargetX, tileTargetY, false);
                            }
                            else
                            {
                                WorldGen.KillTile(tileTargetX, tileTargetY, true);
                            }
                            this.itemTime = this.inventory[this.selectedItem].useTime;
                        }
                    }
                    if (
                        (
                            (Main.tile[tileTargetX, tileTargetY].wall > 0)
                            && (
                                ((this.itemTime == 0) && (this.itemAnimation > 0))
                                && this.controlUseItem
                            )
                        ) && (this.inventory[this.selectedItem].hammer > 0)
                    )
                    {
                        if ((this.hitTileX != tileTargetX) || (this.hitTileY != tileTargetY))
                        {
                            this.hitTile = 0;
                            this.hitTileX = tileTargetX;
                            this.hitTileY = tileTargetY;
                        }
                        this.hitTile += this.inventory[this.selectedItem].hammer;
                        if (this.hitTile >= 100)
                        {
                            this.hitTile = 0;
                            WorldGen.KillWall(tileTargetX, tileTargetY, false);
                        }
                        else
                        {
                            WorldGen.KillWall(tileTargetX, tileTargetY, true);
                        }
                        this.itemTime = this.inventory[this.selectedItem].useTime;
                    }
                }
                if (this.inventory[this.selectedItem].createTile >= 0)
                {
                    tileTargetX = (int)((Main.mouseState.X + Main.screenPosition.X) / 16f);
                    tileTargetY = (int)((Main.mouseState.Y + Main.screenPosition.Y) / 16f);
                    if (
                        (
                            (
                                (
                                    (
                                        ((this.position.X / 16f) - tileRangeX)
                                        - this.inventory[this.selectedItem].tileBoost
                                    ) <= tileTargetX
                                )
                                && (
                                    (
                                        (
                                            (((this.position.X + this.width) / 16f) + tileRangeX)
                                            + this.inventory[this.selectedItem].tileBoost
                                        ) - 1f
                                    ) >= tileTargetX
                                )
                            )
                            && (
                                (
                                    ((this.position.Y / 16f) - tileRangeY)
                                    - this.inventory[this.selectedItem].tileBoost
                                ) <= tileTargetY
                            )
                        )
                        && (
                            (
                                (
                                    (((this.position.Y + this.height) / 16f) + tileRangeY)
                                    + this.inventory[this.selectedItem].tileBoost
                                ) - 2f
                            ) >= tileTargetY
                        )
                    )
                    {
                        this.showItemIcon = true;
                        if (
                            (
                                !Main.tile[tileTargetX, tileTargetY].active
                                && (
                                    ((this.itemTime == 0) && (this.itemAnimation > 0))
                                    && this.controlUseItem
                                )
                            )
                            && (
                                (
                                    (
                                        (
                                            Main.tile[tileTargetX + 1, tileTargetY].active
                                            || (Main.tile[tileTargetX + 1, tileTargetY].wall > 0)
                                        )
                                        || (
                                            Main.tile[tileTargetX - 1, tileTargetY].active
                                            || (Main.tile[tileTargetX - 1, tileTargetY].wall > 0)
                                        )
                                    )
                                    || (
                                        (
                                            Main.tile[tileTargetX, tileTargetY + 1].active
                                            || (Main.tile[tileTargetX, tileTargetY + 1].wall > 0)
                                        ) || Main.tile[tileTargetX, tileTargetY - 1].active
                                    )
                                ) || (Main.tile[tileTargetX, tileTargetY - 1].wall > 0)
                            )
                        )
                        {
                            WorldGen.PlaceTile(
                                tileTargetX,
                                tileTargetY,
                                this.inventory[this.selectedItem].createTile,
                                false
                            );
                            if (Main.tile[tileTargetX, tileTargetY].active)
                            {
                                this.itemTime = this.inventory[this.selectedItem].useTime;
                            }
                        }
                    }
                }
                if (this.inventory[this.selectedItem].createWall >= 0)
                {
                    tileTargetX = (int)((Main.mouseState.X + Main.screenPosition.X) / 16f);
                    tileTargetY = (int)((Main.mouseState.Y + Main.screenPosition.Y) / 16f);
                    if (
                        (
                            (
                                (
                                    (
                                        ((this.position.X / 16f) - tileRangeX)
                                        - this.inventory[this.selectedItem].tileBoost
                                    ) <= tileTargetX
                                )
                                && (
                                    (
                                        (
                                            (((this.position.X + this.width) / 16f) + tileRangeX)
                                            + this.inventory[this.selectedItem].tileBoost
                                        ) - 1f
                                    ) >= tileTargetX
                                )
                            )
                            && (
                                (
                                    ((this.position.Y / 16f) - tileRangeY)
                                    - this.inventory[this.selectedItem].tileBoost
                                ) <= tileTargetY
                            )
                        )
                        && (
                            (
                                (
                                    (((this.position.Y + this.height) / 16f) + tileRangeY)
                                    + this.inventory[this.selectedItem].tileBoost
                                ) - 2f
                            ) >= tileTargetY
                        )
                    )
                    {
                        this.showItemIcon = true;
                        if (
                            (
                                (
                                    ((this.itemTime == 0) && (this.itemAnimation > 0))
                                    && this.controlUseItem
                                )
                                && (
                                    (
                                        (
                                            (
                                                Main.tile[tileTargetX + 1, tileTargetY].active
                                                || (
                                                    Main.tile[tileTargetX + 1, tileTargetY].wall > 0
                                                )
                                            )
                                            || (
                                                Main.tile[tileTargetX - 1, tileTargetY].active
                                                || (
                                                    Main.tile[tileTargetX - 1, tileTargetY].wall > 0
                                                )
                                            )
                                        )
                                        || (
                                            (
                                                Main.tile[tileTargetX, tileTargetY + 1].active
                                                || (
                                                    Main.tile[tileTargetX, tileTargetY + 1].wall > 0
                                                )
                                            ) || Main.tile[tileTargetX, tileTargetY - 1].active
                                        )
                                    ) || (Main.tile[tileTargetX, tileTargetY - 1].wall > 0)
                                )
                            )
                            && (
                                Main.tile[tileTargetX, tileTargetY].wall
                                != this.inventory[this.selectedItem].createWall
                            )
                        )
                        {
                            WorldGen.PlaceWall(
                                tileTargetX,
                                tileTargetY,
                                this.inventory[this.selectedItem].createWall,
                                false
                            );
                            if (
                                Main.tile[tileTargetX, tileTargetY].wall
                                == this.inventory[this.selectedItem].createWall
                            )
                            {
                                this.itemTime = this.inventory[this.selectedItem].useTime;
                            }
                        }
                    }
                }
                if (
                    (
                        (this.inventory[this.selectedItem].damage >= 0)
                        && (this.inventory[this.selectedItem].type > 0)
                    ) && (this.itemAnimation > 0)
                )
                {
                    Rectangle rectangle = new Rectangle();
                    bool flag = false;
                    rectangle = new Rectangle(
                        (int)this.itemLocation.X,
                        (int)this.itemLocation.Y,
                        Main.itemTexture[this.inventory[this.selectedItem].type].Width,
                        Main.itemTexture[this.inventory[this.selectedItem].type].Height
                    )
                    {
                        Width = (int)(
                            (float)rectangle.Width * this.inventory[this.selectedItem].scale
                        ),
                        Height = (int)(
                            (float)rectangle.Height * this.inventory[this.selectedItem].scale
                        ),
                    };
                    if (this.direction == -1)
                    {
                        rectangle.X -= rectangle.Width;
                    }
                    rectangle.Y -= rectangle.Height;
                    if (this.inventory[this.selectedItem].useStyle == 1)
                    {
                        if (
                            this.itemAnimation
                            < (this.inventory[this.selectedItem].useAnimation * 0.333)
                        )
                        {
                            if (this.direction == -1)
                            {
                                rectangle.X -= ((int)(rectangle.Width * 1.4)) - rectangle.Width;
                            }
                            rectangle.Width = (int)(rectangle.Width * 1.4);
                            rectangle.Y += (int)(rectangle.Height * 0.5);
                            rectangle.Height = (int)(rectangle.Height * 1.1);
                        }
                        else if (
                            this.itemAnimation
                            >= (this.inventory[this.selectedItem].useAnimation * 0.666)
                        )
                        {
                            if (this.direction == 1)
                            {
                                rectangle.X -= rectangle.Width * 2;
                            }
                            rectangle.Width *= 2;
                            rectangle.Y -= ((int)(rectangle.Height * 1.4)) - rectangle.Height;
                            rectangle.Height = (int)(rectangle.Height * 1.4);
                        }
                    }
                    else if (this.inventory[this.selectedItem].useStyle == 3)
                    {
                        if (
                            this.itemAnimation
                            > (this.inventory[this.selectedItem].useAnimation * 0.666)
                        )
                        {
                            flag = true;
                        }
                        else
                        {
                            if (this.direction == -1)
                            {
                                rectangle.X -= ((int)(rectangle.Width * 1.4)) - rectangle.Width;
                            }
                            rectangle.Width = (int)(rectangle.Width * 1.4);
                            rectangle.Y += (int)(rectangle.Height * 0.6);
                            rectangle.Height = (int)(rectangle.Height * 0.6);
                        }
                    }
                    if (!flag)
                    {
                        int num3 = rectangle.X / 0x10;
                        int num4 = ((rectangle.X + rectangle.Width) / 0x10) + 1;
                        int num5 = rectangle.Y / 0x10;
                        int num6 = ((rectangle.Y + rectangle.Height) / 0x10) + 1;
                        for (int j = num3; j < num4; j++)
                        {
                            for (int m = num5; m < num6; m++)
                            {
                                if (Main.tile[j, m].type == 3)
                                {
                                    WorldGen.KillTile(j, m, false);
                                }
                            }
                        }
                        for (int k = 0; k < 0x3e8; k++)
                        {
                            if (Main.npc[k].active && (Main.npc[k].immune[i] == 0))
                            {
                                Rectangle rectangle2 = new Rectangle(
                                    (int)Main.npc[k].position.X,
                                    (int)Main.npc[k].position.Y,
                                    Main.npc[k].width,
                                    Main.npc[k].height
                                );
                                if (rectangle.Intersects(rectangle2))
                                {
                                    Main.npc[k]
                                        .StrikeNPC(
                                            this.inventory[this.selectedItem].damage,
                                            this.inventory[this.selectedItem].knockBack,
                                            this.direction
                                        );
                                    Main.npc[k].immune[i] = this.itemAnimation;
                                }
                            }
                        }
                    }
                }
                if (
                    ((this.itemTime == 0) && (this.inventory[this.selectedItem].healLife > 0))
                    && (this.itemAnimation > 0)
                )
                {
                    this.statLife += this.inventory[this.selectedItem].healLife;
                    this.itemTime = this.inventory[this.selectedItem].useTime;
                }
                if (
                    (this.itemTime == this.inventory[this.selectedItem].useTime)
                    && this.inventory[this.selectedItem].consumable
                )
                {
                    Item item1 = this.inventory[this.selectedItem];
                    item1.stack--;
                    if (this.inventory[this.selectedItem].stack <= 0)
                    {
                        this.itemTime = this.itemAnimation;
                    }
                }
                if ((this.inventory[this.selectedItem].stack <= 0) && (this.itemAnimation == 0))
                {
                    this.inventory[this.selectedItem] = new Item();
                }
            }
        }

        public bool ItemSpace(Item newItem)
        {
            int num;
            for (num = 0; num < 40; num++)
            {
                if (this.inventory[num].type == 0)
                {
                    return true;
                }
            }
            for (num = 0; num < 40; num++)
            {
                if (
                    (
                        (this.inventory[num].type > 0)
                        && (this.inventory[num].stack < this.inventory[num].maxStack)
                    ) && newItem.IsTheSameAs(this.inventory[num])
                )
                {
                    return true;
                }
            }
            return false;
        }

        private void PlayerFrame()
        {
            this.headFrame.X = 0x22 * this.head;
            this.bodyFrame.X = 0x22 * this.body;
            this.legFrame.X = 0x22 * this.legs;
            this.headFrame.Y = 0;
            if (this.itemAnimation > 0)
            {
                if (
                    (this.inventory[this.selectedItem].useStyle == 1)
                    || (this.inventory[this.selectedItem].type == 0)
                )
                {
                    if (this.itemAnimation < (this.itemAnimationMax * 0.333))
                    {
                        this.bodyFrame.Y = 200;
                    }
                    else if (this.itemAnimation < (this.itemAnimationMax * 0.666))
                    {
                        this.bodyFrame.Y = 150;
                    }
                    else
                    {
                        this.bodyFrame.Y = 100;
                    }
                }
                else if (this.inventory[this.selectedItem].useStyle == 2)
                {
                    if (this.itemAnimation < (this.itemAnimationMax * 0.5))
                    {
                        this.bodyFrame.Y = 150;
                    }
                    else
                    {
                        this.bodyFrame.Y = 200;
                    }
                }
                else if (this.inventory[this.selectedItem].useStyle == 3)
                {
                    if (this.itemAnimation > (this.itemAnimationMax * 0.666))
                    {
                        this.bodyFrame.Y = 100;
                    }
                    else
                    {
                        this.bodyFrame.Y = 200;
                    }
                }
            }
            else if (this.inventory[this.selectedItem].holdStyle == 1)
            {
                this.bodyFrame.Y = 200;
            }
            else if (this.velocity.Y < 0f)
            {
                this.bodyFrame.Y = 50;
            }
            else if (this.velocity.Y > 0f)
            {
                this.bodyFrame.Y = 50;
            }
            else
            {
                this.bodyFrame.Y = 0;
            }
            if (this.velocity.Y < 0f)
            {
                this.legFrame.Y = 100;
            }
            else if (this.velocity.Y > 0f)
            {
                this.legFrame.Y = 100;
            }
            else if (this.velocity.X != 0f)
            {
                if (
                    ((this.direction < 0) && (this.velocity.X > 0f))
                    || ((this.direction > 0) && (this.velocity.X < 0f))
                )
                {
                    this.legFrameCounter = 12.0;
                }
                this.legFrameCounter += 0.4 + Math.Abs((double)(this.velocity.X * 0.4));
                if (this.legFrameCounter < 6.0)
                {
                    this.legFrame.Y = 0;
                }
                else if (this.legFrameCounter < 12.0)
                {
                    this.legFrame.Y = 50;
                }
                else if (this.legFrameCounter < 18.0)
                {
                    this.legFrame.Y = 100;
                }
                else if (this.legFrameCounter < 24.0)
                {
                    this.legFrame.Y = 50;
                }
                else
                {
                    this.legFrame.Y = 0;
                    this.legFrameCounter = 0.0;
                }
            }
            else
            {
                this.legFrameCounter = 6.0;
                this.legFrame.Y = 0;
            }
        }

        public static void SetupPlayers()
        {
            for (int i = 0; i < 0x10; i++)
            {
                Main.player[i] = new Player();
                Main.player[i].name = "Some n00b";
                Main.player[i].armor[0] = new Item();
                Main.player[i].armor[1] = new Item();
                Main.player[i].armor[2] = new Item();
                for (int j = 0; j < 40; j++)
                {
                    Main.player[i].inventory[j] = new Item();
                }
                Main.player[i].inventory[0].SetDefaults("Copper Pickaxe");
                Main.player[i].inventory[1].SetDefaults("Copper Axe");
                Main.player[i].inventory[2].SetDefaults("Copper Hammer");
                Main.player[i].inventory[30].SetDefaults(0x10);
                Main.player[i].inventory[0x1f].SetDefaults(0x12);
                Main.player[i].armor[1].SetDefaults(15);
                Main.player[i].armor[2].SetDefaults(0x11);
            }
        }

        public void Spawn()
        {
            this.headPosition = new Vector2();
            this.bodyPosition = new Vector2();
            this.legPosition = new Vector2();
            this.headRotation = 0f;
            this.bodyRotation = 0f;
            this.legRotation = 0f;
            this.statLife = this.statLifeMax;
            this.immune = true;
            this.dead = false;
            this.immuneTime = 0;
            this.active = true;
            this.position.X = ((Main.spawnTileX * 0x10) + 8) - (this.width / 2);
            this.position.Y = (Main.spawnTileY * 0x10) - this.height;
            this.velocity.X = 0f;
            this.velocity.Y = 0f;
            for (int i = Main.spawnTileX - 1; i < (Main.spawnTileX + 2); i++)
            {
                for (int j = Main.spawnTileY - 3; j < Main.spawnTileY; j++)
                {
                    if (Main.tileSolid[Main.tile[i, j].type])
                    {
                        Main.tile[i, j].active = false;
                        Main.tile[i, j].type = 0;
                        WorldGen.SquareTileFrame(i, j, true);
                    }
                }
            }
            Main.screenPosition.X = (this.position.X + (this.width / 2)) - (Main.screenWidth / 2);
            Main.screenPosition.Y = (this.position.Y + (this.height / 2)) - (Main.screenHeight / 2);
        }

        public void UpdatePlayer(int i)
        {
            if (this.active)
            {
                if (this.dead)
                {
                    this.changeItem = -1;
                    this.itemAnimation = 0;
                    this.immuneAlpha += 2;
                    if (this.immuneAlpha > 0xff)
                    {
                        this.immuneAlpha = 0xff;
                    }
                    this.respawnTimer--;
                    this.headPosition += this.headVelocity;
                    this.bodyPosition += this.bodyVelocity;
                    this.legPosition += this.legVelocity;
                    this.headRotation += this.headVelocity.X * 0.1f;
                    this.bodyRotation += this.bodyVelocity.X * 0.1f;
                    this.legRotation += this.legVelocity.X * 0.1f;
                    this.headVelocity.Y += 0.1f;
                    this.bodyVelocity.Y += 0.1f;
                    this.legVelocity.Y += 0.1f;
                    this.headVelocity.X *= 0.99f;
                    this.bodyVelocity.X *= 0.99f;
                    this.legVelocity.X *= 0.99f;
                    if (this.respawnTimer <= 0)
                    {
                        this.Spawn();
                    }
                }
                else
                {
                    int num4;
                    if (i == Main.myPlayer)
                    {
                        this.controlUp = false;
                        this.controlLeft = false;
                        this.controlDown = false;
                        this.controlRight = false;
                        this.controlJump = false;
                        this.controlUseItem = false;
                        this.controlUseTile = false;
                        if (Main.keyState.IsKeyDown(Keys.W))
                        {
                            this.controlUp = true;
                        }
                        if (Main.keyState.IsKeyDown(Keys.A))
                        {
                            this.controlLeft = true;
                        }
                        if (Main.keyState.IsKeyDown(Keys.S))
                        {
                            this.controlDown = true;
                        }
                        if (Main.keyState.IsKeyDown(Keys.D))
                        {
                            this.controlRight = true;
                        }
                        if (Main.keyState.IsKeyDown(Keys.Space))
                        {
                            this.controlJump = true;
                        }
                        if (
                            !(
                                (Main.mouseState.LeftButton != ButtonState.Pressed)
                                || this.mouseInterface
                            )
                        )
                        {
                            this.controlUseItem = true;
                        }
                        if (
                            !(
                                (Main.mouseState.RightButton != ButtonState.Pressed)
                                || this.mouseInterface
                            )
                        )
                        {
                            this.controlUseTile = true;
                        }
                        if (Main.keyState.IsKeyDown(Keys.Escape))
                        {
                            if (this.releaseInventory)
                            {
                                if (!Main.playerInventory)
                                {
                                    Recipe.FindRecipes();
                                    Main.playerInventory = true;
                                    Main.soundMenuOpen.Play();
                                }
                                else
                                {
                                    Main.playerInventory = false;
                                    Main.soundMenuClose.Play();
                                }
                            }
                            this.releaseInventory = false;
                        }
                        else
                        {
                            this.releaseInventory = true;
                        }
                        if (this.delayUseItem)
                        {
                            if (!this.controlUseItem)
                            {
                                this.delayUseItem = false;
                            }
                            this.controlUseItem = false;
                        }
                        if ((this.itemAnimation == 0) && (this.itemTime == 0))
                        {
                            int num3;
                            if (
                                (
                                    Main.keyState.IsKeyDown(Keys.Q)
                                    && (this.inventory[this.selectedItem].type > 0)
                                )
                                || (
                                    (
                                        (
                                            (
                                                (Main.mouseState.LeftButton == ButtonState.Pressed)
                                                && !this.mouseInterface
                                            ) && Main.mouseLeftRelease
                                        ) || !Main.playerInventory
                                    ) && (Main.mouseItem.type > 0)
                                )
                            )
                            {
                                Item item = new Item();
                                bool flag = false;
                                if (
                                    (
                                        (
                                            (
                                                (Main.mouseState.LeftButton == ButtonState.Pressed)
                                                && !this.mouseInterface
                                            ) && Main.mouseLeftRelease
                                        ) || !Main.playerInventory
                                    ) && (Main.mouseItem.type > 0)
                                )
                                {
                                    item = this.inventory[this.selectedItem];
                                    this.inventory[this.selectedItem] = Main.mouseItem;
                                    this.delayUseItem = true;
                                    this.controlUseItem = false;
                                    flag = true;
                                }
                                int index = Item.NewItem(
                                    (int)this.position.X,
                                    (int)this.position.Y,
                                    this.width,
                                    this.height,
                                    this.inventory[this.selectedItem].type
                                );
                                if (
                                    (!flag && (this.inventory[this.selectedItem].type == 8))
                                    && (this.inventory[this.selectedItem].stack > 1)
                                )
                                {
                                    Item item1 = this.inventory[this.selectedItem];
                                    item1.stack--;
                                }
                                else
                                {
                                    this.inventory[this.selectedItem].position = Main.item[
                                        index
                                    ].position;
                                    Main.item[index] = this.inventory[this.selectedItem];
                                    this.inventory[this.selectedItem] = new Item();
                                }
                                Main.item[index].noGrabDelay = 100;
                                Main.item[index].velocity.Y = -2f;
                                Main.item[index].velocity.X =
                                    (4 * this.direction) + this.velocity.X;
                                if (
                                    (
                                        (
                                            (Main.mouseState.LeftButton == ButtonState.Pressed)
                                            && !this.mouseInterface
                                        ) || !Main.playerInventory
                                    ) && (Main.mouseItem.type > 0)
                                )
                                {
                                    this.inventory[this.selectedItem] = item;
                                    Main.mouseItem = new Item();
                                }
                                else
                                {
                                    this.itemAnimation = 10;
                                    this.itemAnimationMax = 10;
                                }
                                Recipe.FindRecipes();
                            }
                            if (!Main.playerInventory)
                            {
                                int selectedItem = this.selectedItem;
                                if (Main.keyState.IsKeyDown(Keys.D1))
                                {
                                    this.selectedItem = 0;
                                }
                                if (Main.keyState.IsKeyDown(Keys.D2))
                                {
                                    this.selectedItem = 1;
                                }
                                if (Main.keyState.IsKeyDown(Keys.D3))
                                {
                                    this.selectedItem = 2;
                                }
                                if (Main.keyState.IsKeyDown(Keys.D4))
                                {
                                    this.selectedItem = 3;
                                }
                                if (Main.keyState.IsKeyDown(Keys.D5))
                                {
                                    this.selectedItem = 4;
                                }
                                if (Main.keyState.IsKeyDown(Keys.D6))
                                {
                                    this.selectedItem = 5;
                                }
                                if (Main.keyState.IsKeyDown(Keys.D7))
                                {
                                    this.selectedItem = 6;
                                }
                                if (Main.keyState.IsKeyDown(Keys.D8))
                                {
                                    this.selectedItem = 7;
                                }
                                if (Main.keyState.IsKeyDown(Keys.D9))
                                {
                                    this.selectedItem = 8;
                                }
                                if (Main.keyState.IsKeyDown(Keys.D0))
                                {
                                    this.selectedItem = 9;
                                }
                                if (selectedItem != this.selectedItem)
                                {
                                    Main.soundInstanceMenuTick.Play();
                                }
                                num3 =
                                    (
                                        Main.mouseState.ScrollWheelValue
                                        - Main.oldMouseState.ScrollWheelValue
                                    ) / 120;
                                while (num3 > 9)
                                {
                                    num3 -= 10;
                                }
                                while (num3 < 0)
                                {
                                    num3 += 10;
                                }
                                this.selectedItem -= num3;
                                if (num3 != 0)
                                {
                                    Main.soundInstanceMenuTick.Play();
                                }
                                if (this.changeItem >= 0)
                                {
                                    if (this.selectedItem != this.changeItem)
                                    {
                                        Main.soundInstanceMenuTick.Play();
                                    }
                                    this.selectedItem = this.changeItem;
                                    this.changeItem = -1;
                                }
                                while (this.selectedItem > 9)
                                {
                                    this.selectedItem -= 10;
                                }
                                while (this.selectedItem < 0)
                                {
                                    this.selectedItem += 10;
                                }
                            }
                            else
                            {
                                num3 =
                                    (
                                        Main.mouseState.ScrollWheelValue
                                        - Main.oldMouseState.ScrollWheelValue
                                    ) / 120;
                                Main.focusRecipe += num3;
                                if (Main.focusRecipe > (Main.numAvailableRecipes - 1))
                                {
                                    Main.focusRecipe = Main.numAvailableRecipes - 1;
                                }
                                if (Main.focusRecipe < 0)
                                {
                                    Main.focusRecipe = 0;
                                }
                            }
                        }
                    }
                    if (this.mouseInterface)
                    {
                        this.delayUseItem = true;
                    }
                    tileTargetX = (int)((Main.mouseState.X + Main.screenPosition.X) / 16f);
                    tileTargetY = (int)((Main.mouseState.Y + Main.screenPosition.Y) / 16f);
                    if (this.immune)
                    {
                        this.immuneTime--;
                        if (this.immuneTime <= 0)
                        {
                            this.immune = false;
                        }
                        this.immuneAlpha += this.immuneAlphaDirection * 50;
                        if (this.immuneAlpha <= 50)
                        {
                            this.immuneAlphaDirection = 1;
                        }
                        else if (this.immuneAlpha >= 0xcd)
                        {
                            this.immuneAlphaDirection = -1;
                        }
                    }
                    else
                    {
                        this.immuneAlpha = 0;
                    }
                    this.statDefense = 10;
                    for (num4 = 0; num4 < 3; num4++)
                    {
                        this.statDefense += this.armor[num4].defense;
                    }
                    this.body = this.armor[1].bodySlot;
                    this.legs = this.armor[2].legSlot;
                    this.headFrame.Width = 0x20;
                    this.headFrame.Height = 0x30;
                    this.bodyFrame.Width = 0x20;
                    this.bodyFrame.Height = 0x30;
                    this.legFrame.Width = 0x20;
                    this.legFrame.Height = 0x30;
                    if (this.controlLeft && (this.velocity.X > -maxRunSpeed))
                    {
                        if (this.velocity.X > runSlowdown)
                        {
                            this.velocity.X -= runSlowdown;
                        }
                        this.velocity.X -= runAcceleration;
                        if ((this.itemAnimation == 0) || this.inventory[this.selectedItem].useTurn)
                        {
                            this.direction = -1;
                        }
                    }
                    else if (this.controlRight && (this.velocity.X < maxRunSpeed))
                    {
                        if (this.velocity.X < -runSlowdown)
                        {
                            this.velocity.X += runSlowdown;
                        }
                        this.velocity.X += runAcceleration;
                        if ((this.itemAnimation == 0) || this.inventory[this.selectedItem].useTurn)
                        {
                            this.direction = 1;
                        }
                    }
                    else if (this.velocity.Y == 0f)
                    {
                        if (this.velocity.X > runSlowdown)
                        {
                            this.velocity.X -= runSlowdown;
                        }
                        else if (this.velocity.X < -runSlowdown)
                        {
                            this.velocity.X += runSlowdown;
                        }
                        else
                        {
                            this.velocity.X = 0f;
                        }
                    }
                    else if (this.velocity.X > (runSlowdown * 0.5))
                    {
                        this.velocity.X -= runSlowdown * 0.5f;
                    }
                    else if (this.velocity.X < (-runSlowdown * 0.5))
                    {
                        this.velocity.X += runSlowdown * 0.5f;
                    }
                    else
                    {
                        this.velocity.X = 0f;
                    }
                    if (this.controlJump)
                    {
                        if (this.jump > 0)
                        {
                            if (this.velocity.Y > (-jumpSpeed + (gravity * 2f)))
                            {
                                this.jump = 0;
                            }
                            else
                            {
                                this.velocity.Y = -jumpSpeed;
                                this.jump--;
                            }
                        }
                        else if ((this.velocity.Y == 0f) && this.releaseJump)
                        {
                            this.velocity.Y = -jumpSpeed;
                            this.jump = jumpHeight;
                        }
                        this.releaseJump = false;
                    }
                    else
                    {
                        this.jump = 0;
                        this.releaseJump = true;
                    }
                    this.velocity.Y += gravity;
                    if (this.velocity.Y > maxFallSpeed)
                    {
                        this.velocity.Y = maxFallSpeed;
                    }
                    for (num4 = 0; num4 < 0x3e8; num4++)
                    {
                        if (Main.item[num4].active && (Main.item[num4].noGrabDelay == 0))
                        {
                            Rectangle rectangle2 = new Rectangle(
                                (int)this.position.X,
                                (int)this.position.Y,
                                this.width,
                                this.height
                            );
                            if (
                                rectangle2.Intersects(
                                    new Rectangle(
                                        (int)Main.item[num4].position.X,
                                        (int)Main.item[num4].position.Y,
                                        Main.item[num4].width,
                                        Main.item[num4].height
                                    )
                                )
                            )
                            {
                                if (
                                    (this.inventory[this.selectedItem].type != 0)
                                    || (this.itemAnimation <= 0)
                                )
                                {
                                    Main.item[num4] = this.GetItem(i, Main.item[num4]);
                                }
                            }
                            else
                            {
                                rectangle2 = new Rectangle(
                                    ((int)this.position.X) - itemGrabRange,
                                    ((int)this.position.Y) - itemGrabRange,
                                    this.width + (itemGrabRange * 2),
                                    this.height + (itemGrabRange * 2)
                                );
                                if (
                                    rectangle2.Intersects(
                                        new Rectangle(
                                            (int)Main.item[num4].position.X,
                                            (int)Main.item[num4].position.Y,
                                            Main.item[num4].width,
                                            Main.item[num4].height
                                        )
                                    ) && this.ItemSpace(Main.item[num4])
                                )
                                {
                                    Main.item[num4].beingGrabbed = true;
                                    if (
                                        (this.position.X + (this.width * 0.5))
                                        > (
                                            Main.item[num4].position.X
                                            + (Main.item[num4].width * 0.5)
                                        )
                                    )
                                    {
                                        if (
                                            Main.item[num4].velocity.X
                                            < (itemGrabSpeedMax + this.velocity.X)
                                        )
                                        {
                                            Main.item[num4].velocity.X += itemGrabSpeed;
                                        }
                                        if (Main.item[num4].velocity.X < 0f)
                                        {
                                            Main.item[num4].velocity.X += itemGrabSpeed * 0.75f;
                                        }
                                    }
                                    else
                                    {
                                        if (
                                            Main.item[num4].velocity.X
                                            > (-itemGrabSpeedMax + this.velocity.X)
                                        )
                                        {
                                            Main.item[num4].velocity.X -= itemGrabSpeed;
                                        }
                                        if (Main.item[num4].velocity.X > 0f)
                                        {
                                            Main.item[num4].velocity.X -= itemGrabSpeed * 0.75f;
                                        }
                                    }
                                    if (
                                        (this.position.Y + (this.height * 0.5))
                                        > (
                                            Main.item[num4].position.Y
                                            + (Main.item[num4].height * 0.5)
                                        )
                                    )
                                    {
                                        if (Main.item[num4].velocity.Y < itemGrabSpeedMax)
                                        {
                                            Main.item[num4].velocity.Y += itemGrabSpeed;
                                        }
                                        if (Main.item[num4].velocity.Y < 0f)
                                        {
                                            Main.item[num4].velocity.Y += itemGrabSpeed * 0.75f;
                                        }
                                    }
                                    else
                                    {
                                        if (Main.item[num4].velocity.Y > -itemGrabSpeedMax)
                                        {
                                            Main.item[num4].velocity.Y -= itemGrabSpeed;
                                        }
                                        if (Main.item[num4].velocity.Y > 0f)
                                        {
                                            Main.item[num4].velocity.Y -= itemGrabSpeed * 0.75f;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (
                        (
                            (
                                (
                                    (((this.position.X / 16f) - tileRangeX) <= tileTargetX)
                                    && (
                                        ((((this.position.X + this.width) / 16f) + tileRangeX) - 1f)
                                        >= tileTargetX
                                    )
                                ) && (((this.position.Y / 16f) - tileRangeY) <= tileTargetY)
                            )
                            && (
                                ((((this.position.Y + this.height) / 16f) + tileRangeY) - 2f)
                                >= tileTargetY
                            )
                        ) && Main.tile[tileTargetX, tileTargetY].active
                    )
                    {
                        if (Main.tile[tileTargetX, tileTargetY].type == 4)
                        {
                            this.showItemIcon = true;
                            this.showItemIcon2 = 8;
                        }
                        if (
                            (Main.tile[tileTargetX, tileTargetY].type == 10)
                            || (Main.tile[tileTargetX, tileTargetY].type == 11)
                        )
                        {
                            this.showItemIcon = true;
                            this.showItemIcon2 = 0x19;
                        }
                        if (this.controlUseTile)
                        {
                            if (this.releaseUseTile)
                            {
                                if (Main.tile[tileTargetX, tileTargetY].type == 4)
                                {
                                    WorldGen.KillTile(tileTargetX, tileTargetY, false);
                                }
                                else if (Main.tile[tileTargetX, tileTargetY].type == 10)
                                {
                                    WorldGen.OpenDoor(tileTargetX, tileTargetY, this.direction);
                                }
                                else if (Main.tile[tileTargetX, tileTargetY].type == 11)
                                {
                                    WorldGen.CloseDoor(tileTargetX, tileTargetY);
                                }
                            }
                            this.releaseUseTile = false;
                        }
                        else
                        {
                            this.releaseUseTile = true;
                        }
                    }
                    Rectangle rectangle = new Rectangle(
                        (int)this.position.X,
                        (int)this.position.Y,
                        this.width,
                        this.height
                    );
                    for (num4 = 0; num4 < 0x3e8; num4++)
                    {
                        if (
                            Main.npc[num4].active
                            && rectangle.Intersects(
                                new Rectangle(
                                    (int)Main.npc[num4].position.X,
                                    (int)Main.npc[num4].position.Y,
                                    Main.npc[num4].width,
                                    Main.npc[num4].height
                                )
                            )
                        )
                        {
                            int hitDirection = -1;
                            if (
                                (Main.npc[num4].position.X + (Main.npc[num4].width / 2))
                                < (this.position.X + (this.width / 2))
                            )
                            {
                                hitDirection = 1;
                            }
                            this.Hurt(Main.npc[num4].damage, hitDirection);
                        }
                    }
                    this.velocity = Collision.TileCollision(
                        this.position,
                        this.velocity,
                        this.width,
                        this.height
                    );
                    this.position += this.velocity;
                    this.ItemCheck(i);
                    this.PlayerFrame();
                    if (this.statLife > this.statLifeMax)
                    {
                        this.statLife = this.statLifeMax;
                    }
                }
            }
        }
    }
}
