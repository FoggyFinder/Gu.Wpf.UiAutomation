﻿namespace Gu.Wpf.UiAutomation.UITests.Patterns
{
    using NUnit.Framework;

    public class ExpandCollapsePatternTests
    {
        private static readonly string ExeFileName = Application.FindExe("WpfApplication.exe");

        [Test]
        public void ExpanderTest()
        {
            using (var app = Application.Launch(ExeFileName, "ExpanderWindow"))
            {
                var window = app.MainWindow;
                var expander = window.FindExpander();
                Assert.NotNull(expander);
                var ecp = expander.Patterns.ExpandCollapse.Pattern;
                Assert.AreEqual(ExpandCollapseState.Expanded, ecp.ExpandCollapseState.Value);

                ecp.Collapse();
                Assert.AreEqual(ExpandCollapseState.Collapsed, ecp.ExpandCollapseState.Value);

                ecp.Expand();
                Assert.AreEqual(ExpandCollapseState.Expanded, ecp.ExpandCollapseState.Value);
            }
        }
    }
}
