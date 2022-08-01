using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;

namespace nOisE
{
    class Program
    {
        static bool replace0var = false;
        static int lastViewing = 0;
        static int viewingVar = 0;

        static Dictionary<int, int> vars = new Dictionary<int, int>();
        static void Main(string[] args)
        {
            var myBitmap = new Bitmap(Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + @"\input.png");

            if (!(myBitmap.Height <= 255 && myBitmap.Width <= 255))
            {
                //Console.WriteLine("Program must be a 255x255 image or lower resolution.");
                Console.ReadLine();
                return;
            }

            for (int y = 0; y < myBitmap.Height; y++) {
                for (int x = 0; x < myBitmap.Width; x++)
                {
                    try
                    {
                        Color pixelColor = myBitmap.GetPixel(x, y);

                        //Console.WriteLine(x + " " + y +  " " + pixelColor.R);

                        switch ((int)pixelColor.R)
                        {
                            case 0:
                                //Switch viewed variable
                                if ((int)pixelColor.G >= 0 && (int)pixelColor.G <= 255)
                                {
                                    lastViewing = viewingVar;
                                    viewingVar = (int)pixelColor.G;
                                }
                                break;
                            case 17:
                                //Create Var with Val
                                int defVal = pixelColor.G;
                                if (replace0var && defVal == 0)
                                {
                                    defVal = vars[viewingVar];
                                }
                                vars.Add(viewingVar, defVal);
                                break;
                            case 34:
                                // Set variable to value
                                int defValue = pixelColor.G;
                                if (replace0var && defValue == 0)
                                {
                                    defValue = vars[viewingVar];
                                }
                                vars[viewingVar] = defValue;
                                break;
                            case 51:
                                // Set variable to addition value
                                int add1 = pixelColor.G;
                                if (replace0var && add1 == 0)
                                {
                                    add1 = vars[viewingVar];
                                }
                                int add2 = pixelColor.B;
                                if (replace0var && add2 == 0)
                                {
                                    add2 = vars[viewingVar];
                                }

                                vars[viewingVar] = add1 + add2;

                                break;
                            case 68:
                                // Set variable to subtraction value
                                int sub1 = pixelColor.G;
                                if (replace0var && sub1 == 0)
                                {
                                    sub1 = vars[viewingVar];
                                }
                                int sub2 = pixelColor.B;
                                if (replace0var && sub2 == 0)
                                {
                                    sub2 = vars[viewingVar];
                                }

                                vars[viewingVar] = sub1 - sub2;

                                break;
                            case 85:
                                // Add the value of the last viewed var to the viewed var
                                vars[viewingVar] = vars[viewingVar] + vars[lastViewing];

                                break;
                            case 102:
                                // Set var value to ascii input value
                                vars[viewingVar] = (int)Console.ReadKey().KeyChar;

                                break;
                            case 119:
                                // Goto image location
                                int xDest = pixelColor.G;
                                if (replace0var && xDest == 0)
                                {
                                    xDest = vars[viewingVar];
                                }
                                int yDest = pixelColor.B;
                                if (replace0var && yDest == 0)
                                {
                                    yDest = vars[viewingVar];
                                }

                                x = xDest - 1;
                                y = yDest;
                                break;
                            case 136:
                                Console.Write("debug");
                                break;
                            case 153:
                                Console.Write("\n");
                                break;
                            case 170:
                                if (vars[viewingVar] != pixelColor.G)
                                {
                                    x += pixelColor.B;
                                }
                                break;
                            case 187:
                                if (vars[viewingVar] == pixelColor.G)
                                {
                                    x += pixelColor.B;
                                }
                                break;
                            case 69:
                                break;
                            case 221:
                                int asciiChar = pixelColor.G;
                                if (replace0var && asciiChar == 0)
                                {
                                    asciiChar = vars[viewingVar];
                                }
                                Console.Write(Encoding.ASCII.GetString(new byte[] { (byte)asciiChar }));
                                break;
                            case 238:
                                Console.Clear();
                                break;
                            case 255:
                                if (pixelColor.G == 0)
                                {
                                    replace0var = false;
                                }
                                else
                                {
                                    replace0var = true;
                                }
                                break;
                        }
                    } catch (Exception e)
                    {
                        Console.WriteLine("bro we caught an error in your code, i really didnt care enough to program this to detect the error but its pixel x: " + x + " and y: " + y);
                    }
                    
                }
            }
            Console.WriteLine("\n\nProgram ended.");
            while (true) { }
        }
    }
}
