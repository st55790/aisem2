using System;

namespace TravelingSalesmanProblem
{
    public class Program
    {

        static void Main(string[] args)
        {

            PubController pubController = new PubController();
            pubController.LoadPubs("D:\\MagisterskeStudium\\NNUI1 - Zaklady umele inteligence\\SEMESTRALNI_PRACE02\\Pubs.txt");
            //Rout rout = new Rout(pubController.Pubs);

            //Population population = new Population(rout);
            //population.GenerateNewPopulation();

            System system = new System(pubController.Pubs);
            system.Run();
            //pubController.CalculatePathBetweenPubs();
            //pubController.PrintDistances();
        }
    }
}
