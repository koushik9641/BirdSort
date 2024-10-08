using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Spine.Unity;
using Random = UnityEngine.Random;
using UnityEngine.UIElements;
using System.Linq;

public class BirdSittingPositions : MonoBehaviour
{
    public int positionsFilled = 0;
    public static int steps = 0;
    public static Transform birdSelected;

    public bool selected = false;
    bool sorted = false, available = true;
    Transform lastTarget;
    public int brunchid;
    //public static List<GameObject> selectedbirds;

    private void OnMouseDown()
    {
        // SoundManager.instance.Play(SoundManager.instance.clickSound);
        //print("selected="+selected);
        //print("steps="+steps);
        if(LevelManager.Instance.IsShuffleOn)
        {
            ShuffleBirds();
            LevelManager.Instance.IsShuffleOn = false;
            return;
        }
        if (selected)
        {
            print("Selected enter");
            steps = 0;

            /*foreach (GameObject item in BirdsSortingController.instance.selectedbirds)
            {
                item.GetComponent<Bird>().selecting = false;
                item.transform.parent.localScale = new Vector3(1f, 1f, 1);
                item.GetComponent<SkeletonAnimation>().AnimationName = "idle";

            }*/

            Unselectallbirds();

            selected = false;
            Unselectallbrunch();
            //BirdsSortingController.instance.selectedbirds.Clear(); // Chnaged Now //
            //print("Selected end");
            //gameObject.transform.GetChild(positionsFilled - 1).GetChild(0).GetComponent<Bird>().selecting = false;
            return;
        }
        else
        {
            selected = true;
            print("Selected true");
        }
        steps++;
        if (steps == 1 && positionsFilled > 0)
        {
            print("positionsFilled >0");

            // transform.GetChild(positionsFilled - 1).localScale = new Vector3(1.3f, 1.3f, 1);

            if (positionsFilled - 1 == 0/*transform.GetChild(positionsFilled).childCount==0*/)
            {
                Unselectallbirds();
                //gameObject.transform.GetChild(positionsFilled - 1).GetChild(0).GetComponent<Bird>().selecting = true;
                //BirdsSortingController.instance.selectedbirds.Clear();  /// Changed New //
                BirdsSortingController.instance.selectedbirds.Add(transform.GetChild(positionsFilled - 1).GetChild(0).gameObject);
                //selectedbirds[1].GetComponent<Bird>().selecting = true;
                //selectedbirds[1].transform.parent.localScale = new Vector3(1.3f, 1.3f, 1);
                //selectedbirds[1].GetComponent<SkeletonAnimation>().AnimationName = "touching";
                foreach (GameObject item in BirdsSortingController.instance.selectedbirds)
                {
                    item.GetComponent<Bird>().selecting = true;
                    item.transform.parent.localScale = new Vector3(1.3f, 1.3f, 1);
                    item.GetComponent<SkeletonAnimation>().AnimationName = "touching";
                }
            }

            if (positionsFilled >= 2)
            {
                if (transform.GetChild(positionsFilled - 2).childCount >= 1 && transform.GetChild(positionsFilled - 1).GetChild(0).GetComponent<Bird>().birdClass != transform.GetChild(positionsFilled - 2).GetChild(0).GetComponent<Bird>().birdClass)
                {
                    Unselectallbirds();
                    //gameObject.transform.GetChild(positionsFilled - 1).GetChild(0).GetComponent<Bird>().selecting = true;
                    //BirdsSortingController.instance.selectedbirds.Clear();  /// Changed New //
                    BirdsSortingController.instance.selectedbirds.Add(transform.GetChild(positionsFilled - 1).GetChild(0).gameObject);
                    //selectedbirds[1].GetComponent<Bird>().selecting = true;
                    //selectedbirds[1].transform.parent.localScale = new Vector3(1.3f, 1.3f, 1);
                    //selectedbirds[1].GetComponent<SkeletonAnimation>().AnimationName = "touching";
                    foreach (GameObject item in BirdsSortingController.instance.selectedbirds)
                    {
                        item.GetComponent<Bird>().selecting = true;
                        item.transform.parent.localScale = new Vector3(1.3f, 1.3f, 1);
                        item.GetComponent<SkeletonAnimation>().AnimationName = "touching";
                    }
                }





                if (transform.GetChild(positionsFilled - 1).GetChild(0).GetComponent<Bird>().birdClass == transform.GetChild(positionsFilled - 2).GetChild(0).GetComponent<Bird>().birdClass)
                {
                    Unselectallbirds();
                    //BirdsSortingController.instance.selectedbirds.Clear(); /// Changed New //
                    BirdsSortingController.instance.selectedbirds.Add(transform.GetChild(positionsFilled - 1).GetChild(0).gameObject);
                    BirdsSortingController.instance.selectedbirds.Add(transform.GetChild(positionsFilled - 2).GetChild(0).gameObject);
                    foreach (GameObject item in BirdsSortingController.instance.selectedbirds)
                    {
                        item.GetComponent<Bird>().selecting = true;
                        item.transform.parent.localScale = new Vector3(1.3f, 1.3f, 1);
                        item.GetComponent<SkeletonAnimation>().AnimationName = "touching";
                    }

                    if (positionsFilled >= 3)
                    {

                        if (transform.GetChild(positionsFilled - 1).GetChild(0).GetComponent<Bird>().birdClass == transform.GetChild(positionsFilled - 3).GetChild(0).GetComponent<Bird>().birdClass)
                        {
                            Unselectallbirds();
                            //BirdsSortingController.instance.selectedbirds.Clear();  /// Changed New ///
                            BirdsSortingController.instance.selectedbirds.Add(transform.GetChild(positionsFilled - 1).GetChild(0).gameObject);
                            BirdsSortingController.instance.selectedbirds.Add(transform.GetChild(positionsFilled - 2).GetChild(0).gameObject);
                            BirdsSortingController.instance.selectedbirds.Add(transform.GetChild(positionsFilled - 3).GetChild(0).gameObject);
                            foreach (GameObject item in BirdsSortingController.instance.selectedbirds)
                            {
                                item.transform.parent.localScale = new Vector3(1.3f, 1.3f, 1);
                                item.GetComponent<Bird>().selecting = true;
                                item.GetComponent<SkeletonAnimation>().AnimationName = "touching";
                            }

                        }

                    }

                }

            }

            //touchanim(transform.GetChild(positionsFilled - 1).GetChild(0));

            //for (int i = 0; i < 4; i++)
            //{
            //    if(transform.GetChild(positionsFilled - 1).GetChild(0).GetComponent<Bird>().birdClass== transform.GetChild(positionsFilled - 2).GetChild(0).GetComponent<Bird>().birdClass)
            //    {

            //    }
            //}
            //transform.GetChild(positionsFilled - 1).GetChild(0).gameObject.GetComponent<SkeletonAnimation>().AnimationName = "touching";
            //gameObject.transform.GetChild(positionsFilled-1).GetChild(0).GetComponent<SkeletonAnimation>().AnimationName = "touching";


            try
            {
                birdSelected = transform.GetChild(positionsFilled - 1).GetChild(0);

            }
            catch (Exception e)
            {
                Debug.Log(e);
                steps = 0;
                selected = false;
                Unselectallbrunch();
                return;
            }
        }
        else if (steps == 2)
        {
            //print("steps == 2");
            foreach (GameObject item in BirdsSortingController.instance.selectedbirds)
            {
                //item.GetComponent<SkeletonAnimation>().AnimationName = "idle";
                foreach (Transform t in item.GetComponent<Bird>().sittingPositionsHolders)
                {
                    Debug.Log("calling 1");
                    t.GetComponent<BirdSittingPositions>().selected = false;

                    //gameObject.transform.GetChild(positionsFilled - 1).GetChild(0).GetComponent<SkeletonAnimation>().AnimationName = "idle";

                }
            }



            if (positionsFilled != 0)
            {
                //foreach (GameObject item in BirdsSortingController.instance.selectedbirds)
                //{

                if (positionsFilled != transform.childCount /*&& transform.GetChild(positionsFilled - 1).childCount != 0*/)
                {
                    Debug.Log("counting1");
                    Debug.Log("counting1 item name in pos" + transform.GetChild(positionsFilled - 1).GetChild(0).name);
                    //Debug.Log("counting1 item name" + item.name);

                    if (transform.GetChild(positionsFilled - 1).GetChild(0).GetComponent<Bird>().birdClass == BirdsSortingController.instance.selectedbirds[0].GetComponent<Bird>().birdClass)
                    {
                        Debug.Log("counting2");
                        List<Bird> movedBirds = new();
                        List<Transform> movedFroms = new();
                        foreach (GameObject ite in BirdsSortingController.instance.selectedbirds)
                        {
                            if (positionsFilled != transform.childCount)
                            {
                                if (ite.transform.parent.gameObject.transform.parent.GetComponent<BirdSittingPositions>().brunchid != transform.GetComponent<BirdSittingPositions>().brunchid)
                                {
                                    movedBirds.Add(ite.GetComponent<Bird>());
                                    movedFroms.Add(ite.GetComponent<Bird>().target);
                                    ite.GetComponent<Bird>().MoveToNextTarget(transform.GetChild(positionsFilled));
                                    //selected = false;
                                    Unselectallbrunch(); //---- New ----
                                                         //BirdsSortingController.instance.selectedbirds.Clear();
                                }

                                else
                                {

                                    //Unselectallbirds();
                                    ite.GetComponent<Bird>().selecting = false;
                                    ite.transform.parent.localScale = new Vector3(1f, 1f, 1);

                                    Unselectallbrunch();


                                }

                            }
                            else
                            {
                                /*foreach (GameObject itemm in BirdsSortingController.instance.selectedbirds)
                                {
                                    itemm.GetComponent<Bird>().selecting = false;
                                    itemm.transform.parent.localScale = new Vector3(1f, 1f, 1);
                                    itemm.GetComponent<SkeletonAnimation>().AnimationName = "idle";
                                    //selected = false;// New
                                    Unselectallbrunch();
                                }*/

                                Unselectallbirds();
                                Unselectallbrunch();
                            }


                        }
                        if(movedBirds.Count>0)
                        {
                            LevelManager.Instance.RecordMove(movedBirds,movedFroms);
                        }

                        //positionsFilled++;

                    }
                    else
                    {
                        /*foreach (GameObject itemm in BirdsSortingController.instance.selectedbirds)
                        {
                            itemm.GetComponent<Bird>().selecting = false;
                            itemm.transform.parent.localScale = new Vector3(1f, 1f, 1);
                            itemm.GetComponent<SkeletonAnimation>().AnimationName = "idle";
                            Unselectallbrunch();
                            //selected = false; // New
                        }*/

                        Unselectallbirds();
                        Unselectallbrunch();
                    }


                }
                else
                {
                    /*foreach (GameObject itemm in BirdsSortingController.instance.selectedbirds)
                    {
                        itemm.GetComponent<Bird>().selecting = false;
                        itemm.transform.parent.localScale = new Vector3(1f, 1f, 1);
                        itemm.GetComponent<SkeletonAnimation>().AnimationName = "idle";
                        //selected = false; // New
                        Unselectallbrunch();
                    }*/
                    Unselectallbirds();
                    Unselectallbrunch();
                }
                //}
            }
            else if (positionsFilled == 0)
            {
                //print("positionsFilled == 0");
                int i = 0;
                foreach (GameObject item in BirdsSortingController.instance.selectedbirds)
                {
                    var movedBirds = new List<Bird>();
                    List<Transform> movedFroms = new();
                    if (item.transform.parent.gameObject.transform.parent.GetComponent<BirdSittingPositions>().brunchid != transform.GetComponent<BirdSittingPositions>().brunchid)
                    {
                        movedBirds.Add(item.GetComponent<Bird>());
                        movedFroms.Add(item.GetComponent<Bird>().target);
                        item.GetComponent<Bird>().MoveToNextTarget(transform.GetChild(positionsFilled));
                        selected = false;
                        Unselectallbrunch(); //----New ---
                                             //BirdsSortingController.instance.selectedbirds.Clear();
                    }
                    else
                    {

                        //Unselectallbirds();
                        item.GetComponent<Bird>().selecting = false;
                        item.transform.parent.localScale = new Vector3(1f, 1f, 1);

                        Unselectallbrunch();


                    }
                    i++;
                    if(movedBirds.Count>0)
                    {
                        LevelManager.Instance.RecordMove(movedBirds,movedFroms);
                    }
                }
                i = 0;
                //birdSelected.GetComponent<Bird>().MoveToNextTarget(transform.GetChild(positionsFilled));
            }

            steps = 0;
        }

        if (positionsFilled == 0)
        {

            //print("positionsFilled == 0 2");

            steps = 0;
            foreach (GameObject item in BirdsSortingController.instance.selectedbirds)
            {
                foreach (Transform t in item.GetComponent<Bird>().sittingPositionsHolders)
                {
                    Debug.Log("calling 2");
                    t.GetComponent<BirdSittingPositions>().selected = false;
                    if (positionsFilled >= 1 && gameObject.transform.GetChild(positionsFilled - 1).childCount > 0)
                    {
                        gameObject.transform.GetChild(positionsFilled - 1).GetChild(0).GetComponent<SkeletonAnimation>().AnimationName = "idle";
                        //selected = false; // New
                        Unselectallbrunch();
                    }
                }
            }

        }
    }
    private void Start()
    {
        lastTarget = BirdsSortingController.instance.lastTarget;
    }
    private void Update()
    {


        if (available)
        {


            if (!selected && positionsFilled != 0)
            {
                //foreach (GameObject item in selectedbirds)
                //{
                //    Debug.Log("item name " + item.name);
                //    item.transform.parent.localScale = new Vector3(1f, 1f, 1);
                //    Debug.Log("pos   name " + item.transform.parent.name);
                //}
                //transform.GetChild(positionsFilled - 1).
                //gameObject.transform.GetChild(positionsFilled - 1).GetChild(0).GetComponent<SkeletonAnimation>().AnimationName = "idle";
            }
            if (positionsFilled == transform.childCount && !sorted)
            {
                int matched = 0;
                if (transform.GetChild(positionsFilled - 1).childCount != 0)
                {
                    for (int i = 0; i < transform.childCount; i++)
                    {
                        // print(transform.GetChild(positionsFilled - 1).name);
                        if (positionsFilled >= 1 && transform.GetChild(i).childCount > 0 && transform.GetChild(positionsFilled - 1).childCount > 0)
                        {
                            if (transform.GetChild(i).GetChild(0).GetComponent<Bird>().birdClass == transform.GetChild(positionsFilled - 1).GetChild(0).GetComponent<Bird>().birdClass)
                            {
                                matched++;
                            }
                        }
                    }
                }
                if (matched == transform.childCount)
                {
                    SoundManager.instance.Play(SoundManager.instance.sortedSfx);
                    sorted = true;
                    GetComponent<Collider2D>().enabled = false;
                    BirdsSortingController.instance.birdsSorted++;

                }
            }
            if (sorted)
            {
                //transform.position += Vector3.up * 0.1f;
                // Trigger the tilt animation
                tiltbranch();
                StartCoroutine(FlyAfterSorted());
            }
        }
    }

    public void tiltbranch()
    {
        Animator parentAnimator = transform.parent.GetComponent<Animator>();
        if (parentAnimator != null)
        {
            parentAnimator.SetBool("tilt_now", true);
            StartCoroutine(ResetTiltAfterDelay(parentAnimator));
        }
    }
    private IEnumerator ResetTiltAfterDelay(Animator parentAnimator)
    {
        yield return new WaitForSeconds(0.25f);
        parentAnimator.SetBool("tilt_now", false);
    }

    IEnumerator FlyAfterSorted()
    {
        Debug.Log("sorted All");



        sorted = false;
        selected = false;
        Unselectallbrunch();
        available = false;
        foreach (Transform t in transform)
        {
            t.GetChild(0).GetComponent<Bird>().moveSpeed = 30.0f;
            t.GetChild(0).GetComponent<Bird>().MoveToNextTarget(lastTarget, true);
            yield return new WaitForSeconds(0.2f);
        }
        positionsFilled = 0;
        yield return new WaitForSeconds(0.2f);
        available = true;
        GetComponent<Collider2D>().enabled = true;
        BirdsSortingController.instance.levelcomplete();


    }

    public void moving()
    {

    }

    public void Unselectallbrunch()
    {


        GameObject[] allbrunches = GameObject.FindGameObjectsWithTag("branch");
        foreach (GameObject brunch in allbrunches)
        {
            brunch.GetComponent<BirdSittingPositions>().selected = false;

        }
    }

    public void Unselectallbirds()
    {

        GameObject[] allbirds = GameObject.FindGameObjectsWithTag("bird");
        foreach (GameObject item in allbirds)
        {
            item.GetComponent<Bird>().selecting = false;
            item.transform.parent.localScale = new Vector3(1f, 1f, 1);
            item.GetComponent<SkeletonAnimation>().AnimationName = "idle";

        }

        BirdsSortingController.instance.selectedbirds.Clear();

    }


    public void touchanim(GameObject selectedbird)
    {
        selectedbird.GetComponent<SkeletonAnimation>().AnimationName = "touching";
    }
    private BirdSittingPositions FindBirdSittingPositionByBrunchId(int brunchid)
    {
        // Find all BirdSittingPositions in the scene or within a specific parent object
        BirdSittingPositions[] allBirdPositions = FindObjectsOfType<BirdSittingPositions>();

        // Iterate through all found BirdSittingPositions and return the one that matches the brunchid
        foreach (BirdSittingPositions birdPos in allBirdPositions)
        {
            if (birdPos.brunchid == brunchid)
            {
                return birdPos;
            }
        }

        // Return null if no matching brunchid is found
        return null;
    }

    private void ShuffleBirds()
    {
        var birds = transform.GetComponentsInChildren<Bird>();
        if(birds.Length <= 1)
        {
            Debug.LogError("Not enough birds to shuffle");
            return;
        }
        var poses = birds.Select(bird=> bird.transform.parent).ToArray();
        var shuffled = poses.ToArray();
        int maxItrations = 50;
        for(int i=0; i<maxItrations; i++ )
        {
            shuffled.Shuffle();
            if(!poses.SequenceEqual(shuffled))
            {
                break;
            }
        }
        for(int i=0; i<birds.Length;i++)
        {
            birds[i].MoveToNextTarget(shuffled[i]);
        }
    }

    public void swapposBirds(int branchid)
    {
        // This method will only work for the specified branch
        if (branchid == brunchid)
        {
            // Find the children of this GameObject (assumed to be pos (1), pos (2), pos (3), and pos (4))
            Transform pos1 = transform.Find("pos (1)");
            Transform pos2 = transform.Find("pos (2)");
            Transform pos3 = transform.Find("pos (3)");
            Transform pos4 = transform.Find("pos (4)");

            if (pos1 != null && pos2 != null && pos3 != null && pos4 != null)
            {
                // Create a list of all positions
                List<Transform> positions = new List<Transform> { pos1, pos2, pos3, pos4 };

                // Find the positions that currently have birds (positions with child GameObjects)
                List<Transform> filledPositions = new List<Transform>();
                foreach (Transform pos in positions)
                {
                    if (pos.childCount > 0) // If the position has any child GameObject (bird)
                    {
                        filledPositions.Add(pos);
                    }
                }

                // If there are at least 2 filled positions, shuffle them
                if (filledPositions.Count > 1)
                {
                    // Perform a simple shuffle of filled positions
                    for (int i = 0; i < filledPositions.Count; i++)
                    {
                        // Randomly swap this position with any other filled position
                        int randomIndex = Random.Range(0, filledPositions.Count);

                        // Swap the birds between the positions
                        Vector3 tempPosition = filledPositions[i].position;
                        filledPositions[i].position = filledPositions[randomIndex].position;
                        filledPositions[randomIndex].position = tempPosition;
                    }

                    Debug.Log("Positions shuffled for brunchid " + branchid);
                }
                else
                {
                    Debug.Log("Not enough birds to shuffle.");
                }
            }
            else
            {
                Debug.LogError("Child positions not found!");
            }
        }
    }


}
