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

        /*
        public string ItemSlot1 { get; set; }
        public string ItemSlot2 { get; set; }
        public string ItemSlot3 { get; set; }
        public string ItemSlot4 { get; set; }
        public string ItemSlot5 { get; set; }
        public string ItemSlot6 { get; set; }
        public string ItemSlot7 { get; set; }
        public string ItemSlot8 { get; set; }
        public string ItemSlot9 { get; set; }
        */
    }
}
