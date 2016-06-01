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
