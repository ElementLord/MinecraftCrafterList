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
