using MinecraftCrafterList.Model;
using System.Windows.Controls;

namespace MinecraftCrafterList.CustomUserControl
{
    /// <summary>
    /// Interaction logic for ItemSlot.xaml
    /// </summary>
    public partial class ItemSlot : UserControl
    {

        public ItemSlot()
        {
            InitializeComponent();
        }

        public void InsertItem(Item i, bool showQuanity)
        {
            ItemSlotBackground.Children.Add(new ItemButton(i, showQuanity, false));
        }
    }
}
