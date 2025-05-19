namespace Terraria;

public class Recipe
{
    public static readonly int MaxRecipes = 100;
    public static readonly int MaxRequirements = 10;
    private static Recipe newRecipe = new Recipe();
    private static int numRecipes;
    public readonly Item CreateItem = new Item();
    public readonly Item[] RequiredItem = new Item[MaxRecipes];

    public Recipe()
    {
        for (var i = 0; i < MaxRequirements; i++)
            RequiredItem[i] = new Item();
    }

    private static void AddRecipe()
    {
        Main.Recipe[numRecipes] = newRecipe;
        newRecipe = new Recipe();
        numRecipes++;
    }

    public void Create()
    {
        for (var i = 0; i < MaxRequirements; i++)
        {
            if (RequiredItem[i].Type == 0)
                break;

            var stack = RequiredItem[i].Stack;
            for (var j = 0; j < 40; j++)
            {
                if (Main.Player[Main.MyPlayer].Inventory[j].IsTheSameAs(RequiredItem[i]))
                {
                    if (Main.Player[Main.MyPlayer].Inventory[j].Stack > stack)
                    {
                        Item item1 = Main.Player[Main.MyPlayer].Inventory[j];
                        item1.Stack -= stack;
                        stack = 0;
                    }
                    else
                    {
                        stack -= Main.Player[Main.MyPlayer].Inventory[j].Stack;
                        Main.Player[Main.MyPlayer].Inventory[j] = new Item();
                    }
                }

                if (stack <= 0)
                    break;
            }
        }

        FindRecipes();
    }

    public static void FindRecipes()
    {
        int num3;
        var num = Main.AvailableRecipe[Main.focusRecipe];
        var num2 = Main.AvailableRecipeY[Main.focusRecipe];
        for (num3 = 0; num3 < MaxRecipes; num3++)
            Main.AvailableRecipe[num3] = 0;

        Main.numAvailableRecipes = 0;
        for (num3 = 0; num3 < MaxRecipes; num3++)
        {
            if (Main.Recipe[num3].CreateItem.Type == 0)
                break;

            var flag = true;
            for (var i = 0; i < MaxRequirements; i++)
            {
                if (Main.Recipe[num3].RequiredItem[i].Type == 0)
                    break;

                var stack = Main.Recipe[num3].RequiredItem[i].Stack;
                for (var j = 0; j < 40; j++)
                {
                    if (
                        Main.Player[Main.MyPlayer]
                        .Inventory[j]
                        .IsTheSameAs(Main.Recipe[num3].RequiredItem[i])
                    )
                        stack -= Main.Player[Main.MyPlayer].Inventory[j].Stack;

                    if (stack <= 0)
                        break;
                }

                if (stack > 0)
                {
                    flag = false;
                    break;
                }
            }

            if (flag)
            {
                Main.AvailableRecipe[Main.numAvailableRecipes] = num3;
                Main.numAvailableRecipes++;
            }
        }

        num3 = 0;
        while (num3 < Main.numAvailableRecipes)
        {
            if (num == Main.AvailableRecipe[num3])
            {
                Main.focusRecipe = num3;
                break;
            }

            num3++;
        }

        if (Main.focusRecipe >= Main.numAvailableRecipes)
            Main.focusRecipe = Main.numAvailableRecipes - 1;

        if (Main.focusRecipe < 0)
            Main.focusRecipe = 0;

        var num7 = Main.AvailableRecipeY[Main.focusRecipe] - num2;
        for (num3 = 0; num3 < MaxRecipes; num3++)
            Main.AvailableRecipeY[num3] -= num7;
    }

    public static void SetupRecipes()
    {
        newRecipe.CreateItem.SetDefaults(8);
        newRecipe.CreateItem.Stack = 3;
        newRecipe.RequiredItem[0].SetDefaults(0x17);
        newRecipe.RequiredItem[0].Stack = 2;
        newRecipe.RequiredItem[1].SetDefaults(9);
        AddRecipe();
        newRecipe.CreateItem.SetDefaults(0x1a);
        newRecipe.CreateItem.Stack = 4;
        newRecipe.RequiredItem[0].SetDefaults(3);
        AddRecipe();
        newRecipe.CreateItem.SetDefaults(0x19);
        newRecipe.RequiredItem[0].SetDefaults(9);
        newRecipe.RequiredItem[0].Stack = 5;
        AddRecipe();
        newRecipe.CreateItem.SetDefaults(0x18);
        newRecipe.RequiredItem[0].SetDefaults(9);
        newRecipe.RequiredItem[0].Stack = 7;
        AddRecipe();
        newRecipe.CreateItem.SetDefaults(20);
        newRecipe.RequiredItem[0].SetDefaults(12);
        newRecipe.RequiredItem[0].Stack = 3;
        AddRecipe();
        newRecipe.CreateItem.SetDefaults("Copper Pickaxe");
        newRecipe.RequiredItem[0].SetDefaults(20);
        newRecipe.RequiredItem[0].Stack = 12;
        newRecipe.RequiredItem[1].SetDefaults(9);
        newRecipe.RequiredItem[1].Stack = 4;
        AddRecipe();
        newRecipe.CreateItem.SetDefaults("Copper Axe");
        newRecipe.RequiredItem[0].SetDefaults(20);
        newRecipe.RequiredItem[0].Stack = 9;
        newRecipe.RequiredItem[1].SetDefaults(9);
        newRecipe.RequiredItem[1].Stack = 3;
        AddRecipe();
        newRecipe.CreateItem.SetDefaults("Copper Hammer");
        newRecipe.RequiredItem[0].SetDefaults(20);
        newRecipe.RequiredItem[0].Stack = 10;
        newRecipe.RequiredItem[1].SetDefaults(9);
        newRecipe.RequiredItem[1].Stack = 3;
        AddRecipe();
        newRecipe.CreateItem.SetDefaults("Copper Broadsword");
        newRecipe.RequiredItem[0].SetDefaults(20);
        newRecipe.RequiredItem[0].Stack = 8;
        AddRecipe();
        newRecipe.CreateItem.SetDefaults("Copper Shortsword");
        newRecipe.RequiredItem[0].SetDefaults(20);
        newRecipe.RequiredItem[0].Stack = 7;
        AddRecipe();
        newRecipe.CreateItem.SetDefaults(0x13);
        newRecipe.RequiredItem[0].SetDefaults(13);
        newRecipe.RequiredItem[0].Stack = 3;
        AddRecipe();
        newRecipe.CreateItem.SetDefaults("Gold Pickaxe");
        newRecipe.RequiredItem[0].SetDefaults(0x13);
        newRecipe.RequiredItem[0].Stack = 12;
        newRecipe.RequiredItem[1].SetDefaults(9);
        newRecipe.RequiredItem[1].Stack = 4;
        AddRecipe();
        newRecipe.CreateItem.SetDefaults("Gold Axe");
        newRecipe.RequiredItem[0].SetDefaults(0x13);
        newRecipe.RequiredItem[0].Stack = 9;
        newRecipe.RequiredItem[1].SetDefaults(9);
        newRecipe.RequiredItem[1].Stack = 3;
        AddRecipe();
        newRecipe.CreateItem.SetDefaults("Gold Hammer");
        newRecipe.RequiredItem[0].SetDefaults(0x13);
        newRecipe.RequiredItem[0].Stack = 10;
        newRecipe.RequiredItem[1].SetDefaults(9);
        newRecipe.RequiredItem[1].Stack = 3;
        AddRecipe();
        newRecipe.CreateItem.SetDefaults("Gold Broadsword");
        newRecipe.RequiredItem[0].SetDefaults(0x13);
        newRecipe.RequiredItem[0].Stack = 8;
        AddRecipe();
        newRecipe.CreateItem.SetDefaults("Gold Shortsword");
        newRecipe.RequiredItem[0].SetDefaults(0x13);
        newRecipe.RequiredItem[0].Stack = 7;
        AddRecipe();
        newRecipe.CreateItem.SetDefaults(0x16);
        newRecipe.RequiredItem[0].SetDefaults(11);
        newRecipe.RequiredItem[0].Stack = 3;
        AddRecipe();
        newRecipe.CreateItem.SetDefaults(1);
        newRecipe.RequiredItem[0].SetDefaults(0x16);
        newRecipe.RequiredItem[0].Stack = 12;
        newRecipe.RequiredItem[1].SetDefaults(9);
        newRecipe.RequiredItem[1].Stack = 3;
        AddRecipe();
        newRecipe.CreateItem.SetDefaults(10);
        newRecipe.RequiredItem[0].SetDefaults(0x16);
        newRecipe.RequiredItem[0].Stack = 9;
        newRecipe.RequiredItem[1].SetDefaults(9);
        newRecipe.RequiredItem[1].Stack = 3;
        AddRecipe();
        newRecipe.CreateItem.SetDefaults(7);
        newRecipe.RequiredItem[0].SetDefaults(0x16);
        newRecipe.RequiredItem[0].Stack = 10;
        newRecipe.RequiredItem[1].SetDefaults(9);
        newRecipe.RequiredItem[1].Stack = 3;
        AddRecipe();
        newRecipe.CreateItem.SetDefaults(4);
        newRecipe.RequiredItem[0].SetDefaults(0x16);
        newRecipe.RequiredItem[0].Stack = 8;
        AddRecipe();
        newRecipe.CreateItem.SetDefaults(6);
        newRecipe.RequiredItem[0].SetDefaults(0x16);
        newRecipe.RequiredItem[0].Stack = 7;
        AddRecipe();
        newRecipe.CreateItem.SetDefaults(0x15);
        newRecipe.RequiredItem[0].SetDefaults(14);
        newRecipe.RequiredItem[0].Stack = 3;
        AddRecipe();
        newRecipe.CreateItem.SetDefaults("Silver Pickaxe");
        newRecipe.RequiredItem[0].SetDefaults(0x15);
        newRecipe.RequiredItem[0].Stack = 12;
        newRecipe.RequiredItem[1].SetDefaults(9);
        newRecipe.RequiredItem[1].Stack = 4;
        AddRecipe();
        newRecipe.CreateItem.SetDefaults("Silver Axe");
        newRecipe.RequiredItem[0].SetDefaults(0x15);
        newRecipe.RequiredItem[0].Stack = 9;
        newRecipe.RequiredItem[1].SetDefaults(9);
        newRecipe.RequiredItem[1].Stack = 3;
        AddRecipe();
        newRecipe.CreateItem.SetDefaults("Silver Hammer");
        newRecipe.RequiredItem[0].SetDefaults(0x15);
        newRecipe.RequiredItem[0].Stack = 10;
        newRecipe.RequiredItem[1].SetDefaults(9);
        newRecipe.RequiredItem[1].Stack = 3;
        AddRecipe();
        newRecipe.CreateItem.SetDefaults("Silver Broadsword");
        newRecipe.RequiredItem[0].SetDefaults(0x15);
        newRecipe.RequiredItem[0].Stack = 8;
        AddRecipe();
        newRecipe.CreateItem.SetDefaults("Silver Shortsword");
        newRecipe.RequiredItem[0].SetDefaults(0x15);
        newRecipe.RequiredItem[0].Stack = 7;
        AddRecipe();
    }
}
