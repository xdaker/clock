using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace NetworkLibrary
{
    class Controller
    {
        private Network _network;
        private List<NicMonitor> _listNicMonitor;
        private List<string> _nicNames;
        private NicMonitor _effective { get; set; }
        public Controller(Network network)
        {
            _network = network;
            _listNicMonitor = new List<NicMonitor>();
            Thread receiveThread = new Thread(Start);
            receiveThread.IsBackground = true;
            receiveThread.Start();
        }
        public void NicStop()
        {
            _effective.Cancel();
        }
        public void NicStart()
        {
            NicStop();
            Thread receiveThread = new Thread(Start);
            receiveThread.IsBackground = true;
            receiveThread.Start();
        }
        private void Start()
        {
            try
            {
                _nicNames = NicMonitor.GetNicNames().ToList();
                if (_nicNames.Count > 0)
                {
                    foreach (var list in _nicNames)
                    {
                        _listNicMonitor.Add(new NicMonitor());
                    }
                    int i = 0;
                    foreach (var list in _listNicMonitor)
                        Test(list, _nicNames[i++]);
                }
            }
            catch {
                Error?.Invoke(this, EventArgs.Empty);
            }
        }

        private void Event(NicMonitor nicMonitor)
        {
            nicMonitor.Error += (ss, ee) =>
            {
                Error?.Invoke(ss, ee);
            };
            nicMonitor.Stopped += (ss, ee) =>
            {
                Stopped?.Invoke(ss, ee);
            };
            nicMonitor.Update += (ss, ee) =>
            {
                _network.Download = ee.Sent /1024;
                _network.Upload = ee.Recv /1024;
                Update?.Invoke(ss, ee);
            };
        }
        private void Test(NicMonitor nic,string NicNames)
        {
            Thread receiveThread = new Thread(()=>
            {
                nic.Update += (ss, ee) =>
                {
                    if (ee.Recv != 0 || ee.Sent != 0)
                    {
                        foreach (var list in _listNicMonitor)
                        {
                            if (!list.Equals(nic))
                            {
                                list.Cancel();
                            }
                        }
                        Event(nic);
                        _effective = nic;
                        _listNicMonitor.Clear();
                    }
                };
                nic.Start(NicNames);
            });
            receiveThread.IsBackground = true;
            receiveThread.Start();

        }
        public event EventHandler Error;

        public event EventHandler Stopped;

        public event EventHandler<NicDataEventArgs> Update;
    }
}
