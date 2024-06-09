using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PCSCript : MonoBehaviour
{
    [SerializeField] private GameObject page1;
    [SerializeField] private GameObject page2;
    [SerializeField] private GameObject page3;
    [SerializeField] private GameObject nC;
    [SerializeField] private GameObject good;
    [SerializeField] private GameObject bad;
    [SerializeField] private TextMeshProUGUI customerWords;
    [SerializeField] private string customWords;
    [SerializeField] private ConstraintedMovement cm;
    [SerializeField] private GameObject computerCamera;
    [SerializeField] private Sprite[] cardBack = new Sprite[4];
    [SerializeField] private Sprite[] stars = new Sprite[4];
    [SerializeField] private GameObject starObject;
    [SerializeField] private GameObject cardObject;
    [SerializeField] private GameObject objective2;
    [SerializeField] private GameObject stuffToHide;
    [SerializeField] private TextMeshProUGUI[] options;
    public TimerScript ts;
    private int correctBackground = 0;
    private int selectedBackground = 0;
    private int correctOption = 0;
    private string selectedOption = "";
    private int scoreMult = 0;
    private int page = 0;
    public bool shipped = false;
    public bool started = false;
    private string[] keywords = new string[3];
    // less than 6 is option 0, less than 12 is option 1, less than 18 is option 2, 18-23 is option 3
    private string[] keywordDB = { "Straitforward", "Primordial", "Crude", "Basal", "White", "Simple", "Colorful", "Floral", "Leafy", "Verdant", "Green", "Lush", "Sleek", "Fancy", "Elegant", "Frilly", "Ostentatious", "Black", "Bubbly", "Unique", "Peach", "Daisy", "Pippin", "Sockdolager" };
    private string[] optionDB = { "Basic Company", "Traditional", "Reliable", "Artsy Business", "Farmy Peeps", "Organic", "Tesla Endorsed", "Tron Bikes", "Nuclear", "Soft", "Sweet", "Comfortable" };

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1);
    }

    public void NewContract()
    {
        page1.SetActive(false);
        page2.SetActive(false);
        page3.SetActive(false);
        nC.SetActive(true);
        good.SetActive(false);
        bad.SetActive(false);
        LeanTween.moveY(nC, 25f, 1);
        Delay();
        LeanTween.moveY(nC, 450f, 0.5f);
    }

    public void Satisfaction()
    {
        page1.SetActive(false);
        page2.SetActive(false);
        page3.SetActive(false);
        nC.SetActive(false);
        good.SetActive(true);
        bad.SetActive(false);
        LeanTween.moveY(good, 25f, 1);
        Delay();
        LeanTween.moveY(good, 450f, 0.5f);
    }

    public void youSuck()
    {
        page1.SetActive(false);
        page2.SetActive(false);
        page3.SetActive(false);
        nC.SetActive(false);
        good.SetActive(false);
        bad.SetActive(true);
        LeanTween.moveY(bad, 25f, 1);
        Delay();
        LeanTween.moveY(bad, 450f, 0.5f);
    }

    public void displayCustomer()
    {
        switch(page)
        {
            case 0:
                page1.SetActive(true);
                page2.SetActive(false);
                page3.SetActive(false);
                break;
            case 1:
                page1.SetActive(false);
                page2.SetActive(true);
                page3.SetActive(false);
                break;
            default:
                page1.SetActive(false);
                page2.SetActive(false);
                page3.SetActive(true);
                break;
        }
    }

    void RandomizeValues()
    {
        correctBackground = (int)Mathf.Round(Random.Range(0, 3));
        int selected = 0;
        while (selected < 3)
        {
            int rand = correctBackground*6 + (int)Mathf.Round(Random.Range(0, 5));
            if(selected > 0)
            {
                foreach(string keyword in keywords)
                {
                    if(keyword == keywordDB[rand])
                    {
                        goto outside;
                    }
                }
                keywords[selected] = keywordDB[rand];
                selected++;
            outside:
                continue;
            }
        }
        options[0].text = optionDB[(int)Mathf.Round(Random.Range(0, 2))];
        options[1].text = optionDB[(int)Mathf.Round(Random.Range(3, 5))];
        options[2].text = optionDB[(int)Mathf.Round(Random.Range(6, 9))];
        customerWords.text = "I'm Looking for something a little " + keywords[0] + "that brings out a " + keywords[1] + " vibe and feels " + keywords[2];
    }

    public void StartUpPC()
    {
        Cursor.lockState = CursorLockMode.Confined;
        stuffToHide.SetActive(false);
        NewContract();
        RandomizeValues();
        page = 0;
        displayCustomer();
        stuffToHide.SetActive(true);
        started = true;
    }

    public void SelectOption0()
    {
        if(page == 0)
        {
            scoreMult = correctBackground == 0 ? scoreMult + 3 : scoreMult;
        }
        else
        {
            scoreMult = correctOption == 0 ? scoreMult + 3 : scoreMult;
        }
    }

    public void SelectOption1()
    {
        if (page == 0)
        {
            scoreMult = correctBackground == 1 ? scoreMult + 3 : scoreMult;
        }
        else
        {
            scoreMult = correctOption == 1 ? scoreMult + 3 : scoreMult;
        }
    }

    public void SelectOption2()
    {
        if (page == 0)
        {
            scoreMult = correctBackground == 2 ? scoreMult + 3 : scoreMult;
        }
        else
        {
            scoreMult = correctOption == 2 ? scoreMult + 3 : scoreMult;
        }
    }

    public void CustomText(string arg0)
    {
        customWords = arg0;
    }

    public void SelectOption3()
    {
        if (page == 0)
        {
            scoreMult = correctBackground == 3 ? scoreMult + 3 : scoreMult;
        }
        else
        {
            for(int i = 0;i < 3; i++)
            {
                if (customWords.Contains(keywords[i]))
                {
                    scoreMult += 2;
                }
            }
        }
    }

    private void nextPage()
    {
        if(page == 1)
        {
            displayCustomer();
            cardObject.GetComponent<Image>().sprite = cardBack[selectedBackground];
        }
        else if(page == 2)
        {
            cm.movementActive = true;
            computerCamera.SetActive(false);
            objective2.SetActive(true);
        } else
        {
            if(scoreMult > 6) { scoreMult = 6; }
            starObject.GetComponent<Image>().sprite = stars[scoreMult-2];
            stuffToHide.SetActive(false);
            if(scoreMult-2 > 2)
            {
                Satisfaction();
            }
            else
            {
                youSuck();
            }
            page = 2;
            displayCustomer();
        }
    }

    void Update()
    {
        if(page == 2 && Input.GetKey(KeyCode.Return) && started)
        {
            page = 0;
            StartUpPC();
        }
        if(ts.TIME == ts.elapsed && ts.TIME != 0 && started)
        {
            page++;
            nextPage();
        }
    }

}
