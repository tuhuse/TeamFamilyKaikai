using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDeathJudgeScript : MonoBehaviour
{
    [SerializeField] private GameObject _cpu1;
    [SerializeField] private GameObject _cpu2;
    [SerializeField] private GameObject _cpu3;
    [SerializeField] private GameObject _cpu1DeathAnim;
    [SerializeField] private GameObject _cpu2DeathAnim;
    [SerializeField] private GameObject _cpu3DeathAnim;
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _playerDeathAnim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject == _cpu1) {
            _cpu1DeathAnim.SetActive(true);
        }
        if (collision.gameObject == _cpu2) {
            _cpu2DeathAnim.SetActive(true);
        }
        if (collision.gameObject == _cpu3) {
            _cpu3DeathAnim.SetActive(true);
        }
        if (collision.gameObject == _player) {
            _playerDeathAnim.SetActive(true);
        }
    }
}
