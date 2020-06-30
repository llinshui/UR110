using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OBBCreate : MonoBehaviour
{
    public Transform[] vertexs = new Transform[8];
    public Material mat;
    GameObject[] edges = new GameObject[12];
    LineRenderer[] lines = new LineRenderer[12];
    public float line_width = 1;
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] edges = new GameObject[12];
        for (int i=0;i<12;i++)
        {
            edges[i] = new GameObject();
            edges[i].AddComponent<LineRenderer>();
            lines[i] = edges[i].GetComponent<LineRenderer>();
            lines[i].material = mat;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateOBB(vertexs);
    }
    void UpdateOBB(Transform[] transforms)
    {
        lines[0].startColor = Color.yellow;
        lines[0].endColor = Color.yellow;
        lines[0].startWidth = line_width;
        lines[0].endWidth = line_width;
        lines[0].positionCount = 2;
        lines[0].positionCount = 2;
        //设置指示线的起点和终点
        lines[0].SetPosition(0, transforms[0].position);
        lines[0].SetPosition(1, transforms[1].position);

        lines[1].startColor = Color.yellow;
        lines[1].endColor = Color.yellow;
        lines[1].startWidth = line_width;
        lines[1].endWidth = line_width;
        lines[1].positionCount = 2;
        //设置指示线的起点和终点
        lines[1].SetPosition(0, transforms[1].position);
        lines[1].SetPosition(1, transforms[2].position);

        lines[2].startColor = Color.yellow;
        lines[2].endColor = Color.yellow;
        lines[2].startWidth = line_width;
        lines[2].endWidth = line_width;
        lines[2].positionCount = 2;
        //设置2示线的起点和终点
        lines[2].SetPosition(0, transforms[2].position);
        lines[2].SetPosition(1, transforms[3].position);

        lines[3].startColor = Color.yellow;
        lines[3].endColor = Color.yellow;
        lines[3].startWidth = line_width;
        lines[3].endWidth = line_width;
        lines[3].positionCount = 2;
        //设置3示线的起点和终点
        lines[3].SetPosition(0, transforms[3].position);
        lines[3].SetPosition(1, transforms[0].position);

        lines[4].startColor = Color.yellow;
        lines[4].endColor = Color.yellow;
        lines[4].startWidth = line_width;
        lines[4].endWidth = line_width;
        lines[4].positionCount = 2;
        //设置4示线的起点和终点
        lines[4].SetPosition(0, transforms[0].position);
        lines[4].SetPosition(1, transforms[4].position);

        lines[5].startColor = Color.yellow;
        lines[5].endColor = Color.yellow;
        lines[5].startWidth = line_width;
        lines[5].endWidth = line_width;
        lines[5].positionCount = 2;
        //设置5示线的起点和终点
        lines[5].SetPosition(0, transforms[1].position);
        lines[5].SetPosition(1, transforms[5].position);

        lines[6].startColor = Color.yellow;
        lines[6].endColor = Color.yellow;
        lines[6].startWidth = line_width;
        lines[6].endWidth = line_width;
        lines[6].positionCount = 2;
        //设置示线的起点和终点
        lines[6].SetPosition(0, transforms[2].position);
        lines[6].SetPosition(1, transforms[6].position);


        lines[7].startColor = Color.yellow;
        lines[7].endColor = Color.yellow;
        lines[7].startWidth = line_width;
        lines[7].endWidth = line_width;
        lines[7].positionCount = 2;
        //设置指示线的起点和终点
        lines[7].SetPosition(0, transforms[3].position);
        lines[7].SetPosition(1, transforms[7].position);


        lines[8].startColor = Color.yellow;
        lines[8].endColor = Color.yellow;
        lines[8].startWidth = line_width;
        lines[8].endWidth = line_width;
        lines[8].positionCount = 2;
        //设置示线的起点和终点
        lines[8].SetPosition(0, transforms[4].position);
        lines[8].SetPosition(1, transforms[5].position);

        lines[9].startColor = Color.yellow;
        lines[9].endColor = Color.yellow;
        lines[9].startWidth = line_width;
        lines[9].endWidth = line_width;
        lines[9].positionCount = 2;
        //设置9示线的起点和终点
        lines[9].SetPosition(0, transforms[5].position);
        lines[9].SetPosition(1, transforms[6].position);


        lines[10].startColor = Color.yellow;
        lines[10].endColor = Color.yellow;
        lines[10].startWidth = line_width;
        lines[10].endWidth = line_width;
        lines[10].positionCount = 2;
        //设置指示线的起点和终点
        lines[10].SetPosition(0, transforms[6].position);
        lines[10].SetPosition(1, transforms[7].position);

        lines[11].startColor = Color.yellow;
        lines[11].endColor = Color.yellow;
        lines[11].startWidth = line_width;
        lines[11].endWidth = line_width;
        lines[11].positionCount = 2;
        //设置指示线的起点和终点
        lines[11].SetPosition(0, transforms[7].position);
        lines[11].SetPosition(1, transforms[4].position);
    }
}
