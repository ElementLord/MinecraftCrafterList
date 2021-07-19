using MinecraftCrafterList.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for CustomSpinner.xaml
    /// </summary>
    public partial class CustomSpinner : UserControl
    {
        private int CurrentNumber = 0;
        //public bool CraftingTextChanged { get; set; }
        public CraftingPanel CraftingPanel { get; set; }
        
        public CustomSpinner()
        {
            InitializeComponent();
            SpinDisplay.Text = CurrentNumber.ToString();
            //craftingPanel = (CraftingPanel)Parent;
            //if (CraftingTextChanged)
            //{
            //    SpinDisplay.TextChanged += SpinDisplay_Crafting_TextChanged;
            //}
        }

        public int GetCurrentNumber()
        {
            return CurrentNumber;
        }

        public void AddCraftingTextChanged()
        {
            SpinDisplay.TextChanged += SpinDisplay_Crafting_TextChanged;
        }

        private void SpinUp_Click(object sender, RoutedEventArgs e)
        {
            CurrentNumber++;
            SpinDisplay.Text = CurrentNumber.ToString();
            //craftingPanel.SetValue();
        }

        private void SpinDown_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentNumber <= 0)
            {
                CurrentNumber = 0;
            }
            else
            {
                CurrentNumber--;
            }

            SpinDisplay.Text = CurrentNumber.ToString();
            //craftingPanel.SetValue();
        }

        private void SpinDisplay_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void SpinDisplay_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SpinDisplay.Text.Equals(""))
            {
                CurrentNumber = 0;
            }
            else
            {
                CurrentNumber = int.Parse(SpinDisplay.Text);
            }

            //craftingPanel.SetValue();
        }

        private void SpinDisplay_Crafting_TextChanged(object sender, TextChangedEventArgs e)
        {
            //craftingPanel.SetValue();
            
            for (int i = 0; i < CraftingView.numOfSelectedItems; i++)
            {
                CraftingView.craftingItemsCount[i] = 1;
            }

            for (int i = /*1*/CraftingView.numOfSelectedItems; i < CraftingView.craftingItemsCount.Count(); i++)
            {
                CraftingView.craftingItemsCount[i] = 0;
            }

            MainWindow.pageCV.ReCalculate(0);
        }
    }
}
