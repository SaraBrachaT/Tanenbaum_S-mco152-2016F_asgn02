using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerApplication
{
    class MainClass
    {
        static Computer[] computers;
        static Computer defaultComputer;
        static Computer userComputer;

        static void Main(String[] args)
        {
            int?[] defaultLicenses = { 3, 1, 2 };
            defaultComputer = new Computer("01", false, 2560.0, defaultLicenses, 5000);
            userComputer = null;
            computers = new Computer[10];
            do
            {
                int choice = menu();
                switch (choice)
                {
                    case 1:
                        addComputer();
                        break;
                    case 2:
                        specifyPrototypeComputer();
                        break;
                    case 3:
                        computerSummary();
                        break;
                    case 4:
                        computerStatistics();
                        break;
                    case 5:
                        specificComputerStatistics();
                        break;
                    case 6:
                        Console.WriteLine("Thank you for using the computer application. Press any key to exit");
                        Console.ReadKey();
                        Environment.Exit(0);
                        break;
                }
            }
            while (true); //it will repeat until exited

        }

        public static int menu()
        {
            Console.WriteLine("Please select an option"
                + "\n1. Add a computer"
                + "\n2. Specify prototype computer"
                + "\n3. Retrieve summary of specific computer"
                + "\n4. Retrieve summary of computer statistics"
                + "\n5. Retrieve summary of specific computers (by index)"
                + "\n6. Exit");
            return int.Parse(Console.ReadLine());
        }

        public static void addComputer()
        {
            Console.WriteLine("Enter the computer specs. For software, enter the number of software that can be installed and -1 if none can be installed");
            String s = Console.ReadLine();
            string[] values = s.Split(',');
            int?[] software;
            if (int.Parse(values[3]).CompareTo(-1) == 0) software = null;
            else
            {
                software = new int?[int.Parse(values[3])];
                for (int i = 0; i < int.Parse(values[3]); i++)
                {
                    Console.WriteLine("Enter the number of licenses for number " + (i + 1) + "\nIf there is no software installed enter -1");
                    int x = int.Parse(Console.ReadLine());
                    if (x == -1) software[i] = null;
                    else software[i] = x;
                }
            }
            Computer comp = new Computer(values[0], bool.Parse(values[1]), double.Parse(values[2]), software, int.Parse(values[4]));
            for (int i = 0; i < computers.Length; i++)
            {
                //computers[i] = comp ?? null;
                if(computers[i] == null)
                {
                    computers[i] = comp;
                    break;
                }
            }
            Console.WriteLine("Computer Added");
        }

        public static void specifyPrototypeComputer()
        {
            Console.WriteLine("Enter the computer specs. For software, enter the number of software that can be installed and -1 if none can be installed");
            String s = Console.ReadLine();
            string[] values = s.Split(',');
            int?[] software;
            if (int.Parse(values[3]).CompareTo(-1) == 0) software = null;
            else
            {
                software = new int?[5];
                for (int i = 0; i < int.Parse(values[3]); i++)
                {
                    Console.WriteLine("Enter the number of licenses for software number " + (i + 1) + "\nIf there is no software installed enter -1");
                    int x = int.Parse(Console.ReadLine());
                    if (x == -1) software[i] = null;
                    else software[i] = x;
                }
            }
            userComputer = new Computer(values[0], bool.Parse(values[1]), double.Parse(values[2]), software, int.Parse(values[4]));
            Console.WriteLine("Prototype Set");
        }

        public static void computerSummary()
        {
            Console.WriteLine("Which computer (array index) would you like to view?");
            Console.WriteLine((computers[int.Parse(Console.ReadLine())] ?? defaultComputer).ToString());
        }

        public static void computerStatistics()
        {
            double? averageRAM = getAverageRam();
            double percentAntenna = getPercentAntenna();
            double? averageHDCapacity = getAverageHDCapacity();
            double? averageSoftwareLicences = getAvgSoftwareLicenses();
            double?[] averageIndivSoftware = getAvgIndivSoftware();

            StringBuilder sb = new StringBuilder();
            sb.Append("\nComputer Statistics");
            sb.Append("\nAverage RAM amount: ");
            sb.Append(averageRAM);
            sb.Append("\nPercent of computers with antenna: ");
            sb.Append(percentAntenna);
            sb.Append("\nAverage Hard Drive Capacity: ");
            sb.Append(averageHDCapacity);
            sb.Append("\nAverages Software Licenses: ");
            sb.Append(averageSoftwareLicences);
            sb.Append("\nAverage number of licenses for each software");
            for (int i = 0; i < averageIndivSoftware.Length; i++)
            {
                sb.Append("\n\tSoftware ");
                sb.Append(i + 1);
                sb.Append(": ");
                sb.Append(averageIndivSoftware[i] ?? null);
            }
            Console.WriteLine(sb.ToString());
        }

        private static double? getAverageRam()
        {
            double? totalRAM = 0;
            int counter = 0;

            foreach (Computer c in computers)
            {
                if (c != null && c.Ram != 0) //not nullable, int so if nothing valid entered it will be zero
                {
                    totalRAM += c.Ram;
                    counter++;
                }
            }
            return (totalRAM / counter);
        }

        private static double getPercentAntenna()
        {
            double countAntenna = 0;
            double countComputer = 0;
            foreach (Computer c in computers)
            {
                if (c == null) break;
                if (c.HasAntenna != null) countComputer++;
                if (c.HasAntenna == true) countAntenna++; //need the == because bool? and not bool
                
            }
            return countAntenna / countComputer * 100;
        }

        private static double? getAverageHDCapacity()
        {
            double? sumHDCap = 0;
            double countComputer = 0;
            foreach (Computer c in computers)
            {
                if (c == null) break;
                if (c.HardDriveCapacity != null)
                {
                    sumHDCap += c.HardDriveCapacity;
                    countComputer++;
                }
            }
            return sumHDCap / countComputer;
        }

        private static double? getAvgSoftwareLicenses()
        {
            int? sumLicenses = 0;
            int sumSoftware = 0;
            foreach (Computer c in computers)
            {
                if (c == null || c.Software == null) break;
                for (int i = 0; i < c.Software.Length; i++)
                {
                    if (c.Software[i] != null)
                    {
                        sumLicenses += c.Software[i];
                        sumSoftware++;

                    }
                }
            }
            return sumLicenses / sumSoftware;
        }

        private static double?[] getAvgIndivSoftware()
        {
            double?[] averages = new double?[5];
        
            for (int i = 0; i < averages.Length; i++)
            {
                double? sumLicenses = 0;
                double sumComputers = 0;
                foreach (Computer c in computers)
                {
                    if (c == null || c.Software[i] == null) break;
                    sumLicenses += c.Software[i];
                    sumComputers++;

                }
                averages[i] = sumLicenses / sumComputers;
            }

            return averages;
        }



        private static void specificComputerStatistics()
        {
            Console.WriteLine("Enter the number of computers you want the summary to include");
            int numIndeces = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the indices of the computers you want to be included in the summary separated by commas");
            string s = Console.ReadLine();
            string[] values = s.Split(',');
            int[] indeces = new int[numIndeces];
            for (int i = 0; i < values.Length; i++)
            {
                indeces[i] = int.Parse(values[i]);
            }
            //create array of these computers
            Computer[] specificComputers = new Computer[indeces.Length];
            int count = 0;
            
            foreach(int i in indeces)
            {
                Computer c = computers[i] ?? userComputer ?? defaultComputer;
                specificComputers[count++] = c;
            }

            //get average ram
            int totalRam = 0;
            foreach(Computer c in specificComputers)
            {
                totalRam += c.Ram;
            }
            double averageRam = totalRam / specificComputers.Length;

            //get percent antennas
            int antennas = 0;
            int supportAntennas = 0;

            foreach (Computer c in specificComputers)
            {
                if (c == null) break;
                if (c.HasAntenna == true) antennas++; //need the == because bool? and not bool
                if (c.HasAntenna != null) supportAntennas++;
            }

            double percentAntennas = (antennas / supportAntennas * 100);

           //get average hd cap
            double? sumHDCap = 0;
            int countComputer = 0;
            foreach (Computer c in specificComputers)
                if (c.HardDriveCapacity != null)
                {
                    sumHDCap += c.HardDriveCapacity;
                    countComputer++;
                }
            double? avgHdCap =  sumHDCap / countComputer;

            //get average num software licenses
            int? sumLicenses = 0;
            int sumSoftware = 0;
            foreach (Computer c in specificComputers)
            {
                if (c.Software != null)
                {
                    for(int j = 0; j < c.Software.Length; j++)
                    {
                        if (c.Software[j] != null)
                        {
                            sumLicenses += c.Software[j];
                            sumSoftware++;
                        }
                    }
                }

            }
            double? avgSoftwareLicenses = sumLicenses / sumSoftware;

            //get average licenses for indiv program
            double?[] averages = new double?[5];

            for (int i = 0; i < averages.Length; i++)
            {
                double? sumLicensesIndiv = 0;
                double sumComputers = 0;
                foreach (Computer c in specificComputers)
                {
                    if (c == null) break;
                    if (c.Software[i] > 0)
                    {
                        sumLicensesIndiv += c.Software[i];
                        sumComputers++;
                    }
                }
                averages[i] = sumLicensesIndiv / sumComputers;
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("\nSummary of Computers ");
            sb.Append(indeces.ToString());
            sb.Append("\nAverage RAM:");
            sb.Append(averageRam);
            sb.Append("\nPercent computers with antennas: ");
            sb.Append(percentAntennas);
            sb.Append("\nAverage hard drive capacity: ");
            sb.Append(avgHdCap);
            sb.Append("\nAverage software licenses: ");
            sb.Append(avgSoftwareLicenses);
            sb.Append("\nAverage number of licenses for each software");
            for (int i = 0; i < averages.Length; i++)
            {
                sb.Append("\n\tSoftware ");
                sb.Append(i + 1);
                sb.Append(": ");
                sb.Append(averages[i]);
            }
            Console.WriteLine(sb.ToString());
        }
    }
}
