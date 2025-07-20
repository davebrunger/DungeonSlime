namespace MonoGameLibrary.Shapes;

public record Circle(int X, int Y, int Radius)
{
    public Point Location => new(X, Y);

    public static Circle Empty { get; } = new Circle(0, 0, 0);

    public int Top => Y - Radius;

    public int Bottom => Y + Radius;

    public int Left => X - Radius;

    public int Right => X + Radius;

    public Circle(Point location, int radius) : this(location.X, location.Y, radius)
    {
    }

    public Rectangle GetBoundingRectangle()
    {
        return new Rectangle(Left, Top, Radius * 2, Radius * 2);
    }

    public bool Intersects(Circle other)
    {
        int radiiSquared = (Radius + other.Radius) * (Radius + other.Radius);
        float distanceSquared = Vector2.DistanceSquared(Location.ToVector2(), other.Location.ToVector2());
        return distanceSquared < radiiSquared;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y, Radius);
    }
}
