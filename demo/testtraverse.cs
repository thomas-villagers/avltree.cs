using System; 
using System.Collections.Generic; 

public class TestTraverse {

  public static void Main() {
    var avltree = new AVLTree<int>(); 
    for(int i = 15; i > 0; i--)
      avltree.Insert(i); 

    foreach(var i in avltree.ToList())
      Console.Write(i + " "); 
    Console.WriteLine();
    foreach(var i in avltree.ToList(avltree.Postorder()))
      Console.Write(i + " "); 
    Console.WriteLine();
    foreach(var i in avltree.ToList(avltree.Inorder()))
      Console.Write(i + " "); 

    Console.WriteLine();
    var doubles = new List<int>();
    avltree.Map(avltree.Preorder(), x => doubles.Add(2*x));
    foreach(var i in doubles)
      Console.Write(i + " ");
  }
}
