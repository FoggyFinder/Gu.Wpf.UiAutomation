namespace Gu.Wpf.UiAutomation
{
    using System.Collections.Generic;
    using Interop.UIAutomationClient;

    public static class ConditionExt
    {
        /// <summary>
        /// Adds the given condition with an "and"
        /// </summary>
        public static AndCondition And(this Condition self, Condition newCondition)
        {
            // Check if this condition is already an and condition
            if (self is AndCondition thisCondition)
            {
                // If so, just add the new one
                var newConditions = new List<Condition>(thisCondition.ChildCount + 1);
                newConditions.AddRange(thisCondition.Conditions);
                newConditions.Add(newCondition);
                return new AndCondition(newConditions);
            }

            // It is not, so pack it into an and condition
            return new AndCondition(self, newCondition);
        }

        /// <summary>
        /// Adds the given condition with an "or"
        /// </summary>
        public static OrCondition Or(this Condition self, Condition newCondition)
        {
            // Check if this condition is already an or condition
            if (self is OrCondition thisCondition)
            {
                // If so, just add the new one
                var newConditions = new List<Condition>(thisCondition.ChildCount + 1);
                newConditions.AddRange(thisCondition.Conditions);
                newConditions.Add(newCondition);
                return new OrCondition(newConditions);
            }

            // It is not, so pack it into an or condition
            return new OrCondition(self, newCondition);
        }

        /// <summary>
        /// Packs this condition into a not condition
        /// </summary>
        public static NotCondition Not(this Condition self)
        {
            return new NotCondition(self);
        }

        /// <summary>
        /// Packs this condition into a not condition
        /// </summary>
        public static CachedCondition AsCached(this Condition self, IUIAutomation automation)
        {
            return new CachedCondition(self, automation);
        }
    }
}