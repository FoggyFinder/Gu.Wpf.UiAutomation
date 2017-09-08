namespace Gu.Wpf.UiAutomation
{
    using System.Collections.Generic;

    public class ConditionList : List<Condition>
    {
        public ConditionList(params Condition[] conditions)
        {
            this.AddRange(conditions);
        }
    }
}
