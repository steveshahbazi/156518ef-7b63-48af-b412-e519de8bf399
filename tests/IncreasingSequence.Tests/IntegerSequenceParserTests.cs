using System;
using System.Collections.Generic;
using System.Text;

using IncreasingSequence.Core;

namespace IncreasingSequence.Tests;

public class IntegerSequenceParserTests
{
    [Theory]
    [InlineData("1 2 3", new int[] { 1, 2, 3 })]
    [InlineData("-5 -2 0 4", new int[] { -5, -2, 0, 4 })]
    [InlineData("42", new int[] { 42 })]
    [InlineData("1  2  3", new int[] { 1, 2, 3 })]
    public void Parse_ValidInput_ReturnsIntegers(
        string input,
        int[] expected)
    {
        var result = IntegerSequenceParser.Parse(input);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("   ")]
    public void Parse_EmptyOrWhitespaceInput_ReturnsEmpty(
        string input)
    {
        var result = IntegerSequenceParser.Parse(input);

        Assert.Empty(result);
    }

    [Theory]
    [InlineData("1 abc 3")]
    [InlineData("1 2.5 3")]
    [InlineData("1 2147483648 3")]
    public void Parse_InvalidInput_ThrowsFormatException(
        string input)
    {
        Assert.Throws<FormatException>(
            () => IntegerSequenceParser.Parse(input));
    }

    [Fact]
    public void Parse_NullInput_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(
            () => IntegerSequenceParser.Parse(null!));
    }
}

