using System;
using SkiaSharp;
using SkiaSharp.Views.Mac;

namespace fight_simulator
{
    public class BoardRenderer
    {
        SKPaint bluePaint = new SKPaint
        {
            Color = SKColors.Blue,
            IsAntialias = true,
        };

        SKPaint lightGreyPaint = new SKPaint
        {
            Color = SKColors.LightGray,
            IsAntialias = true,
        };

        SKPaint redPaint = new SKPaint
        {
            Color = SKColors.Red,
            IsAntialias = true,
        };

        SKPaint greenPaint = new SKPaint
        {
            Color = SKColors.Green,
            IsAntialias = true,
        };

        SKPaint blackPaint = new SKPaint
        {
            Color = SKColors.Black,
            IsAntialias = true,
        };

        public void Draw(SKPaintSurfaceEventArgs e, BoardManager boardManager)
        {
            var circles = boardManager.GetCircles();

            var canvasHeight = e.Info.Height;
            var canvasWidth = e.Info.Width;

            var boardHeight = boardManager.GetHeight();
            var boardWidth = boardManager.GetWidth();

            var scale = Math.Max((float) boardHeight / canvasHeight, (float) boardWidth / canvasWidth);

            var boardActualHeight = boardHeight / scale;
            var boardActualWidth = boardWidth / scale;

            var shiftY = (canvasHeight - boardActualHeight) / 2;
            var shiftX = (canvasWidth - boardActualWidth) / 2;

            var canvas = e.Surface.Canvas;

            canvas.Clear(SKColors.White);
            canvas.DrawRect(
                (float) shiftX,
                (float) shiftY,
                (float) boardActualWidth,
                (float) boardActualHeight,
                lightGreyPaint);

            foreach (var circle in circles)
            {
                canvas.DrawCircle(
                    (float) ((circle.x / scale) + shiftX),
                    (float) ((circle.y / scale) + shiftY),
                    (float) ((circle.radius) / scale),
                    GetPaint(circle.fraction)
                );
            }
        }

        private SKPaint GetPaint(Fraction fraction)
        {
            switch (fraction)
            {
                case Fraction.Black:
                    return blackPaint;
                case Fraction.Green:
                    return greenPaint;
                case Fraction.Red:
                    return redPaint;
                case Fraction.Blue:
                    return bluePaint;
                default:
                    return greenPaint;
            }
        }
    }
}