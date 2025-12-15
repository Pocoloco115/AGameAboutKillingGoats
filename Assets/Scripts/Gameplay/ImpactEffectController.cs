using System.Collections;
using UnityEngine;

public class ImpactEffectController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(DestroyGameObject());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator DestroyGameObject()
    {
        yield return new WaitForSeconds(0.05f);
        Destroy(gameObject);
    }
}
