using System;
using System.Collections.Generic;
using System.IO;
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
using Un4seen.Bass;
using Un4seen.Bass.AddOn.Enc;
using Un4seen.Bass.Misc;
using System.Runtime.InteropServices;

namespace Radio
{
    public class ClassBass
    {     
        public static bool InitDefaultDevice;
        public static int Stream;
        public static int DS = 44100;

        /// <summary>
        /// Bass.dll initialization
        /// </summary>
        private static bool InitBass(int ds)
        {
            if (!InitDefaultDevice)
                InitDefaultDevice = Bass.BASS_Init(-1, ds, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero);
            return InitDefaultDevice;
        }

        /// <summary>
        /// Play
        /// </summary>
        public static void Play(string url, int vol)
        {
            if (InitBass(DS))
            {
                if (Stream == 0) // If Stream is empty --> create new Stream
                {
                    Stream = Bass.BASS_StreamCreateURL(url, 0, BASSFlag.BASS_DEFAULT, null, IntPtr.Zero);
                    Volume = vol;
                    Bass.BASS_ChannelSetAttribute(Stream, BASSAttribute.BASS_ATTRIB_VOL, Volume / 100F);
                    Bass.BASS_ChannelPlay(Stream, false);
                }
                else // else continue playback
                {
                    Volume = vol;
                    Bass.BASS_ChannelSetAttribute(Stream, BASSAttribute.BASS_ATTRIB_VOL, Volume / 100F);
                    Bass.BASS_ChannelPlay(Stream, false);
                }
            }
            else MessageBox.Show("Bass.dll initialization error!");
        }



        /// <summary>
        /// Pause
        /// </summary>
        public static void Pause() { Bass.BASS_ChannelPause(Stream); }



        /// <summary>
        /// Stop playing
        /// </summary>
        public static void Stop()
        {
            Bass.BASS_ChannelStop(Stream);
            Stream = 0;
        }



        public static int Volume = 100;
        /// <summary>
        /// Volume
        /// </summary>
        public static void SetVolumeToStream(int stream, int vol)
        {
            Volume = vol;
            Bass.BASS_ChannelSetAttribute(stream, BASSAttribute.BASS_ATTRIB_VOL, Volume / 100F);
        }
    }
}