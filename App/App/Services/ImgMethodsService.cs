using Microsoft.AspNetCore.Http;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace App.Services
{
    public class ImgMethodsService
    {
        //COMPARER IFORM WITH BYTE[]
        public static int CompareIFormFileImgWithByteArray(IFormFile form, byte[] ImgByte) 
        {
            if (form != null && form.Length > 0)
            {
                var cont = form.ContentType;
                if (cont == "image/png" || cont == "image/bmp" || cont == "image/jpg")
                {
                    byte[] newArray = ImageIFormForBytetArray(form);
                    return CompareByteArray(ImgByte,newArray);
                }
                else
                {
                    return 0;
                }
            }
            return 0;
        }

        //TRANSFORM IFORM IN BYTE[]
        public static byte[] ImageIFormForBytetArray(IFormFile ImgFile)
        {
            byte[] result = null;
            using (var memoryStream = new MemoryStream()) { 
                ImgFile.CopyTo(memoryStream);
                using(var img = Image.FromStream(memoryStream))
                {
                    var imgCorrectSize = CorrectSizeForStorege(img,40,40);
                    using (var segundMemoryStream = new MemoryStream())
                    {
                        imgCorrectSize.Save(segundMemoryStream,ImageFormat.Bmp);
                        result = segundMemoryStream.ToArray();
                    }
                }
            }
            return result;
        }

        //RESIZE IMAGE
        public static Image CorrectSizeForStorege(Image img, int x, int y)
        {
            return (Image)(new Bitmap(img, new Size(x, y)));
        }

        //COMPARE IMAGES
        public static int CompareImg(Image img1, Image img2)
        {
            List<bool> iHash1 = GetHash(new Bitmap(img1));
            List<bool> iHash2 = GetHash(new Bitmap(img2));

            int equalElements = iHash1.Zip(iHash2, (i, j) => i == j).Count(eq => eq);
            return equalElements;
        }

        private static int CompareByteArray(byte[] arrayOne, byte[] arrayTwo)
        {
            int equalElements = arrayOne.Zip(arrayTwo, (i, j) => i == j).Count(eq => eq);
            int percent = (equalElements * 100) / arrayOne.Length;
            //THIS FUNCTION RETURNS "PERCENT" HOW MANY NUMBER ARE LIKELY
            return percent;
        }

        //GET HASH OF IMAGES
        private static List<bool> GetHash(Bitmap bmpSource)
        {
            List<bool> lResult = new List<bool>();
            //create new image with 16x16 pixel
            Bitmap bmpMin = new Bitmap(bmpSource, new Size(25, 25));
            for (int j = 0; j < bmpMin.Height; j++)
            {
                for (int i = 0; i < bmpMin.Width; i++)
                {
                    //reduce colors to true / false                
                    lResult.Add(bmpMin.GetPixel(i, j).GetBrightness() < 0.5f);
                }
            }
            return lResult;
        }

        
    }
}
