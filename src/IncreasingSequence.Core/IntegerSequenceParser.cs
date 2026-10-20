using System;
using System.Collections.Generic;
using System.Text;

using System.Globalization;

namespace IncreasingSequence.Core;

public static class IntegerSequenceParser
{
    public static IReadOnlyList<int> Parse(string input)
    {
        ArgumentNullException.ThrowIfNull(input);

        if (string.IsNullOrWhiteSpace(input))
        {
            return Array.Empty<int>();
        }

        var tokens = input.Split(
            ' ',
            StringSplitOptions.RemoveEmptyEntries);

        var numbers = new int[tokens.Length];

        for (var i = 0; i < tokens.Length; i++)
        {
            if (!int.TryParse(
                    tokens[i],
                    NumberStyles.Integer,
                    CultureInfo.InvariantCulture,
                    out var number))
            {
                throw new FormatException(
                    $"Invalid integer at position {i + 1}: '{tokens[i]}'.");
            }

            numbers[i] = number;
        }

        return numbers;
    }
}

