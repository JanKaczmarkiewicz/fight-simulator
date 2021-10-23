using System;
using System.Linq;
using AppKit;
using SkiaSharp;
using SkiaSharp.Views.Mac;

namespace fight_simulator
{
    public partial class ViewController : NSViewController
    {
        private BoardManager boardManager = new BoardManager(0.75);
        private BoardRenderer boardRenderer = new BoardRenderer();

        public ViewController(IntPtr handle)
            : base(handle)
        {
        }


        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            skiaView.IgnorePixelScaling = true;
            skiaView.PaintSurface += OnPaintSurface;
        }


        private void OnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            boardRenderer.Draw(e, boardManager);
        }
    }
}