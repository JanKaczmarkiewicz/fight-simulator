using System;
using System.Collections.Generic;
using AppKit;
using Foundation;
using SkiaSharp.Views.Mac;

namespace fight_simulator
{
    public partial class ViewController : NSViewController
    {
        private List<BoardManager> _boardManagers = new List<BoardManager>();
        private readonly BoardRenderer _boardRenderer = new BoardRenderer();

        public ViewController(IntPtr handle)
            : base(handle)
        {
            _boardManagers.Add(new BoardManager());
        }
        
        
        partial void PopBoard (NSObject sender)
        {
            _boardManagers.RemoveAt(_boardManagers.Count - 1);
        }
        
        partial void AddBoard (NSObject sender)
        {
            var manager = new BoardManager();
            manager.StartLoop();
            _boardManagers.Add(manager);
        }

        void UpdateLabels()
        {
            RedLabel.StringValue = $"Red: {0}";
            BlueLabel.StringValue = $"Blue: {0}";
            BlackLabel.StringValue = $"Black: {0}";
            GreenLabel.StringValue = $"Green: {0}";
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