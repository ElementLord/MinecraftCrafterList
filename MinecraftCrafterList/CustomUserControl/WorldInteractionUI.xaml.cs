using MinecraftCrafterList.Controller;
using MinecraftCrafterList.Model;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace MinecraftCrafterList.CustomUserControl
{
    /// <summary>
    /// Interaction logic for WorldInteractionUI.xaml
    /// </summary>
    public partial class WorldInteractionUI : UserControl
    {
        public WorldInteractionUI(Item i)
        {
            Width = 362;
            Height = 200;
            InitializeComponent();

            for (int slotNum = 0; slotNum < 9; slotNum++)
            {
                IEnumerable<Item> recipeItem = ItemList.ItemSearch(MainWindow.fullItemList, i.CurrentRecipeItemSlots()[slotNum]);
                if (!(recipeItem.Count() < 1))
                {
                    switch (slotNum)
                    {
                        case 0:
                            Inventory.InsertItem(recipeItem.First(), false);
                            break;
                        case 1:
                            World.InsertItem(recipeItem.First(), false);
                            break;
                        default:
                            break;
                    }
                }
            }

            Result.InsertItem(i, true);
        }
    }
}
