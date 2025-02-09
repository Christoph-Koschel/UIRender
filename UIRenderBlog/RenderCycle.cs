using System;
using System.Drawing;
using SharpDX.Direct2D1;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.Windows;
using AlphaMode = SharpDX.Direct2D1.AlphaMode;
using Color = UIRender.API.Color;
using Device = SharpDX.Direct3D11.Device;
using Factory = SharpDX.Direct2D1.Factory;
using Factory1 = SharpDX.DXGI.Factory1;
using Form = UIRender.API.Form;

namespace UIRender;

public sealed class RenderCycle
{
    public static void Run(Form form) {
        RenderCycle cycle = new RenderCycle(form);
        cycle.Run();
        cycle.Dispose();
    }

    private Form form;
    private RenderForm window;
    private Device device;
    private SwapChain swapChain;
    private Factory d2dFactory;
    private RenderTarget renderTarget;

    private RenderCycle(Form form) {
        Console.WriteLine("Initialize RenderCycle");
        this.form = form;
        InitApplicationWindow();
        InitGPUAcceleration();
        Init2DFactory();
        form.Initialize();
    }

    private void InitApplicationWindow() {
        Console.WriteLine("Initialize Application Window");
        window = new RenderForm("Application Window");
        window.ClientSize = new Size(form.transform.size.x, form.transform.size.y);
    }

    private void InitGPUAcceleration() {
        Console.WriteLine("Initialize GPU acceleration");
        device = new Device(DriverType.Hardware, DeviceCreationFlags.BgraSupport);
        SwapChainDescription swapChainDescription = new SwapChainDescription() {
            BufferCount = 2,
            ModeDescription = new ModeDescription(window.ClientSize.Width, window.ClientSize.Height, new Rational(60, 1), Format.B8G8R8A8_UNorm),
            Usage = Usage.RenderTargetOutput,
            OutputHandle = window.Handle,
            SampleDescription = new SampleDescription(1, 0),
            IsWindowed = true,
            Flags = SwapChainFlags.AllowModeSwitch
        };
        swapChain = new SwapChain(new Factory1(), device, swapChainDescription);
    }

    private void Init2DFactory() {
        Console.WriteLine("Initialize 2D Factory");
        d2dFactory = new Factory();
        Texture2D backBuffer = Texture2D.FromSwapChain<Texture2D>(swapChain, 0);
        Surface surface = backBuffer.QueryInterface<Surface>();
        renderTarget = new RenderTarget(d2dFactory, surface, new RenderTargetProperties(new PixelFormat(Format.Unknown, AlphaMode.Premultiplied)));
    }

    private void Run() {
        Console.WriteLine("Run render loop");
        RenderLoop.Run(window, () => {
            form.Update();
            renderTarget.BeginDraw();
            renderTarget.Clear(Color.BLACK.ToRaw());
            renderTarget.EndDraw();
            form.Draw(renderTarget);
            swapChain.Present(1, PresentFlags.None);
        });
    }

    private void Dispose() {
        renderTarget.Dispose();
        swapChain.Dispose();
        device.Dispose();
    }
}