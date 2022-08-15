using System;

namespace Football.DAL.Models {
  public class Resolution {
    private const Char DELIM = ',';
    private readonly Boolean _isFullscreen;

    public Int32 Width { get; }
    public Int32 Height { get; }

    public Resolution(Int32 width, Int32 height) {
      Width = width;
      Height = height;
    }

    public Resolution(Int32 width, Int32 height, Boolean isFullscreen)
      : this(width, height) =>
      _isFullscreen = isFullscreen;

    public static Resolution DEFAULT => new Resolution(800, 600);

    public String FormatForFile() => $"{Width}{DELIM}{Height}{DELIM}{_isFullscreen}";
    public static Resolution Parse(String line) {
      String[] data = line.Split(DELIM);

      return new Resolution(width: data.Length > 0 ? Int32.Parse(data[0]) : DEFAULT.Width,
                            height: data.Length > 1 ? Int32.Parse(data[1]) : DEFAULT.Height,
                            isFullscreen: data.Length > 2 && Boolean.Parse(data[2]));
    }

    public override String ToString() => _isFullscreen ? "Fullscreen" : $"{Width} x {Height}";
    public override Boolean Equals(Object obj) => obj is Resolution res && Width == res.Width && Height == res.Height;
    override public Int32 GetHashCode() => Width.GetHashCode() ^ Height.GetHashCode();
  }
}
