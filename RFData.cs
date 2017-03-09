using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZumConsole
{
    public class RFData
    {
        private int Channel;
        private double Average;
        private double Peak;
        public RFData(int chan, double aver, double pk)
        {
            Channel = chan;
            Average = aver;
            Peak = pk;
        }
        public void SetChannel(int chan)
        {
            Channel = chan;
        }
        public void SetAverage(double aver)
        {
            Average = aver;
        }
        public void SetPeak(double pk)
        {
            Peak = pk;
        }
        public int GetChannel()
        {
            return Channel;
        }
        public double GetAverage()
        {
            return Average;
        }
        public double GetPeak()
        {
            return Peak;
        }
    }
}
