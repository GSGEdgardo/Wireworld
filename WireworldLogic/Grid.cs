namespace Wireworld.WireworldLogic
{
    internal class Grid
    {
        // Es preferible usar un array 2D en vez de una lista de listas,
        // ya que es más eficiente para matrices de tamaño estáticas
        private CellState[,] grid;
        private int Rows => grid.GetLength(0);
        private int Cols => grid.GetLength(1);
    }
}
