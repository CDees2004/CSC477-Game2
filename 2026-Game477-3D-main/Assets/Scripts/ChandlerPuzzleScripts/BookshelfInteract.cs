using UnityEngine;

public class BookshelfInteract : MonoBehaviour
{
    public static BookshelfInteract Instance { get; private set; }

    public Books currentBookSlot;


    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 3.0f))
        {
            currentBookSlot = hit.collider.GetComponent<Books>();
        }
        else
        {
            currentBookSlot = null; 
        }
    }
}
