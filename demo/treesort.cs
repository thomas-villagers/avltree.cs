using System; 
using System.Collections.Generic; 
using System.Diagnostics; 

public class Treesort {

  public static void Main() {

    int n = 1000000;
    var random = new Random();
    var L = new List<int>(n);
    Console.WriteLine("Generating {0} random elements...", n); 
    for(int i = 0; i < n; i++)
      L.Add(random.Next()); 

    var T = new AVLTree<int>();
    Console.WriteLine("Sorting {0} random elements...", n); 
    var sw = Stopwatch.StartNew(); 
    foreach(var i in L) 
      T.Insert(i); 
    var elapsedInsert = sw.ElapsedMilliseconds;
    L = T.ToList(); 
    var elapsedRemove = sw.ElapsedMilliseconds;
    sw.Stop(); 
    Console.WriteLine("Insertion: {0} ToList: {1} Combined: {2}", elapsedInsert, elapsedRemove, elapsedInsert + elapsedRemove); 
  }
}
