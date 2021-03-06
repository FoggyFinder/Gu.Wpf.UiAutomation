﻿namespace Gu.Wpf.UiAutomation
{
    public abstract class AnnotationPatternBase<TNativePattern> : PatternBase<TNativePattern>, IAnnotationPattern
        where TNativePattern : class
    {
        private AutomationProperty<AnnotationType> annotationType;
        private AutomationProperty<string> annotationTypeName;
        private AutomationProperty<string> author;
        private AutomationProperty<string> dateTime;
        private AutomationProperty<AutomationElement> target;

        protected AnnotationPatternBase(BasicAutomationElementBase basicAutomationElement, TNativePattern nativePattern)
            : base(basicAutomationElement, nativePattern)
        {
        }

        /// <inheritdoc/>
        public IAnnotationPatternProperties Properties => this.Automation.PropertyLibrary.Annotation;

        /// <inheritdoc/>
        public AutomationProperty<AnnotationType> AnnotationType => this.GetOrCreate(ref this.annotationType, this.Properties.AnnotationTypeId);

        /// <inheritdoc/>
        public AutomationProperty<string> AnnotationTypeName => this.GetOrCreate(ref this.annotationTypeName, this.Properties.AnnotationTypeName);

        /// <inheritdoc/>
        public AutomationProperty<string> Author => this.GetOrCreate(ref this.author, this.Properties.Author);

        /// <inheritdoc/>
        public AutomationProperty<string> DateTime => this.GetOrCreate(ref this.dateTime, this.Properties.DateTime);

        /// <inheritdoc/>
        public AutomationProperty<AutomationElement> Target => this.GetOrCreate(ref this.target, this.Properties.Target);
    }
}
