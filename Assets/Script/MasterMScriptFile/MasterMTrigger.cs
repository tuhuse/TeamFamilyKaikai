using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterMTrigger : MonoBehaviour
{
    [SerializeField] private MasterMFrog _masterFrog;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Enemy")) {
            _masterFrog.Jump();
        }
    }
}
