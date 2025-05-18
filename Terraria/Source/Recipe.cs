namespace Terraria
{
    using System;

    public class Recipe
    {
        public Item createItem = new Item();
        public static int maxRecipes = 100;
        public static int maxRequirements = 10;
        private static Recipe newRecipe = new Recipe();
        public static int numRecipes = 0;
        public Item[] requiredItem = new Item[maxRecipes];

        public Recipe()
        {
            for (int i = 0; i < maxRequirements; i++)
            {
                this.requiredItem[i] = new Item();
            }
        }

        private static void addRecipe()
        {
            Main.recipe[numRecipes] = newRecipe;
            newRecipe = new Recipe();
            numRecipes++;
        }

        public void Create()
        {
            for (int i = 0; i < maxRequirements; i++)
            {
                if (this.requiredItem[i].type == 0)
                {
                    break;
                }
                int stack = this.requiredItem[i].stack;
                for (int j = 0; j < 40; j++)
                {
                    if (Main.player[Main.myPlayer].inventory[j].IsTheSameAs(this.requiredItem[i]))
                    {
                        if (Main.player[Main.myPlayer].inventory[j].stack > stack)
                        {
                            Item item1 = Main.player[Main.myPlayer].inventory[j];
                            item1.stack -= stack;
                            stack = 0;
                        }
                        else
                        {
                            stack -= Main.player[Main.myPlayer].inventory[j].stack;
                            Main.player[Main.myPlayer].inventory[j] = new Item();
                        }
                    }
                    if (stack <= 0)
                    {
                        break;
                    }
                }
            }
            FindRecipes();
        }

        public static void FindRecipes()
        {
            int num3;
            int num = Main.availableRecipe[Main.focusRecipe];
            float num2 = Main.availableRecipeY[Main.focusRecipe];
            for (num3 = 0; num3 < maxRecipes; num3++)
            {
                Main.availableRecipe[num3] = 0;
            }
            Main.numAvailableRecipes = 0;
            for (num3 = 0; num3 < maxRecipes; num3++)
            {
                if (Main.recipe[num3].createItem.type == 0)
                {
                    break;
                }
                bool flag = true;
                for (int i = 0; i < maxRequirements; i++)
                {
                    if (Main.recipe[num3].requiredItem[i].type == 0)
                    {
                        break;
                    }
                    int stack = Main.recipe[num3].requiredItem[i].stack;
                    for (int j = 0; j < 40; j++)
                    {
                        if (
                            Main.player[Main.myPlayer]
                                .inventory[j]
                                .IsTheSameAs(Main.recipe[num3].requiredItem[i])
                        )
                        {
                            stack -= Main.player[Main.myPlayer].inventory[j].stack;
                        }
                        if (stack <= 0)
                        {
                            break;
                        }
                    }
                    if (stack > 0)
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag)
                {
                    Main.availableRecipe[Main.numAvailableRecipes] = num3;
                    Main.numAvailableRecipes++;
                }
            }
            num3 = 0;
            while (num3 < Main.numAvailableRecipes)
            {
                if (num == Main.availableRecipe[num3])
                {
                    Main.focusRecipe = num3;
                    break;
                }
                num3++;
            }
            if (Main.focusRecipe >= Main.numAvailableRecipes)
            {
                Main.focusRecipe = Main.numAvailableRecipes - 1;
            }
            if (Main.focusRecipe < 0)
            {
                Main.focusRecipe = 0;
            }
            float num7 = Main.availableRecipeY[Main.focusRecipe] - num2;
            for (num3 = 0; num3 < maxRecipes; num3++)
            {
                Main.availableRecipeY[num3] -= num7;
            }
        }

        public static void SetupRecipes()
        {
            newRecipe.createItem.SetDefaults(8);
            newRecipe.createItem.stack = 3;
            newRecipe.requiredItem[0].SetDefaults(0x17);
            newRecipe.requiredItem[0].stack = 2;
            newRecipe.requiredItem[1].SetDefaults(9);
            addRecipe();
            newRecipe.createItem.SetDefaults(0x1a);
            newRecipe.createItem.stack = 4;
            newRecipe.requiredItem[0].SetDefaults(3);
            addRecipe();
            newRecipe.createItem.SetDefaults(0x19);
            newRecipe.requiredItem[0].SetDefaults(9);
            newRecipe.requiredItem[0].stack = 5;
            addRecipe();
            newRecipe.createItem.SetDefaults(0x18);
            newRecipe.requiredItem[0].SetDefaults(9);
            newRecipe.requiredItem[0].stack = 7;
            addRecipe();
            newRecipe.createItem.SetDefaults(20);
            newRecipe.requiredItem[0].SetDefaults(12);
            newRecipe.requiredItem[0].stack = 3;
            addRecipe();
            newRecipe.createItem.SetDefaults("Copper Pickaxe");
            newRecipe.requiredItem[0].SetDefaults(20);
            newRecipe.requiredItem[0].stack = 12;
            newRecipe.requiredItem[1].SetDefaults(9);
            newRecipe.requiredItem[1].stack = 4;
            addRecipe();
            newRecipe.createItem.SetDefaults("Copper Axe");
            newRecipe.requiredItem[0].SetDefaults(20);
            newRecipe.requiredItem[0].stack = 9;
            newRecipe.requiredItem[1].SetDefaults(9);
            newRecipe.requiredItem[1].stack = 3;
            addRecipe();
            newRecipe.createItem.SetDefaults("Copper Hammer");
            newRecipe.requiredItem[0].SetDefaults(20);
            newRecipe.requiredItem[0].stack = 10;
            newRecipe.requiredItem[1].SetDefaults(9);
            newRecipe.requiredItem[1].stack = 3;
            addRecipe();
            newRecipe.createItem.SetDefaults("Copper Broadsword");
            newRecipe.requiredItem[0].SetDefaults(20);
            newRecipe.requiredItem[0].stack = 8;
            addRecipe();
            newRecipe.createItem.SetDefaults("Copper Shortsword");
            newRecipe.requiredItem[0].SetDefaults(20);
            newRecipe.requiredItem[0].stack = 7;
            addRecipe();
            newRecipe.createItem.SetDefaults(0x13);
            newRecipe.requiredItem[0].SetDefaults(13);
            newRecipe.requiredItem[0].stack = 3;
            addRecipe();
            newRecipe.createItem.SetDefaults("Gold Pickaxe");
            newRecipe.requiredItem[0].SetDefaults(0x13);
            newRecipe.requiredItem[0].stack = 12;
            newRecipe.requiredItem[1].SetDefaults(9);
            newRecipe.requiredItem[1].stack = 4;
            addRecipe();
            newRecipe.createItem.SetDefaults("Gold Axe");
            newRecipe.requiredItem[0].SetDefaults(0x13);
            newRecipe.requiredItem[0].stack = 9;
            newRecipe.requiredItem[1].SetDefaults(9);
            newRecipe.requiredItem[1].stack = 3;
            addRecipe();
            newRecipe.createItem.SetDefaults("Gold Hammer");
            newRecipe.requiredItem[0].SetDefaults(0x13);
            newRecipe.requiredItem[0].stack = 10;
            newRecipe.requiredItem[1].SetDefaults(9);
            newRecipe.requiredItem[1].stack = 3;
            addRecipe();
            newRecipe.createItem.SetDefaults("Gold Broadsword");
            newRecipe.requiredItem[0].SetDefaults(0x13);
            newRecipe.requiredItem[0].stack = 8;
            addRecipe();
            newRecipe.createItem.SetDefaults("Gold Shortsword");
            newRecipe.requiredItem[0].SetDefaults(0x13);
            newRecipe.requiredItem[0].stack = 7;
            addRecipe();
            newRecipe.createItem.SetDefaults(0x16);
            newRecipe.requiredItem[0].SetDefaults(11);
            newRecipe.requiredItem[0].stack = 3;
            addRecipe();
            newRecipe.createItem.SetDefaults(1);
            newRecipe.requiredItem[0].SetDefaults(0x16);
            newRecipe.requiredItem[0].stack = 12;
            newRecipe.requiredItem[1].SetDefaults(9);
            newRecipe.requiredItem[1].stack = 3;
            addRecipe();
            newRecipe.createItem.SetDefaults(10);
            newRecipe.requiredItem[0].SetDefaults(0x16);
            newRecipe.requiredItem[0].stack = 9;
            newRecipe.requiredItem[1].SetDefaults(9);
            newRecipe.requiredItem[1].stack = 3;
            addRecipe();
            newRecipe.createItem.SetDefaults(7);
            newRecipe.requiredItem[0].SetDefaults(0x16);
            newRecipe.requiredItem[0].stack = 10;
            newRecipe.requiredItem[1].SetDefaults(9);
            newRecipe.requiredItem[1].stack = 3;
            addRecipe();
            newRecipe.createItem.SetDefaults(4);
            newRecipe.requiredItem[0].SetDefaults(0x16);
            newRecipe.requiredItem[0].stack = 8;
            addRecipe();
            newRecipe.createItem.SetDefaults(6);
            newRecipe.requiredItem[0].SetDefaults(0x16);
            newRecipe.requiredItem[0].stack = 7;
            addRecipe();
            newRecipe.createItem.SetDefaults(0x15);
            newRecipe.requiredItem[0].SetDefaults(14);
            newRecipe.requiredItem[0].stack = 3;
            addRecipe();
            newRecipe.createItem.SetDefaults("Silver Pickaxe");
            newRecipe.requiredItem[0].SetDefaults(0x15);
            newRecipe.requiredItem[0].stack = 12;
            newRecipe.requiredItem[1].SetDefaults(9);
            newRecipe.requiredItem[1].stack = 4;
            addRecipe();
            newRecipe.createItem.SetDefaults("Silver Axe");
            newRecipe.requiredItem[0].SetDefaults(0x15);
            newRecipe.requiredItem[0].stack = 9;
            newRecipe.requiredItem[1].SetDefaults(9);
            newRecipe.requiredItem[1].stack = 3;
            addRecipe();
            newRecipe.createItem.SetDefaults("Silver Hammer");
            newRecipe.requiredItem[0].SetDefaults(0x15);
            newRecipe.requiredItem[0].stack = 10;
            newRecipe.requiredItem[1].SetDefaults(9);
            newRecipe.requiredItem[1].stack = 3;
            addRecipe();
            newRecipe.createItem.SetDefaults("Silver Broadsword");
            newRecipe.requiredItem[0].SetDefaults(0x15);
            newRecipe.requiredItem[0].stack = 8;
            addRecipe();
            newRecipe.createItem.SetDefaults("Silver Shortsword");
            newRecipe.requiredItem[0].SetDefaults(0x15);
            newRecipe.requiredItem[0].stack = 7;
            addRecipe();
        }
    }
}
