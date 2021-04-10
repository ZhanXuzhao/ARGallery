using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class ArController : MonoBehaviour
{
    public GameObject spawnPrefab;
    private GameObject spawnedObject;
    private static List<ARRaycastHit> Hits;
    private ARRaycastManager mRaycastManager;


    // Start is called before the first frame update
    void Start()
    {
        Hits = new List<ARRaycastHit>();
        mRaycastManager = GetComponent<ARRaycastManager>();
        Debug.Log("ar controller mRaycastManager: " + mRaycastManager);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 0)
        {
            return;
        }

        Debug.Log("ar controller input count > 0");
        var touch = Input.GetTouch(0);
        if (mRaycastManager.Raycast(touch.position, Hits,
            TrackableType.PlaneWithinPolygon | TrackableType.PlaneWithinBounds))
        {
            var hitPose = Hits[0].pose;

            if (spawnedObject == null)
            {
                Debug.Log("ar controller new spawned");
                spawnedObject = Instantiate(spawnPrefab, hitPose.position, hitPose.rotation);
            }
            else
            {
                Debug.Log("ar controller update spawned");
                spawnedObject.transform.position = hitPose.position;
                spawnedObject.transform.rotation = hitPose.rotation;
            }
        }
    }
}