namespace MonoGameLibrary;

public class GameBase : Game
{
    public SpriteBatch SpriteBatch { get; private set; } = null!;

    public InputManager Input { get; private set; } = null!;

    public bool ExitOnEscape { get; }

    public GameBase(string title, int width, int height, bool fullScreen, bool exitOnEscape)
    {
        ExitOnEscape = exitOnEscape;

        new GraphicsDeviceManager(this)
        {
            PreferredBackBufferWidth = width,
            PreferredBackBufferHeight = height,
            IsFullScreen = fullScreen
        }.ApplyChanges();

        Window.Title = title;
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        base.Initialize();

        SpriteBatch = new SpriteBatch(GraphicsDevice);

        Input = new InputManager(new KeyboardInfo(Keyboard.GetState()));
    }

    protected override void Update(GameTime gameTime)
    {
        Input = Input.Update(Keyboard.GetState());

        if (ExitOnEscape && Input.KeyboardInfo.IsKeyDown(Keys.Escape))
        {
            Exit();
        }

        base.Update(gameTime);
    }
}
