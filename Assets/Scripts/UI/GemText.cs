using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GemText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gemText;
    Vector2 newPos;
    
   // Vector2 newSize;

    // Start is called before the first frame update
    void Start()
    {

        newPos = transform.position;
        Destroy(gameObject,2f);
        // newSize = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
      
            Vector2 pos = new Vector2(newPos.x, newPos.y + 0.5f);
            // Vector3 size = new Vector2(transform.localScale.x*2,transform.localScale.y*2);
            transform.position = Vector2.Lerp(transform.position, pos, Time.deltaTime);
            // transform.localScale= Vector2.Lerp(transform.localScale, size, Time.deltaTime);
        

    }
}
