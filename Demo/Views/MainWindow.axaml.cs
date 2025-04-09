using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.VisualTree;
using System.Linq;
using Avalonia.Platform;

namespace DragDropTest;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        
        ExtendClientAreaToDecorationsHint = true;
        //ExtendClientAreaChromeHints = ExtendClientAreaChromeHints.PreferSystemChrome;
        ExtendClientAreaChromeHints = ExtendClientAreaChromeHints.SystemChrome | ExtendClientAreaChromeHints.OSXThickTitleBar;
  
    }

    private Control? _capturedControl;
    private bool _isDragging = false;

    private void Item_PointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (sender is Control control)
        {
            e.Pointer.Capture(control);
            _capturedControl = control;
            _isDragging = true;
            DebugInfo.Text = $"🎯 Pointer captured by: {control}";
        }
    }

    private void Item_PointerReleased(object? sender, PointerReleasedEventArgs e)
    {
        if (_capturedControl != null)
        {
            e.Pointer.Capture(null);
            DebugInfo.Text = $"🔓 Released pointer from: {_capturedControl}";
            _capturedControl = null;
            _isDragging = false;
        }
    }

    private void Window_PointerMoved(object? sender, PointerEventArgs e)
    {
 
        var root = this.GetVisualRoot() as Visual;
        
        var point = e.GetPosition(root);
        var position = e.GetPosition(root);

        var visuals = root.GetVisualsAt(position).OfType<Visual>().Take(1).ToList();
        var info = string.Join("\n", visuals.Select(v =>
        {
            var name = (v as Control)?.Name;
            var type = v.GetType().Name;
            return $"{type} {(string.IsNullOrEmpty(name) ? "" : $"(Name={name})")}";
        }));

        DebugInfo.Text = $"🖱 Pointer over:\n{info}\n\nPoint: {point.X}, {point.Y} \n\nDragging: {_isDragging}";
    }
}