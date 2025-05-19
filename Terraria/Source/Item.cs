using Microsoft.Xna.Framework;

namespace Terraria;

public class Item
{
    private static readonly float Gravity = 0.1f;
    private static readonly float MaxFallSpeed = 10f;
    public bool Active;
    public int Alpha;
    public bool AutoReuse;
    public int Axe;
    public bool BeingGrabbed;
    public int BodySlot;
    public Color Color;
    public bool Consumable;
    public int CreateTile = -1;
    public int CreateWall = -1;
    public int Damage;
    public int Defense;
    public int Hammer;
    public int HeadSlot;
    public int HealLife;
    public int Height;
    public int HoldStyle;
    public float KnockBack;
    public int LegSlot;
    public int MaxStack;
    public string Name;
    public int NoGrabDelay;
    public int Pick;
    public Vector2 Position;
    public float Scale = 1f;
    public int Stack;
    public int TileBoost;
    public int Type;
    public int UseAnimation;
    public int UseSound;
    public int UseStyle;
    public int UseTime;
    public bool UseTurn;
    public Vector2 Velocity;
    public int Width;

    public object Clone()
    {
        return MemberwiseClone();
    }

    public Color GetAlpha(Color newColor)
    {
        var r = newColor.R - Alpha;
        var g = newColor.G - Alpha;
        var b = newColor.B - Alpha;
        var a = newColor.A - Alpha;
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

    public bool IsTheSameAs(Item compareItem)
    {
        return Name == compareItem.Name;
    }

    public static int NewItem(int x, int y, int width, int height, int type)
    {
        int num2;
        var index = -1;
        for (num2 = 0; num2 < 0x3e8; num2++)
            if (!Main.Item[num2].Active)
            {
                index = num2;
                break;
            }

        if (index == -1)
        {
            index = 0x3e7;
            for (num2 = 0; num2 < 0x3e7; num2++)
            {
                Main.Item[num2] = new Item();
                Main.Item[num2] = Main.Item[num2 + 1];
            }
        }

        Main.Item[index] = new Item();
        Main.Item[index].SetDefaults(type);
        Main.Item[index].Position.X = x + width / 2 - Main.Item[index].Width / 2;
        Main.Item[index].Position.Y = y + height / 2 - Main.Item[index].Height / 2;
        Main.Item[index].Velocity.X = Main.Rand.Next(-20, 0x15) * 0.1f;
        Main.Item[index].Velocity.Y = Main.Rand.Next(-30, -10) * 0.1f;
        Main.Item[index].Active = true;
        return index;
    }

    public void SetDefaults(int type)
    {
        Active = true;
        Alpha = 0;
        AutoReuse = false;
        Axe = 0;
        Color = new Color();
        Consumable = false;
        CreateTile = -1;
        CreateWall = -1;
        Damage = -1;
        HoldStyle = 0;
        KnockBack = 0f;
        MaxStack = 1;
        Pick = 0;
        Scale = 1f;
        Stack = 1;
        TileBoost = 0;
        Type = type;
        UseStyle = 0;
        UseTurn = false;
        if (Type == 1)
        {
            Name = "Iron Pickaxe";
            UseStyle = 1;
            UseTurn = true;
            UseAnimation = 20;
            UseTime = 13;
            AutoReuse = true;
            Width = 0x18;
            Height = 0x1c;
            Damage = 5;
            Pick = 0x2d;
            UseSound = 1;
        }
        else if (Type == 2)
        {
            Name = "Dirt Block";
            UseStyle = 1;
            UseTurn = true;
            UseAnimation = 15;
            UseTime = 10;
            AutoReuse = true;
            MaxStack = 0x63;
            Consumable = true;
            CreateTile = 0;
            Width = 12;
            Height = 12;
        }
        else if (Type == 3)
        {
            Name = "Stone Block";
            UseStyle = 1;
            UseTurn = true;
            UseAnimation = 15;
            UseTime = 10;
            AutoReuse = true;
            MaxStack = 0x63;
            Consumable = true;
            CreateTile = 1;
            Width = 12;
            Height = 12;
        }
        else if (Type == 4)
        {
            Name = "Iron Broadsword";
            UseStyle = 1;
            UseTurn = false;
            UseAnimation = 0x15;
            UseTime = 0x15;
            Width = 0x18;
            Height = 0x1c;
            Damage = 10;
            KnockBack = 5f;
            UseSound = 1;
            Scale = 1f;
        }
        else if (Type == 5)
        {
            Name = "Mushroom";
            UseStyle = 2;
            UseSound = 2;
            UseTurn = false;
            UseAnimation = 0x11;
            UseTime = 0x11;
            Width = 0x10;
            Height = 0x12;
            HealLife = 20;
            MaxStack = 0x63;
            Consumable = true;
        }
        else if (Type == 6)
        {
            Name = "Iron Shortsword";
            UseStyle = 3;
            UseTurn = false;
            UseAnimation = 12;
            UseTime = 12;
            Width = 0x18;
            Height = 0x1c;
            Damage = 8;
            KnockBack = 4f;
            Scale = 0.9f;
            UseSound = 1;
            UseTurn = true;
        }
        else if (Type == 7)
        {
            Name = "Iron Hammer";
            AutoReuse = true;
            UseStyle = 1;
            UseTurn = true;
            UseAnimation = 30;
            UseTime = 20;
            Hammer = 0x2d;
            Width = 0x18;
            Height = 0x1c;
            Damage = 7;
            KnockBack = 6.5f;
            Scale = 1.2f;
            UseSound = 1;
        }
        else if (Type == 8)
        {
            Name = "Torch";
            UseStyle = 1;
            UseTurn = true;
            UseAnimation = 15;
            UseTime = 10;
            HoldStyle = 1;
            AutoReuse = true;
            MaxStack = 0x63;
            Consumable = true;
            CreateTile = 4;
            Width = 10;
            Height = 12;
            Damage = 1;
        }
        else if (Type == 9)
        {
            Name = "Wood";
            UseTurn = true;
            UseAnimation = 15;
            UseTime = 10;
            AutoReuse = true;
            MaxStack = 0x63;
            Consumable = true;
            Width = 8;
            Height = 10;
        }
        else if (Type == 10)
        {
            Name = "Iron Axe";
            UseStyle = 1;
            UseTurn = true;
            UseAnimation = 0x1b;
            KnockBack = 5.5f;
            UseTime = 0x13;
            AutoReuse = true;
            Width = 0x18;
            Height = 0x1c;
            Damage = 5;
            Axe = 9;
            Scale = 1.1f;
            UseSound = 1;
        }
        else if (Type == 11)
        {
            Name = "Iron Ore";
            UseStyle = 1;
            UseTurn = true;
            UseAnimation = 15;
            UseTime = 10;
            AutoReuse = true;
            MaxStack = 0x63;
            Consumable = true;
            CreateTile = 6;
            Width = 12;
            Height = 12;
        }
        else if (Type == 12)
        {
            Name = "Copper Ore";
            UseStyle = 1;
            UseTurn = true;
            UseAnimation = 15;
            UseTime = 10;
            AutoReuse = true;
            MaxStack = 0x63;
            Consumable = true;
            CreateTile = 7;
            Width = 12;
            Height = 12;
        }
        else if (Type == 13)
        {
            Name = "Gold Ore";
            UseStyle = 1;
            UseTurn = true;
            UseAnimation = 15;
            UseTime = 10;
            AutoReuse = true;
            MaxStack = 0x63;
            Consumable = true;
            CreateTile = 8;
            Width = 12;
            Height = 12;
        }
        else if (Type == 14)
        {
            Name = "Silver Ore";
            UseStyle = 1;
            UseTurn = true;
            UseAnimation = 15;
            UseTime = 10;
            AutoReuse = true;
            MaxStack = 0x63;
            Consumable = true;
            CreateTile = 9;
            Width = 12;
            Height = 12;
        }
        else if (Type == 15)
        {
            Name = "Green Tunic";
            Width = 0x18;
            Height = 0x1c;
            BodySlot = 1;
            Defense = 2;
        }
        else if (Type == 0x10)
        {
            Name = "Gray Tunic";
            Width = 0x18;
            Height = 0x1c;
            BodySlot = 2;
            Defense = 2;
        }
        else if (Type == 0x11)
        {
            Name = "White Pants";
            Width = 0x18;
            Height = 0x1c;
            LegSlot = 1;
            Defense = 1;
        }
        else if (Type == 0x12)
        {
            Name = "Blue Pants";
            Width = 0x18;
            Height = 0x1c;
            LegSlot = 2;
            Defense = 1;
        }
        else if (Type == 0x13)
        {
            Name = "Gold Bar";
            Width = 20;
            Height = 20;
            MaxStack = 0x63;
        }
        else if (Type == 20)
        {
            Name = "Copper Bar";
            Width = 20;
            Height = 20;
            MaxStack = 0x63;
        }
        else if (Type == 0x15)
        {
            Name = "Silver Bar";
            Width = 20;
            Height = 20;
            MaxStack = 0x63;
        }
        else if (Type == 0x16)
        {
            Name = "Iron Bar";
            Width = 20;
            Height = 20;
            MaxStack = 0x63;
        }
        else if (Type == 0x17)
        {
            Name = "Gel";
            Width = 20;
            Height = 12;
            MaxStack = 12;
            Alpha = 0xaf;
            Color = new Color(0, 80, 0xff, 100);
        }
        else if (Type == 0x18)
        {
            Name = "Wooden Sword";
            UseStyle = 1;
            UseTurn = false;
            UseAnimation = 0x19;
            Width = 0x18;
            Height = 0x1c;
            Damage = 7;
            KnockBack = 4f;
            Scale = 0.95f;
            UseSound = 1;
        }
        else if (Type == 0x19)
        {
            Name = "Wooden Door";
            UseStyle = 1;
            UseTurn = true;
            UseAnimation = 15;
            UseTime = 10;
            MaxStack = 0x63;
            Consumable = true;
            CreateTile = 10;
            Width = 14;
            Height = 0x1c;
        }
        else if (Type == 0x1a)
        {
            Name = "Stone Wall";
            UseStyle = 1;
            UseTurn = true;
            UseAnimation = 15;
            UseTime = 10;
            AutoReuse = true;
            MaxStack = 0x63;
            Consumable = true;
            CreateWall = 1;
            Width = 12;
            Height = 12;
        }
    }

    public void SetDefaults(string itemName)
    {
        if (itemName == "Gold Pickaxe")
        {
            SetDefaults(1);
            Color = new Color(210, 190, 0, 100);
            UseTime = 13;
            Pick = 0x41;
            UseAnimation = 0x13;
            Scale = 1.1f;
            Damage = 7;
        }

        if (itemName == "Gold Broadsword")
        {
            SetDefaults(4);
            Color = new Color(210, 190, 0, 100);
            UseAnimation = 0x13;
            Damage = 15;
            Scale = 1.1f;
        }

        if (itemName == "Gold Shortsword")
        {
            SetDefaults(6);
            Color = new Color(210, 190, 0, 100);
            Damage = 12;
            UseAnimation = 10;
            Scale = 1f;
        }

        if (itemName == "Gold Axe")
        {
            SetDefaults(10);
            Color = new Color(210, 190, 0, 100);
            UseTime = 0x11;
            Axe = 13;
            UseAnimation = 0x19;
            Scale = 1.2f;
            Damage = 8;
        }

        if (itemName == "Gold Hammer")
        {
            SetDefaults(7);
            Color = new Color(210, 190, 0, 100);
            UseAnimation = 0x1b;
            UseTime = 0x11;
            Hammer = 0x41;
            Scale = 1.3f;
            Damage = 10;
        }

        if (itemName == "Silver Pickaxe")
        {
            SetDefaults(1);
            Color = new Color(180, 180, 180, 100);
            UseTime = 0x11;
            Pick = 0x37;
            UseAnimation = 20;
            Scale = 1.05f;
            Damage = 6;
        }

        if (itemName == "Silver Broadsword")
        {
            SetDefaults(4);
            Color = new Color(180, 180, 180, 100);
            UseAnimation = 20;
            Damage = 13;
            Scale = 1.05f;
        }

        if (itemName == "Silver Shortsword")
        {
            SetDefaults(6);
            Color = new Color(180, 180, 180, 100);
            Damage = 11;
            UseAnimation = 11;
            Scale = 0.95f;
        }

        if (itemName == "Silver Axe")
        {
            SetDefaults(10);
            Color = new Color(180, 180, 180, 100);
            UseTime = 0x12;
            Axe = 11;
            UseAnimation = 0x1a;
            Scale = 1.15f;
            Damage = 7;
        }

        if (itemName == "Silver Hammer")
        {
            SetDefaults(7);
            Color = new Color(180, 180, 180, 100);
            UseAnimation = 0x1c;
            UseTime = 0x17;
            Scale = 1.25f;
            Damage = 9;
            Hammer = 0x37;
        }

        if (itemName == "Copper Pickaxe")
        {
            SetDefaults(1);
            Color = new Color(180, 100, 0x2d, 80);
            UseTime = 15;
            Pick = 0x23;
            UseAnimation = 0x17;
            Scale = 0.9f;
            TileBoost = -1;
            Damage = 2;
        }

        if (itemName == "Copper Broadsword")
        {
            SetDefaults(4);
            Color = new Color(180, 100, 0x2d, 80);
            UseAnimation = 0x17;
            Damage = 8;
            Scale = 0.9f;
        }

        if (itemName == "Copper Shortsword")
        {
            SetDefaults(6);
            Color = new Color(180, 100, 0x2d, 80);
            Damage = 6;
            UseAnimation = 13;
            Scale = 0.8f;
        }

        if (itemName == "Copper Axe")
        {
            SetDefaults(10);
            Color = new Color(180, 100, 0x2d, 80);
            UseTime = 0x15;
            Axe = 8;
            UseAnimation = 30;
            Scale = 1f;
            Damage = 3;
            TileBoost = -1;
        }

        if (itemName == "Copper Hammer")
        {
            SetDefaults(7);
            Color = new Color(180, 100, 0x2d, 80);
            UseAnimation = 0x21;
            UseTime = 0x17;
            Scale = 1.1f;
            Damage = 4;
            Hammer = 0x23;
            TileBoost = -1;
        }

        Name = itemName;
    }

    public void UpdateItem(int i)
    {
        if (!BeingGrabbed)
        {
            Velocity.Y += Gravity;
            if (Velocity.Y > MaxFallSpeed)
                Velocity.Y = MaxFallSpeed;

            Velocity.X *= 0.95f;
            if (Velocity.X < 0.1 && Velocity.X > -0.1)
                Velocity.X = 0f;

            Velocity = Collision.TileCollision(
                Position,
                Velocity,
                Width,
                Height
            );
        }
        else
            BeingGrabbed = false;

        if (Type == 8)
        {
            Lighting.AddLight(
                (int)((Position.X - 7f) / 16f),
                (int)((Position.Y - 7f) / 16f),
                0xff
            );
        }

        Position += Velocity;
        if (NoGrabDelay > 0)
            NoGrabDelay--;
    }
}
