using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideLine : MonoBehaviour
{
    public GameObject startObject;
    public GameObject EndObject;
    public GameObject MidObject;

    [Range(0,1)]
    public float intervalRate = 1;//间隔比
    public Transform startPos,endPos;
    List<GameObject> linePoints = new List<GameObject>();

    Vector3 oldStartPos, oldEndPos;
    bool flag = false;
    // Start is called before the first frame update
    void Start()
    {
        CreateLine(startPos, endPos);
        oldStartPos = startPos.position;
        oldEndPos = endPos.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(startPos.position,oldStartPos)>0.01|| Vector3.Distance(endPos.position, oldEndPos) > 0.01)
        {
            flag = true;
        }
        if (!flag)
        {
            
        }
        else
        {
            print(Vector3.Distance(startPos.position, endPos.position));
            flag = false;
            DestroyLine();
            CreateLine(startPos, endPos);
            oldStartPos = startPos.position;
            oldEndPos = endPos.position;
        }
        
    }

    void CreateLine(Transform start, Transform end)
    {
        CreateStart(start);
        CreateMid(start, end);
        CreateEnd(end);
    }

    void CreateStart(Transform start)
    {
        GameObject temp = Instantiate(startObject);
        temp.transform.position = start.position;
        temp.transform.rotation = start.rotation;
        temp.name = "起点";
        linePoints.Add(temp);
    }
    void CreateEnd(Transform end)
    {
        GameObject temp = Instantiate(EndObject);
        temp.transform.position = end.position;
        temp.transform.rotation = end.rotation;
        temp.name = "终点";
        linePoints.Add(temp);
    }
    void CreateMid(Transform start, Transform end)
    {
        float dis = Vector3.Distance(start.position, end.position);
        int cnt = (int)dis/4;
        int cnt_Y = (int)(cnt * intervalRate);
        int cnt_N = cnt - cnt_Y;
        for(int i=0;i<cnt;i++)
        {
            GameObject temp = Instantiate(MidObject);
            temp.transform.position = start.position + (end.position - start.position).normalized * (i + intervalRate/2)*4;
            temp.transform.localScale = new Vector3(1,1 , 1);
            temp.transform.rotation = Quaternion.FromToRotation(new Vector3(-1, 0, 0), end.position - start.position);
            temp.name = "连接线" + i.ToString();
            linePoints.Add(temp);
        }
    }
    void DestroyLine()
    {
        foreach(GameObject linePoint in linePoints)
        {
            Destroy(linePoint);
        }
        linePoints.Clear();
    }
}
