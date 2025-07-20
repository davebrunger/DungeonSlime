namespace MonoGameLibrary.Tiles;

public class XmlTileMapLoader : ITileMapLoader
{
    public TileMap Load(Stream stream, Func<string, Texture2D> loadTexture)
    {
        using var reader = XmlReader.Create(stream);
        var document = XDocument.Load(reader);
        var root = document.Root!;

        var tileSetElement = root.Element("TileSet")!;
        var region = tileSetElement.Attribute("region")!.Value;
        var bits = region.Split(" ", StringSplitOptions.RemoveEmptyEntries);
        var x = int.Parse(bits[0]);
        var y = int.Parse(bits[1]);
        var width = int.Parse(bits[2]);
        var height = int.Parse(bits[3]);

        var tileWidth = int.Parse(tileSetElement.Attribute("tileWidth")!.Value);
        var tileHeight = int.Parse(tileSetElement.Attribute("tileHeight")!.Value);
        var content = tileSetElement.Value;

        var texture = loadTexture(content);
        var textureRegion = new TextureRegion(texture, new Rectangle(x, y, width, height));
        var tileSet = new TileSet(textureRegion, tileWidth, tileHeight);

        var tiles = root.Element("Tiles")!;
        var rows = tiles.Value.Trim().Split("\n", StringSplitOptions.RemoveEmptyEntries);
 
        var tileMap = Enumerable.Range(0, rows.Length)
            .SelectMany(row =>
            {
                var columns = rows[row].Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries);
                return Enumerable.Range(0, columns.Length)
                    .Select(column =>
                    {
                        var tileIndex = int.Parse(columns[column]);
                        return (Key: (column, row), Value: tileIndex);
                    });
            })
            .ToImmutableDictionary(t => t.Key, t => t.Value);

        return new TileMap(tileSet, tileMap);
    }
}