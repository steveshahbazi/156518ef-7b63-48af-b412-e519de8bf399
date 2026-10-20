
using IncreasingSequence.Core;

namespace IncreasingSequence.Tests;

public class AssessmentTestCases
{
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    [InlineData(6)]
    [InlineData(7)]
    [InlineData(8)]
    [InlineData(9)]
    [InlineData(10)]
    [InlineData(11)]
    public void FindLongest_MatchesAssessmentTestCase(
        int caseNumber)
    {
        // Locate the directory for the current test case.
        var testDataDirectory = Path.Combine(
            AppContext.BaseDirectory,
            "TestData",
            $"case{caseNumber}");

        // Each folder contains the same two filenames.
        var inputFile = Path.Combine(
            testDataDirectory,
            "input.txt");

        var expectedFile = Path.Combine(
            testDataDirectory,
            "expected.txt");

        // Read the supplied input and expected output.
        var input = File.ReadAllText(inputFile).Trim();
        var expected = File.ReadAllText(expectedFile).Trim();

        // Execute the algorithm.
        var actual = IncreasingSequenceService.FindLongest(input);

        // Verify the result.
        Assert.Equal(expected, actual);
    }
}
