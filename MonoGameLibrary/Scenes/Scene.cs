namespace MonoGameLibrary.Scenes;

public abstract class Scene : IDisposable
{
    private const float ONE_OVER_ROOT_TWO = 0.70710678118f; // 1 / sqrt(2)

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

    protected Vector2 GetDirectionFromKeys()
    {
        var direction = Vector2.Zero;

        if (Input.KeyboardInfo.IsKeyDown(Keys.W) || Input.KeyboardInfo.IsKeyDown(Keys.Up))
        {
            direction.Y -= 1;
        }

        if (Input.KeyboardInfo.IsKeyDown(Keys.S) || Input.KeyboardInfo.IsKeyDown(Keys.Down))
        {
            direction.Y += 1;
        }

        if (Input.KeyboardInfo.IsKeyDown(Keys.A) || Input.KeyboardInfo.IsKeyDown(Keys.Left))
        {
            direction.X -= 1;
        }

        if (Input.KeyboardInfo.IsKeyDown(Keys.D) || Input.KeyboardInfo.IsKeyDown(Keys.Right))
        {
            direction.X += 1;
        }

        return direction.X == 0 || direction.Y == 0
            ? new Vector2(direction.X, direction.Y)
            : new Vector2(direction.X * ONE_OVER_ROOT_TWO, direction.Y * ONE_OVER_ROOT_TWO);
    }
}
