using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkmyvision : MonoBehaviour
{//senstivity of the vision
    public enum ensenstivity {HIGH , LOW };

    //checking the senstivity

    public ensenstivity senstivity = ensenstivity.HIGH;

    // is target in sight
    public bool targetInSight = false;

    //field of vision
    public float fieldOfVision = 45f;

   public float angle = 180f;
    public bool cls = false;
        public bool ins = false;

    //target
    private Transform target = null;

    //my eyes
    public Transform myEyes = null;

    //transform component
    public Transform npcTransform = null;

    //my sphere collider
    private SphereCollider ccollider = null;

    //last knwn location
    public Vector3 lastKnownLocation = Vector3.zero;

    private void Awake()
    {
        npcTransform = GetComponent<Transform>();
        ccollider = GetComponent<SphereCollider>();
        lastKnownLocation = npcTransform.position;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    bool inMyFieldOfVision() {

        Vector3 dirtoTarget = target.position - myEyes.position;
        angle = Vector3.Angle(myEyes.forward, dirtoTarget);

        if (angle <= fieldOfVision) return true;
        else return false;

    }


    bool ClearLineOfSight() {
        if (!(myEyes == null && target == null))
        {
            RaycastHit hit;
            if (Physics.Raycast(myEyes.position, (target.position - myEyes.position).normalized,
                out hit, ccollider.radius))
            {

                if (hit.transform.CompareTag("Player")) { return true; }
                else return false;


            }
            return false;
        }
        return false;

    }

    void UpdateSight()
    {

        switch (senstivity)
        {
            case ensenstivity.HIGH:
                cls = ClearLineOfSight();
                ins = inMyFieldOfVision();
                targetInSight = ins&& cls;
                break;

            case ensenstivity.LOW:
                cls = ClearLineOfSight();
                ins = inMyFieldOfVision();
                targetInSight = ins || cls;
                break;
        }


    }


    private void OnTriggerStay(Collider other)
    {
        UpdateSight();

        //Update last known sighting
        if (targetInSight)
            lastKnownLocation = target.position;

    }


    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        targetInSight = false;


    }

    // Start is called before the first frame update


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
