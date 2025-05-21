# Wireworld Simulator

![C#](https://img.shields.io/badge/c%23-%23239120.svg?)
![Last Commit](https://img.shields.io/github/last-commit/GSGEdgardo/wireworld)
![Institution](https://img.shields.io/badge/institution-Universidad%20Cat%C3%B3lica%20del%20Norte-blue)
![License](https://img.shields.io/badge/license-MIT-blue.svg?)

# Description

This repository contains a cellular automaton simulator implementing [Wireworld](https://en.wikipedia.org/wiki/Wireworld) in C#. The system is capable of loading and simulating grid-based electronic circuits defined in JSON format, including features such as signal diodes, clocks, and logic gates. The console-based visual output uses color-coded blocks to represent the different states of each cell.

A special diode configuration (diode.json) includes automatic pulse injection at fixed intervals to create a periodic signal.

# Wireworld Rules

Wireworld is a cellular automaton that simulates the behavior of electronic circuits. The rules for Wireworld are as follows:

* **Empty**: An empty cell remains empty.
* **Conductor**: A conductor cell becomes an electron head if it has exactly one or two neighboring electron heads. Otherwise, it remains a conductor.
* **Electron Head**: A electron head becomes an electron tail.
* **Electron Tail**: An electron tail becomes a conductor.

# How it works

* Each cell can be in one of four states: Empty, Conductor, Electron Head, or Electron Tail.

* Transition rules are implemented based on neighboring cell states (following Wireworld logic).

* The main program loads a JSON file describing the circuit and executes a fixed number of iterations.

* If the diode.json file is loaded, the program injects an electron pulse every 10 iterations to demonstrate signal propagation through a diode.

# Run the simulation

1. Make sure you have the .NET 8.0 SDK installed.
2. Clone the repository and navigate to the project directory.
3. Run the simulator with `Ctrl + F5` or `dotnet run` in the terminal.
4. The file to be simulated can be chosen in the `Program.cs` file. The default is `diode.json`, but you can change it to any other JSON file in the `json` folder.

# Folder Structure
```
Wireworld/
│
├── WireworldLogic/          # Core simulation logic (Grid, Loop, CellState)
│   ├── Grid.cs
│   ├── Loop.cs
│   └── CellState.cs
│
├── JsonFiles/               # Circuit definitions in JSON format
│   ├── diode.json
│   └── circuit.json
│
├── Program.cs               # Main entry point and pulse injector logic
├── Wireworld.csproj
└── README.md
```

# Cell State Legend

| State        | Console Color |
| -------------|:-------------:|
| Empty        | Black         |
| Conductor    | Yellow        |
| Electron Head| Blue          |
| Electron Tail| Red           |

# License
This project is licensed under the MIT License. See the [MIT LICENSE](https://github.com/GSGEdgardo/Wireworld?tab=MIT-1-ov-file#readme) file for details.