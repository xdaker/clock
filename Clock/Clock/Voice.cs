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

        public Uri filePath;
        public TimeSpan Position => _player.Position;
        public Voice(VoiceType type)
        {
            _type = type;
            exePath = System.Environment.CurrentDirectory;
            switch (_type)
            {
                case VoiceType.Click:
                    filePath = new Uri(exePath + @"\package\Feedback.SAO.Click.wav", UriKind.RelativeOrAbsolute);
                    break;
                case VoiceType.Message:
                    filePath = new Uri(exePath + @"\package\Notify.SAO.Message.wav", UriKind.RelativeOrAbsolute);
                    break;
                case VoiceType.Present:
                    filePath = new Uri(exePath + @"\package\Notify.SAO.Present.wav", UriKind.RelativeOrAbsolute);
                    break;
            }
            
        }
        public void Play()
        {
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
    }
   public enum VoiceType
    {
        Click =0,
        Message,
        Present
    }
}
