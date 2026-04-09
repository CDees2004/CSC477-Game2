using UnityEngine;
using State = evilLabStates;

public class InventoryItem_RedFlask : CalebInventoryItem
{
    public override void Use()
    {
        foreach (GameObject item in GameObject.FindGameObjectsWithTag("dynamicColor"))
        {
            item.GetComponent<Renderer>().material.color = Color.red;
        }
        console.SendMessage("ChangeState", State.RED, SendMessageOptions.DontRequireReceiver);
    }
}
