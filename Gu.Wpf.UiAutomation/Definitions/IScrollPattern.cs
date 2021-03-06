namespace Gu.Wpf.UiAutomation
{
    public interface IScrollPattern : IPattern
    {
        IScrollPatternProperties Properties { get; }

        AutomationProperty<bool> HorizontallyScrollable { get; }

        AutomationProperty<double> HorizontalScrollPercent { get; }

        AutomationProperty<double> HorizontalViewSize { get; }

        AutomationProperty<bool> VerticallyScrollable { get; }

        AutomationProperty<double> VerticalScrollPercent { get; }

        AutomationProperty<double> VerticalViewSize { get; }

        void Scroll(ScrollAmount horizontalAmount, ScrollAmount verticalAmount);

        void SetScrollPercent(double horizontalPercent, double verticalPercent);
    }
}