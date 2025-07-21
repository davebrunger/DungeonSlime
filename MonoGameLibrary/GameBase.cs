using MonoGameLibrary.Scenes;

namespace MonoGameLibrary;

public class GameBase : Game
{
    private Scene? activeScene;

    private Scene? nextScene;

    public SpriteBatch SpriteBatch { get; private set; } = null!;

    public InputManager Input { get; private set; } = null!;

    public bool ExitOnEscape { get; set; }

    public AudioController Audio { get; private set; } = null!;

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

        Audio = new AudioController();
    }

    protected override void UnloadContent()
    {
        Audio.Dispose();
        base.UnloadContent();
    }

    protected override void Update(GameTime gameTime)
    {
        Input = Input.Update(Keyboard.GetState());

        Audio.Update();

        if (ExitOnEscape && Input.KeyboardInfo.IsKeyDown(Keys.Escape))
        {
            Exit();
        }

        if (nextScene != null)
        {
            TransitionScene();
        }

        activeScene?.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        activeScene?.Draw(gameTime);

        base.Draw(gameTime);
    }

    public void ChangeScene(Scene scene)
    {
        nextScene = scene;
    }

    private void TransitionScene()
    {
        activeScene?.Dispose();

        GC.Collect();

        activeScene = nextScene;
        nextScene = null;

        activeScene?.Initialize();
    }
}
