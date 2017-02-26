using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerApplication
{
    class Computer
    {
        readonly string id;
        bool? hasAntenna;
        public bool? HasAntenna {
            get { return hasAntenna; }
            set { hasAntenna = value; }
        }
        double? hardDriveCapacity;
        public double? HardDriveCapacity
        {
            get{ return hardDriveCapacity; }
            set{ if(value >= 0) hardDriveCapacity = value;}
        }
        int?[] software;
        public int?[] Software
        {
            get { return software; }
        }
    
        int ram;
        public int Ram
        {
           get
            {
                int reportedRam = ram;
                if (hasAntenna.Equals(true)) reportedRam -= 100;
                else reportedRam -= 50;
                for (int i = 0; i < software.Length; i++)
                {
                    if (software[i] > 0) reportedRam -= 10;
                }
            
                return reportedRam;
            }
            set { if(ram >= 1000)ram = value;}
        }

        public Computer(string id, bool? hasAntenna, double hardDriveCap, int?[] soft, int ram)
        { 
            this.id = id;
            this.hasAntenna = hasAntenna;
            this.hardDriveCapacity = hardDriveCap;
            software = new int?[5];
            this.ram = ram;
            for (int i = 0; i < soft.Length; i++)
            {
                if(soft[i] != null) software[i] = soft[i];
            }
            
        }

        
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("\nComputer ID: ");
            sb.Append(id);
            sb.Append("\nHas Antenna: ");
            sb.Append(hasAntenna);
            sb.Append("\nHard Drive Capacity: ");
            sb.Append(hardDriveCapacity);
            for(int i = 0; i < software.Length; i++)
            {
                sb.Append("\nSoftware #" + (i + 1) + ": ");
                sb.Append(software[i].ToString() + " licenses" ?? "This software is not supported");
            }
            sb.Append("\nRam Amount: ");
            sb.Append(ram);

            return sb.ToString();
        }
    }
}
