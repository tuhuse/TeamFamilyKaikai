using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoManager : MonoBehaviour {
    [SerializeField] private GameObject _titleParent;
    [SerializeField] private GameObject _move;
    [SerializeField] private AudioSource _audio;
    [SerializeField] private GameObject _titlePanel;
    [SerializeField] List<GameObject> _gameClearText = new List<GameObject>(); //0にgo,1にreturnのテキストを入れる
    // Start is called before the first frame update
    void Start() {
        StartCoroutine(FirstStart());
            }

    // Update is called once per frame
    void Update() {
        if (_move.activeSelf) {
            if (Input.anyKeyDown || Input.GetMouseButtonDown(0)) {
                _titleParent.SetActive(true);
                _gameClearText[0].SetActive(true);
                _gameClearText[1].SetActive(true);
                _gameClearText[2].SetActive(true);
                _move.SetActive(false);
                _audio.enabled = true;
                StartCoroutine(FirstStart());
            }
        }

      


    }
    private IEnumerator FirstStart() {
        yield return new WaitForSeconds(15f);
        _titlePanel.SetActive(true);
        _gameClearText[0].SetActive(false);
        _gameClearText[1].SetActive(false);
        _gameClearText[2].SetActive(false);
        yield return new WaitForSeconds(1f);
        _move.SetActive(true);
        _titleParent.SetActive(false);
        yield return new WaitForSeconds(1f);
        _titlePanel.SetActive(false);      
        _audio.enabled = false;
      
    }
}
