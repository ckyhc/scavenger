using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
  private Camera mainCamera;
  private Bounds cameraBounds;
  public Transform targetToFollow;

  public float translationFactor = 20;
  private void Awake() => mainCamera = Camera.main;

    private void Start()
    {
        var height = mainCamera.orthographicSize;
        var width = height * mainCamera.aspect;

        var minX = Globals.WorldBounds.min.x + width;
        var maxX = Globals.WorldBounds.extents.x - width;

        var minY = Globals.WorldBounds.min.y + height;
        var maxY = Globals.WorldBounds.extents.y - height;

        cameraBounds = new Bounds();
        cameraBounds.SetMinMax(
            new Vector3(minX, minY, this.transform.position.z),
            new Vector3(maxX, maxY, this.transform.position.z)
            );
    }

  void FixedUpdate(){
    if(transform.position != targetToFollow.position) {
      float newX = transform.position.x + (targetToFollow.position.x - transform.position.x) / translationFactor; 
      float newY = transform.position.y + (targetToFollow.position.y - transform.position.y) / translationFactor;  

      this.transform.position = new Vector3(
            Mathf.Clamp(newX, cameraBounds.min.x, cameraBounds.max.x),
            Mathf.Clamp(newY, cameraBounds.min.y, cameraBounds.max.y),
            transform.position.z
        ); 
    }
  }
}
