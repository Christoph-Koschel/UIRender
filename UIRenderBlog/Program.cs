using UIRender.Forms;

namespace UIRender;

public static class Program {
    public static void Main(string[] args) {
        RenderCycle.Run(new MyForm());
    }
}