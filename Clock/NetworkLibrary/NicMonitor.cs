using System;
using System.Diagnostics;

namespace NetworkLibrary
{
    public class NicDataEventArgs : EventArgs
    {
        #region Constructors

        public NicDataEventArgs(float recv, float sent)
        {
            Recv = recv;
            Sent = sent;
        }

        #endregion Constructors

        #region Properties

        public float Recv
        {
            get; private set;
        }

        public float Sent
        {
            get; private set;
        }

        #endregion Properties
    }

    class NicMonitor
    {
        #region Fields

        const string Network = "Network Interface";
        const string Recv = "Bytes Received/sec";
        const string Sent = "Bytes Sent/sec";

        static PerformanceCounterCategory category;

        volatile bool cancellationReq;
        string instance { get; set; }

        #endregion Fields

        #region Constructors

        public NicMonitor()
        {
          //  instance = ins;
        }

        #endregion Constructors

        #region Events

        public event EventHandler Error;

        public event EventHandler Stopped;

        public event EventHandler<NicDataEventArgs> Update;

        #endregion Events

        #region Properties

        public Exception Exception
        {
            get; private set;
        }

        #endregion Properties

        #region Methods

        public static string[] GetNicNames()
        {
            if (!PerformanceCounterCategory.Exists(Network)
                || !PerformanceCounterCategory.CounterExists(Recv, Network)
                || !PerformanceCounterCategory.CounterExists(Sent, Network))
            {
                throw new Exception("系统找不到相应的监视计数器");
            }
            category = new PerformanceCounterCategory(Network);

            return category.GetInstanceNames();
        }

        public void Cancel()
        {
            cancellationReq = true;
        }

        public void Start(string ins)
        {
            try
            {
                cancellationReq = false;
                //建立两个PerformanceCounter来分别对应接收和发送监控
                using (var crecv = new PerformanceCounter(Network, Recv, ins))
                using (var csent = new PerformanceCounter(Network, Sent, ins))
                {
                    while (!cancellationReq)
                    {
                        //得到下一个值
                        var valrecv = crecv.NextValue();
                        var valsent = csent.NextValue();

                        //发送事件数据
                        OnUpdate(new NicDataEventArgs(valrecv, valsent));
                        //等待1秒
                        System.Threading.Thread.Sleep(1000);
                    }
                }
            }
            catch (Exception ex)
            {
                Exception = ex;
                Error?.Invoke(this, EventArgs.Empty);
            }
            finally
            {
                OnStopped(EventArgs.Empty);
            }
        }

        protected virtual void OnStopped(EventArgs e)
        {
            var handler = this.Stopped;
            if (handler != null)
                handler(this, e);
        }

        protected virtual void OnUpdate(NicDataEventArgs args)
        {
            if (Update != null)
                Update(this, args);
        }

        #endregion Methods
    }
}
