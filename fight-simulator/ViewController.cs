using System;
using AppKit;
using Foundation;
using SkiaSharp.Views.Mac;

namespace fight_simulator
{
    public partial class ViewController : NSViewController
    {
        private readonly BoardManager _boardManager = new BoardManager(0.75);
        private readonly BoardRenderer _boardRenderer = new BoardRenderer();

        public ViewController(IntPtr handle)
            : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            
            skiaView.IgnorePixelScaling = true;
            skiaView.PaintSurface += OnPaintSurface;

            _boardManager.StartLoop(
                () => { skiaView.NeedsDisplay = true; }
            );
        }


        private void OnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            _boardRenderer.Draw(e, _boardManager);
        }
    }
}