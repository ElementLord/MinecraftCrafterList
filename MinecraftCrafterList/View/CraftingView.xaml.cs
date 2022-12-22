using MinecraftCrafterList.Controller;
using MinecraftCrafterList.CustomUserControl;
using MinecraftCrafterList.Model;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MinecraftCrafterList.View
{
    /// <summary>
    /// Interaction logic for CraftingView.xaml
    /// </summary>
    public partial class CraftingView : Page
    {
        //Full Lists
        /**/

        //Crafting Lists and Variables
        public static List<Item> findCraftingStack = new List<Item>();
        public static List<Item> craftingStack = new List<Item>();
        public static List<int> craftingItemsCount = new List<int>();
        public static int numOfSelectedItems = 0;

        //Multi-Item Selection Variables
        public static bool firstTimeMulti = false;

        public CraftingView()
        {
            InitializeComponent();
            PopulateItemDisplay();
        }

        public void PopulateItemDisplay()
        {
            int gridColumn = 0;
            int gridRow = 0;
            int maxColumns = 4;

            ItemDisplay.Children.Clear();
            ItemDisplay.RowDefinitions.Clear();
            ItemDisplay.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(45) });
            //ItemDisplay.ColumnDefinitions.Clear();

            foreach (Item i in MainWindow.fullItemList)
            {
                ItemButton b = new ItemButton(i, false, true);

                b.SetGridPosition(gridColumn, gridRow);
                ItemDisplay.Children.Add(b);
                gridColumn++;

                if (gridColumn > (maxColumns - 1))
                {
                    ItemDisplay.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(45) });
                    gridColumn = 0;
                    gridRow++;
                }
            }
        }

        //Menu Control Actions
        //Dynamic Control Actions
        /**/

        //Crafting View Elements Methods
        public void RunTheStuff()
        {
            List<Item> craftingStackPreChange = new List<Item>(craftingStack);

            FindAllCraftingItems();

            for (int i = 0; i < craftingStack.Count(); i++)
            {
                craftingItemsCount.Add(0);
            }

            foreach (Item j in craftingStackPreChange)
            {
                craftingItemsCount[craftingStack.IndexOf(j)]++;
            }

            DisplayCraft(0, Crafting_List);
        }

        public void FindAllCraftingItems()
        {
            Item frontItem = findCraftingStack.First();

            foreach (string name in frontItem.CurrentRecipeItemSlots())
            {
                IEnumerable<Item> craftingItemQuery = ItemList.ItemSearch(MainWindow.fullItemList, name); //Check item 

                if (craftingItemQuery.Count() != 0)
                {
                    Item craftingItem = craftingItemQuery.First();
                    
                    /* Loop Detector */
                    bool loopDetected = false;
                    foreach (string s in craftingItem.CurrentRecipeItemSlots())
                    {
                        if (s.Equals(frontItem.Name))
                        {
                            loopDetected = true;
                        }
                    }

                    if (loopDetected)
                    {
                        craftingItem.SelectedRecipe++;
                        if (craftingItem.SelectedRecipe >= craftingItem.Recipes.Count())
                        {
                            craftingItem.SelectedRecipe = 0;
                        }

                        MessageBox.Show("Selected recipe caused a loop! Selecting next receipe.", "Error");
                    }
                    ///////////////////////////

                    if (!craftingStack.Contains(craftingItem))
                    {
                        craftingStack.Add(craftingItem);
                    }
                    else
                    {
                        craftingStack.Remove(craftingItem);
                        craftingStack.Add(craftingItem);
                    }

                    if (!findCraftingStack.Contains(craftingItem))
                    {
                        findCraftingStack.Add(craftingItem);
                    }
                    else
                    {
                        findCraftingStack.Remove(craftingItem);
                        findCraftingStack.Add(craftingItem);
                    }
                }
            }

            if (findCraftingStack.Count() > 1)
            {
                findCraftingStack.Remove(frontItem);
                FindAllCraftingItems();
            }
            else
            {
                findCraftingStack.Remove(frontItem);
            }
        }

        public void DisplayCraft(int step, Grid craftingList)
        {
            Item currentItem = craftingStack.ElementAt(step);
            CraftingPanel cPanel = new CraftingPanel(currentItem, step);

            craftingList.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(220) });
            cPanel.SetGridPosition(0, step);
            craftingList.Children.Add(cPanel);
            ++step;

            if (step < craftingStack.Count())
            {
                DisplayCraft(step, craftingList);
            }
        }

        //Control Creators and Editors
        /**/

        //Utility Methods
        /**/

        public void ClearList()
        {
            findCraftingStack.Clear();
            craftingStack.Clear();
            craftingItemsCount.Clear();
        }

        public void ClearView()
        {
            Crafting_List.Children.Clear();
            Crafting_List.RowDefinitions.Clear();
        }

        public UIElement SearchView(int rowPos)
        {
            return Crafting_List.Children
                .Cast<UIElement>()
                .Where(i => Grid.GetRow(i) == rowPos) /*&& i.Uid.Contains(query)*/
                .First<UIElement>();
        }

        public void ReCalculate(int rowPos)
        {
            ((CraftingPanel)SearchView(rowPos)).SetValue(); //Could use a while loop have instead

            if (++rowPos < craftingItemsCount.Count())
            {
                ReCalculate(rowPos);
            }
        }
    }
}
