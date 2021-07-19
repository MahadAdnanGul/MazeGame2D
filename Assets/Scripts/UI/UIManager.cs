using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI time;
    private int elapsedTime;
    // Start is called before the first frame update
    void Start()
    {
        elapsedTime = 0;
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        while(true)
        {
            yield return new WaitForSeconds(1);
            elapsedTime++;
            time.text = "Time: " + elapsedTime;
        }
    }
}
