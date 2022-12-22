using System.Collections.Generic;
using System.Linq;
using System.Windows.Media.Imaging;

namespace MinecraftCrafterList.Model
{
    public class Item
    {
        //public List<string> modNames = new List<string> {"Minecraft"};
        public enum TextColor { White = 0, Grey = 1, Blue = 2, Red = 3 }

        public string Name { get; set; }
        public string Mod { get; set; }
        public string ImageUrl { get; set; }
        public BitmapImage ImageBitmap { get; set; }
        public List<Recipe> Recipes { get; set; } 
        public int SelectedRecipe { get; set; }
        public int StackSize { get; set; }

        public TextColor NameColor { get; set; }
        public string FlavourText1 { get; set; }
        public TextColor FlavourTextColor1 { get; set; }
        public string FlavourText2 { get; set; }
        public TextColor FlavourTextColor2 { get; set; }

        //public double OperationPerFuel { get; set; }

        public Recipe.CraftingType CurrentRecipeCraftingType()
        {
            return Recipes.ElementAt(SelectedRecipe).TypeOfCrafting;
        }

        public int CurrentRecipeNumPerCraft()
        {
            return Recipes.ElementAt(SelectedRecipe).NumPerCraft;
        }

        public string[] CurrentRecipeItemSlots()
        {
            return Recipes.ElementAt(SelectedRecipe).ItemSlots;
        }
    }
}
