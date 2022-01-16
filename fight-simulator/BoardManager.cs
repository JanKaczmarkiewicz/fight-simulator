using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;

namespace fight_simulator
{
    public class BoardManager
    {
        private const int InitialFractionMembersCount = 5;
        private readonly int _fractionsCount = Enum.GetNames(typeof(Fraction)).Length;
        private readonly Random _rnd = new Random();
        private readonly double _ratio;
        private List<Circle> _circles = new List<Circle>();

        private Dictionary<Fraction, int> points =
            Enum.GetValues(typeof(Fraction)).Cast<Fraction>().ToDictionary(value => value, value => 5);

        public List<Circle> GetCircles() => _circles;

        public double GetHeight() => _ratio;

        public double GetWidth() => 1;

        public BoardManager(double ratio)
        {
            _ratio = ratio;

            for (var i = 0; i < _fractionsCount * InitialFractionMembersCount; i++)
            {
                Enum.TryParse((i % _fractionsCount).ToString(), out Fraction fraction);
                _circles.Add(CreateCircle(fraction));
            }
        }

        private int GetFractionPoints(Fraction fraction) => points[fraction];
        private void IncrementFractionPoints(Fraction fraction) => points[fraction] += 1;
        private void DecrementFractionPoints(Fraction fraction) => points[fraction] -= 1;

        private Circle CreateCircle(Fraction fraction)
        {
            const double radius = 0.05;
            const double velocity = 0.001;
            var circle = new Circle()
            {
                X = 0,
                Y = 0,
                Fraction = fraction,
                DirectionAngle = _rnd.NextDouble() * 2 * Math.PI,
                Radius = radius,
                Velocity = velocity
            };
            
            do
            {
                circle.X = _rnd.NextDouble() * (GetWidth() - 2 * radius) + radius;
                circle.Y = _rnd.NextDouble() * (GetHeight() - 2 * radius) + radius;
            } while (_circles.Any((currentCircle) => AreColliding(circle, currentCircle)));

            return circle;
        }

        private static bool AreColliding(Circle circle1, Circle circle2)
        {
            var minDistance = circle1.Radius + circle2.Radius;
            return Math.Pow(circle1.X - circle2.X, 2) + Math.Pow(circle1.Y - circle2.Y, 2) <= Math.Pow(minDistance, 2);
        }

        private void UpdateCircles()
        {
            var circlesToRemove = new HashSet<int>();
            for (var i = 0; i < _circles.Count; i++)
            {
                var circle = _circles[i];

                if (circle.Y - circle.Radius <= 0)
                    circle.DirectionAngle = 0 * Math.PI - circle.DirectionAngle;

                if (circle.X + circle.Radius >= GetWidth())
                    circle.DirectionAngle = 1 * Math.PI - circle.DirectionAngle;

                if (circle.Y + circle.Radius >= GetHeight())
                    circle.DirectionAngle = 2 * Math.PI - circle.DirectionAngle;

                if (circle.X - circle.Radius <= 0)
                    circle.DirectionAngle = 3 * Math.PI - circle.DirectionAngle;

                for (var j = i + 1; j < _circles.Count; j++)
                {
                    var otherCircle = _circles[j];

                    if (!AreColliding(circle, otherCircle)) continue;

                    if (GetFractionPoints(circle.Fraction) > GetFractionPoints(otherCircle.Fraction))
                    {
                        IncrementFractionPoints(circle.Fraction);
                        DecrementFractionPoints(otherCircle.Fraction);
                        circlesToRemove.Add(j);
                        continue;
                    }
                    
                    if (GetFractionPoints(circle.Fraction) < GetFractionPoints(otherCircle.Fraction))
                    {
                        IncrementFractionPoints(otherCircle.Fraction);
                        DecrementFractionPoints(circle.Fraction);
                        circlesToRemove.Add(i);
                        continue;
                    }

                    if (GetFractionPoints(circle.Fraction) == GetFractionPoints(otherCircle.Fraction))
                    {
                        if (circle.Fraction == otherCircle.Fraction)
                        {
                            IncrementFractionPoints(circle.Fraction);
                        }
                        else
                        {
                            DecrementFractionPoints(circle.Fraction);
                            DecrementFractionPoints(otherCircle.Fraction);
                        }

                        (circle.DirectionAngle, otherCircle.DirectionAngle) =
                            (otherCircle.DirectionAngle, circle.DirectionAngle);
                        
                        _circles[j] = otherCircle;
                    }
                }

                var nextX = circle.X + (Math.Cos(circle.DirectionAngle) * circle.Velocity);
                var nextY = circle.Y + (Math.Sin(circle.DirectionAngle) * circle.Velocity);

                circle.X = nextX;
                circle.Y = nextY;

                _circles[i] = circle;
            }

            // remove circles
            _circles = _circles.Where((circle, i) => !circlesToRemove.Contains(i)).ToList();
        }

        public void StartLoop(Action action)
        {
            NSTimer.CreateRepeatingScheduledTimer(
                new TimeSpan(
                    0,
                    0,
                    0,
                    0,
                    10
                ),
                (t) =>
                {
                    UpdateCircles();
                    action();
                });
        }
    }
}