using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SneakAnim : MonoBehaviour
{
    private Animator _sneakAnim = default;

    private bool _isStartPositionMoveOut = false;
    private bool _isPositionMoveOut = false;
    private bool _isPositionMoveIn = false;
    private bool _isPositionLastMoveIn = false;
    private bool _isOneShot = default;

    private float _positionX = default;
    private float _outStopPosition = 154f;
    private float _inStopPosition = 109.5f;

    private const float STARTPOSITIONMOVEOUTX = 0.15f;

    private const float POSITIONMOVEOUTX = 0.25f;
    private const float POSITIONMOVEINX = 0.35f;

    

    private const float WAITSTARTINTIMIDATION = 3f;
    private const float WAITSTARTSCREENOUT = 1f;

    

    private const float TIMEDELTATIMEMULTIPLE = 1000f;
    private AudioSource _sneakAudio = default;
    [SerializeField] private AudioClip _eatSE = default;

    [SerializeField] GameObject _camera = default;
    [SerializeField] SelectCharacter _selectScript = default;

    [SerializeField] OutLineScript _childScript = default;

    [SerializeField] private BoxCollider2D _childBoxcollider = default;

    [SerializeField] private ParticleSystem[] _fireWorks;
    private float _fireworksSeconds = 0.1f;
    [SerializeField] private AudioClip _fireworkSound;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Firework());
        _sneakAudio = this.GetComponent<AudioSource>();
       _sneakAnim = GetComponent<Animator>();
        _sneakAnim.SetBool("Intimidation", true);
        _positionX = this.transform.position.x;
        StartCoroutine(StartIntimidation());
    }

    // Update is called once per frame
    void Update()
    {
        if (_isPositionMoveOut) 
        {
            //�ւ��J�����O���肬��܂Ŏ����Ă���
            ScreenOut();
        }

        if (_isPositionMoveIn && !_isPositionLastMoveIn) {
            //�J�����̒��ɓ����Ă���
            ScreenIn(true);
        } else if (_isPositionLastMoveIn && _isPositionLastMoveIn) {
            ScreenIn(false);
        }


        if (_isStartPositionMoveOut) 
        {
            //��ԍŏ��̈Њd�s��
            StartScreenOUT();
        }
    }

    public void Attack() 
    {
        //�J�G����H�ׂ�
        _sneakAnim.SetBool("ScreenIn", true);
        _isPositionMoveIn = true;
    }

    private IEnumerator StartIntimidation() 
    {
        //�Њd�s��
        yield return new WaitForSeconds(WAITSTARTINTIMIDATION);
           
            _sneakAnim.SetBool("Intimidation", false);
        yield return new WaitForSeconds(WAITSTARTSCREENOUT);
        _isStartPositionMoveOut = true;
    }

    private void StartScreenOUT() 
    {
        //��ԍŏ��ɂ������Ɖ�ʊO�ɍs��
        if (this.transform.position.x >= _camera.transform.position.x-_outStopPosition) 
        {
            this.transform.position -= new Vector3(STARTPOSITIONMOVEOUTX, 0, 0)*Time.deltaTime* TIMEDELTATIMEMULTIPLE;
        } 
        else 
        {
            _sneakAnim.SetBool("ScreenIn", false);
            _isPositionMoveOut = false;
            _isStartPositionMoveOut = false;
            _selectScript.GoTxt();
        }
    }


    private void ScreenOut() 
    {
        //��ʊO�ɏo�Ă���
        if (this.transform.position.x >= _camera.transform.position.x - _outStopPosition)
        {
            this.transform.position -= new Vector3(POSITIONMOVEOUTX, 0, 0) * Time.deltaTime * TIMEDELTATIMEMULTIPLE;
        } 
        else 
        {
            _sneakAnim.SetBool("ScreenIn", false);
            _isPositionMoveOut = false;
            _childBoxcollider.enabled = true;
            _isOneShot = false;
        }
    }
    public void Access(bool access) {
        if (access) {
            _isPositionLastMoveIn = true;
        }
       
    }
    public void ScreenIn(bool screenIn) 
    {
        if (screenIn) 
        {
            if (!_isOneShot) 
            {
                _sneakAudio.PlayOneShot(_eatSE);
                _isOneShot = true;
            }
            //��ʓ��ɓ���
            if (this.transform.position.x <= _camera.transform.position.x - _inStopPosition) 
            {
                this.transform.position += new Vector3(POSITIONMOVEINX, 0, 0) * Time.deltaTime * TIMEDELTATIMEMULTIPLE;
            } else {
                _isPositionMoveIn = false;
                _isPositionMoveOut = true;
            }
        } else {
            float move = 0.2f;
            if (this.transform.position.x <= _camera.transform.position.x - _inStopPosition) {
                this.transform.position += new Vector3(move, 0, 0) * Time.deltaTime * TIMEDELTATIMEMULTIPLE;
                
            } else 
            {
                _isPositionMoveIn = false;
            }
        }

    }

    public void StopPositionIncrease(float value)
    {
        _outStopPosition -= value;
        _inStopPosition -= value;
    }
    private IEnumerator Firework() {


        yield return new WaitForSeconds(_fireworksSeconds);
        _fireWorks[0].Play();
        _sneakAudio.PlayOneShot(_fireworkSound);
        yield return new WaitForSeconds(_fireworksSeconds);
        _fireWorks[1].Play();
        _sneakAudio.PlayOneShot(_fireworkSound);
        yield return new WaitForSeconds(_fireworksSeconds);
        _fireWorks[2].Play();
        _sneakAudio.PlayOneShot(_fireworkSound);
        yield return new WaitForSeconds(_fireworksSeconds);
        _fireWorks[3].Play();
        _sneakAudio.PlayOneShot(_fireworkSound);
        yield return new WaitForSeconds(_fireworksSeconds);
        _fireWorks[4].Play();
        _sneakAudio.PlayOneShot(_fireworkSound);
        yield return new WaitForSeconds(_fireworksSeconds);
        _fireWorks[5].Play();
        _sneakAudio.PlayOneShot(_fireworkSound);
        yield return new WaitForSeconds(_fireworksSeconds);
        _fireWorks[6].Play();
        _sneakAudio.PlayOneShot(_fireworkSound);
        yield return new WaitForSeconds(_fireworksSeconds);
        _fireWorks[7].Play();
        _sneakAudio.PlayOneShot(_fireworkSound);
        yield return new WaitForSeconds(_fireworksSeconds);
        _fireWorks[8].Play();
        _sneakAudio.PlayOneShot(_fireworkSound);
        yield return new WaitForSeconds(_fireworksSeconds);
        _fireWorks[9].Play();
        _sneakAudio.PlayOneShot(_fireworkSound);
    }
}
