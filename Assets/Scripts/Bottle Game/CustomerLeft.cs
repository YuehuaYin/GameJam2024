using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerLeft : Customer
{
    public void fill()
    {
        if (currectLiquid < targetLiquid)
        {
            currectLiquid++;
            addLiquid();
        }
        else
        {
            bottleGame.GetComponent<BottleGame>().finishLeft();
        }
    }
}
