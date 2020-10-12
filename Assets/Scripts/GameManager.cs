using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] levelChunks;
    public int amountChunks;
    // Start is called before the first frame update
    void Start()
    {
        int amountChunksTypes = levelChunks.Length;
        float nextPosition = 0f;
        for (int i = 0; i <= amountChunks; i++) {
            GameObject toInitiatie = levelChunks[Mathf.FloorToInt(UnityEngine.Random.Range(0f, Mathf.Round(amountChunksTypes)))];
            if (nextPosition == 0f) {
                nextPosition = ((toInitiatie.GetComponent<Transform>().localScale.x) / 2) - 4;
            }
            Instantiate(toInitiatie, new Vector3(nextPosition, 0, 0), Quaternion.identity);
            nextPosition = nextPosition + toInitiatie.GetComponent<Transform>().localScale.x;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
