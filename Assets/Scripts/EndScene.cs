using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndScene : MonoBehaviour {
    public Text winnerText;
    private int currentIndex;
    // Start is called before the first frame update
    void Start () {
        // startAnimator = startButton.GetComponent<Animator> ();
        // exitAnimator = exitButton.GetComponent<Animator> ();
        currentIndex = 0;
        winnerText.text = $"Player {Winner.winnerIndex + 1} Win!!";
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
    public void returnToTitle () {
        SceneManager.LoadScene ("title");
    }
    public void exitGame () {
        Application.Quit ();
    }
}