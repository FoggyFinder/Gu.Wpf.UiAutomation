namespace Gu.Wpf.UiAutomation
{
    using System.Collections.Generic;

    public abstract class JunctionCondition : Condition
    {
        protected JunctionCondition()
        {
            this.Conditions = new List<Condition>();
        }

        public List<Condition> Conditions { get; }

        public int ChildCount => this.Conditions.Count;
    }
}
