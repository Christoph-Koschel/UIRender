using SharpDX.Direct2D1;
using SharpDX.Mathematics.Interop;

namespace UIRender.API;

public class Rect : Drawable {
    public Color backgroundColor;

    public Rect(int x, int y, int width, int height, Color backgroundColor)
        : this(new Vector2(x, y), new Vector2(width, height), backgroundColor) {
    }

    public Rect(Vector2 position, Vector2 size, Color backgroundColor)
        : base(position, size) {
        this.backgroundColor = backgroundColor;
    }

    public override void Draw(RenderTarget target) {
        target.BeginDraw();
        SolidColorBrush brush = new SolidColorBrush(target, backgroundColor.ToRaw());

        Vector2 p1 = transform.AbsolutePosition();
        Vector2 p2 = p1 + transform.size;

        target.FillRectangle(new RawRectangleF(p1.x, p1.y, p2.x, p2.y), brush);
        target.EndDraw();
        brush.Dispose();

        foreach (Drawable child in children) {
            child.Draw(target);
        }
    }
}