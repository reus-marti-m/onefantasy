namespace OneFantasy.Api.Domain.Helpers
{
    public class ProbabilityPriceCalculator
    {

        private static readonly (decimal Min, decimal Max, int Price)[] _bands =
        [
            (1.01m, 1.11m, 100),
            (1.12m, 1.24m,  90),
            (1.25m, 1.42m,  80),
            (1.43m, 1.66m,  70),
            (1.67m, 1.99m,  60),
            (2.00m, 2.49m,  50),
            (2.50m, 3.33m,  40),
            (3.34m, 4.99m,  30),
            (5.00m, 9.99m,  20),
            (10.00m, 30.00m, 10)
        ];

        public static int GetPrice(decimal probability)
        {
            foreach (var (min, max, price) in _bands)
            {
                if (probability >= min && probability <= max)
                    return price;
            }
            return 10;
        }

    }
}
