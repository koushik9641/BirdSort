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
    private bool hasTilted = false;
    private void Start()
    {

        anim = GetComponent<Animator>();

        Debug.Log("adding transform");
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

            if (transform.position == target.position && selecting == false && !hasTilted)
            {
                // Bird has reached the target
                transform.parent = target;
                transform.rotation = Quaternion.Euler(transform.rotation.x, target.parent.eulerAngles.y + 180f, transform.rotation.z);

                // Set the animation to idle
                gameObject.GetComponent<SkeletonAnimation>().AnimationName = "idle";

                // Call tiltbranch now that the bird is on the target branch
                if (target != null && target.parent != null)
                {
                    BirdSittingPositions sittingPositions = target.parent.GetComponent<BirdSittingPositions>();
                    if (sittingPositions != null)
                    {
                        sittingPositions.tiltbranch();
                    }
                    else
                    {
                        Debug.LogWarning("BirdSittingPositions component is missing on the target's parent: " + target.parent.gameObject.name);
                    }
                }
                else
                {
                    Debug.LogWarning("Target or Target's parent is null when trying to call tiltbranch.");
                }

                // Mark the tilt as done
                hasTilted = true;
            }
            else if (transform.position != target.position && selecting == false)
            {
                // Bird is flying towards the target
                if (target.position.x > transform.position.x)
                {
                    transform.rotation = Quaternion.Euler(transform.rotation.x, 180, transform.rotation.z);
                }
                else
                {
                    transform.rotation = Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z);
                }

                // Set the animation to fly only while the bird is moving
                gameObject.GetComponent<SkeletonAnimation>().AnimationName = "fly";
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

        // Reset the hasTilted flag when moving to a new target
        hasTilted = false;

        // Log the name of the target's parent GameObject
        Debug.Log("Moving to target's parent: " + target.parent.gameObject.name);

        // Call tiltbranch when bird moves to a new target
        transform.parent.parent.GetComponent<BirdSittingPositions>().tiltbranch();


        // Find the GameObject by its name
        GameObject dialogSwap = GameObject.Find("dialogswap");

        // Check if the GameObject is found and active in the hierarchy
        if (dialogSwap != null && dialogSwap.activeInHierarchy)
        {
            // Disable the GameObject
            dialogSwap.SetActive(false);
        }
        else
        {
            Debug.Log("GameObject 'dialogswap' not found or already inactive.");
        }
    }


}
