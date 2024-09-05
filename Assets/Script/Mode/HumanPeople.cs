using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HumanPeople : MonoBehaviour {
    [Header("canvasmanager")]
    [SerializeField] private List<GameObject> _frogsButton = new List<GameObject>();  // キャラクター選択ボタンのリスト
    [SerializeField] private SelectCharacter _selectCharacterScript = default;  // キャラクター選択のスクリプト
    //[SerializeField] private MoveButtonScript _move;  // 移動ボタンのスクリプト
    [SerializeField] private Difficulty _difficulty;  // 難易度設定のスクリプト
    private AudioSource _audio;
    private int _humanPeople = default;  // 現在選択されている人数（ボタンインデックス）
    private bool _isMoveButton = false;  // 移動ボタンが押されているかどうか
    [SerializeField] private SpriteRenderer[] _frog;

    private enum Human {
        One,    // 1人プレイ
        Two,    // 2人プレイ
        Three,  // 3人プレイ
        Four    // 4人プレイ
    }
    private Human _humans = default;  // 現在選択されているプレイヤー人数
    private bool _select = false;  // 選択状態を管理
    private Image _image;  // ボタンのImageコンポーネント（未使用）

    // Start is called before the first frame update
    void Start() {
        _audio = GetComponent<AudioSource>();
        // 初期化処理（現在は空）
        Selecet(true);
        SelectNumberPeople(true);
    }

    // Update is called once per frame
    void Update() {
        if (_select) {
            // 「Fire1」ボタンが押されたときの処理
            //if (Input.GetButtonDown("Fire1")) {
            //    SelectNumberPeople(false);  // ボタンの表示を非表示にする
            //    _move._next = false;  // 移動のフラグをリセット
            //    Selecet(false);  // 選択状態を解除
            //}

            // 左スティックの垂直方向の入力を取得
            float controllerStick = Input.GetAxis("L_Stick_Vartical") * -1;

            // スティックが中心に戻ったときの処理
            if (controllerStick == 0) {
                _isMoveButton = false;
            }

            // スティックが下に動いたときの処理
            if (controllerStick < 0 && !_isMoveButton) {
                // 現在のボタンの色を変更
                _frogsButton[_humanPeople].GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
                _humanPeople++;
                // リストの範囲外に出たら最初に戻す
                if (_frogsButton.Count <= _humanPeople) {
                    _humanPeople = 0;
                }
                // 新しいボタンの色を変更
                _frogsButton[_humanPeople].GetComponent<Image>().color = new Color(1, 1, 1, 1);
                _isMoveButton = true;
            }

            // スティックが上に動いたときの処理
            if (controllerStick > 0 && !_isMoveButton) {
                // 現在のボタンの色を変更
                _frogsButton[_humanPeople].GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
                _humanPeople--;
                // リストの範囲外に出たら最後に戻す
                if (0 > _humanPeople) {
                    _humanPeople = _frogsButton.Count - 1;
                }
                // 新しいボタンの色を変更
                _frogsButton[_humanPeople].GetComponent<Image>().color = new Color(1, 1, 1, 1);
                _isMoveButton = true;
            }

            // 現在の人数設定に応じた処理
            HumanSwitch();

            // 「1pA」ボタンが押されたときの処理
            if (Input.GetButtonDown("1pA")) {
                
                if (_humans == Human.Four) {
                    // 4人プレイの設定
                    Switch();
                    SelectNumberPeople(false);
                    _selectCharacterScript.SummonSneak();
                    Selecet(false);
                } else {
                    _audio.PlayOneShot(_audio.clip);
                    // 1人、2人または3人プレイの設定
                    Switch();
                    _difficulty.Selecet(true);
                    _difficulty.Diffculty(true);
                    SelectNumberPeople(false);
                    Selecet(false);
                }
            }
        }
    }

    // ボタンがクリックされたときの処理
    public void OnClick() {
        Switch();
    }

    // 現在のプレイヤー人数に応じた処理を実行
    private void Switch() {
        switch (_humans) {
            case Human.One:
                _selectCharacterScript.Humans(1);　// 1人プレイ設定
                break;
            case Human.Two:
                _selectCharacterScript.Humans(2);  // 2人プレイ設定
                break;
            case Human.Three:
                _selectCharacterScript.Humans(3);  // 3人プレイ設定
                break;
            case Human.Four:
                _selectCharacterScript.Humans(4);  // 4人プレイ設定
                break;
        }
    }

    // 現在のボタンインデックスに応じたプレイヤー人数の設定
    private void HumanSwitch() {
        switch (_humanPeople) {
            case 0:
                _humans = Human.One;
                _frog[1].enabled=false;
                _frog[2].enabled = false;
                _frog[6].enabled = false;
                _frog[7].enabled = false;
                _frog[8].enabled = false;
                _frog[9].enabled = false;
                _frog[0].enabled = true;
                break;
            case 1:
                _humans = Human.Two;
                _frog[3].enabled = false;
                _frog[4].enabled = false;
                _frog[5].enabled = false;
                _frog[0].enabled = false;
                _frog[1].enabled = true;
                _frog[2].enabled = true;
                break;
            case 2:
                _humans = Human.Three;
                _frog[1].enabled = false;
                _frog[2].enabled = false;
                _frog[6].enabled = false;
                _frog[7].enabled = false;
                _frog[8].enabled = false;
                _frog[9].enabled = false;
                _frog[3].enabled = true;
                _frog[4].enabled = true;
                _frog[5].enabled = true;
                break;
            case 3:
                _humans = Human.Four;
                _frog[0].enabled = false;
                _frog[3].enabled = false;
                _frog[4].enabled = false;
                _frog[5].enabled = false;
                _frog[6].enabled = true;
                _frog[7].enabled = true;
                _frog[8].enabled = true;
                _frog[9].enabled = true;
                break;
        }
    }

    // 選択状態の設定
    public void Selecet(bool on) {
        if (on) {
            StartCoroutine(Boot());  // 選択状態にするためのコルーチンを開始
        } else {
            _select = false;  // 選択状態を解除
        }
    }

    // 選択状態にするためのコルーチン
    private IEnumerator Boot() {
        yield return new WaitForSeconds(0.1f);  // 0.1秒待機
        _select = true;  // 選択状態に設定
    }

    // ボタンの表示/非表示を設定
    public void SelectNumberPeople(bool buttonActive) {
        if (buttonActive) {
            // ボタンを表示
            _frogsButton[0].SetActive(true);
            _frogsButton[1].SetActive(true);
            _frogsButton[2].SetActive(true);
            _frogsButton[3].SetActive(true);
        }
        if (!buttonActive) {
            // ボタンを表示
            _frogsButton[0].SetActive(false);
            _frogsButton[1].SetActive(false);
            _frogsButton[2].SetActive(false);
            _frogsButton[3].SetActive(false);
        }

    }
}
