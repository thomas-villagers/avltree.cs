public class TestAVL {

  public static void Main() {
    var avltree = new AVLTree<string>(); 
    avltree.Insert("Jamie"); 
    avltree.Insert("Tywin"); 
    avltree.Insert("Tyrion"); 
    avltree.Insert("Myrcella"); 
    avltree.Insert("Joffrey"); 
    avltree.Insert("Tommen"); 
    avltree.Insert("Cersei"); 

//    var n = avltree.Find("Tywin");
//    System.Console.WriteLine("Found: " + n.value); 

    avltree.PrintDot(); 
  }
}
