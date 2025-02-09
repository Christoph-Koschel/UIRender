namespace UIRender.API;

public sealed class Transform {
    public Transform parent;
    public Vector2 position;
    public Vector2 size;

    public Transform(Transform parent, Vector2 position, Vector2 size) {
        this.parent = parent;
        this.position = position;
        this.size = size;
    }

    public Transform(Vector2 position, Vector2 size)
        : this(null, position, size) {
    }

    public Vector2 AbsolutePosition() {
        if (parent != null) {
            return parent.AbsolutePosition() + position;
        }

        return position;
    }
}