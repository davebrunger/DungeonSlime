namespace MonoGameLibrary.Tiles;

public record TileMap(TileSet TileSet, IImmutableDictionary<(int x, int y), int> Tiles)
{
    public Vector2 Scale { get; init; } = Vector2.One;
    public int Rows { get; } = Tiles.Keys.Max(t => t.y) + 1;
    public int Columns { get; } = Tiles.Keys.Max(t => t.x) + 1;
    public float TileWidth => TileSet.TileWidth * Scale.X;
    public float TileHeight => TileSet.TileHeight * Scale.Y;

    public TextureRegion GetTile(int column, int row)
    {
        return TileSet.Tiles[Tiles[(column, row)]];
    }
}