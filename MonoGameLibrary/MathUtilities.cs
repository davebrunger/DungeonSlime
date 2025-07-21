namespace MonoGameLibrary;

public static class MathUtilities
{
    public static Vector2 GetRandomUnitVector()
    {
        float angle = (float)(Random.Shared.NextDouble() * Math.PI * 2);
        return ToUnitVector(angle);
    }

    public static Vector2 ToUnitVector(float angleInRadians)
    {
        float x = (float)Math.Cos(angleInRadians);
        float y = (float)Math.Sin(angleInRadians);
        return new Vector2(x, y);
    }
}