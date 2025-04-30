# Minefield: SnifferPup & Ally's Safe Passage

This project is a C# console application that simulates guiding a dog, **SnifferPup**, and a girl, **Ally**, safely through a minefield. The minefield is a grid represented by integers. SnifferPup finds a path from a designated start point to an end point, traversing safe path cells. Ally follows SnifferPup's trail, always stepping onto the cell SnifferPup just vacated, and they can never occupy the same cell at the same time.

## Features

- **Object-Oriented Design:**  
  The codebase is organized into multiple classes:

  - `Minefield`: Encapsulates the grid and related logic.
  - `Pathfinder`: Handles pathfinding logic.
  - `Character`: Represents SnifferPup and Ally, tracking their positions and movement.
  - `Program`: Entry point, orchestrates the simulation.

- **Minefield Representation:**
  - `0` = bomb (cannot be stepped on)
  - `1` = safe path (traversable)
  - `2` = designated start/end points (traversable only as the first or last step)
- **SnifferPup's Pathfinding:**
  - Starts at a cell marked `2`.
  - Moves to adjacent cells (8 directions: up, down, left, right, and diagonals) marked `1`.
  - Can move adjacent to bombs (`0`), but cannot step onto them.
  - Aims to reach the end cell marked `2`.
- **Ally's Movement:**
  - Follows SnifferPup's path, always one step behind.
  - Never occupies the same cell as SnifferPup.
  - Never steps on a bomb (`0`). The path found ensures this.
- **Path Existence:**
  The algorithm finds _a_ path if one exists according to the rules (moving between `1`s from the start `2` to the end `2`, avoiding `0`s).

## Example

Given the following minefield (used in `Program.cs`):

```
[0, 2, 0, 0, 0],  // Start at (0, 1)
[0, 1, 0, 0, 0],
[0, 0, 1, 0, 0],
[0, 0, 0, 1, 0],
[0, 0, 0, 1, 0],
[0, 0, 1, 0, 0],
[0, 0, 2, 0, 0]   // End at (6, 2)
```

- `2` = start/end point
- `1` = safe path
- `0` = bomb

The program finds a safe path for SnifferPup and Ally from the start `2` at `(0, 1)` to the end `2` at `(6, 2)`. Note that the path cells (like `(1, 1)`) can be adjacent to bombs.

## Project Structure

```
minefield/
├── Character.cs          # Character class for SnifferPup and Ally
├── Minefield.cs          # Minefield grid and logic
├── Pathfinder.cs         # Pathfinding logic (BFS)
├── Program.cs            # Main application and simulation orchestration
├── minefield.csproj      # .NET project file (targets net8.0)
└── Dockerfile            # Multi-stage Docker build
```

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Docker Desktop](https://www.docker.com/products/docker-desktop) (optional, for containerized build/run)

### Build and Run Locally

1.  **Navigate to the project directory:**
    ```bash
    cd minefield
    ```
2.  **Run the application:**
    ```bash
    dotnet run
    ```

### Build and Run with Docker

1.  **Navigate to the project directory:**
    ```bash
    cd minefield
    ```
2.  **Build the Docker image:**
    ```bash
    docker build -t minefield-app .
    ```
3.  **Run the application in a container:**
    ```bash
    docker run --rm minefield-app
    ```

## Output

The application prints the safe path found for SnifferPup and Ally, step by step.

Example output for the minefield above:

```
Safe path for SnifferPup and Ally:
Step 1: (0, 1)
Step 2: (1, 1)
Step 3: (2, 2)
Step 4: (3, 3)
Step 5: (4, 3)
Step 6: (5, 2)
Step 7: (6, 2)

Ally's trail:
Step 1: Ally at (0, 1), SnifferPup at (1, 1)
Step 2: Ally at (1, 1), SnifferPup at (2, 2)
Step 3: Ally at (2, 2), SnifferPup at (3, 3)
Step 4: Ally at (3, 3), SnifferPup at (4, 3)
Step 5: Ally at (4, 3), SnifferPup at (5, 2)
Step 6: Ally at (5, 2), SnifferPup at (6, 2)
Step 7: Ally at (6, 2)
```

_(Note: The exact path steps between start and end might vary slightly depending on the BFS implementation details, but it will always be a valid path according to the rules.)_

## Implementation Notes

- Uses Breadth-First Search (BFS) to find the shortest path in terms of steps.
- The path starts at a `2`, traverses adjacent `1`s (horizontally, vertically, or diagonally), and ends at the target `2`.
- The pathfinding logic allows movement to cells adjacent to bombs (`0`), but prevents stepping directly onto a bomb.
- Ensures Ally never steps on a bomb and never shares a cell with SnifferPup by following one step behind on the found path.
- Easily customizable for different minefield layouts and start/end positions (marked as `2`).
- The codebase is modular and object-oriented, making it easy to extend and maintain.
