using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelingSalesmanProblem
{
    public class System
    {
        private List<Pub> pubs;
        private int generation = 0;
        private Rout bestRout;
        private double bestFitness = 0;


        public System(List<Pub> pubs)
        {
            this.pubs = pubs;
        }

        public void Run() {
            //Vytvoření prvni generace
            //Generuj prvních X ruznych Rout
            //Population
            Rout rout = new Rout(pubs);
            Population population = new Population(rout);
            //population.GenerateNewPopulation();

            while (/*generation < 100*/true) {
                //SELEKCE
                //nejak vybrat x% TOP a těch nějak párovat v potomky
                //KRIZENI
                population.GenerateNewPopulation();
                //MUTACE
                generation++;
                Console.WriteLine($"Generace: {generation}\nDistance: {population.BestDistance}\n\n");
                if (generation == 1000)
                {
                    Rout bestRout = population.PopulationOfRouts[0];
                    foreach (var item in population.PopulationOfRouts)
                    {
                        if (bestRout.Fitness < item.Fitness)
                        {
                            bestRout = item;
                        }
                    }

                    bestRout.PrintPath();
                    Console.ReadKey();
                }
            }
        }
    }
}
