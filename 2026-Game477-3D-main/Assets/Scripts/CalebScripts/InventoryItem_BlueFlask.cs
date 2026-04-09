using UnityEngine;
using State = evilLabStates;


public class InventoryItem_BlueFlask : CalebInventoryItem
{   
    public override void Use()
    {
        foreach (GameObject item in GameObject.FindGameObjectsWithTag("dynamicColor"))
        {
            item.GetComponent<Renderer>().material.color = Color.blue;
        }
        console.SendMessage("ChangeState", State.BLUE, SendMessageOptions.DontRequireReceiver);
        
    }
}
