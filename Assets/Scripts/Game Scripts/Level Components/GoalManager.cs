using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalManager : MonoBehaviour
{
    [SerializeField] private Camera goalCamera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RotateCamera();       
    }

    private void RotateCamera()
    {
        goalCamera.transform.RotateAround(this.transform.position, this.transform.up, 0.1f);
    }
    

}
