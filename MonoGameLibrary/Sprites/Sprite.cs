namespace MonoGameLibrary.Sprites;

public record Sprite(TextureRegion Region)
{
    public Color Color { get; init; } = Color.White;

    public float Rotation { get; init; } = 0f;

    public Vector2 Scale { get; init; } = Vector2.One;

    public Vector2 Origin { get; init; } = Vector2.Zero;

    public SpriteEffects Effects { get; init; } = SpriteEffects.None;

    public float LayerDepth { get; init; } = 0f;

    public float Width => Region.Width * Scale.X;

    public float Height => Region.Height * Scale.Y;

    public Sprite WithCenteredOrigin()
    {
        return this with
        {
            Origin = new Vector2(Region.Width, Region.Height) * 0.5f
        };
    }

    /// <summary>
    /// Gets the bounding circle for the sprite at a given position.
    /// </summary>
    /// <param name="position">Position of the sprite.</param>
    /// <returns>A <see cref="Circle"/> representing the bounding circle.</returns>
    /// <remarks>Assumes the sprite's origin is at the top-left corner.</remarks>
    public Circle GetBoundingCircle(Vector2 position) 
    {
        return new Circle(
            (int)(position.X + Width * 0.5f),
            (int)(position.Y + Height * 0.5f),
            (int)(Math.Max(Width, Height) * 0.5f)
        );
    }  

    /// <summary>
    /// Gets the bounding circle for the sprite at a given position.
    /// </summary>
    /// <param name="position">Position of the sprite.</param>
    /// <returns>A <see cref="Circle"/> representing the bounding circle.</returns>
    /// <remarks>Assumes the sprite's origin is at the top-left corner.</remarks>
    public Rectangle GetBoundingRectangle(Vector2 position) 
    {
        return new Rectangle(
            (int)position.X,
            (int)position.Y,
            (int)Width,
            (int)Height
        );
    }  
}