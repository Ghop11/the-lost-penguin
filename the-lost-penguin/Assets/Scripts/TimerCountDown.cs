using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerCountDown : MonoBehaviour
{

    public static TimerCountDown instance;
    
    public void Awake()
    {
        instance = this;
    }
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void RespawnNow(Transform t, GameObject g)
    {
        var pickUps = Instantiate(g, t.position, t.rotation);
    }

    private IEnumerator TimerStarts(Transform t, GameObject g)
    {
        print("waiting");
        yield return new WaitForSeconds(5);
        RespawnNow(t, g);
    }

    public void RespawnMoreThrowable(Transform t, GameObject g)
    {
        Transform T = t;
        print('s');
        StartCoroutine(TimerStarts(T, g));
    }



}
