using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.CompareTag("Player")) {//ÉvÉåÉCÉÑÅ[Ç∆CPU


          Player2 player=  collision.gameObject.GetComponent<Player2>();
            this.transform.SetParent(collision.gameObject.transform);
            this.gameObject.SetActive(false);
           player._isWaterItem = true;
           player._itemIcon.SetActive(true);
           player._itemSelectScript.ItemIcon(1);
        }

    }
}
