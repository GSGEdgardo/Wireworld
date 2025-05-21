using System.Text.Json;

namespace Wireworld.WireworldLogic
{
    internal class Grid
    {
        // Es preferible usar un array 2D en vez de una lista de listas,
        // ya que es más eficiente para matrices de tamaño estáticas
        private CellState[,] grid;
        private int Rows => grid.GetLength(0);
        private int Cols => grid.GetLength(1);
        
        // Método para inicializar la cuadrícula
        public Grid(CellState[,] initialGrid)
        {
            grid = initialGrid;
        }
        /*
         * Un indexador en C# es un tipo especial de propiedad que permite acceder a
         * las instancias de una clase o estructura mediante el operador de acceso a matrices[].
         * Los indexadores pueden ser muy útiles para crear "matrices inteligentes" o encapsular
         * datos en una sintaxis simplificada. 
         * En C# cuando se utiliza una matriz bidimensional como CellState[,]
         * La convención dice que el primer índice es la fila 'y' y el segundo índice es la columna 'x'.
         */
        public CellState this[int y, int x] 
        {
            get => grid[y, x];
            set => grid[y, x] = value;
        }

        // métodos de fábrica para crear las matrices según el tipo de entrada:
        // Primer método es para crear una cuadrícula a partir de un array 2D
        public static Grid FromMatrix(List<List<CellState>> input)
        {
            int rows = input.Count;
            int cols = input.Count;
            var data = new CellState[rows, cols];

            for (int y = 0; y < rows; y++) 
            {
                for (int x = 0; x < cols; x++) 
                {
                    data[y, x] = input[y][x];
                }
            }
            return new Grid(data);
        }
        /*
        Segundo método es para crear una cuadrícula a partir de un Json
        Se espera que el Json venga con forma de una grilla, como un array de arrays de strings
        Algo así como:
        [
          [" ", "#", "#", " "],
          ["#", "h", "t", "#"],
          ["#", "#", " ", "#"]
        ]
        Al deserializar el json, se debería tener una estructura similar en la variable raw:
            raw[0] = [" ", "#", "#", " "];
            raw[1] = ["#", "h", "t", "#"];
            raw[2] = ["#", "#", " ", "#"];
        Para obtener el número de columnas se puede usar raw[0].Length al igual que raw[1].Length o raw[2].Length
        */

        public static Grid FromJson(string json) 
        {
            var raw = JsonSerializer.Deserialize<string[][]>(json);

            int rows = raw.Length;
            int cols = raw[0].Length;
            var data = new CellState[rows, cols];

            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < cols; x++)
                {
                    data[y, x] = ParseCell(raw[y][x]);
                }
            }
            return new Grid(data);
        }
        // Este método ParseCell se encarga de convertir el string que viene del Json a un CellState válido.
        private static CellState ParseCell(string s) 
        {
            return s.Trim().ToLower() switch
            {
                " " or "empty" => CellState.Empty,
                "#" or "c" or "conductor" => CellState.Conductor,
                "h" or "head" => CellState.ElectronHead,
                "t" or "tail" => CellState.ElectronTail,
                _ => throw new ArgumentException($"Unknown cell state: {s}")
            };
        }
    }
}
