using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using URControl;
using URDate;
using System.Xml;

public class URMissionControl : MonoBehaviour//左
{
    //连接参数
    public string IP;
    public Image status;//连接状态图片
    URDateHandle URDateCollector = new URDateHandle();
    URControlHandle URController = new URControlHandle();
    int DataRefreshRate = 50;
    double BasicSpeed = 0.15;
    double BasicAcceleration = 0.15;
    private int Control_Port = 30003;
    private double SpeedRate;
    private double AccelerationRate;

    string[] temp_Pos = new string[12];
    float[] current_Pos = new float[12];
    double [] Axis_Pos = new double[12];
    public Text[] txPos = new Text[6];

    //离线任务参数
    XMLRead xmlRead;
    List<Mission> mission_List = new List<Mission>();
    int index = -1;
    public Text txLog;
    //public Text txIndex;
    public Button btnNext;
    public Text txCutTips;
    Mission BoMission;
    string fileName;

    //实时显示
    public GameObject[] URJoints = new GameObject[6];
    //public GameObject[] tools = new GameObject[2];
    //public GameObject[] toolsJia = new GameObject[2];
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (URController.isConnect)
        {
            status.color = Color.green;
            URJoints[0].transform.localEulerAngles = new Vector3(URJoints[0].transform.localEulerAngles.x, -current_Pos[0], URJoints[0].transform.localEulerAngles.z);
            URJoints[1].transform.localEulerAngles = new Vector3(URJoints[1].transform.localEulerAngles.x, URJoints[1].transform.localEulerAngles.y, current_Pos[1] + 90);
            URJoints[2].transform.localEulerAngles = new Vector3(URJoints[2].transform.localEulerAngles.x, URJoints[2].transform.localEulerAngles.y, current_Pos[2]);
            URJoints[3].transform.localEulerAngles = new Vector3(URJoints[3].transform.localEulerAngles.x, URJoints[3].transform.localEulerAngles.y, current_Pos[3] + 90);
            URJoints[4].transform.localEulerAngles = new Vector3(URJoints[4].transform.localEulerAngles.x, -current_Pos[4], URJoints[4].transform.localEulerAngles.z);
            URJoints[5].transform.localEulerAngles = new Vector3(URJoints[5].transform.localEulerAngles.x, URJoints[5].transform.localEulerAngles.y, current_Pos[5]);
            for (int i = 0; i < 6; i++)
            {
                txPos[i].text = temp_Pos[i];
            }
            //print("连接成功");

            //status.color = Color.green;
            //URJoints[0].transform.localEulerAngles = new Vector3(URJoints[0].transform.localEulerAngles.x, -current_Pos[0], URJoints[0].transform.localEulerAngles.z);
            //URJoints[1].transform.localEulerAngles = new Vector3(URJoints[1].transform.localEulerAngles.x, -(current_Pos[1] + 90), URJoints[1].transform.localEulerAngles.z);
            //URJoints[2].transform.localEulerAngles = new Vector3(URJoints[2].transform.localEulerAngles.x, -current_Pos[2], URJoints[2].transform.localEulerAngles.z);
            //URJoints[3].transform.localEulerAngles = new Vector3(URJoints[3].transform.localEulerAngles.x, -current_Pos[3] + 90, URJoints[3].transform.localEulerAngles.z);
            //URJoints[4].transform.localEulerAngles = new Vector3(URJoints[4].transform.localEulerAngles.x, -current_Pos[4] - 180, URJoints[4].transform.localEulerAngles.z);
            //URJoints[5].transform.localEulerAngles = new Vector3(URJoints[5].transform.localEulerAngles.x, current_Pos[5], URJoints[5].transform.localEulerAngles.z);
        }
        else
            status.color = Color.red;        
    }

    //连接按钮
    public void Connect()
    {
        URDateHandle.ScanRate = DataRefreshRate;
        URDateCollector.InitialMoniter(IP);
        //初始化URControlHandle，生成一个clientSocket，方便从30001-30003端口直接发送指令
        URController.Creat_client(IP, Control_Port);
        //初始化速度和加速度(基准速度0.15 最高变成2倍即0.2，最低变成0.1倍即0.01)
        SpeedRate = BasicSpeed;
        AccelerationRate = BasicAcceleration;
        URDateCollector.OnGetPositionSuccess += new URDateHandle.GetPositionSuccess(UpdatePositionsValue);//获取TCP位置
        URDateCollector.OnGetAngleSuccess += new URDateHandle.GetAngleSuccess(UpdateAnglesValue);//获取关节角度
    }
    void UpdatePositionsValue(float[] Positions)
    {
        for (int i = 6; i < 12; i++)
        {
            current_Pos[i] = Positions[i - 6];
            temp_Pos[i] = current_Pos[i].ToString("0.0");
        }
    }
    void UpdateAnglesValue(double[] Angles)
    {
        for (int i = 0; i < Angles.Length; i++)
        {
            if (Angles[i] > 180)
                Angles[i] -= 360;
        }
        for (int i = 0; i < 6; i++)
        {
            current_Pos[i] = (float)Angles[i];
            Axis_Pos[i] = Angles[i];
            temp_Pos[i] = current_Pos[i].ToString("0.00");
        }
        
    }
    //任务选择按钮
    public void MissionChoose(int xml_index)//7.3任务变更
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
            case 4:
                fileName = "jianxian.xml";
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
        btnNext.interactable = true;
    }

    //下一步任务按钮
    public void NextMission()
    {
        index++;
        if (index < mission_List.Count)
        {
            //显示任务细节文本
            //txIndex.text = (index + 1).ToString();
            txLog.text = mission_List[index].Log;
            //print(CommandScripts.MissionDo(mission_List[index], AccelerationRate, SpeedRate));
            if (mission_List[index].IOindex == -1)//运动命令
                URController.Send_command(CommandScripts.MissionDo(mission_List[index], AccelerationRate, SpeedRate));
            else if (mission_List[index].IOindex == -2)//直线运动命令
                URController.Send_command(CommandScripts.MissionDodirect(mission_List[index], AccelerationRate, SpeedRate));
            else if (mission_List[index].IOindex == 100)//剥线任务
            {
                BoMission = mission_List[index];
                StartCoroutine(GetReady());
            }
            else if (mission_List[index].IOindex == 2)//拧螺栓任务
            {
                StartCoroutine(Ning());
            }
            else//其余都是普通IO开/关
            {
                StartCoroutine(IOProcess(mission_List[index].IOindex));
                //if (fileName == "ningluoshuan.xml")//抓取工具
                //    ShowTool(mission_List[index].IOindex, 1);
                //else
                //    ShowTool(mission_List[index].IOindex, 0);
            }
        }
        else
        {
            txLog.text = "任务执行完毕";
            btnNext.interactable = false;
        }
    }

    //紧停
    public void JinTing()
    {
        URController.Send_command(CommandScripts.MoveStop());
    }
    //IO开/关
    IEnumerator IOProcess(int IO_index)
    {
        URController.Send_command(CommandScripts.IO(IO_index, true));
        yield return new WaitForSeconds(1);
        URController.Send_command(CommandScripts.IO(IO_index, false));
    }
    #region 剥线流程
    IEnumerator GetReady()
    {
        for (int i = 3; i > 0; i--)
        {
            txCutTips.text = "检测到剥线任务，请准备\n";
            txCutTips.text += i.ToString() + "s";
            TipsColorChange(i);
            yield return new WaitForSeconds(1);
        }
        StartCoroutine(ClampProcess());
    }
    IEnumerator ClampProcess()
    {
        for (int i = 12; i > 0; i--)
        {
            txCutTips.text = "夹线中\n";
            txCutTips.text += i.ToString() + "s";
            TipsColorChange(i);
            yield return new WaitForSeconds(1);
        }
        StartCoroutine(ReadyProcess());
    }
    IEnumerator ReleaseProcess()
    {
        for (int i = 20; i > 0; i--)
        {
            txCutTips.text = "退线中\n";
            txCutTips.text += i.ToString() + "s";
            TipsColorChange(i);
            yield return new WaitForSeconds(1);
        }
        txCutTips.text = "剥线完成";
        TipsColorChange(100);
    }
    IEnumerator ReadyProcess()
    {
        for (int i = 3; i > 0; i--)
        {
            txCutTips.text = "准备按下剥线\n";
            txCutTips.text += i.ToString() + "s";
            TipsColorChange(i);
            yield return new WaitForSeconds(1);
        }
        StartCoroutine(FollowProcess());
    }
    IEnumerator FollowProcess()
    {
        URController.Send_command(CommandScripts.MoveTCP("X", -1, AccelerationRate, 0.001056));
        for (int i = 100; i > 0; i--)
        {
            txCutTips.text = "剥线中\n";
            txCutTips.text += i.ToString() + "s";
            TipsColorChange(i);
            yield return new WaitForSeconds(1);
        }
        //for (int i = 6; i > 0; i--)
        //{
        //    URController.Send_command(CommandScripts.MissionDo(mission_List[index], AccelerationRate, 0.0025));
        //    for (int j = 0; j < 17; j++)
        //    {
        //        txCutTips.text = "剥线中\n";
        //        txCutTips.text += (i*17-j).ToString() + "s";
        //        TipsColorChange(i * 17 - j);
        //        yield return new WaitForSeconds(1);
        //    }
        //    index++;
        //}
        URController.Send_command(CommandScripts.MoveStop());
        index++;
        StartCoroutine(ReleaseProcess());
    }
    void TipsColorChange(int i)
    {
        if (i <= 5)
        {
            txCutTips.color = Color.red;
        }
        else
            txCutTips.color = Color.black;
    }
#endregion
    //拧流程
    IEnumerator Ning()
    {
        URController.Send_command(CommandScripts.IO(2, true));
        yield return new WaitForSeconds(1);
        URController.Send_command(CommandScripts.IO(2, false));
        yield return new WaitForSeconds(3);
        URController.Send_command(CommandScripts.IO(3, true));
        yield return new WaitForSeconds(1);
        URController.Send_command(CommandScripts.IO(3, false));
    }
    //void ShowTool(int io,int toolIndex)
    //{
    //    if(io==0)
    //    {
    //        tools[toolIndex].SetActive(true);
    //        toolsJia[toolIndex].SetActive(false);
    //    }
    //    else if(io == 1)
    //    {
    //        tools[toolIndex].SetActive(false);
    //        toolsJia[toolIndex].SetActive(true);
    //    }
    //    else
    //    {
    //        print("Nothing");
    //    }
    //}
    //+按钮
    public void SingleAxisJogZheng(int index)
    {
        URController.Send_Command(CommandScripts.MoveAxis(index, 1, AccelerationRate, SpeedRate, Axis_Pos));
    }
    //-按钮
    public void SingleAxisJogFu(int index)
    {
        URController.Send_Command(CommandScripts.MoveAxis(index, -1, AccelerationRate, SpeedRate, Axis_Pos));
    }
    public void EndTcpMoveZheng(string Axis)
    {
        URController.Send_Command(CommandScripts.MoveTCP(Axis, 1, AccelerationRate, SpeedRate));
    }
    public void EndTcpMoveFu(string Axis)
    {
        URController.Send_Command(CommandScripts.MoveTCP(Axis, -1, AccelerationRate, SpeedRate));
    }
}
