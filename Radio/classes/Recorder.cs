    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading.Tasks;
    using Un4seen.Bass;

    namespace Radio
    {
        public static class Recorder
        {
            #region Fileds

            private static int handle_;
            private static bool isReady_;
            private static bool isReccording_;
            private static FileStream recordFileStream_;
            private static RECORDPROC _recordProc;

            #endregion



            #region Properties

            private static int Handle
            {
                get { return handle_; }
                set { handle_ = value; }
            }

            public static bool IsReady
            {
                get { return isReady_; }
                private set { isReady_ = value; }
            }

            public static bool IsRecording
            {
                get { return isReccording_; }
                private set { isReccording_ = value; }
            }

            #endregion



            #region Methods

            private static bool RecordProc(int handle, IntPtr buffer, int length, IntPtr user)
            {
                byte[] byteBuffer = new byte[length];
                Marshal.Copy(buffer, byteBuffer, 0, length);
                recordFileStream_.Write(byteBuffer, 0, length);
                return true;
            }



            public static void Start()
            {
                if (IsReady = Bass.BASS_RecordInit( /*Properties.Settings.Default.BassRecordDevice*/-1))
                {
                    string fileTitle = "Tile";
                    string fileToSave = Path.Combine(Environment.CurrentDirectory,
                        string.Format("{0}.{1}.wav", fileTitle, DateTime.Now.ToString("yyyy-MM-dd.HH-mm-ss")));
                    //if (!Directory.Exists(Properties.Settings.Default.RecordsFolder))
                    //    Directory.CreateDirectory(Properties.Settings.Default.RecordsFolder);
                    recordFileStream_ = new FileStream(fileToSave, FileMode.Create);
                    _recordProc = new RECORDPROC(RecordProc);
                    IsRecording = (Handle = Bass.BASS_RecordStart(44100, 2, BASSFlag.BASS_RECORD_PAUSE, 50, _recordProc,
                                      IntPtr.Zero)) != 0;
                    if (IsRecording)
                        Bass.BASS_ChannelPlay(Handle, false);
                }
            }



            public static void Stop()
            {
                if (IsReady)
                {
                    if (IsRecording)
                        Bass.BASS_ChannelStop(Handle);
                    Bass.BASS_RecordFree();
                    recordFileStream_.Flush();
                    recordFileStream_.Close();
                    IsRecording = false;
                    IsReady = false;
                }
            }

            #endregion
        }
    }
