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
        //public static MainWindow mainWindow;
        public static Frame cav;
        public static CraftingView pageCV = new CraftingView();
        public static ItemView pageIV = new ItemView();

        //Variables
        public static bool singleItemMode = true;

        public MainWindow()
        {
            InitializeComponent();
            cav = ChangableView;//mainWindow = this;
            PageChange(0);
        }

        public static void PageChange(int pageNum)
        {
            switch (pageNum)
            {
                case 0:
                    cav.Content = pageCV;/*mainWindow*/
                    break;
                case 1:
                    cav.Content = pageIV;/*mainWindow*/
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
            //ClearListAndView(true, true, false);
            CraftingView.firstTimeMulti = false;
        }

        private void MultipleItemMenuButton_Click(object sender, RoutedEventArgs e)
        {
            Single_Item.IsChecked = false;
            singleItemMode = false;
            Multiple_Items.IsChecked = true;
            //ClearListAndView(true, true, false);
        }

        private void RunMultiCraftMenuButton_Click(object sender, RoutedEventArgs e)
        {
            if (Multiple_Items.IsChecked == true)
            {
                if (CraftingView.findCraftingStack.Count() == 0)
                {
                    MessageBox.Show("No items have been selected.", "Error");
                }
                else
                {
                    //ClearListAndView(false, false, true);
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


        //private void FileMenuButton_Click(object sender, RoutedEventArgs e)
        //{
        //    MenuItem menuItemPressed = (MenuItem)sender;

        //    if (menuItemPressed.Name.Contains("Exit"))
        //    {
        //        Application.Current.Shutdown();
        //    }
        //}


        //private void ModesMenuButton_Click(object sender, RoutedEventArgs e)
        //{
        //    MenuItem menuItemPressed = (MenuItem)sender;

        //    if (menuItemPressed.Name.Contains("Single_Item"))
        //    {
        //        Single_Item.IsChecked = true;
        //        singleItemMode = true;
        //        Multiple_Items.IsChecked = false;
        //        //ClearListAndView(true, true, false);
        //        CraftingView.firstTimeMulti = false;
        //    }
        //    else if (menuItemPressed.Name.Contains("Multiple_Items"))
        //    {
        //        Single_Item.IsChecked = false;
        //        singleItemMode = false;
        //        Multiple_Items.IsChecked = true;
        //        //ClearListAndView(true, true, false);
        //    }
        //    else if (menuItemPressed.Name.Contains("Run_Multi_Craft"))
        //    {
        //        if (Multiple_Items.IsChecked == true)
        //        {
        //            if (CraftingView.findCraftingStack.Count() == 0)
        //            {
        //                MessageBox.Show("No items have been selected.", "Error");
        //            }
        //            else
        //            {
        //                //ClearListAndView(false, false, true);
        //                CraftingView.firstTimeMulti = false;
        //                /*RunTheStuff();
        //                foreach (Button b in ItemDisplay.Children)
        //                {
        //                    b.BorderBrush = ColourSets.itemButtonBorderBase;
        //                }*/
        //            }
        //        }
        //        else
        //        {
        //            MessageBox.Show("'Multiple Items' Modes must be selected to use 'Run Multi Craft'.", "Error");
        //        }
        //    }
        //}

        //private void ItemsMenuButton_Click(object sender, RoutedEventArgs e)
        //{
        //    MenuItem menuItemPressed = (MenuItem)sender;

        //    if (menuItemPressed.Name.Contains("Add_Item"))
        //    {
        //        PageChange(1);
        //    }
        //    else if (menuItemPressed.Name.Contains("Edit_Item"))
        //    {

        //    }
        //    else if (menuItemPressed.Name.Contains("Remove_Item"))
        //    {

        //    }
        //}

    }
}
