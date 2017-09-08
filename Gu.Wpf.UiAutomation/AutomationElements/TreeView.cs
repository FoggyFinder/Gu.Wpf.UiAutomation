﻿namespace Gu.Wpf.UiAutomation
{
    using System.Collections.Generic;
    using System.Linq;

    public class TreeView : Control
    {
        private readonly Condition treeViewItemCondition;

        public TreeView(BasicAutomationElementBase basicAutomationElement)
            : base(basicAutomationElement)
        {
            this.treeViewItemCondition = this.ConditionFactory.ByControlType(ControlType.TreeItem);
        }

        /// <summary>
        /// The currently selected <see cref="TreeViewItem" />
        /// </summary>
        public TreeViewItem SelectedTreeViewItem => this.SearchSelectedItem(this.Items);

        /// <summary>
        /// All child <see cref="TreeViewItem" /> objects from this <see cref="TreeView" />
        /// </summary>
        public IReadOnlyList<TreeViewItem> Items => this.BasicAutomationElement.FindAll(
            TreeScope.Children,
            this.treeViewItemCondition,
            x => new TreeViewItem(x));

        private TreeViewItem SearchSelectedItem(IReadOnlyList<TreeViewItem> treeItems)
        {
            // Search for a selected item in the direct children
            var directSelectedItem = treeItems.FirstOrDefault(t => t.IsSelected);
            if (directSelectedItem != null)
            {
                return directSelectedItem;
            }

            // Loop thru the children and search in their children
            foreach (var treeItem in treeItems)
            {
                var selectedInChildItem = this.SearchSelectedItem(treeItem.Items);
                if (selectedInChildItem != null)
                {
                    return selectedInChildItem;
                }
            }

            return null;
        }
    }
}
