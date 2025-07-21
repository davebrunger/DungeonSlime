namespace DungeonSlime.Scenes;

public class TitleScene : Scene
{
    private const string DUNGEON_TEXT = "Dungeon";
    private const string SLIME_TEXT = "Slime";
    private const string PRESS_ENTER_TEXT = "Press Enter to Start";

    private SpriteFont font = null!;
    private SpriteFont font5x = null!;
    private Vector2 dungeonTextPosition;
    private Vector2 dungeonTextOrigin;
    private Vector2 slimeTextPosition;
    private Vector2 slimeTextOrigin;
    private Vector2 pressEnterTextPosition;
    private Vector2 pressEnterTextOrigin;

    private Texture2D background = null!;
    private Rectangle backgroundDestination;
    private Vector2 backgroundOffset;
    private float scrollSpeed = 50;

    public TitleScene(GameBase game) : base(game)
    {
        game.ExitOnEscape = true;
    }

    public override void Initialize()
    {
        base.Initialize();

        var size = font5x.MeasureString(DUNGEON_TEXT);
        dungeonTextPosition = new Vector2(640, 100);
        dungeonTextOrigin = size / 2;

        size = font5x.MeasureString(SLIME_TEXT);
        slimeTextPosition = new Vector2(757, 207);
        slimeTextOrigin = size / 2;

        size = font.MeasureString(PRESS_ENTER_TEXT);
        pressEnterTextPosition = new Vector2(640, 620);
        pressEnterTextOrigin = size / 2;

        backgroundOffset = Vector2.Zero;
        backgroundDestination = GraphicsDevice.PresentationParameters.Bounds;
    }

    public override void LoadContent()
    {
        font = Content.Load<SpriteFont>("Fonts/04B_30");
        font5x = Content.Load<SpriteFont>("Fonts/04B_30_5x");
        background = Content.Load<Texture2D>("Images/background-pattern");
    }

    public override void Update(GameTime gameTime)
    {
        if (Input.KeyboardInfo.IsKeyDown(Keys.Enter))
        {
            ChangeScene(game => new GameScene(game));
        }

        var offset = scrollSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        backgroundOffset = new Vector2(backgroundOffset.X - offset, backgroundOffset.Y - offset);
        backgroundOffset = new Vector2(backgroundOffset.X % background.Width, backgroundOffset.Y % background.Height);
    }

    public override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(new Color(32, 40, 78, 255));

        SpriteBatch.Begin(samplerState: SamplerState.PointWrap);
        SpriteBatch.Draw(background, backgroundDestination, new Rectangle(backgroundOffset.ToPoint(), backgroundDestination.Size), Color.White);
        SpriteBatch.End();

        SpriteBatch.Begin(samplerState: SamplerState.PointClamp);
        Color dropShadowColor = Color.Black * 0.5f;
        SpriteBatch.DrawString(font5x, DUNGEON_TEXT, dungeonTextPosition + new Vector2(10, 10), dropShadowColor, 0.0f, dungeonTextOrigin, 1.0f, SpriteEffects.None, 1.0f);
        SpriteBatch.DrawString(font5x, DUNGEON_TEXT, dungeonTextPosition, Color.White, 0.0f, dungeonTextOrigin, 1.0f, SpriteEffects.None, 1.0f);
        SpriteBatch.DrawString(font5x, SLIME_TEXT, slimeTextPosition + new Vector2(10, 10), dropShadowColor, 0.0f, slimeTextOrigin, 1.0f, SpriteEffects.None, 1.0f);
        SpriteBatch.DrawString(font5x, SLIME_TEXT, slimeTextPosition, Color.White, 0.0f, slimeTextOrigin, 1.0f, SpriteEffects.None, 1.0f);
        SpriteBatch.DrawString(font, PRESS_ENTER_TEXT, pressEnterTextPosition, Color.White, 0.0f, pressEnterTextOrigin, 1.0f, SpriteEffects.None, 0.0f);
        SpriteBatch.End();
    }
}