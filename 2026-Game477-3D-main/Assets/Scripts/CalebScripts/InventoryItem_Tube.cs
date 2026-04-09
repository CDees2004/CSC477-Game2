using UnityEngine;
using State = evilLabStates;

public class InventoryItem_Tube : CalebInventoryItem
{
	public override void Use()
	{
		foreach (GameObject item in GameObject.FindGameObjectsWithTag("dynamicColor"))
		{
			item.GetComponent<Renderer>().material.color = Color.green;
		}
		console.SendMessage("ChangeState", State.GREEN, SendMessageOptions.DontRequireReceiver);
	}
}
