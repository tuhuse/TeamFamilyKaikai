using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayercontrollerScript : MonoBehaviour {
    [SerializeField] private GameObject _downEffect;
    [SerializeField] private GameObject _tongue;
    [SerializeField] private GameObject _mucusEffect;
    [SerializeField] private GameObject _pruduction;
    [SerializeField] private GameObject _enemyEffect;
    [SerializeField] private GameObject _itemIcon = default;
    [SerializeField] private GameObject _player = default;

    public GameObject _projectile = default;

    [Header("髭、粘液アイテム発射位置")] [SerializeField] private Transform _spawn = default;

    [Header("髭")] [SerializeField] private GameObject _beard = default;

    [Header("粘液")] [SerializeField] private GameObject _mucus;

    [Header("水玉")] [SerializeField] private GameObject _waterBall;

    [SerializeField] private GameObject _xButton;

    [Header("水玉発射位置")] [SerializeField] private Transform _waterSpawn;

    [Header("スピードアップエフェクト")] [SerializeField] public GameObject _waterEffect;

    [SerializeField] private GameObject _brake;
    private Rigidbody2D _rb;

    private bool _isAlive = false;
    private bool _isJump = false;
    private bool _isOneshot = false;
    private bool _isJumping = false;

    private bool _isBeardItem = false;
    private bool _isWaterItem = false;
    private bool _isPridictionItem = false;
    private bool _isMucasItem = false;
    private bool _isGetItem = false;
    public bool _isInvincivle = false;
    private bool _isMucusJump = false;
    private bool _isFrogjump = false; //斜め飛びをしているか
    public bool _isGetWater = false;
    private bool _isShine = false;
    private string _nameJoyStick = default;

    [SerializeField] private int _playernumber;// プレイヤー番号（1から始まる）
    [SerializeField] private int _rank = default;
    [SerializeField]
    private int _joynumber;

    private float _downMultipl = 1f;　//障害物に当たった時の抵抗力
    private float _downSpeed;　//実際の減少するスピード
    private float _returnSpeed = 0.035f;//スピードダウンから立て直す速さ
    private float _randomItemLottery = default;//アイテムで使うRandom.Rangeの値を入れる
    private float _speedUp = 0;

    [Header("プレイヤー速度")] [SerializeField] private float _movespeed = 100;
    [Header("プレイヤージャンプ")] [SerializeField] private float _jumppower = 200f;

    private const float WATERSPEEDUPMULTIPLE = 1.2f; //水アイテム使用時のスピードアップ倍率
    private const float MOVESPEED = 100f;//プレイヤーのスピードの基準
    private const float JUMPMIN = 10f;//粘液踏んだ時のジャンプ力
    private const float JUMPMAX = 200f;
    private const float SPEEDMIN = 60f;
    private const float SPEEDRESETWAITTIME = 2f; //スピードアップしている時間
    private const float INVINCIBLETIME = 5f; //無敵時間

    private const float MAXRANDOMRANGE = 10001;
    private const float MINRANDOMRANGE = 1;

    private const float ITEMSELECTWAIT = 3f;
    private const float MOVEJUMP = 35f;

    private const float TIMEDELTATIME = 1000f;

    private SpriteRenderer _pridictionSpriterenderer = default;
    [SerializeField] private ItemSelects _itemSelectScript = default;


    [SerializeField] AudioClip _jumpSE = default;
    [SerializeField] AudioClip _speeddownSE = default;
    [SerializeField] AudioClip _damageSE = default;

    [SerializeField] AudioClip _beardSE = default;
    [SerializeField] AudioClip _waterSE = default;
    [SerializeField] AudioClip _pridictionSE = default;
    [SerializeField] AudioClip _mucasSE = default;

    private AudioSource _frogSE = default;

    private Vector3 _cpuposition = default;

    // Start is called before the first frame update

    public Animator _pridictionFrogAnim;

    private enum Sitiuation {
        Stay,
        Run,
        Jump,
        Brake
    }
    private Sitiuation _sitiuation = default;
    void Start() {


        _frogSE = this.GetComponent<AudioSource>();
        _pridictionSpriterenderer = this.GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();
        StartCoroutine(StartWait());
        _pridictionFrogAnim = this.GetComponent<Animator>();


    }

    private void FixedUpdate() {
        if (!_isJump && !_isMucusJump) {
            _brake.SetActive(false);
            //_jumppower -= JUMPMAX / 40f;
            _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y - (_jumppower / MOVEJUMP));//* Time.deltaTime)
        }
        if (!_isJump && _isMucusJump) {
            _brake.SetActive(false);
            _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y - (_jumppower / JUMPMIN));//* Time.deltaTime)
        }
    }
    // Update is called once per frame
    void Update() {

        HandlePlayerInput(_playernumber);

    }
    void HandlePlayerInput(int playerNumber) {

        string xbutton = _playernumber + "pA";
        if (!_isAlive) {
            return;
        }
        if (_isAlive && !_isFrogjump)//生きてる時に動けるように
        {
            // 割り当てられたコントローラーの入力を処理
            //if (!string.IsNullOrEmpty(_nameJoyStick)) {
            // 左スティックの水平入力を取得
            float horizontalInput = Input.GetAxis(_joynumber + "pLstickHorizontal");
            //移動
            if (Input.GetKey(KeyCode.A) || horizontalInput < 0) {
                _brake.SetActive(true);

                MoveLeftControll();
            }
            if (Input.GetKey(KeyCode.D) || horizontalInput > 0) {

                _brake.SetActive(false);
                MoveRightControll();
            }
            //ジャンプ


            if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown(xbutton)) {

                if (_isJump) {

                    _isJump = false;

                    _frogSE.PlayOneShot(_jumpSE);
                    _rb.velocity = new Vector2(_rb.velocity.x, _jumppower);
                }
            }

            //アイテム取得後

            //髭が出たら
            if (_isBeardItem) {
                Beard();

            }

            //水が出たら
            if (_isWaterItem) {
                Water();

            }

            //無敵が出たら
            if (_isPridictionItem) {
                Pridiction();

            }

            //粘液が出たら
            if (_isMucasItem) {
                Mucas();

            }

            //}


            if (!_isJump && !_isJumping) {
                _isJump = true;
            }
            //else {
            //    _isJump = false;
            //}

            if (_movespeed <= MOVESPEED - 10) {

                if (!_isOneshot) {
                    SpeedDownSE();
                    //移動速度を徐々に元に戻す
                    _downEffect.gameObject.SetActive(true);
                    _isOneshot = true;

                }

            }
            if (this._rb.velocity.x != 0) {
                if (_movespeed <= MOVESPEED) {
                    _movespeed = Mathf.Abs(_movespeed) + (_returnSpeed * Time.deltaTime * TIMEDELTATIME);
                } else {
                    SEReproduction();
                    _downEffect.gameObject.SetActive(false);
                }


            } else {
                _brake.SetActive(false);
                _pridictionFrogAnim.SetBool("Brake", false);
                _pridictionFrogAnim.SetBool("Run", false);
                _pridictionFrogAnim.SetBool("ShineRun", false);
                _pridictionFrogAnim.SetBool("ShineBrake", false);
                _pridictionFrogAnim.SetBool("ShineJump", false);
                //_pridictionFrogAnim.SetBool("Jump", false);

            }

        }
        if (_isShine) {
            ShineTime();
        } else {
            _pridictionFrogAnim.SetBool("ShineRun", false);
            _pridictionFrogAnim.SetBool("ShineBrake", false);
            _pridictionFrogAnim.SetBool("ShineJump", false);
        }

    }
    public void ShineAnime(bool sine) {
        if (sine) {
            _isShine = true;
        } else {
            _isShine = false;
        }
    }
    private void ShineTime() {
        string run = "Run";
        string shineRun = "ShineRun";
        string jump = "Jump";
        string shineJump = "ShineJump";
        string brake = "Brake";
        string shineBrake = "ShineBrake";
        if (_pridictionFrogAnim.GetBool(run)) {
            _pridictionFrogAnim.SetBool(shineRun, true);
            _pridictionFrogAnim.SetBool(shineBrake, false);
            _pridictionFrogAnim.SetBool(shineJump, false);
        } else if (_pridictionFrogAnim.GetBool(jump)) {
            _pridictionFrogAnim.SetBool(shineJump, true);
            _pridictionFrogAnim.SetBool(shineBrake, false);
            _pridictionFrogAnim.SetBool(shineRun, false);
        } else if (_pridictionFrogAnim.GetBool(brake)) {
            _pridictionFrogAnim.SetBool(shineBrake, true);
            _pridictionFrogAnim.SetBool(shineRun, false);
            _pridictionFrogAnim.SetBool(shineJump, false);
        }



    }
   
    private void MoveLeftControll() {
        ////反対に向かせる
        //if (!_pridictionSpriterenderer.flipX) {
        //    _pridictionSpriterenderer.flipX = true;
        //}
        if (!_pridictionFrogAnim.GetBool("Jump")) {

            _pridictionFrogAnim.SetBool("Brake", true);
            _pridictionFrogAnim.SetBool("Run", false);
        } else {
            _pridictionFrogAnim.SetBool("Brake", false);
            _pridictionFrogAnim.SetBool("Run", false);
        }

        float brake = -50;
        //通常の移動
        _rb.velocity = new Vector3(_movespeed + brake, _rb.velocity.y, 0); //* Time.deltaTime ;
    }
    private void MoveRightControll() {
        //反対に向かせる
        if (_pridictionSpriterenderer.flipX) {
            _pridictionSpriterenderer.flipX = false;
        }
        //通常の移動       
        if (!_pridictionFrogAnim.GetBool("Jump")) {
            _pridictionFrogAnim.SetBool("Brake", false);
            _pridictionFrogAnim.SetBool("Run", true);
        } else {
            _pridictionFrogAnim.SetBool("Brake", false);
            _pridictionFrogAnim.SetBool("Run", false);
        }

        _rb.velocity = new Vector3(_movespeed + _speedUp, _rb.velocity.y, 0); //* Time.deltaTime ;



    }
    private void OnCollisionStay2D(Collision2D collision) {
        //床に足が着いてるとき
        if (collision.gameObject.CompareTag("Flor")) {
            _rb.gravityScale = 10;
            _jumppower = JUMPMAX;
            _pridictionFrogAnim.SetBool("Jump", false);
            _isJumping = false;

        }


    }


    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Flor") {
            _isFrogjump = false;

        }

    }

    private void OnCollisionExit2D(Collision2D collision) {

        //床から足が離れたとき
        if (collision.gameObject.CompareTag("Flor")) {
            //_isJump = false;
            _isJumping = true;
            _isJump = false;
            _pridictionFrogAnim.SetBool("Jump", true);
            if (_player.activeSelf) {
                StartCoroutine(JUMP());
            }


        }

    }
    private IEnumerator JUMP() {
        yield return new WaitForSeconds(0.28f);
        _rb.gravityScale = 40f;
    }
    private void SpeedDownSE() {
        if (!_isOneshot) {
            _isOneshot = true;
            _frogSE.PlayOneShot(_speeddownSE);
        }
    }

    private void SEReproduction() {
        if (_isOneshot) {
            _isOneshot = false;

        }

    }

    public void CallCoroutine() {
        StartCoroutine(RandomItem());

    }



    public IEnumerator RandomItem() //アイテム抽選
    {
        if (!_isGetItem) {

            _itemIcon.SetActive(true);
            _itemSelectScript.ItemIcon(0);
            //アイテムを持った状態に変える
            _isGetItem = true;


            yield return new WaitForSeconds(ITEMSELECTWAIT);

            //アイテムを持っていなかったら抽選する






            //４位の時
            if (_rank == 4) {
                _randomItemLottery = Random.Range(MINRANDOMRANGE, MAXRANDOMRANGE);

                //８０％で水
                if (_randomItemLottery >= 2001) {
                    _isWaterItem = true;
                    _itemSelectScript.ItemIcon(1);
                }
                //１５％でひげ
                else if (_randomItemLottery >= 500) {
                    _itemSelectScript.ItemIcon(2);
                    _isBeardItem = true;
                }
                //５％で無敵
                else {
                    _itemSelectScript.ItemIcon(3);
                    _isPridictionItem = true;
                }

            }
            //３位の時
            else if (_rank == 3) {
                _randomItemLottery = Random.Range(MINRANDOMRANGE, MAXRANDOMRANGE);

                //５０％で水
                if (_randomItemLottery >= 5001) {
                    _itemSelectScript.ItemIcon(1);
                    _isWaterItem = true;
                }
                //30％でひげ
                else if (_randomItemLottery >= 2001) {
                    _itemSelectScript.ItemIcon(2);
                    _isBeardItem = true;
                }
                //１５％で無敵
                else if (_randomItemLottery >= 501) {
                    _itemSelectScript.ItemIcon(3);
                    _isPridictionItem = true;
                }
                //５％でねんえき
                else {
                    _itemSelectScript.ItemIcon(4);
                    _isMucasItem = true;
                }

            }
            //２位の時
            else if (_rank == 2) {
                _randomItemLottery = Random.Range(MINRANDOMRANGE, MAXRANDOMRANGE);

                //５０％でひげ
                if (_randomItemLottery >= 5001) {
                    _itemSelectScript.ItemIcon(2);
                    _isBeardItem = true;
                }
                //２０％で水 
                else if (_randomItemLottery >= 3001) {
                    _itemSelectScript.ItemIcon(1);
                    _isWaterItem = true;
                }
                //２０％で無敵 
                else if (_randomItemLottery >= 1001) {
                    _itemSelectScript.ItemIcon(3);
                    _isPridictionItem = true;
                }
                //１０％で粘液
                else {
                    _itemSelectScript.ItemIcon(4);
                    _isMucasItem = true;
                }

            }
            //１位の時
            else if (_rank == 1) {
                _randomItemLottery = Random.Range(MINRANDOMRANGE, MAXRANDOMRANGE);

                //６０％で粘液
                if (_randomItemLottery >= 4001) {
                    _itemSelectScript.ItemIcon(4);
                    _isMucasItem = true;
                }
                //３０で無敵
                else if (_randomItemLottery >= 1001) {
                    _itemSelectScript.ItemIcon(3);
                    _isPridictionItem = true;
                }
                //５％でひげ
                else if (_randomItemLottery >= 501) {
                    _itemSelectScript.ItemIcon(2);
                    _isBeardItem = true;
                }
                //５％で水
                else {
                    _itemSelectScript.ItemIcon(1);
                    _isWaterItem = true;
                }

            }
        }
    }

    private void Beard() {
        _xButton.SetActive(true);
        if (Input.GetKeyDown(KeyCode.G) || Input.GetButtonDown(_joynumber + "pX")) {
            _xButton.SetActive(false);
            _itemIcon.SetActive(false);
            _frogSE.PlayOneShot(_beardSE);

            //髭の生成
            _projectile = Instantiate(_beard, _spawn.position, Quaternion.identity);

            //アイテム取る前の状態にリセット
            _isGetItem = false;
            _isBeardItem = false;
        }
    }

    private void Mucas() {
        _xButton.SetActive(true);
        if (Input.GetKeyDown(KeyCode.G) || Input.GetButtonDown(_joynumber + "pX")) {
            _xButton.SetActive(false);
            _itemIcon.SetActive(false);
            _frogSE.PlayOneShot(_mucasSE);

            //粘液の生成
            _projectile = Instantiate(_mucus, _waterSpawn.position, Quaternion.identity);

            //アイテム取る前の状態にリセット
            _isGetItem = false;
            _isMucasItem = false;
        }
    }

    private void Pridiction() {
        _xButton.SetActive(true);
        if (Input.GetKeyDown(KeyCode.G) || Input.GetButtonDown(_joynumber + "pX")) {
            _xButton.SetActive(false);
            _itemIcon.SetActive(false);
            _frogSE.PlayOneShot(_pridictionSE);

            //カエルを無敵状態にする
            _pruduction.SetActive(true);
            _isInvincivle = true;

            //アイテム取る前の状態にリセット
            _isGetItem = false;
            _isPridictionItem = false;
            StartCoroutine(InvincibleEnd());
        }
    }

    private void Water() {
        _xButton.SetActive(true);
        if (Input.GetKeyDown(KeyCode.G) || Input.GetButtonDown(_joynumber + "pX")) {
            _xButton.SetActive(false);
            _itemIcon.SetActive(false);
            _frogSE.PlayOneShot(_waterSE);

            //水玉生成
            _projectile = Instantiate(_waterBall, _waterSpawn.position, Quaternion.identity, this.transform);
            //↓
            //OnTriggerEnter2D内のcollision.gameobject.layer == 11内の処理に続く

            //アイテム取る前の状態にリセット
            _isGetItem = false;
            _isWaterItem = false;

            _isGetWater = true;
        }
    }


    public void BeardCollision() {

        _frogSE.PlayOneShot(_damageSE);
        _movespeed = SPEEDMIN;
    }
    public void MucusCollision() {

        _isMucusJump = true;
        StartCoroutine(MucusJumpTime());
    }


    private void OnTriggerEnter2D(Collider2D collision) {

        //水玉に当たった時（スピードアップ）
        if (collision.gameObject.layer == 11 && _projectile != null && collision.gameObject == _projectile.gameObject) {
            _movespeed = MOVESPEED * WATERSPEEDUPMULTIPLE;
            _waterEffect.gameObject.SetActive(true);
            collision.gameObject.SetActive(false);

            StartCoroutine(SpeedUpReset());
        }
    }


    private IEnumerator MucusJumpTime() {
        _mucusEffect.SetActive(true);
        yield return new WaitForSeconds(3);
        _isMucusJump = false;
        _mucusEffect.SetActive(false);
    }

    public void ObstacleCollision(float speedDownValue) {
        //無敵じゃなかったら
        if (!_isInvincivle && !_isGetWater) {
            //障害物に当たった時

            _frogSE.PlayOneShot(_damageSE);
            _movespeed = speedDownValue;
        }
    }

    private IEnumerator SpeedUpReset() {

        //３秒後、スピードアップを止める
        yield return new WaitForSeconds(SPEEDRESETWAITTIME);
        {
            _isGetWater = false;
            _waterEffect.gameObject.SetActive(false);

            if (_movespeed >= MOVESPEED) {
                _movespeed = MOVESPEED;
            }

        }
    }

    private IEnumerator InvincibleEnd() {
        //5秒間の間、無敵になる
        yield return new WaitForSeconds(INVINCIBLETIME);
        //５秒たったら元に戻す
        _isInvincivle = false;
        _pruduction.SetActive(false);

    }

    private IEnumerator StartWait() {
        _tongue.SetActive(false);
        yield return new WaitForSeconds(3);
        _tongue.SetActive(true);
        _isAlive = true;


    }
    private IEnumerator CollisionEffect() {
        _enemyEffect.SetActive(true);
        yield return new WaitForSeconds(1);
        _enemyEffect.SetActive(false);
        SpeedDown(false);
    }

    public void RankChange(int nowrank) {
        _rank = nowrank;
    }


    public void SpeedUp(bool speed) {

        if (speed) {
            _movespeed = MOVESPEED;
            _speedUp = 100f;
            StartCoroutine(Timecount());
        } else {
            _rb.velocity = new Vector2(_movespeed, _rb.velocity.y);
            _speedUp = 0;
        }

    }
    private IEnumerator Timecount() {
        yield return new WaitForSeconds(0.3f);
        SpeedUp(false);
    }


    public void SpeedDown(bool poff) {
        if (poff) {
            StartCoroutine(CollisionEffect());
        }
    }
}
