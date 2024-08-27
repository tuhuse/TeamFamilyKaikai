using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongueGaugeScriptKai : MonoBehaviour
{
    private Transform _gaugeTransform;
    private Vector2 _full = new Vector2(1f, 1f);
    private Vector2 _empty = new Vector2(0f, 1f);
    private float _fillSpeed = 5;//ゲージが満タンになるまでの速度
    private float _timeAmount = 1;//ゲージが満タンになるまでの目標値
    private bool _isFilling = false;//ゲージが満タンかどうかのbool
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        //ゲージを目標値まであげる
        if (_isFilling) {
            _gaugeTransform.localScale = Vector2.Lerp(_gaugeTransform.localScale,
                Vector2.Lerp(_empty, _full, _timeAmount), _fillSpeed * Time.deltaTime);
        }
        //ゲージが満タンになったら消す
        if (Vector2.Distance(_gaugeTransform.localScale, Vector2.Lerp(_empty, _full, _timeAmount)) < 0.01f) {
            _isFilling = false;
            gameObject.SetActive(false);
        }
    }
    public void StartCoolDown(float coolDownDuration) {
        _gaugeTransform.localScale = _empty;//ゲージリセット
        _timeAmount = 1f;//ゲージの満タンを目標地位設定
        _fillSpeed = 1f / coolDownDuration;//ゲージの充電速度設定
        _isFilling = true;//ゲージの充電を開始
        gameObject.SetActive(true);//ゲージ表示
    }
}
