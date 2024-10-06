using UnityEngine;
using UnityEngine.UI;  // Correct namespace for legacy UI

public class Counter : MonoBehaviour
{
    public Text counterText;  // This should match the type of UI component you are using

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
