using System;
using System.Collections.Generic;
using System.Linq;
using AppKit;
using Foundation;
using SkiaSharp.Views.Mac;

namespace fight_simulator
{
    public partial class ViewController : NSViewController
    {
        private readonly List<BoardManager> _boardManagers = new List<BoardManager>();
        private readonly BoardRenderer _boardRenderer = new BoardRenderer();

        private readonly Dictionary<Fraction, int> points =
            Enum.GetValues(typeof(Fraction)).Cast<Fraction>().ToDictionary(value => value, value => 5);

        public ViewController(IntPtr handle)
            : base(handle)
        {
            _boardManagers.Add(new BoardManager(points));
        }


        partial void PopBoard(NSObject sender)
        {
            _boardManagers.RemoveAt(_boardManagers.Count - 1);
        }

        partial void AddBoard(NSObject sender)
        {
            var manager = new BoardManager(points);
            manager.StartLoop();
            _boardManagers.Add(manager);
        }

        private void UpdateLabels()
        {
            RedLabel.StringValue = $"Red: {points.GetValueOrDefault(Fraction.Red)}";
            BlueLabel.StringValue = $"Blue: {points.GetValueOrDefault(Fraction.Blue)}";
            BlackLabel.StringValue = $"Black: {points.GetValueOrDefault(Fraction.Black)}";
            GreenLabel.StringValue = $"Green: {points.GetValueOrDefault(Fraction.Green)}";
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            skiaView.IgnorePixelScaling = true;
            skiaView.PaintSurface += OnPaintSurface;

            _boardManagers.ForEach(boardManager => boardManager.StartLoop());

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
                    UpdateLabels();
                    skiaView.NeedsDisplay = true;
                });
        }


        private void OnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            _boardRenderer.Draw(e, _boardManagers);
        }
    }
}