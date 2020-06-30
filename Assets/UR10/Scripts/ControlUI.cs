using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlUI : MonoBehaviour
{
    GameObject robot, mode;
    GameObject[] controlModes = new GameObject[6];
    int robot_index = 0;
    int mode_index = -1;

    GameObject[] controlRobots = new GameObject[2];
    GameObject[] ChoseModes = new GameObject[3];
    GameObject activeMode,activeRobot,activeAA;
    // Start is called before the first frame update
    void Start()
    {
        robot = this.transform.Find("机械臂选择").gameObject;
        mode = GameObject.Find("模式选择");
        controlModes[0] = this.transform.Find("离线任务-主").gameObject;
        controlModes[1] = this.transform.Find("离线任务-从").gameObject;
        controlModes[2] = this.transform.Find("单轴点动-主").gameObject;
        controlModes[3] = this.transform.Find("单轴点动-从").gameObject;
        controlModes[4] = this.transform.Find("末端联动-主").gameObject;
        controlModes[5] = this.transform.Find("末端联动-从").gameObject;
        activeMode = null;
        activeRobot = null;
        activeAA = null;
        Init();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void RobotChoose(int i)
    {
        robot_index = i;
        if (activeRobot == null)
            activeRobot = controlRobots[robot_index];
        else
        {
            activeRobot.GetComponent<Image>().color = Color.white;
        }
        activeRobot = controlRobots[i];
        activeRobot.GetComponent<Image>().color = Color.red;
        if(activeAA!=null)
        {
            activeAA.GetComponent<Image>().color = Color.white;
        }
        if(activeAA!=null&&activeMode.activeSelf)
        {
            activeMode.SetActive(false);
        }
    }
    public void ModeChoose(int i)
    {
        mode_index = i;
        if(activeMode==null)
        {
            activeMode = controlModes[robot_index + mode_index * 2];
        }
        else
        {
            activeMode.SetActive(false);
        }
        if(activeAA==null)
        {
            activeAA = ChoseModes[mode_index];
        }
        else
        {
            activeAA.GetComponent<Image>().color = Color.white;
        }
        activeAA = ChoseModes[mode_index];
        activeAA.GetComponent<Image>().color = Color.red;
        activeMode = controlModes[robot_index + mode_index * 2];
        activeMode.SetActive(true);
    }
    void Init()
    {
        controlRobots[0] = robot.transform.Find("Primary").gameObject;
        controlRobots[1] = robot.transform.Find("Secondary").gameObject;
        ChoseModes[0] = mode.transform.Find("离线任务").gameObject;
        ChoseModes[1] = mode.transform.Find("单轴点动").gameObject;
        ChoseModes[2] = mode.transform.Find("末端联动").gameObject;
    }
}
