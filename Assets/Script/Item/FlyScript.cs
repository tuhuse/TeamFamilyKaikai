using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FlyScript : MonoBehaviour
{
    public Animator _flyAnimator;
    [SerializeField] private PlayercontrollerScript _playerScript;
 
    private void Start() {
        _flyAnimator = GetComponent<Animator>();

    }
   
    private void OnTriggerEnter2D(Collider2D collision) {
        
        if (collision.gameObject.layer == 12 || collision.gameObject.CompareTag("CPU")) {//ÉvÉåÉCÉÑÅ[Ç∆CPU

            this.transform.SetParent(collision.gameObject.transform);
            this.gameObject.SetActive(false);

        }

    }

}

