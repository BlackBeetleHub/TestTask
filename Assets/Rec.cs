using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rec : MonoBehaviour {

    public Rec(Record record)
    {
        userName.GetComponent<TextMesh>().text += " " + record.userName;
        Scope.GetComponent<TextMesh>().text += " " + record.scope;
        SpendTime.GetComponent<TextMesh>().text += " " + record.spendTime;
        LastStart.GetComponent<TextMesh>().text += " " + record.lastEntered;
        CircumStance.GetComponent<TextMesh>().text += " " + record.circumStanceDead;
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public GameObject userName;
    public GameObject Scope;
    public GameObject SpendTime;
    public GameObject LastStart;
    public GameObject CircumStance;
}
