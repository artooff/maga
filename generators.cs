using MathNet.Numerics.Distributions;
using System.Text;

namespace Generators
{
    public class Programm
    {
        public static List<(double, double)> GenerateNormalPoints(int count)
        {
            var normalDist = new Normal(0, 1);
            var list = new List<(double, double)>();
            

            for(int i = 0; i < count; i++)
            {
                var x = normalDist.Sample();
                var y = normalDist.Density(x);
                list.Add((x, y));
            }
            
            return list;
        }

        public static List<(double, double)> GenerateUniformPoints(int count)
        {
            var normalDist = new ContinuousUniform();
            var list = new List<(double, double)>();


            for (int i = 0; i < count; i++)
            {
                var x = normalDist.Sample();
                var y = normalDist.Density(x);
                list.Add((x, y));
            }

            return list;
        }

        public static List<(double, double)> GenerateExponentialPoints(int count)
        {
            var normalDist = new Exponential(1);
            var list = new List<(double, double)>();


            for (int i = 0; i < count; i++)
            {
                var x = normalDist.Sample();
                var y = normalDist.Density(x);
                list.Add((x, y));
            }

            return list;
        }

        public static void Main()
        {
            var list = GenerateExponentialPoints(50);
            foreach(var x in list)
            {
                Console.WriteLine(x);
            }
        }
    }
}
