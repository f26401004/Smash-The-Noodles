using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour {
    // public GameObject startButton;
    // public GameObject exitButton;
    // private Animator startAnimator;
    // private Animator exitAnimator;
    public GameObject about;
    public GameObject start;
    public GameObject exitAbout;
    private int currentIndex;
    // Start is called before the first frame update
    void Start () {
        // startAnimator = startButton.GetComponent<Animator> ();
        // exitAnimator = exitButton.GetComponent<Animator> ();
        currentIndex = 0;
        about.active = false;
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
    public void openAboutCanvas () {
        about.active = true;
        EventSystem.current.SetSelectedGameObject (exitAbout);
    }
    public void closeAboutCanvas () {
        about.active = false;
        EventSystem.current.SetSelectedGameObject (start);
    }

    public void enterGame () {
        SceneManager.LoadScene ("level0");
    }
    public void exitGame () {
        Application.Quit ();
    }
}