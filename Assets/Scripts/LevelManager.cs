
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using dotmob;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }
    public static event Action LevelCompleted;

    [SerializeField] private float _minXDistanceBetweenHolders;
    [SerializeField] private Camera _camera;
    [SerializeField] private Holder _holderPrefab;
    [SerializeField] private Holder _holderPrefab_even;

    [SerializeField] private GameObject _brunchPrefab_right;

    [SerializeField] private GameObject _brunchPrefab_left;

    [SerializeField] private GameObject[] birdprefablist;

    List<GameObject> brunchlist = new List<GameObject>();

    [SerializeField] private Transform birdinstancelocator;

    [SerializeField] private AudioClip _winClip;

    [SerializeField] private GameObject birdcontrollerscript;

    public int numberofbrunches;

    public int maxnumberbrunchadd;

    private int brunchadded;

    public GameMode GameMode { get; private set; } = GameMode.Easy;
    public Level Level { get; private set; }

    private readonly List<Holder> _holders = new List<Holder>();

    private readonly Stack<MoveData> _undoStack = new Stack<MoveData>();

    public State CurrentState { get; private set; } = State.None;

    public bool HaveUndo => _undoStack.Count > 0;

    public bool IsTransfer { get; set; }

    
    private void Awake()
    {
        Instance = this;
        var loadGameData = GameManager.LoadGameData;
        GameMode = loadGameData.GameMode;
        Level = loadGameData.Level;
        brunchadded=0;
        LoadLevel();
        CurrentState = State.Playing;
    }


    // New method Done by Albert
    private void LoadLevel()
    {
        numberofbrunches = Level.map.Count;
        var list = PositionsForHolders(numberofbrunches, out var width).ToList();
        _camera.orthographicSize = 0.5f * width * Screen.height / Screen.width;

        var levelMap = Level.LiquidDataMap;
        var hasSet = new HashSet<int>();

        for (var i = 0; i < levelMap.Count; i++)
        {
            var levelColumn = levelMap[i];

            // Instantiate the branch (left or right)
            GameObject brunch;
            if (i < Mathf.CeilToInt(Level.map.Count / 2f))
            {
                brunch = Instantiate(_brunchPrefab_left, list[i], Quaternion.identity);
            }
            else
            {
                brunch = Instantiate(_brunchPrefab_right, list[i], Quaternion.identity);
            }

            brunch.transform.GetChild(0).GetComponent<BirdSittingPositions>().brunchid = i + 1;
            brunchlist.Add(brunch);

            // Start the coroutine to instantiate birds for this branch
            StartCoroutine(InstantiateBirdsForBranch(brunch, levelColumn, hasSet));
        }

        birdcontrollerscript.GetComponent<BirdsSortingController>().birdTypes = hasSet.Count;
    }

    private IEnumerator InstantiateBirdsForBranch(GameObject brunch, IEnumerable<LiquidData> birdsinbrnch, HashSet<int> hasSet)
    {
        bool isLeftBranch = brunch.transform.position.x < 0;

        foreach (var birdData in birdsinbrnch)
        {
            for (var k = 0; k < birdData.value; k++)
            {
                Vector3 offScreenPosition = isLeftBranch
                    ? new Vector3(-Screen.width * 0.15f - 1f, brunch.transform.position.y, brunch.transform.position.z)
                    : new Vector3(Screen.width * 0.15f + 1f, brunch.transform.position.y, brunch.transform.position.z);

                var birdInstance = Instantiate(birdprefablist[birdData.groupId], offScreenPosition, Quaternion.identity);
                birdInstance.GetComponent<Bird>().target = brunch.transform.GetChild(0).GetChild(brunch.transform.GetChild(0).GetComponent<BirdSittingPositions>().positionsFilled);
                brunch.transform.GetChild(0).GetComponent<BirdSittingPositions>().positionsFilled++;
                birdInstance.GetComponent<Bird>().startisfinish = true;
                hasSet.Add(birdData.groupId);

                Vector3 endPos = birdInstance.GetComponent<Bird>().target.position;
                Vector3 startPos = endPos + new Vector3(isLeftBranch ? -5f : 2f, 2f, 0);

                // Pass the brunch object to the coroutine
                StartCoroutine(MoveBirdToPosition(birdInstance.transform, startPos, endPos, brunch));
            }
        }

        yield return null;
    }

    private IEnumerator MoveBirdToPosition(Transform bird, Vector3 startPos, Vector3 endPos, GameObject brunch)
    {
        float journeyTime = 1.5f;
        float elapsedTime = 0f;

        while (elapsedTime < journeyTime)
        {
            float t = elapsedTime / journeyTime;
            float smoothT = Mathf.SmoothStep(0f, 1f, t);
            Vector3 smoothPos = Vector3.Lerp(startPos, endPos, smoothT) + new Vector3(0, Mathf.Sin(smoothT * Mathf.PI) * 2f, 0);

            bird.position = smoothPos;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        bird.position = endPos;

        // Call the tilbranch() method on the brunch object
        brunch.transform.GetChild(0).GetComponent<BirdSittingPositions>().tiltbranch();
    }



    //private void LoadLevel()
    //{
    //    //print("Level map"+Level.map.Count);
    //    //print(Screen.height);
    //    //print(Screen.width);
    //    numberofbrunches =Level.map.Count;
    //    var list = PositionsForHolders(numberofbrunches, out var width).ToList();
    //    //print(width);
    //    _camera.orthographicSize = 0.5f * width * Screen.height / Screen.width;

    //    //_camera.orthographicSize = 0.5f  * width * Screen.width / Screen.height;



    //    var levelMap = Level.LiquidDataMap;
    //    var hasSet = new HashSet<int>();
    //    for (var i = 0; i < levelMap.Count; i++)
    //    {
    //        var levelColumn = levelMap[i];

    //        if(i < Mathf.CeilToInt(Level.map.Count / 2f)){
    //        var brunch = Instantiate(_brunchPrefab_left, list[i], Quaternion.identity);
    //        brunch.transform.GetChild(0).GetComponent<BirdSittingPositions>().brunchid = i + 1;
    //            var birdsinbrnch = levelColumn.ToList();

    //        brunchlist.Add(brunch);

    //        for(var j=0;j<birdsinbrnch.Count;j++){
    //            for(var k=0;k<birdsinbrnch[j].value;k++){




    //            var bird_intstance = Instantiate(birdprefablist[birdsinbrnch[j].groupId],brunch.transform.GetChild(0).GetChild(brunch.transform.GetChild(0).transform.GetComponent<BirdSittingPositions>().positionsFilled));
    //            bird_intstance.transform.GetComponent<Bird>().target=bird_intstance.transform;
    //            brunch.transform.GetChild(0).transform.GetComponent<BirdSittingPositions>().positionsFilled++;
    //            bird_intstance.transform.GetComponent<Bird>().startisfinish=true;



    //            hasSet.Add(birdsinbrnch[j].groupId);
    //            }

    //        }


    //        //holder.Init(levelColumn,true);
    //        //_holders.Add(holder);
    //        }
    //        else{
    //        var brunch = Instantiate(_brunchPrefab_right, list[i], Quaternion.identity);
    //            brunch.transform.GetChild(0).GetComponent<BirdSittingPositions>().brunchid = i + 1;
    //            var birdsinbrnch = levelColumn.ToList();

    //        brunchlist.Add(brunch);




    //        for(var j=0;j<birdsinbrnch.Count;j++){
    //            for(var k=0;k<birdsinbrnch[j].value;k++){

    //            var bird_intstance = Instantiate(birdprefablist[birdsinbrnch[j].groupId],brunch.transform.GetChild(0).GetChild(brunch.transform.GetChild(0).transform.GetComponent<BirdSittingPositions>().positionsFilled));
    //            bird_intstance.transform.GetComponent<Bird>().target=bird_intstance.transform;
    //            brunch.transform.GetChild(0).transform.GetComponent<BirdSittingPositions>().positionsFilled++;
    //            bird_intstance.transform.GetComponent<Bird>().startisfinish=true;


    //            hasSet.Add(birdsinbrnch[j].groupId);
    //            }

    //        }
    //        //holder.Init(levelColumn,true);
    //        //_holders.Add(holder);

    //        }


    //    }


    //   birdcontrollerscript.GetComponent<BirdsSortingController>().birdTypes=hasSet.Count;

    //}





    public void Addemptyholder(){

        //print("Number of holder"+_holders.Count);

        if(brunchadded<maxnumberbrunchadd){

        numberofbrunches=numberofbrunches+1;

        

        //var list = PositionsForHolders(numberofbrunches, out var width).ToList();
        //_camera.orthographicSize = 0.5f * width * Screen.height / Screen.width;           
            
            if(Mathf.CeilToInt(brunchlist.Count / 2f)%2 == 0)
            {
            var brunch = Instantiate(_brunchPrefab_right, brunchlist[brunchlist.Count-1].transform.position, Quaternion.identity);
            
            brunch.transform.position=brunch.transform.position +_minXDistanceBetweenHolders * Vector3.up;
                brunch.transform.GetChild(0).GetComponent<BirdSittingPositions>().brunchid = brunchlist.Count + 1;
                brunchlist.Add(brunch);
            }
            else{

                var brunch = Instantiate(_brunchPrefab_right, brunchlist[brunchlist.Count-1].transform.position, Quaternion.identity);
            
                brunch.transform.position=brunch.transform.position +_minXDistanceBetweenHolders * Vector3.up;
                brunch.transform.GetChild(0).GetComponent<BirdSittingPositions>().brunchid = brunchlist.Count + 1;
                brunchlist.Add(brunch);

            }

            brunchadded++;

        }

           

        

        
        
          /*  //var levelColumn = levelMap[i];
            for (var i = 0; i < _holders.Count; i++)
        {
            _holders[i].transform.position=list[i];
            _holders[i].Init(Level.LiquidDataMap[0],false);


        }

        //print("Len of list"+list.Count);
            
            var holder = Instantiate(_holderPrefab, list[_holders.Count], Quaternion.identity);
            holder.Init(Level.LiquidDataMap[0],false);
            _holders.Add(holder);
            //print("Number of holder"+_holders.Count);*/
        

    }

    public void OnClickUndo()
    {
       //Debug.Log("UNDO :" + _undoStack.Count);
       //Debug.Log("State :" + CurrentState);
        if (CurrentState != State.Playing || _undoStack.Count <= 0)
            return;

        var moveData = _undoStack.Pop();
        //print(moveData.ToHolder);
        //print(moveData.FromHolder);
        //print(moveData.Liquid1.Value);
        Undomove(moveData.ToHolder, moveData.FromHolder, moveData.Liquid1,moveData.Liquidvalue);
    }

    private void Update()
    {
        /*if (CurrentState != State.Playing)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            var collider = Physics2D.OverlapPoint(_camera.ScreenToWorldPoint(Input.mousePosition));
            if (collider != null)
            {
                var holder = collider.GetComponent<Holder>();

                if (holder != null)
                    OnClickHolder(holder);
            }
        }*/
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void OnClickHolder(Holder holder)
    {
        //if(IsTransfer)
          //  return;

          if((holder.Liquids.ToList().Count == 1) && (holder.CurrentTotal == holder.MAXValue ))
          return;

          

          

        
            
        var pendingHolder = _holders.FirstOrDefault(h => h.IsPending);
        

        if (pendingHolder != null && pendingHolder != holder)
        {
            
            if (holder.TopLiquid == null ||
                (pendingHolder.TopLiquid.GroupId == holder.TopLiquid.GroupId && !holder.IsFull ))
            {
                

                _undoStack.Push(new MoveData
                {
                    FromHolder = pendingHolder,
                    ToHolder = holder,
                    Liquid1 = pendingHolder.TopLiquid,
                    Liquidvalue = pendingHolder.TopLiquid.Value
                    

                });
                //print(_undoStack.All(l=>Liquid));
                //MoveBallFromOneToAnother(pendingHolder, holder);


                IsTransfer = true;
                StartCoroutine(SimpleCoroutine.CoroutineEnumerator(pendingHolder.MoveAndTransferLiquid(holder, CheckAndGameOver),()=>
                {
                    
                    IsTransfer = false;
                 
                }));
            }
            else
            {
                pendingHolder.ClearPending();
                holder.StartPending();
            }
        }
        else if (holder.Liquids.Any())
        {
            if (!holder.IsPending)
                holder.StartPending();
            else
            {
                holder.ClearPending();
            }
        }
    }



    private void MoveBallFromOneToAnother(Holder fromHolder, Holder toHolder)
    {
        toHolder.Move(fromHolder.RemoveTopBall());
        //bottlefull(toHolder);
        CheckAndGameOver();
    }


    private void Undomove(Holder fromHolder, Holder toHolder, Liquid transferliquid, float liquidvalue)
    {

        //print(transferliquid.Value);

        //fromHolder.MoveAndTransferLiquid(toHolder, CheckAndGameOver);
        StartCoroutine(SimpleCoroutine.CoroutineEnumerator(fromHolder.UndoMoveAndTransferLiquid(toHolder,liquidvalue)));

        //print("Exit function");
        
                //StartCoroutine(SimpleCoroutine.CoroutineEnumerator(fromHolder.MoveAndTransferLiquid(toHolder, CheckAndGameOver),()=>
                //{                 
                //}));

        //CheckAndGameOver();
    }






    private void CheckAndGameOver()
    {

    



        /*if (
            _holders.All(holder =>
        {
            var liquids = holder.Liquids.ToList();
            Debug.Log(_holders.Count);
            Debug.Log(liquids.Count);
            return liquids.Count == 0 || liquids.Count == 1;
        }) &&
        _holders.Where(holder => holder.Liquids.Any()).GroupBy(holder => holder.Liquids.First().GroupId)
            .All(holders => holders.Count() == 1))*/


            if (
            _holders.All(holder =>
        {
            var liquids = holder.Liquids.ToList();
            //Debug.Log(_holders.Count);
            //Debug.Log(liquids.Count);
            return (liquids.Count == 0) || (liquids.Count == 1 && (holder.CurrentTotal == holder.MAXValue ));
        }))
        {
            //Debug.Log("Xong 1 chai");
            OverTheGame();
        }
    }

    public void OverTheGame()
    {
        if (CurrentState != State.Playing)
            return;

        PlayClipIfCan(_winClip);
        CurrentState = State.Over;

        ResourceManager.CompleteLevel(GameMode, Level.no);
        LevelCompleted?.Invoke();
    }

    private void PlayClipIfCan(AudioClip clip, float volume = 0.35f)
    {
        if (!AudioManager.IsSoundEnable || clip == null)
            return;
        AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, volume);
    }

    public IEnumerable<Vector2> PositionsForHolders(int count, out float expectWidth)
    {
        expectWidth = 5 * _minXDistanceBetweenHolders;
        /*if (count <= 7)
        {
            var minPoint = transform.position - ((count - 1) / 2f) * _minXDistanceBetweenHolders * Vector3.up -
                           Vector3.right * 1f;

            expectWidth = Mathf.Max(count * _minXDistanceBetweenHolders, expectWidth);

            return Enumerable.Range(0, count)
                .Select(i => (Vector2) minPoint + i * _minXDistanceBetweenHolders * Vector2.up);
        }*/

        var aspect = (float) Screen.width / Screen.height;

        var maxCountInRow = Mathf.CeilToInt(count / 2f);

        if ((maxCountInRow + 1) * _minXDistanceBetweenHolders > expectWidth)
        {
            expectWidth = (maxCountInRow+1) * _minXDistanceBetweenHolders;
            //expectWidth = 6 * _minXDistanceBetweenHolders;
        }
        
        

        var height = expectWidth / aspect;

        //print("height="+height);
        //print("aspect="+aspect);
        //print("1/aspect="+1/aspect);
        //print("expectWidth="+expectWidth);

        var height1=4.6f;
        var height2=5.0f;

        if(maxCountInRow>=1&&maxCountInRow<=4){

            height1=3.4f;
            height2=4.0f;

            //height1=height1+((maxCountInRow)*1.2f);
            //height2=height2+((maxCountInRow)*1.2f);

        }

        if(maxCountInRow>4&&maxCountInRow<=5){

            height1=4.6f;
            height2=5.0f;           

        }

        

        if(maxCountInRow>5){
            height1=4.6f;
            height2=5.0f;

            height1=height1+((maxCountInRow - 5)*1.2f);
            height2=height2+((maxCountInRow - 5)*1.2f);

        }
        

        


        var list = new List<Vector2>();
        //var topRowMinPoint = transform.position - Vector3.up * height / 15f -
                             //((maxCountInRow - 1) / 2f) * _minXDistanceBetweenHolders * Vector3.up -
                             //(Vector3.right * 5f);

                             var topRowMinPoint = transform.position  -
                             ((maxCountInRow - 1) / 2f) * _minXDistanceBetweenHolders * Vector3.up -
                             (Vector3.right * height1);
        list.AddRange(Enumerable.Range(0, maxCountInRow)
            .Select(i => (Vector2) topRowMinPoint + i * _minXDistanceBetweenHolders * Vector2.up));

        var lowRowMinPoint = transform.position   -
                             (((count - maxCountInRow) - 1) / 2f) * _minXDistanceBetweenHolders * Vector3.up +
                             (Vector3.right * height2);
        list.AddRange(Enumerable.Range(0, count - maxCountInRow)
            .Select(i => (Vector2) lowRowMinPoint + i * _minXDistanceBetweenHolders * Vector2.up));


        
        return list;
    }


    public enum State
    {
        None,
        Playing,
        Over
    }

    public struct MoveData
    {
        public Holder FromHolder { get; set; }
        public Holder ToHolder { get; set; }
        public Liquid Liquid1 { get; set; }
        public float Liquidvalue { get; set; }
    }
}

[Serializable]
public struct LevelGroup : IEnumerable<Level>
{
    public List<Level> levels;

    public IEnumerator<Level> GetEnumerator()
    {
        return levels?.GetEnumerator() ?? Enumerable.Empty<Level>().GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

[Serializable]
public struct Level
{
    public int no;
    public List<LevelColumn> map;

    public List<IEnumerable<LiquidData>> LiquidDataMap => map.Select(GetLiquidDatas).ToList();

    public static IEnumerable<LiquidData> GetLiquidDatas(LevelColumn column)
    {
        var list = column.ToList();

        for (var j = 0; j < list.Count; j++)
        {
            var currentGroup = list[j];
            var count = 0;
            for (; j < list.Count; j++)
            {
                if (currentGroup == list[j])
                {
                    count++;
                }
                else
                {
                    j--;
                    break;
                }
            }

            yield return new LiquidData
            {
                groupId = currentGroup,
                value = count
            };
        }
    }
}

public struct LiquidData
{
    public int groupId;
    public float value;
}

[Serializable]
public struct LevelColumn : IEnumerable<int>
{
    public List<int> values;

    public IEnumerator<int> GetEnumerator()
    {
        return values?.GetEnumerator() ?? Enumerable.Empty<int>().GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}