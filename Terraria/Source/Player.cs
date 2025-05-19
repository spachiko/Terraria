using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Terraria;

public class Player
{
    private static int tileTargetX;
    private static int tileTargetY;
    private static readonly float Gravity = 0.4f;
    private static readonly float ItemGrabSpeed = 0.4f;
    private static readonly float ItemGrabSpeedMax = 4f;
    private static readonly float JumpSpeed = 5.01f;
    private static readonly float MaxFallSpeed = 10f;
    private static readonly float MaxRunSpeed = 3f;
    private static readonly float RunAcceleration = 0.08f;
    private static readonly float RunSlowdown = 0.2f;
    private static readonly int ItemGrabRange = 0x20;
    private static readonly int JumpHeight = 15;
    private static readonly int TileRangeX = 5;
    private static readonly int TileRangeY = 4;
    private readonly int _head = Main.Rand.Next(2);
    public readonly Item[] Armor = new Item[3];
    public readonly int Height = 0x2a;
    public readonly Item[] Inventory = new Item[40];
    public readonly int StatLifeMax = 100;
    public readonly int Width = 20;
    private int _body;
    private Vector2 _bodyVelocity;
    private bool _controlJump;
    private bool _controlLeft;
    private bool _controlRight;
    private bool _controlUseItem;
    private bool _controlUseTile;
    private Vector2 _headVelocity;
    private int _hitTile;
    private int _hitTileX;
    private int _hitTileY;
    private bool _immune;
    private int _immuneAlpha;
    private int _immuneAlphaDirection;
    private int _immuneTime;
    private int _itemAnimationMax;
    private int _itemTime;
    private int _jump;
    private double _legFrameCounter;
    private int _legs;
    private Vector2 _legVelocity;
    private bool _releaseInventory;
    private bool _releaseJump;
    private bool _releaseUseTile;
    private int _respawnTimer;
    private int _statDefense = 10;
    private Vector2 _velocity;
    public bool Active;
    public int ActiveNpCs;
    public Rectangle BodyFrame;
    public double BodyFrameCounter;
    public Vector2 BodyPosition;
    public float BodyRotation;
    public int ChangeItem = -1;
    public bool ControlDown;
    public bool ControlUp;
    public bool Dead;
    public bool DelayUseItem;
    public int Direction = 1;
    public Rectangle HeadFrame;
    public double HeadFrameCounter;
    public Vector2 HeadPosition;
    public float HeadRotation;
    public int ItemAnimation;
    public int ItemHeight;
    public Vector2 ItemLocation;
    public float ItemRotation;
    public int ItemWidth;
    public Rectangle LegFrame;
    public Vector2 LegPosition;
    public float LegRotation;
    public bool MouseInterface;
    public string Name = "";
    public Vector2 Position;
    public bool ReleaseUseItem;
    public int SelectedItem;
    public bool ShowItemIcon;
    public int ShowItemIcon2;
    public int StatAttack = 0;
    public int StatLife = 100;

    private void DropItems()
    {
        for (var i = 10; i < 40; i++)
        {
            var index = Item.NewItem(
                (int)Position.X,
                (int)Position.Y,
                Width,
                Height,
                Inventory[i].Type
            );

            Inventory[i].Position = Main.Item[index].Position;
            Main.Item[index] = Inventory[i];
            Inventory[i] = new Item();
            SelectedItem = 0;
            Main.Item[index].Velocity.Y = Main.Rand.Next(-20, 1) * 0.1f;
            Main.Item[index].Velocity.X = Main.Rand.Next(-20, 0x15) * 0.1f;
            Main.Item[index].NoGrabDelay = 100;
        }
    }

    public Color GetDeathAlpha(Color newColor)
    {
        var r = newColor.R + (int)(_immuneAlpha * 0.9);
        var g = newColor.G + (int)(_immuneAlpha * 0.5);
        var b = newColor.B + (int)(_immuneAlpha * 0.5);
        var a = newColor.A + (int)(_immuneAlpha * 0.4);
        if (a < 0)
            a = 0;

        if (a > 0xff)
            a = 0xff;

        return new Color(r, g, b, a);
    }

    public Color GetImmuneAlpha(Color newColor)
    {
        var r = newColor.R - _immuneAlpha;
        var g = newColor.G - _immuneAlpha;
        var b = newColor.B - _immuneAlpha;
        var a = newColor.A - _immuneAlpha;
        if (a < 0)
            a = 0;

        if (a > 0xff)
            a = 0xff;

        return new Color(r, g, b, a);
    }

    private Item GetItem(int plr, Item newItem)
    {
        Item item = newItem;
        if (newItem.NoGrabDelay <= 0)
        {
            int num;
            for (num = 0; num < 40; num++)
                if (
                    Inventory[num].Type > 0
                    && Inventory[num].Stack < Inventory[num].MaxStack && item.IsTheSameAs(Inventory[num])
                )
                {
                    Main.soundInstanceGrab.Stop();
                    Main.soundInstanceGrab = Main.soundGrab.CreateInstance();
                    Main.soundInstanceGrab.Play();
                    if (
                        item.Stack + Inventory[num].Stack <= Inventory[num].MaxStack
                    )
                    {
                        Item item1 = Inventory[num];
                        item1.Stack += item.Stack;
                        if (plr == Main.MyPlayer)
                            Recipe.FindRecipes();

                        return new Item();
                    }

                    item.Stack -= Inventory[num].MaxStack - Inventory[num].Stack;
                    Inventory[num].Stack = Inventory[num].MaxStack;
                    if (plr == Main.MyPlayer)
                        Recipe.FindRecipes();
                }

            for (num = 0; num < 40; num++)
                if (Inventory[num].Type == 0)
                {
                    Inventory[num] = item;
                    Main.soundInstanceGrab.Stop();
                    Main.soundInstanceGrab = Main.soundGrab.CreateInstance();
                    Main.soundInstanceGrab.Play();
                    if (plr == Main.MyPlayer)
                        Recipe.FindRecipes();

                    return new Item();
                }
        }

        return item;
    }

    private void Hurt(int damage, int hitDirection)
    {
        if (!_immune && !Main.GodMode)
        {
            var num = Main.CalculateDamage(damage, _statDefense);
            if (num >= 1.0)
            {
                int num3;
                Color color;
                StatLife -= (int)num;
                _immune = true;
                _immuneTime = 40;
                _velocity.X = 4.5f * hitDirection;
                _velocity.Y = -3.5f;
                var index = Main.Rand.Next(3);
                Main.SoundInstancePlayerHit[index].Stop();
                Main.SoundInstancePlayerHit[index] = Main.SoundPlayerHit[index]
                    .CreateInstance();

                Main.SoundInstancePlayerHit[index].Play();
                if (StatLife > 0)
                {
                    for (num3 = 0; num3 < num / StatLifeMax * 100.0; num3++)
                    {
                        color = new Color();
                        Dust.NewDust(
                            Position,
                            Width,
                            Height,
                            5,
                            2 * hitDirection,
                            -2f,
                            0,
                            color
                        );
                    }
                }
                else
                {
                    DropItems();
                    Main.soundPlayerKilled.Play();
                    _headVelocity.Y = Main.Rand.Next(-40, -10) * 0.1f;
                    _bodyVelocity.Y = Main.Rand.Next(-40, -10) * 0.1f;
                    _legVelocity.Y = Main.Rand.Next(-40, -10) * 0.1f;
                    _headVelocity.X =
                        Main.Rand.Next(-20, 0x15) * 0.1f + 2 * hitDirection;

                    _bodyVelocity.X =
                        Main.Rand.Next(-20, 0x15) * 0.1f + 2 * hitDirection;

                    _legVelocity.X =
                        Main.Rand.Next(-20, 0x15) * 0.1f + 2 * hitDirection;

                    for (
                        num3 = 0;
                        num3 < 20.0 + num / StatLifeMax * 100.0;
                        num3++
                    )
                    {
                        color = new Color();
                        Dust.NewDust(
                            Position,
                            Width,
                            Height,
                            5,
                            2 * hitDirection,
                            -2f,
                            0,
                            color
                        );
                    }

                    Dead = true;
                    _respawnTimer = 300;
                    _immuneAlpha = 0;
                }
            }
        }
    }

    private void ItemCheck(int i)
    {
        if (Inventory[SelectedItem].AutoReuse)
            ReleaseUseItem = true;

        if (
            _controlUseItem && ItemAnimation == 0 && ReleaseUseItem
            && Inventory[SelectedItem].UseStyle > 0
        )
        {
            ItemAnimation = Inventory[SelectedItem].UseAnimation;
            _itemAnimationMax = ItemAnimation;
            if (Inventory[SelectedItem].UseSound > 0)
                Main.SoundInstanceItem[Inventory[SelectedItem].UseSound].Play();
        }

        if (ItemAnimation > 0)
        {
            ItemHeight = Main.ItemTexture[Inventory[SelectedItem].Type].Height;
            ItemWidth = Main.ItemTexture[Inventory[SelectedItem].Type].Width;
            ItemAnimation--;
            if (Inventory[SelectedItem].UseStyle == 1)
            {
                if (
                    ItemAnimation
                    < Inventory[SelectedItem].UseAnimation * 0.333
                )
                {
                    ItemLocation.X =
                        Position.X + Width * 0.5f
                                   + (
                                       Main.ItemTexture[
                                           Inventory[SelectedItem].Type
                                       ].Width * 0.5f - 4f
                                   ) * Direction;

                    ItemLocation.Y = Position.Y + 24f;
                }
                else if (
                    ItemAnimation
                    < Inventory[SelectedItem].UseAnimation * 0.666
                )
                {
                    ItemLocation.X =
                        Position.X + Width * 0.5f
                                   + (
                                       Main.ItemTexture[
                                           Inventory[SelectedItem].Type
                                       ].Width * 0.5f - 10f
                                   ) * Direction;

                    ItemLocation.Y = Position.Y + 10f;
                }
                else
                {
                    ItemLocation.X =
                        Position.X + Width * 0.5f
                        - (
                            Main.ItemTexture[
                                Inventory[SelectedItem].Type
                            ].Width * 0.5f - 4f
                        ) * Direction;

                    ItemLocation.Y = Position.Y + 6f;
                }

                ItemRotation =
                (
                    ItemAnimation
                    / (float)Inventory[SelectedItem].UseAnimation - 0.5f
                ) * -Direction * 3.5f - Direction * 0.3f;
            }
            else if (Inventory[SelectedItem].UseStyle == 2)
            {
                ItemRotation =
                    ItemAnimation
                    / (float)Inventory[SelectedItem].UseAnimation * Direction * 2f + -1.4f * Direction;

                if (ItemAnimation < Inventory[SelectedItem].UseAnimation * 0.5)
                {
                    ItemLocation.X =
                        Position.X + Width * 0.5f
                                   + (
                                       Main.ItemTexture[
                                           Inventory[SelectedItem].Type
                                       ].Width * 0.5f - 9f - ItemRotation * 12f * Direction
                                   ) * Direction;

                    ItemLocation.Y =
                        Position.Y + 38f + ItemRotation * Direction * 4f;
                }
                else
                {
                    ItemLocation.X =
                        Position.X + Width * 0.5f
                                   + (
                                       Main.ItemTexture[
                                           Inventory[SelectedItem].Type
                                       ].Width * 0.5f - 9f - ItemRotation * 16f * Direction
                                   ) * Direction;

                    ItemLocation.Y =
                        Position.Y + 38f + ItemRotation * Direction;
                }
            }
            else if (Inventory[SelectedItem].UseStyle == 3)
            {
                if (
                    ItemAnimation
                    > Inventory[SelectedItem].UseAnimation * 0.666
                )
                {
                    ItemLocation.X = -1000f;
                    ItemLocation.Y = -1000f;
                    ItemRotation = -1.3f * Direction;
                }
                else
                {
                    ItemLocation.X =
                        Position.X + Width * 0.5f
                                   + (
                                       Main.ItemTexture[
                                           Inventory[SelectedItem].Type
                                       ].Width * 0.5f - 4f
                                   ) * Direction;

                    ItemLocation.Y = Position.Y + 24f;
                    var num =
                        ItemAnimation
                        / (float)
                        Inventory[
                            SelectedItem
                        ].UseAnimation
                        * Main.ItemTexture[
                            Inventory[SelectedItem].Type
                        ].Width * Direction * Inventory[SelectedItem].Scale * 1.2f - 10 * Direction;

                    if (num > -4f && Direction == -1)
                        num = -4f;

                    if (num < 4f && Direction == 1)
                        num = 4f;

                    ItemLocation.X -= num;
                    ItemRotation = 0.8f * Direction;
                }
            }
        }
        else if (Inventory[SelectedItem].HoldStyle == 1)
        {
            ItemLocation.X =
                Position.X + Width * 0.5f
                           + (
                               Main.ItemTexture[Inventory[SelectedItem].Type].Width * 0.5f
                               + 4f
                           ) * Direction;

            ItemLocation.Y = Position.Y + 24f;
            ItemRotation = 0f;
        }

        if (Inventory[SelectedItem].Type == 8)
        {
            var maxValue = 20;
            if (ItemAnimation > 0)
                maxValue = 7;

            if (Direction == -1)
            {
                if (Main.Rand.Next(maxValue) == 0)
                {
                    Dust.NewDust(
                        new Vector2(ItemLocation.X - 16f, ItemLocation.Y - 14f),
                        4,
                        4,
                        6,
                        0f,
                        0f,
                        100
                    );
                }

                Lighting.AddLight(
                    (int)((ItemLocation.X - 16f + _velocity.X) / 16f),
                    (int)((ItemLocation.Y - 14f) / 16f),
                    0xff
                );
            }
            else
            {
                Main.screenPosition.X =
                    Position.X + Width * 0.5f - Main.ScreenWidth * 0.5f;

                Main.screenPosition.Y =
                    Position.Y + Height * 0.5f - Main.ScreenHeight * 0.5f;

                if (Main.Rand.Next(maxValue) == 0)
                {
                    Dust.NewDust(
                        new Vector2(ItemLocation.X + 6f, ItemLocation.Y - 14f),
                        4,
                        4,
                        6,
                        0f,
                        0f,
                        100
                    );
                }

                Lighting.AddLight(
                    (int)((ItemLocation.X + 6f + _velocity.X) / 16f),
                    (int)((ItemLocation.Y - 14f) / 16f),
                    0xff
                );
            }
        }

        ReleaseUseItem = !_controlUseItem;

        if (_itemTime > 0)
            _itemTime--;

        if (i == Main.MyPlayer)
        {
            if (
                (
                    Inventory[SelectedItem].Pick > 0
                    || Inventory[SelectedItem].Axe > 0 || Inventory[SelectedItem].Hammer > 0
                )
                && Position.X / 16f - TileRangeX
                                    - Inventory[SelectedItem].TileBoost <= tileTargetX
                && (Position.X + Width) / 16f + TileRangeX
                                              + Inventory[SelectedItem].TileBoost - 1f >= tileTargetX
                && Position.Y / 16f - TileRangeY
                                    - Inventory[SelectedItem].TileBoost <= tileTargetY
                && (Position.Y + Height) / 16f + TileRangeY
                                               + Inventory[SelectedItem].TileBoost - 2f >= tileTargetY
            )
            {
                ShowItemIcon = true;
                if (
                    Main.Tile[tileTargetX, tileTargetY].Active
                    && _itemTime == 0 && ItemAnimation > 0
                    && _controlUseItem
                )
                {
                    if (_hitTileX != tileTargetX || _hitTileY != tileTargetY)
                    {
                        _hitTile = 0;
                        _hitTileX = tileTargetX;
                        _hitTileY = tileTargetY;
                    }

                    if (Main.TileNoFail[Main.Tile[tileTargetX, tileTargetY].Type])
                        _hitTile = 100;

                    if (
                        Main.Tile[tileTargetX, tileTargetY].Type == 5
                        || Main.Tile[tileTargetX, tileTargetY].Type == 10 || Main.Tile[tileTargetX, tileTargetY].Type == 11
                    )
                    {
                        _hitTile += Inventory[SelectedItem].Axe;
                        if (Inventory[SelectedItem].Axe > 0)
                        {
                            if (_hitTile >= 100)
                            {
                                _hitTile = 0;
                                WorldGen.KillTile(tileTargetX, tileTargetY);
                            }
                            else
                                WorldGen.KillTile(tileTargetX, tileTargetY, true);

                            _itemTime = Inventory[SelectedItem].UseTime;
                        }
                    }
                    else if (Inventory[SelectedItem].Pick > 0)
                    {
                        _hitTile += Inventory[SelectedItem].Pick;
                        if (_hitTile >= 100)
                        {
                            _hitTile = 0;
                            WorldGen.KillTile(tileTargetX, tileTargetY);
                        }
                        else
                            WorldGen.KillTile(tileTargetX, tileTargetY, true);

                        _itemTime = Inventory[SelectedItem].UseTime;
                    }
                }

                if (
                    Main.Tile[tileTargetX, tileTargetY].Wall > 0
                    && _itemTime == 0 && ItemAnimation > 0
                    && _controlUseItem && Inventory[SelectedItem].Hammer > 0
                )
                {
                    if (_hitTileX != tileTargetX || _hitTileY != tileTargetY)
                    {
                        _hitTile = 0;
                        _hitTileX = tileTargetX;
                        _hitTileY = tileTargetY;
                    }

                    _hitTile += Inventory[SelectedItem].Hammer;
                    if (_hitTile >= 100)
                    {
                        _hitTile = 0;
                        WorldGen.KillWall(tileTargetX, tileTargetY);
                    }
                    else
                        WorldGen.KillWall(tileTargetX, tileTargetY, true);

                    _itemTime = Inventory[SelectedItem].UseTime;
                }
            }

            if (Inventory[SelectedItem].CreateTile >= 0)
            {
                tileTargetX = (int)((Main.mouseState.X + Main.screenPosition.X) / 16f);
                tileTargetY = (int)((Main.mouseState.Y + Main.screenPosition.Y) / 16f);
                if (
                    Position.X / 16f - TileRangeX
                                     - Inventory[SelectedItem].TileBoost <= tileTargetX
                    && (Position.X + Width) / 16f + TileRangeX
                                                  + Inventory[SelectedItem].TileBoost - 1f >= tileTargetX
                    && Position.Y / 16f - TileRangeY
                                        - Inventory[SelectedItem].TileBoost <= tileTargetY
                    && (Position.Y + Height) / 16f + TileRangeY
                                                   + Inventory[SelectedItem].TileBoost - 2f >= tileTargetY
                )
                {
                    ShowItemIcon = true;
                    if (
                        !Main.Tile[tileTargetX, tileTargetY].Active
                        && _itemTime == 0 && ItemAnimation > 0
                        && _controlUseItem
                        && (
                            Main.Tile[tileTargetX + 1, tileTargetY].Active
                            || Main.Tile[tileTargetX + 1, tileTargetY].Wall > 0
                            || Main.Tile[tileTargetX - 1, tileTargetY].Active
                            || Main.Tile[tileTargetX - 1, tileTargetY].Wall > 0
                            || Main.Tile[tileTargetX, tileTargetY + 1].Active
                            || Main.Tile[tileTargetX, tileTargetY + 1].Wall > 0 ||
                            Main.Tile[tileTargetX, tileTargetY - 1].Active || Main.Tile[tileTargetX, tileTargetY - 1].Wall > 0
                        )
                    )
                    {
                        WorldGen.PlaceTile(
                            tileTargetX,
                            tileTargetY,
                            Inventory[SelectedItem].CreateTile
                        );

                        if (Main.Tile[tileTargetX, tileTargetY].Active)
                            _itemTime = Inventory[SelectedItem].UseTime;
                    }
                }
            }

            if (Inventory[SelectedItem].CreateWall >= 0)
            {
                tileTargetX = (int)((Main.mouseState.X + Main.screenPosition.X) / 16f);
                tileTargetY = (int)((Main.mouseState.Y + Main.screenPosition.Y) / 16f);
                if (
                    Position.X / 16f - TileRangeX
                                     - Inventory[SelectedItem].TileBoost <= tileTargetX
                    && (Position.X + Width) / 16f + TileRangeX
                                                  + Inventory[SelectedItem].TileBoost - 1f >= tileTargetX
                    && Position.Y / 16f - TileRangeY
                                        - Inventory[SelectedItem].TileBoost <= tileTargetY
                    && (Position.Y + Height) / 16f + TileRangeY
                                                   + Inventory[SelectedItem].TileBoost - 2f >= tileTargetY
                )
                {
                    ShowItemIcon = true;
                    if (
                        _itemTime == 0 && ItemAnimation > 0
                                       && _controlUseItem
                                       && (
                                           Main.Tile[tileTargetX + 1, tileTargetY].Active
                                           || Main.Tile[tileTargetX + 1, tileTargetY].Wall > 0
                                           || Main.Tile[tileTargetX - 1, tileTargetY].Active
                                           || Main.Tile[tileTargetX - 1, tileTargetY].Wall > 0
                                           || Main.Tile[tileTargetX, tileTargetY + 1].Active
                                           || Main.Tile[tileTargetX, tileTargetY + 1].Wall > 0 ||
                                           Main.Tile[tileTargetX, tileTargetY - 1].Active ||
                                           Main.Tile[tileTargetX, tileTargetY - 1].Wall > 0
                                       )
                                       && Main.Tile[tileTargetX, tileTargetY].Wall
                                       != Inventory[SelectedItem].CreateWall
                    )
                    {
                        WorldGen.PlaceWall(
                            tileTargetX,
                            tileTargetY,
                            Inventory[SelectedItem].CreateWall
                        );

                        if (
                            Main.Tile[tileTargetX, tileTargetY].Wall
                            == Inventory[SelectedItem].CreateWall
                        )
                            _itemTime = Inventory[SelectedItem].UseTime;
                    }
                }
            }

            if (
                Inventory[SelectedItem].Damage >= 0
                && Inventory[SelectedItem].Type > 0 && ItemAnimation > 0
            )
            {
                Rectangle rectangle = new Rectangle();
                var flag = false;
                rectangle = new Rectangle(
                    (int)ItemLocation.X,
                    (int)ItemLocation.Y,
                    Main.ItemTexture[Inventory[SelectedItem].Type].Width,
                    Main.ItemTexture[Inventory[SelectedItem].Type].Height
                )
                {
                    Width = (int)(
                        rectangle.Width * Inventory[SelectedItem].Scale
                    ),
                    Height = (int)(
                        rectangle.Height * Inventory[SelectedItem].Scale
                    )
                };

                if (Direction == -1)
                    rectangle.X -= rectangle.Width;

                rectangle.Y -= rectangle.Height;
                if (Inventory[SelectedItem].UseStyle == 1)
                {
                    if (
                        ItemAnimation
                        < Inventory[SelectedItem].UseAnimation * 0.333
                    )
                    {
                        if (Direction == -1)
                            rectangle.X -= (int)(rectangle.Width * 1.4) - rectangle.Width;

                        rectangle.Width = (int)(rectangle.Width * 1.4);
                        rectangle.Y += (int)(rectangle.Height * 0.5);
                        rectangle.Height = (int)(rectangle.Height * 1.1);
                    }
                    else if (
                        ItemAnimation
                        >= Inventory[SelectedItem].UseAnimation * 0.666
                    )
                    {
                        if (Direction == 1)
                            rectangle.X -= rectangle.Width * 2;

                        rectangle.Width *= 2;
                        rectangle.Y -= (int)(rectangle.Height * 1.4) - rectangle.Height;
                        rectangle.Height = (int)(rectangle.Height * 1.4);
                    }
                }
                else if (Inventory[SelectedItem].UseStyle == 3)
                {
                    if (
                        ItemAnimation
                        > Inventory[SelectedItem].UseAnimation * 0.666
                    )
                        flag = true;
                    else
                    {
                        if (Direction == -1)
                            rectangle.X -= (int)(rectangle.Width * 1.4) - rectangle.Width;

                        rectangle.Width = (int)(rectangle.Width * 1.4);
                        rectangle.Y += (int)(rectangle.Height * 0.6);
                        rectangle.Height = (int)(rectangle.Height * 0.6);
                    }
                }

                if (!flag)
                {
                    var num3 = rectangle.X / 0x10;
                    var num4 = (rectangle.X + rectangle.Width) / 0x10 + 1;
                    var num5 = rectangle.Y / 0x10;
                    var num6 = (rectangle.Y + rectangle.Height) / 0x10 + 1;
                    for (var j = num3; j < num4; j++)
                    for (var m = num5; m < num6; m++)
                        if (Main.Tile[j, m].Type == 3)
                            WorldGen.KillTile(j, m);

                    for (var k = 0; k < 0x3e8; k++)
                        if (Main.Npc[k].Active && Main.Npc[k].Immune[i] == 0)
                        {
                            Rectangle rectangle2 = new Rectangle(
                                (int)Main.Npc[k].Position.X,
                                (int)Main.Npc[k].Position.Y,
                                Main.Npc[k].Width,
                                Main.Npc[k].Height
                            );

                            if (rectangle.Intersects(rectangle2))
                            {
                                Main.Npc[k]
                                    .StrikeNpc(
                                        Inventory[SelectedItem].Damage,
                                        Inventory[SelectedItem].KnockBack,
                                        Direction
                                    );

                                Main.Npc[k].Immune[i] = ItemAnimation;
                            }
                        }
                }
            }

            if (
                _itemTime == 0 && Inventory[SelectedItem].HealLife > 0
                               && ItemAnimation > 0
            )
            {
                StatLife += Inventory[SelectedItem].HealLife;
                _itemTime = Inventory[SelectedItem].UseTime;
            }

            if (
                _itemTime == Inventory[SelectedItem].UseTime
                && Inventory[SelectedItem].Consumable
            )
            {
                Item item1 = Inventory[SelectedItem];
                item1.Stack--;
                if (Inventory[SelectedItem].Stack <= 0)
                    _itemTime = ItemAnimation;
            }

            if (Inventory[SelectedItem].Stack <= 0 && ItemAnimation == 0)
                Inventory[SelectedItem] = new Item();
        }
    }

    private bool ItemSpace(Item newItem)
    {
        int num;
        for (num = 0; num < 40; num++)
            if (Inventory[num].Type == 0)
                return true;

        for (num = 0; num < 40; num++)
            if (
                Inventory[num].Type > 0
                && Inventory[num].Stack < Inventory[num].MaxStack && newItem.IsTheSameAs(Inventory[num])
            )
                return true;

        return false;
    }

    private void PlayerFrame()
    {
        HeadFrame.X = 0x22 * _head;
        BodyFrame.X = 0x22 * _body;
        LegFrame.X = 0x22 * _legs;
        HeadFrame.Y = 0;
        if (ItemAnimation > 0)
        {
            if (
                Inventory[SelectedItem].UseStyle == 1
                || Inventory[SelectedItem].Type == 0
            )
            {
                if (ItemAnimation < _itemAnimationMax * 0.333)
                    BodyFrame.Y = 200;
                else if (ItemAnimation < _itemAnimationMax * 0.666)
                    BodyFrame.Y = 150;
                else
                    BodyFrame.Y = 100;
            }
            else if (Inventory[SelectedItem].UseStyle == 2)
                BodyFrame.Y = ItemAnimation < _itemAnimationMax * 0.5 ? 150 : 200;
            else if (Inventory[SelectedItem].UseStyle == 3)
                BodyFrame.Y = ItemAnimation > _itemAnimationMax * 0.666 ? 100 : 200;
        }
        else if (Inventory[SelectedItem].HoldStyle == 1)
            BodyFrame.Y = 200;
        else if (_velocity.Y < 0f)
            BodyFrame.Y = 50;
        else if (_velocity.Y > 0f)
            BodyFrame.Y = 50;
        else
            BodyFrame.Y = 0;

        if (_velocity.Y < 0f)
            LegFrame.Y = 100;
        else if (_velocity.Y > 0f)
            LegFrame.Y = 100;
        else if (_velocity.X != 0f)
        {
            if (
                (Direction < 0 && _velocity.X > 0f)
                || (Direction > 0 && _velocity.X < 0f)
            )
                _legFrameCounter = 12.0;

            _legFrameCounter += 0.4 + Math.Abs(_velocity.X * 0.4);
            if (_legFrameCounter < 6.0)
                LegFrame.Y = 0;
            else if (_legFrameCounter < 12.0)
                LegFrame.Y = 50;
            else if (_legFrameCounter < 18.0)
                LegFrame.Y = 100;
            else if (_legFrameCounter < 24.0)
                LegFrame.Y = 50;
            else
            {
                LegFrame.Y = 0;
                _legFrameCounter = 0.0;
            }
        }
        else
        {
            _legFrameCounter = 6.0;
            LegFrame.Y = 0;
        }
    }

    public static void SetupPlayers()
    {
        for (var i = 0; i < 0x10; i++)
        {
            Main.Player[i] = new Player
            {
                Name = "Some n00b",
                Armor =
                {
                    [0] = new Item(),
                    [1] = new Item(),
                    [2] = new Item()
                }
            };

            for (var j = 0; j < 40; j++)
                Main.Player[i].Inventory[j] = new Item();

            Main.Player[i].Inventory[0].SetDefaults("Copper Pickaxe");
            Main.Player[i].Inventory[1].SetDefaults("Copper Axe");
            Main.Player[i].Inventory[2].SetDefaults("Copper Hammer");
            Main.Player[i].Inventory[30].SetDefaults(0x10);
            Main.Player[i].Inventory[0x1f].SetDefaults(0x12);
            Main.Player[i].Armor[1].SetDefaults(15);
            Main.Player[i].Armor[2].SetDefaults(0x11);
        }
    }

    public void Spawn()
    {
        HeadPosition = new Vector2();
        BodyPosition = new Vector2();
        LegPosition = new Vector2();
        HeadRotation = 0f;
        BodyRotation = 0f;
        LegRotation = 0f;
        StatLife = StatLifeMax;
        _immune = true;
        Dead = false;
        _immuneTime = 0;
        Active = true;
        Position.X = Main.spawnTileX * 0x10 + 8 - Width / 2;
        Position.Y = Main.spawnTileY * 0x10 - Height;
        _velocity.X = 0f;
        _velocity.Y = 0f;
        for (var i = Main.spawnTileX - 1; i < Main.spawnTileX + 2; i++)
        for (var j = Main.spawnTileY - 3; j < Main.spawnTileY; j++)
            if (Main.TileSolid[Main.Tile[i, j].Type])
            {
                Main.Tile[i, j].Active = false;
                Main.Tile[i, j].Type = 0;
                WorldGen.SquareTileFrame(i, j);
            }

        Main.screenPosition.X = Position.X + Width / 2 - Main.ScreenWidth / 2;
        Main.screenPosition.Y = Position.Y + Height / 2 - Main.ScreenHeight / 2;
    }

    public void UpdatePlayer(int i)
    {
        if (Active)
        {
            if (Dead)
            {
                ChangeItem = -1;
                ItemAnimation = 0;
                _immuneAlpha += 2;
                if (_immuneAlpha > 0xff)
                    _immuneAlpha = 0xff;

                _respawnTimer--;
                HeadPosition += _headVelocity;
                BodyPosition += _bodyVelocity;
                LegPosition += _legVelocity;
                HeadRotation += _headVelocity.X * 0.1f;
                BodyRotation += _bodyVelocity.X * 0.1f;
                LegRotation += _legVelocity.X * 0.1f;
                _headVelocity.Y += 0.1f;
                _bodyVelocity.Y += 0.1f;
                _legVelocity.Y += 0.1f;
                _headVelocity.X *= 0.99f;
                _bodyVelocity.X *= 0.99f;
                _legVelocity.X *= 0.99f;
                if (_respawnTimer <= 0)
                    Spawn();
            }
            else
            {
                int num4;
                if (i == Main.MyPlayer)
                {
                    ControlUp = false;
                    _controlLeft = false;
                    ControlDown = false;
                    _controlRight = false;
                    _controlJump = false;
                    _controlUseItem = false;
                    _controlUseTile = false;
                    if (Main.keyState.IsKeyDown(Keys.W))
                        ControlUp = true;

                    if (Main.keyState.IsKeyDown(Keys.A))
                        _controlLeft = true;

                    if (Main.keyState.IsKeyDown(Keys.S))
                        ControlDown = true;

                    if (Main.keyState.IsKeyDown(Keys.D))
                        _controlRight = true;

                    if (Main.keyState.IsKeyDown(Keys.Space))
                        _controlJump = true;

                    if (
                        !(
                            Main.mouseState.LeftButton != ButtonState.Pressed
                            || MouseInterface
                        )
                    )
                        _controlUseItem = true;

                    if (
                        !(
                            Main.mouseState.RightButton != ButtonState.Pressed
                            || MouseInterface
                        )
                    )
                        _controlUseTile = true;

                    if (Main.keyState.IsKeyDown(Keys.Escape))
                    {
                        if (_releaseInventory)
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

                        _releaseInventory = false;
                    }
                    else
                        _releaseInventory = true;

                    if (DelayUseItem)
                    {
                        if (!_controlUseItem)
                            DelayUseItem = false;

                        _controlUseItem = false;
                    }

                    if (ItemAnimation == 0 && _itemTime == 0)
                    {
                        int num3;
                        if (
                            (
                                Main.keyState.IsKeyDown(Keys.Q)
                                && Inventory[SelectedItem].Type > 0
                            )
                            || (
                                (
                                    (
                                        Main.mouseState.LeftButton == ButtonState.Pressed
                                        && !MouseInterface && Main.mouseLeftRelease
                                    ) || !Main.playerInventory
                                ) && Main.mouseItem.Type > 0
                            )
                        )
                        {
                            Item item = new Item();
                            var flag = false;
                            if (
                                (
                                    (
                                        Main.mouseState.LeftButton == ButtonState.Pressed
                                        && !MouseInterface && Main.mouseLeftRelease
                                    ) || !Main.playerInventory
                                ) && Main.mouseItem.Type > 0
                            )
                            {
                                item = Inventory[SelectedItem];
                                Inventory[SelectedItem] = Main.mouseItem;
                                DelayUseItem = true;
                                _controlUseItem = false;
                                flag = true;
                            }

                            var index = Item.NewItem(
                                (int)Position.X,
                                (int)Position.Y,
                                Width,
                                Height,
                                Inventory[SelectedItem].Type
                            );

                            if (
                                !flag && Inventory[SelectedItem].Type == 8
                                      && Inventory[SelectedItem].Stack > 1
                            )
                            {
                                Item item1 = Inventory[SelectedItem];
                                item1.Stack--;
                            }
                            else
                            {
                                Inventory[SelectedItem].Position = Main.Item[
                                    index
                                ].Position;

                                Main.Item[index] = Inventory[SelectedItem];
                                Inventory[SelectedItem] = new Item();
                            }

                            Main.Item[index].NoGrabDelay = 100;
                            Main.Item[index].Velocity.Y = -2f;
                            Main.Item[index].Velocity.X =
                                4 * Direction + _velocity.X;

                            if (
                                (
                                    (
                                        Main.mouseState.LeftButton == ButtonState.Pressed
                                        && !MouseInterface
                                    ) || !Main.playerInventory
                                ) && Main.mouseItem.Type > 0
                            )
                            {
                                Inventory[SelectedItem] = item;
                                Main.mouseItem = new Item();
                            }
                            else
                            {
                                ItemAnimation = 10;
                                _itemAnimationMax = 10;
                            }

                            Recipe.FindRecipes();
                        }

                        if (!Main.playerInventory)
                        {
                            var selectedItem = SelectedItem;
                            if (Main.keyState.IsKeyDown(Keys.D1))
                                SelectedItem = 0;

                            if (Main.keyState.IsKeyDown(Keys.D2))
                                SelectedItem = 1;

                            if (Main.keyState.IsKeyDown(Keys.D3))
                                SelectedItem = 2;

                            if (Main.keyState.IsKeyDown(Keys.D4))
                                SelectedItem = 3;

                            if (Main.keyState.IsKeyDown(Keys.D5))
                                SelectedItem = 4;

                            if (Main.keyState.IsKeyDown(Keys.D6))
                                SelectedItem = 5;

                            if (Main.keyState.IsKeyDown(Keys.D7))
                                SelectedItem = 6;

                            if (Main.keyState.IsKeyDown(Keys.D8))
                                SelectedItem = 7;

                            if (Main.keyState.IsKeyDown(Keys.D9))
                                SelectedItem = 8;

                            if (Main.keyState.IsKeyDown(Keys.D0))
                                SelectedItem = 9;

                            if (selectedItem != SelectedItem)
                                Main.soundInstanceMenuTick.Play();

                            num3 =
                            (
                                Main.mouseState.ScrollWheelValue
                                - Main.oldMouseState.ScrollWheelValue
                            ) / 120;

                            while (num3 > 9)
                                num3 -= 10;

                            while (num3 < 0)
                                num3 += 10;

                            SelectedItem -= num3;
                            if (num3 != 0)
                                Main.soundInstanceMenuTick.Play();

                            if (ChangeItem >= 0)
                            {
                                if (SelectedItem != ChangeItem)
                                    Main.soundInstanceMenuTick.Play();

                                SelectedItem = ChangeItem;
                                ChangeItem = -1;
                            }

                            while (SelectedItem > 9)
                                SelectedItem -= 10;

                            while (SelectedItem < 0)
                                SelectedItem += 10;
                        }
                        else
                        {
                            num3 =
                            (
                                Main.mouseState.ScrollWheelValue
                                - Main.oldMouseState.ScrollWheelValue
                            ) / 120;

                            Main.focusRecipe += num3;
                            if (Main.focusRecipe > Main.numAvailableRecipes - 1)
                                Main.focusRecipe = Main.numAvailableRecipes - 1;

                            if (Main.focusRecipe < 0)
                                Main.focusRecipe = 0;
                        }
                    }
                }

                if (MouseInterface)
                    DelayUseItem = true;

                tileTargetX = (int)((Main.mouseState.X + Main.screenPosition.X) / 16f);
                tileTargetY = (int)((Main.mouseState.Y + Main.screenPosition.Y) / 16f);
                if (_immune)
                {
                    _immuneTime--;
                    if (_immuneTime <= 0)
                        _immune = false;

                    _immuneAlpha += _immuneAlphaDirection * 50;
                    if (_immuneAlpha <= 50)
                        _immuneAlphaDirection = 1;
                    else if (_immuneAlpha >= 0xcd)
                        _immuneAlphaDirection = -1;
                }
                else
                    _immuneAlpha = 0;

                _statDefense = 10;
                for (num4 = 0; num4 < 3; num4++)
                    _statDefense += Armor[num4].Defense;

                _body = Armor[1].BodySlot;
                _legs = Armor[2].LegSlot;
                HeadFrame.Width = 0x20;
                HeadFrame.Height = 0x30;
                BodyFrame.Width = 0x20;
                BodyFrame.Height = 0x30;
                LegFrame.Width = 0x20;
                LegFrame.Height = 0x30;
                if (_controlLeft && _velocity.X > -MaxRunSpeed)
                {
                    if (_velocity.X > RunSlowdown)
                        _velocity.X -= RunSlowdown;

                    _velocity.X -= RunAcceleration;
                    if (ItemAnimation == 0 || Inventory[SelectedItem].UseTurn)
                        Direction = -1;
                }
                else if (_controlRight && _velocity.X < MaxRunSpeed)
                {
                    if (_velocity.X < -RunSlowdown)
                        _velocity.X += RunSlowdown;

                    _velocity.X += RunAcceleration;
                    if (ItemAnimation == 0 || Inventory[SelectedItem].UseTurn)
                        Direction = 1;
                }
                else if (_velocity.Y == 0f)
                {
                    if (_velocity.X > RunSlowdown)
                        _velocity.X -= RunSlowdown;
                    else if (_velocity.X < -RunSlowdown)
                        _velocity.X += RunSlowdown;
                    else
                        _velocity.X = 0f;
                }
                else if (_velocity.X > RunSlowdown * 0.5)
                    _velocity.X -= RunSlowdown * 0.5f;
                else if (_velocity.X < -RunSlowdown * 0.5)
                    _velocity.X += RunSlowdown * 0.5f;
                else
                    _velocity.X = 0f;

                if (_controlJump)
                {
                    if (_jump > 0)
                    {
                        if (_velocity.Y > -JumpSpeed + Gravity * 2f)
                            _jump = 0;
                        else
                        {
                            _velocity.Y = -JumpSpeed;
                            _jump--;
                        }
                    }
                    else if (_velocity.Y == 0f && _releaseJump)
                    {
                        _velocity.Y = -JumpSpeed;
                        _jump = JumpHeight;
                    }

                    _releaseJump = false;
                }
                else
                {
                    _jump = 0;
                    _releaseJump = true;
                }

                _velocity.Y += Gravity;
                if (_velocity.Y > MaxFallSpeed)
                    _velocity.Y = MaxFallSpeed;

                for (num4 = 0; num4 < 0x3e8; num4++)
                    if (Main.Item[num4].Active && Main.Item[num4].NoGrabDelay == 0)
                    {
                        Rectangle rectangle2 = new Rectangle(
                            (int)Position.X,
                            (int)Position.Y,
                            Width,
                            Height
                        );

                        if (
                            rectangle2.Intersects(
                                new Rectangle(
                                    (int)Main.Item[num4].Position.X,
                                    (int)Main.Item[num4].Position.Y,
                                    Main.Item[num4].Width,
                                    Main.Item[num4].Height
                                )
                            )
                        )
                        {
                            if (
                                Inventory[SelectedItem].Type != 0
                                || ItemAnimation <= 0
                            )
                                Main.Item[num4] = GetItem(i, Main.Item[num4]);
                        }
                        else
                        {
                            rectangle2 = new Rectangle(
                                (int)Position.X - ItemGrabRange,
                                (int)Position.Y - ItemGrabRange,
                                Width + ItemGrabRange * 2,
                                Height + ItemGrabRange * 2
                            );

                            if (
                                rectangle2.Intersects(
                                    new Rectangle(
                                        (int)Main.Item[num4].Position.X,
                                        (int)Main.Item[num4].Position.Y,
                                        Main.Item[num4].Width,
                                        Main.Item[num4].Height
                                    )
                                ) && ItemSpace(Main.Item[num4])
                            )
                            {
                                Main.Item[num4].BeingGrabbed = true;
                                if (
                                    Position.X + Width * 0.5
                                    > Main.Item[num4].Position.X
                                    + Main.Item[num4].Width * 0.5
                                )
                                {
                                    if (
                                        Main.Item[num4].Velocity.X
                                        < ItemGrabSpeedMax + _velocity.X
                                    )
                                        Main.Item[num4].Velocity.X += ItemGrabSpeed;

                                    if (Main.Item[num4].Velocity.X < 0f)
                                        Main.Item[num4].Velocity.X += ItemGrabSpeed * 0.75f;
                                }
                                else
                                {
                                    if (
                                        Main.Item[num4].Velocity.X
                                        > -ItemGrabSpeedMax + _velocity.X
                                    )
                                        Main.Item[num4].Velocity.X -= ItemGrabSpeed;

                                    if (Main.Item[num4].Velocity.X > 0f)
                                        Main.Item[num4].Velocity.X -= ItemGrabSpeed * 0.75f;
                                }

                                if (
                                    Position.Y + Height * 0.5
                                    > Main.Item[num4].Position.Y
                                    + Main.Item[num4].Height * 0.5
                                )
                                {
                                    if (Main.Item[num4].Velocity.Y < ItemGrabSpeedMax)
                                        Main.Item[num4].Velocity.Y += ItemGrabSpeed;

                                    if (Main.Item[num4].Velocity.Y < 0f)
                                        Main.Item[num4].Velocity.Y += ItemGrabSpeed * 0.75f;
                                }
                                else
                                {
                                    if (Main.Item[num4].Velocity.Y > -ItemGrabSpeedMax)
                                        Main.Item[num4].Velocity.Y -= ItemGrabSpeed;

                                    if (Main.Item[num4].Velocity.Y > 0f)
                                        Main.Item[num4].Velocity.Y -= ItemGrabSpeed * 0.75f;
                                }
                            }
                        }
                    }

                if (
                    Position.X / 16f - TileRangeX <= tileTargetX
                    && (Position.X + Width) / 16f + TileRangeX - 1f
                    >= tileTargetX && Position.Y / 16f - TileRangeY <= tileTargetY
                    && (Position.Y + Height) / 16f + TileRangeY - 2f
                    >= tileTargetY && Main.Tile[tileTargetX, tileTargetY].Active
                )
                {
                    if (Main.Tile[tileTargetX, tileTargetY].Type == 4)
                    {
                        ShowItemIcon = true;
                        ShowItemIcon2 = 8;
                    }

                    if (
                        Main.Tile[tileTargetX, tileTargetY].Type == 10
                        || Main.Tile[tileTargetX, tileTargetY].Type == 11
                    )
                    {
                        ShowItemIcon = true;
                        ShowItemIcon2 = 0x19;
                    }

                    if (_controlUseTile)
                    {
                        if (_releaseUseTile)
                        {
                            if (Main.Tile[tileTargetX, tileTargetY].Type == 4)
                                WorldGen.KillTile(tileTargetX, tileTargetY);
                            else if (Main.Tile[tileTargetX, tileTargetY].Type == 10)
                                WorldGen.OpenDoor(tileTargetX, tileTargetY, Direction);
                            else if (Main.Tile[tileTargetX, tileTargetY].Type == 11)
                                WorldGen.CloseDoor(tileTargetX, tileTargetY);
                        }

                        _releaseUseTile = false;
                    }
                    else
                        _releaseUseTile = true;
                }

                Rectangle rectangle = new Rectangle(
                    (int)Position.X,
                    (int)Position.Y,
                    Width,
                    Height
                );

                for (num4 = 0; num4 < 0x3e8; num4++)
                    if (
                        Main.Npc[num4].Active
                        && rectangle.Intersects(
                            new Rectangle(
                                (int)Main.Npc[num4].Position.X,
                                (int)Main.Npc[num4].Position.Y,
                                Main.Npc[num4].Width,
                                Main.Npc[num4].Height
                            )
                        )
                    )
                    {
                        var hitDirection = -1;
                        if (
                            Main.Npc[num4].Position.X + Main.Npc[num4].Width / 2
                            < Position.X + Width / 2
                        )
                            hitDirection = 1;

                        Hurt(Main.Npc[num4].Damage, hitDirection);
                    }

                _velocity = Collision.TileCollision(
                    Position,
                    _velocity,
                    Width,
                    Height
                );

                Position += _velocity;
                ItemCheck(i);
                PlayerFrame();
                if (StatLife > StatLifeMax)
                    StatLife = StatLifeMax;
            }
        }
    }
}
