﻿namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.Collections.Generic;
    using System.Windows;

    public abstract class BasicAutomationElementBase
    {
        protected BasicAutomationElementBase(AutomationBase automation)
        {
            this.Automation = automation;
            this.Properties = new AutomationElementPropertyValues(this);
        }

        public abstract AutomationElementPatternValuesBase Patterns { get; }

        public AutomationElementPropertyValues Properties { get; }

        /// <summary>
        /// Underlying <see cref="AutomationBase" /> object where this element belongs to
        /// </summary>
        public AutomationBase Automation { get; }

        /// <summary>
        /// Gets the desired property value. Ends in an exception if the property is not supported or not cached.
        /// </summary>
        public object GetPropertyValue(PropertyId property)
        {
            return this.GetPropertyValue<object>(property);
        }

        public T GetPropertyValue<T>(PropertyId property)
        {
            if (Equals(property, PropertyId.NotSupportedByFramework))
            {
                throw new NotSupportedByFrameworkException();
            }

            var isCacheActive = CacheRequest.IsCachingActive;
            try
            {
                var value = this.InternalGetPropertyValue(property.Id, isCacheActive, useDefaultIfNotSupported: false);
                if (value == this.Automation.NotSupportedValue)
                {
                    throw new PropertyNotSupportedException(property);
                }

                return property.Convert<T>(this.Automation, value);
            }
            catch (Exception ex)
            {
                if (isCacheActive)
                {
                    var cacheRequest = CacheRequest.Current;
                    if (!cacheRequest.Properties.Contains(property))
                    {
                        throw new PropertyNotCachedException(property, ex);
                    }
                }

                // Should actually never come here
                throw;
            }
        }

        /// <summary>
        /// Tries to get the property value.
        /// Returns false and sets a default value if the property is not supported.
        /// </summary>
        public bool TryGetPropertyValue(PropertyId property, out object value)
        {
            return this.TryGetPropertyValue<object>(property, out value);
        }

        public bool TryGetPropertyValue<T>(PropertyId property, out T value)
        {
            if (Equals(property, PropertyId.NotSupportedByFramework))
            {
                throw new NotSupportedByFrameworkException();
            }

            var isCacheActive = CacheRequest.IsCachingActive;
            try
            {
                var internalValue = this.InternalGetPropertyValue(property.Id, isCacheActive, useDefaultIfNotSupported: false);
                if (internalValue == this.Automation.NotSupportedValue)
                {
                    value = default(T);
                    return false;
                }

                value = property.Convert<T>(this.Automation, internalValue);
                return true;
            }
            catch (Exception ex)
            {
                if (isCacheActive)
                {
                    var cacheRequest = CacheRequest.Current;
                    if (!cacheRequest.Properties.Contains(property))
                    {
                        throw new PropertyNotCachedException(property, ex);
                    }
                }

                // Should actually never come here
                throw;
            }
        }

        public T GetNativePattern<T>(PatternId pattern)
        {
            if (Equals(pattern, PatternId.NotSupportedByFramework))
            {
                throw new NotSupportedByFrameworkException();
            }

            var isCacheActive = CacheRequest.IsCachingActive;
            try
            {
                var nativePattern = this.InternalGetPattern(pattern.Id, isCacheActive);
                if (nativePattern == null)
                {
                    throw new InvalidOperationException("Native pattern is null");
                }

                return (T)nativePattern;
            }
            catch (Exception ex)
            {
                if (isCacheActive)
                {
                    var cacheRequest = CacheRequest.Current;
                    if (!cacheRequest.Patterns.Contains(pattern))
                    {
                        throw new PatternNotCachedException(pattern, ex);
                    }
                }

                throw new PatternNotSupportedException(pattern, ex);
            }
        }

        public bool TryGetNativePattern<T>(PatternId pattern, out T nativePattern)
        {
            nativePattern = default(T);
            if (Equals(pattern, PatternId.NotSupportedByFramework))
            {
                return false;
            }

            var isCacheActive = CacheRequest.IsCachingActive;
            nativePattern = (T)this.InternalGetPattern(pattern.Id, isCacheActive);
            return nativePattern != null;
        }

        public Point GetClickablePoint()
        {
            if (!this.TryGetClickablePoint(out var point))
            {
                throw new NoClickablePointException();
            }

            return point;
        }

        public abstract void SetFocus();

        public abstract IReadOnlyList<AutomationElement> FindAll(TreeScope treeScope, Condition condition);

        public abstract IReadOnlyList<T> FindAll<T>(TreeScope treeScope, Condition condition, Func<BasicAutomationElementBase, T> wrap)
            where T : AutomationElement;

        public abstract AutomationElement FindFirst(TreeScope treeScope, Condition condition);

        public abstract T FindFirst<T>(TreeScope treeScope, Condition condition, Func<BasicAutomationElementBase, T> wrap)
            where T : AutomationElement;

        public abstract AutomationElement FindIndexed(TreeScope treeScope, Condition condition, int index);

        public abstract T FindIndexed<T>(TreeScope treeScope, Condition condition, int index, Func<BasicAutomationElementBase, T> wrap)
            where T : AutomationElement;

        public abstract bool TryGetClickablePoint(out Point point);

        public abstract IDisposable SubscribeToEvent(EventId @event, TreeScope treeScope, Action<AutomationElement, EventId> action);

        public abstract IDisposable SubscribeToPropertyChangedEvent(TreeScope treeScope, Action<AutomationElement, PropertyId, object> action, PropertyId[] properties);

        public abstract IDisposable SubscribeToStructureChangedEvent(TreeScope treeScope, Action<AutomationElement, StructureChangeType, int[]> action);

        public abstract IReadOnlyList<PatternId> GetSupportedPatterns();

        public abstract IReadOnlyList<PropertyId> GetSupportedProperties();

        public abstract AutomationElement GetUpdatedCache();

        public abstract IReadOnlyList<AutomationElement> GetCachedChildren();

        public abstract AutomationElement GetCachedParent();

        /// <summary>
        /// Gets the desired property value
        /// </summary>
        /// <param name="propertyId">The id of the property to get</param>
        /// <param name="cached">Flag to indicate if the cached or current value should be fetched</param>
        /// <param name="useDefaultIfNotSupported">
        /// Flag to indicate, if the default value should be used if the property is not supported
        /// </param>
        /// <returns>The value / default value of the property or <see cref="AutomationBase.NotSupportedValue" /></returns>
        protected abstract object InternalGetPropertyValue(int propertyId, bool cached, bool useDefaultIfNotSupported);

        /// <summary>
        /// Gets the desired pattern
        /// </summary>
        /// <param name="patternId">The id of the pattern to get</param>
        /// <param name="cached">Flag to indicate if the cached or current pattern should be fetched</param>
        /// <returns>The pattern or null if it was not found / cached</returns>
        protected abstract object InternalGetPattern(int patternId, bool cached);
    }
}
