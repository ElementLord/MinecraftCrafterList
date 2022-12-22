using MinecraftCrafterList.Controller;
using MinecraftCrafterList.Model;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace MinecraftCrafterList.CustomUserControl
{
    /// <summary>
    /// Interaction logic for SmeltingUI.xaml
    /// </summary>
    public partial class SmeltingUI : UserControl
    {
        public SmeltingUI(Item i)
        {
            Width = 362;
            Height = 200;
            InitializeComponent();
            IEnumerable<Item> recipeItem = ItemList.ItemSearch(MainWindow.fullItemList, i.CurrentRecipeItemSlots()[0]);
            Input.InsertItem(recipeItem.First(), false);
            Result.InsertItem(i, true);
        }
    }
}
