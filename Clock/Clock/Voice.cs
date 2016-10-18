using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace ColckWindow
{
    //http://www.cnblogs.com/yangyisen/p/5108578.html (MediaPlayer的使用)
    public class Voice
    {
        private VoiceType _type;
        private MediaPlayer _player = new MediaPlayer();
        private string exePath;
        private Configure _configure;
        public Uri filePath;
        public TimeSpan Position => _player.Position;
        public Voice(Configure configure)
        {
            exePath = System.Environment.CurrentDirectory;
            _configure = configure;
        }
        public void Play(VoiceType type)
        {
            _type = type;
            SeletePath(type);
            _player.Open(filePath);
            _player.Play();
        }
        public void Pause()
        {
            _player.Pause();
        }
        public void Stop()
        {
            _player.Stop();
        }
        public void Close()
        {
            _player.Close();
        }
        public void Volume(double volume)
        {
            _player.Volume = volume;
        }

        private void SeletePath(VoiceType type)
        {
            switch (_type)
            {
                case VoiceType.Click:
                    filePath = new Uri(exePath + @"\package\Feedback.SAO.Click.wav", UriKind.RelativeOrAbsolute);
                    break;
                case VoiceType.Message:
                    filePath = new Uri(_configure.RemindPath, UriKind.RelativeOrAbsolute);
                    _player.Volume = _configure.RemindVolume; 
                    break;
                case VoiceType.Present:
                    filePath = new Uri(_configure.AlarmPath, UriKind.RelativeOrAbsolute);
                    _player.Volume = _configure.RemindVolume;
                    break;
            }
        }
    }
   public enum VoiceType
    {
        Click =0,
        Message,
        Present
    }
}
