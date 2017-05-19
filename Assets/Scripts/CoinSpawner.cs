using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : Spawner
{

    public static new int count;

    public override void Update()
    {
        if (count != 10)
        {
            base.Update();
        }
    }

    public override void Increase()
    {
        count++;
    }

    public override void Decrease()
    {
        count--;
    }

}
