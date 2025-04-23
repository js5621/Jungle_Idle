using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class ButtonEquipController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] GameObject parentBtnGoup;
    int childState = 0;

    public void Start()
    {
        SetChildState();
    }

    public void ProcessEquipByButton()
    {
        if (transform.GetChild(0).GetComponent<TextMeshProUGUI>().text.Contains("ÀåÂø"))
        {
            Debug.Log(" ÀåÂø½ÃµµÁß ");
            transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "ÇØÁ¦";
            transform.GetComponent<UnityEngine.UI.Image>().color = Color.red;

            for (int i = 0; i < parentBtnGoup.transform.childCount; i++)
            {

                if (parentBtnGoup.transform.GetChild(i) == transform)
                {
                    continue;
                }

                else
                {
                    if (parentBtnGoup.transform.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().text.Contains("ÇØÁ¦"))
                    {
                        parentBtnGoup.transform.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().text = "ÀåÂø";
                    }

                    parentBtnGoup.transform.GetChild(i).GetComponent<UnityEngine.UI.Image>().color = Color.green;
                }
            }
        }

        else if (transform.GetChild(0).GetComponent<TextMeshProUGUI>().text.Contains("ÇØÁ¦"))
        {
            transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "ÀåÂø";
            transform.GetComponent<UnityEngine.UI.Image>().color = Color.green;


        }
    }

    public void SetChildState()
    {
        for (int i = 0; i < parentBtnGoup.transform.childCount; i++)
        {
            if (parentBtnGoup.transform.GetChild(i).gameObject == transform)
            {
                childState = i;
                break;
            }
        }
    }
        // Update is called once per frame
    void Update()
    {

    }
}
