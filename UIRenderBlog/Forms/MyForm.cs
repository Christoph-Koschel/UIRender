#define RUN_ANIMATION

using UIRender.API;

namespace UIRender.Forms;


public sealed class MyForm : Form {
    private Rect outerRect;
    private Rect innerRect;

    public MyForm() : base(800, 600) {

    }

    public override void Initialize() {
        outerRect = new Rect(350, 250, 100, 100, Color.FromHEX(0x881BBD));

        innerRect = new Rect(0, 0, 50, 50, Color.FromHEX(0x6DADDF));
        outerRect.children.Add(innerRect);

        Add(outerRect);
    }

    public override void Update() {
#if RUN_ANIMATION
        outerRect.transform.position.x++;
        if (outerRect.transform.position.x > transform.size.x) {
            outerRect.transform.position.x = 0;
        }

        outerRect.transform.position.y++;
        if (outerRect.transform.position.y > transform.size.y) {
            outerRect.transform.position.y = 0;
        }
#endif
    }
}