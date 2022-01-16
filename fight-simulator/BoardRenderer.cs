using System;
using SkiaSharp;
using SkiaSharp.Views.Mac;

namespace fight_simulator
{
    public class BoardRenderer
    {
        SKPaint _bluePaint = new SKPaint
        {
            Color = SKColors.Blue,
        };

        SKPaint _lightGreyPaint = new SKPaint
        {
            Color = SKColors.LightGray,
        };

        SKPaint _redPaint = new SKPaint
        {
            Color = SKColors.Red,
        };

        SKPaint _greenPaint = new SKPaint
        {
            Color = SKColors.Green,
        };

        SKPaint _blackPaint = new SKPaint
        {
            Color = SKColors.Black,
        };

        public void Draw(SKPaintSurfaceEventArgs e, BoardManager boardManager)
        {
            var circles = boardManager.GetCircles();

            var windowHeight = e.Info.Height;
            var windowWidth = e.Info.Width;

            var boardHeight = boardManager.GetHeight();
            var boardWidth = boardManager.GetWidth();

            var scale = Math.Max(
                boardHeight / windowHeight,
                boardWidth / windowWidth
            );

            var boardActualHeight = boardHeight / scale;
            var boardActualWidth = boardWidth / scale;

            var shiftY = (windowHeight - boardActualHeight) / 2;
            var shiftX = (windowWidth - boardActualWidth) / 2;

            var canvas = e.Surface.Canvas;

            canvas.Clear(SKColors.White);
            canvas.DrawRect(
                (float) shiftX,
                (float) shiftY,
                (float) boardActualWidth,
                (float) boardActualHeight,
                _lightGreyPaint
            );

            foreach (var circle in circles)
                canvas.DrawCircle(
                    (float) ((circle.X / scale) + shiftX),
                    (float) ((circle.Y / scale) + shiftY),
                    (float) ((circle.Radius) / scale),
                    GetPaint(circle.Fraction)
                );
        }

        private SKPaint GetPaint(Fraction fraction)
        {
            switch (fraction)
            {
                case Fraction.Black:
                    return _blackPaint;
                case Fraction.Green:
                    return _greenPaint;
                case Fraction.Red:
                    return _redPaint;
                case Fraction.Blue:
                    return _bluePaint;
                default:
                    return _greenPaint;
            }
        }
    }
}