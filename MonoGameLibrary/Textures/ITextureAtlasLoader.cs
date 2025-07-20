namespace MonoGameLibrary.Textures;

public interface ITextureAtlasLoader
{
    TextureAtlas Load(Stream stream, Func<string, Texture2D> loadTexture);
}