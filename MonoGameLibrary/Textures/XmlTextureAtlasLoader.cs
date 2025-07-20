namespace MonoGameLibrary.Textures;

public class XmlTextureAtlasLoader : ITextureAtlasLoader
{
    public TextureAtlas Load(Stream stream, Func<string, Texture2D> loadTexture)
    {
        using var reader = XmlReader.Create(stream);
        var document = XDocument.Load(reader);
        var root = document.Root!;

        var texture = loadTexture(root.Element("Texture")!.Value);

        var regions = root.Element("Regions")!.Elements("Region")
            .ToImmutableDictionary(
                region => region.Attribute("name")!.Value,
                region => new TextureRegion(texture, new Rectangle(
                    int.Parse(region.Attribute("x")!.Value),
                    int.Parse(region.Attribute("y")!.Value),
                    int.Parse(region.Attribute("width")!.Value),
                    int.Parse(region.Attribute("height")!.Value)
                )
            ));

        var animations = root.Element("Animations")!.Elements("Animation")!
            .ToImmutableDictionary(
                animation => animation.Attribute("name")!.Value,
                animation => new Animation(
                    animation.Elements("Frame")
                        .Select(frame => regions[frame.Attribute("region")!.Value])
                        .ToImmutableList(),
                    TimeSpan.FromMilliseconds(float.Parse(animation.Attribute("delay")?.Value ?? "0"))
                ));

        return new TextureAtlas(
            texture,
            regions,
            animations
        );
    }
}