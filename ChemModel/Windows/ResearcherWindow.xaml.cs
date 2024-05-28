using ChemModel.ViewModels;
using ScottPlot;
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
using System.Windows.Shapes;
using ChemModel.Errors;
using static ChemModel.ViewModels.ICloseWindow;

namespace ChemModel.Windows
{
    /// <summary>
    /// Interaction logic for ResearcherWindow.xaml
    /// </summary>
    public partial class ResearcherWindow : Window
    {
        public ResearcherWindow()
        {
            Loaded += ResearcherWindow_Loaded;
            InitializeComponent();
            this.Closed += (sender, e) => Owner.Close();
            var paramsModel = new ParamsViewModel();
            DataContext = paramsModel;
            matCombo.SelectionChanged += (sender, e) => paramsModel.MaterialSelected();
            

        }

      
        private void ResearcherWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Validation.GetHasError(this);
            if (DataContext is ICloseWindows viewModel)
            {
                viewModel.Close +=
                    () => { this.Close(); };

                Closing += (s, e) => { e.Cancel = !viewModel.CanClose(); };

                (DataContext as ParamsViewModel).TList = GetAllTextBoxes(AsdGrid);

               
            }

            
        }

        private static readonly Regex _posReg = new Regex("[^0-9,]+");
        private static readonly Regex _reg = new Regex("[^0-9,-]+");

        private static bool IsTextAllowedPos(string text)
        {
            return !_posReg.IsMatch(text);
        }

        public static List<TextBox> GetAllTextBoxes(DependencyObject depObj)
        {
            var textBoxes = new List<TextBox>();
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                var child = VisualTreeHelper.GetChild(depObj, i);
                if (child is TextBox)
                {
                    textBoxes.Add(child as TextBox);
                }
                else if (child is DependencyObject)
                {
                    textBoxes.AddRange(GetAllTextBoxes(child));
                }
            }
            return textBoxes;
        }
       

        private async Task Something()
        {
            while (true)
            {
               
                Thread.Sleep(250);
            }
        }

        private static bool IsTextAllowed(string text)
        {

            return !_reg.IsMatch(text);
        }

        private void TextBox_PreviewPositive(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowedPos(e.Text);
        }

        private void TextBox_Preview(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }
    }
}