﻿namespace Gu.Wpf.UiAutomation.UITests.Elements
{
    using System.IO;
    using NUnit.Framework;

    public class PopupTests
    {
        private static readonly string ExeFileName = Path.Combine(
            TestContext.CurrentContext.TestDirectory,
            @"..\..\TestApplications\WpfApplication\bin\WpfApplication.exe");

        [Test]
        public void CheckBoxInPopupTest()
        {
            using (var app = Application.Launch(ExeFileName, "PopupWindow"))
            {
                var window = app.MainWindow;
                var btn = window.FindToggleButton("PopupToggleButton1");
                btn.Click();
                Wait.UntilInputIsProcessed();
                var popup = window.Popup;
                Assert.NotNull(popup);
                var popupChildren = popup.FindAllChildren();
                Assert.AreEqual(1, popupChildren.Count);
                var check = popupChildren[0].AsCheckBox();
                Assert.AreEqual("This is a popup", check.Text);
            }
        }

        [Test]
        public void MenuInPopupTest()
        {
            using (var app = Application.Launch(ExeFileName, "PopupWindow"))
            {
                var window = app.MainWindow;
                var btn = window.FindToggleButton("PopupToggleButton2");
                btn.Click();
                Wait.UntilInputIsProcessed();
                var popup = window.Popup;
                Assert.That(popup, Is.Not.Null);
                var popupChildren = popup.FindAllChildren();
                Assert.That(popupChildren, Has.Length.EqualTo(1));
                var menu = popupChildren[0].AsMenu();
                Assert.AreEqual(1, menu.Items.Count);
                var menuItem = menu.Items[0];
                Assert.That(menuItem.Text, Is.EqualTo("Some MenuItem"));
            }
        }
    }
}
