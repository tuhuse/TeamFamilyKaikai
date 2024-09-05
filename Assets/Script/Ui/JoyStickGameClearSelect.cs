using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoyStickGameClearSelect : MonoBehaviour {
    [SerializeField] List<GameObject> _gameClearText = new List<GameObject>(); //0にgo,1にreturnのテキストを入れる
    [SerializeField] GameObject _pauseManager = default; //canvasmanagerを入れる
    private int _selectButton = 2; //今見ているボタン
    public GameObject _preButton = default;　//ひとつ前に選択していたボタン

    private bool _isSelect;

    private bool _isFarstSelect = false;

    private bool _isGameOver = false;
    // Start is called before the first frame update
    // Update is called once per frame
    void Update() {
     
        float lstick = Input.GetAxis("L_Stick_Horizontal");
        //右矢印を押したら
        if (lstick > 0 && !_isSelect) {
            _isFarstSelect = true;
            _isSelect = true;
            _selectButton = 1;
            _preButton.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
            

            _gameClearText[1].GetComponent<Image>().color = new Color(1, 1, 1, 1);

            _gameClearText[0].GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);


        }

        //左矢印を押した場合
        if (lstick  < 0 && !_isSelect) {
            _isFarstSelect = true;
            _isSelect = true;
            _selectButton = 0;
            _preButton.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
           

            _gameClearText[0].GetComponent<Image>().color = new Color(1, 1, 1, 1);

            _gameClearText[1].GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);

        }

        //矢印ボタンを離したら
        if (lstick == 0) {
            //また選択できるようにする
            _isSelect = false;
        }

        //Aボタンを押したら
        if (Input.GetButton("Fire2") && _isFarstSelect) {
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
}