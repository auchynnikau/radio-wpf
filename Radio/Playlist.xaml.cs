using System;
using System.IO;
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
    public partial class Playlist : Window
    {
        /// <summary>
        /// Playlist opening
        /// </summary>
        public Playlist()
        {
            InitializeComponent();
            // Displaing of all links after opening the Playlist window
            listBox.Items.Clear();
            StreamReader streamReader2 = new StreamReader("playlist.txt");
            string str = "";
            while (!streamReader2.EndOfStream)
            {
                str = streamReader2.ReadLine();
                listBox.Items.Add(str);
            }
            streamReader2.Close();
        }



        /// <summary>
        /// Button Refresh (Reading all links form the file and displaying them)
        /// </summary>
        private void ButtonRefresh_Click(object sender, RoutedEventArgs e)
        {
            listBox.Items.Clear();
            StreamReader streamReader2 = new StreamReader("playlist.txt");
            string str = "";
            while (!streamReader2.EndOfStream)
            {
                str = streamReader2.ReadLine();
                listBox.Items.Add(str);
            }
        }



        /// <summary>
        /// Button Confirm (Copying of choosen link from the listBox to the textBox)
        /// </summary>
        private void Confirm_Click(object sender, RoutedEventArgs e)
        { ClassConfirm.TextBox.Text = listBox.SelectedItem.ToString(); }



        /// <summary>
        /// Button Delete (Delete choosen links from ListBox and text file (playlist.txt))
        /// </summary>
        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (listBox.SelectedIndex != -1)
            {
                listBox.Items.RemoveAt(listBox.SelectedIndex);
                StreamWriter streamwriter1 = new StreamWriter("playlist.txt");

                foreach (string line in listBox.Items)
                    streamwriter1.WriteLine(line);
                streamwriter1.Close();
                MessageBox.Show("The URL was removed from the playlist.");
            }
            else
                MessageBox.Show("Error! Select the URL you want to remove.");
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