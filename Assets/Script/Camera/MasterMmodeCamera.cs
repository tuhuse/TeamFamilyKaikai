using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterMmodeCamera : MonoBehaviour
{
    [SerializeField] private Transform _frogTransform;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

      

    }
    private void LateUpdate() {
        transform.position = new Vector3(_frogTransform.position.x, 8,-10);
    }
}
