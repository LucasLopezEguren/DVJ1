using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    public enum Transitions
    {
        Crossfade,
        CircleWipe
    }

    public Transitions transition;

    public float transitionTime = 1f;

    private Animator transitionAnim;

    // Start is called before the first frame update
    void Start()
    {
        switch (transition)
        {
            case Transitions.Crossfade:
                transform.Find("Crossfade").gameObject.SetActive(true);
                transitionAnim = transform.Find("Crossfade").gameObject.GetComponent<Animator>();
                break;
            case Transitions.CircleWipe:
                transform.Find("CircleWipe").gameObject.SetActive(true);
                transitionAnim = transform.Find("CircleWipe").gameObject.GetComponent<Animator>();
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadNextLevel(string scene)
    {
        StartCoroutine(LoadScene(scene));
    }

    IEnumerator LoadScene(string scene)
    {
        transitionAnim.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(scene);
    }

}
