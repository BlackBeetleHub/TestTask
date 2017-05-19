using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


[Serializable]
public class record
{

    public int scope;
    public float spendTime;
    public string date;
    public string circumStance;
    public DateTime time = DateTime.Now;

    public record() { }

    public record(string dt, int scp, float spend, string cir)
    {
        date = dt;
        //time = dt;
        scope = scp;
        spendTime = spend;
        circumStance = cir;
    }
}

public class Human : Entity
{
    public override void Attack() { }
    public override void GetHit(Entity entity) { }

    public override void Start()
    {
        base.Start();
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        string enemyName = collision.gameObject.tag;
        if (enemyName == "Zombie" || enemyName == "Mummy")
        {
            FileManager.WriteInfo(enemyName);
            CoinSpawner.count = 0;
            StatusBar.count = 0;
            Application.LoadLevel("menu");
        }
    }
}

