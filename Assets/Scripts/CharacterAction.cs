using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAction : MonoBehaviour {

    public Socket socket;
    private Item hold;
    private Item touch;

    private string payload (Item item) => $"{{\"x\":\"{item.gameObject.transform.position.x}\",\"y\":\"{item.gameObject.transform.position.y}\",\"item\":\"{item.type.ToString()}\",\"key\":\"{item.key}\"}}";

    public void TryPick (Item item) {
        // send try pick message
        socket.senMessage (3, payload (item));
    }

    void OnTriggerStay2D (Collider2D other) {
        vat item = other.GetComponent<Item> ();
        if (!item) {
            return;
        }
        touch = item;
    }

}