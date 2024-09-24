using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TutorialMnager : MonoBehaviour {
    [SerializeField] private GameObject _titlePanel;
    [SerializeField] private GameObject _comentetar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Skip")||Input.GetKeyDown(KeyCode.Return)) {
            StartCoroutine(FeedOut());
        }
    }

    private void OnTriggerEnter2D(Collider2D gate) {
        if (gate.gameObject.CompareTag("Player")) {
            StartCoroutine(FeedOut());
        }
    }
    private IEnumerator FeedOut() {
        _comentetar.SetActive(false);
        _titlePanel.gameObject.GetComponent<Animator>().enabled = true;
        float waittime = 1;
        yield return new WaitForSeconds(waittime);
        SceneManager.LoadScene("StageScene");
    }
}
