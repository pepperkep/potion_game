using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TempEffect : MonoBehaviour
{

    public float tickCount;
    public float tickTime;
    public string returnPoolName = "";
    [HideInInspector]
    public MonoBehaviour targetComponent;
    private IEnumerator effectRoutine;

    // Start is called before the first frame update
    void OnEnable()
    {
        FindTarget();
        effectRoutine = ApplyEffect();
        StartCoroutine(effectRoutine);
    }

    public void KillEffect()
    {
        ObjectPool.Instance.AddToPool(returnPoolName, this.gameObject);
    }

    private IEnumerator ApplyEffect()
    {
        int count = 0;
        while(count < tickCount)
        {
            ApplyTick();
            count++;
            yield return new WaitForSeconds(tickTime);
        }
        EndEffect();
        if(returnPoolName == "")
        {
            Destroy(this.gameObject);
        }
        else
        {
            ObjectPool.Instance.AddToPool(returnPoolName, this.gameObject);
        }
    }

    public abstract void FindTarget();

    public abstract void EndEffect();

    public abstract void ApplyTick();
}
