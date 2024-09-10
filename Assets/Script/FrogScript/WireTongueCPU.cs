using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireTongueCPU : MonoBehaviour {
    public bool _isExtension = false;
    private bool _isAttack = false;
    public bool _isCoolDown = true;
    private bool _isJudge = false;
    public bool _underAttack = false;
    private bool _isFrogCatch = false;
    private bool _isJustOnes = false;
    private bool _isReduction = false;

    //それぞれ親オブジェクトのカエルのみを入れる
    [Header("CPU1")] [SerializeField] private GameObject _enemy1 = default;
    [Header("CPU2")] [SerializeField] private GameObject _enemy2 = default;
    [Header("CPU3")] [SerializeField] private GameObject _enemy3 = default;

    [SerializeField] GameObject _mySelf = default;


    private Rigidbody2D _cpuRB = default;

    private float _tongueScaleY = default;

    private const float DELTATIMEMULTIPLE = 1000f;
    private const float PLUSSCALESPEEDY = 0.1f;
    private const float TONGUEMAXEXTENSION = 10f;
    private const float TONGUESCALEX = 3f;
    private const float TONGUESCALEY = 0.01f;

    [SerializeField] private GameObject _dashSmoke = default;
    private GameObject _dashSmokeClone = default;

    private SpriteRenderer _dashSmokeRenderer = default;
    private Animator _dashSmokeAnim = default;

    private SpriteRenderer _thisSprite = default;
    // Start is called before the first frame update
    void Start() {
        _tongueScaleY = this.transform.localScale.y;
        _cpuRB = GetComponentInParent<Rigidbody2D>();
        _thisSprite = this.GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update() {


        if (_isExtension&&!_isReduction) {
            this.transform.localScale += new Vector3(0, PLUSSCALESPEEDY) * Time.deltaTime * DELTATIMEMULTIPLE;
            //_isCoolDown = true;

        } 
        else if (!_isFrogCatch) 
        {
            if (this.transform.localScale.y > _tongueScaleY && !_isJustOnes) 
            {
                this.transform.localScale -= new Vector3(0, PLUSSCALESPEEDY, 0) * Time.deltaTime * DELTATIMEMULTIPLE;

            } 
            else if (_isCoolDown && !_isJustOnes) 
            {
                _isJustOnes = true;
                _thisSprite.enabled = false;
                Judge();
            }
        }
        else 
        {
            _underAttack = false;
            if (this.transform.localScale.y >= _tongueScaleY && !_isJustOnes) 
            {
                this.transform.localScale -= new Vector3(0, PLUSSCALESPEEDY, 0) * Time.deltaTime * DELTATIMEMULTIPLE;
            } else if (_isCoolDown && !_isJustOnes) {
                _isJustOnes = true;
                _thisSprite.enabled = false;

                Judge();
            }
        }

        if (this.transform.localScale.y > TONGUEMAXEXTENSION) {
            _isExtension = false;
            _isReduction = true;
        }

    }
    private void Judge() {

        if (_isJudge) 
        {
            StartCoroutine(LongCoolDown());
            _isJudge = false;
        } else {
            StartCoroutine(CoolDown());
        }
    }
    public void JudgeCoolDown(bool judge) {
        if (judge) {
            
            Judge();
            judge = false;
        }
       
    }
    private IEnumerator CoolDown() {

        yield return new WaitForSeconds(8);
        _isExtension = false;
        _isCoolDown = false;
        _isJustOnes = false;
        _isReduction = false;

        if (_mySelf.GetComponent<FrogCpu>()) 
        {
            
            _mySelf.GetComponent<FrogCpu>().ReproductionTongue();
        } 
        else if (_mySelf.GetComponent<FrogCpuMulti>())
        {
           
            _mySelf.GetComponent<FrogCpuMulti>().ReproductionTongue();
        } 
        else if (_mySelf.GetComponent<FrogCpuMulti2>())
        {
           
            _mySelf.GetComponent<FrogCpuMulti2>().ReproductionTongue();
        }


    }
    private IEnumerator LongCoolDown() {

        yield return new WaitForSeconds(8);
        _isExtension = false;
        _isCoolDown = false;
        _isJustOnes = false;
        _isReduction = false;

        if (_mySelf.GetComponent<FrogCpu>())
        {
           
            _mySelf.GetComponent<FrogCpu>().ReproductionTongue();
        } 
        else if (_mySelf.GetComponent<FrogCpuMulti>())
        {
    
            _mySelf.GetComponent<FrogCpuMulti>().ReproductionTongue();
        } 
        else if (_mySelf.GetComponent<FrogCpuMulti2>()) 
        { 
            _mySelf.GetComponent<FrogCpuMulti2>().ReproductionTongue();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Flor")) {
            _isExtension = false;
        }
        if (collision.gameObject.tag == "Player" && _underAttack) 
        {
           

            if (_mySelf.gameObject.GetComponent<FrogCpu>() != null&&!collision.gameObject.GetComponent<PlayercontrollerScript>()._isInvincivle) 
            {
                //ダッシュエフェクトを持っていなかったら
                if (_dashSmokeClone == null) {
                    //煙を生成して、コンポーネントを取得する
                    _dashSmokeClone = Instantiate(_dashSmoke);
                    _dashSmokeAnim = _dashSmokeClone.GetComponent<Animator>();
                    _dashSmokeRenderer = _dashSmokeClone.GetComponent<SpriteRenderer>();
                }
                _dashSmokeClone.transform.position = this.transform.position;
                _dashSmokeAnim.SetBool("Dash", true);
                _dashSmokeRenderer.enabled = true;

                _mySelf.gameObject.GetComponent<FrogCpu>().SpeedUp(true);
            } 
            else if (_mySelf.gameObject.GetComponent<FrogCpuMulti>() != null && !collision.gameObject.GetComponent<PlayercontrollerScript>()._isInvincivle) 
            {
                //ダッシュエフェクトを持っていなかったら
                if (_dashSmokeClone == null) {
                    //煙を生成して、コンポーネントを取得する
                    _dashSmokeClone = Instantiate(_dashSmoke);
                    _dashSmokeAnim = _dashSmokeClone.GetComponent<Animator>();
                    _dashSmokeRenderer = _dashSmokeClone.GetComponent<SpriteRenderer>();
                }
                _dashSmokeClone.transform.position = this.transform.position;
                _dashSmokeAnim.SetBool("Dash", true);
                _dashSmokeRenderer.enabled = true;

                _mySelf.gameObject.GetComponent<FrogCpuMulti>().SpeedUp(true);
            } 
            else if (_mySelf.gameObject.GetComponent<FrogCpuMulti2>() != null && !collision.gameObject.GetComponent<PlayercontrollerScript>()._isInvincivle) 
            {
                //ダッシュエフェクトを持っていなかったら
                if (_dashSmokeClone == null) {
                    //煙を生成して、コンポーネントを取得する
                    _dashSmokeClone = Instantiate(_dashSmoke);
                    _dashSmokeAnim = _dashSmokeClone.GetComponent<Animator>();
                    _dashSmokeRenderer = _dashSmokeClone.GetComponent<SpriteRenderer>();
                }
                _dashSmokeClone.transform.position = this.transform.position;
                _dashSmokeAnim.SetBool("Dash", true);
                _dashSmokeRenderer.enabled = true;

                _mySelf.gameObject.GetComponent<FrogCpuMulti2>().SpeedUp(true);
            }


            _isExtension = false;
            _isFrogCatch = true;
            _isJudge = true;
        }

        if (collision.gameObject.tag == "CPU" && _underAttack &&
            ((collision.gameObject.GetComponent<FrogCpu>()&&!collision.gameObject.GetComponent<FrogCpu>()._isPridictionAbility) ||
            (collision.gameObject.GetComponent<FrogCpuMulti>() && !collision.gameObject.GetComponent<FrogCpuMulti>()._isPridictionAbility) ||
            (collision.gameObject.GetComponent<FrogCpuMulti2>() && !collision.gameObject.GetComponent<FrogCpuMulti2>()._isPridictionAbility)))
        {
            //ダッシュエフェクトを持っていなかったら
           

            if (_mySelf.gameObject.GetComponent<FrogCpu>() != null) 
            {
                if (_dashSmokeClone == null) {
                    //煙を生成して、コンポーネントを取得する
                    _dashSmokeClone = Instantiate(_dashSmoke);
                    _dashSmokeAnim = _dashSmokeClone.GetComponent<Animator>();
                    _dashSmokeRenderer = _dashSmokeClone.GetComponent<SpriteRenderer>();
                }
                _dashSmokeClone.transform.position = this.transform.position;
                _dashSmokeAnim.SetBool("Dash", true);
                _dashSmokeRenderer.enabled = true;


                _mySelf.gameObject.GetComponent<FrogCpu>().SpeedUp(true);
            } 
            else if(_mySelf.gameObject.GetComponent<FrogCpuMulti>() != null) 
            {

                if (_dashSmokeClone == null) {
                    //煙を生成して、コンポーネントを取得する
                    _dashSmokeClone = Instantiate(_dashSmoke);
                    _dashSmokeAnim = _dashSmokeClone.GetComponent<Animator>();
                    _dashSmokeRenderer = _dashSmokeClone.GetComponent<SpriteRenderer>();
                }
                _dashSmokeClone.transform.position = this.transform.position;
                _dashSmokeAnim.SetBool("Dash", true);
                _dashSmokeRenderer.enabled = true;


                _mySelf.gameObject.GetComponent<FrogCpuMulti>().SpeedUp(true);
            }
            else if (_mySelf.gameObject.GetComponent<FrogCpuMulti2>() != null) 
            {
                if (_dashSmokeClone == null) {
                    //煙を生成して、コンポーネントを取得する
                    _dashSmokeClone = Instantiate(_dashSmoke);
                    _dashSmokeAnim = _dashSmokeClone.GetComponent<Animator>();
                    _dashSmokeRenderer = _dashSmokeClone.GetComponent<SpriteRenderer>();
                }
                _dashSmokeClone.transform.position = this.transform.position;
                _dashSmokeAnim.SetBool("Dash", true);
                _dashSmokeRenderer.enabled = true;


                _mySelf.gameObject.GetComponent<FrogCpuMulti2>().SpeedUp(true);
            }
            _isExtension = false;
            _isFrogCatch = true;
            _isJudge = true;
        }
    }
    public void TongueAttack() 
    {
   
        _isExtension = true;
        _underAttack = true;
        _isCoolDown = true;
        _thisSprite.enabled = true;
    }
}
