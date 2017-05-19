using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mummy : Zombie
{

    public override void Start()
    {
        base.Start();
        speed = speed * 2;
    }

    public override void Update()
    {
        base.Update();
    }

}
