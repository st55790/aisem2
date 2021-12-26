using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelingSalesmanProblem
{
    public class Population
    {
        public List<Rout> PopulationOfRouts { get; set; } = new List<Rout>();
        private Rout initRout;
        private double bestFitness = 0;
        private Random random = new Random();
        public double BestDistance { get; set; } = double.MaxValue;

        public Population(Rout rout)
        {
            initRout = rout;
            GenerateFirstPopulation(1000);
        }

        private void GenerateFirstPopulation(int populationSize)
        {
            PopulationOfRouts = new List<Rout>();
            for (int i = 0; i < 1000; i++)
            {
                PopulationOfRouts.Add(new Rout(initRout.Shuffle()));
                //PopulationOfRouts[i].PrintPath();
                //Console.WriteLine($"Distance: {PopulationOfRouts[i].Distance}\nFitness: {PopulationOfRouts[i].Fitness}");

                //Console.WriteLine("\n");
            }
            //Console.WriteLine("\n");
            GetBestFitness();
        }

        public void GetBestFitness() {
            foreach (Rout item in PopulationOfRouts)
            {
                if(item.Fitness > bestFitness)
                {
                    bestFitness = item.Fitness;
                }
                if (item.Distance < BestDistance)
                {
                    BestDistance = item.Distance;
                }
            }
        }

        public void GenerateNewPopulation() {
            List<Rout> tmp = new List<Rout>();
            List<Rout> sortedPopulation = new List<Rout>(PopulationOfRouts);
            sortedPopulation.Sort();
            sortedPopulation.Reverse();

            for (int i = 0; i < 1000; i++)
            {
                Rout firstParent = SelectParent(sortedPopulation);
                Rout secondParent = SelectParent(sortedPopulation);
                Rout child = Crossing(firstParent, secondParent);
                child = Mutate(child);
                tmp.Add(child);
            }

            PopulationOfRouts = tmp;
            GetBestFitness();
        }

        private Rout Crossing(Rout firstParent, Rout secondParent)
        {
            int start = random.Next(firstParent.VisitedPubs.Count);
            int end = random.Next(start, firstParent.VisitedPubs.Count);
            List<Pub> tmp = new List<Pub>();
            tmp = firstParent.VisitedPubs.GetRange(start, end-start);

            for (int i = 0; i < secondParent.VisitedPubs.Count; i++)
            {
                if (!tmp.Contains(secondParent.VisitedPubs[i])){
                    tmp.Add(secondParent.VisitedPubs[i]);
                }
            }

            return new Rout(tmp);
        }

        public Rout SelectParent(List<Rout> list) {
            double chance = random.NextDouble();
            if (chance <= 0.4) {
                return list[random.Next(0, 250)];
            } else if (chance <= 0.7) {
                //Random from 25-49
                return list[random.Next(250, 500)];
            }
            else if (chance <= 0.9)
            {
                //Random from 50-74
                return list[random.Next(500, 750)];
            }
            else {
                //Radon from 75-99
                return list[random.Next(750, 1000)];
            }
        }

        public Rout Mutate(Rout rout) {
            double chance = random.Next();
            if (chance < 0.05) {
                int i = random.Next(0, 1000);
                int j = random.Next(0, 1000);
                Pub tmp = rout.VisitedPubs[i];
                rout.VisitedPubs[i] = rout.VisitedPubs[j];
                rout.VisitedPubs[j] = tmp;
            }
            return new Rout(rout.VisitedPubs);
        }
    }
}
