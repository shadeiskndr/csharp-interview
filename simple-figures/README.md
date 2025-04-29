# C# Geometric Figures Demo with Aggregation and Docker

This project is a C# console application demonstrating object-oriented principles for modeling simple 2D geometric figures (`Point`, `Line`, `Circle`). It showcases the **Composite design pattern** through an `Aggregation` class, allowing groups of figures to be treated as a single entity for transformations like moving and rotating.

The application is built using .NET 8 and includes a multi-stage `Dockerfile` for creating optimized, containerized builds.

## Key Features

- **Geometric Figures**:
  - `Point`: Represents a location in 2D space (X, Y).
  - `Line`: Defined by a start `Point` and an end `Point`. Includes a `Length()` method.
  - `Circle`: Defined by a center `Point` and a `Radius`. Includes `Area()` and `Circumference()` methods.
- **Common Interface (`IGeometricFigure`)**: All figures implement this interface, ensuring they support:
  - `Move(dx, dy)`: Translates the figure by a given offset.
  - `Rotate(angleDegrees, origin)`: Rotates the figure around a specified `Point` origin by a given angle in degrees.
- **Aggregation (Composite Pattern)**:
  - The `Aggregation` class holds a collection of `IGeometricFigure` objects.
  - It _also_ implements `IGeometricFigure`, allowing aggregations to contain other aggregations.
  - When `Move` or `Rotate` is called on an `Aggregation`, the operation is applied to _all_ contained figures.
- **Docker Support**: Includes a multi-stage `Dockerfile` for efficient, small, and secure container images.
- **.NET 8**: Built using the latest LTS version of .NET.

## Technology Stack

- C#
- .NET 8
- Docker

## Project Structure

```
/simple-figures/
├── Dockerfile            # Defines the Docker build process (multi-stage).
├── simple-figures.csproj # .NET project file (targets net8.0).
├── GeometryFigures.cs    # Contains IGeometricFigure interface and Point, Line, Circle classes.
├── Aggregation.cs        # Contains the Aggregation class (Composite pattern).
├── Program.cs            # Main application entry point, demonstrates usage.
└── README.md             # This file.
```

## Getting Started

### Prerequisites

- .NET 8 SDK installed ([Download .NET](https://dotnet.microsoft.com/download))
- Docker Desktop installed (Optional, for containerized build/run) ([Install Docker](https://www.docker.com/products/docker-desktop/))

### Building and Running Locally

1.  **Clone the repository** (or ensure you have the files in a local directory).
2.  **Navigate to the project directory** in your terminal:
    ```bash
    cd simple-figures
    ```
3.  **Run the application:**
    ```bash
    dotnet run
    ```
    This will compile and execute the `Program.cs` file, printing the output to your console.

### Building and Running with Docker

1.  **Navigate to the project directory** containing the `Dockerfile`:
    ```bash
    cd simple-figures
    ```
2.  **Build the Docker image:**
    ```bash
    docker build -t geometric-figures-app .
    ```
    _(You can replace `geometric-figures-app` with your preferred image name)_
3.  **Run the application in a container:**
    ```bash
    docker run --rm geometric-figures-app
    ```
    This will start a container from the built image, run the application, print the output, and automatically remove the container upon completion (`--rm`).

## How It Works

- The `IGeometricFigure` interface defines a common contract for all shapes.
- `Point`, `Line`, and `Circle` provide concrete implementations of this interface. `Line` and `Circle` delegate movement and rotation primarily to their constituent `Point`(s).
- `Aggregation` acts as a composite. It holds a list of `IGeometricFigure` objects. When its `Move` or `Rotate` methods are called, it iterates through its list and calls the corresponding method on each child figure. This allows complex structures to be manipulated easily.

## Usage Example (`Program.cs`)

The `Program.cs` file demonstrates how to create figures, add them to an aggregation, and perform transformations:

```csharp:Program.cs
using System;
using Geometry;

class Program
{
    static void Main(string[] args)
    {
        // 1. Create individual figures
        var p1 = new Point(0, 0);
        var p2 = new Point(3, 4); // Used to define the line, but not added to aggregation directly
        var line = new Line(new Point(0, 0), new Point(3, 4));
        var circle = new Circle(new Point(1, 1), 5);

        // 2. Create an aggregation and add figures to it
        var aggregation = new Aggregation();
        aggregation.Add(p1);    // Add the point
        aggregation.Add(line);  // Add the line
        aggregation.Add(circle);// Add the circle

        // 3. Print initial state
        Console.WriteLine("Original:");
        Console.WriteLine(p1);
        Console.WriteLine(line);
        Console.WriteLine(circle);
        Console.WriteLine(aggregation);

        // 4. Move the entire aggregation
        aggregation.Move(2, 3);
        Console.WriteLine("\nAfter Move(2, 3):");
        Console.WriteLine(p1); // Note: p1 itself is modified as it's referenced in the aggregation
        Console.WriteLine(line);
        Console.WriteLine(circle);
        Console.WriteLine(aggregation);

        // 5. Rotate the entire aggregation around the origin (0,0)
        aggregation.Rotate(90, new Point(0, 0));
        Console.WriteLine("\nAfter Rotate(90, origin):");
        Console.WriteLine(p1);
        Console.WriteLine(line);
        Console.WriteLine(circle);
        Console.WriteLine(aggregation);
    }
}
```

### Expected Output

Running the application (either locally or via Docker) will produce the following output, showing the state of the figures before and after transformations:

```text
Original:
(0, 0)
Line[(0, 0) -> (3, 4)]
Circle[Center: (1, 1), Radius: 5]
Aggregation containing:
  (0, 0)
  Line[(0, 0) -> (3, 4)]
  Circle[Center: (1, 1), Radius: 5]

After Move(2, 3):
(2, 3)
Line[(2, 3) -> (5, 7)]
Circle[Center: (3, 4), Radius: 5]
Aggregation containing:
  (2, 3)
  Line[(2, 3) -> (5, 7)]
  Circle[Center: (3, 4), Radius: 5]

After Rotate(90, origin):
(-3, 2)
Line[(-3, 2) -> (-7, 5)]
Circle[Center: (-4, 3.0000000000000004), Radius: 5]
Aggregation containing:
  (-3, 2)
  Line[(-3, 2) -> (-7, 5)]
  Circle[Center: (-4, 3.0000000000000004), Radius: 5]
```

_(Note: The small floating-point discrepancy in the rotated Circle's center Y-coordinate is normal for floating-point arithmetic.)_

## Dockerfile Explained

The `Dockerfile` uses a multi-stage build approach:

```dockerfile:Dockerfile
# Build stage: Uses the full .NET SDK to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy csproj and restore dependencies first (leverages Docker layer caching)
COPY *.csproj ./
RUN dotnet restore

# Copy the rest of the source code and publish the release build
COPY . ./
RUN dotnet publish -c Release -o out

# Runtime stage: Uses the smaller .NET Runtime image
FROM mcr.microsoft.com/dotnet/runtime:8.0
WORKDIR /app
# Copy only the published application output from the build stage
COPY --from=build /app/out ./

# Set the entrypoint to run the application DLL
ENTRYPOINT ["dotnet", "simple-figures.dll"]
```

**Benefits of Multi-Stage Build:**

- **Smaller Final Image:** The final image only includes the runtime and the compiled application, not the SDK and source code.
- **Improved Security:** Fewer tools and components in the final image reduce the potential attack surface.
- **Optimized Caching:** Separating dependency restoration (`dotnet restore`) from code copying/building improves Docker build cache efficiency.
