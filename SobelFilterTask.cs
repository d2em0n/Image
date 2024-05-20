using Microsoft.CodeAnalysis.CSharp.Syntax;
using SkiaSharp;
using System;

namespace Recognizer;
internal static class SobelFilterTask
{
    public static double[,] SobelFilter(double[,] g, double[,] sx)
    {
        var sSize = sx.GetLength(0);
        var width = g.GetLength(0);
        var height = g.GetLength(1);
        var result = new double[width, height];
        for (int y = sSize / 2; y < height - sSize / 2; y++)
            for (int x = sSize / 2; x < width - sSize / 2; x++)
            {
                result[x, y] = FilteredPoint(g, sx, x, y);
            }
        return result;
    }

    public static double FilteredPoint(double[,] g, double[,] sx, int pointX, int pointY)
    {
        var sSize = sx.GetLength(0);
        var xG = pointX - sSize / 2;
        var yG = pointY - sSize / 2;
        double gx = 0;
        double gy = 0;
        for (var y = 0; y < sSize; y++)
        {
            xG = pointX - sSize / 2; ;
            for (var x = 0; x < sSize; x++)
            {
                gx += g[xG, yG] * sx[x, y];
                gy += g[xG, yG] * sx[y, x];
                xG++;
            }
            yG++;
        }
        return Math.Sqrt(gx * gx + gy * gy);
    }
}