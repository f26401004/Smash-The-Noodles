using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Animator anim;

    void Start() {
        anim = GetComponentInChildren<Animator>();
    }

    public void WalkTo(Vector3 pos){
        float x = pos.x - transform.position.x;
        anim.SetFloat("HorizontalAxis", Mathf.Abs(x));

        if(x > 0){
            transform.localScale = new Vector3(1, 1, 1);
        }else if(x < 0){
            transform.localScale = new Vector3(-1, 1, 1);
        }

        transform.position = pos;
    }
}
