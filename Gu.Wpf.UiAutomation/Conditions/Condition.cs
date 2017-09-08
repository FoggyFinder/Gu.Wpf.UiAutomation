namespace Gu.Wpf.UiAutomation
{
    using Interop.UIAutomationClient;

    /// <summary>
    /// Base class for the conditions
    /// </summary>
    public abstract class Condition
    {
        public abstract Interop.UIAutomationClient.IUIAutomationCondition ToNative(IUIAutomation automation);
    }
}
