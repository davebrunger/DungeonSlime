using Microsoft.Xna.Framework.Input;

namespace MonoGameLibrary.Input;

public record KeyboardInfo(KeyboardState CurrentState)
{
    public KeyboardState PreviousState { get; init; } = CurrentState;

    public bool IsKeyDown(Keys key) => CurrentState.IsKeyDown(key);

    public bool IsKeyUp(Keys key) => CurrentState.IsKeyUp(key);

    public bool WasKeyJustPressed(Keys key) => CurrentState.IsKeyDown(key) && PreviousState.IsKeyUp(key);

    public bool WasKeyJustReleased(Keys key) => CurrentState.IsKeyUp(key) && PreviousState.IsKeyDown(key);
    
    public KeyboardInfo Update(KeyboardState newState)
    {
        return new KeyboardInfo(newState)
        {
            PreviousState = CurrentState
        };
    }
}