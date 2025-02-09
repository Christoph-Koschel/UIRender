namespace UIRender.API;

public struct Vector2 {
    public int x;
    public int y;

    public Vector2(int x, int y) {
        this.x = x;
        this.y = y;
    }

    public static Vector2 operator +(Vector2 a, Vector2 b) {
        return new Vector2(a.x + b.x, a.y + b.y);
    }

    public static Vector2 operator -(Vector2 a, Vector2 b) {
        return new Vector2(a.x - b.x, a.y - b.y);
    }

    public static readonly Vector2 ZERO = new Vector2(0, 0);
}