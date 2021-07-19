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
    /// Interaction logic for NoRecipeUI.xaml
    /// </summary>
    public partial class NoRecipeUI : UserControl
    {
        public NoRecipeUI(Item i)
        {
            Width = 362;
            Height = 200;
            InitializeComponent();
            Result.InsertItem(i, false);
        }
    }
}
