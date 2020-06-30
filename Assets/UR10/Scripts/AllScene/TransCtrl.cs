using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TransCtrl : MonoBehaviour
{
    public float Rotatespeed = 1.0f;
    public float Movespeed = 1.0f;
    int direction;
    public Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        direction = -1;
    }

    // Update is called once per frame
    void Update()
    {
        if(direction==0)
        {
            this.transform.RotateAround(new Vector3(0, 120-175*Mathf.Tan(15*Mathf.Deg2Rad), 0), Vector3.up, Time.deltaTime * Rotatespeed);
        }
        else if(direction==1)
        {
            this.transform.RotateAround(new Vector3(0, 120 - 175 * Mathf.Tan(15 * Mathf.Deg2Rad), 0), Vector3.up, -Time.deltaTime * Rotatespeed);
        }
        else if(direction==2)
        {
            this.transform.Translate(Vector3.up* Time.deltaTime * Movespeed);
        }
        else if (direction == 3)
        {
            this.transform.Translate(-Vector3.up * Time.deltaTime * Movespeed);
        }
        this.GetComponent<Camera>().fieldOfView = slider.value * 100;
    }
    public void BackScene()
    {
        SceneManager.LoadScene(0);
    }
    public void CameraMove(int dir)
    {
        direction = dir;
    }
    public void CameraStop()
    {
        direction = -1;
    }
}
