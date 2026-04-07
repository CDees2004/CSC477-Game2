using UnityEngine;
using UnityEngine.UI;

public class InventoryItem_Book : InventoryItem
{
    // room selection panel and buttons
    public GameObject roomPanelUI;
    public GameObject chandlerRoomButton;
    // add other teammate buttons here

    private void Start()
    {
        if (roomPanelUI != null)
        {
            roomPanelUI.SetActive(false);
        }

        if(chandlerRoomButton != null)
        {
            Button chandlerRoom = chandlerRoomButton.GetComponent<Button>();
            chandlerRoom.onClick.AddListener(()=> // have to use a nasty lambda for this 
            SceneController.LoadPuzzleRoom("ChandlerRoom")); 
        }
    }


    public override void Use()
    {
        print("Used podium");
        // open the menu to allow for room transportation 
        //if(roomPanelUI != null)
        //{
        //    roomPanelUI.SetActive(true); 
        //}
        // changing scene according to book used 
        SceneController.LoadPuzzleRoom("ChandlerRoom"); 
    }
}
