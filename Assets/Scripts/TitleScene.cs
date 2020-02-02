using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour {
    // public GameObject startButton;
    // public GameObject exitButton;
    // private Animator startAnimator;
    // private Animator exitAnimator;
    private int currentIndex;
    // Start is called before the first frame update
    void Start () {
<<<<<<< HEAD
        startAnimator = startButton.GetComponent<Animator> ();
        exitAnimator = exitButton.GetComponent<Animator> ();
=======
        // startAnimator = startButton.GetComponent<Animator> ();
        // exitAnimator = exitButton.GetComponent<Animator> ();
>>>>>>> eedcfaed397b683d776e155ff1aca145bd861bda
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
    public void enterGame () {
        SceneManager.LoadScene ("level0");
    }
    public void exitGame () {
        Application.Quit ();
    }
}