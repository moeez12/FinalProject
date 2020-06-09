using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class gunHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator anim;
    public GameObject muzzleFlash;
    public AudioSource shootSound;
    public AudioSource RelodSound;
    public Firetype firetypee;
    public GameObject attack_point;
    [SerializeField]


    public enum Firetype
    {
        Single,
        Multiple

    }

    public Firetype FireTypee
    {
        get { return firetypee; }
        set { firetypee = value; }

    }






    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    void PlayReloadSound()
    { RelodSound.Play(); }

   public void shootAnimation() {
        anim.SetTrigger("shoott");
    }
  
    void turnOnMuzzleFlash()
    {

        muzzleFlash.SetActive(true);
    }
    void turnOfMuzzleFlash()
    {

        muzzleFlash.SetActive(false);
    }
    public void playshootSound()
    { shootSound.Play();

    }

    void TurnOnAttackPoint()
    { attack_point.SetActive(true); }
    void TurnOfAttackPoint()
    {if(attack_point.activeInHierarchy) attack_point.SetActive(false); }











    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
