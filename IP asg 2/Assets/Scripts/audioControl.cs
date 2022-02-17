using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class audioControl : MonoBehaviour
{
    public AudioSource music;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        music.Play();
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
        if (other.tag=="exit")
        {
            SceneManager.LoadScene("Main scene");
        }
        if (other.tag == "quiz")
        {
            SceneManager.LoadScene("Quiz");
        }
    }
}
