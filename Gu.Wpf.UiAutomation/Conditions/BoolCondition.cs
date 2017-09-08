namespace Gu.Wpf.UiAutomation
{
    public abstract class BoolCondition : Condition
    {
        public BoolCondition(bool booleanValue)
        {
            this.BooleanValue = booleanValue;
        }

        public bool BooleanValue { get; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"BOOL: {this.BooleanValue}";
        }
    }
}
