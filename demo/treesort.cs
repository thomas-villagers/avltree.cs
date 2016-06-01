using System; 
using System.Linq; 
using System.Collections.Generic; 
using System.Diagnostics; 

public class Treesort {

  public static void Main() {

    int n = 1000000;
    var random = new Random();
    Console.WriteLine("Generating {0} random elements...", n);
    var numbers = Enumerable.Range(0,n).Select(x => random.Next()); 

    var T = new AVLTree<int>();
    Console.WriteLine("Sorting {0} random elements...", n); 
    var sw = Stopwatch.StartNew(); 
    foreach(var i in numbers) 
      T.Insert(i); 
    var elapsedInsert = sw.ElapsedMilliseconds;
    T.ToList(); 
    var elapsedRemove = sw.ElapsedMilliseconds;
    sw.Stop(); 
    Console.WriteLine("Insertion: {0} ToList: {1} Combined: {2}", elapsedInsert, elapsedRemove, elapsedInsert + elapsedRemove); 
  }
}
