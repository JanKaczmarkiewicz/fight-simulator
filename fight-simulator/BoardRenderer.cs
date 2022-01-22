using System;
using System.Collections.Generic;
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

        public void Draw(SKPaintSurfaceEventArgs e, List<BoardManager> boardManagers)
        {
            var windowHeight = e.Info.Height;
            var windowWidth = e.Info.Width;

            var boardsInColumn = 4;
            
            var boardHeight = boardManagers[0].GetHeight();
            var boardWidth = boardManagers[0].GetWidth();

            var totalBoardsWidth = Math.Min(boardsInColumn, boardManagers.Count) * boardWidth;
            var totalBoardsHeight =  boardHeight * Math.Ceiling((double) boardManagers.Count / boardsInColumn);
            
            var canvas = e.Surface.Canvas;
            
            var scale = Math.Max(
                totalBoardsHeight / windowHeight,
                totalBoardsWidth / windowWidth
            );

            canvas.Clear(SKColors.White);

            for (var i = 0; i < boardManagers.Count; i++)
            {
                var boardActualHeight = boardHeight / scale;
                var boardActualWidth = boardWidth / scale;

                var shiftY = boardActualHeight * Math.Floor((double) i / boardsInColumn);
                var shiftX = boardActualWidth * (i % boardsInColumn);
                
                canvas.DrawRect(
                    (float) shiftX,
                    (float) shiftY,
                    (float) boardActualWidth,
                    (float) boardActualHeight,
                    _lightGreyPaint
                );
                
                var leftUp = new SKPoint((float)shiftX, (float)shiftY);
                var rightUp = new SKPoint((float)shiftX + (float) boardActualWidth, (float)shiftY);
                var leftDown = new SKPoint((float)shiftX, (float)shiftY + (float)boardActualHeight);
                var rightDown = new SKPoint((float)shiftX + (float) boardActualWidth, (float)shiftY + (float)boardActualHeight);
                    
                canvas.DrawLine(leftUp, rightUp, _blackPaint);
                canvas.DrawLine(rightUp, rightDown, _blackPaint);
                canvas.DrawLine(rightDown, leftDown, _blackPaint);
                canvas.DrawLine(leftDown, leftUp, _blackPaint);

                boardManagers[i].GetCircles().ForEach((circle) => 
                    canvas.DrawCircle(
                        (float) ((circle.X / scale) + shiftX),
                        (float) ((circle.Y / scale) + shiftY),
                        (float) ((circle.Radius) / scale),
                        GetPaint(circle.Fraction)
                    ));
            }
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