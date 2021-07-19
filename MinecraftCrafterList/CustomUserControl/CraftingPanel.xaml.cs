using MinecraftCrafterList.Controller;
using MinecraftCrafterList.Model;
using MinecraftCrafterList.View;
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
    /// Interaction logic for CraftingPanel.xaml
    /// </summary>
    public partial class CraftingPanel : UserControl
    {
        public Item ItemForDisplay { get; set; }
        public int StepOfItem { get; set; }
        private readonly bool PastInitiation = false;
        //public double AmountOfCrafts { get; set; }

        public CraftingPanel(Item item, int step)
        {
            ItemForDisplay = item;
            StepOfItem = step;
            HorizontalAlignment = HorizontalAlignment.Left;
            VerticalAlignment = VerticalAlignment.Top;
            InitializeComponent();
            SetValue();
            SetCraftingUI();
            SetComboBox();
            Spinner.CraftingPanel = this;
            Spinner.AddCraftingTextChanged();
            PastInitiation = true;
        }

        public void SetCraftingUI()
        {
            switch (ItemForDisplay.CurrentRecipeCraftingType())
            {
                case Recipe.CraftingType.No_Recipe:
                    Panel.Children.Add(new NoRecipeUI(ItemForDisplay)
                    {
                        HorizontalAlignment = HorizontalAlignment.Left,
                        VerticalAlignment = VerticalAlignment.Top,
                        Margin = new Thickness(10, 10, 0, 0)
                    });
                    break;
                case Recipe.CraftingType.Crafting_Table:
                    Panel.Children.Add(new CraftingTableUI(ItemForDisplay) 
                    {
                        HorizontalAlignment = HorizontalAlignment.Left,
                        VerticalAlignment = VerticalAlignment.Top,
                        Margin = new Thickness(10, 10, 0, 0)
                    });
                    break;
                case Recipe.CraftingType.Smelting:
                    Panel.Children.Add(new SmeltingUI(ItemForDisplay)
                    {
                        HorizontalAlignment = HorizontalAlignment.Left,
                        VerticalAlignment = VerticalAlignment.Top,
                        Margin = new Thickness(10, 10, 0, 0)
                    });
                    break;
                case Recipe.CraftingType.Brewing:
                    Panel.Children.Add(new BrewingUI(ItemForDisplay)
                    {
                        HorizontalAlignment = HorizontalAlignment.Left,
                        VerticalAlignment = VerticalAlignment.Top,
                        Margin = new Thickness(10, 10, 0, 0)
                    });
                    break;
                case Recipe.CraftingType.Other:
                    Panel.Children.Add(new WorldInteractionUI(ItemForDisplay)
                    {
                        HorizontalAlignment = HorizontalAlignment.Left,
                        VerticalAlignment = VerticalAlignment.Top,
                        Margin = new Thickness(10, 10, 0, 0)
                    });
                    break;
            }
        }

        public void SetValue()
        {
            //int numOfItemsUsed = 0;
            double totalNum = CraftingView.craftingItemsCount[StepOfItem] + Spinner.GetCurrentNumber();
            double amountOfCrafts;
            double totalFromCraft;

            if (ItemForDisplay.CurrentRecipeCraftingType() == Recipe.CraftingType.No_Recipe)
            {
                amountOfCrafts = totalNum;
                totalFromCraft = totalNum;
            }
            else
            {
                amountOfCrafts = Math.Ceiling(totalNum / (ItemForDisplay.CurrentRecipeNumPerCraft() * 1.0)); /*CraftingView.craftingItemsCount[step]*//*currentItem*/
                totalFromCraft = ItemForDisplay.CurrentRecipeNumPerCraft() * amountOfCrafts; /*currentItem*/
            }

            //switch (ItemForDisplay.CurrentRecipeCraftingType())
            //{
            //    case Recipe.CraftingType.Crafting_Table:
            //        numOfItemsUsed = 9;
            //        break;
            //    case Recipe.CraftingType.Smelting:
            //        numOfItemsUsed = 1;
            //        break;
            //    case Recipe.CraftingType.Brewing:
            //        numOfItemsUsed = 4;
            //        break;
            //    case Recipe.CraftingType.Other:
            //        numOfItemsUsed = 2;
            //        break;
            //}

            double totalStackFromCraft = totalFromCraft / ItemForDisplay.StackSize;
            double remainderFromCraft = totalFromCraft - totalNum;

            ItemTotal.Content = "Total Items (Stacks): " + totalFromCraft + " (" + totalStackFromCraft + ")";
            ItemRemainder.Content = "Remaining/Unused Items: " + remainderFromCraft;
            ItemRequired.Content = "Required Items: " + CraftingView.craftingItemsCount[StepOfItem];
            NumOfOperation.Content = "x " + amountOfCrafts;

            foreach (string name in ItemForDisplay.CurrentRecipeItemSlots())/*item.Recipe*/ //int i = 0; i < numOfItemsUsed; i++
            {
                IEnumerable<Item> craftingItemQuery = ItemList.ItemSearch(MainWindow.fullItemList, name);

                if (craftingItemQuery.Count() != 0)
                {
                    //for (int i = 0; i < AmountOfCrafts; i++){}

                    int index = CraftingView.craftingStack.IndexOf(craftingItemQuery.First());//CraftingView.craftingStack.IndexOf(craftingItemQuery.First())
                    CraftingView.craftingItemsCount[index] = CraftingView.craftingItemsCount[index] + (int)amountOfCrafts;
                }
            }
        }

        public void SetComboBox()
        {
            List<ComboBoxItem> cbi = new List<ComboBoxItem>();
            int recipeNum = 0;

            foreach (Recipe r in ItemForDisplay.Recipes)
            {
                StackPanel comboBoxItem = new StackPanel() { Orientation = Orientation.Horizontal };

                comboBoxItem.Children.Add(new Label()
                {
                    Content = recipeNum + " (" + r.TypeOfCrafting.ToString() + "): ",
                    Height = 26
                });

                foreach (string slot in r.ItemSlots)
                {
                    IEnumerable<Item> itemSearch = ItemList.ItemSearch(MainWindow.fullItemList, slot);
                    if (itemSearch.Count() >= 1)
                    {
                        comboBoxItem.Children.Add(new Image()
                        {
                            Source = new BitmapImage(new Uri(itemSearch.First().ImageUrl, UriKind.Relative)),
                            Height = 15,
                            Width = 15,
                            Stretch = Stretch.Uniform
                        });
                    }
                }

                //bool recipeSelected = false;
                //if (recipeNum == ItemForDisplay.SelectedRecipe)
                //{
                //    recipeSelected = true;
                //}
                recipeNum++;

                cbi.Add(new ComboBoxItem()
                {
                    Content = comboBoxItem,
                    HorizontalContentAlignment = HorizontalAlignment.Left,
                    VerticalContentAlignment = VerticalAlignment.Center,
                    //IsSelected = recipeSelected
                });
            }

            RecipeComboBox.ItemsSource = cbi;
            RecipeComboBox.SelectedIndex = ItemForDisplay.SelectedRecipe;
        }

        public void SetGridPosition(int column, int row)
        {
            Grid.SetColumn(this, column);
            Grid.SetRow(this, row);
        }

        private void RecipeComboBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (PastInitiation)
            {
                ItemForDisplay.SelectedRecipe = RecipeComboBox.SelectedIndex;

                List<Item> items = new List<Item>();
                for (int i = 0; i < CraftingView.numOfSelectedItems; i++)
                {
                    items.Add(CraftingView.craftingStack[i]);
                }

                MainWindow.pageCV.ClearList();
                MainWindow.pageCV.ClearView();
                CraftingView.findCraftingStack = new List<Item>(items);
                CraftingView.craftingStack = new List<Item>(items);
                MainWindow.pageCV.RunTheStuff();
            }
        }
    }
}
