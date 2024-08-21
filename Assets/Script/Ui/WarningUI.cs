using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningUI : MonoBehaviour {
    [Header("Šë‹@UI")] [SerializeField] private SpriteRenderer _warning;
    private GameObject _sprite;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Enemy")) {
            StartCoroutine(WarningUIStart());
        }
    }
    private IEnumerator WarningUIStart() {
        _warning.enabled = true;
        yield return new WaitForSeconds(0.8f);
        _warning.enabled = false;
    }
}
