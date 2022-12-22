using MinecraftCrafterList.Controller;
using MinecraftCrafterList.Model;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace MinecraftCrafterList.CustomUserControl
{
    /// <summary>
    /// Interaction logic for CraftingTableUI.xaml
    /// </summary>
    public partial class CraftingTableUI : UserControl
    {
        public CraftingTableUI(Item i)
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
                            LeftTop.InsertItem(recipeItem.First(), false);
                            break;
                        case 1:
                            CentreTop.InsertItem(recipeItem.First(), false);
                            break;
                        case 2:
                            RightTop.InsertItem(recipeItem.First(), false);
                            break;
                        case 3:
                            LeftMiddle.InsertItem(recipeItem.First(), false);
                            break;
                        case 4:
                            CentreMiddle.InsertItem(recipeItem.First(), false);
                            break;
                        case 5:
                            RightMiddle.InsertItem(recipeItem.First(), false);
                            break;
                        case 6:
                            LeftBottom.InsertItem(recipeItem.First(), false);
                            break;
                        case 7:
                            CentreBottom.InsertItem(recipeItem.First(), false);
                            break;
                        case 8:
                            RightBottom.InsertItem(recipeItem.First(), false);
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
