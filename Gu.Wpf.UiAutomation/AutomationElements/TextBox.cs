﻿namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.Linq;
    using System.Threading;
    using Gu.Wpf.UiAutomation.WindowsAPI;

    public class TextBox : Control
    {
        public TextBox(BasicAutomationElementBase basicAutomationElement)
            : base(basicAutomationElement)
        {
        }

        public string Text
        {
            get
            {
                if (this.Properties.IsPassword)
                {
                    throw new MethodNotSupportedException($"Text from element '{this.ToString()}' cannot be retrieved because it is set as password.");
                }

                var valuePattern = this.Patterns.Value.PatternOrDefault;
                if (valuePattern != null)
                {
                    return valuePattern.Value;
                }

                var textPattern = this.Patterns.Text.PatternOrDefault;
                if (textPattern != null)
                {
                    return textPattern.DocumentRange.GetText(int.MaxValue);
                }

                throw new MethodNotSupportedException($"AutomationElement '{this.ToString()}' supports neither ValuePattern or TextPattern");
            }

            set
            {
                var valuePattern = this.Patterns.Value.PatternOrDefault;
                if (valuePattern != null)
                {
                    valuePattern.SetValue(value);
                }
                else
                {
                    this.Enter(value);
                }
            }
        }

        /// <summary>
        /// Simulate typing in text. This is slower than setting Text but raises more events.
        /// </summary>
        public void Enter(string value)
        {
            this.Focus();
            var valuePattern = this.Patterns.Value.PatternOrDefault;
            valuePattern?.SetValue(string.Empty);
            if (string.IsNullOrEmpty(value))
            {
                return;
            }

            var lines = value.Replace("\r\n", "\n").Split('\n');
            Keyboard.Type(lines[0]);
            foreach (var line in lines.Skip(1))
            {
                Keyboard.Type(VirtualKeyShort.RETURN);
                Keyboard.Type(line);
            }

            // give some time to process input.
            var stopTime = DateTime.Now.AddSeconds(1);
            while (DateTime.Now < stopTime)
            {
                if (this.Text == value)
                {
                    return;
                }

                if (!Thread.Yield())
                {
                    Thread.Sleep(10);
                }
            }
        }
    }
}
