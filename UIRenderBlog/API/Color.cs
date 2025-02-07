using SharpDX.Mathematics.Interop;

namespace UIRender.API;

public sealed class Color {
    private readonly byte r;
    private readonly byte g;
    private readonly byte b;
    private readonly byte a;

    private Color(byte r, byte g, byte b, byte a) {
        this.r = r;
        this.g = g;
        this.b = b;
        this.a = a;
    }

    public RawColor4 ToRaw() => new RawColor4(r / 255F, g / 255F, b / 255F, a / 255F);

    public static Color FromRGBA(byte r, byte g, byte b, byte a) {
        return new Color(r, g, b, a);
    }

    public static Color FromRGB(byte r, byte g, byte b) {
        return new Color(r, g, b, 0xFF);
    }

    public static Color FromHEXA(uint hex) {
        return FromRGBA(
            (byte)((hex & 0xFF00_0000) >> 24),
            (byte)((hex & 0x00FF_0000) >> 16),
            (byte)((hex & 0x0000_FF00) >> 8),
            (byte)(hex & 0x0000_00FF)
        );
    }
    public static Color FromHEX(uint hex) => FromHEXA(hex << 8 | 0xFF);

    public static readonly Color BLACK = FromHEX(0x0);
    public static readonly Color WHITE = FromHEX(0xFFFFFF);
    public static readonly Color TRANSPARENT = FromHEXA(0x0);
}