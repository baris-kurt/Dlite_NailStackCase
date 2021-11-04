using UnityEngine;

public class NailController : MonoBehaviour
{
    public enum NailState
    {
        Idle,
        Collected,
    }

    public NailState currentState;

    private void Start()
    {
        currentState = NailState.Idle;
    }

    public GameObject LastNail { get; set; }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("Obstacle")) GameManager.Instance.ObstacledNail(transform.gameObject);
        if (!other.transform.CompareTag("Nail") || other.GetComponent<NailController>().currentState == NailState.Collected) return;
        GameManager.Instance.CollidedNail(other.gameObject);
    }

    /*  // DAHA ÖNCEDEN KENDİ İÇERİSİNDE KONTROL EDİLİYORDU.
     
     
    void FixedUpdate()
    {
        
        if (CurrentState == NailState.STAYING) return;
        
        Vector3 pos = collectedNail.transform.position;
        // pos.x = Mathf.SmoothDamp(collectedNail.transform.position.x, Player.transform.position.x, ref _velocity2, smoothSpeed);
        pos.x = Mathf.Lerp(collectedNail.transform.position.x, Player.transform.position.x, 0.1f);
        pos.y = lastNail.position.y;
        pos.z = lastNail.position.z + collectedNail.transform.lossyScale.z + 0.03f;
        collectedNail.transform.position = pos;

    }
    */
    
}
