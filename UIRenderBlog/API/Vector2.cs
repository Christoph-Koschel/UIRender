namespace UIRender.API;

public struct Vector2 {
    public int x;
    public int y;

    public Vector2(int x, int y) {
        this.x = x;
        this.y = y;
    }

    public static readonly Vector2 ZERO = new Vector2(0, 0);
}