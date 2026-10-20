using System;
using System.Collections.Generic;
using System.Text;

using System.Globalization;

namespace IncreasingSequence.Core;

public static class IncreasingSequenceService
{
    public static string FindLongest(string input)
    {
        var numbers = IntegerSequenceParser.Parse(input);

        var result = IncreasingSequenceFinder.FindLongest(numbers);

        return string.Join(
            " ",
            result.Select(number =>
                number.ToString(CultureInfo.InvariantCulture)));
    }
}
