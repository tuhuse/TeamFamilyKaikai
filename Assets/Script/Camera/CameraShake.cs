using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraShake : MonoBehaviour {
    private float _shakeMagnitude = 1f;// 振動の強さを制御するパラメータ
    private bool _isCameraShake = false;
    private bool _isShake = false;
    private bool _play = true;
    private Vector3 _originalPosition;
    [SerializeField] private Image _redColor;
    [SerializeField] private StageRoopManFixed _stageRoopMan;
    // Start is called before the first frame update
    void Start() {
        _originalPosition = transform.localPosition;
    }

    void Update() {
        if (_play) {
            if (_isCameraShake) {
                _isShake = true;
                // ランダムな振動を生成
                //float shakeX = Random.Range(-1f, 1f) * _shakeMagnitude;
                float shakeY = Random.Range(-1f, 1f) * _shakeMagnitude;
                _stageRoopMan.Camerashake(true);


                _redColor.enabled = true;
                // 振動をカメラに適用
                transform.localPosition += new Vector3(0f, shakeY, 0f);

            } else if (!_isCameraShake && _isShake) {
                _isShake = false;
                // 振動が終了したら元の位置に戻す
                transform.localPosition = _originalPosition;
            }
        }
    }
       

   // 振動を開始するメソッド
    public void StartCameraShake() {
        _isCameraShake = true;
    }
    public void StopCameraShake(bool lastStop) {
        if (!lastStop) {
            _isCameraShake = false;
            _stageRoopMan.Camerashake(false);
        } else {
            _isCameraShake = false;
            _stageRoopMan.Camerashake(false);
            _play = false;
        }
        
    }
   
  
}
