using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoriteScrpit : MonoBehaviour
{
    private float _speed = -20f;
        
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0f, _speed * Time.deltaTime /2, 0f);
        transform.Translate(_speed * Time.deltaTime, 0f, 0f);
        
    }
}
