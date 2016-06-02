public class TestTraverse {

  public static void Main() {
    var avltree = new AVLTree<int>(); 
    for(int i = 15; i > 0; i--)
      avltree.Insert(i); 

    foreach(var i in avltree.ToList())
      System.Console.Write(i + " "); 
    System.Console.WriteLine();
    foreach(var i in avltree.ToList(avltree.Postorder()))
      System.Console.Write(i + " "); 
    System.Console.WriteLine();
    foreach(var i in avltree.ToList(avltree.Inorder()))
      System.Console.Write(i + " "); 

  }
}
