using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundNote : MonoBehaviour
{
    public bool NoteActive = false;
    public AudioSource NotePlay;
    // Start is called before the first frame update
    void Start()
    {
        NotePlay = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (NotePlay==true)
        {
            NotePlay.Play();
        }
    }
}
