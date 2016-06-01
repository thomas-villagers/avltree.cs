<div id="table-of-contents">
<h2>Table of Contents</h2>
<div id="text-table-of-contents">
<ul>
<li><a href="#orgheadline1">1. Balanced binary AVL Tree</a></li>
<li><a href="#orgheadline2">2. Graphviz Output</a></li>
<li><a href="#orgheadline7">3. Demo</a>
<ul>
<li><a href="#orgheadline3">3.1. Inorder Insertion and Single Rotations</a></li>
<li><a href="#orgheadline4">3.2. Double Rotations</a></li>
<li><a href="#orgheadline5">3.3. Strings</a></li>
<li><a href="#orgheadline6">3.4. Random Insertion</a></li>
</ul>
</li>
</ul>
</div>
</div>


# Balanced binary AVL Tree<a id="orgheadline1"></a>

    using System;
    using System.Collections.Generic; 
    
    public class AVLTree<T> {
    
      public Node root;
      int numElements;
      Func<T, T, int> compare; 
    
      public int Count {
        get { return numElements; }
      }
    
      public AVLTree(Func<T, T, int> compare) {
        this.compare = compare; 
        root = null;
        numElements = 0; 
      }
    
      public AVLTree() : this((x,y) => Comparer<T>.Default.Compare(x,y)) { }
    
      public void Insert(T value) {
        numElements++; 
        if (root == null) 
          root = new Node(value, null); 
        else {
          root = root.Insert(value, compare); 
        }
      }
    
      public Node Find(T value) {
        return root.Find(value, compare);
      }
    
      public class Node {
        public readonly T value;
        Node parent;
        public Node left;
        public Node right;
        public int height;
    
        public Node(T value, Node parent) {
          this.value = value;
          this.parent = parent;
          left = null;
          right = null;
          height = 1;
        }
    
        public Node Insert(T value, Func<T, T, int> compare) {
          if (compare(value, this.value) < 0) {
            return Insert(ref left, value, compare); 
          } else {
            return Insert(ref right, value, compare); 
          }
        }
    
        private Node Insert(ref Node node, T value, Func<T, T, int> compare) {
          if (node == null) {
            node = new Node(value, this); 
            return node.Rebalance(); 
          }
          else  
            return node.Insert(value, compare);
        }    
    
        public Node Find(T value, Func<T, T, int> compare) {
          int cmp = compare(this.value, value);
          if (cmp == 0) return this;
          if (cmp > 0) return left.Find(value,compare);
          return right.Find(value,compare);
        }
    
        private Node Rebalance() {
          Node v = this;
          Node newRoot = this; 
          while (v != null) {
            if (Math.Abs(ChildHeight(v.left) - ChildHeight(v.right)) > 1) {
              v = Restructure(v);
            }
            v.height = 1 + v.MaxChildHeight();    
            newRoot = v;
            v = v.parent; 
          }
          return newRoot; 
        }
    
        private static int ChildHeight(Node child) {
          return (child == null) ? 0 : child.height;
        }
    
        private int MaxChildHeight() {
          return Math.Max(ChildHeight(left), ChildHeight(right)); 
        }
    
        private Node ChildWithMaxHeight() {
          return (ChildHeight(left) > ChildHeight(right)) ? left : right;
        }    
    
        private Node Restructure(Node z) {
          var y = z.ChildWithMaxHeight();
          var x = y.ChildWithMaxHeight();
          Node a,b,c; 
          Node T1, T2; 
          if (x == y.left && y == z.left) {
            a = x; b = y; c = z; 
            T1 = a.right;
            T2 = b.right;
          } else if (x == y.right && y == z.right) {
            a = z; b = y; c = x; 
            T1 = b.left;
            T2 = c.left;
          } else if (x == y.left && y == z.right) {
            a = z; b = x; c = y; 
            T1 = b.left;
            T2 = b.right;
          } else {
            a = y; b = x; c = z;
            T1 = b.left;
            T2 = b.right;
          }
          if (z.parent != null) {
            if (z == z.parent.left)
              z.parent.left = b;
            else z.parent.right = b; 
          }
          b.parent = z.parent; 
    
          b.left = a;
          a.parent = b;
          b.right = c;
          c.parent = b;
    
          a.right = T1;
          if (T1 != null) T1.parent = a; 
          c.left = T2;
          if (T2 != null) T2.parent = c; 
          a.height = 1 + a.MaxChildHeight();
          b.height = 1 + b.MaxChildHeight();
          c.height = 1 + c.MaxChildHeight();
          return b;
        }        
      }  
    }

-   **Insertion:** O(log n)
-   **Find:** O(log n)

# Graphviz Output<a id="orgheadline2"></a>

    using System;
    
    public static class AVLTreeExtensions {
    
      private static void PrintNode<T>(T rootValue, T childValue) {
        Console.WriteLine("  \"{0}\" -> \"{1}\"", rootValue, childValue);
      }
    
      private static void PrintNode<T>(T value, int empties) {
        Console.WriteLine("  empty{0} [label=\"\", style=invis];", empties);
        Console.WriteLine("  \"{0}\" -> empty{1}", value,  empties);
      }
    
      private static void PrintSubTree<T>(AVLTree<T>.Node node, ref int empties) {
    
        if (node.left == null && node.right == null) {
          Console.WriteLine("  \"{0}\" [shape=rectangle,xlabel={1}];", node.value,node.height);
          return;
        }
        Console.WriteLine("  \"{0}\" [xlabel={1}];", node.value,node.height);
    
        if (node.left != null) {
          PrintNode(node.value, node.left.value);
          PrintSubTree(node.left, ref empties);
        } else if (node.right != null) {
          PrintNode(node.value, empties++);
        }
    
        if (node.right != null) {
          PrintNode(node.value, node.right.value);
          PrintSubTree(node.right, ref empties);
        } else if (node.left != null) {
          PrintNode(node.value, empties++);
        }
    
      }
    
      public static void PrintDot<T>(this AVLTree<T> tree) {
        Console.WriteLine("digraph G {\n  forcelabels=true;");
        int empties = 0;
        PrintSubTree(tree.root, ref empties); 
        Console.WriteLine("}"); 
      }
    }

# Demo<a id="orgheadline7"></a>

## Inorder Insertion and Single Rotations<a id="orgheadline3"></a>

    public class TestAVL {
    
      public static void Main() {
        var avltree = new AVLTree<int>(); 
        for(int i = 15; i > 0; i--)
          avltree.Insert(i); 
    
        avltree.PrintDot(); 
      }
    }

    mcs demo/testdot.cs src/avltreeextensions.cs src/avltree.cs
    mono demo/testdot.exe

![img](images/avltree.png)

## Double Rotations<a id="orgheadline4"></a>

    public class TestAVL {
    
      public static void Main() {
        var avltree = new AVLTree<int>(); 
        avltree.Insert(1); 
        avltree.Insert(3); 
        avltree.Insert(2); 
        avltree.Insert(5); 
        avltree.Insert(4); 
        avltree.Insert(7); 
        avltree.Insert(6); 
        avltree.Insert(9); 
        avltree.Insert(8); 
        avltree.Insert(11); 
        avltree.Insert(10); 
        avltree.Insert(13); 
        avltree.Insert(12); 
        avltree.Insert(15); 
        avltree.Insert(14); 
        avltree.PrintDot(); 
      }
    }

    mcs demo/testdot2.cs src/avltreeextensions.cs src/avltree.cs
    mono demo/testdot2.exe

![img](images/avltree2.png)

## Strings<a id="orgheadline5"></a>

    public class TestAVL {
    
      public static void Main() {
        var avltree = new AVLTree<string>(); 
        avltree.Insert("Jamie"); 
        avltree.Insert("Tywin"); 
        avltree.Insert("Tyrion"); 
        avltree.Insert("Myrcella"); 
        avltree.Insert("Joffrey"); 
        avltree.Insert("Tomnen"); 
        avltree.Insert("Cersei"); 
    
    //    var n = avltree.Find("Tywin");
    //    System.Console.WriteLine("Found: " + n.value); 
    
        avltree.PrintDot(); 
      }
    }

    mcs demo/teststrings.cs src/avltreeextensions.cs src/avltree.cs
    mono demo/teststrings.exe

![img](images/avltree3.png)

## Random Insertion<a id="orgheadline6"></a>

Sibling heights should only differ by 1: 

    using System; 
    using System.Linq; 
    
    public class TestAVL {
    
      public static void Main() {
        var avltree = new AVLTree<int>(); 
        var numbers = Enumerable.Range(1,64).ToList();
        var random = new Random(); 
        while (numbers.Count > 0) {  // we need distinct numbers or my print method will fail
          int nextIndex = random.Next(0, numbers.Count);
          avltree.Insert(numbers[nextIndex]);
          numbers.RemoveAt(nextIndex); 
        }
        avltree.PrintDot(); 
      }
    }

    mcs demo/testrandom.cs src/avltreeextensions.cs src/avltree.cs
    mono demo/testrandom.exe

![img](images/avltree4.png)