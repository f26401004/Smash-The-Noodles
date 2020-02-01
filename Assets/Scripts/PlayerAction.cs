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
		if (socket.isLeader)
		{
            // 直接撿起，並送Pick
            socket.itemSet.Remove(item.key);
            Pick(item);
		}
		else
		{
            // send try pick message
            socket.sendMessage(3, payload(item));
        }
        
    }

	public void Update()
	{
		if (Input.GetKeyDown(KeyCode.P) && touch)
		{
            TryPick(touch);
		}
	}

	public void Drop () {
        if (hold == null) {
            return;
        }
        // send drop message to room manager
        socket.sendMessage (6, payload (hold));

        hold.gameObject.transform.parent = null;
        socket.itemSet.Add(hold.key, hold);
        hold = null;
    }
}