using Microsoft.Win32;
using MinecraftCrafterList.Controller;
using MinecraftCrafterList.Miscellaneous;
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

namespace MinecraftCrafterList.View
{
    /// <summary>
    /// Interaction logic for CraftingView.xaml
    /// </summary>
    public partial class ItemView : Page
    {
        string path = "";
        IEnumerable<Item> ItemListDisplayed = MainWindow.fullItemList;

        public ItemView()
        {
            List<string> colourEnumList = new List<string>();
            
            InitializeComponent();
            ItemListDisplay.ItemsSource = ItemListDisplayed;
            
            foreach(Item.TextColor textColor in Enum.GetValues(typeof(Item.TextColor)))
            {
                colourEnumList.Add(textColor.ToString());
            }
            NameColour.ItemsSource = colourEnumList;
            FlavourColour1.ItemsSource = colourEnumList;
            FlavourColour2.ItemsSource = colourEnumList;
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog() 
            { 
                Multiselect = false,
                Filter = "Image files (*.png;*.jpeg;*.gif)|*.png;*.jpeg;*.gif|All files (*.*)|*.*"
            };
            
            if (fileDialog.ShowDialog() == true)
            {
                path = fileDialog.FileName;
                FilePath.Text = path;
                ImageDisplay.Source = ImageSet.LoadImage(path);
                //LoadImage();
            }
        }

        //public void LoadImage()
        //{
        //    try
        //    {
        //        ImageDisplay.Source = new BitmapImage(new Uri(path));//, UriKind.Relative
        //    }
        //    catch (Exception) //e
        //    {
        //        ImageDisplay.Source = ImageSet.MissingImage;//new BitmapImage(new Uri("../../Images/Items/Missing Image.png", UriKind.Relative));
        //    }
        //}

        private void ItemListDisplay_SectionChanged(object sender, RoutedEventArgs e)
        {
            Item i = ItemListDisplayed.ElementAt(ItemListDisplay.SelectedIndex);

            ItemName.Text = i.Name;
            NameColour.SelectedIndex = (int)i.NameColor;
            //path = i.ImageUrl;
            //FilePath.Text = path;
            //LoadImage();
            FilePath.Text = i.ImageUrl;
            ImageDisplay.Source = i.ImageBitmap;
            StackSize.Text = i.StackSize + "";
            FlavourText1.Text = i.FlavourText1;
            FlavourColour1.SelectedIndex = (int)i.FlavourTextColor1;
            FlavourText2.Text = i.FlavourText2;
            FlavourColour2.SelectedIndex = (int)i.FlavourTextColor2;
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            RefreshItemListDisplay();
        }

        private void RefreshItemListDisplay()
        {
            ItemListDisplayed = ItemList.ItemIgnoreCaseSearch(MainWindow.fullItemList, SearchBar.Text);
            ItemListDisplay.ItemsSource = ItemListDisplayed;
        }

        public void Submit_Edit()
        {
            if (MessageBox.Show("Are you sure you want to submit this edit?", "Notice", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Item i = ItemListDisplayed.ElementAt(ItemListDisplay.SelectedIndex);

                i.Name = ItemName.Text;
                i.NameColor = (Item.TextColor)NameColour.SelectedIndex;//(int)
                if (!i.ImageUrl.Equals(FilePath.Text))
                {
                    i.ImageUrl = FilePath.Text;
                    i.ImageBitmap = (BitmapImage)ImageDisplay.Source;
                }
                i.StackSize = int.Parse(StackSize.Text);
                i.FlavourText1 = FlavourText1.Text;
                i.FlavourTextColor1 = (Item.TextColor)FlavourColour1.SelectedIndex;//(int)
                i.FlavourText2 = FlavourText2.Text;
                i.FlavourTextColor2 = (Item.TextColor)FlavourColour2.SelectedIndex;//(int)

                RefreshItemListDisplay();
                //CustomUserControl.ItemButton.RefreshItemButton();
                //ItemListDisplay.InvalidateArrange();
                //ItemListDisplay.UpdateLayout();
            }
        }
    }
}
