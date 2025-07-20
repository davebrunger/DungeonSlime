namespace MonoGameLibrary.ExtensionMethods;

public static class GraphicsDeviceExtensionMethods
{
    public static Rectangle GetScreenBounds(this GraphicsDevice graphicsDevice)
    {
        return new Rectangle(0, 0, graphicsDevice.PresentationParameters.BackBufferWidth, graphicsDevice.PresentationParameters.BackBufferHeight);
    }
}
