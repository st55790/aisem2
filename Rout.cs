using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelingSalesmanProblem
{
    public class Rout : IComparable<Rout>
    {
        public List<Pub> VisitedPubs { get; set; }
        public double Distance { get; set; }
        public double Fitness { get; set; }



        public Rout(List<Pub> list) {
            VisitedPubs = list;
            Distance = CalculateDistance();
            Fitness = CalculateFitnes();
        }

        private double CalculateFitnes()
        {
            //https://stackoverflow.com/questions/9649553/fitness-function-in-genetic-algorithm-for-the-travelling-salesman
            return 1 / Distance;
        }

        private double CalculateDistance()
        {
            double distance = 0;
            for (int i = 0; i < VisitedPubs.Count; i++)
            {
                Pub firstPub = VisitedPubs[i];
                Pub secondPub = VisitedPubs[(i + 1) % VisitedPubs.Count];
                distance += GetDistanceFromLatLonInM(firstPub.Lat, firstPub.Lon, secondPub.Lat, secondPub.Lon);
            }

            return distance;
        }

        private double GetDistanceFromLatLonInM(double lat1, double lon1, double lat2, double lon2)
        {
            var R = 6371; // Radius of the earth in km
            var dLat = Deg2rad(lat2 - lat1);  // deg2rad below
            var dLon = Deg2rad(lon2 - lon1);
            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(Deg2rad(lat1)) * Math.Cos(Deg2rad(lat2)) *
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            var d = R * c * 1; // Distance in m
            //double d = 6371 * Math.Acos(
            //        Math.Sin(lat1) * Math.Sin(lat2) +
            //        Math.Cos(lat1) * Math.Cos(lat2) *
            //        Math.Cos(lon1 - lon2)
            //    );
            return d;
        }

        private double Deg2rad(double deg)
        {
            return deg * (Math.PI / 180);
        }

        public List<Pub> Shuffle() {
            Random random = new Random();
            List<Pub> tmp = new List<Pub>(VisitedPubs);
            int count = tmp.Count;

            while (1 < count)
            {
                count--;
                int k = random.Next(count + 1);
                Pub v = tmp[k];
                tmp[k] = tmp[count];
                tmp[count] = v;
            }

            return tmp;
        }

        public void PrintPath() {
            foreach (var item in VisitedPubs)
            {
                Console.Write(item.ID + "->");
            }
            Console.WriteLine("\n");
        }

        public int CompareTo(Rout other)
        {
            return Fitness.CompareTo(other.Fitness);
        }
    }
}
