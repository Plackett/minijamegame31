using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PCSCript : MonoBehaviour
{
    public TMP_InputField tmpInputField;
    [SerializeField] private GameObject page1;
    [SerializeField] private GameObject page2;
    [SerializeField] private GameObject page3;
    [SerializeField] private audioController aC;
    [SerializeField] private GameObject nC;
    [SerializeField] private GameObject good;
    [SerializeField] private GameObject bad;
    [SerializeField] private TextMeshProUGUI customerWords;
    [SerializeField] private TextMeshProUGUI quota;
    [SerializeField] private string customWords;
    [SerializeField] private ConstraintedMovement cm;
    [SerializeField] private GameObject computerCamera;
    [SerializeField] private Sprite[] cardBack = new Sprite[4];
    [SerializeField] private Sprite[] stars = new Sprite[5];
    [SerializeField] private GameObject starObject;
    [SerializeField] private GameObject cardObject;
    [SerializeField] private GameObject objective2;
    [SerializeField] private GameObject objective5;
    [SerializeField] private GameObject stuffToHide;
    [SerializeField] private GameObject cards;
    [SerializeField] private Texture2D cursorTexture;
    public GameObject door;
    [SerializeField] private TextMeshProUGUI[] options;
    [SerializeField] private GameObject endPos;
    [SerializeField] private GameObject startPos;
    public TimerScript ts;
    private int correctBackground = 0;
    private int selectedBackground = 0;
    private int todayQuota = 10;
    public int contractsCompleted = 0;
    public int contractsFailed = 0;
    public int overallScore = 0;
    [SerializeField] private int scoreMult = 0;
    public int page = 0;
    public bool shipped = false;
    public bool started = false;
    private bool debounce = true;
    public string[] keywords = new string[3];
    // less than 6 is option 0, less than 12 is option 1, less than 18 is option 2, 18-23 is option 3
    private string[] keywordDB = { "Straitforward", "Primordial", "Crude", "Basal", "White", "Simple", "Colorful", "Floral", "Leafy", "Verdant", "Green", "Lush", "Sleek", "Fancy", "Elegant", "Frilly", "Ostentatious", "Black", "Bubbly", "Unique", "Peach", "Daisy", "Pippin", "Sockdolager" };
    private string[] optionDB = { "Basic Company", "Traditional", "Reliable", "Artsy Business", "Farmy Peeps", "Organic", "Tesla Endorsed", "Tron Bikes", "Nuclear", "Soft", "Sweet", "Comfortable" };

    IEnumerator WaitToDisplay()
    {
        yield return new WaitForSeconds(3);
        debounce = true;
        quota.text = "" + (contractsCompleted + contractsFailed) + "/10";
        page = 2;
        displayCustomer();
    }

    IEnumerator StartPCEnd()
    {
        yield return new WaitForSeconds(5);
        RandomizeValues();
        page = 0;
        displayCustomer();
        stuffToHide.SetActive(true);
    }

    IEnumerator ResetTransform(GameObject toMove)
    {
        yield return new WaitForSeconds(2);
        LeanTween.move(toMove,startPos.transform.position, 1f).setEase(LeanTweenType.easeInBack);
        yield return new WaitForSeconds(1);
        toMove.SetActive(false);
    }

    public void NewContract()
    {
        page1.SetActive(false);
        page2.SetActive(false);
        page3.SetActive(false);
        nC.SetActive(true);
        good.SetActive(false);
        bad.SetActive(false);
        LeanTween.move(nC,endPos.transform.position, 1f).setEase(LeanTweenType.easeInBack);
        StartCoroutine(ResetTransform(nC));
    }

    public void Satisfaction()
    {
        page1.SetActive(false);
        page2.SetActive(false);
        page3.SetActive(false);
        nC.SetActive(false);
        good.SetActive(true);
        bad.SetActive(false);
        LeanTween.move(good, endPos.transform.position, 1f).setEase(LeanTweenType.easeInBack);
        StartCoroutine(ResetTransform(good));
    }

    public void youSuck()
    {
        page1.SetActive(false);
        page2.SetActive(false);
        page3.SetActive(false);
        nC.SetActive(false);
        good.SetActive(false);
        bad.SetActive(true);
        LeanTween.move(bad, endPos.transform.position, 1f).setEase(LeanTweenType.easeInBack);
        StartCoroutine(ResetTransform(bad));
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
        int[] selectedKeyWords = { correctBackground*6 + (int)Mathf.Round(Random.Range(0, 5)),
            correctBackground*6 + (int)Mathf.Round(Random.Range(0, 5)),
            correctBackground*6 + (int)Mathf.Round(Random.Range(0, 5)),
        };
        while (selectedKeyWords[1] == selectedKeyWords[0])
        {
            selectedKeyWords[1] = correctBackground * 6 + (int)Mathf.Round(Random.Range(0, 5));
        }
        while (selectedKeyWords[2] == selectedKeyWords[1] || selectedKeyWords[2] == selectedKeyWords[0])
        {
            selectedKeyWords[2] = correctBackground * 6 + (int)Mathf.Round(Random.Range(0, 5));
        }
        keywords[0] = keywordDB[selectedKeyWords[0]];
        keywords[1] = keywordDB[selectedKeyWords[1]];
        keywords[2] = keywordDB[selectedKeyWords[2]];
        options[0].text = optionDB[(int)Mathf.Round(Random.Range(0, 2))];
        options[1].text = optionDB[(int)Mathf.Round(Random.Range(3, 5))];
        options[2].text = optionDB[(int)Mathf.Round(Random.Range(6, 9))];
        customerWords.text = "I'm Looking for something a little " + keywords[0] + " that brings out a " + keywords[1] + " vibe and feels " + keywords[2];
    }

    public void StartUpPC()
    {
        aC.StopBackTrackOffice();
        started = true;
        aC.StartBackTrackPC();
        ts.StopTime();
        ts.StartTime(30 - contractsFailed * 5);
        Cursor.lockState = CursorLockMode.Confined;
        stuffToHide.SetActive(false);
        NewContract();
        StartCoroutine(StartPCEnd());
    }

    public void SelectOption0()
    {
        selectedBackground = 0;
        if (page == 0)
        {
            if (correctBackground == 0)
            {
                scoreMult += 3;
            }
        }
        else
        {
            if (correctBackground == 0)
            {
                scoreMult += 3;
            }
        }
        page++;
        nextPage();
    }

    public void SelectOption1()
    {
        selectedBackground = 1;
        if (page == 0)
        {
            if (correctBackground == 1)
            {
                scoreMult += 3;
            }
        }
        else
        {
            if (correctBackground == 1)
            {
                scoreMult += 3;
            }
        }
        page++;
        nextPage();
    }

    public void SelectOption2()
    {
        selectedBackground = 2;
        if (page == 0)
        {
            if(correctBackground == 2)
            {
                scoreMult += 3;
            }
        }
        else
        {
            if (correctBackground == 2)
            {
                scoreMult += 3;
            }
        }
        page++;
        nextPage();
    }

    public void CustomText(string arg0)
    {
        customWords = arg0;
    }

    public void SelectOption3()
    {
        selectedBackground = 3;
        if (page == 0)
        {
            if (page == 0)
            {
                if (correctBackground == 3)
                {
                    scoreMult += 3;
                }
            }
            else
            {
                if (correctBackground == 3)
                {
                    scoreMult += 3;
                }
            }
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
        page++;
        nextPage();
    }

    public void nextPage()
    {
        aC.PlayClick();
        ts.StopTime();
        if(page == 1)
        {
            ts.StartTime(30-contractsFailed*5);
            displayCustomer();
            cardObject.GetComponent<Image>().sprite = cardBack[selectedBackground];
        }
        else if(page == 2)
        {
#if UNITY_WEBGL
                Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.ForceSoftware);
#else
            Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
#endif
            Cursor.lockState = CursorLockMode.Confined;
            cm.setMovementActive(true);
            computerCamera.SetActive(false);
            objective2.SetActive(true);
            cards.SetActive(true);
            aC.PrinterTrack();
            aC.StopBackTrackPC();
            aC.StartBackTrackOffice();
            this.gameObject.SetActive(false);
        } else
        {
            if(debounce == true)
            {
                debounce = false;
                overallScore += scoreMult;
                if (scoreMult - 2 > 4)
                {
                    scoreMult = 6;
                }
                if (scoreMult < 2)
                {
                    scoreMult = 2;
                }
                if (scoreMult - 2 > 2)
                {
                    aC.PlayGood();
                    contractsCompleted++;
                    Satisfaction();
                }
                else
                {
                    aC.PlayBad();
                    contractsFailed++;
                    youSuck();
                }
                starObject.GetComponent<Image>().sprite = stars[(scoreMult - 2)];
                stuffToHide.SetActive(false);
                StartCoroutine(WaitToDisplay());
            }
        }
    }

    void Update()
    {
        if((page == 2 && Input.GetKey(KeyCode.Return) && started) || (page == 2 && Input.GetMouseButtonDown(0) && started))
        {
            if (contractsCompleted + contractsFailed >= todayQuota)
            {
                cm.setMovementActive(true);
                computerCamera.SetActive(false);
                objective5.SetActive(true);
                door.GetComponent<doorScript>().enabled = true;
                door.GetComponent<doorScript>().clickEnabled = true;
                this.gameObject.SetActive(false);
            }
            else
            {
                scoreMult = 0;
                page = 0;
                StartUpPC();
            }
        }
        if(ts.elapsed >= ts.TIME && ts.TIME != 0 && started)
        {
            page++;
            nextPage();
        }
    }

    void Start()
    {
        tmpInputField.onEndEdit.AddListener(CustomText);
    }

}
