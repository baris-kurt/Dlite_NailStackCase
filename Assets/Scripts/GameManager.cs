using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    private static GameManager _instance;
    
    public List<GameObject> collectedNails;
    public GameObject Player;



    // private Vector3 _velocity = Vector3.zero;
    // public float smoothSpeed = 3f;
    
    public static GameManager Instance{
        get { 
            if(_instance == null) Debug.LogError("ERROR@GameManager");
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }
    
    private void FixedUpdate()
    {
        foreach (var collectedNail in collectedNails)
        {
            // BU KISMI OKUNABİLİR OLMASI AÇISINDAN AYRI AYRI ELE ALDIM.
            
            var collectedNailScript = collectedNail.GetComponent<NailController>();
            Vector3 pos = collectedNail.transform.position;
            pos.x = Mathf.Lerp(collectedNail.transform.position.x, collectedNailScript.LastNail.transform.position.x, 0.15f);
            pos.y = collectedNailScript.LastNail.transform.position.y;
            pos.z = collectedNailScript.LastNail.transform.position.z + collectedNail.transform.localScale.z;
            collectedNail.transform.position = pos;
            
         



            /*
             
            [ ** BU ALANDA DAHA ÖNCE DENEMİŞ OLDUĞUM DALGA EFEKTLERİ VAR, BUNLARIN KENDİ İÇERİSİNDE HATALARI VE ESTETİK AÇIDAN YANLIŞLIKLARI VARDI. ** ]
             
           Vector3 pos = collectedNail.transform.localPosition;
           pos.x = Mathf.SmoothDamp(collectedNail.transform.position.x, Player.transform.position.x, ref _velocity, 0.125f);
           collectedNail.transform.localPosition = pos;

            collectedNail.transform.position = lastNail.position + new Vector3(0,0,collectedNail.transform.lossyScale.z);

            collectedNail.transform.position = Vector3.SmoothDamp(collectedNail.transform.position, Player.transform.position + new Vector3(0,0,lastNail.transform.lossyScale.z), ref _velocity, smoothSpeed);
            collectedNail.transform.position = lastNail.position + new Vector3(0,0,lastNail.transform.lossyScale.z);
            Vector3 pos = Player.transform.position + new Vector3(0,0,collectedNail.transform.lossyScale.z);
            pos.y = Mathf.SmoothDamp(collectedNail.transform.position.y, pos.y, ref _velocity, 0.25f)
            collectedNail.transform.position = pos;
            */
        }
    }
    public void CollidedNail(GameObject collided)
    {
        
        // Player.GetComponent<BoxCollider>().size = new Vector3(1, 1, Player.transform.localScale.z + 0.65f);
        // collided.transform.parent = Player.transform;
        
        //  collided.transform.position = new Vector3(lastNail.transform.localPosition.x, lastNail.transform.localPosition.y, lastNail.localScale.z + 
        // collided.transform.position = lastNail.position + new Vector3(0,0,lastNail.transform.lossyScale.z);
        
        
        collided.GetComponent<Rigidbody>().isKinematic = collided.GetComponent<Collider>().isTrigger = true;
        collided.GetComponent<NailController>().LastNail = collectedNails.Count <= 0 ? Player : collectedNails[collectedNails.Count - 1];
        collectedNails.Add(collided);
        collided.GetComponent<NailController>().currentState = NailController.NailState.Collected;
        

    }
    
    public void ObstacledNail(GameObject collided)
    {
        
       var tmp = collectedNails.SingleOrDefault(obj => obj == collided);
       
       
       if(collectedNails.ElementAtOrDefault(collectedNails.IndexOf(tmp) + 1) == null) // ÖNÜNDE HERHANGİ BİR NAIL OLMAYANLAR DİREKT YOK OLACAK.
       {
           collectedNails.Remove(collided);
           Destroy(collided);
       }
       else if(collectedNails.ElementAtOrDefault(collectedNails.IndexOf(tmp) - 1) != null) // ÖNÜNDE OLUP, ARKASINDA OLMAYAN NAILLER BİR ÖNCEKİNE KUYRUK ŞEKİLDE AKTARILACAK.
       {
           collectedNails[collectedNails.IndexOf(tmp) + 1].GetComponent<NailController>().LastNail = collectedNails[collectedNails.IndexOf(tmp) - 1];
           collectedNails.Remove(collided);
           Destroy(collided);
       }
       else // ÖNÜNDE OLUP ARKASI SADECE PLAYERA YASLI OLAN NAILA ÇARPILDIĞINDA, LASTNAIL PLAYER OLACAK ŞEKİLDE DEĞİŞTİRİLECEK.
       {
           collectedNails[collectedNails.IndexOf(tmp) + 1].GetComponent<NailController>().LastNail = Player;
           collectedNails.Remove(collided);
           Destroy(collided);
       }


    }
}
