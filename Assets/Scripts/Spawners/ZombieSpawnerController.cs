using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawnerController : ZombieSpawner
{
    public override void Update()
    {
        if (StatusBar.count > 5 && count < 1)
        {
            base.Update();
        }
    }
}
