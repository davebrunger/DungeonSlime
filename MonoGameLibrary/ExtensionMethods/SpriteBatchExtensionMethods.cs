using MonoGameLibrary.Tiles;

namespace MonoGameLibrary.ExtensionMethods;

public static class SpriteBatchExtensionMethods
{
    public static void Draw(this SpriteBatch spriteBatch, TextureRegion textureRegion, Vector2 position, Color color)
    {
        spriteBatch.Draw(textureRegion.Texture, position, textureRegion.SourceRectangle, color);
    }

    public static void Draw(this SpriteBatch spriteBatch, TextureRegion textureRegion, Vector2 position, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects = SpriteEffects.None, float layerDepth = 0f)
    {
        spriteBatch.Draw(textureRegion.Texture, position, textureRegion.SourceRectangle, color, rotation, origin, scale, effects, layerDepth);
    }

    public static void Draw(this SpriteBatch spriteBatch, TextureRegion textureRegion, Vector2 position, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effects = SpriteEffects.None, float layerDepth = 0f)
    {
        spriteBatch.Draw(textureRegion.Texture, position, textureRegion.SourceRectangle, color, rotation, origin, scale, effects, layerDepth);
    }

    public static void Draw(this SpriteBatch spriteBatch, Sprite sprite, Vector2 position)
    {
        spriteBatch.Draw(sprite.Region, position, sprite.Color, sprite.Rotation, sprite.Origin, sprite.Scale, sprite.Effects, sprite.LayerDepth);
    }

    public static void Draw(this SpriteBatch spriteBatch, TileMap tileMap)
    {
        foreach (var tile in tileMap.Tiles)
        {
            var position = new Vector2(tile.Key.x * tileMap.TileWidth, tile.Key.y * tileMap.TileHeight);
            var textureRegion = tileMap.GetTile(tile.Key.x, tile.Key.y);
            spriteBatch.Draw(textureRegion, position, Color.White, 0, Vector2.Zero, tileMap.Scale, SpriteEffects.None, 1);
        }
    }
}
