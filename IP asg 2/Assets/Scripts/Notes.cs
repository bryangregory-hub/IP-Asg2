using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notes : MonoBehaviour
{
    public void Update()
    {
        SnapToGrid();
    }
    private void SnapToGrid() {
        var position = new Vector3(
            Mathf.RoundToInt(this.transform.position.x),
            Mathf.RoundToInt(this.transform.position.y),
            Mathf.RoundToInt(this.transform.position.z)
            ) ;
        this.transform.position = position;
        this.transform.rotation = Quaternion.identity;
    }
}
