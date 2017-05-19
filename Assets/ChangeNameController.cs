using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

public class ChangeNameController : MonoBehaviour
{

    public Button yourButton;
    public InputField iField;
    public GameObject tex;

    [SerializeField]
    public string text;

    void Start()
    {
        Button btn = GetComponent<Button>();
        text = iField.text;
        
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {

        XmlSerializer formatter = new XmlSerializer(typeof(string));
        text = iField.text;
        FileManager.CurrentUserName = text;
        using (FileStream fs = new FileStream(FileManager.FileNameConfig, FileMode.OpenOrCreate))
        {
            formatter.Serialize(fs, text);
        }
    }
}
