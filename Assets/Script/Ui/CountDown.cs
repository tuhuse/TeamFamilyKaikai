using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDown : MonoBehaviour
{
    [SerializeField] private GameObject _count1;
    [SerializeField] private GameObject _count2;
    [SerializeField] private GameObject _count3;
    [SerializeField] private GameObject _startText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Count1() {
        _count1.SetActive(false);
    }
    public void Count2() {
        _count2.SetActive(false);
        _count1.SetActive(true);
    }
    public void Count3() {
        _count3.SetActive(false);
        _count2.SetActive(true);
        _startText.SetActive(true);
    }
}
