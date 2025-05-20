namespace Wireworld.WireworldLogic
{
    enum CellState
    {
        Empty, // Empty -> Empty
        ElectronHead, // Electron Head -> Electron Tail
        ElectronTail, // Electron Tail -> Conductor
        // Conductor -> Electron Head si tiene exactamente 1 o 2 vecinos ElectronHead
        Conductor
    }
}
