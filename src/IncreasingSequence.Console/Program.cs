
using IncreasingSequence.Core;

try
{
    string? input;

    if (args.Length > 0)
    {
        // Accept the sequence as command-line arguments.
        input = string.Join(" ", args);
    }
    else if (Console.IsInputRedirected)
    {
        // Support piped input and Docker execution.
        input = Console.ReadLine();
    }
    else
    {
        // Interactive execution.
        Console.WriteLine(
            "Enter integers separated by spaces:");

        input = Console.ReadLine();
    }

    if (string.IsNullOrWhiteSpace(input))
    {
        Console.Error.WriteLine(
            "Error: Please provide a sequence of integers.");

        return 1;
    }

    var result = IncreasingSequenceService.FindLongest(input);

    Console.WriteLine(result);

    return 0;
}
catch (FormatException ex)
{
    Console.Error.WriteLine($"Invalid input: {ex.Message}");
    return 1;
}
catch (IOException ex)
{
    Console.Error.WriteLine($"Input/output error: {ex.Message}");
    return 1;
}
