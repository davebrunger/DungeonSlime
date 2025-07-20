using Microsoft.Xna.Framework.Input;

namespace MonoGameLibrary.Input;

public record InputManager(KeyboardInfo KeyboardInfo)
{
    public InputManager Update(KeyboardState keyboardState)
    {
        return new InputManager(KeyboardInfo.Update(keyboardState));
    }
}
