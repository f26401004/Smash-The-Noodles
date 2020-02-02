using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
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
    public VideoPlayer video;
    private int currentIndex;
    // Start is called before the first frame update
    void Start () {
        // startAnimator = startButton.GetComponent<Animator> ();
        // exitAnimator = exitButton.GetComponent<Animator> ();
        currentIndex = 0;
        about.SetActive(false);
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
        about.SetActive(true);
        EventSystem.current.SetSelectedGameObject (exitAbout);
        video.Play();
    }
    public void closeAboutCanvas () {
        about.SetActive(false);
        EventSystem.current.SetSelectedGameObject (start);
        video.Stop();
    }

    public void enterGame () {
        SceneManager.LoadScene ("level0");
    }
    public void exitGame () {
        Application.Quit ();
    }
}