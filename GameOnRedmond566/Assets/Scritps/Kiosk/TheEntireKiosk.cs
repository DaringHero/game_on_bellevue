//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.SceneManagement;
//using UnityEngine.UI;

//public class TheEntireKiosk : MonoBehaviour
//{
//  ///////////////////////// UI
//  // Common
//  public Image Frame;
//  public Image Background;
//  public Button Next;
//  public Image FadedBG;

//  // Idle
//  public Image IntroImage;
//  public GameObject LoadingWheel;

//  // PlayerIn
//  public Image ShardImage;
//  public ItemPanel WheatPanel;
//  public ItemPanel IronPanel;
//  public ItemPanel WaterPanel;
//  public Text AreaName;
//  public Text SpecificName;
//  public Text QuestCompletedCounter;

//  // Quest
//  public QuestMenu questMenu;
//  public Text CurrentQuestText;


//  // Gather
//  public GatherItemPanel[] item;

//  // Challenge
//  public Text ChallengeText;
//  public Image ScaryBoi;

//  public Sprite ScaryBoiPurple;
//  public Sprite ScaryBoiGreen;
//  public Sprite ScaryBoiOrange;

//  public FlashFeedback flashFeedback;

//  // Quest Complete
//  public Text QuestCompleteText;

//  // New Quest
//  public Text NewQuestText;


//  ///////////////////////// Not UI
//  private bool allowNewPlayer = true;
//  private bool backOut = false;
//  private bool newActivePlayer = false;
//  private Player activePlayer;
//  private Location location;
//  private Challenge activeChallenge;
//  private Quest currentPlayersQuest;


//  public enum State
//  {
//    Idle,
//    PlayerIn,
//    Quest,
//    Gather,
//    Challenge,
//    QuestComplete,
//    NewQuest

//  }

//  public enum Step
//  {
//    Begin,
//    Update,
//    End
//  }

//  public State currentState;
//  public Step currentStep;

//  delegate void StepFunc();

//  private StepFunc[,] stateFunctions;

//  void IdleBegin()
//  {
//    IntroImage.gameObject.SetActive(true);
//    currentStep = Step.Update;
//  }

//  void IdleUpdate()
//  {
//    // If something gets tagged in.
//    if (newActivePlayer == true)
//    {
//      allowNewPlayer = false;
//      newActivePlayer = false;
//      activePlayer = GetPlayer();

//      if (activePlayer.registered)
//      {
//        currentStep = Step.End;
//      }
//    }

//    if(backOut)
//    {
//      SceneManager.LoadScene(SceneTransitioner.BootNoBittoys);
//    }
//  }

//  void IdleEnd()
//  {
//    IntroImage.gameObject.SetActive(false);

//    currentState = State.PlayerIn;
//    currentStep = Step.Begin;
//  }

//  void UpdateItemCounters()
//  {
//    WheatPanel.text = "" + activePlayer.getItemCount(Item.Wheat);
//    IronPanel.text = "" + activePlayer.getItemCount(Item.Iron);
//    WaterPanel.text = "" + activePlayer.getItemCount(Item.Water);

//    QuestCompletedCounter.text = "Quest Completed: " + activePlayer.completedQuestCounter;
//  }

//  void PlayerInBegin()
//  {
//    ShardImage.gameObject.SetActive(true);
//    WheatPanel.gameObject.SetActive(true);
//    IronPanel.gameObject.SetActive(true);
//    WaterPanel.gameObject.SetActive(true);
//    FadedBG.gameObject.SetActive(true);
//    QuestCompletedCounter.gameObject.SetActive(true);

//    UpdateItemCounters();

//    ShardImage.color = ShardManager.getColor(activePlayer.shardAffinity);

//    currentStep = Step.Update;
//  }

//  void PlayerInUpdate()
//  {
//    if (Questor.IsThereAChallenge(location, activePlayer) == true) //this function always returns true - intended?
//    {
//      activeChallenge = Questor.GetChallenge(location, activePlayer);

//      currentState = State.Challenge;
//      currentStep = Step.Begin;
//    }
//    else
//    {
//      currentState = State.Gather;
//      currentStep = Step.Begin;
//    }
//  }

//  void PlayerInEnd()
//  {
//    allowNewPlayer = true;
//    ShardImage.gameObject.SetActive(false);
//    WheatPanel.gameObject.SetActive(false);
//    IronPanel.gameObject.SetActive(false);
//    WaterPanel.gameObject.SetActive(false);
//    FadedBG.gameObject.SetActive(false);
//    QuestCompletedCounter.gameObject.SetActive(false);
//    activePlayer.syncPlayer();

//    currentState = State.Idle;
//    currentStep = Step.Begin;
//  }

//  void QuestBegin()
//  {
//    // Check Quest
//    questMenu.gameObject.SetActive(true);
//    Next.gameObject.SetActive(true);
//    CurrentQuestText.gameObject.SetActive(true);

//    CurrentQuestText.text = "Current Quest";

//    Quest q = QuestManager.Quests[activePlayer.activeQuest];
//    bool[] b = q.checkRequirements(activePlayer);
//    questMenu.setCurrentQuest(q, b);

//    currentStep = Step.Update;
//  }

//  void QuestUpdate()
//  {
//    if (backOut || nextHit)
//    {
//      currentStep = Step.End;
//    }
//  }

//  void QuestEnd()
//  {
//    questMenu.gameObject.SetActive(false);
//    Next.gameObject.SetActive(false);
//    CurrentQuestText.gameObject.SetActive(false);

//    currentState = State.PlayerIn;
//    currentStep = Step.End;
//  }

//  void GatherBegin()
//  {
//    Next.gameObject.SetActive(true);

//    List<Item> givenItems = Questor.GetItems(location, activePlayer);

//    int i = 0;
//    foreach (Item im in givenItems)
//    {
//      activePlayer.giveItem(im, 1);
//      item[i].gameObject.SetActive(true);
//      item[i].setItem(im);

//      ++i;
//    }
    
//    currentStep = Step.Update;
//  }

//  void GatherUpdate()
//  {
//    if (backOut || nextHit)
//    {
//      UpdateItemCounters();
//      currentStep = Step.End;
//    }
//  }

//  void GatherEnd()
//  {
    
//    foreach(GatherItemPanel gip in item)
//    {
//      gip.gameObject.SetActive(false);
//    }

//    Next.gameObject.SetActive(false);

//    currentState = State.QuestComplete;
//    currentStep = Step.Begin;
//  }

//  void ChallengeBegin()
//  {
//    allowNewPlayer = true;
//    ChallengeText.gameObject.SetActive(true);
//    ScaryBoi.gameObject.SetActive(true);

//    ShardColor challengeColor = activeChallenge.getChallengeColor();
//    ChallengeText.text = "A challenge appears of " + ShardManager.getName(challengeColor) + " light!";

//    switch(challengeColor)
//    {
//      case ShardColor.Purple: ScaryBoi.sprite = ScaryBoiPurple; break;
//      case ShardColor.Green: ScaryBoi.sprite = ScaryBoiGreen; break;
//      case ShardColor.Orange: ScaryBoi.sprite = ScaryBoiOrange; break;
//    }

//    currentStep = Step.Update;
//  }

//  bool challengeCompleted = false;
//  bool feedbackAnimation = false;
//  Color feedbackTo;

//  void ChallengeUpdate()
//  {
//    if(feedbackAnimation)
//    {
//      if(flashFeedback.update(Time.deltaTime, feedbackTo))
//      {
//        feedbackAnimation = false;
//        if(challengeCompleted)
//        {
//          challengeCompleted = false;
//          currentStep = Step.End;
//        }
//      }
//    }

//    if (newActivePlayer == true) // Someone else scanned in!
//    {
//      allowNewPlayer = false;
//      newActivePlayer = false;
//      Player supportPlayer = GetPlayer();

//      Debug.Log(supportPlayer.shardAffinity);

//      if (activeChallenge.getChallengeColor() == ShardManager.Add(supportPlayer.shardAffinity, activePlayer.shardAffinity))
//      {
//        // Mix is effective, reward players
//        feedbackAnimation = true;
//        feedbackTo = flashFeedback.affirmative;
//        challengeCompleted = true;
//      }
//      else
//      {
//        // Mix is not effective, try again
//        feedbackAnimation = true;
//        feedbackTo = flashFeedback.denial;
//        allowNewPlayer = true;
//      }
//    }
//  }

//  void ChallengeEnd()
//  {
//    ChallengeText.gameObject.SetActive(false);
//    ScaryBoi.gameObject.SetActive(false);

//    currentState = State.Gather;
//    currentStep = Step.Begin;
//  }

//  void QuestCompleteBegin()
//  {
//    if (activePlayer.activeQuest != -1) // If player doesn't have an active quest
//    {
//      // Get active quest
//      int questID = activePlayer.activeQuest;
//      currentPlayersQuest = QuestManager.Quests[questID];

//      bool[] status = currentPlayersQuest.checkRequirements(activePlayer);

//      // Check if quest is complete
//      bool complete = true;
//      foreach (bool b in status)
//      {
//        if (b == false)
//        {
//          complete = false;
//          break;
//        }
//      }

//      if (complete)
//      {
//        // Display, Quest Complete!
//        QuestCompleteText.gameObject.SetActive(true);
//        Next.gameObject.SetActive(true);
//        questMenu.gameObject.SetActive(true);

//        QuestCompleteText.text = "Quest complete!";

//        questMenu.setCurrentQuest(currentPlayersQuest, status);

//        // Take quest items
//        foreach (QuestRequirement req in currentPlayersQuest.requirements)
//        {
//          activePlayer.takeItem(req.item, req.count);
//        }

//        activePlayer.completedQuestCounter += 1;
        
//        currentStep = Step.Update;
//      }
//      else
//      {
//        currentState = State.Quest;
//        currentStep = Step.Begin;
//      }
//    }
//    else
//    {
//      currentState = State.NewQuest;
//      currentStep = Step.Begin;
//    }
//  }

//  void QuestCompleteUpdate()
//  {
//    if(nextHit)
//    {
//      UpdateItemCounters();

//      currentStep = Step.End;
//    }
//  }

//  void QuestCompleteEnd()
//  {
//    QuestCompleteText.gameObject.SetActive(false);
//    Next.gameObject.SetActive(false);
//    questMenu.gameObject.SetActive(false);

//    currentState = State.NewQuest;
//    currentStep = Step.Begin;
//  }


//  void NewQuestBegin()
//  {
//    NewQuestText.gameObject.SetActive(true);
//    questMenu.gameObject.SetActive(true);
//    Next.gameObject.SetActive(true);

//    NewQuestText.text = "New Quest!";

//    Quest newQuest = Questor.GetQuest(location);
//    questMenu.setCurrentQuest(newQuest, newQuest.checkRequirements(activePlayer));
//    activePlayer.activeQuest = newQuest.ID;

//    currentStep = Step.Update;
//  }

//  void NewQuestUpdate()
//  {
//    if(nextHit)

//    {
//      currentStep = Step.End;
//    }
//  }

//  void NewQuestEnd()
//  {
//    NewQuestText.gameObject.SetActive(false);
//    questMenu.gameObject.SetActive(false);
//    Next.gameObject.SetActive(false);

//    currentState = State.PlayerIn;
//    currentStep = Step.End;

//  }

//  void SetupStateFunctions()
//  {
//    stateFunctions = new StepFunc[7, 3];

//    stateFunctions[0, 0] = IdleBegin;
//    stateFunctions[0, 1] = IdleUpdate;
//    stateFunctions[0, 2] = IdleEnd;

//    stateFunctions[1, 0] = PlayerInBegin;
//    stateFunctions[1, 1] = PlayerInUpdate;
//    stateFunctions[1, 2] = PlayerInEnd;

//    stateFunctions[2, 0] = QuestBegin;
//    stateFunctions[2, 1] = QuestUpdate;
//    stateFunctions[2, 2] = QuestEnd;

//    stateFunctions[3, 0] = GatherBegin;
//    stateFunctions[3, 1] = GatherUpdate;
//    stateFunctions[3, 2] = GatherEnd;

//    stateFunctions[4, 0] = ChallengeBegin;
//    stateFunctions[4, 1] = ChallengeUpdate;
//    stateFunctions[4, 2] = ChallengeEnd;

//    stateFunctions[5, 0] = QuestCompleteBegin;
//    stateFunctions[5, 1] = QuestCompleteUpdate;
//    stateFunctions[5, 2] = QuestCompleteEnd;

//    stateFunctions[6, 0] = NewQuestBegin;
//    stateFunctions[6, 1] = NewQuestUpdate;
//    stateFunctions[6, 2] = NewQuestEnd;
//  }


//  //////////////////// NOT STATE MACHINE
//  void Start ()
//  {
//    SetupStateFunctions();
//    currentState = State.Idle;
//    currentStep = Step.Begin;

//    location = LocationManager.Locations[SceneTransitioner.kioskLocation];

//    AreaName.text = location.areaName;
//    SpecificName.text = location.specificName;
//    Frame.color = location.frameColor;
//    Background.sprite = location.background;

//    BitToysWrapper.inst.registerOnClaimToy(OnScan);
//    BitToysWrapper.inst.registerOnDetectToy(OnDetect);    
//    Next.onClick.AddListener(OnNext);
//  }

//  bool nextHit = false;

//  void OnNext()
//  {
//    nextHit = true;
//  }

//  void OnDetect(string text)
//  {
//    if (allowNewPlayer)
//    {
//      LoadingWheel.gameObject.SetActive(true); //is the loading wheel supposed to spin??
//    }
//  }

//  private Player playerStorage;

//  void OnScan(ToyWrapper toy, bool TLL)
//  {
//    if (allowNewPlayer)
//    {
//      LoadingWheel.gameObject.SetActive(false);
//      playerStorage = new Player(toy);
//      newActivePlayer = true;
//    }
//  }

//  Player GetPlayer()
//  {
//    return playerStorage;
//  }

//  // Update is called once per frame
//  void Update()
//  {
//    State stateBefore;
//    Step stepBefore;


//    if (Input.GetKeyDown(KeyCode.Escape))
//    {
//      backOut = true;
//    }

//    // Essentially: If we change states then there's still stuff to do, potentially lots of stuff,
//    // if our state is the same then we're waiting for something and we can let the engine resume.
//    do
//    {
//      stateBefore = currentState;
//      stepBefore = currentStep;
//      stateFunctions[(int)currentState, (int)currentStep]();
//      backOut = false;
//      nextHit = false;

//    } while (stateBefore != currentState || stepBefore != currentStep);
//  }
//}
