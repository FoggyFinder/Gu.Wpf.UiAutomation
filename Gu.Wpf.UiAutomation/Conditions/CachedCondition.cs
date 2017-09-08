namespace Gu.Wpf.UiAutomation
{
    using System;
    using Interop.UIAutomationClient;

    public class CachedCondition : Condition
    {
        private readonly Condition condition;
        private readonly IUIAutomation cachedAutomation;
        private readonly IUIAutomationCondition native;

        public CachedCondition(Condition condition, IUIAutomation cachedAutomation)
        {
            this.condition = condition;
            this.cachedAutomation = cachedAutomation;
            this.native = condition.ToNative(cachedAutomation);
        }

        public override string ToString()
        {
            return this.condition.ToString();
        }

        public override IUIAutomationCondition ToNative(IUIAutomation automation)
        {
            if (!ReferenceEquals(this.cachedAutomation, automation))
            {
                throw new InvalidOperationException($"Calling ToNative with a different instance of automation.");
            }

            return this.native;
        }
    }
}