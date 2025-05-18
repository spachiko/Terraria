namespace Terraria
{
    using System;
    using Microsoft.Xna.Framework;

    public class Item
    {
        public bool active;
        public int alpha;
        public bool autoReuse;
        public int axe;
        public bool beingGrabbed;
        public int bodySlot;
        public Color color;
        public bool consumable;
        public int createTile = -1;
        public int createWall = -1;
        public int damage;
        public int defense;
        public static float gravity = 0.1f;
        public int hammer;
        public int headSlot;
        public int healLife;
        public int height;
        public int holdStyle;
        public float knockBack;
        public int legSlot;
        public static float maxFallSpeed = 10f;
        public int maxStack;
        public string name;
        public int noGrabDelay;
        public int pick;
        public Vector2 position;
        public float scale = 1f;
        public int stack;
        public int tileBoost;
        public int type;
        public int useAnimation;
        public int useSound = 0;
        public int useStyle;
        public int useTime;
        public bool useTurn;
        public Vector2 velocity;
        public int width;

        public object Clone()
        {
            return base.MemberwiseClone();
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

        public bool IsTheSameAs(Item compareItem)
        {
            return (this.name == compareItem.name);
        }

        public static int NewItem(int X, int Y, int Width, int Height, int Type)
        {
            int num2;
            int index = -1;
            for (num2 = 0; num2 < 0x3e8; num2++)
            {
                if (!Main.item[num2].active)
                {
                    index = num2;
                    break;
                }
            }
            if (index == -1)
            {
                index = 0x3e7;
                for (num2 = 0; num2 < 0x3e7; num2++)
                {
                    Main.item[num2] = new Item();
                    Main.item[num2] = Main.item[num2 + 1];
                }
            }
            Main.item[index] = new Item();
            Main.item[index].SetDefaults(Type);
            Main.item[index].position.X = (X + (Width / 2)) - (Main.item[index].width / 2);
            Main.item[index].position.Y = (Y + (Height / 2)) - (Main.item[index].height / 2);
            Main.item[index].velocity.X = Main.rand.Next(-20, 0x15) * 0.1f;
            Main.item[index].velocity.Y = Main.rand.Next(-30, -10) * 0.1f;
            Main.item[index].active = true;
            return index;
        }

        public void SetDefaults(int Type)
        {
            this.active = true;
            this.alpha = 0;
            this.autoReuse = false;
            this.axe = 0;
            this.color = new Color();
            this.consumable = false;
            this.createTile = -1;
            this.createWall = -1;
            this.damage = -1;
            this.holdStyle = 0;
            this.knockBack = 0f;
            this.maxStack = 1;
            this.pick = 0;
            this.scale = 1f;
            this.stack = 1;
            this.tileBoost = 0;
            this.type = Type;
            this.useStyle = 0;
            this.useTurn = false;
            if (this.type == 1)
            {
                this.name = "Iron Pickaxe";
                this.useStyle = 1;
                this.useTurn = true;
                this.useAnimation = 20;
                this.useTime = 13;
                this.autoReuse = true;
                this.width = 0x18;
                this.height = 0x1c;
                this.damage = 5;
                this.pick = 0x2d;
                this.useSound = 1;
            }
            else if (this.type == 2)
            {
                this.name = "Dirt Block";
                this.useStyle = 1;
                this.useTurn = true;
                this.useAnimation = 15;
                this.useTime = 10;
                this.autoReuse = true;
                this.maxStack = 0x63;
                this.consumable = true;
                this.createTile = 0;
                this.width = 12;
                this.height = 12;
            }
            else if (this.type == 3)
            {
                this.name = "Stone Block";
                this.useStyle = 1;
                this.useTurn = true;
                this.useAnimation = 15;
                this.useTime = 10;
                this.autoReuse = true;
                this.maxStack = 0x63;
                this.consumable = true;
                this.createTile = 1;
                this.width = 12;
                this.height = 12;
            }
            else if (this.type == 4)
            {
                this.name = "Iron Broadsword";
                this.useStyle = 1;
                this.useTurn = false;
                this.useAnimation = 0x15;
                this.useTime = 0x15;
                this.width = 0x18;
                this.height = 0x1c;
                this.damage = 10;
                this.knockBack = 5f;
                this.useSound = 1;
                this.scale = 1f;
            }
            else if (this.type == 5)
            {
                this.name = "Mushroom";
                this.useStyle = 2;
                this.useSound = 2;
                this.useTurn = false;
                this.useAnimation = 0x11;
                this.useTime = 0x11;
                this.width = 0x10;
                this.height = 0x12;
                this.healLife = 20;
                this.maxStack = 0x63;
                this.consumable = true;
            }
            else if (this.type == 6)
            {
                this.name = "Iron Shortsword";
                this.useStyle = 3;
                this.useTurn = false;
                this.useAnimation = 12;
                this.useTime = 12;
                this.width = 0x18;
                this.height = 0x1c;
                this.damage = 8;
                this.knockBack = 4f;
                this.scale = 0.9f;
                this.useSound = 1;
                this.useTurn = true;
            }
            else if (this.type == 7)
            {
                this.name = "Iron Hammer";
                this.autoReuse = true;
                this.useStyle = 1;
                this.useTurn = true;
                this.useAnimation = 30;
                this.useTime = 20;
                this.hammer = 0x2d;
                this.width = 0x18;
                this.height = 0x1c;
                this.damage = 7;
                this.knockBack = 6.5f;
                this.scale = 1.2f;
                this.useSound = 1;
            }
            else if (this.type == 8)
            {
                this.name = "Torch";
                this.useStyle = 1;
                this.useTurn = true;
                this.useAnimation = 15;
                this.useTime = 10;
                this.holdStyle = 1;
                this.autoReuse = true;
                this.maxStack = 0x63;
                this.consumable = true;
                this.createTile = 4;
                this.width = 10;
                this.height = 12;
                this.damage = 1;
            }
            else if (this.type == 9)
            {
                this.name = "Wood";
                this.useTurn = true;
                this.useAnimation = 15;
                this.useTime = 10;
                this.autoReuse = true;
                this.maxStack = 0x63;
                this.consumable = true;
                this.width = 8;
                this.height = 10;
            }
            else if (this.type == 10)
            {
                this.name = "Iron Axe";
                this.useStyle = 1;
                this.useTurn = true;
                this.useAnimation = 0x1b;
                this.knockBack = 5.5f;
                this.useTime = 0x13;
                this.autoReuse = true;
                this.width = 0x18;
                this.height = 0x1c;
                this.damage = 5;
                this.axe = 9;
                this.scale = 1.1f;
                this.useSound = 1;
            }
            else if (this.type == 11)
            {
                this.name = "Iron Ore";
                this.useStyle = 1;
                this.useTurn = true;
                this.useAnimation = 15;
                this.useTime = 10;
                this.autoReuse = true;
                this.maxStack = 0x63;
                this.consumable = true;
                this.createTile = 6;
                this.width = 12;
                this.height = 12;
            }
            else if (this.type == 12)
            {
                this.name = "Copper Ore";
                this.useStyle = 1;
                this.useTurn = true;
                this.useAnimation = 15;
                this.useTime = 10;
                this.autoReuse = true;
                this.maxStack = 0x63;
                this.consumable = true;
                this.createTile = 7;
                this.width = 12;
                this.height = 12;
            }
            else if (this.type == 13)
            {
                this.name = "Gold Ore";
                this.useStyle = 1;
                this.useTurn = true;
                this.useAnimation = 15;
                this.useTime = 10;
                this.autoReuse = true;
                this.maxStack = 0x63;
                this.consumable = true;
                this.createTile = 8;
                this.width = 12;
                this.height = 12;
            }
            else if (this.type == 14)
            {
                this.name = "Silver Ore";
                this.useStyle = 1;
                this.useTurn = true;
                this.useAnimation = 15;
                this.useTime = 10;
                this.autoReuse = true;
                this.maxStack = 0x63;
                this.consumable = true;
                this.createTile = 9;
                this.width = 12;
                this.height = 12;
            }
            else if (this.type == 15)
            {
                this.name = "Green Tunic";
                this.width = 0x18;
                this.height = 0x1c;
                this.bodySlot = 1;
                this.defense = 2;
            }
            else if (this.type == 0x10)
            {
                this.name = "Gray Tunic";
                this.width = 0x18;
                this.height = 0x1c;
                this.bodySlot = 2;
                this.defense = 2;
            }
            else if (this.type == 0x11)
            {
                this.name = "White Pants";
                this.width = 0x18;
                this.height = 0x1c;
                this.legSlot = 1;
                this.defense = 1;
            }
            else if (this.type == 0x12)
            {
                this.name = "Blue Pants";
                this.width = 0x18;
                this.height = 0x1c;
                this.legSlot = 2;
                this.defense = 1;
            }
            else if (this.type == 0x13)
            {
                this.name = "Gold Bar";
                this.width = 20;
                this.height = 20;
                this.maxStack = 0x63;
            }
            else if (this.type == 20)
            {
                this.name = "Copper Bar";
                this.width = 20;
                this.height = 20;
                this.maxStack = 0x63;
            }
            else if (this.type == 0x15)
            {
                this.name = "Silver Bar";
                this.width = 20;
                this.height = 20;
                this.maxStack = 0x63;
            }
            else if (this.type == 0x16)
            {
                this.name = "Iron Bar";
                this.width = 20;
                this.height = 20;
                this.maxStack = 0x63;
            }
            else if (this.type == 0x17)
            {
                this.name = "Gel";
                this.width = 20;
                this.height = 12;
                this.maxStack = 12;
                this.alpha = 0xaf;
                this.color = new Color(0, 80, 0xff, 100);
            }
            else if (this.type == 0x18)
            {
                this.name = "Wooden Sword";
                this.useStyle = 1;
                this.useTurn = false;
                this.useAnimation = 0x19;
                this.width = 0x18;
                this.height = 0x1c;
                this.damage = 7;
                this.knockBack = 4f;
                this.scale = 0.95f;
                this.useSound = 1;
            }
            else if (this.type == 0x19)
            {
                this.name = "Wooden Door";
                this.useStyle = 1;
                this.useTurn = true;
                this.useAnimation = 15;
                this.useTime = 10;
                this.maxStack = 0x63;
                this.consumable = true;
                this.createTile = 10;
                this.width = 14;
                this.height = 0x1c;
            }
            else if (this.type == 0x1a)
            {
                this.name = "Stone Wall";
                this.useStyle = 1;
                this.useTurn = true;
                this.useAnimation = 15;
                this.useTime = 10;
                this.autoReuse = true;
                this.maxStack = 0x63;
                this.consumable = true;
                this.createWall = 1;
                this.width = 12;
                this.height = 12;
            }
        }

        public void SetDefaults(string ItemName)
        {
            if (ItemName == "Gold Pickaxe")
            {
                this.SetDefaults(1);
                this.color = new Color(210, 190, 0, 100);
                this.useTime = 13;
                this.pick = 0x41;
                this.useAnimation = 0x13;
                this.scale = 1.1f;
                this.damage = 7;
            }
            if (ItemName == "Gold Broadsword")
            {
                this.SetDefaults(4);
                this.color = new Color(210, 190, 0, 100);
                this.useAnimation = 0x13;
                this.damage = 15;
                this.scale = 1.1f;
            }
            if (ItemName == "Gold Shortsword")
            {
                this.SetDefaults(6);
                this.color = new Color(210, 190, 0, 100);
                this.damage = 12;
                this.useAnimation = 10;
                this.scale = 1f;
            }
            if (ItemName == "Gold Axe")
            {
                this.SetDefaults(10);
                this.color = new Color(210, 190, 0, 100);
                this.useTime = 0x11;
                this.axe = 13;
                this.useAnimation = 0x19;
                this.scale = 1.2f;
                this.damage = 8;
            }
            if (ItemName == "Gold Hammer")
            {
                this.SetDefaults(7);
                this.color = new Color(210, 190, 0, 100);
                this.useAnimation = 0x1b;
                this.useTime = 0x11;
                this.hammer = 0x41;
                this.scale = 1.3f;
                this.damage = 10;
            }
            if (ItemName == "Silver Pickaxe")
            {
                this.SetDefaults(1);
                this.color = new Color(180, 180, 180, 100);
                this.useTime = 0x11;
                this.pick = 0x37;
                this.useAnimation = 20;
                this.scale = 1.05f;
                this.damage = 6;
            }
            if (ItemName == "Silver Broadsword")
            {
                this.SetDefaults(4);
                this.color = new Color(180, 180, 180, 100);
                this.useAnimation = 20;
                this.damage = 13;
                this.scale = 1.05f;
            }
            if (ItemName == "Silver Shortsword")
            {
                this.SetDefaults(6);
                this.color = new Color(180, 180, 180, 100);
                this.damage = 11;
                this.useAnimation = 11;
                this.scale = 0.95f;
            }
            if (ItemName == "Silver Axe")
            {
                this.SetDefaults(10);
                this.color = new Color(180, 180, 180, 100);
                this.useTime = 0x12;
                this.axe = 11;
                this.useAnimation = 0x1a;
                this.scale = 1.15f;
                this.damage = 7;
            }
            if (ItemName == "Silver Hammer")
            {
                this.SetDefaults(7);
                this.color = new Color(180, 180, 180, 100);
                this.useAnimation = 0x1c;
                this.useTime = 0x17;
                this.scale = 1.25f;
                this.damage = 9;
                this.hammer = 0x37;
            }
            if (ItemName == "Copper Pickaxe")
            {
                this.SetDefaults(1);
                this.color = new Color(180, 100, 0x2d, 80);
                this.useTime = 15;
                this.pick = 0x23;
                this.useAnimation = 0x17;
                this.scale = 0.9f;
                this.tileBoost = -1;
                this.damage = 2;
            }
            if (ItemName == "Copper Broadsword")
            {
                this.SetDefaults(4);
                this.color = new Color(180, 100, 0x2d, 80);
                this.useAnimation = 0x17;
                this.damage = 8;
                this.scale = 0.9f;
            }
            if (ItemName == "Copper Shortsword")
            {
                this.SetDefaults(6);
                this.color = new Color(180, 100, 0x2d, 80);
                this.damage = 6;
                this.useAnimation = 13;
                this.scale = 0.8f;
            }
            if (ItemName == "Copper Axe")
            {
                this.SetDefaults(10);
                this.color = new Color(180, 100, 0x2d, 80);
                this.useTime = 0x15;
                this.axe = 8;
                this.useAnimation = 30;
                this.scale = 1f;
                this.damage = 3;
                this.tileBoost = -1;
            }
            if (ItemName == "Copper Hammer")
            {
                this.SetDefaults(7);
                this.color = new Color(180, 100, 0x2d, 80);
                this.useAnimation = 0x21;
                this.useTime = 0x17;
                this.scale = 1.1f;
                this.damage = 4;
                this.hammer = 0x23;
                this.tileBoost = -1;
            }
            this.name = ItemName;
        }

        public void UpdateItem(int i)
        {
            if (!this.beingGrabbed)
            {
                this.velocity.Y += gravity;
                if (this.velocity.Y > maxFallSpeed)
                {
                    this.velocity.Y = maxFallSpeed;
                }
                this.velocity.X *= 0.95f;
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
            }
            else
            {
                this.beingGrabbed = false;
            }
            if (this.type == 8)
            {
                Lighting.addLight(
                    (int)((this.position.X - 7f) / 16f),
                    (int)((this.position.Y - 7f) / 16f),
                    0xff
                );
            }
            this.position += this.velocity;
            if (this.noGrabDelay > 0)
            {
                this.noGrabDelay--;
            }
        }
    }
}
