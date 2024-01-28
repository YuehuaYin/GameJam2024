using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerRight : Customer
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
            bottleGame.finishRight(bigDaddyWholeThing);
        }
    }

    protected override void fail()
    {
        bottleGame.failRight(bigDaddyWholeThing);
    }
}
