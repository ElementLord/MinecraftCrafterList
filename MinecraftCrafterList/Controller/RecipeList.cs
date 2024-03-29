﻿using System.Collections.Generic;
using System.Linq;
using System.IO;
using MinecraftCrafterList.Model;

namespace MinecraftCrafterList.Controller
{
    public class RecipeList
    {
        public static List<Recipe> GatherRecipes()
        {
            string[] lines = File.ReadAllLines(@"C:\Users\ihami\Desktop\Personal\Code Projects\MinecraftCrafterList\MinecraftCrafterList\Item_Lists\Minecraft Item Recipes.csv");
            List<Recipe> allReceipes = new List<Recipe>();
            char[] spearator = { ',' };

            foreach (string line in lines)
            {
                string[] splitLine = line.Split(spearator);

                if (splitLine.Length != 1 && !splitLine[0].Equals("Item Result"))
                {
                    allReceipes.Add(new Recipe
                    {
                        ItemResult = splitLine[0],
                        ModName = splitLine[1],
                        TypeOfCrafting = (Recipe.CraftingType)int.Parse(splitLine[2]),
                        NumPerCraft = int.Parse(splitLine[3]),
                        ItemSlots = new string[]{ splitLine[4], splitLine[5], splitLine[6],
                                                    splitLine[7], splitLine[8], splitLine[9],
                                                    splitLine[10], splitLine[11], splitLine[12] }
                    });
                }
            }

            return allReceipes;
        }

        public static IEnumerable<Recipe> RecipeSearch(List<Recipe> list, string query)
        {
            return from itemFromList in list
                   where itemFromList.ItemResult.Equals(query)
                   select itemFromList;
        }
    }
}
