using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageInfoOpener : MonoBehaviour
{
    public GameObject stageInfoPanel;

    public void OpenPanel()
    {
        stageInfoPanel.SetActive(true);

        Animator animator = stageInfoPanel.GetComponent<Animator>();

        if (stageInfoPanel != null)
        {
            animator.SetBool("bOpen", true);
        }
    }

    public void ClosePanel()
    {
        Animator animator = stageInfoPanel.GetComponent<Animator>();

        if (stageInfoPanel != null)
        {
            animator.SetBool("bOpen", false);
            StartCoroutine(nameof(WaitOutPanel));
        }
    }


    private void Start()
    {
        stageInfoPanel.SetActive(false);
    }

    IEnumerator WaitOutPanel()
    {
        yield return new WaitForSeconds(0.5f);

        stageInfoPanel.SetActive(false);
    }
}
