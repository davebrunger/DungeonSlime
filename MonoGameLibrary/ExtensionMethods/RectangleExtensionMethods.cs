namespace MonoGameLibrary.ExtensionMethods;

public static class RectangleExtensionMethods
{
    public static Vector2 Constrain(this Rectangle container, Vector2 position, Rectangle containedRectangle)
    {
        var x = containedRectangle.Left < container.Left ? container.Left : 
                (containedRectangle.Right > container.Right ? container.Right - containedRectangle.Width : position.X);
        var y = containedRectangle.Top < container.Top ? container.Top :
                (containedRectangle.Bottom > container.Bottom ? container.Bottom - containedRectangle.Height : position.Y);
        return new Vector2(x, y);
    }

    public static (Vector2 NewPosition, Vector2 Normal) Reflect(this Rectangle container, Vector2 position, Vector2 velocity, Func<Vector2, Rectangle> getContainedRectangle)
    {
        var newPosition = position + velocity;
        var containedRectangle = getContainedRectangle(newPosition);

        var (x, normalX) = containedRectangle.Left < container.Left ? (container.Left, Vector2.UnitX.X) :
                (containedRectangle.Right > container.Right ? (container.Right - containedRectangle.Width, -Vector2.UnitX.X) : (newPosition.X, Vector2.Zero.X));
        var (y, normalY) = containedRectangle.Top < container.Top ? (container.Top, Vector2.UnitY.Y) :
                (containedRectangle.Bottom > container.Bottom ? (container.Bottom - containedRectangle.Height, -Vector2.UnitY.Y) : (newPosition.Y, Vector2.Zero.Y));

        var normal = new Vector2(normalX, normalY);
        var newVelocity = normal == Vector2.Zero ? velocity : Vector2.Reflect(velocity, normal);
 
        return (new Vector2(x, y), newVelocity);
    }
}
