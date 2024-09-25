using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class ChallengeTextScript : MonoBehaviour {
    [SerializeField] private Text _challenge;
    [SerializeField] private GameObject[] _yesnoButton;
    [SerializeField] private AudioClip _typingSound;
    [SerializeField] private MasterM _mastermScript;
    private AudioSource _audio;
    private float _typingSpeed = 0.3f;
    private string _message = "ちょうせんしますか？";

    void Awake() {
        _audio = this.gameObject.GetComponent<AudioSource>();
        //StartCoroutine(Text());
    }

    private IEnumerator Text() {
        
        // 初期化
        _challenge.text = "";
        _yesnoButton[0].SetActive(false);
        _yesnoButton[1].SetActive(false);
        print("1");
        foreach (char letter in _message) {
            print("ふぉーーーーえーーーち");
            _audio.PlayOneShot(_typingSound);
            _challenge.text += letter;
            yield return new WaitForSeconds(_typingSpeed);
        }
        print("2");
        // ボタンをアクティブにする
        _mastermScript.Button();
        _yesnoButton[0].SetActive(true);
        _yesnoButton[1].SetActive(true);
    }

    // 再度表示したい場合のメソッド
    public void RestartText() {
        StartCoroutine(Text());
        print("RestartTExt");
    }
}
