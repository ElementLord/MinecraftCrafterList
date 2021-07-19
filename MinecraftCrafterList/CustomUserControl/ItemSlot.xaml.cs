using MinecraftCrafterList.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
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
            /*Height = 45,
            Width = 45,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center*/
        }

        //public void SetToItemSlotSize()
        //{
        //    Height = 49;
        //    Width = 49;
        //}

        //public void SetToResultSlotSize()
        //{
        //    Height = 61;
        //    Width = 61;
        //}
    }
}
