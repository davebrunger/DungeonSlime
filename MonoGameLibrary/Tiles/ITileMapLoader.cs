namespace   MonoGameLibrary.Tiles;

public interface ITileMapLoader
{
    TileMap Load(Stream stream, Func<string, Texture2D> loadTexture);
}