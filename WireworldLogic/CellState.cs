namespace Wireworld.WireworldLogic
{
    enum CellState
    {
        Empty, // Empty -> Empty -> color negro
        ElectronHead, // Electron Head -> Electron Tail -> color amarillo
        ElectronTail, // Electron Tail -> Conductor -> color rojo
        // Conductor -> Electron Head si tiene exactamente 1 o 2 vecinos ElectronHead -> color azul
        Conductor
    }
}
