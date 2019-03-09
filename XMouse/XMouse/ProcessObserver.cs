using System;
using System.Diagnostics;
using System.IO;
using System.Xml.Serialization;

namespace XMouse
{
    public class ProcessObserver
    {
        protected XMouseConfig config;

        public ProcessObserver()
        {
            LoadParams();
        }

        protected void LoadParams()
        {
            try
            {
                var serializer = new XmlSerializer(typeof(XMouseConfig));
                using (var reader = new FileStream("XMouse.Config.xml", FileMode.Open))
                {
                    config = (XMouseConfig)serializer.Deserialize(reader);
                }
            }
            catch (Exception) { }
        }

        public bool ControlShouldBeStopped()
        {
            try
            {
                var allProcesses = Process.GetProcesses();

                foreach (var proc in allProcesses)
                {
                    foreach (var cfgProc in config.Processes.Proc)
                    {
                        if (proc.ProcessName.Equals(cfgProc)) return true;
                    }
                }

                return false;
            }
            catch (Exception) { return true; }
        }
    }
}
