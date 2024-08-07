using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectGameOverScript : MonoBehaviour
{
    [SerializeField] private GameObject[] _button;
    public int _select = 0;
    // Start is called before the first frame update
    void Start()
    {
        _button[_select].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void RightCursor() {
        _button[_select].SetActive(false);
        _select++;
        if (_select >= _button.Length) {
            print("Right++");
            _select = 0;
        }
        _button[_select].SetActive(true);
    }
    public void LeftCursor() {
        _button[_select].SetActive(false);
        _select--;
        if (_select < 0) {
            _select = _button.Length - 1;
        }
        _button[_select].SetActive(true);

    }
}
