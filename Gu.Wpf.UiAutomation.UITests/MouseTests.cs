﻿namespace Gu.Wpf.UiAutomation.UITests
{
    using System.Windows;
    using NUnit.Framework;

    [TestFixture]
    public class MouseTests
    {
        private static readonly string ExeFileName = Application.FindExe("WpfApplication.exe");

        [Test]
        public void MoveTest()
        {
            Mouse.Position = new Point(0, 0);
            Mouse.MoveBy(800, 0);
            Mouse.MoveBy(0, 400);
            Mouse.MoveBy(-400, -200);
        }

        [Test]
        public void ClickTest()
        {
            using (var app = Application.Launch(ExeFileName, "EmptyWindow"))
            {
                var mainWindow = app.MainWindow;
                var mouseX = mainWindow.Properties.BoundingRectangle.Value.Left + 50;
                var mouseY = mainWindow.Properties.BoundingRectangle.Value.Top + 200;
                Mouse.Position = new Point(mouseX, mouseY);
                Mouse.Down(MouseButton.Left);
                Mouse.MoveBy(100, 10);
                Mouse.MoveBy(10, 50);
                Mouse.Up(MouseButton.Left);
            }
        }
    }
}
