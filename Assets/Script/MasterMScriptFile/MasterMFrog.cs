using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterMFrog : MonoBehaviour {
   
    [Header("スキル発射位置")]
    [SerializeField]
    private Transform _spawn;
    [Header("粘液発射位置")]
    [SerializeField] private Transform _mucusSpawn;

    //ゲームオブジェクト
    [Header("粘液")]
    [SerializeField] private GameObject _mucus;
    [Header("髭")]
    [SerializeField] private GameObject _beard;
    [Header("水玉")]
    [SerializeField] private GameObject _waterBall;
    [Header("プレイヤー")]
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _mucusEffect;
    [SerializeField] private GameObject _waterCpuEffect;
    [SerializeField] private GameObject _pruduction;
    [SerializeField] private GameObject _downEffect;
    [SerializeField] private GameObject _enemyEffect;
    [SerializeField] private GameObject _distancetoCPU1;
    [SerializeField] private GameObject _distancetoCPU2;
    [SerializeField] private GameObject _itemIcon;

    //スクリプト呼び出し
    [SerializeField] private ItemSelects _item;
    [SerializeField] private WireTongueCPU _tongue;
    [SerializeField] private SelectCharacter _select;

    private bool _isAlive = default;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpPower;
    
    private Rigidbody2D _rb;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _isAlive = true;
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
