using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
public class soundNote : MonoBehaviour
{
    
    public bool NoteActive;
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
        if (NoteActive == true && tag == "MusicBar")
        {
            NotePlay.Play();
        }
    }
    public void TrueeNoteActive()
    {
        NoteActive = true;
    }
    public void falsifyNoteActive()
    {
        NoteActive = false;
    }
}
