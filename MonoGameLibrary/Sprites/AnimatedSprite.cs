namespace MonoGameLibrary.Sprites;

public record AnimatedSprite(Animation Animation) : Sprite(Animation.Frames[0])
{
    public int Frame { get; init; } = 0;

    public TimeSpan Elapsed { get; init; } = TimeSpan.Zero;

    public AnimatedSprite Update(GameTime gameTime)
    {
        var elapsed = Elapsed + gameTime.ElapsedGameTime;

        if (elapsed < Animation.Delay)
        {
            return this with { Elapsed = elapsed };
        }

        var frame = (Frame + 1) % Animation.Frames.Count;

        return this with 
        {
            Elapsed = elapsed - Animation.Delay,
            Frame = frame,
            Region = Animation.Frames[frame]
        };
    }
}