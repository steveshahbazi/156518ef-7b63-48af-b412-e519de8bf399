using IncreasingSequence.Core;

namespace IncreasingSequence.Tests;

public class IncreasingSequenceFinderTests
{
    [Theory]
    [InlineData(
        new int[] { 6, 1, 5, 9, 2 },
        new int[] { 1, 5, 9 })]
    [InlineData(
        new int[] { 1, 2, 3, 4, 5 },
        new int[] { 1, 2, 3, 4, 5 })]
    [InlineData(
        new int[] { 5, 4, 3, 2, 1 },
        new int[] { 5 })]
    [InlineData(
        new int[] { 3, 5, 7, 2, 4, 6 },
        new int[] { 3, 5, 7 })]
    [InlineData(
        new int[] { 1, 2, 2, 3 },
        new int[] { 1, 2 })]
    [InlineData(
        new int[] { -5, -3, -1, -4, 0, 2 },
        new int[] { -5, -3, -1 })]
    [InlineData(
        new int[] { 42 },
        new int[] { 42 })]
    public void FindLongest_ReturnsExpectedSequence(
        int[] input,
        int[] expected)
    {
        var result = IncreasingSequenceFinder.FindLongest(input);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void FindLongest_EmptyInput_ReturnsEmptySequence()
    {
        var result = IncreasingSequenceFinder.FindLongest(
            Array.Empty<int>());

        Assert.Empty(result);
    }

    [Fact]
    public void FindLongest_NullInput_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(
            () => IncreasingSequenceFinder.FindLongest(null!));
    }

    [Fact]
    public void FindLongest_DoesNotModifyInput()
    {
        int[] input = [3, 1, 2, 5, 0];
        int[] original = (int[])input.Clone();

        IncreasingSequenceFinder.FindLongest(input);

        Assert.Equal(original, input);
    }

    [Fact]
    public void FindLongest_EqualValues_DoNotIncreaseRunLength()
    {
        int[] input = [4, 4, 4, 4];

        var result = IncreasingSequenceFinder.FindLongest(input);

        Assert.Equal(new[] { 4 }, result);
    }
}
