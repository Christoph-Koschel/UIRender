using SharpDX.Direct2D1;

namespace UIRender.API;

public abstract class DrawContext {
    private Drawable rootNode;
    public Transform transform => rootNode.transform;

    protected DrawContext(int width, int height) {
        rootNode = new Rect(Vector2.ZERO, new Vector2(width, height), Color.TRANSPARENT);
    }

    public void Draw(RenderTarget target) => rootNode.Draw(target);

    protected void Add(Drawable drawable) => rootNode.children.Add(drawable);
}