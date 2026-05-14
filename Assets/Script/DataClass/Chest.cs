using UnityEngine;
public class Chest : MonoBehaviour 
{
    private string id;
    public string Id { get { return id; } set { id = value; } }

    // È®·ü
    public int e = 0;
    public int d = 0;
    public int c = 0;
    public int b = 0;
    public int a = 0;
    public int s = 0;


    public void Reset() {
        e = 0;
        d = 0;
        c = 0; 
        b = 0; 
        a = 0;
        s = 0;
    }
}
