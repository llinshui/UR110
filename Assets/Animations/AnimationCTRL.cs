using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCTRL : MonoBehaviour
{
    Animation ani;
    // Start is called before the first frame update
    void Start()
    {
        ani = this.GetComponent<Animation>();
        if (ani == null)
            print("这个组件没有动画");
        else
        {
            foreach (AnimationState state in ani)
            {
                state.speed /= 60;
            }
            ani.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
