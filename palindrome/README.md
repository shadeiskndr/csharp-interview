# Palindrome Checker (.NET 8, Docker)

This project is a C# console application that checks if a given string is a palindrome, ignoring specified "trash" symbols and character casing. It demonstrates efficient string scanning and can be built and run both locally and in a Docker container.

## Features

- **Customizable Trash Symbols:** Specify a set of symbols to ignore during palindrome checking.
- **Case Insensitive:** Comparison ignores character casing.
- **Efficient Algorithm:** Scans the input string only once, without creating new strings or modifying the input.
- **Object-Oriented Design:** Palindrome logic is encapsulated in a dedicated service class.
- **.NET 8:** Built with the latest LTS version of .NET.
- **Docker Support:** Multi-stage Dockerfile for optimized container builds.

## Usage

The application exposes a class:

```csharp
class PalindromeService
{
    public PalindromeService(string trashSymbols);
    public bool IsPalindrome(string inputString);
}
```

- **trashSymbols:** A string containing all symbols to ignore (passed to the constructor).
- **inputString:** The string to check (passed to `IsPalindrome`).

### Example 1

```csharp
var palindrome = new PalindromeService("!@$");
bool result = palindrome.IsPalindrome("a@b!!b$a"); // result == true
```

### Example 2

```csharp
var palindrome = new PalindromeService("#?");
bool result = palindrome.IsPalindrome("?Aa#c"); // result == false
```

## Project Structure

```
palindrome/
├── Dockerfile              # Multi-stage Docker build
├── palindrome.csproj       # .NET project file (targets net8.0)
├── Program.cs              # Main application entry point
└── PalindromeService.cs    # Palindrome checking logic (OOP)
```

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Docker Desktop](https://www.docker.com/products/docker-desktop) (optional, for containerized build/run)

### Build and Run Locally

1. **Navigate to the project directory:**
   ```bash
   cd palindrome
   ```
2. **Run the application:**
   ```bash
   dotnet run
   ```

### Build and Run with Docker

1. **Navigate to the project directory:**
   ```bash
   cd palindrome
   ```
2. **Build the Docker image:**
   ```bash
   docker build -t palindrome-app .
   ```
3. **Run the application in a container:**
   ```bash
   docker run --rm palindrome-app
   ```

## Example Output

```
True
False
```

## Implementation Notes

- The palindrome check uses two pointers to scan from both ends, skipping trash symbols and comparing characters case-insensitively.
- No new strings are created and the input is not modified.
- Each character is visited at most once.
- The palindrome logic is encapsulated in the `PalindromeService` class for reusability and testability.
