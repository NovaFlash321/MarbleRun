using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private Camera spawnCamera;
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
        spawnCamera.transform.RotateAround(this.transform.position, this.transform.up, 0.1f);
    }
}
