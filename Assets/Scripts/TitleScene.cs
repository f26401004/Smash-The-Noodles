using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : MonoBehaviour {
    public GameObject startButton;
    public GameObject exitButton;
    private Animator startAnimator;
    private Animator exitAnimator;
    private int currentIndex;
    // Start is called before the first frame update
    void Start () {
        startAnimator = startButton.GetComponent<Animator> ();
        endAnimator = endButton.GetComponent<Animator> ();
        currentIndex = 0;
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown (KeyCode.Up)) {
            currentIndex = (currentIndex + 3) % 2
        }
        if (Input.GetKeyDown (KeyCode.Down)) {
            currentIndex = (currentIndex + 1) % 2
        }
    }
}