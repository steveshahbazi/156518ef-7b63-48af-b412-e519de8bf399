namespace IncreasingSequence.Core;

public static class IncreasingSequenceFinder
{
    public static IReadOnlyList<int> FindLongest(
        IReadOnlyList<int> numbers)
    {
        ArgumentNullException.ThrowIfNull(numbers);

        if (numbers.Count == 0)
        {
            return Array.Empty<int>();
        }

        var bestStart = 0;
        var bestLength = 1;

        var currentStart = 0;
        var currentLength = 1;

        for (var i = 1; i < numbers.Count; i++)
        {
            if (numbers[i] > numbers[i - 1])
            {
                currentLength++;
            }
            else
            {
                currentStart = i;
                currentLength = 1;
            }

            if (currentLength > bestLength)
            {
                bestStart = currentStart;
                bestLength = currentLength;
            }
        }

        var result = new int[bestLength];

        for (var i = 0; i < bestLength; i++)
        {
            result[i] = numbers[bestStart + i];
        }

        return result;
    }
}
