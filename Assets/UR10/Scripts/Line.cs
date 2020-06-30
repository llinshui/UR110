using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    float radius;
    Vector3 startPoint, endPoint;
    Material colorMat;
    Material colorMat1;

    int count;
    float length;

    float preRadius = 0.5f;
    float preLength = 2;
    List<GameObject> lines = new List<GameObject>();

    public void Create(float rad, Vector3 start, Vector3 end, Material mat, Material mat1)
    {
        radius = rad;
        startPoint = start;
        endPoint = end;
        colorMat = mat;
        colorMat1 = mat1;
        length = Vector3.Distance(startPoint, endPoint);
        count = (int)(length / preLength) +1;
        for(int i=0;i<count;i++)
        {
            GameObject gameObject=GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            gameObject.name = "高压线" + i.ToString();
            gameObject.transform.position = startPoint + (endPoint - startPoint).normalized*i*2;
            gameObject.transform.eulerAngles = new Vector3(0, 0, 90);
            gameObject.transform.localScale = new Vector3(radius / preRadius, preLength/2, radius / preRadius);
            gameObject.GetComponent<MeshRenderer>().material = colorMat;
            lines.Add(gameObject);
        }
    }
    public void DestroyLines()
    {
        for (int i = 0; i < lines.Count; i++)
        {
            Destroy(lines[i]);
        }
        lines.Clear();
    }
}
