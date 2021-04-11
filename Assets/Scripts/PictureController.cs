using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PictureController : MonoBehaviour
{
    public GameObject picturePrefab;
    private float pictureGap = .2f;
    private List<GameObject> pictureList;
    private int textureLoadProgress = 0;
    private int column = 6;
    private int row = 3;

    private int planCount = 1;
    private System.Random random = new System.Random();
    private int textureStartPosition = 0;
    private float time = 0f;
    private float changePicturePeriod = 30f;
    private Vector3 scale;

    void Start()
    {
        textureStartPosition = random.Next(0, urls.Length);
        pictureList = new List<GameObject>();
        for (int p = 0; p < planCount; p++)
        {
            for (int r = 0; r < row; r++)
            {
                for (int c = 0; c < column; c++)
                {
                    var x = c * (picturePrefab.transform.localScale.x + pictureGap) + gameObject.transform.position.x;
                    var y = r * (picturePrefab.transform.localScale.y + pictureGap) + 1.0f + gameObject.transform.position.y;
                    var z = p * 2 + gameObject.transform.position.z;
                    var picture = Instantiate(picturePrefab, new Vector3(x, y, z), gameObject.transform.rotation);
                    pictureList.Add(picture);
                    picture.transform.parent = gameObject.transform;
                }

            }
        }
        pictureList.Reverse();

        startLoadTexture();
    }

    void Update()
    {
        //gameObject.transform.Rotate(Vector3.up * Time.deltaTime * rotateSpeed, Space.Self);
        time += Time.deltaTime;
    }

    private void startLoadTexture()
    {
        StartCoroutine(GetTexture(pictureList[textureLoadProgress], textureLoadProgress));
    }

    private static GameObject getPictureQuad(GameObject pictureContainer)
    {
        return pictureContainer.transform.GetChild(1).gameObject;
    }


    IEnumerator GetTexture(GameObject target, int index)
    {
        var textureUrl = urls[(index + textureStartPosition) % urls.Length];

        UnityWebRequest www = UnityWebRequestTexture.GetTexture(textureUrl);
        yield return www.SendWebRequest();

        Texture2D myTexture = DownloadHandlerTexture.GetContent(www);
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
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20200703_205718.jpg?e=1618129943&token=rw7j6jbUgnMMJP63lL1jFd2hVkOACNdfdV39WBjD:u2TS0SFIKJmZ7oWQVk0mxxBEKa0=",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20200703_205731.jpg?e=1618129943&token=rw7j6jbUgnMMJP63lL1jFd2hVkOACNdfdV39WBjD:1kPrwcyhqPqG9SG2Dizlfa-iD6M=",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20200703_211401.jpg?e=1618129943&token=rw7j6jbUgnMMJP63lL1jFd2hVkOACNdfdV39WBjD:lnABvMujv9f3UNisxgBg96YqxpE=",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20200703_212522.jpg?e=1618129943&token=rw7j6jbUgnMMJP63lL1jFd2hVkOACNdfdV39WBjD:huQXUF2WTRRrGKf87msc63KjSY4=",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20200703_212603.jpg?e=1618129943&token=rw7j6jbUgnMMJP63lL1jFd2hVkOACNdfdV39WBjD:eXLXA4q7o4X7MCW8Tzh568iPFeQ=",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20200703_213159.jpg?e=1618129943&token=rw7j6jbUgnMMJP63lL1jFd2hVkOACNdfdV39WBjD:f9_bvjO0avGl1JzePes1I9NQBKk=",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20200703_213359.jpg?e=1618129943&token=rw7j6jbUgnMMJP63lL1jFd2hVkOACNdfdV39WBjD:dz6eFtlmW-hBjYbjlAjsh5Z21gU=",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20200703_213612.jpg?e=1618129943&token=rw7j6jbUgnMMJP63lL1jFd2hVkOACNdfdV39WBjD:ttdnWajP4Jwx0GFZ-Fze79msFIs=",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20200703_213925.jpg?e=1618129943&token=rw7j6jbUgnMMJP63lL1jFd2hVkOACNdfdV39WBjD:AJkl3x0_xE26pMWJSoY7jX4wluM=",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20200703_213933.jpg?e=1618129943&token=rw7j6jbUgnMMJP63lL1jFd2hVkOACNdfdV39WBjD:tlYyqc-8kj3hNn0rW0cphmS_7JM=",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20200703_214702.jpg?e=1618129943&token=rw7j6jbUgnMMJP63lL1jFd2hVkOACNdfdV39WBjD:snRAVFjVCP-lh9kF14x9jIOeJzI=",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20200703_215041.jpg?e=1618129943&token=rw7j6jbUgnMMJP63lL1jFd2hVkOACNdfdV39WBjD:J_ZsiorehkrnZWufFw3AHmuxePA=",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20200703_220214.jpg?e=1618129943&token=rw7j6jbUgnMMJP63lL1jFd2hVkOACNdfdV39WBjD:nLpoSsT9tPT0i54-bmNAhhdDmrY=",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20200703_220231.jpg?e=1618129943&token=rw7j6jbUgnMMJP63lL1jFd2hVkOACNdfdV39WBjD:4CmoOmGTuBAYbs9ZTCE1KUk-n74=",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20200703_220234.jpg?e=1618129943&token=rw7j6jbUgnMMJP63lL1jFd2hVkOACNdfdV39WBjD:OSl77YUIuJ1ddQRk8pBvWYxWJXI=",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20200703_220319.jpg?e=1618129943&token=rw7j6jbUgnMMJP63lL1jFd2hVkOACNdfdV39WBjD:rK9uot0qxXJ--pwrjkHOKLzjiY8=",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20200703_220639.jpg?e=1618129943&token=rw7j6jbUgnMMJP63lL1jFd2hVkOACNdfdV39WBjD:PcIX_PijXMENbC8KKcCQ9qN34yM=",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20200703_220641.jpg?e=1618129943&token=rw7j6jbUgnMMJP63lL1jFd2hVkOACNdfdV39WBjD:xY98YRij-K461aJUcrBeCoZKsSg=",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20200703_220642.jpg?e=1618129943&token=rw7j6jbUgnMMJP63lL1jFd2hVkOACNdfdV39WBjD:-fGq23CgwpADsF6q5gDhAVq9mI4=",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20200703_220652.jpg?e=1618129943&token=rw7j6jbUgnMMJP63lL1jFd2hVkOACNdfdV39WBjD:9b6F8VdZMRgI7s5qdFnsd_C7FiI=",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20201004_124159.jpg?e=1618129943&token=rw7j6jbUgnMMJP63lL1jFd2hVkOACNdfdV39WBjD:ryVoOIVlAAjBghlW-1E0ubm9pg0=",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20201004_125219.jpg?e=1618129943&token=rw7j6jbUgnMMJP63lL1jFd2hVkOACNdfdV39WBjD:dnvZTUYuPue4vO7KZQe7qjGyPLI=",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20201004_132402_1.jpg?e=1618129943&token=rw7j6jbUgnMMJP63lL1jFd2hVkOACNdfdV39WBjD:_JpVeqMXyjeAvxp2OedImQiCkN",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20201004_133442.jpg?e=1618129943&token=rw7j6jbUgnMMJP63lL1jFd2hVkOACNdfdV39WBjD:6pBEQC-kjAXb1OnNHmib4lqT4Qw=",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20201013_092136.jpg?e=1618129943&token=rw7j6jbUgnMMJP63lL1jFd2hVkOACNdfdV39WBjD:N0Z273EcYZn4C_3mzifkLr9ZmfI=",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20201013_160726.jpg?e=1618129943&token=rw7j6jbUgnMMJP63lL1jFd2hVkOACNdfdV39WBjD:WdTxhqdN5qWU2Tq5CLTuJ3YgMLI=",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20201013_161034.jpg?e=1618129943&token=rw7j6jbUgnMMJP63lL1jFd2hVkOACNdfdV39WBjD:Y8eiAJptuNfLnVlxYl0Z-Gf5WNE=",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20201013_161735.jpg?e=1618129943&token=rw7j6jbUgnMMJP63lL1jFd2hVkOACNdfdV39WBjD:tSxJwCoUi0vTxmuVDFqGG7O7zVk=",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20201013_163809.jpg?e=1618129943&token=rw7j6jbUgnMMJP63lL1jFd2hVkOACNdfdV39WBjD:sd1Hbm3DoJlaPw7gyFyDka2R3Cg=",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20201013_164327.jpg?e=1618129943&token=rw7j6jbUgnMMJP63lL1jFd2hVkOACNdfdV39WBjD:AQIm3eg_ltpt--crHEExInnFpNE=",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20201016_233207.jpg?e=1618129943&token=rw7j6jbUgnMMJP63lL1jFd2hVkOACNdfdV39WBjD:zCfqevZTkSID-3JDdJmcefXQQoc=",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20201017_013253.jpg?e=1618129943&token=rw7j6jbUgnMMJP63lL1jFd2hVkOACNdfdV39WBjD:BBBc2EYvmw3mmxsvs5LtdqqBnrk=",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20201021_082033.jpg?e=1618129943&token=rw7j6jbUgnMMJP63lL1jFd2hVkOACNdfdV39WBjD:Cd_pe-_J8SABLSdq19IXZqpEE64=",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20201025_134854.jpg?e=1618129943&token=rw7j6jbUgnMMJP63lL1jFd2hVkOACNdfdV39WBjD:MzDDqlVKs8BRjV44Kqp_hu22f20=",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20201025_150233.jpg?e=1618129943&token=rw7j6jbUgnMMJP63lL1jFd2hVkOACNdfdV39WBjD:prwNbUFVGR0Ftunm7ACQibV3_NA=",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20201025_153729.jpg?e=1618129943&token=rw7j6jbUgnMMJP63lL1jFd2hVkOACNdfdV39WBjD:pWh_0rp-7UFCscwcK3PRgKqepBk=",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20201027_082211.jpg?e=1618129943&token=rw7j6jbUgnMMJP63lL1jFd2hVkOACNdfdV39WBjD:biNH19_LQIN_gB46LR2SRyEYjns=",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20201027_090908.jpg?e=1618129943&token=rw7j6jbUgnMMJP63lL1jFd2hVkOACNdfdV39WBjD:p8jqnFkcis9DnMU-JtkZMqAxejw=",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20201031_115906.jpg?e=1618129943&token=rw7j6jbUgnMMJP63lL1jFd2hVkOACNdfdV39WBjD:vpx0H9-tDfgeIwAREFDDE0lPSzs=",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20201031_121438.jpg?e=1618129943&token=rw7j6jbUgnMMJP63lL1jFd2hVkOACNdfdV39WBjD:bOpKeo5DdwX5T-yHd9Nqx5OYDdU=",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20201031_122534.jpg?e=1618129943&token=rw7j6jbUgnMMJP63lL1jFd2hVkOACNdfdV39WBjD:YZJV8GMQIM7HZFV5hBW8QGo95bQ=",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20201031_154733.jpg?e=1618129943&token=rw7j6jbUgnMMJP63lL1jFd2hVkOACNdfdV39WBjD:oZCkcPCy4saFBRxxBsNj47__RwI=",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20201031_154734.jpg?e=1618129943&token=rw7j6jbUgnMMJP63lL1jFd2hVkOACNdfdV39WBjD:vYnTBTcVm_0I9BMGjlARG5IMHDc=",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20201031_154743.jpg?e=1618129943&token=rw7j6jbUgnMMJP63lL1jFd2hVkOACNdfdV39WBjD:ONApsEXPa9iodrvUP7NSZ1eeRcw=",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20201031_154815.jpg?e=1618129943&token=rw7j6jbUgnMMJP63lL1jFd2hVkOACNdfdV39WBjD:J1zKw7QGPAdEWrRpvtEM78V6lWE=",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20201101_122200.jpg?e=1618129943&token=rw7j6jbUgnMMJP63lL1jFd2hVkOACNdfdV39WBjD:WO_eHSk1YdbbMJqHggUBae73H98=",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20201101_122202.jpg?e=1618129943&token=rw7j6jbUgnMMJP63lL1jFd2hVkOACNdfdV39WBjD:ME5xmTnNeQ92JoxC_F7qI2mdZzc=",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20201105_155741.jpg?e=1618129943&token=rw7j6jbUgnMMJP63lL1jFd2hVkOACNdfdV39WBjD:W_JbaR7I5OVGuwsBONdCjjafwiw=",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20201108_113947.jpg?e=1618129943&token=rw7j6jbUgnMMJP63lL1jFd2hVkOACNdfdV39WBjD:H1ukSpdeV9O0MyMCZvJFHSPQEa4=",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20201109_151912.jpg?e=1618129943&token=rw7j6jbUgnMMJP63lL1jFd2hVkOACNdfdV39WBjD:u72yjx-2TCFkQA5keygV4ubFjVs=",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20201109_155741.jpg?e=1618129943&token=rw7j6jbUgnMMJP63lL1jFd2hVkOACNdfdV39WBjD:ZsfOXhFny4CpVKRmOxqKpPE_ZVw=",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20201128_121601.jpg",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20201128_122438.jpg",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20201128_122507.jpg",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20201128_125725.jpg",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20201128_165407.jpg",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20201128_173754.jpg",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20201128_173846.jpg",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20201128_174007.jpg",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20201128_174038.jpg",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20201128_174232.jpg",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20201129_103554.jpg",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20201212_172926.jpg",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20201212_180806.jpg",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20201212_181631.jpg",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20201212_181634.jpg",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20201212_211015.jpg",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20201220_100824.jpg",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20201220_101727.jpg",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20201227_143142.jpg",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20210103_145153.jpg",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20210113_130858.jpg",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20210114_123408.jpg",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20210114_123410.jpg",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20210114_123640.jpg",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20210117_105031.jpg",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20210117_105316.jpg",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20210117_111357.jpg",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20210117_111538.jpg",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20210117_141443.jpg",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20210130_113157.jpg",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20210130_114622.jpg",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20210130_120653.jpg",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20210130_123342.jpg",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20210130_124008.jpg",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20210130_130907.jpg",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20210203_074219.jpg",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20210210_130250.jpg",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20210220_072815.jpg",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20210313_094534.jpg",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20210313_191055.jpg",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20210313_195826.jpg",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20210313_204125.jpg",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20210315_075822.jpg",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20210315_080408.jpg",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20210321_154533.jpg",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20210321_160723.jpg",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20210331_125816.jpg",
        "http://qre14mzdf.hn-bkt.clouddn.com/IMG_20210406_175744.jpg",
    };

}