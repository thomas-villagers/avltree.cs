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
