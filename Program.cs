using Microsoft.VisualBasic;
using Wireworld.WireworldLogic;

namespace Wireworld 
{
    class Program 
    {
        static void Main(string[] args)
        {
            string fileName = "diode.json"; // acá se selecciona que archivo json se quiere probar
            string filePath = Path.Combine("JsonFiles", fileName);
            string json = File.ReadAllText(filePath);
            Grid grid = Grid.FromJson(json);
            int pulseInterval = 10;

            // Posición donde inyectar el nuevo pulso
            int pulseY = 3;
            int pulseX = 0;
            int pulseY2 = 9;

            // Solo inyectar pulsos si el archivo es diode.json
            bool isDiode = Path.GetFileName(filePath).Equals("diode.json", StringComparison.OrdinalIgnoreCase);

            for (int iter = 0; iter < 5000; iter++)
            {
                Console.Clear();
                PrintGrid(grid, iter);
                grid = Loop.Step(grid);

                if (isDiode && iter % pulseInterval == 0)
                {
                    // Evita errores si la grilla es más pequeña que las posiciones dadas
                    if (pulseY < grid.Rows && pulseX < grid.Cols &&
                        pulseY2 < grid.Rows && pulseX < grid.Cols)
                    {
                        var currentCell = grid[pulseY, pulseX];
                        if (currentCell == CellState.Empty || currentCell == CellState.Conductor)
                        {
                            grid[pulseY, pulseX] = CellState.ElectronHead;
                            grid[pulseY2, pulseX] = CellState.ElectronHead;
                        }
                    }
                }

                Thread.Sleep(200);
            }
        }



        static void PrintGrid(Grid grid, int iteration)
        {
            Console.Clear(); // Limpia la consola para no llenar de texto
            Console.WriteLine($"Iteración: {iteration}\n");

            for (int y = 0; y < grid.Rows; y++)
            {
                for (int x = 0; x < grid.Cols; x++)
                {
                    var cell = grid[y, x];
                    Console.ForegroundColor = cell switch
                    {
                        CellState.Empty => ConsoleColor.Black,
                        CellState.Conductor => ConsoleColor.Yellow,
                        CellState.ElectronHead => ConsoleColor.Blue,
                        CellState.ElectronTail => ConsoleColor.Red,
                        _ => ConsoleColor.Gray
                    };
                    Console.Write('█');
                }

                Console.WriteLine();
            }

            Console.ResetColor();
        }



        static char CellToChar(CellState cell)
        {
            return cell switch
            {
                CellState.Empty => ' ',
                CellState.Conductor => '#',
                CellState.ElectronHead => 'H',
                CellState.ElectronTail => 't',
                _ => '?'
            };
        }
    }
}