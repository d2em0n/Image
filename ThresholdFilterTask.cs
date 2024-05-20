using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;

namespace Recognizer;

public static class ThresholdFilterTask
{
	public static double[,] ThresholdFilter(double[,] original, double whitePixelsFraction)
	{
        var n = original.Length;
        var minNumberAboveTreshold = (int)(n * whitePixelsFraction);

        var originalValues = GetSortedValues(original);

        var treshold = double.MaxValue;

        if (minNumberAboveTreshold != 0)
            treshold = originalValues[n - minNumberAboveTreshold];

        var xSize = original.GetLength(0);
        var ySize = original.GetLength(1);
        var result = new double[xSize, ySize];

        for (var row = 0; row < ySize; row++)
        {
            for (var col = 0; col < xSize; col++)
            {
                if (original[col, row] >= treshold)
                result[col, row] = 1;                
            }
        }
        return result;
	}

	private static List<double> GetSortedValues(double[,] original)
	{	
		var result = new List<double>(original.Length);
		foreach (var value in original)
		{
			result.Add(value);
		}
		result.Sort();
		return result;			;
	}	
}
