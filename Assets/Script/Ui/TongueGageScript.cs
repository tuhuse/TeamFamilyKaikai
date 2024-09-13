using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TongueGageScript : MonoBehaviour {
    [SerializeField] private float _abilityCooldownTimer;
    private float _abilityMeasureTimer;
    [SerializeField] private Image _circleGauge;
    [SerializeField] private Image _leftEye;
    [SerializeField] private Image _rightEye = default;
    [SerializeField] private Sprite _grayFrog = default;
    private bool _isCooldownActive = false;

    // Start is called before the first frame update
    void Start() {
        _abilityMeasureTimer = 0;
        _circleGauge.fillAmount = 1f; // 初期値を設定
    }

    // Update is called once per frame
    void Update() {
        if (_isCooldownActive) {
            _abilityMeasureTimer -= Time.deltaTime;
            float fillValue = 1 - (_abilityMeasureTimer / _abilityCooldownTimer);
            _circleGauge.fillAmount = Mathf.Clamp01(fillValue);

            if (_abilityMeasureTimer <= 0) {
                _isCooldownActive = false;
                _circleGauge.fillAmount = 1f; // クールダウン後にゲージを満タンにする
            }
        }
    }

    // クールダウンを開始するメソッド
    public void TongueUIStartCooldown() {
        _leftEye.enabled = false;
        _rightEye.enabled = false;
        _abilityMeasureTimer = _abilityCooldownTimer;
        _circleGauge.fillAmount = 0f; // ゲージを一気に減少させる
        _isCooldownActive = true;
    }

    // クールダウンを停止するメソッド
    public void TongueUIStopCooldown() {
        _leftEye.enabled = true;
        _rightEye.enabled = true;
        _isCooldownActive = false;
        _circleGauge.fillAmount = 1f; // クールダウンが手動で停止された場合にゲージを満タンにする
    }
    public void TongueCoolDownFloat(float cooldown) {
        _abilityCooldownTimer = cooldown;
    }

    public void DethFrog() {
        _circleGauge.sprite = _grayFrog;
        _leftEye.enabled = false;
        _rightEye.enabled = false;

    }
}
