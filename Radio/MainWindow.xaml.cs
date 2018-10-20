using System;
using System.Collections;
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
using System.IO;
using Un4seen.Bass;
using System.Reflection;
using System.Windows.Resources;

namespace Radio
{
    public partial class MainWindow : Window
    {      
        public MainWindow()
        {
            InitializeComponent();
            ClassConfirm.TextBox = textBox;
        }



        /// <summary>
        /// GIF Animation's playing
        /// </summary>
        private void singularity_MediaEnded(object sender, RoutedEventArgs e)
        {
            singularity.Position = new TimeSpan(0, 0, 1);
            singularity.Play();
        }



        /// <summary>
        /// Button Play/Pause (Start or Pause stream playing)
        /// </summary>
        public static bool play = true;
        private void Play_Click(object sender, RoutedEventArgs e)
        {
            if (textBox.Text != string.Empty)
            {
                if ((textBox.Text.StartsWith("http://")) || (textBox.Text.StartsWith("https://")))
                {
                    string url = textBox.Text;
                    ButtonPlay.Background = new ImageBrush(new BitmapImage(new Uri("icons/Play.png", UriKind.Relative)));
            
                    if (play)
                    {
                        ClassBass.Play(url, ClassBass.Volume);
                        play = false;
                        ButtonPlay.Background = new ImageBrush(new BitmapImage(new Uri("icons/Pause.png", UriKind.Relative)));
                    }
                    else
                    {
                        ClassBass.Pause();
                        play = true;
                        ButtonPlay.Background = new ImageBrush(new BitmapImage(new Uri("icons/Play.png", UriKind.Relative)));
                    }
                }
                else MessageBox.Show("Error! URL is incorrect.");
            }
            else MessageBox.Show("Error! Input the radiostation's URL.");
        }



        /// <summary>
        /// Button Stop stream playing
        /// </summary>
        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            ClassBass.Stop();
            ButtonPlay.Background = new ImageBrush(new BitmapImage(new Uri("icons/Play.png", UriKind.Relative)));
        }



        /// <summary>
        /// Button Add to Playlist
        /// </summary>
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (textBox.Text != string.Empty)
            {
                if ((textBox.Text.StartsWith("http://")) || (textBox.Text.StartsWith("https://")))
                {   
                    // File creating if it isn't created before
                    FileInfo file = new FileInfo("playlist.txt");
                    if (file.Exists == false)
                        file.Create();
                    else // If file is alredy exists
                    {
                        // Adding radiostation's URL to the file
                        int i = 0;
                        string link = textBox.Text, j;
                        StreamReader streamReader1 = new StreamReader("playlist.txt");

                        // Checking of already existing links
                        while (!streamReader1.EndOfStream)
                        {
                            j = streamReader1.ReadLine();
                            if (link == j) i++;
                        }
                        streamReader1.Close();

                        // If there are no copies of the link --> Add to file
                        if (i == 0)
                        {
                            File.AppendAllText("playlist.txt", link);
                            StreamWriter playlist;
                            playlist = file.AppendText();
                            playlist.WriteLine();
                            playlist.Close();
                            MessageBox.Show("The URL was added to the playlist.");
                        }
                        else MessageBox.Show("Error! URL already exists.");
                    }
                }
                else MessageBox.Show("Error! URL is incorrect.");
            }
            else MessageBox.Show("Error! Input the radiostation's URL.");
        }



        /// <summary>
        /// Show Playlist window
        /// </summary>
        public static bool windowplaylistopened = false;
        public void ButtonList_Click(object sender, RoutedEventArgs e)
        {
            if (windowplaylistopened == false)
            {
                Playlist playlist = new Playlist();
                playlist.Closed += delegate { windowplaylistopened = false; };
                playlist.Show();
                windowplaylistopened = true;
            }
            else MessageBox.Show("Error! This window is already open.");
        }



        /// <summary>
        /// Recording
        /// </summary>
        public static bool record = true;
        private void ButtonRecording_Click(object sender, RoutedEventArgs e)
        {
            if (record)
            {
                Recorder.Start();
                record = false;
                ButtonRecording.Background = new ImageBrush(new BitmapImage(new Uri("icons/StopRecording.png", UriKind.Relative)));
            }
            else
            {
                Recorder.Stop();
                record = true;
                ButtonRecording.Background = new ImageBrush(new BitmapImage(new Uri("icons/StartRecording.png", UriKind.Relative)));
            }
        }



        /// <summary>
        /// Volume
        /// </summary>
        private void Slider_Value(object sender, RoutedPropertyChangedEventArgs<double> e)
        { ClassBass.SetVolumeToStream(ClassBass.Stream, (int)Slider.Value); }



        /// <summary>
        /// Possibility of dragging the window
        /// </summary>
        private void Border_MouseMove(object sender, MouseButtonEventArgs e) { DragMove(); }



        /// <summary>
        /// Button Minimize window
        /// </summary>
        private void ButtonHide_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
                WindowState = WindowState.Minimized;
        }



        /// <summary>
        /// Button Close window
        /// </summary>
        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            foreach (Window w in App.Current.Windows)
                w.Close();
        }



        /// <summary>
        /// Info Menu Item (Open Info window)
        /// </summary>
        public static bool windowinfoopened = false;
        private void Info_Click(object sender, RoutedEventArgs e)
        {
            if (windowinfoopened == false)
            {
                Info info = new Info();
                info.Closed += delegate { windowinfoopened = false; };
                info.Show();
                windowinfoopened = true;
            }
            else MessageBox.Show("Error! This window is already open.");
        }



        /// <summary>
        /// Author Menu Item (Open Author window)
        /// </summary>
        public static bool windowauthoropened = false;
        private void Author_Click(object sender, RoutedEventArgs e)
        {
            if (windowauthoropened == false)
            {
              Author author = new Author();
              author.Closed += delegate { windowauthoropened = false; };
              author.Show();
              windowauthoropened = true;
            }
            else MessageBox.Show("Error! This window is already open.");
        }
    }
}