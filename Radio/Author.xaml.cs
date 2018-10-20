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
using System.Windows.Shapes;

namespace Radio
{
    public partial class Author : Window
    {
        public Author()
        {
            InitializeComponent();
        }



        /// <summary>
        /// Button Close the window
        /// </summary>
        private void ButtonClose_Click(object sender, RoutedEventArgs e) { Close(); }



        /// <summary>
        /// Possibility of dragging the window
        /// </summary>
        private void Border_MouseMove(object sender, MouseButtonEventArgs e) { DragMove(); }
    }
}
