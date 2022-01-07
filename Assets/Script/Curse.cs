using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Curse : MonoBehaviour
{
    [Header("Curse Bar")]
	public Bar curseBar;
    [SerializeField] private Text levelText;
	[SerializeField] private float[] curseLevel = {100};
    private float currentCurse;
    private int currCurseLv;
    public bool dead = false;

    // Start is called before the first frame update
    void Start()
    {
        currCurseLv = 0;
        curseBar.SetMaxValue(curseLevel[currCurseLv]);
        curseBar.SetValue(0);
        currentCurse = 0;
        levelText.text = "Lv" + currCurseLv;
    }

    public void TakeCurseDamage(float curseDamage)
    {
        if(dead)
        {
            return;
        }

        currentCurse += curseDamage;
        
        if(currentCurse >= curseLevel[currCurseLv])
        {
            currentCurse -= curseLevel[currCurseLv];
            currCurseLv++;
            if(currCurseLv == curseLevel.Length)
            {
                dead = true;
                return;
            }
            curseBar.SetMaxValue(curseLevel[currCurseLv]);
            levelText.text = "Lv" + currCurseLv;
        }

        curseBar.SetValue(currentCurse);
    }
}
