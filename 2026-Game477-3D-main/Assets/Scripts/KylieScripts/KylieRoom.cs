using System;
using System.Collections.Generic;
using UnityEngine;
using WaterState = WaterPuzzleStates;

public enum WaterPuzzleStates
{
    DRY,
    RAINY
}

public class KylieRoom : PuzzleManager
{
    public WaterState WState { get; private set; }
    public GameObject pondwater;

    private void Start()
    {
        WState = WaterState.RAINY;
    }

    private void Update()
    {
        switch (WState)
        {
            case WaterState.DRY:
                pondwater.SetActive(false);
                break;

            case WaterState.RAINY:
                pondwater.SetActive(true);
                break;
        }
    }

    public void WaterChangeState(WaterState newState)
    {
        WState = newState;
    }
}
