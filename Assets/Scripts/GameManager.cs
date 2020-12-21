using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private enum Combo
    {
        normal,
        deorbital,
        cosmical,
        blackOlish,
        astronmical,
        supernova
    }
    public GameObject[] levelChunks;
    public GameObject[] FinishingChunks;
    public int amountChunks;
    public Text comboNumber;
    public GameObject hitCombo;
    public GameObject deorbitalImg;
    public GameObject cosmicalImg;
    public GameObject blackHolishImg;
    public GameObject astronomicalImg;
    public GameObject superNovaImg;
    private Color transparencyNumber;
    private Color transparencyText;
    private bool maleNarratorActivate;
    private Stats stats;
    private SkillTree skillTree;
    private Combo actualCombo = Combo.normal;

    // Start is called before the first frame update
    void Start()
    {
        string actualSceneName = SceneManager.GetActiveScene().name;
        stats = (Stats)GameObject.Find("Stats").GetComponent("Stats");
        skillTree = (SkillTree)GameObject.Find("SkillTree").GetComponent("SkillTree");
        if(stats && actualSceneName != "BossFight") stats.ResetStats();
        if(skillTree) skillTree.RestExperiencedEarnedInLevel();
        try
        {
            maleNarratorActivate = FindObjectOfType<CheatCodes>().maleNarratorActivate;
        }
        catch
        {
            Debug.Log("No CheatCodes found");
        }
        int amountChunksTypes = levelChunks.Length;
        float nextPosition = 0f;
        for (int i = 0; i <= amountChunks && actualSceneName.Contains("Level") ; i++)
        {
            GameObject toInitiatie = levelChunks[Mathf.FloorToInt(UnityEngine.Random.Range(0f, Mathf.Round(amountChunksTypes)))];
            if (i == 0)
            {
                nextPosition = -((toInitiatie.GetComponent<BoxCollider>().size.x) / 2);
            }
            else
            {
                nextPosition = nextPosition + ((toInitiatie.GetComponent<BoxCollider>().size.x) / 2);
            }
            Instantiate(toInitiatie, new Vector3(nextPosition, 0, 0), Quaternion.identity);
            nextPosition = nextPosition + (toInitiatie.GetComponent<BoxCollider>().size.x) / 2;
        }
        if (actualSceneName.Contains("Level")){
            GameObject finishingChunk = FinishingChunks[Mathf.FloorToInt(UnityEngine.Random.Range(0f, Mathf.Round(FinishingChunks.Length)))];
            Instantiate(finishingChunk, new Vector3(nextPosition + ((finishingChunk.GetComponent<BoxCollider>().size.x) / 2), 0, 0), Quaternion.identity);
        }
        comboNumber.text = "";
        transparencyNumber = comboNumber.color;
        transparencyNumber.a = 0.0f;
        transparencyText.a = 0.0f;
        hitCombo.SetActive(false);
    }
    private float comboCountLifeTime = 3;
    private float comboCurrentTime = 0;
    private int comboCount = 0;
    // Update is called once per frame
    private float transparency;
    private bool superNova = false;
    private bool astonomical = false;
    private bool blackHolish = false;
    private bool cosmic = false;
    private bool deorbital = false;
    void Update() {
        if (comboCount >= 3 && comboCurrentTime > comboCountLifeTime) {
            comboCount = 0;
            actualCombo = Combo.normal;
            comboCurrentTime = 0f;
            superNova = false;
        }
        if (comboCount >= 3)
        {
            hitCombo.SetActive(true);
            transparency = (comboCountLifeTime - comboCurrentTime) / comboCountLifeTime;
            comboNumber.text = comboCount.ToString();
            if (comboCount < 5)
            {
            }
            else if (comboCount < 8)
            {
                if (!deorbital) {
                    TurnOffRanks();
                    deorbitalImg.SetActive(true);
                    deorbital = true;
                    if (maleNarratorActivate) {
                        FindObjectOfType<AudioManager>().Play("Deorbital");
                    } else {
                        FindObjectOfType<AudioManager>().Play("FDeorbital");
                    }
                }
            } else if ( comboCount < 13 ) {
                if (!cosmic) {
                    cosmic = true;
                    TurnOffRanks();
                    cosmicalImg.SetActive(true);
                    if (maleNarratorActivate) {
                        FindObjectOfType<AudioManager>().Play("Cosmic");
                    } else {
                        FindObjectOfType<AudioManager>().Play("FCosmic");
                    }
                }
            } else if ( comboCount < 21) {
                if (!blackHolish) {
                    blackHolish = true;
                    TurnOffRanks();
                    blackHolishImg.SetActive(true);
                    if (maleNarratorActivate) {
                        FindObjectOfType<AudioManager>().Play("BlackHolish");
                    } else {
                        FindObjectOfType<AudioManager>().Play("FBlackHolish");
                    }
                }
            } else if (comboCount < 34) {
                if (!astonomical) {
                    astonomical = true;
                    TurnOffRanks();
                    astronomicalImg.SetActive(true);
                    if (maleNarratorActivate) {
                        FindObjectOfType<AudioManager>().Play("Astronomical");
                    } else {
                        FindObjectOfType<AudioManager>().Play("FAstronomical");
                    }
                }
            } else if (comboCount >= 34) {
                actualCombo = Combo.supernova;
                if (!superNova)
                {
                    superNova = true;
                    TurnOffRanks();
                    superNovaImg.SetActive(true);
                    if (maleNarratorActivate)
                    {
                        FindObjectOfType<AudioManager>().Play("SuperNova");
                    }
                    else
                    {
                        FindObjectOfType<AudioManager>().Play("FSuperNova");
                    }
                }
            }
            transparencyNumber.a = transparency;
            comboNumber.color = transparencyNumber;
        }
        if (comboCount > 0)
        {
            comboCurrentTime = comboCurrentTime + Time.deltaTime;
        }
        if (comboCount < 3)
        {
            hitCombo.SetActive(false);
            TurnOffRanks();
            transparency = 0;
            transparencyNumber.a = transparency;
            transparencyText.a = transparency;
            comboNumber.color = transparencyNumber;
            superNova = false;
            astonomical = false;
            blackHolish = false;
            cosmic = false;
            deorbital = false;
        }
        if (stats && stats.LevelComplete && stats.MaxCombo < comboCount)
        {
            stats.MaxCombo = comboCount;
        }
    }

    public void AddComboHit()
    {
        comboCount++;
        comboCurrentTime = 0f;
        switch (actualCombo)
        {
            case Combo.normal:
                stats.Score += 100;
                break;
            case Combo.deorbital:
                stats.Score += (100 * stats.deorbital);
                break;
            case Combo.cosmical:
                stats.Score += (100 * stats.cosmical);
                break;
            case Combo.blackOlish:
                stats.Score += (100 * stats.blackHolish);
                break;
            case Combo.astronmical:
                stats.Score += (100 * stats.astronomical);
                break;
            case Combo.supernova:
                stats.Score += (100 * stats.supernova);
                break;
            default:
                break;
        }
    }

    public void ComboInterrupt()
    {
        if (stats && stats.MaxCombo < comboCount)
        {
            stats.MaxCombo = comboCount;
        }
        comboCount = 0;
        actualCombo = Combo.normal;
    }

    public void TurnOffRanks() {
        deorbitalImg.SetActive(false);
        cosmicalImg.SetActive(false);
        blackHolishImg.SetActive(false);
        astronomicalImg.SetActive(false);
        superNovaImg.SetActive(false);
    }
}
