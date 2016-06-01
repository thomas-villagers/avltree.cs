using System; 
using System.Collections.Generic; 

static class AVLTreeListExtensions {

  public delegate void TraversalDelegate<T>(AVLTree<T>.Node tree, List<T> list); 

  private static void TraversePreorder<T>(AVLTree<T>.Node tree, List<T> list) {
    if (tree.left != null) TraversePreorder(tree.left, list);
    list.Add(tree.value);
    if (tree.right != null) TraversePreorder(tree.right, list); 
  }

  private static void TraversePostorder<T>(AVLTree<T>.Node tree, List<T> list) {
    if (tree.right != null) TraversePostorder(tree.right, list); 
    list.Add(tree.value);
    if (tree.left != null) TraversePostorder(tree.left, list);
  }

  private static void TraverseInorder<T>(AVLTree<T>.Node tree, List<T> list) {
    list.Add(tree.value);
    if (tree.right != null) TraverseInorder(tree.right, list); 
    if (tree.left != null) TraverseInorder(tree.left, list);
  }

  public static TraversalDelegate<T> Postorder<T>(this AVLTree<T> tree) {
    return TraversePostorder<T>; 
  }

  public static TraversalDelegate<T> Preorder<T>(this AVLTree<T> tree) {
    return TraversePreorder<T>; 
  }

  public static TraversalDelegate<T> Inorder<T>(this AVLTree<T> tree) {
    return TraverseInorder<T>; 
  }

  public static List<T> ToList<T>(this AVLTree<T> tree, TraversalDelegate<T> traversalmethod) {
    var list = new List<T>();
    traversalmethod(tree.root, list);
    return list;
  }

  public static List<T> ToList<T>(this AVLTree<T> tree) {
    return tree.ToList<T>(TraversePreorder<T>);
  }
}
