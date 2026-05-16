using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalancedTreeImplementations
{
    public class AVLTree<T> where T : IComparable<T>
    {
        private AVLNode root;

        // Node definition for the AVL Tree
        // Make the Node class public to allow accessibility
        public class AVLNode
        {
            public T Value { get; set; }
            public AVLNode Left { get; set; }
            public AVLNode Right { get; set; }
            public int Height { get; set; }

            public AVLNode(T value)
            {
                Value = value;
                Height = 1; // Initially, when a node is created, its height is set to 1.
            }
        }

        public void Insert(T value)
        {
            root = Insert(root, value);
        }

        private AVLNode Insert(AVLNode node, T value)
        {
            if (node == null)
                return new AVLNode(value);

            if (value.CompareTo(node.Value) < 0)
                node.Left = Insert(node.Left, value);
            else if (value.CompareTo(node.Value) > 0)
                node.Right = Insert(node.Right, value);
            else
                return node; // Duplicate values are not allowed

            UpdateHeight(node);
            // return node;
            return Balance(node);
        }

        public bool Exists(T value)
        {
            return searchBST(root, value) != null;
        }

        private AVLNode Exist(AVLNode node, T value)
        {
            if (node == null || node.Value.Equals(value))
            {
                return node;
            }
            if (value.CompareTo(node.Value) < 0)
            {
                return Exist(node.Left, value);
            }
            else
            {
                return Exist(node.Right, value);
            }
        }

        public AVLNode searchBST(T value)
        {
            return searchBST(root, value);
        }

        private AVLNode searchBST(AVLNode node, T value)
        {
            if (node == null || node.Value.Equals(value))
            {
                return node;
            }

            if (value.CompareTo(node.Value) < 0)
            {
                return searchBST(node.Left, value);
            }
            else
            {
                return searchBST(node.Right, value);
            }
        }

        public void delete(T value)
        {
            root = delete(root, value);
        }

        private AVLNode delete(AVLNode node, T value)
        {
            // Temporary variable (used later for successor node)
            AVLNode temp = node?.Left;

            // Base case: empty tree or reached null, nothing to delete
            if (node == null)
                return node;

            // 1. Search for the node to delete (same as Insert traversal)
            if (value.CompareTo(node.Value) < 0)          // Value is smaller → go left
            {
                node.Left = delete(node.Left, value);
            }
            else if (value.CompareTo(node.Value) > 0)     // Value is larger → go right
            {
                node.Right = delete(node.Right, value);
            }
            else
            {
                // 2. Node found – handle the three deletion cases

                // Case 1: Leaf node (no children)
                if (node.Left == null && node.Right == null)
                {
                    return null;   // Delete by returning null to the parent
                }

                // Case 2: Only one child
                if (node.Left == null)           // Only right child exists
                    return node.Right;           // Replace node with its right child
                else if (node.Right == null)     // Only left child exists
                    return node.Left;            // Replace node with its left child

                // Case 3: Two children
                // Find the smallest value in the right subtree (in‑order successor)
                temp = GetMinValueNode(node.Right);
                // Copy the successor's value into the current node
                node.Value = temp.Value;
                // Delete the successor from the right subtree (it will be either a leaf or have only a right child)
                node.Right = delete(node.Right, temp.Value);
            }

            // 3. After deletion (or after recursion), update the height of the current node
            UpdateHeight(node);

            // 4. Rebalance the current node (may require single or double rotation)
            return Balance(node);
        }

        // Helper: finds the node with the smallest value in a given subtree
        private AVLNode GetMinValueNode(AVLNode node)
        {
            AVLNode current = node;
            // Keep moving left until no left child exists
            while (current?.Left != null)
                current = current.Left;
            return current;   // This node contains the minimum value in the subtree
        }
        private void UpdateHeight(AVLNode node)
        {
            //this will add 1 to the max height and update the node height.
            node.Height = 1 + Math.Max(Height(node.Left), Height(node.Right));
        }

        private int Height(AVLNode node)
        {
            //this will get the height of the node, incase the node is null it will return 0.
            return node != null ? node.Height : 0;
        }

        private int GetBalanceFactor(AVLNode node)
        {
            return (node != null) ? Height(node.Left) - Height(node.Right) : 0;
        }

        private AVLNode Balance(AVLNode node)
        {

            //this function will balance the node.

            int balanceFactor = GetBalanceFactor(node);

            //decide which rotation to use and work accordengly.

            // RR - Right Rotation Case : Parent BF=-2 , Child BF for right child = -1 or 0
            if (balanceFactor < -1 && GetBalanceFactor(node.Right) <= 0)
                return LeftRotate(node);

            // LL Case: Parent BF=+2 , Child BF for left child = +1 or 0
            if (balanceFactor > 1 && GetBalanceFactor(node.Left) >= 0)
                return RightRotate(node);


            // LR - Left Rotation Case : Parent BF=+2 , Child BF for right child = -1 
            if (balanceFactor > 1 && GetBalanceFactor(node.Left) < 0)
            {
                //Step1: Perform left rotation
                node.Left = LeftRotate(node.Left);
                //Step2: Perfrom Rigth Rotation
                return RightRotate(node);
            }

            // RL - Right-Left Rotation Case : Parent BF=-2 , Child BF for right child = +1
            if (balanceFactor < -1 && GetBalanceFactor(node.Right) > 0)
            {
                //Step1: Perform right rotation
                node.Right = RightRotate(node.Right);
                //Step2: Perfrom Left Rotation
                return LeftRotate(node);
            }

            return node;
        }

        private AVLNode RightRotate(AVLNode OriginalRoot)
        {

            //Remember the algorithm
            // The left child of the node becomes the new root of the subtree.
            // The original root node becomes the right child of the new root.
            // If the new root already had a right child, it becomes the left child of the new right child(the original root).


            // The left child of the node becomes the new root of the subtree.
            AVLNode NewRoot = OriginalRoot.Left;

            //Save the Original Rigth Child Temperorly
            AVLNode OriginalRightChild = NewRoot.Right;


            //The original root node becomes the right child of the new root.
            NewRoot.Right = OriginalRoot;

            // The original root node becomes the right child of the new root.
            OriginalRoot.Left = OriginalRightChild;

            //After the rotation, the heights of the nodes may no longer be correct.
            //These two lines call a method UpdateHeight for
            //both OriginalRoot and NewRoot to recalculate their heights based on the heights of their children.
            //This is crucial for maintaining the balance of the AVL tree.
            UpdateHeight(OriginalRoot);
            UpdateHeight(NewRoot);

            //Finally, the node NewRoot, which is now the root of this subtree after the rotation, is returned.
            return NewRoot;
        }

        private AVLNode LeftRotate(AVLNode OriginalRoot)
        {

            //Remember the algorithm: go back to presentation.
            // The right child of the node becomes the new root of the subtree.
            // The original root node becomes the left child of the new root.
            // If the new root already had a left child, it becomes the right child of the new right child(the original root).

            //Right child of the node becomes the new root of the subtree
            AVLNode NewRoot = OriginalRoot.Right;
            //Save the Original Left Child Temperorly
            AVLNode OriginalLeftChild = NewRoot.Left;

            //Original root node becomes the left child of the new root.
            NewRoot.Left = OriginalRoot;

            //The new root  left child,it becomes the right child of the new right child(the original root)
            OriginalRoot.Right = OriginalLeftChild;

            //After the rotation, the heights of the nodes may no longer be correct.
            //These two lines call a method UpdateHeight for
            //both OriginalRoot and NewRoot to recalculate their heights based on the heights of their children.
            //This is crucial for maintaining the balance of the AVL tree.
            UpdateHeight(OriginalRoot);
            UpdateHeight(NewRoot);

            //Finally, the node NewRoot, which is now the root of this subtree after the rotation, is returned.
            return NewRoot;
        }

        public void PrintTree()
        {
            PrintTree(root, "", true);
        }

        private void PrintTree(AVLNode node, string indent, bool last)
        {
            if (node != null)
            {
                Console.Write(indent);
                if (node == root)
                {
                    Console.Write("Root----");
                    indent += "     ";
                }
                else if (last)
                {
                    Console.Write("R----");
                    indent += "     ";
                }
                else
                {
                    Console.Write("L----");
                    indent += "|    ";
                }
                Console.WriteLine(node.Value);
                PrintTree(node.Left, indent, false);
                PrintTree(node.Right, indent, true);
            }
        }

        //the auto complete code starts here.
        public IEnumerable<string> AutoComplete(string prefix)
        {
            if (typeof(T) != typeof(string))
                throw new InvalidOperationException("AutoComplete is only supported for AVLTree<string>.");

            List<string> results = new List<string>();
            AutoComplete(root, prefix, results);
            results.Sort();
            return results;
        }

        private void AutoComplete(AVLNode node, string prefix, List<string> results)
        {
            if (node == null)
                return;

            if (!(node.Value is string valueStr))
                return; // Should not happen when T==string, but guard anyway.

            if (valueStr.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
            {
                results.Add(valueStr);
                AutoComplete(node.Left, prefix, results);
                AutoComplete(node.Right, prefix, results);
            }
            else
            {
                // If the current node's value does not start with the prefix,
                // the method decides which subtree to explore next based on alphabetical order:
                // If the prefix is lexicographically smaller than the node's value, it recurses into the left subtree, as any potential matches must be in the left due to the properties of the binary search tree (BST).
                // Conversely, if the prefix is larger, it searches the right subtree.

                if (string.Compare(prefix, valueStr, StringComparison.OrdinalIgnoreCase) < 0)
                    AutoComplete(node.Left, prefix, results);
                else
                    AutoComplete(node.Right, prefix, results);
            }
        }
    }
}
