using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddPointListener : MonoBehaviour
{
    //public GameObject scoreText;
    public Score scoreScript;
    
    // Start is called before the first frame update
    void Start()
    {
        //Score scoreScript = scoreText.GetComponent<Score>();
        EventSystem.Current.RegisterListener<AddPointEvent>(AddPointEventInfo);
        
    }

    // Update is called once per frame
    void AddPointEventInfo(AddPointEvent addPoint)
    {
        Debug.Log("1");
        Debug.Log(scoreScript);
        scoreScript.AddScore(addPoint.point);
        
    }
}
