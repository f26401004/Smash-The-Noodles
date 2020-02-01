using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    public CharacterMovement p1, p2;
    private PlayerAction p1a, p2a;

	private void Start()
	{
        p1a = p1.GetComponent<PlayerAction>();
        p2a = p2.GetComponent<PlayerAction>();
    }

	void Update()
    {
        Vector2 p1dir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Vector2 p2dir = new Vector2(Input.GetAxis("Horizontal2"), Input.GetAxis("Vertical2"));

        p1.WalkTowards(p1dir);
        p2.WalkTowards(p2dir);

        if (Input.GetKeyDown(KeyCode.W)) p1.Jump();
        if (Input.GetKeyDown(KeyCode.UpArrow)) p2.Jump();

        if (Input.GetKeyDown(KeyCode.E) && p1a.touch)
        {
            p1a.Pick(p1a.touch);
        }
        if (Input.GetKeyDown(KeyCode.R) && p1a.hold)
        {
            p1a.Drop();
        }

        // p2
        if (Input.GetKeyDown(KeyCode.P) && p2a.touch)
        {
            p2a.Pick(p2a.touch);
        }
        if (Input.GetKeyDown(KeyCode.O) && p2a.hold)
        {
            p2a.Drop();
        }
    }
}
