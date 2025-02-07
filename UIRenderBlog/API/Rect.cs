namespace UIRender.API;

public class Rect {
    public Vector2 position;
    public Vector2 size;
    public Color backgroundColor;

    public Rect(int x, int y, int width, int height, Color backgroundColor)
        : this(new Vector2(x, y), new Vector2(width, height), backgroundColor) {
    }

    public Rect(Vector2 position, Vector2 size, Color backgroundColor) {
        this.position = position;
        this.size = size;
        this.backgroundColor = backgroundColor;
    }
}