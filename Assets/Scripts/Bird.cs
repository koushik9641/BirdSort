using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class Bird : MonoBehaviour
{
    public enum BirdClass
    {
        bird1,
        bird2,
        bird3,
        bird4,
        bird5,
        bird6,
        bird7,
        bird8,
        bird9,
        bird10,
        bird11,
        bird12
    }
    public BirdClass birdClass;
    public Transform target;
    public Transform[] sittingPositionsHolders;
    public GameObject[] sittingPositionsHoldersobj;

    public float moveSpeed = 20f;
    Animator anim;
    SkeletonAnimation skeletonAnimation;
    public bool startisfinish = false;
    int idle = Animator.StringToHash("idle");
    public BirdSittingPositions birdSittingPositions;
    public bool selecting = false;

    private void Start()
    {

        anim = GetComponent<Animator>();

        //sittingPositionsHoldersobj = GameObject.FindGameObjectsWithTag("branch");
        //sittingPositionsHolders = new Transform[sittingPositionsHoldersobj.Length];


        //StartCoroutine(waithforbirds());

    }




    IEnumerator waithforbirds()
    {

        yield return new WaitForSeconds(0.01f);

        for (int i = 0; i < sittingPositionsHolders.Length; i++)
        {
            Debug.Log("adding transform");
            sittingPositionsHolders[i] = sittingPositionsHoldersobj[i].GetComponent<Transform>();

        }

    Loop:
        int index = Random.Range(0, sittingPositionsHoldersobj.Length);
        Debug.Log("index val" + index + " gameobname " + gameObject.name);
        birdSittingPositions = sittingPositionsHolders[index].GetComponent<BirdSittingPositions>();
        if (birdSittingPositions.positionsFilled == birdSittingPositions.transform.childCount)
        {
            goto Loop;
        }
        else
        {
            target = birdSittingPositions.transform.GetChild(birdSittingPositions.positionsFilled);
            transform.position = target.position;
            birdSittingPositions.positionsFilled++;
        }
        anim = GetComponent<Animator>();

        startisfinish = true;

    }
    public void posholderloop()
    {
        sittingPositionsHoldersobj = GameObject.FindGameObjectsWithTag("branch");
        sittingPositionsHolders = new Transform[sittingPositionsHoldersobj.Length];

        for (int i = 0; i < sittingPositionsHolders.Length; i++)
        {
            Debug.Log("adding transform");
            sittingPositionsHolders[i] = sittingPositionsHoldersobj[i].GetComponent<Transform>();

        }


    }
    private void Update()
    {

        if (startisfinish == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            if (transform.position == target.position && selecting == false)
            {
                // gameObject.GetComponent<SkeletonAnimation>().
                transform.parent = target;
                transform.rotation = Quaternion.Euler(transform.rotation.x, target.parent.eulerAngles.y + 180f, transform.rotation.z);

                gameObject.GetComponent<SkeletonAnimation>().AnimationName = "idle";
            }
            else if (target.position.x > transform.position.x && selecting == false)
            {
                transform.rotation = Quaternion.Euler(transform.rotation.x, 180, transform.rotation.z);
                gameObject.GetComponent<SkeletonAnimation>().AnimationName = "fly";
                //anim.Play("Fly");
            }
            else if (selecting == false)
            {
                transform.rotation = Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z);
                gameObject.GetComponent<SkeletonAnimation>().AnimationName = "fly";
                //anim.Play("Fly");
            }

        }

    }

    public void MoveToNextTarget(Transform nextTarget, bool sorted = false)
    {



        transform.parent.parent.GetComponent<BirdSittingPositions>().positionsFilled--;
        if (!sorted)
        {
            selecting = false;
            nextTarget.parent.GetComponent<BirdSittingPositions>().positionsFilled++;
            transform.parent.parent.GetComponent<BirdSittingPositions>().selected = false;
            SoundManager.instance.Play(SoundManager.instance.birdFlySound);
            if (birdClass == BirdClass.bird1)
            {
                SoundManager.instance.Play(SoundManager.instance.bigBirdSound);
            }
            else if (birdClass == BirdClass.bird2 || birdClass == BirdClass.bird3)
            {
                SoundManager.instance.Play(SoundManager.instance.smallBirdSound);
            }
        }
        transform.parent.localScale = new Vector3(1, 1, 1);
        target = nextTarget;




    }



}
