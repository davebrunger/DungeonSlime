namespace MonoGameLibrary.Textures;

public record Animation(ImmutableList<TextureRegion> Frames, TimeSpan Delay);