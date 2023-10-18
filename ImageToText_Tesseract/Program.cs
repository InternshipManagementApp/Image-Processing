using IronOcr;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageToText_Tesseract
{
    internal class Program
    {
        public FileInfo[] getImages(string path) //get all images from a directory
        {
            DirectoryInfo directory = new DirectoryInfo(path);
            FileInfo[] result = directory.GetFiles("*.*");
            return result;
        }
        static public void Main(string[] args)
        {

            Program p = new Program();
            FileInfo[] images = p.getImages("../../ImagesClock");
            string filePath = "../../result.txt";
            if (File.Exists(filePath)) // Check if file exists
            {
                // Delete the file
                File.Delete(filePath);
            }

            foreach (FileInfo image in images)
            {
                var Ocr = new IronTesseract(); // nothing to configure
                //Ocr.Language = OcrLanguage.English;
                Ocr.Language = OcrLanguage.Financial; //it will read only the specified language  - numbers, decimal
                Ocr.Configuration.PageSegmentationMode = TesseractPageSegmentationMode.SingleChar;
                Ocr.Configuration.WhiteListCharacters = "01234567890";
                using (var Input = new OcrInput()) //object to the image
                {
                
                    Input.AddImage(image.FullName); //add images to the input with a route
                    Input.Binarize(); //color picture to binary
                    //Input.ToGrayScale(); // color picture to grayscale
                    var Result = Ocr.Read(Input);  //read from the image the characters

                     File.AppendAllText(filePath, image.Name + " " + Result.Text +  Environment.NewLine);
                
                
                }
            }

        }
    }
}
