using UnityEngine;
using UnityEngine.UI;  

public class Counter : MonoBehaviour
{
    public Text counterText; 

    private int count = 0;

    void Start()
    {
        InvokeRepeating("UpdateCounter", 1.0f, 1.0f);
    }

    void UpdateCounter()
    {
        count += 100;
        counterText.text = count.ToString();
    }
}
