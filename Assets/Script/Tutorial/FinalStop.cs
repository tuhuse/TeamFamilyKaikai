using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalStop : MonoBehaviour
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
        string player = "Player";
        if (collision.gameObject.CompareTag(player)) {
            collision.gameObject.GetComponent<Player2>().WaitStart();
          

        }
    }
}
