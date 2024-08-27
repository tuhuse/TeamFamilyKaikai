using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FlyScript : MonoBehaviour
{
   
 
    private void Start() {
      
    }
   
    private void OnTriggerEnter2D(Collider2D collision) {
        
        if (collision.gameObject.CompareTag("Player")) 
        {//ÉvÉåÉCÉÑÅ[Ç∆CPU

            
          collision.gameObject.GetComponent<PlayercontrollerScript>().CallCoroutine();
            this.transform.SetParent(collision.gameObject.transform);
            this.gameObject.SetActive(false);

        }
        if (collision.gameObject.CompareTag("CPU")) {
          
            this.transform.SetParent(collision.gameObject.transform);
            this.gameObject.SetActive(false);
        }
       

    }

}

