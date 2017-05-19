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
    public System.DateTime spendTime;

    public Record(System.DateTime lastEnteredGame, string userName, int scope, System.DateTime spendTime, string circumStanceDead)
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
    public const string FILE_NAME_DATA = "data.xml";
    public const string FILE_NAME_CONFIG = "config.xml";

    public GameObject record;
    public List<Record> records = new List<Record>();
    private XmlDocument xmlDocument;

    public static void WriteInfo(string circumStanceDead)
    {
        XmlDocument document = new XmlDocument();
        document.Load(FILE_NAME_DATA);
        XmlElement xRoot = document.DocumentElement;
        XmlElement Param = document.CreateElement("record");
        XmlElement data = document.CreateElement("date");
        XmlElement time = document.CreateElement("time");
        XmlElement spendTime = document.CreateElement("spendTime");
        XmlElement circumStance = document.CreateElement("circumStance");
        XmlElement scope = document.CreateElement("scope");

        XmlText textData = document.CreateTextNode(System.DateTime.Now.ToShortDateString());
        XmlText textTime = document.CreateTextNode(System.DateTime.Now.ToShortTimeString());
        XmlText textSpendTime = document.CreateTextNode(System.DateTime.Now.ToShortTimeString());
        XmlText textCircumStance = document.CreateTextNode(circumStanceDead);
        XmlText textScope = document.CreateTextNode(StatusBar.count.ToString());

        data.AppendChild(textData);
        time.AppendChild(textTime);
        spendTime.AppendChild(textSpendTime);
        circumStance.AppendChild(textCircumStance);
        scope.AppendChild(textScope);

        Param.AppendChild(data);
        Param.AppendChild(time);
        Param.AppendChild(spendTime);
        Param.AppendChild(circumStance);
        Param.AppendChild(scope);

        xRoot.AppendChild(Param);
        document.Save(FILE_NAME_DATA);
    }

    void Start()
    {
        xmlDocument = new XmlDocument();
        XmlDocument xmlDocumentConfig = new XmlDocument();
        xmlDocumentConfig.Load(FILE_NAME_CONFIG);
        XmlElement xTmpRoot = xmlDocumentConfig.DocumentElement;
        xmlDocument.Load(FILE_NAME_DATA);
        XmlElement xmlRoot = xmlDocument.DocumentElement;
        string name = "";
        foreach (XmlNode xmlNode in xmlDocumentConfig)
        {
            if (xmlNode.Name == "user")
            {
                name += xmlNode.InnerText;
            }
        }

        XmlSerializer formatter = new XmlSerializer(typeof(string));

        using (FileStream fileStream = new FileStream(FILE_NAME_CONFIG, FileMode.OpenOrCreate))
        {
            name += (string)formatter.Deserialize(fileStream);
        }

        foreach (XmlNode xmlNode in xmlRoot)
        {
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
            records.Add(new Record(Convert.ToDateTime(dateTime), name, Convert.ToInt32(scope), Convert.ToDateTime(dateTimeSpend), circumStance));
        }
        for (int i = 0; i < records.Count; i++)
        {
            GameObject go = Instantiate(record, new Vector3(record.transform.position.x, record.transform.position.y - i * 3, -3), Quaternion.identity) as GameObject;
            go.GetComponent<TextMesh>().text = records[i].userName + "          " + records[i].scope + "            " + records[i].spendTime.Minute + "              " + records[i].lastEntered.ToShortDateString() + "             " + records[i].circumStanceDead;
        }
    }

}
