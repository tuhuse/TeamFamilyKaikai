using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongueGaugeScriptKai : MonoBehaviour
{
    private Transform _gaugeTransform;
    private Vector2 _full = new Vector2(1f, 1f);
    private Vector2 _empty = new Vector2(0f, 1f);
    private float _fillSpeed = 5;//�Q�[�W�����^���ɂȂ�܂ł̑��x
    private float _timeAmount = 1;//�Q�[�W�����^���ɂȂ�܂ł̖ڕW�l
    private bool _isFilling = false;//�Q�[�W�����^�����ǂ�����bool
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        //�Q�[�W��ڕW�l�܂ł�����
        if (_isFilling) {
            _gaugeTransform.localScale = Vector2.Lerp(_gaugeTransform.localScale,
                Vector2.Lerp(_empty, _full, _timeAmount), _fillSpeed * Time.deltaTime);
        }
        //�Q�[�W�����^���ɂȂ��������
        if (Vector2.Distance(_gaugeTransform.localScale, Vector2.Lerp(_empty, _full, _timeAmount)) < 0.01f) {
            _isFilling = false;
            gameObject.SetActive(false);
        }
    }
    public void StartCoolDown(float coolDownDuration) {
        _gaugeTransform.localScale = _empty;//�Q�[�W���Z�b�g
        _timeAmount = 1f;//�Q�[�W�̖��^����ڕW�n�ʐݒ�
        _fillSpeed = 1f / coolDownDuration;//�Q�[�W�̏[�d���x�ݒ�
        _isFilling = true;//�Q�[�W�̏[�d���J�n
        gameObject.SetActive(true);//�Q�[�W�\��
    }
}
