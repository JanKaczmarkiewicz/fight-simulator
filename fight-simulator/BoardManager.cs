using System;
using System.Linq;

namespace fight_simulator
{
    public class BoardManager
    {
        const int InitialFractionMembersCount = 10;

        private int fractionsCount = Enum.GetNames(typeof(Fraction)).Length;

        private Random rnd = new Random();
        private double ratio;
        private Circle[] circles;

        public Circle[] GetCircles() => circles;

        public double GetHeight() => ratio;

        public double GetWidth() => 1;

        public BoardManager(double ratio)
        {
            var radius = 0.01;
            this.ratio = ratio;
            this.circles = Enumerable
                .Range(1, fractionsCount * InitialFractionMembersCount)
                .Select((val) =>
                {
                    Fraction fraction;
                    Fraction.TryParse((val % fractionsCount).ToString(), out fraction);
                    return fraction;
                })
                .Select((fraction) => new Circle()
                    {
                        x = rnd.NextDouble() * (GetWidth() - 2 * radius) + radius,
                        y = rnd.NextDouble() * (GetHeight() - 2 * radius) + radius,
                        fraction = fraction,
                        radius = 0.01
                    }
                ).ToArray();
        }
    }
}