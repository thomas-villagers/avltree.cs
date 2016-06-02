public class TestRange {

  public static void Main() {
    var avltree = new AVLTree<int>(); 
    for(int i = 15; i > 0; i--)
      avltree.Insert(i); 

    foreach(var i in avltree.Range(2,14))
      System.Console.Write(i + " "); 
  }
}
