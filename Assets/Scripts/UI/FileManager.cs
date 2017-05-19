using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System;
using System.IO;
using System.Xml.Serialization;
using System.Linq;
using UnityEngine;

public struct Record
{

    public int scope;
    public string userName;
    public string circumStanceDead;
    public System.DateTime lastEntered;
    public float spendTime;

    public Record(System.DateTime lastEnteredGame, string userName, int scope, float spendTime, string circumStanceDead)
    {
        this.userName = userName;
        this.scope = scope;
        this.spendTime = spendTime;
        this.circumStanceDead = circumStanceDead;
        lastEntered = lastEnteredGame;
    }

}

public class FileManager : MonoBehaviour
{
    public static string FileNameData = "data.xml";
    public static string CurrentUserName = "No Name";

    public GameObject User;
    public GameObject Scope;
    public GameObject SpendTime;
    public GameObject LastStart;
    public GameObject CircumStance;

    public GameObject record;
    public List<Record> records = new List<Record>();
    private XmlDocument xmlDocument;

    public static void WriteInfo(string circumStanceDead)
    {
        XmlDocument document = new XmlDocument();
        document.Load(FileNameData);
        XmlElement xRoot = document.DocumentElement;
        XmlElement Param = document.CreateElement("record");
        XmlElement data = document.CreateElement("date");
        XmlElement time = document.CreateElement("time");
        XmlElement spendTime = document.CreateElement("spendTime");
        XmlElement circumStance = document.CreateElement("circumStance");
        XmlElement scope = document.CreateElement("scope");
        XmlAttribute curName = document.CreateAttribute("name");
        curName.Value = CurrentUserName;
        XmlText textData = document.CreateTextNode(System.DateTime.Now.ToShortDateString());
        XmlText textTime = document.CreateTextNode(System.DateTime.Now.ToShortTimeString());
        XmlText textSpendTime = document.CreateTextNode(Labyrinth.timeSpend.ToString());
        XmlText textCircumStance = document.CreateTextNode(circumStanceDead);
        XmlText textScope = document.CreateTextNode(StatusBar.count.ToString());

        data.AppendChild(textData);
        time.AppendChild(textTime);
        spendTime.AppendChild(textSpendTime);
        circumStance.AppendChild(textCircumStance);
        scope.AppendChild(textScope);
        Param.Attributes.Append(curName);
        Param.AppendChild(data);
        Param.AppendChild(time);
        Param.AppendChild(spendTime);
        Param.AppendChild(circumStance);
        Param.AppendChild(scope);

        xRoot.AppendChild(Param);
        document.Save(FileNameData);
    }

    void Start()
    {
        xmlDocument = new XmlDocument();
        xmlDocument.Load(FileNameData);
        XmlElement xmlRoot = xmlDocument.DocumentElement;
        foreach (XmlNode xmlNode in xmlRoot)
        {
            string currentName = "";
            if (xmlNode.Attributes.Count > 0)
            {
                XmlNode attr = xmlNode.Attributes.GetNamedItem("name");
                currentName = attr.InnerText;
            }
            string dateTime = "";
            string dateTimeSpend = "";
            string circumStance = "";
            string scope = "";
            foreach (XmlNode childNode in xmlNode.ChildNodes)
            {
                if (childNode.Name == "date")
                {
                    dateTime += childNode.InnerText;
                }
                if (childNode.Name == "time")
                {
                    dateTime += " " + childNode.InnerText;
                }
                if (childNode.Name == "spendTime")
                {
                    dateTimeSpend += childNode.InnerText;
                }
                if (childNode.Name == "circumStance")
                {
                    circumStance += childNode.InnerText;
                }
                if (childNode.Name == "scope")
                {
                    scope += childNode.InnerText;
                }
            }
            records.Add(new Record(Convert.ToDateTime(dateTime), currentName, Convert.ToInt32(scope), float.Parse(dateTimeSpend), circumStance));
        }
        for (int i = 0, k = 1; i < records.Count; i++, k++)
        {
            GameObject g = Instantiate(User, new Vector3(User.transform.position.x, User.transform.position.y - k * 3, -3), Quaternion.identity) as GameObject;
            g.GetComponent<TextMesh>().text = records[records.Count - i - 1].userName;
            g = Instantiate(Scope, new Vector3(Scope.transform.position.x, Scope.transform.position.y - k * 3, -3), Quaternion.identity) as GameObject;
            g.GetComponent<TextMesh>().text = records[records.Count - i - 1].scope.ToString();
            g = Instantiate(SpendTime, new Vector3(SpendTime.transform.position.x, SpendTime.transform.position.y - k * 3, -3), Quaternion.identity) as GameObject;
            g.GetComponent<TextMesh>().text = records[records.Count - i - 1].spendTime.ToString();
            g = Instantiate(LastStart, new Vector3(LastStart.transform.position.x, LastStart.transform.position.y - k * 3, -3), Quaternion.identity) as GameObject;
            g.GetComponent<TextMesh>().text = records[records.Count - i - 1].lastEntered.ToShortDateString();
            g = Instantiate(CircumStance, new Vector3(CircumStance.transform.position.x, CircumStance.transform.position.y - k * 3, -3), Quaternion.identity) as GameObject;
            g.GetComponent<TextMesh>().text = records[records.Count - i - 1].circumStanceDead;
        }
    }

}
