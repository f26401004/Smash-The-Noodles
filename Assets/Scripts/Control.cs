using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    public CharacterMovement p1, p2;
    
    void Update()
    {
        Vector2 p1dir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Vector2 p2dir = new Vector2(Input.GetAxis("Horizontal2"), Input.GetAxis("Vertical2"));

        p1.WalkTowards(p1dir);
        p2.WalkTowards(p2dir);

        if (Input.GetKeyDown(KeyCode.W)) p1.Jump();
        if (Input.GetKeyDown(KeyCode.UpArrow)) p2.Jump();
    }
}
