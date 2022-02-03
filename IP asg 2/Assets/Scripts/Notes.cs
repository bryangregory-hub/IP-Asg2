using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notes : MonoBehaviour
{
    [SerializeField] private Vector3 gridSize = default;
    public void Update()
    {
        SnapToGrid();
    }
    
    private void SnapToGrid()
    {
        var position = new Vector3(
            Mathf.Round(this.transform.position.x / this.gridSize.x)* this.gridSize.x,
            Mathf.Round(this.transform.position.y / this.gridSize.y)* this.gridSize.y,
            Mathf.Round(this.transform.position.z / this.gridSize.z)* this.gridSize.z);
        this.transform.position = position;
    }
}
