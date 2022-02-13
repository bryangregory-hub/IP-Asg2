using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

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
     void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="MusicBar")
        {
            print("wadad");
        }
        
        if (NoteActive == true && other.gameObject.tag == "MusicBar")
        {
            print("wadad");
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
