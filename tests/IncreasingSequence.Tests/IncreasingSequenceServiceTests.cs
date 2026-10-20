using System;
using System.Collections.Generic;
using System.Text;

using IncreasingSequence.Core;

namespace IncreasingSequence.Tests;

public class IncreasingSequenceServiceTests
{
    [Theory]
    [InlineData("6 1 5 9 2", "1 5 9")]
    [InlineData("1 2 3 4 5", "1 2 3 4 5")]
    [InlineData("5 4 3 2 1", "5")]
    [InlineData("3 5 7 2 4 6", "3 5 7")]
    [InlineData("-5 -3 -1 -4 0 2", "-5 -3 -1")]
    [InlineData("4 4 4", "4")]
    [InlineData("42", "42")]
    [InlineData("", "")]
    public void FindLongest_ReturnsExpectedOutput(
        string input,
        string expected)
    {
        var result = IncreasingSequenceService.FindLongest(input);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void FindLongest_InvalidInput_ThrowsFormatException()
    {
        Assert.Throws<FormatException>(
            () => IncreasingSequenceService.FindLongest("1 abc 3"));
    }

    [Fact]
    public void FindLongest_NullInput_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(
            () => IncreasingSequenceService.FindLongest(null!));
    }
}
