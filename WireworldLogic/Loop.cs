namespace Wireworld.WireworldLogic
{
    internal static class Loop
    {
        public static Grid Step(Grid current) 
        {
            int rows = current.Rows;
            int cols = current.Cols;
            var nextGrid = new CellState[rows, cols];

            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < cols; x++)
                {
                    var cell = current[y, x];
                    /*
                     Estado anterior 	Estado posterior
                        Aislante(Empty) ->	Aislante
                        Conductor 	    ->  Si uno o dos vecinos son ElectronHead: ElectronHead Sino Conductor
                        ElectronHead    ->  ElectronTail
                        ElectronTail 	->  Conductor
                     */
                    switch (cell) 
                    {
                        case CellState.Empty:
                            nextGrid[y, x] = CellState.Empty;
                            break;
                        case CellState.ElectronHead:
                            nextGrid[y, x] = CellState.ElectronTail;
                            break;

                        case CellState.ElectronTail:
                            nextGrid[y, x] = CellState.Conductor;
                            break;
                        case CellState.Conductor:
                            int EHneighbors = CountEHNeighbors(current, y, x);
                            if (EHneighbors == 1 || EHneighbors == 2) 
                            {
                                nextGrid[y, x] = CellState.ElectronHead;
                            }
                            else
                            {
                                nextGrid[y, x] = CellState.Conductor;
                            }
                            break;
                    }
                }
            }
            return new Grid(nextGrid);
        }

        private static int CountEHNeighbors(Grid grid, int y, int x)
        {
            int EHcount = 0;
            //Hay que recorrer los 8 vecinos según una posición
            // Este for va desde -1 a 1, es decir, revisa -1, 0 y 1 en las filas
            for (int deltaRow = -1; deltaRow <= 1; deltaRow++)
            {
                // Este for va desde -1 a 1, es decir, revisa -1, 0 y 1 en las columnas
                for (int deltaCol = -1; deltaCol <= 1; deltaCol++) 
                {
                    if (deltaRow == 0 && deltaCol == 0) //es decir, se está revisando la misma celda
                    {
                        continue; //no se cuenta la misma celda
                    }

                    // Se obtiene la posición del vecino
                    int neighborRow = deltaRow + y;
                    int neighborCol = deltaCol + x;

                    // Se verifica si el vecino está dentro de los límites de la cuadrícula
                    bool isInBounds =
                        neighborRow >= 0 && neighborRow < grid.Rows && 
                        neighborCol >= 0 && neighborCol < grid.Cols;

                    if (isInBounds && grid[neighborRow, neighborCol] == CellState.ElectronHead)
                    {
                        EHcount++;
                    }
                }
            }

            return EHcount;
        }
    }
}
