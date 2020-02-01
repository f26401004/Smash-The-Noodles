using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScene : MonoBehaviour {
    private int currentIndex;
    // Start is called before the first frame update
    void Start () {
        // startAnimator = startButton.GetComponent<Animator> ();
        // exitAnimator = exitButton.GetComponent<Animator> ();
        currentIndex = 0;
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown (KeyCode.UpArrow)) {
            currentIndex = (currentIndex + 3) % 2;
        }
        if (Input.GetKeyDown (KeyCode.DownArrow)) {
            currentIndex = (currentIndex + 1) % 2;
        }
    }

    // Update is called once per frame
    void Update () {

    }

    public void enterGame () {
        SceneManager.LoadScene ("level0");
    }
    public void returnToTitle () {
        SceneManager.LoadScene ("title");
    }
    public void exitGame () {
        Application.Quit ();
    }
}