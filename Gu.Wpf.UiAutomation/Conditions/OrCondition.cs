namespace Gu.Wpf.UiAutomation
{
    using System.Collections.Generic;
    using System.Linq;
    using Interop.UIAutomationClient;

    public class OrCondition : JunctionCondition
    {
        public OrCondition(Condition condition1, Condition condition2)
            : this(new[] { condition1, condition2 })
        {
        }

        public OrCondition(Condition condition1, Condition condition2, Condition condition3)
            : this(new[] { condition1, condition2, condition3 })
        {
        }

        public OrCondition(IEnumerable<Condition> conditions)
        {
            this.Conditions.AddRange(conditions);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"({string.Join(" OR ", this.Conditions.Select(c => c.ToString()))})";
        }

        public override IUIAutomationCondition ToNative(IUIAutomation automation)
        {
            return automation.CreateOrConditionFromArray(this.Conditions.Select(c => c.ToNative(automation)).ToArray());
        }
    }
}
