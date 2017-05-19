using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MummySpawnerController : MummySpawner
{
    public const int MUMMY_MAX_COUNT = 1;
    public const int COIN_MAX_COUNT = 10;

    public override void Update()
    {
        if (count < MUMMY_MAX_COUNT && StatusBar.count > COIN_MAX_COUNT)
        {
            base.Update();
        }
    }
}
