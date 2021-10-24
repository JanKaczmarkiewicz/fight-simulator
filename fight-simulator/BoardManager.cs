using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;

namespace fight_simulator
{
    public class BoardManager
    {
        const int InitialFractionMembersCount = 5;

        private int _fractionsCount = Enum.GetNames(typeof(Fraction)).Length;

        private Random _rnd = new Random();
        private double _ratio;
        private Circle[] _circles;

        public Circle[] GetCircles() => _circles;

        public double GetHeight() => _ratio;

        public double GetWidth() => 1;

        public BoardManager(double ratio)
        {
            this._ratio = ratio;
            this._circles = Enumerable
                .Range(1, _fractionsCount * InitialFractionMembersCount)
                .Select((val) =>
                {
                    Fraction fraction;
                    Fraction.TryParse((val % _fractionsCount).ToString(), out fraction);
                    return fraction;
                }).Select(
                    (fraction) => CreateCircle(fraction)
                ).ToArray();
        }

        private Circle CreateCircle(Fraction fraction)
        {
            const double radius = 0.01;
            return new Circle()
            {
                X = _rnd.NextDouble() * (GetWidth() - 2 * radius) + radius,
                Y = _rnd.NextDouble() * (GetHeight() - 2 * radius) + radius,
                Fraction = fraction,
                DirectionAngle = _rnd.NextDouble() * 2 * Math.PI,
                Radius = radius,
                Velocity = 0.001
            };
        }

        public void UpdateCircles()
        {
            for (var i = 0; i < _circles.Length; i++)
            {
                var nextX = _circles[i].X + (Math.Cos(_circles[i].DirectionAngle) * _circles[i].Velocity);
                var nextY = _circles[i].Y + (Math.Sin(_circles[i].DirectionAngle) * _circles[i].Velocity);
                
                
                _circles[i].X = nextX;
                _circles[i].Y = nextY;
            }
        }

        public void StartLoop(Action action)
        {
            NSTimer.CreateRepeatingScheduledTimer(
                new TimeSpan(
                    0,
                    0,
                    0,
                    0,
                    33
                ),
                (t) =>
                {
                    UpdateCircles();
                    action();
                });
        }
    }
}