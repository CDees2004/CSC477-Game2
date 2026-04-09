using System;
using UnityEngine;

public class Books : MonoBehaviour
{
    // set in inspector 
    public string requiredBookID;
    // swapping these objects 
    public GameObject filledVisual;
    public GameObject missingBookVisual;

    public Boolean isFilled = false;

    public bool TryPlace(string bookID)
    {
        if (isFilled) return false;

        if (bookID == requiredBookID)
        {
            // places the book and checks if its placed correctly 
            isFilled = true;

            filledVisual.SetActive(true);
            missingBookVisual.SetActive(false);

            PuzzleController.Instance.CheckSolved();

            return true;
        }
        // wrong book
        return false; 
    }
}
