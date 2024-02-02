namespace HerBudget
{
    internal class Program
    {
        static void Main(string[] args)
        {
            WebStarter ws = new WebStarter(args);
            ws.ShowWeb();
        }
    }
}