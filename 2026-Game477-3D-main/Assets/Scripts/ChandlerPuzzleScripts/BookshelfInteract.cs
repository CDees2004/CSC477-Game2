using UnityEngine;

public class BookshelfInteract : MonoBehaviour
{
    public static BookshelfInteract Instance { get; private set; }

    public BookshelfSlot currentBookSlot;


    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 3.0f))
        {
            currentBookSlot = hit.collider.GetComponent<BookshelfSlot>();
        }
        else
        {
            currentBookSlot = null; 
        }
    }
}
