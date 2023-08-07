namespace RandomWalks
{
    public class Programm
    {

        public static int EstimateMaxStepsCount(int walksCount)
        {
            int maxStepsCount = 0;
            for (int stepsCount = 1; stepsCount < 31; stepsCount++)
            {
                int goodDistancesCount = 0; // Количество прогулок при которых расстояние от начальной точки оказалось меньше 4
                for (int i = 0; i < walksCount; i++) //осуществляем большое количество прогулок
                {
                    (int x, int y) = RandomWalk(stepsCount);
                    int distance = Math.Abs(x) + Math.Abs(y);

                    if (distance < 4)
                    {
                        goodDistancesCount++;
                    }
                }
                double goodResultsPercentage = (double)goodDistancesCount / walksCount * 100;

                if(goodResultsPercentage > 50 && stepsCount > maxStepsCount)//т.к. расстояние должно быть в среднем больше 4,
                    //то учитываем те результаты, в которых расстояние больше 4 более чем в 50 процентах случаев, а количество шагов максимально
                {
                    maxStepsCount = stepsCount;
                } 

                Console.WriteLine($"Walk size = {stepsCount}, % of good results = {goodResultsPercentage}");

                
            }

            return maxStepsCount;
        }
        public static (int, int) RandomWalk(int stepsCount)
        {
            var random = new Random();

            int x = 0, y = 0;
            for(int i = 0; i < stepsCount; i++)
            {
                var moveProbability = random.NextDouble();
                if (moveProbability < 0.5)
                {
                    x--; // Смещение влево
                }
                else if (moveProbability < 0.6)
                {
                    x++; // Смещение вправо
                }
                else if (moveProbability < 0.7)
                {
                    y--; // Смещение вверх
                }
                else
                {
                    y++; // Смещение вниз
                }
            }

            return (x, y);
        }
        public static void Main()
        {
            Console.WriteLine(EstimateMaxStepsCount(10000));
        }
    }
}
