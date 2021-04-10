using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour
{
    public GameObject wallPrefab;

    public float wallWidth = 20;

    public float wallHeight = 10;
    public float doorWidth = 3;
    public float wallGap = 0.01f;

    private int horizonCount;

    private int verticalCount;
    private int doorHorizonCount;

    private float prefabWidth;
    private float prefabHeight;

    private GameObject[,] wallMatrix;
    private Vector3[,] wallMatrixPosition;
    private Vector3[,] wallMatrixVelocity;
    private float lerpTime = 0f;
    public float lerpTimeTotal = 2f;


    // Start is called before the first frame update
    void Start()
    {
        prefabWidth = wallPrefab.transform.localScale.x;
        prefabHeight = wallPrefab.transform.localScale.y;
        horizonCount = (int) Math.Round(wallWidth / prefabWidth);
        verticalCount = (int) Math.Round(wallHeight / prefabHeight);
        doorHorizonCount = (int) Math.Round(doorWidth / prefabWidth);
        wallMatrix = new GameObject[verticalCount, horizonCount];
        wallMatrixPosition = new Vector3[verticalCount, horizonCount];
        for (int i = 0; i < horizonCount; i++)
        {
            for (int j = 0; j < verticalCount; j++)
            {
                var position = new Vector3((prefabWidth + wallGap) * i - wallWidth / 2,
                    (prefabHeight + wallGap) * j + prefabHeight / 2,
                    0);
                var wall = Instantiate(wallPrefab, position, Quaternion.identity);
                wallMatrix[j, i] = wall;
                wallMatrixPosition[j, i] = wall.transform.position;
                // wallMatrixVelocity[j, i] = new Vector3(0, 0, 0);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (lerpTime < lerpTimeTotal)
        {
            openFromCenter(lerpTime / lerpTimeTotal);
            lerpTime += Time.deltaTime;
        }
    }

    void openFromCenter(float progress)
    {
        var curSpeed = new Vector3(0, 0, 0);
        for (int i = 0; i < doorHorizonCount; i++)
        {
            var x = horizonCount / 2 - doorHorizonCount / 2 + i;
            for (int j = 0; j < verticalCount; j++)
            {
                var wallPiece = wallMatrix[j, x];
                // wallPiece.SetActive(false);
                var oldPosition = wallMatrixPosition[j, x];
                var newPosition = new Vector3(oldPosition.x, oldPosition.y, oldPosition.z + 3f);
                // var currentVelocity = wallMatrixVelocity[j, x];
                wallPiece.transform.position = Vector3.Lerp(oldPosition, newPosition, interceptor(progress));
                // wallPiece.transform.position =
                // Vector3.SmoothDamp(oldPosition, newPosition, ref currentVelocity, 3);
                // transform.position = newPosition;
            }
        }
    }

    /**
     * 
     */
    private float interceptor(float progress)
    {
        return Mathf.Sin((progress - 0.5f) * Mathf.PI) / 2 + 0.5f;
    }
}