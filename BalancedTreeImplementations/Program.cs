using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalancedTreeImplementations
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AVLTree<int> Avltree = new AVLTree<int>();

            int[] valuesAV = { 40, 30, 50, 20, 25, 45, 55, 42, 52 };
            Console.WriteLine("Now we will test the AVL Tree implementation");
            foreach (var value in valuesAV)
            {
                Console.WriteLine($"Inserting {value} into the AVL tree.");
                Avltree.Insert(value);
                Avltree.PrintTree();
                Console.WriteLine("\n-------------------------------------------------\n");
            }

            Console.WriteLine($"Deletion 30 into the AVL tree.");
            Avltree.delete(50);
            Avltree.PrintTree();

            int searchvalue = 25;
            bool exists = Avltree.Exists(searchvalue);
            Console.WriteLine($"\nExists for value {searchvalue}: " + (exists ? "Found" : "Not Found"));

            var Foundnode = Avltree.searchBST(searchvalue);
            Console.WriteLine($"Searching for value {searchvalue}: " + (Foundnode != null ? $"Found node with value {Foundnode.Value}" : $"Not Found node"));

            // Auto-complete feature demonstration
            AVLTree<string> treeAuto = new AVLTree<string>();
            string[] words = { "Ahmad", "Mohammed", "Motasem", "Mohab", "Abla", "Abeer", "Abdullah", "Abbas", "Montaser", "Khalil", "Khalid", "Bob", "carle" };

            foreach (var word in words)
            {
                treeAuto.Insert(word);
            }
            treeAuto.PrintTree();

            Console.WriteLine("\nEnter a prefix to search:\n");
            string prefix = Console.ReadLine();
            var completions = treeAuto.AutoComplete(prefix);

            Console.WriteLine($"\nSuggestions for '{prefix}':\n");
            foreach (var completion in completions)
            {
                Console.WriteLine(completion);
            }

            Console.WriteLine("\n=====================================================\n");
            Console.WriteLine("Now we will test the Red-Black Tree implementation\n");

            RedBlackTree<int> rbTree = new RedBlackTree<int>();

            int[] valuesRB = { 20, 10, 30, 5, 15, 25, 35, 3, 7, 12, 17, 22, 27, 32, 37, 1, 2 };

            foreach (var value in valuesRB)
            {
                //  Console.WriteLine($"Inserting {value} to the tree\n");
                rbTree.Insert(value);

            }
            rbTree.PrintTree();
            Console.WriteLine("\n--------------------------------\n");

            // Search for a value in the tree
            int searchValue = 15;
            RedBlackTree<int>.Node foundNode = rbTree.Find(searchValue);
            if (foundNode != null)
            {
                Console.WriteLine($"Node with value {searchValue} found with color {(foundNode.IsRed ? "RED" : "BLACK")}");
            }
            else
            {
                Console.WriteLine($"Node with value {searchValue} not found");
            }

            searchValue = 10;
            foundNode = rbTree.Find(searchValue);
            if (foundNode != null)
            {
                Console.WriteLine($"Node with value {searchValue} found with color {(foundNode.IsRed ? "RED" : "BLACK")}");
            }
            else
            {
                Console.WriteLine($"Node with value {searchValue} not found");
            }
            Console.WriteLine("\n--------------------------------\n");

            //Deleting Node 20
            int valueToDelete = 2;
            do
            {
                if (rbTree.Delete(valueToDelete))
                {
                    Console.WriteLine("After deleting :" + valueToDelete);
                    rbTree.PrintTree();
                }
                else
                    Console.WriteLine("No node deleted could not find " + valueToDelete);
                Console.WriteLine("Enter value to delete (Enter 0 to exit):");
                valueToDelete = Convert.ToInt32(Console.ReadLine());

            } while (valueToDelete > 0);

            Console.WriteLine("\n--------------------------------\n");


        }
    }
}
