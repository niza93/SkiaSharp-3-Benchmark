using SkiaSharp;
using SkiaSharp.Views.Desktop;
using System.Diagnostics;
using System.Windows;

namespace SkiaSharp3._116._1.NET
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    private const int RectCount = 100000; // Количество примитивов для отрисовки
    private readonly Stopwatch _stopwatch = new();
    private readonly Stopwatch _stopwatchGL = new();
    public MainWindow()
    {
      InitializeComponent();
    }

    private void SKElement_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
    {
      _stopwatch.Restart();

      var canvas = e.Surface.Canvas;
      Benchmark(canvas, e.Info);
      _stopwatch.Stop();
      block.Text = $"SK rendering: {_stopwatch.ElapsedMilliseconds} ms";
    }

    private void SKElement_PaintSurface(object sender, SKPaintGLSurfaceEventArgs e)
    {
      _stopwatchGL.Restart();

      var canvas = e.Surface.Canvas;
      Benchmark(canvas, e.Info);

      _stopwatchGL.Stop();
      blockGL.Text = $"SKGL rendering: {_stopwatchGL.ElapsedMilliseconds} ms";
    }

    private void Benchmark(SKCanvas canvas, SKImageInfo info)
    {
      canvas.Clear(SKColors.White);

      var paint = new SKPaint
      {
        Color = SKColors.Green,
        IsAntialias = true
      };

      var random = new Random();
      for (int i = 0; i < RectCount; i++)
      {
        float x = (float)random.NextDouble() * info.Width;
        float y = (float)random.NextDouble() * info.Height;
        float size = 5;

        //if (x > 100 && x < 300 && y > 100 && y < 300)
        canvas.DrawCircle(x, y, size, paint);
      }
    }
  }
}