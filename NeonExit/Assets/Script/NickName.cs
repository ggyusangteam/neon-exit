using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NickName : MonoBehaviour
{
    public InputField inputField;
    public GameObject startBtn;
    public GameObject inputNickName;

    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.DeleteKey("NickName");
		
        /*
        if (PlayerPrefs.HasKey("NickName"))
        {
            startBtn.SetActive(true);
            inputNickName.SetActive(false);
        }
        */
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setNickName()
    {
        if (inputField.text != "")
        {
            string nickName = inputField.text;

            PlayerPrefs.SetString("NickName", nickName);

            startBtn.SetActive(true);
            inputNickName.SetActive(false);
        }
    }
	
}
