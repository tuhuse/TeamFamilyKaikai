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

    private float _positionX = default;
    private float _outStopPosition = 154f;
    private float _inStopPosition = 109.5f;

    private const float STARTPOSITIONMOVEOUTX = 0.15f;

    private const float POSITIONMOVEOUTX = 0.25f;
    private const float POSITIONMOVEINX = 0.35f;

    

    private const float WAITSTARTINTIMIDATION = 3f;
    private const float WAITSTARTSCREENOUT = 1f;

    

    private const float TIMEDELTATIMEMULTIPLE = 1000f;

    [SerializeField] GameObject _camera = default;
    [SerializeField] SelectCharacter _selectScript = default;

    [SerializeField] OutLineScript _childScript = default;

    // Start is called before the first frame update
    void Start()
    {
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
            //蛇をカメラ外ぎりぎりまで持っていく
            ScreenOut();
        }

        if (_isPositionMoveIn && !_isPositionLastMoveIn) {
            //カメラの中に入ってくる
            ScreenIn(true);
        } else if (_isPositionLastMoveIn && _isPositionLastMoveIn) {
            ScreenIn(false);
        }


        if (_isStartPositionMoveOut) 
        {
            //一番最初の威嚇行動
            StartScreenOUT();
        }
    }

    public void Attack() 
    {
        //カエルを食べる
        _sneakAnim.SetBool("ScreenIn", true);
        _isPositionMoveIn = true;
    }

    private IEnumerator StartIntimidation() 
    {
        //威嚇行動
        yield return new WaitForSeconds(WAITSTARTINTIMIDATION);
           
            _sneakAnim.SetBool("Intimidation", false);
        yield return new WaitForSeconds(WAITSTARTSCREENOUT);
        _isStartPositionMoveOut = true;
    }

    private void StartScreenOUT() 
    {
        //一番最初にゆっくりと画面外に行く
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
        //画面外に出ていく
        if (this.transform.position.x >= _camera.transform.position.x - _outStopPosition)
        {
            this.transform.position -= new Vector3(POSITIONMOVEOUTX, 0, 0) * Time.deltaTime * TIMEDELTATIMEMULTIPLE;
        } 
        else 
        {
            _sneakAnim.SetBool("ScreenIn", false);
            _isPositionMoveOut = false;
        }
    }
    public void Access(bool access) {
        if (access) {
            _isPositionLastMoveIn = true;
        }
       
    }
    public void ScreenIn(bool screenIn) 
    {
        if (screenIn) {
            //画面内に入る
            if (this.transform.position.x <= _camera.transform.position.x - _inStopPosition) {
                this.transform.position += new Vector3(POSITIONMOVEINX, 0, 0) * Time.deltaTime * TIMEDELTATIMEMULTIPLE;
            } else {
                _isPositionMoveIn = false;
                _isPositionMoveOut = true;
            }
        } else {
            float move = 0.2f;
            if (this.transform.position.x <= _camera.transform.position.x - _inStopPosition) {
                this.transform.position += new Vector3(move, 0, 0) * Time.deltaTime * TIMEDELTATIMEMULTIPLE;
                
            } else {
                _isPositionMoveIn = false;
               
            }
        }

    }

    public void StopPositionIncrease(float value)
    {
        _outStopPosition -= value;
        _inStopPosition -= value;
    }
}
