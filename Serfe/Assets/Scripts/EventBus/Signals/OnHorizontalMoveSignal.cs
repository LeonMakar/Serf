public class OnHorizontalMoveSignal
{
    public readonly MovementType MovementType;
    public OnHorizontalMoveSignal(MovementType movementType)
    {
        MovementType = movementType;
    }
}
public enum MovementType
{
    Left,
    Right,
    Up,
    Down,
}
