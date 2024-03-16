using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering;

public class UIActionComponent : MonoBehaviour
{
    private string actionName;
    public string ActionName { get { return actionName; } set { actionName = value; actionTitle.text = value; } }

    [SerializeField]
    private TMP_Text actionTitle;
    [SerializeField]
    private GameObject actionKeyPrefab;

    private List<KeyCode> keys;
    private List<GameObject> uiKeys;
    // Start is called before the first frame update
    void Start()
    {
        keys = new List<KeyCode>();
        if(uiKeys==null)
        uiKeys = new List<GameObject>();
    }

    public void AddKeyCode(KeyCode key)
    {
        keys.Add(key);

        for(int i = 0; i < keys.Count; i++)
        {
            if(uiKeys.Count <= i)
            {
                GameObject newButton = Instantiate(actionKeyPrefab);
                newButton.transform.position += Vector3.right * 150f;
                newButton.GetComponentInChildren<TMP_Text>().text = keys[i].ToString();
                uiKeys.Add(newButton);
            }
            else
            {
                uiKeys[i].GetComponentInChildren<TMP_Text>().text = keys[i].ToString();
            }
        }
    }

    private string CleanName(string raw)
    {
        return raw.Replace("Alpha ", "");
    }

    public void ModifyKeyCodes(List<KeyCode> keys)
    {
        if (uiKeys == null)
        {
            uiKeys = new List<GameObject>();
        }
        this.keys = keys;
        for (int i = 0; i < keys.Count; i++)
        {
            if (uiKeys.Count <= i)
            {
                GameObject newButton = Instantiate(actionKeyPrefab,transform);
                newButton.transform.position += Vector3.right * 150f*uiKeys.Count;
                newButton.GetComponentInChildren<TMP_Text>().text = CleanName(keys[i].ToString());
                uiKeys.Add(newButton);
            }
            else
            {
                uiKeys[i].GetComponentInChildren<TMP_Text>().text = CleanName(keys[i].ToString());
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
