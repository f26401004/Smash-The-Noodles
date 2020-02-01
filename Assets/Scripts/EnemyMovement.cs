using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {
    public Animator anim;

    private Vector3 targetPosition;

    void Start () {
        anim = GetComponentInChildren<Animator> ();
    }

    void Update () {
        Vector3 nextPosition = Vector3.Lerp (transform.position, targetPosition, 0.5f);
        float x = nextPosition.x - transform.position.x;
        anim.SetFloat ("HorizontalAxis", Mathf.Abs (x));

        if (x > 0) {
            transform.localScale = new Vector3 (1, 1, 1);
        } else if (x < 0) {
            transform.localScale = new Vector3 (-1, 1, 1);
        }

        transform.position = nextPosition;
    }

    public void WalkTo (Vector3 pos) {

        targetPosition = pos;
    }
}