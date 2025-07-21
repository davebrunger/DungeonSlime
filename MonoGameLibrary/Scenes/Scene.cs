namespace MonoGameLibrary.Scenes;

public abstract class Scene : IDisposable
{
    private readonly GameBase game;

    protected ContentManager Content { get; }

    public bool IsDisposed { get; private set; }

    protected InputManager Input => game.Input;

    protected GraphicsDevice GraphicsDevice => game.GraphicsDevice;

    protected SpriteBatch SpriteBatch => game.SpriteBatch;

    protected AudioController Audio => game.Audio;

    public Scene(GameBase game)
    {
        this.game = game;
        Content = new ContentManager(game.Content.ServiceProvider)
        {
            RootDirectory = game.Content.RootDirectory
        };
    }

    ~Scene()
    {
        Dispose(false);
    }

    public virtual void Initialize()
    {
        LoadContent();
    }

    public virtual void LoadContent()
    {
    }

    public virtual void UnloadContent()
    {
        Content.Unload();
    }

    public virtual void Update(GameTime gameTime)
    {
    }

    public virtual void Draw(GameTime gameTime)
    {
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (IsDisposed)
        {
            return;
        }

        if (disposing)
        {
            UnloadContent();
            Content.Dispose();
        }

        IsDisposed = true;
    }
    
    protected void ChangeScene(Func<GameBase, Scene> createScene)
    {
        game.ChangeScene(createScene(game));
    }
}
