using UnityEngine;
using System.Collections;

public class Allegiance : MonoBehaviour {


    public void SetAllegiance(int choice)
    {
        switch (choice)
        {
            case 0:
                PlayerPrefs.SetString("Vote", "Trump");
                break;
            case 1:
                PlayerPrefs.SetString("Vote", "Clinton");
                break;
            default:
                PlayerPrefs.SetString("Vote", "NA");
                break;
        }
    }

}
