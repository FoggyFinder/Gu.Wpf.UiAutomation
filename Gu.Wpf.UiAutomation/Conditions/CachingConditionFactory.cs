namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.Collections.Concurrent;
    using Interop.UIAutomationClient;

    /// <summary>
    /// Helper class with some commonly used conditions
    /// </summary>
    public class CachingConditionFactory : ConditionFactory
    {
        private readonly IUIAutomation automation;
        private readonly ConcurrentDictionary<string, CachedCondition> idConditions = new ConcurrentDictionary<string, CachedCondition>();
        private readonly ConcurrentDictionary<string, CachedCondition> nameOrIdConditions = new ConcurrentDictionary<string, CachedCondition>();
        private readonly ConcurrentDictionary<TypeAndStringKey, CachedCondition> typeNameOrIdConditions = new ConcurrentDictionary<TypeAndStringKey, CachedCondition>();
        private readonly ConcurrentDictionary<ControlType, CachedCondition> controlTypeConditions = new ConcurrentDictionary<ControlType, CachedCondition>();

        public CachingConditionFactory(IPropertyLibray propertyLibrary, IUIAutomation automation)
            : base(propertyLibrary)
        {
            this.automation = automation;
        }

        public override Condition ByAutomationId(string automationId)
        {
            return this.idConditions.GetOrAdd(
                automationId,
                _ => base.ByAutomationId(automationId).AsCached(this.automation));
        }

        public override Condition ByControlType(ControlType controlType)
        {
            return this.controlTypeConditions.GetOrAdd(
                controlType,
                _ => base.ByControlType(controlType).AsCached(this.automation));
        }

        public override Condition ByTypeNameOrId(ControlType controlType, string name)
        {
            return this.typeNameOrIdConditions.GetOrAdd(
                new TypeAndStringKey(controlType, name),
                _ => new AndCondition(
                    this.ByControlType(controlType),
                    this.ByNameOrId(name)).AsCached(this.automation));
        }

        public override Condition ByNameOrId(string key)
        {
            return this.nameOrIdConditions.GetOrAdd(
                key,
                _ => new OrCondition(
                    this.ByName(key),
                    this.ByAutomationId(key)).AsCached(this.automation));
        }

        private struct TypeAndStringKey : IEquatable<TypeAndStringKey>
        {
            public TypeAndStringKey(ControlType controlType, string text)
            {
                this.ControlType = controlType;
                this.Text = text;
            }

            public ControlType ControlType { get; }

            public string Text { get; }

            public static bool operator ==(TypeAndStringKey left, TypeAndStringKey right)
            {
                return left.Equals(right);
            }

            public static bool operator !=(TypeAndStringKey left, TypeAndStringKey right)
            {
                return !left.Equals(right);
            }

            public bool Equals(TypeAndStringKey other)
            {
                return this.ControlType == other.ControlType && string.Equals(this.Text, other.Text);
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj))
                {
                    return false;
                }

                return obj is TypeAndStringKey && this.Equals((TypeAndStringKey)obj);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    return ((int)this.ControlType * 397) ^ this.Text.GetHashCode();
                }
            }
        }
    }
}