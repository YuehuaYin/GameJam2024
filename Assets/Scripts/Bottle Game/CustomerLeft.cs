using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerLeft : Customer
{
    public override void Fill()
    {
        if (currectLiquid < targetLiquid)
        {
            currectLiquid++;
            AddLiquid();
        }
        else
        {
            bottleGame.finishLeft(bigDaddyWholeThing);
        }
    }

    protected override void fail()
    {
        bottleGame.failLeft(bigDaddyWholeThing);
    }
}
