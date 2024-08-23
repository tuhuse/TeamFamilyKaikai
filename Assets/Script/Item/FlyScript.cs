using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FlyScript : MonoBehaviour
{
    public Animator _flyAnimator;
 
    private void Start() {
        _flyAnimator = GetComponent<Animator>();
        
    }
   
    private void OnTriggerEnter2D(Collider2D collision) {
        
        if (collision.gameObject.layer == 12) 
        {//プレイヤーとCPU

            
          collision.gameObject.GetComponent<PlayercontrollerScript>().CallCoroutine();
            this.transform.SetParent(collision.gameObject.transform);
            this.gameObject.SetActive(false);

        }
        if (collision.gameObject.CompareTag("CPU")) {
          
            this.transform.SetParent(collision.gameObject.transform);
            this.gameObject.SetActive(false);
        }
        if (collision.gameObject.layer == 13) {//プレイヤーとCPU
            collision.gameObject.GetComponent<PlayercontrollerScript>().CallCoroutine();
            this.transform.SetParent(collision.gameObject.transform);
            this.gameObject.SetActive(false);

        }
        if (collision.gameObject.layer == 10) {
            collision.gameObject.GetComponent<PlayercontrollerScript>().CallCoroutine();
        }
        if (collision.gameObject.layer == 8) {
            collision.gameObject.GetComponent<PlayercontrollerScript>().CallCoroutine();
        }

    }

}

