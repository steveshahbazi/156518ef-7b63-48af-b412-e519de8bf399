# Increasing Sequence Finder

[![.NET CI](https://github.com/steveshahbazi/UUID/actions/workflows/ci.yml/badge.svg)](https://github.com/steveshahbazi/UUID/actions/workflows/ci.yml)

## Overview

A C#/.NET 10 console application that finds the longest contiguous strictly increasing sequence from a list of integers.

The application accepts a sequence of space-separated integers and returns the longest sequence in which each number is strictly greater than the previous number.

If multiple increasing sequences have the same maximum length, the earliest sequence is returned.

### Example

**Input:**

```text
6 1 5 9 2
```

**Output:**

```text
1 5 9
```

**Tie-breaking example:**

Input:

```text
6 2 4 6 1 5 9 2
```

Output:

```text
2 4 6
```

Both `2 4 6` and `1 5 9` are increasing sequences of length three. The application returns `2 4 6` because it appears first.

## Technology Stack

- C# / .NET 10
- xUnit for unit testing
- Coverlet for code coverage collection
- ReportGenerator for HTML coverage reports
- GitHub Actions for continuous integration
- Docker for containerisation
- Built-in .NET analyzers and EditorConfig for code quality

## Project Structure

```text
src/
  IncreasingSequence.Core/
    IncreasingSequenceFinder.cs
    IntegerSequenceParser.cs
    IncreasingSequenceService.cs
  IncreasingSequence.Console/
    Program.cs

tests/
  IncreasingSequence.Tests/
    TestData/
      case1/
      case2/
      ...
      case11/

.github/
  workflows/
    ci.yml

Dockerfile
.dockerignore
.editorconfig
Directory.Build.props
IncreasingSequence.slnx
README.md
```

The solution separates the sequence-finding logic from the console application, allowing the core functionality to be tested independently and reused in other applications.

## Prerequisites

To build and run the application locally:

- .NET 10 SDK

Alternatively, Docker can be used to run the application without installing the .NET SDK.

## Build

Restore dependencies:

```bash
dotnet restore
```

Build the solution:

```bash
dotnet build --configuration Release
```

## Running the Application

### Command-Line Input

```bash
dotnet run --project src/IncreasingSequence.Console -- "6 1 5 9 2"
```

Expected output:

```text
1 5 9
```

### Interactive Input

```bash
dotnet run --project src/IncreasingSequence.Console
```

Enter a space-separated sequence of integers when prompted.

### Standard Input

```bash
echo "6 1 5 9 2" | dotnet run --project src/IncreasingSequence.Console
```

Expected output:

```text
1 5 9
```

## Algorithm

The application uses a single-pass algorithm to identify the longest contiguous strictly increasing sequence.

The algorithm works as follows:

1. Start with the first number as the beginning of the current sequence.
2. Iterate through the remaining numbers.
3. If the current number is greater than the previous number, extend the current sequence.
4. Otherwise, start a new sequence from the current number.
5. Update the longest sequence only when a strictly longer sequence is found.
6. Return the longest sequence after processing all numbers.

By updating the longest sequence only when a longer sequence is found, the algorithm automatically preserves the earliest sequence when multiple sequences have the same maximum length.

### Time and Space Complexity

- **Time complexity:** O(n), where n is the number of integers.
- **Auxiliary space complexity:** O(k), where k is the length of the returned sequence, excluding input parsing allocations.

The algorithm processes each integer once and does not require sorting or nested loops.

## Running Tests

The solution uses xUnit for automated unit testing.

Run all tests:

```bash
dotnet test --configuration Release
```

The test suite covers:

- Longest increasing sequence detection
- Equal-length sequences and earliest-occurrence tie-breaking
- Strictly decreasing sequences
- Duplicate values
- Negative integers
- Empty input
- Single-element sequences
- Invalid integer input
- The 11 supplied assessment test cases

The algorithm, parser and application service are tested independently.

## Code Quality

The solution uses built-in .NET analyzers and EditorConfig to maintain consistent coding standards.

The following settings are enabled:

- Nullable reference type analysis
- Build-time static code analysis
- Consistent C# formatting and naming conventions
- Compiler and analyzer warnings treated as errors

### Format Code

```bash
dotnet format
```

### Verify Formatting

```bash
dotnet format --verify-no-changes
```

### Run Quality Checks

```bash
dotnet build --configuration Release
dotnet test --configuration Release
dotnet format --verify-no-changes
```

These checks are also executed automatically through GitHub Actions.

## Docker

The application can be built and executed using Docker.

A multi-stage Dockerfile separates the build environment from the runtime environment.

The build stage uses the .NET 10 SDK, while the final stage uses the smaller .NET 10 runtime image.

The container runs as a non-root user.

### Build the Docker Image

```bash
docker build -t increasing-sequence:latest .
```

### Run with Command-Line Input

```bash
docker run --rm increasing-sequence:latest "6 1 5 9 2"
```

Expected output:

```text
1 5 9
```

### Run with Standard Input

```bash
echo "6 1 5 9 2" | docker run --rm -i increasing-sequence:latest
```

Expected output:

```text
1 5 9
```

### Invalid Input

```bash
docker run --rm increasing-sequence:latest "1 abc 3"
```

The application reports an error to standard error and exits with a non-zero status code.

## Code Coverage

Code coverage is collected using Coverlet and reported using ReportGenerator.

Coverage measurements focus on the `IncreasingSequence.Core` assembly.

### Coverage Results

The local coverage report generated on 20 October 2026 recorded:

| Metric | Coverage |
|---|---|
| Line coverage | 100% (42/42) |
| Branch coverage | 100% (16/16) |
| Method coverage | 100% (3/3) |

All three core classes were fully covered:

- `IncreasingSequenceFinder`
- `IntegerSequenceParser`
- `IncreasingSequenceService`

Coverage reports are regenerated automatically by GitHub Actions.

### Collect Coverage

```bash
dotnet test IncreasingSequence.slnx --configuration Release --collect:"XPlat Code Coverage"
```

### Restore ReportGenerator

```bash
dotnet tool restore
```

### Generate HTML Coverage Report

```bash
dotnet tool run reportgenerator -- -reports:"tests/IncreasingSequence.Tests/TestResults/**/coverage.cobertura.xml" -targetdir:"coverage-report" -reporttypes:"Html;TextSummary" -assemblyfilters:"+IncreasingSequence.Core"
```

Open the generated report in a browser:

```text
coverage-report/index.html
```

The HTML report includes line and branch coverage details.

## GitHub Actions

The repository uses GitHub Actions for continuous integration.

The CI workflow runs automatically on pushes and pull requests targeting the `main` branch.

### CI Pipeline

The workflow performs the following steps:

1. Check out the repository.
2. Install the .NET 10 SDK.
3. Restore project dependencies.
4. Build the solution in Release configuration.
5. Verify formatting and linting.
6. Execute unit tests.
7. Collect code coverage using Coverlet.
8. Generate HTML coverage reports using ReportGenerator.
9. Upload the coverage report as a GitHub Actions artifact.

### Viewing CI Results

Open the repository's **Actions** tab and select the latest `.NET CI` workflow run.

The generated coverage report can be downloaded from the **Artifacts** section of the workflow run.

## Error Handling

The application validates its input before processing.

- Empty or whitespace-only input is rejected by the console application.
- Invalid integers produce an error message.
- Valid negative integers are supported.
- Duplicate or equal values break an increasing sequence.
- Invalid input results in a non-zero exit code.

Errors are written to standard error, while successful results are written to standard output.

## Design Decisions

### Separation of Concerns

The application is divided into three projects:

- **IncreasingSequence.Core:** Contains parsing, sequence-finding and service logic.
- **IncreasingSequence.Console:** Provides the command-line interface.
- **IncreasingSequence.Tests:** Contains automated tests.

This structure improves maintainability, reusability and testability.

### Efficient Algorithm

A single-pass O(n) algorithm is used to avoid unnecessary sorting or nested iterations.

### Deterministic Tie-Breaking

The implementation updates the longest sequence only when a strictly longer sequence is discovered, ensuring that the earliest sequence is returned in the event of a tie.

### Automated Testing

Unit tests verify both normal behaviour and edge cases, including all 11 supplied assessment test cases.

### Code Quality

EditorConfig, built-in .NET analyzers and warnings-as-errors settings enforce consistent coding standards.

### Continuous Integration

GitHub Actions automatically verifies that the solution builds, passes tests, meets formatting requirements and generates code coverage reports.

### Containerisation

Docker provides a consistent execution environment, allowing the application to run without requiring the .NET SDK on the host machine.