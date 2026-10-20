[![.NET CI](https://github.com/steveshahbazi/UUID/actions/workflows/ci.yml/badge.svg)](https://github.com/steveshahbazi/UUID/actions/workflows/ci.yml)

## Running the Application

### Prerequisites

- .NET 10 SDK

### Build

```bash
dotnet restore
dotnet build --configuration Release
```

### Command-Line Usage

```bash
dotnet run --project src/IncreasingSequence.Console -- "6 1 5 9 2"
```

Output:

```text
1 5 9
```

### Interactive Usage

```bash
dotnet run --project src/IncreasingSequence.Console
```

Enter a space-separated sequence of integers when prompted.

### Standard Input

```bash
echo "6 1 5 9 2" | dotnet run --project src/IncreasingSequence.Console
```

### Running Tests

```bash
dotnet test --configuration Release
```

## Algorithm

The application finds the longest contiguous strictly increasing
sequence of integers.

If multiple sequences have the same maximum length, the earliest
sequence is returned.

### Complexity

- Time complexity: O(n)
- Auxiliary space complexity: O(k), where k is the length of the
  returned sequence, excluding the parsed input.

## Error Handling

Invalid input produces an error message on standard error and
returns a non-zero exit code.


## Docker

The application can be built and executed using Docker.

A multi-stage Dockerfile is used to separate compilation from
runtime execution.

The final container runs as a non-root user.

### Build the Image

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

### Error Handling

Invalid input is reported through standard error, and the
container exits with a nonzero status code.



## Code Coverage

Code coverage is collected using Coverlet and reported using
ReportGenerator.

Coverage reports include line and branch coverage for the
core application logic.

### Collect Coverage

```bash
dotnet test IncreasingSequence.slnx \
  --configuration Release \
  --collect:"XPlat Code Coverage"
```

### Generate HTML Report

Restore the reporting tool:

```bash
dotnet tool restore
```

Generate the report:

```bash
dotnet tool run reportgenerator -- \
  -reports:"tests/IncreasingSequence.Tests/TestResults/**/coverage.cobertura.xml" \
  -targetdir:"coverage-report" \
  -reporttypes:"Html;TextSummary" \
  -assemblyfilters:"+IncreasingSequence.Core"
```

Open `coverage-report/index.html` in a browser.

### GitHub Actions

Every push and pull request runs the automated test suite,
collects code coverage and generates a downloadable HTML report.

The report is available under the workflow run's Artifacts section.
