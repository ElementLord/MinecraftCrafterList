using MinecraftCrafterList.Controller;
using MinecraftCrafterList.Miscellaneous;
using MinecraftCrafterList.Model;
using MinecraftCrafterList.View;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace MinecraftCrafterList.CustomUserControl
{
    /// <summary>
    /// Interaction logic for ItemButton.xaml
    /// </summary>
    public partial class ItemButton : UserControl
    {
        public Item ItemObject { get; set; }
        private bool ShowQuanity { get; set; }

        public ItemButton(Item item, bool showQuanity, bool clickable)
        {
            InitializeComponent();

            ItemObject = item;
            ShowQuanity = showQuanity;
            HorizontalAlignment = HorizontalAlignment.Center;
            VerticalAlignment = VerticalAlignment.Center;

            RefreshItemButton();

            if (clickable)
            {
                ButtonForItem.ClickMode = ClickMode.Release;
                ButtonForItem.Click += ItemButton_Click;
            }
        }

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
                default:
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
            if (MainWindow.singleItemMode)
            {
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
                    MainWindow.pageCV.ClearList();
                    MainWindow.pageCV.ClearView();
                    CraftingView.numOfSelectedItems = 0;
                    CraftingView.firstTimeMulti = true;
                }

                if (ItemList.ItemSearch(CraftingView.findCraftingStack, ItemObject.Name).Count() == 1)
                {
                    CraftingView.findCraftingStack.Remove(ItemObject);
                    CraftingView.craftingStack.Remove(ItemObject);
                    ButtonForItem.BorderBrush = ColourSets.itemButtonBorderBase;
                    CraftingView.numOfSelectedItems--;
                }
                else
                {
                    CraftingView.findCraftingStack.Add(ItemObject);
                    CraftingView.craftingStack.Add(ItemObject);
                    ButtonForItem.BorderBrush = ColourSets.itemButtonBorderRed;
                    CraftingView.numOfSelectedItems++;
                }
            }
        }

        public void RefreshItemButton()
        {
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
                default:
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
