﻿namespace Gu.Wpf.UiAutomation.UITests
{
    using System;
    using Gu.Wpf.UiAutomation.WindowsAPI;
    using NUnit.Framework;
    using OperatingSystem = Gu.Wpf.UiAutomation.OperatingSystem;

    [TestFixture]
    public class CalculatorTests
    {
        [Test]
        public void CalculatorTest()
        {
            using (var app = StartApplication())
            {
                var window = app.GetMainWindow(TimeSpan.FromSeconds(1));
                var calc = OperatingSystem.IsWindows10() ? (ICalculator)new Win10Calc(window) : new LegacyCalc(window);

                // Switch to default mode
                System.Threading.Thread.Sleep(1000);
                Keyboard.TypeSimultaneously(VirtualKeyShort.ALT, VirtualKeyShort.KEY_1);
                Wait.UntilInputIsProcessed();
                app.WaitWhileBusy();
                System.Threading.Thread.Sleep(1000);

                // Simple addition
                calc.Button1.Click();
                calc.Button2.Click();
                calc.Button3.Click();
                calc.Button4.Click();
                calc.ButtonAdd.Click();
                calc.Button5.Click();
                calc.Button6.Click();
                calc.Button7.Click();
                calc.Button8.Click();
                calc.ButtonEquals.Click();
                app.WaitWhileBusy();
                var result = calc.Result;
                Assert.That(result, Is.EqualTo("6912"));

                // Date comparison
                using (Keyboard.Pressing(VirtualKeyShort.CONTROL))
                {
                    Keyboard.Type(VirtualKeyShort.KEY_E);
                }
            }
        }

        private static Application StartApplication()
        {
            if (OperatingSystem.IsWindows10())
            {
                // Use the store application on those systems
                return Application.LaunchStoreApp("Microsoft.WindowsCalculator_8wekyb3d8bbwe!App");
            }

            if (OperatingSystem.IsWindowsServer2016())
            {
                // The calc.exe on this system is just a stub which launches win32calc.exe
                return Application.Launch("win32calc.exe");
            }

            return Application.Launch("calc.exe");
        }
    }
}
