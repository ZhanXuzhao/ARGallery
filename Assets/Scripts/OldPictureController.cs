using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using Random = System.Random;

public class PictureController : MonoBehaviour
{
    public GameObject parentGameObject;
    public GameObject pictureContainerPrefab;
    private float picturezWidth = 1.6f;
    private float pictureHeight = 0.9f;
    private float pictureGap = .1f;
    public int oneCircleCount = 8;
    public int circleCount = 1;
    private float circleRadius = 2;
    private List<GameObject> pictureList;
    private int textureLoadProgress = 0;

    private Random random = new Random();
    private int pictureCount = 10;
    private int textureStartPosition = 0;
    public float rotateSpeed = 45f;
    private float time = 0f;
    private float changePicturePeriod = 8f;

    void Start()
    {
        textureStartPosition = random.Next(0, urls.Length);
        pictureList = new List<GameObject>();
        circleRadius = oneCircleCount * (picturezWidth + pictureGap) / (2 * Mathf.PI);
        var theta = 0f;
        pictureCount = oneCircleCount * circleCount;
        for (int i = 0; i < oneCircleCount; i++)
        {
            for (int j = 0; j < circleCount; j++)
            {
                theta = 360f / oneCircleCount * i;
                var x = circleRadius * Mathf.Sin(theta / 180 * Mathf.PI);
                var z = circleRadius * Mathf.Cos(theta / 180 * Mathf.PI);
                var y = (pictureHeight + pictureGap) * j;
                var pictureContainer = Instantiate(pictureContainerPrefab, new Vector3(x, y, z),
                    Quaternion.Euler(0, theta, 0));

                var picture = getPictureQuad(pictureContainer);
                pictureList.Add(picture);
                pictureContainer.transform.parent = parentGameObject.transform;
            }
        }

        startLoadTexture();
    }

    void Update()
    {
        parentGameObject.transform.Rotate(Vector3.up * Time.deltaTime * rotateSpeed, Space.Self);
        time += Time.deltaTime;
    }

    private void startLoadTexture()
    {
        StartCoroutine(GetTexture(pictureList[textureLoadProgress], textureLoadProgress));
    }

    private static GameObject getPictureQuad(GameObject pictureContainer)
    {
        return pictureContainer.transform.GetChild(2).gameObject;
    }


    IEnumerator GetTexture(GameObject target, int index)
    {
        var textureUrl = urls[(index + textureStartPosition) % urls.Length];

        UnityWebRequest www = UnityWebRequestTexture.GetTexture(textureUrl);
        yield return www.SendWebRequest();

        Texture myTexture = DownloadHandlerTexture.GetContent(www);
        var material = new Material(Shader.Find("Standard"));
        material.mainTexture = myTexture;
        material.mainTexture.wrapMode = TextureWrapMode.Clamp;
        target.GetComponent<Renderer>().material = material;
        textureLoadProgress++;
        if (textureLoadProgress < pictureList.Count)
        {
            startLoadTexture();
        }
        else
        {
            if (time > changePicturePeriod)
            {
                time = 0;
                textureStartPosition = random.Next(0, urls.Length);
                textureLoadProgress = 0;
                startLoadTexture();
            }
        }
    }

    private String[] urls =
    {
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20200703_205718.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20200703_205731.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20200703_211131.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20200703_211401.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20200703_211447.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20200703_211644.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20200703_212522.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20200703_212603.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20200703_213159.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20200703_213359.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20200703_214702.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20200703_215041.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20200703_220214.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20200703_220231.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20200703_220234.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20200703_220319.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20200703_220639.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20200703_220641.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20200703_220642.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20200703_220652.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20200703_220655.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20200706_075457.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20200717_203723.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20200719_161226.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20200719_193523.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20200817_213711.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20200831_211520.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20201004_081305.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20201004_081610.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20201004_093708.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20201004_093821.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20201004_095047.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20201004_095318.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20201004_095346.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20201004_105401.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20201004_105430.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20201004_105552.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20201004_124159.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20201004_125219.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20201004_132402_1.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20201004_133442.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20201013_092136.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20201013_160726.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20201013_161034.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20201013_161735.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20201013_163809.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20201013_164327.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20201016_233207.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20201017_013253.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20201021_082033.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20201025_134854.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20201025_144836.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20201025_150233.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20201025_151536.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20201025_152028.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20201025_153729.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20201025_154057.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20201027_075649.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20201027_081228.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20201027_082211.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20201027_090908.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20201028_160011.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20201031_115906.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20201031_121438.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20201031_122534.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20201031_154733.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20201031_154734.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20201031_154743.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20201031_154815.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20201101_122130.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20201101_122200.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20201101_122202.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20201105_155741.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20201105_161349.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20201108_113654.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20201108_113930.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20201108_113942.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20201108_113947.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20201108_190729.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20201109_151912.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20201109_152208.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20201109_155445.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20201109_155741.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20201128_121601.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20201128_122438.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20201128_122507.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20201128_125725.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20201128_160243.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20201128_163036.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20201128_163912.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20201128_164440.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20201128_165333.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20201128_165407.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20201128_173754.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20201128_173846.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20201128_174007.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20201128_174038.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20201128_174232.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20201129_103554.jpg",
        "http://qr8aq4d8n.hn-bkt.clouddn.com/IMG_20201208_070132.jpg",
    };
}