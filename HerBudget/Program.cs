using System.Collections;
using System.Text.RegularExpressions;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;

namespace HerBudget
{
    internal class Program
    {
        static void Main(string[] args)
        {

            using (PdfDocument document = PdfDocument.Open("D:/afterGrad/c#/Adelisa/HerBudget/NovDec.pdf"))
            {
                string pageText = "";
                string pattern = "(?:\\n((?:0[1-9]|1[1,2])/(?:0[1-9]|[12][0-9]|3[01]))\\s*(.+) ((?:-\\d+\\.\\d{2})|(?:\\d+\\.\\d{2})))";
                string path = "D:/afterGrad/c#/Adelisa/HerBudget/NovDec.txt";
                Page page = document.GetPage(3);
                
                //foreach (Page page in document.GetPages())
                //{
                //    foreach(Word word in page.GetWords())
                //    {
                //        pageText += word;
                //    }

                //}

                foreach(Word word in page.GetWords())
                {
                    pageText += word.ToString();
                }

                File.WriteAllText(path, pageText);
            }

                WebStarter ws = new WebStarter(args);
            ws.ShowWeb();
        }
    }
}