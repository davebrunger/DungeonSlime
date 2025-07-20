namespace MonoGameLibrary.Tiles;

public class TileSet
{
    public ImmutableList<TextureRegion> Tiles { get; }
    public int TileWidth { get; }
    public int TileHeight { get; }
    public int Columns { get; }
    public int Rows { get; }
    public int Count => Columns * Rows;

    public TileSet(TextureRegion texture, int tileWidth, int tileHeight)
    {
        TileWidth = tileWidth;
        TileHeight = tileHeight;
        Columns = texture.Width / TileWidth;
        Rows = texture.Height / TileHeight;

        Tiles = Enumerable.Range(0, Count)
            .Select(i =>
            {
                var x = i % Columns * TileWidth;
                var y = i / Columns * TileHeight;
                var sourceRectangle = new Rectangle(texture.SourceRectangle.X + x, texture.SourceRectangle.Y + y, TileWidth, TileHeight);
                return new TextureRegion(texture.Texture, sourceRectangle);
            })
            .ToImmutableList();
    }

    public TextureRegion GetTile(int column, int row) => Tiles[row * Columns + column];
}
