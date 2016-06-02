using System;
using System.Collections.Generic; 

public class TestRange {

  public static void Main() {
    var avltree = new AVLTree<int>(); 
    for(int i = 15; i > 0; i--)
      avltree.Insert(i); 

    foreach(var i in avltree.Range(2,14))
      Console.Write(i + " "); 
    Console.WriteLine();
    
    var doubles = new List<int>();
    avltree.MapRange(2,14, x => doubles.Add(2*x));
    foreach (var i in doubles)
      Console.Write(i + " ");
  }
}
