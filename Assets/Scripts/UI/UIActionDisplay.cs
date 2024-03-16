using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIActionDisplay : MonoBehaviour
{
    [SerializeField]
    private GameObject actionUIPrefab;
    private Dictionary<string, GameObject> actionObjects;

    private static UIActionDisplay instance;
    
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        actionObjects = new Dictionary<string, GameObject>();
    }
    public static void UpdateAction(string actionName, List<KeyCode> codes)
    {
        instance._UpdateAction(actionName, codes);
    }
    public void _UpdateAction(string actionName,List<KeyCode> codes)
    {
        GameObject obj;
        if (actionObjects.ContainsKey(actionName))
        {
             obj= actionObjects[actionName];
        }
        else
        {
            obj = Instantiate(actionUIPrefab,this.transform);
            obj.GetComponent<UIActionComponent>().ActionName= actionName;
            obj.transform.position += Vector3.down * actionObjects.Count * 300f;
            actionObjects.Add(actionName, obj);
        }
        obj.GetComponent<UIActionComponent>().ModifyKeyCodes(codes);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
