using Gtk;

var application = Application.New("org.gir.core", Gio.ApplicationFlags.FlagsNone);
application.OnActivate += (sender, args) =>
{
    var drawingArea = DrawingArea.New();
    drawingArea.SetDrawFunc(); //TODO
    
    var window = ApplicationWindow.New((Application) sender);
    window.Title = "DrawingArea Demo";
    window.Child = drawingArea;
    window.Show();
};
return application.Run();
