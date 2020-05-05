#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System;
using UnityEditor.IMGUI.Controls;
using Cysharp.Threading.Tasks.Internal;
using System.Text;
using System.Text.RegularExpressions;

namespace Cysharp.Threading.Tasks.Editor
{
    public class UniTaskTrackerViewItem : TreeViewItem
    {
        static Regex removeHref = new Regex("<a href.+>(.+)</a>", RegexOptions.Compiled);

        public string TaskType { get; set; }
        public string Elapsed { get; set; }
        public string Status { get; set; }

        string position;
        public string Position
        {
            get { return position; }
            set
            {
                position = value;
                PositionFirstLine = GetFirstLine(position);
            }
        }

        public string PositionFirstLine { get; private set; }

        static string GetFirstLine(string str)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == '\r' || str[i] == '\n')
                {
                    break;
                }
                sb.Append(str[i]);
            }

            return removeHref.Replace(sb.ToString(), "$1");
        }

        public UniTaskTrackerViewItem(int id) : base(id)
        {

        }
    }

    public class UniTaskTrackerTreeView : TreeView
    {
        const string sortedColumnIndexStateKey = "UniTaskTrackerTreeView_sortedColumnIndex";

        public IReadOnlyList<TreeViewItem> CurrentBindingItems;

        public UniTaskTrackerTreeView()
            : this(new TreeViewState(), new MultiColumnHeader(new MultiColumnHeaderState(new[]
            {
                new MultiColumnHeaderState.Column() { headerContent = new GUIContent("TaskType"), width = 20},
                new MultiColumnHeaderState.Column() { headerContent = new GUIContent("Elapsed"), width = 10},
                new MultiColumnHeaderState.Column() { headerContent = new GUIContent("Status"), width = 10},
                new MultiColumnHeaderState.Column() { headerContent = new GUIContent("Position")},
            })))
        {
        }

        UniTaskTrackerTreeView(TreeViewState state, MultiColumnHeader header)
            : base(state, header)
        {
            rowHeight = 20;
            showAlternatingRowBackgrounds = true;
            showBorder = true;
            header.sortingChanged += Header_sortingChanged;

            header.ResizeToFit();
            Reload();

            header.sortedColumnIndex = SessionState.GetInt(sortedColumnIndexStateKey, 1);
        }

        public void ReloadAndSort()
        {
            var currentSelected = this.state.selectedIDs;
            Reload();
            Header_sortingChanged(this.multiColumnHeader);
            this.state.selectedIDs = currentSelected;
        }

        private void Header_sortingChanged(MultiColumnHeader multiColumnHeader)
        {
            SessionState.SetInt(sortedColumnIndexStateKey, multiColumnHeader.sortedColumnIndex);
            var index = multiColumnHeader.sortedColumnIndex;
            var ascending = multiColumnHeader.IsSortedAscending(multiColumnHeader.sortedColumnIndex);

            var items = rootItem.children.Cast<UniTaskTrackerViewItem>();

            IOrderedEnumerable<UniTaskTrackerViewItem> orderedEnumerable;
            switch (index)
            {
                case 0:
                    orderedEnumerable = ascending ? items.OrderBy(item => item.TaskType) : items.OrderByDescending(item => item.TaskType);
                    break;
                case 1:
                    orderedEnumerable = ascending ? items.OrderBy(item => double.Parse(item.Elapsed)) : items.OrderByDescending(item => double.Parse(item.Elapsed));
                    break;
                case 2:
                    orderedEnumerable = ascending ? items.OrderBy(item => item.Status) : items.OrderByDescending(item => item.Elapsed);
                    break;
                case 3:
                    orderedEnumerable = ascending ? items.OrderBy(item => item.Position) : items.OrderByDescending(item => item.PositionFirstLine);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(index), index, null);
            }

            CurrentBindingItems = rootItem.children = orderedEnumerable.Cast<TreeViewItem>().ToList();
            BuildRows(rootItem);
        }

        protected override TreeViewItem BuildRoot()
        {
            var root = new TreeViewItem { depth = -1 };

            var children = new List<TreeViewItem>();

            TaskTracker.ForEachActiveTask((trackingId, awaiterType, status, created, stackTrace) =>
            {
                children.Add(new UniTaskTrackerViewItem(trackingId) { TaskType = awaiterType, Status = status.ToString(), Elapsed = (DateTime.UtcNow - created).TotalSeconds.ToString("00.00"), Position = stackTrace });
            });

            CurrentBindingItems = children;
            root.children = CurrentBindingItems as List<TreeViewItem>;
            return root;
        }

        protected override bool CanMultiSelect(TreeViewItem item)
        {
            return false;
        }

        protected override void RowGUI(RowGUIArgs args)
        {
            var item = args.item as UniTaskTrackerViewItem;

            for (var visibleColumnIndex = 0; visibleColumnIndex < args.GetNumVisibleColumns(); visibleColumnIndex++)
            {
                var rect = args.GetCellRect(visibleColumnIndex);
                var columnIndex = args.GetColumn(visibleColumnIndex);

                var labelStyle = args.selected ? EditorStyles.whiteLabel : EditorStyles.label;
                labelStyle.alignment = TextAnchor.MiddleLeft;
                switch (columnIndex)
                {
                    case 0:
                        EditorGUI.LabelField(rect, item.TaskType, labelStyle);
                        break;
                    case 1:
                        EditorGUI.LabelField(rect, item.Elapsed, labelStyle);
                        break;
                    case 2:
                        EditorGUI.LabelField(rect, item.Status, labelStyle);
                        break;
                    case 3:
                        EditorGUI.LabelField(rect, item.PositionFirstLine, labelStyle);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(columnIndex), columnIndex, null);
                }
            }
        }
    }

}

