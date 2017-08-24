namespace Gu.Wpf.UiAutomation
{
    using System.Collections.Generic;

    public class ItemsControl : Control
    {
        public ItemsControl(BasicAutomationElementBase basicAutomationElement)
            : base(basicAutomationElement)
        {
        }

        /// <summary>
        /// Returns the rows which are currently visible to UIA. Might not be the full list (eg. in virtualized lists)!
        /// </summary>
        public virtual IReadOnlyList<AutomationElement> Items => this.FindAllChildren();
    }
}