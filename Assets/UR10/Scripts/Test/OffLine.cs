using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OffLine : MonoBehaviour
{
    //离线任务参数
    XMLRead xmlRead;
    List<Mission> mission_List = new List<Mission>();
    int index = -1;
    Mission BoMission;
    string fileName;

    //实时显示
    public GameObject[] URJoints = new GameObject[6];
    float[] current_Pos = new float[6];
    public GameObject[] tools = new GameObject[2];
    public GameObject[] toolsJia = new GameObject[2];
    public void MissionChoose(int xml_index)
    {
        switch (xml_index)
        {
            case 0:
                fileName = "ningluoshuan.xml";
                break;
            case 1:
                fileName = "boxian.xml";
                break;
            case 2:
                fileName = "fangxianjia.xml";
                break;
            case 3:
                fileName = "jiaxian.xml";
                break;
            default:
                fileName = "init.xml";
                break;
        }
        print(fileName);
        XMLLoad xmlLoad = new XMLLoad();
        StartCoroutine(xmlLoad.GetXML(fileName));
        mission_List = xmlLoad.mission_List;
        index = -1;
    }
    //下一步任务按钮
    public void NextMission()
    {
        index++;
        if (index < mission_List.Count)
        {
            //显示任务细节文本
            //txIndex.text = (index + 1).ToString();
            //print(CommandScripts.MissionDo(mission_List[index], AccelerationRate, SpeedRate));
            if (mission_List[index].IOindex == -1)//运动命令
            {
                for (int i = 0; i < 6; i++)
                {
                    current_Pos[i]=(float)mission_List[index].Angles[i];
                }
            }
            else if (mission_List[index].IOindex == 0)//装工具
            {
                if(fileName== "ningluoshuan.xml")
                {
                    tools[1].SetActive(true);
                    toolsJia[1].SetActive(false);
                }
                else if (fileName == "boxian.xml")
                {
                    tools[0].SetActive(true);
                    toolsJia[0].SetActive(false);
                }
                else if (fileName == "fangxianjia.xml")
                {
                    tools[0].SetActive(true);
                    toolsJia[0].SetActive(false);
                }
            }
            else if (mission_List[index].IOindex == 1)//关工具
            {
                if (fileName == "ningluoshuan.xml")
                {
                    tools[1].SetActive(false);
                    toolsJia[1].SetActive(true);
                }
                else if (fileName == "boxian.xml")
                {
                    tools[0].SetActive(false);
                    toolsJia[0].SetActive(true);
                }
                else if (fileName == "fangxianjia.xml")
                {
                    tools[0].SetActive(false);
                    toolsJia[0].SetActive(false);
                    toolsJia[1].SetActive(true);
                }
            }

        }
        else
        {

        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //URJoints[0].transform.localEulerAngles = new Vector3(URJoints[0].transform.localEulerAngles.x, -current_Pos[0], URJoints[0].transform.localEulerAngles.z);
        //URJoints[1].transform.localEulerAngles = new Vector3(URJoints[1].transform.localEulerAngles.x, URJoints[1].transform.localEulerAngles.y, current_Pos[1] + 90);
        //URJoints[2].transform.localEulerAngles = new Vector3(URJoints[2].transform.localEulerAngles.x, URJoints[2].transform.localEulerAngles.y, current_Pos[2]);
        //URJoints[3].transform.localEulerAngles = new Vector3(URJoints[3].transform.localEulerAngles.x, URJoints[3].transform.localEulerAngles.y, current_Pos[3] + 90);
        //URJoints[4].transform.localEulerAngles = new Vector3(URJoints[4].transform.localEulerAngles.x, -current_Pos[4], URJoints[4].transform.localEulerAngles.z);
        //URJoints[5].transform.localEulerAngles = new Vector3(URJoints[5].transform.localEulerAngles.x, URJoints[5].transform.localEulerAngles.y, current_Pos[5]);

        URJoints[0].transform.localEulerAngles = new Vector3(URJoints[0].transform.localEulerAngles.x, -current_Pos[0], URJoints[0].transform.localEulerAngles.z);
        URJoints[1].transform.localEulerAngles = new Vector3(URJoints[1].transform.localEulerAngles.x, -(current_Pos[1] + 90), URJoints[1].transform.localEulerAngles.z);
        URJoints[2].transform.localEulerAngles = new Vector3(URJoints[2].transform.localEulerAngles.x, -current_Pos[2], URJoints[2].transform.localEulerAngles.z);
        URJoints[3].transform.localEulerAngles = new Vector3(URJoints[3].transform.localEulerAngles.x, -current_Pos[3] + 90, URJoints[3].transform.localEulerAngles.z);
        URJoints[4].transform.localEulerAngles = new Vector3(URJoints[4].transform.localEulerAngles.x, -current_Pos[4] - 180, URJoints[4].transform.localEulerAngles.z);
        URJoints[5].transform.localEulerAngles = new Vector3(URJoints[5].transform.localEulerAngles.x, current_Pos[5], URJoints[5].transform.localEulerAngles.z);
    }
}
