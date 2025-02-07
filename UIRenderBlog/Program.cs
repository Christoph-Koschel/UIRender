using System;
using System.Drawing;
using SharpDX.Direct2D1;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.Mathematics.Interop;
using SharpDX.Windows;
using UIRender.API;
using AlphaMode = SharpDX.Direct2D1.AlphaMode;
using Color = UIRender.API.Color;
using Device = SharpDX.Direct3D11.Device;
using Factory = SharpDX.Direct2D1.Factory;
using Factory1 = SharpDX.DXGI.Factory1;

namespace UIRender;

public static class Program {
    public static void Main(string[] args) {
        Console.WriteLine("Initialize Application Window");
        RenderForm window = new RenderForm("Application Window");
        window.ClientSize = new Size(800, 600);

        Console.WriteLine("Initialize GPU acceleration");
        Device device = new Device(DriverType.Hardware, DeviceCreationFlags.BgraSupport);
        SwapChainDescription swapChainDescription = new SwapChainDescription() {
            BufferCount = 2,
            ModeDescription = new ModeDescription(window.ClientSize.Width, window.ClientSize.Height, new Rational(60, 1), Format.B8G8R8A8_UNorm),
            Usage = Usage.RenderTargetOutput,
            OutputHandle = window.Handle,
            SampleDescription = new SampleDescription(1, 0),
            IsWindowed = true,
            Flags = SwapChainFlags.AllowModeSwitch
        };
        SwapChain swapChain = new SwapChain(new Factory1(), device, swapChainDescription);

        Console.WriteLine("Initialize 2D Factory");
        Factory d2dFactory = new Factory();
        Texture2D backBuffer = Texture2D.FromSwapChain<Texture2D>(swapChain, 0);
        Surface surface = backBuffer.QueryInterface<Surface>();
        RenderTarget renderTarget = new RenderTarget(d2dFactory, surface, new RenderTargetProperties(new PixelFormat(Format.Unknown, AlphaMode.Premultiplied)));

        Rect rect = new Rect(350, 250, 100, 100, Color.FromHEX(0xA211BF));

        Console.WriteLine("Run render loop");
        RenderLoop.Run(window, () => {
            renderTarget.BeginDraw();
            SolidColorBrush brush = new SolidColorBrush(renderTarget, rect.backgroundColor.ToRaw());
            Vector2 pos1 = rect.position;
            Vector2 pos2 = new Vector2(pos1.x + rect.size.x, pos1.y + rect.size.y);
            renderTarget.FillRectangle(new RawRectangleF(pos1.x, pos1.y, pos2.x, pos2.y), brush);
            renderTarget.EndDraw();

            swapChain.Present(1, PresentFlags.None);
            brush.Dispose();
        });

        Console.WriteLine("Free resources");
        renderTarget.Dispose();
        swapChain.Dispose();
        device.Dispose();
    }
}