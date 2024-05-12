using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public MainManager Manager;
    [SerializeField] AudioClip exitSound;
    [SerializeField] GameObject clonePos;

    private void OnCollisionEnter(Collision other)
    {
        Destroy(other.gameObject);
        DeathSound();
        Manager.GameOver();
    }

    void DeathSound()
    {
        GameObject audioclone = Instantiate(MainUIManager.Instance.AudioSourcePrefab, clonePos.transform.position, Quaternion.identity);

        audioclone.transform.GetComponent<AudioSource>().PlayOneShot(exitSound);

        StartCoroutine(DestroyClone(audioclone));
    }

    IEnumerator DestroyClone(GameObject clonePref)
    {
        yield return new WaitForSeconds(2);

        Destroy(clonePref);
    }
}
