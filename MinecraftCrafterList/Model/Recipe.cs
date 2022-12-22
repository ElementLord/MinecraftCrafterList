using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftCrafterList.Model
{
    public class Recipe
    {
        public enum CraftingType { No_Recipe = 0, Crafting_Table = 1, Smelting = 2, Brewing = 3, Other = 4 }

        public string ItemResult { get; set; }
        public string ModName { get; set; }
        public CraftingType TypeOfCrafting { get; set; }
        public int NumPerCraft { get; set; }
        public string[] ItemSlots { get; set; }
        //public int priority { get; set; }
    }
}
