using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject objPrefab;
    public int createOnStart;

    private List<GameObject> poolObjs = new List<GameObject>();

    private void Start()
    {
        for(int x=0;x<createOnStart;x++)
        {
            CreateNewObject();
        }

    }

    GameObject CreateNewObject()
    {
        GameObject obj = Instantiate(objPrefab);
        obj.SetActive(false);
        poolObjs.Add(obj);

        return obj;
    }

    public GameObject GetObject()
    {
        GameObject obj =  poolObjs.Find(x => x.activeInHierarchy == false);
        if(obj==null)
        {
            obj = CreateNewObject();
        }

        obj.SetActive(true);

        return obj;
    }

}
