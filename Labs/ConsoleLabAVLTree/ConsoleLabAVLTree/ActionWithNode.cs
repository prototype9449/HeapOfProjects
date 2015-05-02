using System;

namespace ConsoleLabAVLTree
{
    public static class ActionWithNode<TKey, TValue> where TKey : IComparable<TKey>, IEquatable<TKey>
    {
        public static void DeleteNodeWithMoving(SubTree<TKey, TValue> deleteNode)
        {
            var hightOfLeftSubTree = deleteNode.Left != null ? deleteNode.Left.Heigh : 0;
            var hightOfRightSubTree = deleteNode.Right != null ? deleteNode.Right.Heigh : 0;

            if (hightOfLeftSubTree > hightOfRightSubTree)
            {
                SubTree<TKey, TValue> movingNode = deleteNode.Left;
                while (movingNode.Right != null)
                {
                    movingNode = movingNode.Right;
                }

                ChangeAndDelete(deleteNode, movingNode);
            }
            else
            {
                SubTree<TKey, TValue> movingNode = deleteNode.Right;
                while (movingNode.Left != null)
                {
                    movingNode = movingNode.Left;
                }

                ChangeAndDelete(deleteNode, movingNode);
            }

        }

        public static void ChangeAndDelete(SubTree<TKey, TValue> deleteNode, SubTree<TKey, TValue> movingNode)
        {
            DeleteAndTie(movingNode);
            movingNode.Root = deleteNode.Root;
            if (deleteNode.Root != null)
            {
                if (deleteNode.IsLeft())
                {
                    deleteNode.Root.Left = movingNode;
                }
                else
                {
                    deleteNode.Root.Right = movingNode;
                }
            }

            movingNode.Left = deleteNode.Left;

            if (deleteNode.Left != null)
                deleteNode.Left.Root = movingNode;
            
            movingNode.Right = deleteNode.Right;

            if (deleteNode.Right != null)
                deleteNode.Right.Root = movingNode;
            
            
        }

        public static void DeleteAndTie(SubTree<TKey, TValue> deleteNode)
        {
            if (deleteNode.Heigh == 0)
            {
                DeleteLeaf(deleteNode);
                return;
            }

            if (deleteNode.IsLeft())
            {
                if (deleteNode.Left != null)
                {
                    deleteNode.Left.Root = deleteNode.Root;
                    deleteNode.Root.Left = deleteNode.Left;
                    return;
                }
                if (deleteNode.Right != null)
                {
                    deleteNode.Right.Root = deleteNode.Root;
                    deleteNode.Root.Left = deleteNode.Right;
                }
            }
            else
            {
                if (deleteNode.Right != null)
                {
                    deleteNode.Right.Root = deleteNode.Root;
                    deleteNode.Root.Right = deleteNode.Right;
                    return;
                }
                if (deleteNode.Left != null)
                {
                    deleteNode.Left.Root = deleteNode.Root;
                    deleteNode.Root.Right = deleteNode.Left;
                }
            }
            deleteNode.Root.CalculateHeighAndBallance();
        }

        public static void DeleteLeaf(SubTree<TKey, TValue> deleteNode)
        {
            if (deleteNode.IsRight())
            {
                deleteNode.Root.Right = null;
            }
            else
            {
                deleteNode.Root.Left = null;
            }
            deleteNode.Root.CalculateHight();
        }
    }
}