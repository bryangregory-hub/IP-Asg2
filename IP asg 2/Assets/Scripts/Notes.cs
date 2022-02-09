using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notes : MonoBehaviour
{
    public float musicSpeed;
    public void Update()
    {
        transform.Translate(0, musicSpeed * Time.deltaTime, 0);
    }
    
    
}
