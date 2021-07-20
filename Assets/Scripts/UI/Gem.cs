using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    [SerializeField] GameObject gem;
    [SerializeField] GameObject gemCanvas;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Destroy(gem);
            collision.gameObject.GetComponent<Player>().CollectGem();
            gemCanvas.SetActive(true);
            Destroy(gemCanvas, 2f);
            Destroy(gameObject, 2f);
        }
    }
}
