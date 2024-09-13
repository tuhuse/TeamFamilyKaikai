using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStop : MonoBehaviour {
    private int _commentNumber = default;
    private bool _isGiveComment = false;
    [SerializeField] private CommentScript _comment = default;
    [SerializeField] private bool _isStartObject = false;
    // Start is called before the first frame update
    void Start() 
    {
        if (_isStartObject) {
            _comment.TutorialCommentChange();
        }
        
    }

    // Update is called once per frame
    void Update() {

    }
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        string player = "Player";
        if (collision.gameObject.CompareTag(player)) 
        {
            collision.gameObject.GetComponent<Player2>().WaitStart();
            _comment.TutorialCommentChange();

        }
    }
}
