using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DebugTemp : MonoBehaviour
{   
    private void Awake()
    {
        int count1 = 0;
        int count2 = 0;
        int count3 = 0;

        GameObject[] gameObjects = FindObjectsOfType<GameObject>();

        for (int j = 0; j < gameObjects.Length; ++j)
        {
            ReceiveDamage receiveDamage = gameObjects[j].GetComponent<ReceiveDamage>();
            AttackCollider attackCollider = gameObjects[j].GetComponent<AttackCollider>();
            CharacterMove characterMove = gameObjects[j].GetComponent<CharacterMove>();

            if (receiveDamage)
                count1++;

            if (attackCollider != null)
                count2++;

            if (characterMove)
                count3++;
        }

        Debug.Log("ReceiveDamage의 갯수: " + count1);
        Debug.Log("AttackCollider의 갯수: " + count2);
        Debug.Log("CharacterMove의 갯수: " + count3);
    }
}
