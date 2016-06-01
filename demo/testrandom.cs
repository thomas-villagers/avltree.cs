using System; 
using System.Linq; 

public class TestAVL {

  public static void Main() {
    var avltree = new AVLTree<int>(); 
    var numbers = Enumerable.Range(1,64).ToList();
    var random = new Random(); 
    while (numbers.Count > 0) {  // we need distinct numbers or my print method will fail
      int nextIndex = random.Next(0, numbers.Count);
      avltree.Insert(numbers[nextIndex]);
      numbers.RemoveAt(nextIndex); 
    }
    avltree.PrintDot(); 
  }
}
