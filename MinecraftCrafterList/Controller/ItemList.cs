using MinecraftCrafterList.Miscellaneous;
using MinecraftCrafterList.Model;
using System.Collections.Generic;
using System.Linq;
using System.IO;
//using System.Windows.Media.Imaging;

namespace MinecraftCrafterList.Controller
{
    public class ItemList
    {
        public static List<Item> GatherItems2(List<Recipe> recipe)
        {
            string[] lines = File.ReadAllLines(@"C:\Users\ihami\Desktop\Personal\Code Projects\MinecraftCrafterList\MinecraftCrafterList\Item_Lists\Minecraft Items.csv");
            List<Item> allItems = new List<Item>();
            char[] spearator = { ',' };

            foreach (string line in lines)
            {
                string[] splitLine = line.Split(spearator);

                if (splitLine.Length != 1 && !splitLine[0].Equals("Item Name"))
                {
                    allItems.Add(new Item
                    {
                        Name = splitLine[0],
                        Mod = splitLine[1],
                        ImageUrl = splitLine[2],
                        ImageBitmap = ImageSet.LoadRelativeImage(splitLine[2]),
                        Recipes = RecipeList.RecipeSearch(recipe, splitLine[0]).ToList(),
                        SelectedRecipe = 0,
                        StackSize = int.Parse(splitLine[3]),

                        NameColor = (Item.TextColor)int.Parse(splitLine[4]),
                        FlavourText1 = splitLine[5],
                        FlavourTextColor1 = (Item.TextColor)int.Parse(splitLine[6]),
                        FlavourText2 = splitLine[7],
                        FlavourTextColor2 = (Item.TextColor)int.Parse(splitLine[8]),
                    });
                }
            }

            return allItems;
        }

        public static IEnumerable<Item> ItemSearch(List<Item> itemList, string query)
        {
            return from itemFromList in itemList
                    where itemFromList.Name.EndsWith(query)
                    select itemFromList;
        }

        public static IEnumerable<Item> ItemContainsSearch(List<Item> itemList, string query)
        {
            return from itemFromList in itemList
                   where itemFromList.Name.Contains(query)
                   select itemFromList;
        }

        public static IEnumerable<Item> ItemIgnoreCaseSearch(List<Item> itemList, string query)
        {
            return from itemFromList in itemList
                   where itemFromList.Name.ToLower().Contains(query.ToLower())
                   select itemFromList;
        }
    }
}
