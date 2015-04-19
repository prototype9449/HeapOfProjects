using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace educationProject
{
    public class BaseStackPanel : StackPanel
    {
        protected bool IsModifiable = true;
        protected StackPanel PlaceDeleting;
        private readonly object _locker = new Object();

        internal void ElementPanel_OnDragEnter(object sender, DragEventArgs dragEventArgs)
        {
            lock (_locker)
            {
                try
                {
                    if (!IsModifiable)
                        return;

                    StackPanel gridForDrop;
                    StackPanel draggingGrid;

                    if (sender is TextStackPanel)
                        gridForDrop = (TextStackPanel)sender;
                    else if (sender is TestStackPanel)
                        gridForDrop = (TestStackPanel)sender;
                    else if (sender is ImageStackPanel)
                        gridForDrop = (ImageStackPanel)sender;
                    else
                        throw new ArgumentException();

                    if (dragEventArgs.Data.GetData("educationProject.TestStackPanel") != null)
                    {
                        draggingGrid = dragEventArgs.Data.GetData("educationProject.TestStackPanel") as StackPanel;
                    }
                    else if (dragEventArgs.Data.GetData("educationProject.TextStackPanel") != null)
                    {
                        draggingGrid = dragEventArgs.Data.GetData("educationProject.TextStackPanel") as StackPanel;
                    }
                    else if (dragEventArgs.Data.GetData("educationProject.ImageStackPanel") != null)
                    {
                        draggingGrid = dragEventArgs.Data.GetData("educationProject.ImageStackPanel") as StackPanel;
                    }
                    else
                        throw new ArgumentException();

                    if (gridForDrop == null || draggingGrid == null)
                        throw new ArgumentException();

                    var indexGridForDrop = PlaceDeleting.Children.IndexOf(gridForDrop);
                    var indexDraggingGrid = PlaceDeleting.Children.IndexOf(draggingGrid);

                    if (indexDraggingGrid < 0 || indexGridForDrop < 0)
                        throw new ArgumentException();
                    if (indexGridForDrop < indexDraggingGrid)
                    {
                        ReplacePanels(indexDraggingGrid, draggingGrid, gridForDrop);
                    }
                    else if (indexDraggingGrid < indexGridForDrop)
                    {
                        ReplacePanels(indexGridForDrop, gridForDrop, draggingGrid);
                    }
                }
                catch
                {
                }
            }
        }


        protected void ReplacePanels(int maxIndexOfPanelForReplacing, StackPanel stackPanelWithLargeIndex, StackPanel stackPanelWithSmallerIndex)
        {
            if (stackPanelWithLargeIndex == null || stackPanelWithSmallerIndex == null)
                throw new ArgumentException("Stack panel is null");

            var stackOfPanels = new Stack<StackPanel>();
            var amoutOfPanelsOnEditingGrid = PlaceDeleting.Children.Count;
            for (var i = amoutOfPanelsOnEditingGrid - 1; i > maxIndexOfPanelForReplacing; i--)
            {
                stackOfPanels.Push((StackPanel)PlaceDeleting.Children[i]);
                PlaceDeleting.Children.RemoveAt(i);
            }
            PlaceDeleting.Children.Remove(stackPanelWithLargeIndex);
            PlaceDeleting.Children.Remove(stackPanelWithSmallerIndex);
            PlaceDeleting.Children.Add(stackPanelWithLargeIndex);
            PlaceDeleting.Children.Add(stackPanelWithSmallerIndex);
            while (stackOfPanels.Count != 0)
            {
                PlaceDeleting.Children.Add(stackOfPanels.Pop());
            }
        }

        protected void This_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragDrop.DoDragDrop(this, this, DragDropEffects.Link);
        }
    }
}
