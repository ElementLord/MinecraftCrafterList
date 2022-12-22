using MinecraftCrafterList.Model;
using System.Windows.Controls;

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
