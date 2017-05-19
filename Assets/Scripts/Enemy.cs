using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{

    public override void Attack()
    {
        //Impl
    }

    public override void GetHit(Entity entity)
    {
        //Imlp
    }

    public override void ExecuteAILogic()
    {
        direction = Labyrinth.navigateMaze.GetRandomDirection(current);
    }
}