using System;

namespace NetworkLibrary
{
    public class Network
    {
        /// <summary>
        /// 下载流量（KB）
        /// </summary>
        public float Upload { get; set; }
        /// <summary>
        /// 上传流量（KB）
        /// </summary>
        public float Download { get; set; }
        
        private Controller _controller { get; set; }
        /// <summary>
        /// 创建一个流量监听对象，并开始监听
        /// </summary>
        public Network()
        {
            _controller = new Controller(this);
            Event();
        }
        /// <summary>
        /// 停止监听网卡流量
        /// </summary>
        public void NicStop()
        {
            _controller.NicStop();
        }
        /// <summary>
        /// 开始监听网卡流量
        /// </summary>
        public void NicStart()
        {
            _controller.NicStart();
        }
        private void Event()
        {
            _controller.Error += (ss, ee) =>
             {
                 Error?.Invoke(ss, ee);
             };
            _controller.Stopped += (ss, ee) =>
            {
                Stopped?.Invoke(ss, ee);
            };
            _controller.Update += (ss, ee) =>
            {
                Update?.Invoke(ss, ee);
            };
        }

        /// <summary>
        /// 错误提示
        /// </summary>
        public event EventHandler Error;

        /// <summary>
        /// 停止监听时触发
        /// </summary>
        public event EventHandler Stopped;

        /// <summary>
        /// 流量数据改变时触发
        /// </summary>
        public event EventHandler<NicDataEventArgs> Update;
    }
}
