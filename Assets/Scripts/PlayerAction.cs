using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : CharacterAction {

    public void Use () {
        if (this.hold == null) {
            return;
        }
        this.hold.Use ();
    }
    public void TryPick (Item item) {
		
        socket.itemSet.Remove(item.key);
        Pick(item);
    }

	public void Update()
	{
		if (Input.GetKeyDown(KeyCode.P) && touch)
		{
            TryPick(touch);
		}
		if (Input.GetKeyDown(KeyCode.O) && hold)
		{
            Drop();
		}
	}

	public void Drop () {
        if (hold == null) {
            return;
        }

        hold.gameObject.transform.parent = null;
        socket.itemSet.Add(hold.key, hold);
        hold.GetComponent<Collider2D>().enabled = true;
        hold.isHeld = false;
        hold = null;
    }
}