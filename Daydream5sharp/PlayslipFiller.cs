using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using ZXing.Common;

namespace Daydream5sharp
{
    public class PlayslipFiller
    {
        internal bool middayRadBtn = false;

        internal bool eveningRadBtn = false;

        internal bool bothRadBtn = false;

        internal bool ezMatchSwitch = false;

        internal byte dayAmount = 1;

        internal List<List<byte[]>> paperPlayslipLists = new List<List<byte[]>>();

        public PlayslipFiller(List<byte[]> pickBytes, string pathRoot)
        {
            paperPlayslipListsMaker(pickBytes);

            Bitmap[] bitmaps = new Bitmap[paperPlayslipLists.Count];

            DateTime dateTime = new DateTime(DateTime.Now.Ticks);

            pathRoot += "/Daydream5_" + dateTime.ToString("O").Replace(":", "_");

            if (!Directory.Exists(pathRoot))
            {
                Directory.CreateDirectory(pathRoot);
            }

            pathRoot += "\\";

            PdfDocument document = new PdfDocument();

            for (int i = 0; i < bitmaps.Length; i++)
            {

                bitmaps[i] = paperPlayslipMaker();
                bitmaps[i].RotateFlip(RotateFlipType.Rotate270FlipNone);
                document  = saveImage(bitmaps[i], pathRoot, document);
            }

            document.Save(pathRoot + dateTime.ToString("O").Replace(":", "_") + ".pdf");
        }

        protected Bitmap paperPlayslipMaker()
        {
            BitMatrix bitmapMatrix = new BitMatrix(816, 312);
            if (dayAmount == 30)
            {
                bitmapMatrix = blackSquare(bitmapMatrix, 495, 9);
            }
            else if (dayAmount == 28)
            {
                bitmapMatrix = blackSquare(bitmapMatrix, 478, 9);
            }
            else if (dayAmount == 21)
            {
                bitmapMatrix = blackSquare(bitmapMatrix, 461, 9);
            }
            else if (dayAmount == 14)
            {
                bitmapMatrix = blackSquare(bitmapMatrix, 444, 9);
            }
            else if (dayAmount == 10)
            {
                bitmapMatrix = blackSquare(bitmapMatrix, 427, 9);
            }
            else if (dayAmount == 7)
            {
                bitmapMatrix = blackSquare(bitmapMatrix, 410, 9);
            }
            else if (dayAmount == 5)
            {
                bitmapMatrix = blackSquare(bitmapMatrix, 393, 9);
            }
            else if (dayAmount == 4)
            {
                bitmapMatrix = blackSquare(bitmapMatrix, 375, 9);
            }
            else if (dayAmount == 3)
            {
                bitmapMatrix = blackSquare(bitmapMatrix, 358, 9);
            }
            else if (dayAmount == 2)
            {
                bitmapMatrix = blackSquare(bitmapMatrix, 341, 9);
            }

            if (middayRadBtn)
            {
                bitmapMatrix = blackSquare(bitmapMatrix, 100, 44);
            }
            else if (eveningRadBtn)
            {
                bitmapMatrix = blackSquare(bitmapMatrix, 100, 61);
            }
            else if (bothRadBtn)
            {
                bitmapMatrix = blackSquare(bitmapMatrix, 100, 78);
            }

            bitmapMatrix = paperPlayslipFill(bitmapMatrix);

            Bitmap bitmap = new Bitmap(816, 312, PixelFormat.Format16bppRgb565);

            for (int x = 0; x < 816; x++)
            {
                for (int y = 0; y < 312; y++)
                {

                    bitmap.SetPixel(x, y, bitmapMatrix[x, y] ? Color.Black : Color.White);

                }
            }
            return bitmap;
        }

        protected BitMatrix blackSquare(BitMatrix bitMatrix, int startX, int startY)
        {

            bitMatrix.setRegion(startX, startY, 13, 13);

            return bitMatrix;
        }

        protected void paperPlayslipListsMaker(List<byte[]> pickBytes)
        {

            int pickBytesPlayslipCount = pickBytes.Count / 10;

            for (int x = 0; x < pickBytesPlayslipCount; x++)
            {
                List<Byte[]> paperPlayslipBytes = new List<byte[]>();

                for (int y = 0; y < 10; y++)
                {
                    paperPlayslipBytes.Add(pickBytes[y]);
                }

                for (int z = 0; z < 10; z++)
                {
                    pickBytes.RemoveAt(0);
                }

                paperPlayslipLists.Add(paperPlayslipBytes);
            }
            if (pickBytes.Count % 10 != 0)
            {
                List<Byte[]> paperPlayslipBytes = new List<byte[]>();

                for (int y = 0; y < pickBytes.Count; y++)
                {
                    paperPlayslipBytes.Add(pickBytes[y]);
                }

                for (int z = 0; z < pickBytes.Count; z++)
                {
                    pickBytes.RemoveAt(0);
                }

                paperPlayslipLists.Add(paperPlayslipBytes);
            }

        }

        protected BitMatrix paperPlayslipFill(BitMatrix bitmapMatrix)
        {

            int xIndex = 0;
            int yIndex = 0;

            if (paperPlayslipLists.Count > 0)
            {
                for (byte x = 0; x < paperPlayslipLists[0].Count; x++)
                {

                    switch (x)
                    {
                        case 0:
                            xIndex = 116;
                            yIndex = 45;
                            break;
                        case 1:
                            xIndex = 236;
                            yIndex = 45;
                            break;
                        case 2:
                            xIndex = 355;
                            yIndex = 45;
                            break;
                        case 3:
                            xIndex = 475;
                            yIndex = 45;
                            break;
                        case 4:
                            xIndex = 595;
                            yIndex = 45;
                            break;
                        case 5:
                            xIndex = 116;
                            yIndex = 183;
                            break;
                        case 6:
                            xIndex = 236;
                            yIndex = 183;
                            break;
                        case 7:
                            xIndex = 356;
                            yIndex = 183;
                            break;
                        case 8:
                            xIndex = 476;
                            yIndex = 183;
                            break;
                        case 9:
                            xIndex = 596;
                            yIndex = 183;
                            break;

                    }

                    for (byte y = 0; y < paperPlayslipLists[0][x].Length; y++)
                    {

                        int yPanelIndex = yIndex;
                        switch (paperPlayslipLists[0][x][y])
                        {
                            case 1:
                            case 2:
                            case 3:
                            case 4:
                            case 5:
                            case 6:
                                break;
                            case 7:
                            case 8:
                            case 9:
                            case 10:
                            case 11:
                            case 12:
                                yPanelIndex += 17;
                                break;
                            case 13:
                            case 14:
                            case 15:
                            case 16:
                            case 17:
                            case 18:
                                yPanelIndex += 17 * 2;
                                break;
                            case 19:
                            case 20:
                            case 21:
                            case 22:
                            case 23:
                            case 24:
                                yPanelIndex += 17 * 3;
                                break;
                            case 25:
                            case 26:
                            case 27:
                            case 28:
                            case 29:
                            case 30:
                                yPanelIndex += 17 * 4;
                                break;
                            case 31:
                            case 32:
                            case 33:
                            case 34:
                            case 35:
                            case 36:
                                yPanelIndex += 17 * 5;
                                break;
                        }

                        int xPanelIndex = xIndex;
                        switch (paperPlayslipLists[0][x][y])
                        {
                            case 1:
                            case 7:
                            case 13:
                            case 19:
                            case 25:
                            case 31:
                                break;
                            case 2:
                            case 8:
                            case 14:
                            case 20:
                            case 26:
                            case 32:
                                xPanelIndex += 17;
                                break;
                            case 3:
                            case 9:
                            case 15:
                            case 21:
                            case 27:
                            case 33:
                                xPanelIndex += 17 * 2;
                                break;
                            case 4:
                            case 10:
                            case 16:
                            case 22:
                            case 28:
                            case 34:
                                xPanelIndex += 17 * 3;
                                break;
                            case 5:
                            case 11:
                            case 17:
                            case 23:
                            case 29:
                            case 35:
                                xPanelIndex += 17 * 4;
                                break;
                            case 6:
                            case 12:
                            case 18:
                            case 24:
                            case 30:
                            case 36:
                                xPanelIndex += 17 * 5;
                                break;
                        }
                        bitmapMatrix = blackSquare(bitmapMatrix, xPanelIndex, yPanelIndex);
                    }

                    if (ezMatchSwitch)
                    {
                        switch (x)
                        {
                            case 0:
                                xIndex = 220;
                                yIndex = 130;
                                break;
                            case 1:
                                xIndex = 341;
                                yIndex = 130;
                                break;
                            case 2:
                                xIndex = 461;
                                yIndex = 130;
                                break;
                            case 3:
                                xIndex = 580;
                                yIndex = 130;
                                break;
                            case 4:
                                xIndex = 700;
                                yIndex = 130;
                                break;
                            case 5:
                                xIndex = 220;
                                yIndex = 268;
                                break;
                            case 6:
                                xIndex = 340;
                                yIndex = 268;
                                break;
                            case 7:
                                xIndex = 461;
                                yIndex = 268;
                                break;
                            case 8:
                                xIndex = 580;
                                yIndex = 268;
                                break;
                            case 9:
                                xIndex = 701;
                                yIndex = 268;
                                break;

                        }

                        bitmapMatrix = blackSquare(bitmapMatrix, xIndex, yIndex);
                    }
                }

                paperPlayslipLists.RemoveAt(0);
            }

            return bitmapMatrix;
        }




        private PdfDocument saveImage(Bitmap bitmap, string pathRoot, PdfDocument document)
        {

            // Generate a unique file name
            DateTime dateTime = new DateTime(DateTime.Now.Ticks);
            pathRoot += dateTime.ToString("O").Replace(":", "_") + "_Playslip" + ".jpg";

            try
            {

                PdfPage page = document.AddPage();

                page.Width = XUnit.FromInch(3.25f);
                page.Height = XUnit.FromInch(8.5f);

                XGraphics gfx = XGraphics.FromPdfPage(page);

                bitmap.Save(pathRoot, ImageFormat.Jpeg);

                XImage image = XImage.FromFile(pathRoot);

                gfx.DrawImage(image, 0, 0);

                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }

            return document;

        }
    }
}
