
namespace DungeonSlime;

public class Game1 : GameBase
{
    private Song theme = null!;

    public Game1() : base("Dungeon Slime", 1280, 720, false, true)
    {
    }

    protected override void LoadContent()
    {
        theme = Content.Load<Song>("audio/theme");
    }

    protected override void Initialize()
    {
        base.Initialize();
        Audio.PlaySong(theme);
        ChangeScene(new Scenes.TitleScene(this));
    }
}
