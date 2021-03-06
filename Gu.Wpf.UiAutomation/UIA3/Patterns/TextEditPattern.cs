﻿namespace Gu.Wpf.UiAutomation.UIA3.Patterns
{
    using Gu.Wpf.UiAutomation.UIA3.Converters;
    using Gu.Wpf.UiAutomation.UIA3.Identifiers;

    public class TextEditPattern : TextPattern, ITextEditPattern
    {
        public new static readonly PatternId Pattern = PatternId.GetOrCreate(Interop.UIAutomationClient.UIA_PatternIds.UIA_TextEditPatternId, "TextEdit", AutomationObjectIds.IsTextEditPatternAvailableProperty);
        public static readonly EventId ConversionTargetChangedEvent = EventId.GetOrCreate(Interop.UIAutomationClient.UIA_EventIds.UIA_TextEdit_ConversionTargetChangedEventId, "ConversionTargetChanged");
        public static readonly EventId TextChangedEvent2 = EventId.GetOrCreate(Interop.UIAutomationClient.UIA_EventIds.UIA_TextEdit_TextChangedEventId, "TextChanged");

        public TextEditPattern(BasicAutomationElementBase basicAutomationElement, Interop.UIAutomationClient.IUIAutomationTextEditPattern nativePattern)
            : base(basicAutomationElement, nativePattern)
        {
            this.ExtendedNativePattern = nativePattern;
        }

        public Interop.UIAutomationClient.IUIAutomationTextEditPattern ExtendedNativePattern { get; }

        ITextEditPatternEvents ITextEditPattern.Events => this.Automation.EventLibrary.TextEdit;

        public ITextRange GetActiveComposition()
        {
            var nativeRange = Com.Call(() => this.ExtendedNativePattern.GetActiveComposition());
            return TextRangeConverter.NativeToManaged((UIA3Automation)this.BasicAutomationElement.Automation, nativeRange);
        }

        public ITextRange GetConversionTarget()
        {
            var nativeRange = Com.Call(() => this.ExtendedNativePattern.GetConversionTarget());
            return TextRangeConverter.NativeToManaged((UIA3Automation)this.BasicAutomationElement.Automation, nativeRange);
        }
    }
}
