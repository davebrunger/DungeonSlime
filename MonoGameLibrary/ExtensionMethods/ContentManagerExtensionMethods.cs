namespace MonoGameLibrary.ExtensionMethods;

public static class ContentManagerExtensionMethods
{
    public static TextureAtlas LoadTextureAtlas(this ContentManager contentManager, string fileName, ITextureAtlasLoader loader)
    {
        string filePath = Path.Combine(contentManager.RootDirectory, fileName);
        using var stream = TitleContainer.OpenStream(filePath);
        return loader.Load(stream, contentManager.Load<Texture2D>);
    }

    public static TileMap LoadTileMap(this ContentManager contentManager, string fileName, ITileMapLoader loader)
    {
        string filePath = Path.Combine(contentManager.RootDirectory, fileName);
        using var stream = TitleContainer.OpenStream(filePath);
        return loader.Load(stream, contentManager.Load<Texture2D>);
    }
}
