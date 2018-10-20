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
    /// <summary>
    /// Логика взаимодействия для Info.xaml
    /// </summary>
    public partial class Info : Window
    {
        public Info()
        {
            InitializeComponent();
        }



        /// <summary>
        /// Possibility of dragging the window
        /// </summary>
        private void Border_MouseMove(object sender, MouseButtonEventArgs e) { DragMove(); }



        /// <summary>
        /// Button Close window
        /// </summary>
        private void ButtonClose_Click(object sender, RoutedEventArgs e) { Close(); }
    }
}
