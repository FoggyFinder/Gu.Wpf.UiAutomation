namespace Gu.Wpf.UiAutomation
{
    /// <summary>
    /// Helper class with some commonly used conditions
    /// </summary>
    public class ConditionFactory
    {
        public ConditionFactory(IPropertyLibray propertyLibrary)
        {
            this.PropertyLibrary = propertyLibrary;
        }

        public IPropertyLibray PropertyLibrary { get; }

        public virtual Condition ByAutomationId(string automationId)
        {
            return new PropertyCondition(this.PropertyLibrary.Element.AutomationId, automationId);
        }

        public virtual Condition ByControlType(ControlType controlType)
        {
            return new PropertyCondition(this.PropertyLibrary.Element.ControlType, controlType);
        }

        public virtual Condition ByClassName(string className)
        {
            return new PropertyCondition(this.PropertyLibrary.Element.ClassName, className);
        }

        public virtual Condition ByName(string name)
        {
            return new PropertyCondition(this.PropertyLibrary.Element.Name, name);
        }

        public virtual Condition ByTypeNameOrId(ControlType controlType, string name)
        {
            return new AndCondition(
                this.ByControlType(controlType),
                this.ByNameOrId(name));
        }

        public virtual Condition ByNameOrId(string key)
        {
            return new OrCondition(
                this.ByName(key),
                this.ByAutomationId(key));
        }

        public virtual Condition ByValue(string text)
        {
            return new PropertyCondition(this.PropertyLibrary.Value.Value, text);
        }

        public virtual Condition ByProcessId(int processId)
        {
            return new PropertyCondition(this.PropertyLibrary.Element.ProcessId, processId);
        }

        public virtual Condition ByLocalizedControlType(string localizedControlType)
        {
           return new PropertyCondition(this.PropertyLibrary.Element.LocalizedControlType, localizedControlType);
        }

        public virtual Condition ByHelpTextProperty(string helpText)
        {
           return new PropertyCondition(this.PropertyLibrary.Element.HelpText, helpText);
        }

        /// <summary>
        /// Searches for a Menu/MenuBar
        /// </summary>
        public OrCondition Menu()
        {
            return new OrCondition(this.ByControlType(ControlType.Menu), this.ByControlType(ControlType.MenuBar));
        }

        /// <summary>
        /// Searches for a DataGrid/List
        /// </summary>
        public OrCondition Grid()
        {
            return new OrCondition(this.ByControlType(ControlType.DataGrid), this.ByControlType(ControlType.List));
        }

        public OrCondition HScrollBar()
        {
            return new OrCondition(this.ByControlType(ControlType.ScrollBar), this.ByName(LocalizedStrings.HorizontalScrollBar));
        }

        public OrCondition VScrollBar()
        {
            return new OrCondition(this.ByControlType(ControlType.ScrollBar), this.ByName(LocalizedStrings.VerticalScrollBar));
        }
    }
}
