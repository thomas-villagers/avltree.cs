using System; 
using System.Collections.Generic; 

static class AVLTreeListExtensions {

  public delegate void TraversalDelegate<T>(AVLTree<T>.Node node, CollectDelegate<T> collect); 
  public delegate void CollectDelegate<T>(T value); 
  public delegate int QueryDelegate<T>(T value);

  public static List<T> ToList<T>(this AVLTree<T> tree, TraversalDelegate<T> traversalmethod) {
    var list = new List<T>(tree.Count);
    traversalmethod(tree.root, x => list.Add(x));
    return list;
  }

  public static List<T> ToList<T>(this AVLTree<T> tree) {
    return tree.ToList<T>(TraversePreorder<T>);
  }

  public static List<T> Range<T>(this AVLTree<T> tree, T minValue, T maxValue) {
    var list = new List<T>();
    RangeQuery(tree.root, x => list.Add(x), x => tree.compare(x,minValue), x => tree.compare(maxValue, x));
    return list; 
  }

  public static TraversalDelegate<T> Preorder<T>(this AVLTree<T> tree) {
    return TraversePreorder<T>; 
  }

  public static TraversalDelegate<T> Postorder<T>(this AVLTree<T> tree) {
    return TraversePostorder<T>; 
  }

  public static TraversalDelegate<T> Inorder<T>(this AVLTree<T> tree) {
    return TraverseInorder<T>; 
  }

  private static void TraversePreorder<T>(AVLTree<T>.Node node, CollectDelegate<T> collect) {
    if (node.left != null) TraversePreorder(node.left, collect);
    collect(node.value); 
    if (node.right != null) TraversePreorder(node.right, collect); 
  }

  private static void TraversePostorder<T>(AVLTree<T>.Node node,  CollectDelegate<T> collect) {
    if (node.right != null) TraversePostorder(node.right, collect); 
    collect(node.value);
    if (node.left != null) TraversePostorder(node.left, collect);
  }

  private static void TraverseInorder<T>(AVLTree<T>.Node node, CollectDelegate<T> collect) {
    collect(node.value);
    if (node.left != null) TraverseInorder(node.left, collect);
    if (node.right != null) TraverseInorder(node.right, collect); 
  }

  private static void RangeQuery<T>(AVLTree<T>.Node node, CollectDelegate<T> collect, QueryDelegate<T> traverseLeft, QueryDelegate<T> traverseRight) { 
    int cmpLeft = traverseLeft(node.value);
    if (cmpLeft > 0 &&  node.left != null) 
      RangeQuery(node.left, collect, traverseLeft, traverseRight);
    
    int cmpRight = traverseRight(node.value); 
    if (cmpLeft > 0 && cmpRight > 0) 
      collect(node.value);
    
    if (cmpRight > 0 && node.right != null)  
      RangeQuery(node.right, collect, traverseLeft, traverseRight);
  }
}
