using System;
using System.Media;

namespace ColckWindow
{
    //http://www.cnblogs.com/yangyisen/p/5108578.html (MediaPlayer的使用)
    public class Voice
    {
        private VoiceType _type;
        private string exePath;
        private Configure _configure;
        public Uri filePath;
        SoundPlayer player = new SoundPlayer();

        public Voice(Configure configure)
        {
            exePath = System.Environment.CurrentDirectory;
            _configure = configure;
        }

        public void Play(VoiceType type)
        {
            _type = type;
            SeletePath(type);
            player.SoundLocation = filePath.OriginalString;
            player.Load();
            player.Play();
        }

        public void Stop()
        {
            player.Stop();
        }

        public void Volume(double volume)
        {

        }

        private void SeletePath(VoiceType type)
        {
            switch (_type)
            {
                case VoiceType.Click:
                    filePath =
                        new Uri(exePath + @".\package\Feedback.SAO.Click.wav",
                            UriKind.RelativeOrAbsolute);
                    break;
                case VoiceType.Message:
                    filePath = new Uri(_configure.RemindPath,
                        UriKind.RelativeOrAbsolute);
                    //  _player.Volume = _configure.RemindVolume; 
                    break;
                case VoiceType.Present:
                    filePath = new Uri(_configure.AlarmPath,
                        UriKind.RelativeOrAbsolute);
                    // _player.Volume = _configure.RemindVolume;
                    break;
            }
        }
    }

    public enum VoiceType
        {
            Click = 0,
            Message,
            Present
        }
    
}
