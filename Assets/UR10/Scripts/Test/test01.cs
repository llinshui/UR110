using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class test01 : MonoBehaviour
{
    public Material mat;
    public Material tishi;
    Line line;
    Vector3[] point = new Vector3[2];
    public GameObject LineUI;
    public Text[] tx = new Text[6];
    // Start is called before the first frame update
    void Start()
    {
        line = new Line();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.L))
        {
            LineUI.SetActive(true);
        }
    }
    public void CreateLine()
    {
        Text2Vector3();
        line.DestroyLines();
        line.Create(1.7f, point[0], point[1], mat, tishi);
        LineUI.SetActive(false);
    }
    void Text2Vector3()
    {
        point[0].x = (float)System.Convert.ToDouble(tx[0].text);
        point[0].y = (float)System.Convert.ToDouble(tx[1].text);
        point[0].z = (float)System.Convert.ToDouble(tx[2].text);

        point[1].x = (float)System.Convert.ToDouble(tx[3].text);
        point[1].y = (float)System.Convert.ToDouble(tx[4].text);
        point[1].z = (float)System.Convert.ToDouble(tx[5].text);
    }
}
