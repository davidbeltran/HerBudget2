using System.Collections;
using System.Text.RegularExpressions;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;
using UglyToad.PdfPig.DocumentLayoutAnalysis.TextExtractor;

namespace HerBudget
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string pageText = "";
            string pattern = "(?:\\n((?:0[1-9]|1[1,2])/(?:0[1-9]|[12][0-9]|3[01]))\\s*(.+) ((?:-\\d+\\.\\d{2})|(?:\\d+\\.\\d{2})))";
            string pathTxt = "D:/afterGrad/c#/Adelisa/HerBudget/NovDec.txt";
            string pathPdf = "D:/afterGrad/c#/Adelisa/HerBudget/NovDec.pdf";
            ArrayList expList = new ArrayList();

            using (PdfDocument document = PdfDocument.Open(pathPdf))
            {

                foreach(Page page in document.GetPages())
                {
                    var text = ContentOrderTextExtractor.GetText(page);
                    pageText += text;
                }
                //File.WriteAllText(pathTxt, pageText);
            }

            MatchCollection matches = Regex.Matches(pageText, pattern);
            foreach (Match match in matches) 
            {
                var temp = new ArrayList();
                temp.Add(match.Groups[1].Value);
                temp.Add(match.Groups[2].Value);
                temp.Add(double.Parse(match.Groups[3].Value));
                expList.Add(temp);
            }

            foreach (ArrayList expense in expList)
            {
                Console.WriteLine("Date: {0}, Detail: {1}, Amount:{2}", expense[0], expense[1], expense[2]);
            }

            WebStarter ws = new WebStarter(args);
            ws.ShowWeb();
        }
    }
}