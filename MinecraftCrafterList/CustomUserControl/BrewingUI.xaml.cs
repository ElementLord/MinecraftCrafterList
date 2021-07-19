using MinecraftCrafterList.Controller;
using MinecraftCrafterList.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MinecraftCrafterList.CustomUserControl
{
    /// <summary>
    /// Interaction logic for BrewingUI.xaml
    /// </summary>
    public partial class BrewingUI : UserControl
    {
        public BrewingUI(Item i)
        {
            Width = 362;
            Height = 200;
            InitializeComponent();

            for (int slotNum = 0; slotNum < 4; slotNum++)
            {
                IEnumerable<Item> recipeItem = ItemList.ItemSearch(MainWindow.fullItemList, i.CurrentRecipeItemSlots()[slotNum]);
                if (!(recipeItem.Count() < 1))
                {
                    switch (slotNum)
                    {
                        case 0:
                            Item.InsertItem(recipeItem.First(), false);
                            break;
                        case 1:
                            Bottle1.InsertItem(recipeItem.First(), false);
                            break;
                        case 2:
                            Bottle2.InsertItem(recipeItem.First(), false);
                            break;
                        case 3:
                            Bottle3.InsertItem(recipeItem.First(), false);
                            break;
                    }
                }
            }

            Result.InsertItem(i, true);
        }
    }
}
