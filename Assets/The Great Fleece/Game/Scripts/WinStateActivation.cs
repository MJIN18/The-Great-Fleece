using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinStateActivation : MonoBehaviour
{
    [SerializeField] private GameObject _winLevelCutscene;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.Instance.HasCard)
            {
                _winLevelCutscene.SetActive(true);
            }
            else
            {
                Debug.Log("Key Card is missing!");
            }
        }
    }
}
