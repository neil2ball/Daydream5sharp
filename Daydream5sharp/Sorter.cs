using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daydream5sharp
{


    public class Sorter
    {
        internal byte[] yellow  = { 1, 3, 5, 7, 9, 11, 13, 15, 17 };
        internal byte[] blue    = { 2, 4, 6, 8, 10, 12, 14, 16, 18 };
        internal byte[] grey    = { 19, 21, 23, 25, 27, 29, 31, 33, 35 };
        internal byte[] green   = { 20, 22, 24, 26, 28, 30, 32, 34, 36 };

        internal List<byte[]> colors = new List<byte[]>();

        public List<byte[]> picks = new List<byte[]>();

        public List<string> pickStrings = new List<string>();

        public Sorter()
        {
            CreatePicks();
        }

        internal void LoadColors()
        {
            Random rng = new Random();
            
            rng.Shuffle<byte>(yellow);
            colors.Add(yellow);

            rng.Shuffle<byte>(blue);
            colors.Add(blue);

            rng.Shuffle<byte>(grey);
            colors.Add(grey);

            rng.Shuffle<byte>(green);
            colors.Add(green);
        }

        internal byte[] ShiftBytes(byte[] bytes)
        {
            byte byt = bytes[0];

            for (byte a = 1; a < bytes.Length; a++)
            {
                bytes[a - 1] = bytes[a];
            }

            bytes[bytes.Length - 1] = byt;

            return bytes;
        }

        internal void SortList()
        {
            //This seems like the easiest way to sort the picks. I turn them into strings, tack 0 in front of those less than ten,
            //sort the list of strings, then change them back into byte arrays.

            foreach (byte[] a in picks)
            {
                string stringPick = "";
                for (byte b = 0; b < a.Length; b++)
                {
                    if (a[b] < 10)
                    {
                        stringPick += "0" + a[b].ToString();
                    }
                    else
                    {
                        stringPick += a[b].ToString();
                    }

                    if (b < a.Length - 1)
                    {
                        stringPick += ",";
                    }
                }

                pickStrings.Add(stringPick);
            }

            picks.Clear();

            pickStrings.Sort();

            foreach (string stringPick in pickStrings)
            {
                string[] strings = stringPick.Split(",");

                byte[] stringBytes = new byte[strings.Length];

                for (byte a = 0; a < stringBytes.Length; a++)
                {
                    stringBytes[a] = Convert.ToByte(strings[a]);
                }

                picks.Add(stringBytes);
            }
        }

        internal void CreatePicks()
        {
            LoadColors();

            for (byte a = 0; a < colors.Count; a++)
            {
                //this only works if the amount of colors are equal. FIX ME
                for (byte b = 0; b < colors[colors.Count - 1].Length; b++)
                {
                    //Different bytes fixed the problem of loading copies of the same pick despite assigning new values.
                    byte[] pick0 = new byte[5];

                    pick0[0] = colors[0].ElementAt(0);
                    pick0[1] = colors[1].ElementAt(0);
                    pick0[2] = colors[2].ElementAt(0);
                    pick0[3] = colors[3].ElementAt(0);
                    pick0[4] = colors[0].ElementAt(1);

                    Array.Sort(pick0);               
                    
                    picks.Add(pick0);

                    byte[] pick1 = new byte[5];

                    pick1[0] = colors[1].ElementAt(1);
                    pick1[1] = colors[2].ElementAt(1);
                    pick1[2] = colors[3].ElementAt(1);
                    pick1[3] = colors[0].ElementAt(2);
                    pick1[4] = colors[1].ElementAt(2);

                    Array.Sort(pick1);
                    
                    picks.Add(pick1);

                    byte[] pick2 = new byte[5];

                    pick2[0] = colors[2].ElementAt(2);
                    pick2[1] = colors[3].ElementAt(2);
                    pick2[2] = colors[0].ElementAt(3);
                    pick2[3] = colors[1].ElementAt(3);
                    pick2[4] = colors[2].ElementAt(3);

                    Array.Sort(pick2);
                    
                    picks.Add(pick2);


                    byte[] pick3 = new byte[5];

                    pick3[0] = colors[3].ElementAt(3);
                    pick3[1] = colors[0].ElementAt(4);
                    pick3[2] = colors[1].ElementAt(4);
                    pick3[3] = colors[2].ElementAt(4);
                    pick3[4] = colors[3].ElementAt(4);

                    Array.Sort(pick3);
                    
                    picks.Add(pick3);

                    byte[] pick4 = new byte[5];

                    pick4[0] = colors[0].ElementAt(5);
                    pick4[1] = colors[1].ElementAt(5);
                    pick4[2] = colors[2].ElementAt(5);
                    pick4[3] = colors[3].ElementAt(5);
                    pick4[4] = colors[3].ElementAt(6);

                    Array.Sort(pick4);
                    
                    picks.Add(pick4);

                    byte[] pick5 = new byte[5];

                    pick5[0] = colors[0].ElementAt(6);
                    pick5[1] = colors[1].ElementAt(6);
                    pick5[2] = colors[2].ElementAt(6);
                    pick5[3] = colors[2].ElementAt(7);
                    pick5[4] = colors[3].ElementAt(7);

                    Array.Sort(pick5);
                    
                    picks.Add(pick5);

                    byte[] pick6 = new byte[5];

                    pick6[0] = colors[0].ElementAt(7);
                    pick6[1] = colors[1].ElementAt(7);
                    pick6[2] = colors[0].ElementAt(8);
                    pick6[3] = colors[1].ElementAt(8);
                    pick6[4] = colors[2].ElementAt(8);

                    Array.Sort(pick6);
                    
                    picks.Add(pick6);

                    //This wheels the numbers in each "color" by n + 1 depending on its location in the list. 
                    byte counter = 1;
                    for (byte c = 0; c < colors.Count; c++)
                    {

                      for (byte d = 0; d < counter; d++)
                      {
                        colors[c] = ShiftBytes(colors[c]);
                      }
                       counter++;                        
                    }
                }

                byte[] color = colors[0];

                colors.Remove(color);

                colors.Add(color);

            }

            SortList();
        }
    }
}
