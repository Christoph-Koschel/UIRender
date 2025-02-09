using SharpDX.Direct2D1;

namespace UIRender.API;

public abstract class Drawable {
    public Transform transform;
    public Drawable parent;
    public RenderTree children;

    protected Drawable(Drawable parent, Vector2 position, Vector2 size) {
        this.parent = parent;
        this.transform = new Transform(position, size);
        this.children = new RenderTree(this);
    }

    protected Drawable(Vector2 position, Vector2 size)
        : this(null, position, size) {
    }

    public abstract void Draw(RenderTarget target);
}