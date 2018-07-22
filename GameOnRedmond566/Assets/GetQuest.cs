using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetQuest : MonoBehaviour
{
    public float waittime = 3.0f;

    public GameObject Activate;
    public GameObject Deactivate;

    public YellOnClaim myYellOnClaim;

    public List<string> QuestStrings;//0.SANC, 1.SWAMP, 2.MOUNTAIN, 3.FOREST, 4.LAKE, 5.WIND //only 4 for now

    public Text DisplayText;
    public int NumberOfQuestableAreas; //this and below must match
    public List<int> QuestWeights;

    public List<InputField> inputWeights;

    public int GetRandomWeightedQuest()
    {
        //    int[] weights = QuestWeights.ToArray();
        int[] weights = new int[NumberOfQuestableAreas];
        int gg = 0;
        foreach(InputField g in inputWeights)
        {
            weights[gg] = System.Int32.Parse(g.text);
            ++gg;
        }

        // Get the total sum of all the weights.
        int weightSum = 0;
        foreach(int i in weights)
        {
            weightSum += i;
        }

        // Step through all the possibilities, one by one, checking to see if each one is selected.
        int index = 0;
        int lastIndex = NumberOfQuestableAreas - 1;
        while (index < lastIndex)
        {
            // Do a probability check with a likelihood of weights[index] / weightSum.
            if (UnityEngine.Random.Range(0, weightSum) < weights[index])
            {
                return index;
            }

            // Remove the last item from the sum of total untested weights and try again.
            weightSum -= weights[index++];
        }

        // No other item was selected, so return very last index.
        return lastIndex;
    }


    private void OnEnable()
    {
        //  int nextQuest = Random.Range(1, QuestStrings.Count);
        int nextQuest = GetRandomWeightedQuest();

        this.myYellOnClaim.MyCurrentToy.customData.SetInt("CurrentQuest", nextQuest);

        this.DisplayText.text = this.QuestStrings[nextQuest];

        Debug.Log("Quest = "+ ((YellOnClaim.Location)nextQuest).ToString());
        UniClipboard.SetText(UniClipboard.GetText() + "\n" + System.DateTime.Now + " " + "Quest is " + ((YellOnClaim.Location)nextQuest).ToString());
        StartCoroutine(WaitAndThenActivate());

    }

    IEnumerator WaitAndThenActivate()
    {
        yield return new WaitForSeconds(waittime);

        this.Activate.SetActive(true);
        this.Deactivate.SetActive(false);
    }
}
