public class TestTraverse {

  public static void Main() {
    var avltree = new AVLTree<int>(); 
    for(int i = 15; i > 0; i--)
      avltree.Insert(i); 

    foreach(var i in avltree.ToList())
      System.Console.WriteLine(i); 
    foreach(var i in avltree.ToList(avltree.Postorder()))
      System.Console.WriteLine(i); 

  }
}
