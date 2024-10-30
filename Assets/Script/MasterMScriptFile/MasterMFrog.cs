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
    private const float MOVESPEED = 100f;
    private const float MOVEJUMPMAX = 200f;

    private int _randomValue;
    private const int MAXVALUE = 10000;
    private const int MINVALUE = 0;
    private const float JUMPMIN = 17f;//粘液踏んだ時のジャンプ力
    private const float MOVEJUMP = 35f;

    private bool _isJumping = false;
    private bool _isMucusJump = false;

    private Rigidbody2D _rb;

    private enum ItemProbability {
        First,
        Second
    }
    private enum Item {
        Water,
        Invincible,
        Beard,
        Mucus,
        Null
    }
    private ItemProbability _itemProbability = default;
    private Item _getItem = default;
    // Start is called before the first frame update
    void Start() {
        _rb = GetComponent<Rigidbody2D>();
        _isAlive = true;
    }
    private void FixedUpdate() {
        //ジャンプ降下処理
        if (_isJumping && !_isMucusJump) {

            _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y - (_jumpPower / MOVEJUMP));//* Time.deltaTime)
        }
        if (_isJumping && _isMucusJump) {
            _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y - (_jumpPower / JUMPMIN));//* Time.deltaTime)
        }
    }
    // Update is called once per frame
    void Update() {
        RankChange();
        DistanceMonoBehaviour();
        UseItem();
    }
    private void DistanceMonoBehaviour() {
        if (_isAlive) {
            _rb.velocity = new Vector2(_moveSpeed, _rb.velocity.y);
        }
    }
    public void Jump() {
        _rb.velocity = new Vector2(_rb.velocity.x, _jumpPower);
    }

    private void RankChange() {
        float player = _player.transform.localPosition.x;
        float mySelf = this.transform.localPosition.x;

        if (mySelf > player) {
            _itemProbability = ItemProbability.First;
        } else {
            _itemProbability = ItemProbability.Second;
        }
    }
    private void ItemLottery() {
        _randomValue = Random.Range(MINVALUE, MAXVALUE);
        if (_itemProbability == ItemProbability.First) {
            if (_randomValue >= 6000) {
                _getItem = Item.Water;
            } else if (_randomValue >= 4000) {
                _getItem = Item.Invincible;
            } else if (_randomValue >= 3000) {
                _getItem = Item.Beard;
            } else {
                _getItem = Item.Mucus;
            }
        } else if (_itemProbability == ItemProbability.Second) {
            if (_randomValue >= 1000) {
                _getItem = Item.Water;
            } else if (_randomValue >= 800) {
                _getItem = Item.Invincible;
            } else {
                _getItem = Item.Beard;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Enemy")) {
            Jump();
        }
        if (collision.gameObject.CompareTag("Fly")) {
            StartCoroutine(GetItem());
        }
    }
    private void UseItem() {
        switch (_getItem) {
            case Item.Water:
                if (_itemProbability == ItemProbability.First) {
                    Instantiate(_beard, _spawn.position, Quaternion.identity);
                    _getItem = Item.Null;
                } else {
                    Instantiate(_beard, _spawn.position, Quaternion.identity);
                    _getItem = Item.Null;
                }
                break;
            case Item.Invincible:
                if (_itemProbability == ItemProbability.First) {
                    Instantiate(_beard, _spawn.position, Quaternion.identity);
                    _getItem = Item.Null;
                } else {
                
                }
                break;
            case Item.Beard:
                if (_itemProbability == ItemProbability.First) {
                    Instantiate(_beard, _spawn.position, Quaternion.identity);
                    _getItem = Item.Null;
                }
                break;

            case Item.Mucus:
                if (_itemProbability == ItemProbability.First) {
                    Instantiate(_beard, _spawn.position, Quaternion.identity);
                    _getItem = Item.Null;
                }
                break;
            case Item.Null:

                break;

        }
    }
    private IEnumerator GetItem() {
        int waitTime = 2;
        yield return new WaitForSeconds(waitTime);
        ItemLottery();
    }

}
