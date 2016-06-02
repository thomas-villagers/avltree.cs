using System;
using System.Collections.Generic; 

public class AVLTree<T> {

  public Node root;
  int numElements;
  public delegate int CompareDelegate(T v1, T v2); 
  public readonly CompareDelegate compare = Comparer<T>.Default.Compare; 

  public int Count {
    get { return numElements; }
  }

  public AVLTree() {
    root = null;
    numElements = 0; 
  }

  public AVLTree(CompareDelegate compare) : this() {
    this.compare = compare; 
  }
  
  public void Insert(T value) {
    numElements++; 
    root = (root == null) ? new Node(value, null) : root.Insert(value, compare); 
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

    public Node Insert(T value, CompareDelegate compare) {
      if (compare(value, this.value) < 0) {
        return Insert(ref left, value, compare); 
      } else {
        return Insert(ref right, value, compare); 
      }
    }

    private Node Insert(ref Node node, T value, CompareDelegate compare) {
      if (node == null) {
        node = new Node(value, this); 
        return node.Rebalance(); 
      }
      else  
        return node.Insert(value, compare);
    }    

    public Node Find(T value, CompareDelegate compare) {
      int cmp = compare(this.value, value);
      if (cmp == 0) return this;
      if (cmp > 0) return left.Find(value,compare);
      return right.Find(value,compare);
    }

    private Node Rebalance() {
      Node v = this;
      Node newRoot = this; 
      bool restructured = false; 
      while (v != null) {
        if (!restructured && Math.Abs(ChildHeight(v.left) - ChildHeight(v.right)) > 1) {
          v = Restructure(v);
          restructured = true; 
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
