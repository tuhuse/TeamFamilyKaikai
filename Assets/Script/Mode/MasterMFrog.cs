using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterMFrog : MonoBehaviour
{
    [SerializeField] private GameObject _player;


    private bool _isAlive = default;

    [SerializeField] private float _distancePlayer;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpPower;
    
    private Rigidbody2D _rb;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _isAlive = true;
        _distancePlayer = 30f;
    }

    // Update is called once per frame
    void Update()
    {
        DistanceMonoBehaviour();
    }
    private void DistanceMonoBehaviour() {
        if (_isAlive) {
            _rb.velocity = new Vector2(_moveSpeed, _rb.velocity.y);
        }
    }
    private void Jump() {
        _rb.velocity = new Vector2(_rb.velocity.x, _jumpPower);
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Enemy")) {
            Jump();
        }
    }
}
