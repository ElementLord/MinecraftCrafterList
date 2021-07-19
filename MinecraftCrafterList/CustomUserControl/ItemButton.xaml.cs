using MinecraftCrafterList.Controller;
using MinecraftCrafterList.Miscellaneous;
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

namespace MinecraftCrafterList.CustomUserControl
{
    /// <summary>
    /// Interaction logic for ItemButton.xaml
    /// </summary>
    public partial class ItemButton : UserControl
    {
        public Item ItemObject { get; set; }
        //public string ItemName { get; set; }
        //public BitmapImage ItemImage { get; set; }
        //public string ItemQuanity { get; set; }
        //public SolidColorBrush TextColour { get; set; }
        //public Color ShadowColour { get; set; }
        private bool ShowQuanity { get; set; }

        public ItemButton(Item item, bool showQuanity, bool clickable)
        {
            InitializeComponent();

            HorizontalAlignment = HorizontalAlignment.Center;
            VerticalAlignment = VerticalAlignment.Center;
            ShowQuanity = showQuanity;

            ItemObject = item;
            //ItemName = item.Name;
            //ToolTipLabel.Content = ItemName;
            //ItemImage = item.ImageBitmap;           //new BitmapImage(new Uri(item.ImageUrl, UriKind.Relative));
            //ImageForItem.ImageSource = ItemImage;

            RefreshItemButton();

            //switch (item.NameColor)
            //{
            //    case Item.TextColor.White:
            //        TextColour = ColourSets.whiteText;
            //        ShadowColour = ColourSets.whiteTextShadow;
            //        break;
            //    case Item.TextColor.Grey:
            //        TextColour = ColourSets.greyText;
            //        ShadowColour = ColourSets.greyTextShadow;
            //        break;
            //    case Item.TextColor.Blue:
            //        TextColour = ColourSets.blueText;
            //        ShadowColour = ColourSets.blueTextShadow;
            //        break;
            //    case Item.TextColor.Red:
            //        TextColour = ColourSets.redText;
            //        ShadowColour = ColourSets.redTextShadow;
            //        break;
            //}
            //ToolTipLabel.Foreground = TextColour;
            //ToolTipDropShadow.Color = ShadowColour;

            //StackPanel sp = new StackPanel();
            //sp.Children.Add(new Label()
            //{
            //    Content = item.Name,
            //    Padding = new Thickness(0, 0, 0, 0),
            //    Foreground = TextColour,    //whiteText
            //    Effect = new DropShadowEffect()
            //    {
            //        ShadowDepth = 3,
            //        Direction = 315,
            //        Color = ShadowColour,   //whiteTextShadow
            //        Opacity = 0.35,
            //        BlurRadius = 0.0
            //    }
            //});

            //if (!item.FlavourText1.Equals("-"))
            //{
            //    /*sp*/
            //    ToolTipStack.Children.Add(NewFlavourTextBlock(item.FlavourText1, item.FlavourTextColor1));
            //}

            //if (!item.FlavourText2.Equals("-"))
            //{
            //    /*sp*/
            //    ToolTipStack.Children.Add(NewFlavourTextBlock(item.FlavourText2, item.FlavourTextColor2));
            //}

            //tooltipBorder.Child = sp;
            //ToolTip = new ToolTip()//ToolTip tt
            //{
            //    Content = sp,
            //    Background = ColourSets.toolTipBackground,
            //    BorderBrush = ColourSets.toolTipBorder,
            //};// = tt;//tooltipBorder


            //if (showQuanity && (item.CurrentRecipeNumPerCraft() > 1))//quanity
            //{
            //    ItemQuanity = item.CurrentRecipeNumPerCraft().ToString();
            //}
            //else
            //{
            //    ItemQuanity = "";
            //}
            //Quanity.Content = ItemQuanity;

            if (clickable)
            {
                ButtonForItem.ClickMode = ClickMode.Release;
                ButtonForItem.Click += ItemButton_Click;
            }
        }

        /*public void SetItem(Item item, bool showQuanity, bool clickable)
        {
            ItemObject = item;
            ItemName = item.Name;
            ItemImage = new BitmapImage(new Uri(item.ImageUrl, UriKind.Relative));

            switch (item.NameColor)
            {
                case Item.TextColor.White:
                    TextColour = ColourSets.whiteText;
                    ShadowColour = ColourSets.whiteTextShadow;
                    break;
                case Item.TextColor.Grey:
                    TextColour = ColourSets.greyText;
                    ShadowColour = ColourSets.greyTextShadow;
                    break;
                case Item.TextColor.Blue:
                    TextColour = ColourSets.blueText;
                    ShadowColour = ColourSets.blueTextShadow;
                    break;
                case Item.TextColor.Red:
                    TextColour = ColourSets.redText;
                    ShadowColour = ColourSets.redTextShadow;
                    break;
            }

            StackPanel sp = new StackPanel();
            sp.Children.Add(new Label()
            {
                Content = item.Name,
                Padding = new Thickness(0, 0, 0, 0),
                Foreground = TextColour,//whiteText
                Effect = new DropShadowEffect()
                {
                    ShadowDepth = 3,
                    Direction = 315,
                    Color = ShadowColour,   //whiteTextShadow
                    Opacity = 0.35,
                    BlurRadius = 0.0
                }
            });

            if (!item.FlavourText1.Equals("-"))
            {
                sp.Children.Add(NewFlavourTextBlock(item.FlavourText1, item.FlavourTextColor1));
            }

            if (!item.FlavourText2.Equals("-"))
            {
                sp.Children.Add(NewFlavourTextBlock(item.FlavourText2, item.FlavourTextColor2));
            }

            //tooltipBorder.Child = sp;
            ToolTip = new ToolTip()//ToolTip tt
            {
                Content = sp,
                Background = ColourSets.toolTipBackground,
                BorderBrush = ColourSets.toolTipBorder,
            };// = tt;//tooltipBorder
            

            if (showQuanity && (item.CurrentRecipeNumPerCraft() > 1))//quanity
            {
                ItemQuanity = item.CurrentRecipeNumPerCraft().ToString();
            }
            else
            {
                ItemQuanity = "";
            }

            if (clickable)
            {
                ButtonForItem.ClickMode = ClickMode.Release;
                ButtonForItem.Click += ItemButton_Click;
            }
        }*/

        public void SetGridPosition(int column, int row)
        {
            Grid.SetColumn(this, column);
            Grid.SetRow(this, row);
        }

        private TextBlock NewFlavourTextBlock(string flavourText, Item.TextColor textColorNum)
        {
            SolidColorBrush textColour = ColourSets.whiteText;
            Color shadowColour = ColourSets.whiteTextShadow;

            switch (textColorNum)
            {
                case Item.TextColor.White:
                    //textColour = whiteText;
                    //shadowColour = whiteTextShadow;
                    break;
                case Item.TextColor.Grey:
                    textColour = ColourSets.greyText;
                    shadowColour = ColourSets.greyTextShadow;
                    break;
                case Item.TextColor.Blue:
                    textColour = ColourSets.blueText;
                    shadowColour = ColourSets.blueTextShadow;
                    break;
                case Item.TextColor.Red:
                    textColour = ColourSets.redText;
                    shadowColour = ColourSets.redTextShadow;
                    break;
            }

            return new TextBlock()
            {
                MaxWidth = 200,
                TextWrapping = TextWrapping.WrapWithOverflow,
                Text = flavourText,   //"Unused at the current time",
                Foreground = textColour,
                Effect = new DropShadowEffect()
                {
                    ShadowDepth = 3,
                    Direction = 315,
                    Color = shadowColour,
                    Opacity = 0.35,
                    BlurRadius = 0.0
                }
            };
        }

        private void ItemButton_Click(object sender, RoutedEventArgs e)
        {
            /*Button itemButtonPressed = (Button)sender;
            IEnumerable<Item> itemQuery = ItemList.ItemSearch(fullItemList, itemButtonPressed.Name.Replace('_', ' '));
            Item resultItem = itemQuery.First();*/

            if (MainWindow.singleItemMode == true)/*Single_Item.IsChecked*/
            {
                //ClearListAndView(true, true, true);
                //CraftingView.findCraftingStack.Clear();
                //CraftingView.craftingStack.Clear();
                //CraftingView.craftingItemsCount.Clear();
                MainWindow.pageCV.ClearList();
                MainWindow.pageCV.ClearView();

                CraftingView.findCraftingStack.Add(ItemObject);
                CraftingView.craftingStack.Add(ItemObject);
                CraftingView.numOfSelectedItems = 1;

                MainWindow.pageCV.RunTheStuff();
            }
            else
            {
                if (CraftingView.firstTimeMulti == false)
                {
                    //ClearListAndView(true, true, true);
                    MainWindow.pageCV.ClearList();
                    MainWindow.pageCV.ClearView();
                    CraftingView.numOfSelectedItems = 0;
                    CraftingView.firstTimeMulti = true;
                }

                if (ItemList.ItemSearch(CraftingView.findCraftingStack, ItemObject.Name).Count() == 1)
                {
                    CraftingView.findCraftingStack.Remove(ItemObject);
                    CraftingView.craftingStack.Remove(ItemObject);
                    ButtonForItem.BorderBrush = ColourSets.itemButtonBorderBase;//itemButtonPressed.
                    CraftingView.numOfSelectedItems--;
                }
                else
                {
                    CraftingView.findCraftingStack.Add(ItemObject);
                    CraftingView.craftingStack.Add(ItemObject);
                    ButtonForItem.BorderBrush = ColourSets.itemButtonBorderRed;//itemButtonPressed.
                    CraftingView.numOfSelectedItems++;
                }
            }

            ////////////////////////////////////////////////////////////////////////////////////////
            /*else if (Multiple_Items.IsChecked == true)
            {
                if (firstTimeMulti == false)
                {
                    ClearListAndView(true, true, true);
                    numOfSelectedItems = 0;
                    firstTimeMulti = true;
                }

                if (ItemList.ItemSearch(findCraftingStack, resultItem.Name).Count() == 1)
                {
                    findCraftingStack.Remove(resultItem);
                    craftingStack.Remove(resultItem);
                    itemButtonPressed.BorderBrush = itemButtonBorderBase;
                    numOfSelectedItems--;
                }
                else
                {
                    findCraftingStack.Add(resultItem);
                    craftingStack.Add(resultItem);
                    itemButtonPressed.BorderBrush = itemButtonBorderRed;
                    numOfSelectedItems++;
                }
            }*/
        }

        public void RefreshItemButton()
        {
            //ItemName = ItemObject.Name;
            //ItemImage = ItemObject.ImageBitmap;
            //ItemQuanity = ItemObject.CurrentRecipeNumPerCraft().ToString();
            ToolTipLabel.Content = ItemObject.Name;
            ImageForItem.ImageSource = ItemObject.ImageBitmap;

            switch (ItemObject.NameColor)
            {
                case Item.TextColor.White:
                    ToolTipLabel.Foreground = ColourSets.whiteText;
                    ToolTipDropShadow.Color = ColourSets.whiteTextShadow;
                    break;
                case Item.TextColor.Grey:
                    ToolTipLabel.Foreground = ColourSets.greyText;
                    ToolTipDropShadow.Color = ColourSets.greyTextShadow;
                    break;
                case Item.TextColor.Blue:
                    ToolTipLabel.Foreground = ColourSets.blueText;
                    ToolTipDropShadow.Color = ColourSets.blueTextShadow;
                    break;
                case Item.TextColor.Red:
                    ToolTipLabel.Foreground = ColourSets.redText;
                    ToolTipDropShadow.Color = ColourSets.redTextShadow;
                    break;
            }

            if (!ItemObject.FlavourText1.Equals("-"))
            {
                ToolTipStack.Children.Add(NewFlavourTextBlock(ItemObject.FlavourText1, ItemObject.FlavourTextColor1));
            }

            if (!ItemObject.FlavourText2.Equals("-"))
            {
                ToolTipStack.Children.Add(NewFlavourTextBlock(ItemObject.FlavourText2, ItemObject.FlavourTextColor2));
            }

            if (ShowQuanity && (ItemObject.CurrentRecipeNumPerCraft() > 1))
            {
                Quanity.Content = ItemObject.CurrentRecipeNumPerCraft().ToString();
            }
        }

    }
}
