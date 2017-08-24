namespace Gu.Wpf.UiAutomation.UITests.Elements
{
    using System.IO;
    using NUnit.Framework;

    public class ItemsControlTests
    {
        private static readonly string ExeFileName = Path.Combine(
            TestContext.CurrentContext.TestDirectory,
            @"..\..\TestApplications\WpfApplication\bin\WpfApplication.exe");

        [Test]
        public void Items()
        {
            using (var app = Application.Launch(ExeFileName, "ItemsControlWindow"))
            {
                var window = app.MainWindow();
                var child = window.FindFirstChild();
                var itemsControl = window.FindItemsControl();
                Assert.AreEqual(2, itemsControl.Items.Count);
            }
        }
    }
}