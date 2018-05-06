using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    public float VulnerabilityTime;
    [SerializeField]
    [Range(1,100)]
    private int Health;
    [SerializeField]
    private bool Vulnerable = true;
    

    public void TakeDamage(int Damage)
    {
        if (Vulnerable)
        {
            Vulnerable = false;
            Health -= Mathf.Abs(Damage);            
            StartCoroutine(ChangeBackToVul());
        }
    }
    private void Update()
    {
        if(Vulnerable) GetComponent<SpriteRenderer>().color = Color.white;
        else GetComponent<SpriteRenderer>().color = Color.blue;
    }
    IEnumerator ChangeBackToVul()
    {        
        yield return new WaitForSeconds(VulnerabilityTime);
        Vulnerable = true;
    }
}
