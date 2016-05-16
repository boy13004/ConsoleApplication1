using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MegaEscritorio
{
    class Program
    {

        /**************************************************************************
         * This method prompts the user for the width of the desk.
         * ***********************************************************************/
        static int promptDeskWidth()
        {
            int width = 0;
            string widthString;

            do
            {
                Console.WriteLine("Please enter the width of the desk in inches: ");
                widthString = Console.ReadLine();
                try
                {
                    width = int.Parse(widthString);

                    if (width <= 0)
                        throw new Exception("Width cannot be less than 1 inch.\n");
                }
                catch (Exception eDeskW)
                {
                    Console.Write(eDeskW.Message);
                }
            }
            while (width <= 0);

            return width;
            
        }

        /**************************************************************************
         * This method prompts the user for the length of the desk.
         * ***********************************************************************/
        static int promptDeskLength()
        {
            int length = 0;
            string lengthString;
            do
            {
                Console.WriteLine("\nPlease enter the length of the desk in inches: ");
                lengthString = Console.ReadLine();
                try
                {
                    length = int.Parse(lengthString);

                    if (length <= 0)
                        throw new Exception("Length cannot be less than 1 inch.\n");
                }
                catch (Exception eDeskL)
                {
                    Console.Write(eDeskL.Message);
                }
            }
            while (length <= 0);

            return length;
        }
        /**************************************************************************
         * This method prompts the user for the number of drawers
         * ***********************************************************************/
        static int promptDrawers()
        {
            string drawerString;
            int drawers = 0;

            do
            {
                Console.WriteLine("\nPlease enter the number of drawers between 0-7: ");
                drawerString = Console.ReadLine();
                try
                {
                    drawers = int.Parse(drawerString);

                    if (drawers > 7 || drawers < 0)
                        throw new Exception("Drawers must be a number 0 - 7.\n");
                }
                catch (Exception eDrawer)
                {
                    Console.WriteLine(eDrawer.Message);
                }

            }

            while (drawers > 7 || drawers < 0);
            return drawers;
            
        }

        /**************************************************************************
         * This method prompts the user for the type of material.
         * ***********************************************************************/
        static string promptMaterial()
        {
            string mat = "";

            do
            {
                Console.WriteLine("\nPlease enter the type of material (Laminate, Oak or Pine): ");
                try
                {
                    mat = Console.ReadLine();

                    if (mat != "Laminate" && mat != "Oak" && mat != "Pine")
                        throw new Exception("Material must be Laminate, Oak or Pine.\n");
                }
                catch (Exception eMat)
                {
                    Console.WriteLine(eMat.Message);
                }
            }
            while (mat != "Laminate" && mat != "Oak" && mat != "Pine");

            return mat;
        }

        /**************************************************************************
         * This method reads in the prices for the rush orders
         * ***********************************************************************/
        static void readFile(int[] rushPrices)
        {
            StreamReader reader = new StreamReader(@"C:\Users\sligy\Desktop\BYU-I\Spring 2016\CIT 301C\rushOrderPrices.txt");

            for (int i = 0; i < 9; i++)
            {
                string priceString = reader.ReadLine();
                rushPrices[i] = int.Parse(priceString);
            }
            reader.Close();
        }

        /**************************************************************************
         * This method asks the user if they want a rush order
         * ***********************************************************************/
        static int promptRush()
        {
            int time = 0;

            do
            {
                Console.WriteLine("\nIf applicable, please enter a rush order time of 3, 5, or 7 days. \nOtherwise please enter the normal production time of 14 days.");
                string rushStream = Console.ReadLine();
                try
                {
                    time = int.Parse(rushStream);

                    if (time != 3 && time != 5 && time != 7 && time != 14)
                        throw new Exception("Please enter either 3, 5, 7 or 14\n");
                }
                catch (Exception eRush)
                {
                    Console.WriteLine(eRush.Message);
                }
            }
            while (time != 3 && time != 5 && time != 7 && time != 14);

            return time;
        }

        /**************************************************************************
         * This method calculates the prices and total price of the order.
         * ***********************************************************************/
        static int calcPrice(int width, int length, int drawers, string mat, int time, ref int surfacePrice, ref int drawerPrice, ref int matPrice, ref int rushPrice, int [] rushPrices)
        {
            surfacePrice = (((length * width) - 1000) * 5);
            if (surfacePrice < 0)
                surfacePrice = 0;

            double deskSize = (width * length); 

            drawerPrice = (drawers * 50);

            switch (mat)
            {
                case "Oak":
                    matPrice = 200;
                    break;
                case "Laminate":
                    matPrice = 100;
                    break;
                case "Pine":
                    matPrice = 50;
                    break;
            }

            switch (time)
            {
                case 3:
                    if (deskSize < 1000)
                        rushPrice = rushPrices[0];
                    else if (deskSize > 1000 && deskSize < 2000)
                        rushPrice = rushPrices[1];
                    else if (deskSize >= 2000)
                        rushPrice = rushPrices[2];
                    break;
                case 5:
                    if (deskSize < 1000)
                        rushPrice = rushPrices[3];
                    else if (deskSize > 1000 && deskSize < 2000)
                        rushPrice = rushPrices[4];
                    else if (deskSize >= 2000)
                        rushPrice = rushPrices[5];
                    break;
                case 7:
                    if (deskSize < 1000)
                        rushPrice = rushPrices[6];
                    else if (deskSize > 1000 && deskSize < 2000)
                        rushPrice = rushPrices[7];
                    else if (deskSize >= 2000)
                        rushPrice = rushPrices[8];
                    break;
                default:
                    rushPrice = 0;
                    break;
                
            }

            int totalPrice = (200 + surfacePrice + drawerPrice + matPrice + rushPrice);

            return totalPrice;

        }

        /**************************************************************************
         * This method displays the complete order to the screen.
         * ***********************************************************************/
        static void displayOrder(int width, int length, int drawers, string mat, int time, int surfacePrice, int drawerPrice, int matPrice, int rushPrice, int totalPrice)
        {
            Console.WriteLine("\nYour Order is as follows: ");
            Console.WriteLine("Width: " + width + " in");
            Console.WriteLine("Length: " + length + " in");
            Console.WriteLine("Oversize Surface Price: $" + surfacePrice + "\n");

            Console.WriteLine("Number of Drawers: " + drawers);
            Console.WriteLine("  Drawer Price: $" + drawerPrice + "\n");

            Console.WriteLine("Material: " + mat);
            Console.WriteLine("  Material Price: $" + matPrice + "\n");

            Console.WriteLine("Order Time: " + time + " days");
            Console.WriteLine("  Rush Price: $" + rushPrice + "\n");

            Console.WriteLine("Total Price: $" + totalPrice);

        }

        /**************************************************************************
         * This method will write the order to a file
         * ***********************************************************************/
         static void writeFile(int width, int length, int surfacePrice, int drawers, int drawerPrice, string mat, int matPrice, int time, int rushPrice, int totalPrice)
        {
            StreamWriter writer;
            writer = new StreamWriter(@"C:\Users\sligy\Desktop\BYU-I\Spring 2016\orderReference.txt");

            writer.WriteLine("{");
            writer.WriteLine("\t\"order\":");
            writer.WriteLine("\t{");
            writer.WriteLine("\t\t\"width\":\"" + width + " in\"");
            writer.WriteLine("\t\t\"length\":\"" + length + " in\"");
            writer.WriteLine("\t\t\"oversizeSurfacePrice\":\"$" + surfacePrice + "\"\n");

            writer.WriteLine("\t\t\"numberOfDrawers\":\"" + drawers + "\"");
            writer.WriteLine("\t\t\"drawerPrice\":\"$" + drawerPrice + "\"\n");

            writer.WriteLine("\t\t\"material\":\"" + mat + "\"");
            writer.WriteLine("\t\t\"materialPrice\":\"$" + matPrice + "\"\n");

            writer.WriteLine("\t\t\"orderTime\":" + time + "days\"");
            writer.WriteLine("\t\t\"rushPrice\":\"$" + rushPrice + "\"\n");

            writer.WriteLine("\t\t\"totalPrice\":\"$" + totalPrice + "\"");
            writer.WriteLine("\t}");
            writer.WriteLine("}");
            writer.Close();
        }

        /**************************************************************************
         * This is the Main method of the program.
         * ***********************************************************************/
        static void Main(string[] args)
        {
            int[] rushPrices = new int[9];
            readFile(rushPrices);

            int width = promptDeskWidth();
            int length = promptDeskLength();
            int drawers = promptDrawers();
            string mat = promptMaterial();
            int time = promptRush();

            int surfacePrice = 0;
            int drawerPrice = 0;
            int matPrice = 0;
            int rushPrice = 0;

            int totalPrice = calcPrice( width, length, drawers, mat, time, ref surfacePrice, ref drawerPrice, ref matPrice, ref rushPrice, rushPrices);

            displayOrder(width, length, drawers, mat, time, surfacePrice, drawerPrice, matPrice, rushPrice, totalPrice);

            writeFile(width, length, surfacePrice, drawers, drawerPrice, mat, matPrice, time, rushPrice, totalPrice);

        }
    }
}
