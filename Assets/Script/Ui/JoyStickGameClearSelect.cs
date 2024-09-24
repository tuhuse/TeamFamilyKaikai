using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoyStickGameClearSelect : MonoBehaviour {
    [SerializeField] List<GameObject> _gameClearText = new List<GameObject>(); //0にgo,1にreturnのテキストを入れる
    [SerializeField] GameObject _pauseManager = default; //canvasmanagerを入れる
    private int _selectButton = 2; //今見ているボタン
    public GameObject _preButton = default; //ひとつ前に選択していたボタン
    [SerializeField] private Animator[] _animator;
    private bool _isSelect;

    private bool _isFarstSelect = false;


    // Start is called before the first frame update
    private void Start() {
        _selectButton = 0;
        StartCoroutine(Wait());
    }
    // Update is called once per frame
    void Update() {
        if (_selectButton == 0) {
            _animator[0].SetBool("Change", true);
            _animator[1].SetBool("Change", false);

        } else if (_selectButton == 1) {
            _animator[0].SetBool("Change", false);
            _animator[1].SetBool("Change", true);

        }
        float lstick = Input.GetAxis("L_Stick_Horizontal");
        float crosskey = Input.GetAxis("Debug Horizontal");
        //右矢印を押したら
        if ((lstick > 0 && !_isSelect) || (crosskey > 0 && !_isSelect)) {

            _isSelect = true;
            _selectButton = 1;

        }

        //左矢印を押した場合
        if ((lstick < 0 && !_isSelect) || (crosskey < 0 && !_isSelect)) {

            _isSelect = true;
            _selectButton = 0;


        }

        //矢印ボタンを離したら
        if (lstick == 0 && crosskey == 0) {
            //また選択できるようにする
            _isSelect = false;
        }

        //Aボタンを押したら
        if ((Input.GetButton("Fire2") && _isFarstSelect) || (Input.GetKeyDown(KeyCode.Return) && _isFarstSelect)) {
            _isFarstSelect = false;
            if (_selectButton == 0) //returnを見ていた場合
            {
                //タイトルに戻る

                _pauseManager.GetComponent<ClearMan>().ClearRetryButton();
            } else //returnを見ていた場合
              {

                //ゲームを終了する
                _pauseManager.GetComponent<ClearMan>().ClearTitleButton();
            }
        }
    }
    private IEnumerator Wait() {

        yield return new WaitForSeconds(1);
        _isFarstSelect = true;
    }
}