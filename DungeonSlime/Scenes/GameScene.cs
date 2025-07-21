namespace DungeonSlime.Scenes;

public class GameScene : Scene
{
    private const float MOVEMENT_SPEED = 5;
    private const float ONE_OVER_ROOT_TWO = 0.70710678118f; // 1 / sqrt(2)

    private AnimatedSprite slime = null!;
    private Vector2 slimePosition = Vector2.Zero;

    private AnimatedSprite bat = null!;
    private Vector2 batPosition = Vector2.Zero;
    private Vector2 batVelocity = Vector2.Zero;

    private TileMap tileMap = null!;
    private Rectangle roomBounds;

    private SoundEffect bounce = null!;
    private SoundEffect collect = null!;

    private SpriteFont font = null!;
    private int score;
    private Vector2 scorePosition;
    private Vector2 scoreOrigin;

    public GameScene(GameBase game) : base(game)
    {
    }

    public override void LoadContent()
    {
        var atlas = Content.LoadTextureAtlas("images/atlas-definition.xml", new XmlTextureAtlasLoader());
        slime = atlas.CreateAnimatedSprite("slime-animation") with { Scale = new Vector2(4, 4) };
        bat = atlas.CreateAnimatedSprite("bat-animation") with { Scale = new Vector2(4, 4) };
        tileMap = Content.LoadTileMap("images/tilemap-definition.xml", new XmlTileMapLoader()) with { Scale = new Vector2(4, 4) };

        bounce = Content.Load<SoundEffect>("audio/bounce");
        collect = Content.Load<SoundEffect>("audio/collect");

        font = Content.Load<SpriteFont>("fonts/04B_30");
    }

    public override void Initialize()
    {
        base.Initialize();

        var screenBounds = GraphicsDevice.GetScreenBounds();
        roomBounds = new Rectangle(
            (int)tileMap.TileWidth,
            (int)tileMap.TileHeight,
            screenBounds.Width - (int)tileMap.TileWidth * 2,
            screenBounds.Height - (int)tileMap.TileHeight * 2
        );

        var centreRow = tileMap.Rows / 2;
        var centreColumn = tileMap.Columns / 2;

        slimePosition = new Vector2(
            centreColumn * tileMap.TileWidth,
            centreRow * tileMap.TileHeight
        );

        batPosition = new Vector2(roomBounds.Left, roomBounds.Top);
        batVelocity = GetRandomUnitVector() * MOVEMENT_SPEED;

        score = 0;
        // Centre Score vertically in tile
        scorePosition = new Vector2(roomBounds.Left, tileMap.TileHeight * 0.5f);
        var scoreOrigin = font.MeasureString("Score").Y * 0.5f;
        this.scoreOrigin = new Vector2(0, scoreOrigin);
    }

    public override void Update(GameTime gameTime)
    {
        slime = slime.Update(gameTime);
        bat = bat.Update(gameTime);

        // Move Slime within screen bounds

        slimePosition += GetDirectionFromKeys() * MOVEMENT_SPEED * GetSpeedFromKeys();

        slimePosition = roomBounds.Constrain(slimePosition, slime.GetBoundingRectangle(slimePosition));

        // Move Bat and reflect off screen bounds

        (batPosition, var newBatVelocity) = roomBounds.Reflect(batPosition, batVelocity, p => bat.GetBoundingRectangle(p));

        if (newBatVelocity != batVelocity)
        {
            batVelocity = newBatVelocity;
            Audio.PlaySoundEffect(bounce);
        }

        // Check for Collision between Slime and Bat

        if (slime.GetBoundingCircle(slimePosition).Intersects(bat.GetBoundingCircle(batPosition)))
        {
            batPosition = GetRandomPosition(tileMap, bat);
            batVelocity = GetRandomUnitVector() * MOVEMENT_SPEED;
            Audio.PlaySoundEffect(collect);
            score += 100;
        }
    }

    public override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        SpriteBatch.Begin();
        SpriteBatch.Draw(tileMap);
        SpriteBatch.Draw(slime, slimePosition);
        SpriteBatch.Draw(bat, batPosition);

        SpriteBatch.DrawString(
            font,               // spriteFont
            $"Score: {score}",  // text
            scorePosition,      // position
            Color.White,        // color
            0.0f,               // rotation
            scoreOrigin,        // origin
            1.0f,               // scale
            SpriteEffects.None, // effects
            0.0f                // layerDepth
        );

        SpriteBatch.End();

        base.Draw(gameTime);
    }

    private float GetSpeedFromKeys()
    {
        return Input.KeyboardInfo.IsKeyDown(Keys.Space) ? 2f : 1f;
    }

    private Vector2 GetDirectionFromKeys()
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

    private static Vector2 GetRandomUnitVector()
    {
        float angle = (float)(Random.Shared.NextDouble() * Math.PI * 2);
        return ToUnitVector(angle);
    }

    private static Vector2 ToUnitVector(float angleInRadians)
    {
        float x = (float)Math.Cos(angleInRadians);
        float y = (float)Math.Sin(angleInRadians);
        return new Vector2(x, y);
    }

    private static Vector2 GetRandomPosition(TileMap tileMap, Sprite sprite)
    {
        int column = Random.Shared.Next(1, tileMap.Columns - 1);
        int row = Random.Shared.Next(1, tileMap.Rows - 1);

        return new Vector2(column * sprite.Width, row * sprite.Height);
    }
}