using System.Collections;
using System.Collections.Generic;

namespace UIRender.API;

public sealed class RenderTree : IEnumerable<Drawable> {
    private Drawable drawable;
    private List<Drawable> drawables;

    private int length => drawables.Count;

    public RenderTree(Drawable drawable) {
        this.drawable = drawable;
        this.drawables = new List<Drawable>();
    }

    public void Add(Drawable drawable) {
        drawable.parent = this.drawable;
        drawable.transform.parent = this.drawable.transform;
        drawables.Add(drawable);
    }

    public IEnumerator<Drawable> GetEnumerator() {
        return drawables.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return GetEnumerator();
    }
}