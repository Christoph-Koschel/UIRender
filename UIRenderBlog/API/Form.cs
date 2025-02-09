namespace UIRender.API;

public abstract class Form : DrawContext {
    protected Form(int width, int height) : base(width, height) {

    }

    public abstract void Initialize();

    public abstract void Update();
}