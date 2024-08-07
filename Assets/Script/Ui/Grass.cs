using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour {
    [SerializeField]
    float _near = 5;
    [SerializeField]
    Vector2 _min;
    GameObject _camera;
    Camera _cam;
    private float _movecam;
    SpriteRenderer _me;

    // Start is called before the first frame update
    void Start() {
        _camera = GameObject.Find("Main Camera");
        _cam = _camera.GetComponent<Camera>();
        _movecam = _camera.transform.position.x;
        _me = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update() {
        Vector3 camrect = _cam.ScreenToWorldPoint(Vector3.zero);
        if (transform.position.x <= camrect.x) {
            transform.position += new Vector3((_me.bounds.size.x * 2) - 0.1f, 0, 0);
        }
        if (_movecam < _camera.transform.position.x) {
            transform.position -= new Vector3(_near / 10 * (_camera.transform.position.x - _movecam), 0, 0);
            _movecam = _camera.transform.position.x;
        }
    }
}

