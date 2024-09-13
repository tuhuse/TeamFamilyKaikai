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
        _circleGauge.fillAmount = 1f; // �����l��ݒ�
    }

    // Update is called once per frame
    void Update() {
        if (_isCooldownActive) {
            _abilityMeasureTimer -= Time.deltaTime;
            float fillValue = 1 - (_abilityMeasureTimer / _abilityCooldownTimer);
            _circleGauge.fillAmount = Mathf.Clamp01(fillValue);

            if (_abilityMeasureTimer <= 0) {
                _isCooldownActive = false;
                _circleGauge.fillAmount = 1f; // �N�[���_�E����ɃQ�[�W�𖞃^���ɂ���
            }
        }
    }

    // �N�[���_�E�����J�n���郁�\�b�h
    public void TongueUIStartCooldown() {
        _leftEye.enabled = false;
        _rightEye.enabled = false;
        _abilityMeasureTimer = _abilityCooldownTimer;
        _circleGauge.fillAmount = 0f; // �Q�[�W����C�Ɍ���������
        _isCooldownActive = true;
    }

    // �N�[���_�E�����~���郁�\�b�h
    public void TongueUIStopCooldown() {
        _leftEye.enabled = true;
        _rightEye.enabled = true;
        _isCooldownActive = false;
        _circleGauge.fillAmount = 1f; // �N�[���_�E�����蓮�Œ�~���ꂽ�ꍇ�ɃQ�[�W�𖞃^���ɂ���
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
