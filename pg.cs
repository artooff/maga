using MathNet.Numerics.Distributions;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Complex;
using MathNet.Numerics.Statistics;
using System.Numerics;

namespace PointsGeneration
{
    public class Programm
    {
        public static (double, double)[] GeneratePoints(double A, double B, int n, double p)
        {
            (double, double)[] points = new (double, double)[n];

            int outlierPointsCount = (int)Math.Round(n * p / 100.0);
            int normalPointsCount = n - outlierPointsCount;

            //нормальное распределение
            for (int i = 0; i < normalPointsCount; i++)
            {
                var x = GenerateNormalX();
                var y = CalculateFunction(x, A, B);
                points[i] = (x, y);
            }

            //выбросы
            for (int i = normalPointsCount; i < n; i++)
            {
                points[i] = GenerateUniformPoint(A);
            }
            return points;
        }
        public static double CalculateFunction(double x, double A, double B)
        {
            return A * Math.Sin(x + B);
        }
        public static double GenerateNormalX(/*double mean, double stdDev*/)
        {
            var normalDist = new Normal(0, 1);
            return normalDist.Sample();
        }

        public static (double, double) GenerateUniformPoint(double A)
        {
            var uniformDist = new ContinuousUniform(0, 2 * Math.PI);

            var x = uniformDist.Sample();
            var y = uniformDist.Sample() * 2 * A - A; // Generating y uniformly between [-A, A]
            return (x, y);
        }

        public static void PrintArray((double x, double y)[] points)
        {
            for (int i = 0; i < points.Length; i++)
            {
                if(i == points.Length - 1)
                    Console.WriteLine("[" + points[i].x + "," + points[i].y + "]");
                else
                    Console.WriteLine("[" + points[i].x + "," + points[i].y + "],");
            }
        }

        public static void Main()
        {
            double A = 5;
            double B = 2;
            double p = 20;
            int n = 50;
            var points = GeneratePoints(A, B, n, 0);//массив генерируемых данных\\



            //борьба с выбросами
            var statisticsMean = Statistics.Mean(points.Select(p => p.Item2));
            var statisticsDev = Statistics.StandardDeviation(points.Select(p => p.Item2));

            Console.WriteLine(statisticsMean + " " + statisticsDev);

            //для определения порога используем "правило трех сигм", если значение величины 
            //отличается от среднего более чем на три стандратных отклонения, то считаем его выбросом
            double threshold = 3.0;

            // Фильтрация выбросов
            for (int i = 0; i < n; i++)
            {
                if (Math.Abs(points[i].Item2 - statisticsMean) > threshold * statisticsDev)
                {
                    points[i] = (points[i].Item1, statisticsMean); // Заменяем выброс на среднее значение
                }
            }
        }
    }
}
