using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ModelChoose : MonoBehaviour
{
    public List<GameObject> Models = new List<GameObject>();
    public Dropdown dropdown;
    public Text tx;
    int index = 0;
    GameObject aliveModel;
    Vector3[] CameraPos = new Vector3[2];
    // Start is called before the first frame update
    void Start()
    {
        aliveModel=Instantiate(Models[index]);
        CameraPos[0] = new Vector3(0, 75, -180);
        CameraPos[1] = new Vector3(0, 5, -70);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ModelChoosen()
    {
        //print(dropdown.value);
        if (dropdown.value != index)
        {
            Destroy(aliveModel);
            index = dropdown.value;
            if (index == 0|| index == 6)
            {
                this.transform.position = CameraPos[0];
            }
            else
                this.transform.position = CameraPos[1];
            StartCoroutine("Wait");
        }
        else
        {
            print("没选中新模型");
        }
    }
    IEnumerator Wait()
    {
        tx.text = "载入中";
        for (int i = 0; i < 3; i++)
        {
            tx.text += "。";
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(0.5f);
        aliveModel =Instantiate(Models[index]);
        tx.text = "";
    }
    public void BackScene()
    {
        SceneManager.LoadScene(0);
    }
}
