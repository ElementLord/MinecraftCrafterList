using MinecraftCrafterList.Controller;
using MinecraftCrafterList.CustomUserControl;
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
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MinecraftCrafterList.View
{
    /// <summary>
    /// Interaction logic for CraftingView.xaml
    /// </summary>
    public partial class CraftingView : Page
    {
        //Full Lists
        //public static List<Recipe> fullRecipeList = RecipeList.GatherRecipes();
        //public static List<Item> fullItemList = ItemList.GatherItems2(fullRecipeList);

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
            //int currentRowCount = ItemDisplay.RowDefinitions.Count();
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
                    //if (currentRowCount <= gridRow)
                    //{
                        
                    //}
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
            //ClearListAndView(true, true, true);
            
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

            DisplayCraft(0, Crafting_List);/*ingReceipe*/
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
                    ////
                    
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
            //int craftingTypeNum = (int)currentItem.CurrentRecipeCraftingType();
            //int numOfUsedItems = 0;
            //double[,] itemMargins = { };
            //double[,] resultMargins = new double[5, 2]
            //{
            //    { 66, 93 },     //No Crafting Result Slot
            //    { 297, 93 },    //Crafting Table Result Slot
            //    { 201, 93 },    //Smelting Result Slot
            //    { 297, 93 },    //Brewing Result Slot
            //    { 297, 93 }     //Other Result Slot
            //};

            //double amountOfCrafts = Math.Ceiling(craftingItemsCount.ElementAt(step) / (currentItem.CurrentRecipeNumPerCraft() * 1.0));
            //double totalFromCraft = currentItem.CurrentRecipeNumPerCraft() * amountOfCrafts;
            //double totalStackFromCraft = totalFromCraft / currentItem.StackSize;
            //double remainderFromCraft = totalFromCraft - craftingItemsCount.ElementAt(step);

            craftingList.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(220) });
            CraftingPanel cp = new CraftingPanel(currentItem, step);
            cp.SetGridPosition(0, step);
            craftingList.Children.Add(cp);

            //switch (currentItem.CurrentRecipeCraftingType())
            //{
            //    case Recipe.CraftingType.No_Recipe:
            //        //amountOfCrafts = craftingItemsCount.ElementAt(step);
            //        //totalFromCraft = craftingItemsCount.ElementAt(step);
            //        //totalStackFromCraft = craftingItemsCount[step] / (currentItem.StackSize * 1.0);
            //        //remainderFromCraft = totalFromCraft - craftingItemsCount.ElementAt(step);
            //        break;
            //    case Recipe.CraftingType.Crafting_Table:
            //        numOfUsedItems = 9;
            //        //itemMargins = new double[9, 2]  //double[,] craftingTableMargins; 
            //        //{
            //        //    { 17, 44 },     //Left Top Slot
            //        //    { 66, 44 },     //Centre Top Slot
            //        //    { 115, 44 },    //Right Top Slot
            //        //    { 17, 93 },     //Left Middle Slot
            //        //    { 66, 93 },     //Centre Middle Slot
            //        //    { 115, 93 },    //Right Middle Slot
            //        //    { 17, 142 },    //Left Bottom Slot
            //        //    { 66, 142 },    //Centre Bottom Slot
            //        //    { 115, 142 }    //Right Bottom Slot
            //        //};
            //        break;
            //    case Recipe.CraftingType.Smelting:
            //        numOfUsedItems = 1;
            //        //itemMargins = new double[1, 2]  //double[,] smeltingMargins;
            //        //{
            //        //    { 66, 44 },     //Item Slot
            //        //};
            //        break;
            //    case Recipe.CraftingType.Brewing:
            //        numOfUsedItems = 4;
            //        //itemMargins = new double[4, 2]  //double[,] brewingMargins
            //        //{
            //        //    { 91, 24 },     //Item Slot    
            //        //    { 27, 118 },    //Bottle Slot 1
            //        //    { 91, 142 },    //Bottle Slot 2
            //        //    { 155, 118 }    //Bottle Slot 3
            //        //};
            //        break;
            //    case Recipe.CraftingType.Other:
            //        numOfUsedItems = 2;
            //        //itemMargins = new double[2, 2]   //double[,] otherMargins 
            //        //{
            //        //    { 17, 93 },     //Left Slot
            //        //    { 115, 93 },    //Right Slot
            //        //};
            //        break;
            //}

            //Displays the Crafting Table UI
            //Grid craftingGrid = NewCraftingUI(currentItem.CurrentRecipeCraftingType());
            //AddingGridChildern(craftingList, craftingGrid, 0, step);

            //Displays crafted items
            //ItemButtonCreator(currentItem, resultMargins[craftingTypeNum, 0], resultMargins[craftingTypeNum, 1]/*297, 93*/, false, false, 0, 0, true, craftingGrid);

            //Displays the number of times the crafting operation is to be done
            //Label numOfOperation = NewLabel("Operation_Num_", "x " + amountOfCrafts, 48, 382, 90, 0, 0);
            //AddingGridChildern(craftingList, numOfOperation, 0, step);

            //Displays the number of items made
            //Label itemTotal = NewLabel("Item_Total_", "Total Items (Stacks): " + totalFromCraft + " (" + totalStackFromCraft + ")", 12, 382, 10, 0, 0);
            //AddingGridChildern(craftingList, itemTotal, 0, step);

            //Displays the number of items not used (In general)
            //Label itemRemainder = NewLabel("Item_Remainder_", "Remaining/Unused Items: " + remainderFromCraft, 12, 382, 30, 0, 0);
            //AddingGridChildern(craftingList, itemRemainder, 0, step);

            //Displays the number of items required
            //Label itemRequired = NewLabel("Item_Required_", "Required Items: " + craftingItemsCount.ElementAt(step), 12, 382, 50, 0, 0);
            //AddingGridChildern(craftingList, itemRequired, 0, step);

            //Displays list of crafting recipe
            //ComboBox recipeBox = NewComboBox(currentItem);
            //AddingGridChildern(craftingList, recipeBox, 0, step);

            //Spinner
            //Label itemExtra = NewLabel("Item_Extra_", "Extra Items: ", 12, 625, 30, 0, 0);
            //AddingGridChildern(craftingList, itemExtra, 0, step);
            //TextBox spinnerNumDisplay = NewSpinnerTextBox(currentItem);
            //spinnerNumDisplay.TextChanged += SpinnerTextBox_TextChanged;
            //AddingGridChildern(craftingList, spinnerNumDisplay, 0, step);
            //Button spinnerNumUp = NewSpinnerButton(currentItem, true);
            //AddingGridChildern(craftingList, spinnerNumUp, 0, step);
            //Button spinnerNumDown = NewSpinnerButton(currentItem, false);
            //AddingGridChildern(craftingList, spinnerNumDown, 0, step);

            //Displays the items used in the crafting reciepe
            //for (int i = 0; i < numOfUsedItems; i++)
            //{
            //    IEnumerable<Item> craftingItemQuery = ItemList.ItemSearch(MainWindow.fullItemList, currentItem.CurrentRecipeItemSlots()[i]);

            //    if (craftingItemQuery.Count() != 0)
            //    {
            //        //ItemButtonCreator(craftingItemQuery.First(), itemMargins[i, 0], itemMargins[i, 1], false, false, 0, 0, false, craftingGrid);

            //        for (int j = 0; j < cp.AmountOfCrafts; j++)
            //        {
            //            craftingItemsCount[craftingStack.IndexOf(craftingItemQuery.First())]++;
            //        }
            //    }
            //}

            ++step;

            if (step < craftingStack.Count())
            {
                DisplayCraft(step, craftingList);/*craftingStack.ElementAt(step),*//*ingReceipe*/
            }

        }

        //Control Creators and Editors
        /**/

        //Utility Methods
        /**/
        //public void AddingGridChildern(Grid parent, UIElement child, int column, int row)
        //{
        //    Grid.SetColumn(child, column);
        //    Grid.SetRow(child, row);
        //    parent.Children.Add(child);
        //}

        public void ClearList() //bool fCSClear, bool cSAndICClear, bool viewClear
        {
            findCraftingStack.Clear();
            craftingStack.Clear();
            craftingItemsCount.Clear();
            //if (fCSClear) { } if (cSAndICClear) { } if (viewClear) { }
        }

        public void ClearView()
        {
            Crafting_List.Children.Clear();
            Crafting_List.RowDefinitions.Clear();
        }

        public UIElement SearchView(int rowPos)/*, string query*/
        {
            return Crafting_List.Children
                .Cast<UIElement>()
                .Where(i => Grid.GetRow(i) == rowPos) /*&& i.Uid.Contains(query)*/
                .First<UIElement>();
        }

        public void ReCalculate(int rowPos)/*, int[] itemsCount*/
        {
            ((CraftingPanel)SearchView(rowPos)).SetValue(); //Could use a while loop have instead

            if (++rowPos < craftingItemsCount.Count())
            {
                ReCalculate(rowPos);/*view,*//*, itemsCount*/
            }
        }

    }
}
