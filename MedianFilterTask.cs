using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Recognizer;

internal static class MedianFilterTask
{
    public static double[,] MedianFilter(double[,] original)
    {
        var xSize = original.GetLength(0);
        var ySize = original.GetLength(1);
        var bordered = DrawBorders(original, xSize, ySize);
        var filteredBordered = new double[xSize + 2, ySize + 2];

        for (var y = 1; y < ySize + 1; y++)
        {
            for (var x = 1; x < xSize + 1; x++)
            {
                filteredBordered[x, y] = InnerPointsFilter(bordered, x, y);
            }
        }
        var filtered = new double[xSize, ySize];
        for (var y = 1; y < ySize + 1; y++)
        {
            for (var x = 1; x < xSize + 1; x++)
            {
                filtered[x - 1, y - 1] = filteredBordered[x, y];
            }
        }
        return filtered;
    }

    private static double[,] DrawBorders(double[,] original, int xSize, int ySize)
    {
        var bordered = new double[xSize + 2, ySize + 2];
        for (var x = 0; x < xSize + 2; x++)
        {
            bordered[x, 0] = -1.0;
            bordered[x, ySize + 1] = -1.0;
        }
        for (var y = 0; y < ySize + 2; y++)
        {
            bordered[0, y] = -1.0;
            bordered[xSize + 1, y] = -1.0;
        }
        for (var y = 0; y < ySize; y++)
        {
            for (var x = 0; x < xSize; x++)
            {
                bordered[x + 1, y + 1] = original[x, y];
            }
        }
        return bordered;
    }

    private static double InnerPointsFilter(double[,] original, int x, int y)
    {
        var xSurroundings = new int[] { -1, 0, 1, -1, 1, -1, 0, 1 };
        var ySurroundings = new int[] { -1, -1, -1, 0, 0, 1, 1, 1 };
        var colorValues = new double[9];
        colorValues[8] = original[x, y];
        for (var i = 0; i < 8; i++)
            colorValues[i] = original[x + xSurroundings[i], y + ySurroundings[i]];
        Array.Sort(colorValues);
        var firstTrueValue = 0;
        foreach (var colorValue in colorValues)
        {
            if (colorValue >= 0) break;
            else firstTrueValue++;
        }
        if (firstTrueValue == 0) return colorValues[4];
        else if (firstTrueValue % 2 == 0)
            return colorValues[firstTrueValue + (colorValues.Length - firstTrueValue) / 2];

        var length = colorValues.Length - firstTrueValue;
        var median1 = firstTrueValue + (colorValues.Length - firstTrueValue) / 2 - 1;
        var median2 = firstTrueValue + (colorValues.Length - firstTrueValue) / 2;
        return (colorValues[median1] + colorValues[median2]) / 2;
    }
}
