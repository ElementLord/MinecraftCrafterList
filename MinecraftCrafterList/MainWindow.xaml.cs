using MinecraftCrafterList.Controller;
using MinecraftCrafterList.CustomUserControl;
using MinecraftCrafterList.Miscellaneous;
using MinecraftCrafterList.Model;
using MinecraftCrafterList.View;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

/// <Notes>
/// To Do List:
/// Prevent infinite loop with iron, coal, gold, etc (Kinda Fixed)
/// Brewing Recipes Display 
/// Rotation of items in recipes
/// 
/// Notes:
/// At Some point, will need to implement the ability to have multiple recipes for one item displayed and counted at the same time
/// </Notes>

namespace MinecraftCrafterList
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Full Lists
        public static List<Recipe> fullRecipeList = RecipeList.GatherRecipes();
        public static List<Item> fullItemList = ItemList.GatherItems2(fullRecipeList);

        //Pages
        public static Frame changableView;
        public static CraftingView pageCV = new CraftingView();
        public static ItemView pageIV = new ItemView();

        //Variables
        public static bool singleItemMode = true;

        public MainWindow()
        {
            InitializeComponent();
            changableView = ChangableView;
            PageChange(0);
        }

        public static void PageChange(int pageNum)
        {
            switch (pageNum)
            {
                case 0:
                    changableView.Content = pageCV;
                    break;
                case 1:
                    changableView.Content = pageIV;
                    break;
                default:
                    break;
            }
        }

        //Menu Control Actions
        //-File
        private void ExitMenuButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        //--Views
        private void Crafting_Click(object sender, RoutedEventArgs e)
        {
            PageChange(0);
            Crafting_View.IsEnabled = true;
            Items_View.IsEnabled = false;
            Crafting.IsChecked = true;
            Items.IsChecked = false;
        }

        private void Items_Click(object sender, RoutedEventArgs e)
        {
            PageChange(1);
            Crafting_View.IsEnabled = false;
            Items_View.IsEnabled = true;
            Crafting.IsChecked = false;
            Items.IsChecked = true;
        }

        //-Crafting View
        private void SingleItemMenuButton_Click(object sender, RoutedEventArgs e)
        {
            Single_Item.IsChecked = true;
            singleItemMode = true;
            Multiple_Items.IsChecked = false;
            CraftingView.firstTimeMulti = false;
        }

        private void MultipleItemMenuButton_Click(object sender, RoutedEventArgs e)
        {
            Single_Item.IsChecked = false;
            singleItemMode = false;
            Multiple_Items.IsChecked = true;
        }

        private void RunMultiCraftMenuButton_Click(object sender, RoutedEventArgs e)
        {
            if (Multiple_Items.IsChecked)
            {
                if (CraftingView.findCraftingStack.Count() == 0)
                {
                    MessageBox.Show("No items have been selected.", "Error");
                }
                else
                {
                    CraftingView.firstTimeMulti = false;
                    pageCV.RunTheStuff();
                    foreach (ItemButton b in pageCV.ItemDisplay.Children)
                    {
                        b.ButtonForItem.BorderBrush = ColourSets.itemButtonBorderBase;
                    }
                }
            }
            else
            {
                MessageBox.Show("'Multiple Items' Modes must be selected to use 'Run Multi Craft'.", "Error");
            }
        }

        //-Items View
        private void AddItemMenuButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SubmitEditMenuButton_Click(object sender, RoutedEventArgs e)
        {
            pageIV.Submit_Edit();
        }

        private void RemoveItemMenuButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EditingMode_Click(object sender, RoutedEventArgs e)
        {
            if (Editing_Mode.IsChecked)
            {
                pageIV.ItemName.IsEnabled = true;
                pageIV.Extention.IsEnabled = true;
                pageIV.NameColour.IsEnabled = true;
                pageIV.BrowseButton.IsEnabled = true;
                pageIV.StackSize.IsEnabled = true;
                pageIV.FlavourText1.IsEnabled = true;
                pageIV.FlavourColour1.IsEnabled = true;
                pageIV.FlavourText2.IsEnabled = true;
                pageIV.FlavourColour2.IsEnabled = true;

                Submit_Edit.Visibility = Visibility.Visible;
            }
            else
            {
                pageIV.ItemName.IsEnabled = false;
                pageIV.Extention.IsEnabled = false;
                pageIV.NameColour.IsEnabled = false;
                pageIV.BrowseButton.IsEnabled = false;
                pageIV.StackSize.IsEnabled = false;
                pageIV.FlavourText1.IsEnabled = false;
                pageIV.FlavourColour1.IsEnabled = false;
                pageIV.FlavourText2.IsEnabled = false;
                pageIV.FlavourColour2.IsEnabled = false;

                Submit_Edit.Visibility = Visibility.Collapsed;
            }
        }
    }
}
