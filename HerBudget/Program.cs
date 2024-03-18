using UglyToad.PdfPig;

namespace HerBudget
{
    internal class Program
    {
        static void Main(string[] args)
        {

            using (PdfDocument document = PdfDocument.Open("NovDec.pdf"))
            {
                foreach (Page page in document.GetPages())
                {
                    string pageText = page.Text;
                }
            }

                WebStarter ws = new WebStarter(args);
            ws.ShowWeb();
        }
    }
}