using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GageSwitch1 : MonoBehaviour
{
    [SerializeField] private GameObject _gage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            _gage.SetActive(false);
        }
    }
}
