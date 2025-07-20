namespace MonoGameLibrary.Textures;

public record TextureRegion(Texture2D Texture, Rectangle SourceRectangle)
{
    public int Width => SourceRectangle.Width;
    public int Height => SourceRectangle.Height;
}