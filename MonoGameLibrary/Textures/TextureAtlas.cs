namespace MonoGameLibrary.Textures;

public record TextureAtlas(Texture2D Texture, ImmutableDictionary<string, TextureRegion> Regions, ImmutableDictionary<string, Animation> Animations)
{
    public Sprite CreateSprite(string regionName)
    {
        return new Sprite(Regions[regionName]);
    }

    public AnimatedSprite CreateAnimatedSprite(string animationName)
    {
        return new AnimatedSprite(Animations[animationName]);
    }
}
