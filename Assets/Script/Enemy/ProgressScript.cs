using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressScript : MonoBehaviour {
    private float _moveIncreased = 0;
    private float _moveValue = 0.025f;
    private float _startWaitProgress = 3.5f;
    private float _waitProgress = 10f;
    private bool _move = false;
    private int _movenumber = default;



    private const float TIMEDELTTIME = 500;
    private const int MAXMOVENUBBER = 4;
    private const float MAXMOVEVALUE = 25f;
    [SerializeField] SneakAnim _snakeAnim = default;
    [SerializeField] private CameraShake _cameraShake;
    private void Start() {

    }
    private void Update() {
        //進む工程を４回繰り返す
        if (_move && _movenumber <= MAXMOVENUBBER) {
            if (_moveIncreased <= MAXMOVEVALUE) {
                _cameraShake.StartCameraShake();
                this.transform.position += new Vector3(_moveValue, 0, 0) * Time.deltaTime * TIMEDELTTIME;
                _moveIncreased += _moveValue * Time.deltaTime * TIMEDELTTIME;
                _snakeAnim.StopPositionIncrease(_moveValue * Time.deltaTime * TIMEDELTTIME);
            } else {
                _cameraShake.StopCameraShake(false);
                _moveIncreased = 0;
                _move = false;
                StartCoroutine(Progress());
                _movenumber++;
            }
        }

    }
    public IEnumerator StartProgress() {
        //一番初めは少し早い時間で前へ進む
        yield return new WaitForSeconds(_startWaitProgress);
        {
            _move = true;
        }
    }

    private IEnumerator Progress() {
        //１０秒ごとに前へ進むの繰り返し
        yield return new WaitForSeconds(_waitProgress);
        {
            _move = true;
        }
    }

}

