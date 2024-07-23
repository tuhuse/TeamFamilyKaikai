using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beard : MonoBehaviour
{
    
    private Rigidbody2D _rb;
    [SerializeField] private float _movebeard;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();        
    }

    // Update is called once per frame
    void Update() {      
            //落下
            _rb.velocity = Vector2.right * _movebeard * 2;
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Flor"))//床に髭当たる
       {
            this.gameObject.SetActive(false);//髭消える
        }
    }
}
