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
    public static float currentCurse = -1;
    public static int currCurseLv = -1;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        if(currCurseLv == -1)
        {
            currCurseLv = 0;
        }
        curseBar.SetMaxValue(curseLevel[currCurseLv]);
        if(currentCurse  == -1)
        {
            curseBar.SetValue(0);
            currentCurse = 0;
        }else
        {
            curseBar.SetValue(currentCurse);
        }
        levelText.text = "Lv" + currCurseLv;
    }

    public void TakeCurseDamage(float curseDamage)
    {
        if(GameManager.playerDead)
        {
            return;
        }

        currentCurse += curseDamage;

        if(currCurseLv != -1 && currentCurse >= curseLevel[currCurseLv])
        {
            currentCurse -= curseLevel[currCurseLv];
            currCurseLv++;
            if(currCurseLv == curseLevel.Length)
            {
                this.GetComponent<PlayerMovement>().enabled = false;
                this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                curseBar.SetMaxValue(curseLevel[currCurseLv-1]);
                levelText.text = "Lv" + currCurseLv;
                currentCurse = -1;
                currCurseLv = -1;
                Health.playerHealth = -1;
                Health.playerMaxHealth = -1;
                GameManager.playerDead = true;
                anim.SetTrigger("die");       
                return;
            }
            curseBar.SetMaxValue(curseLevel[currCurseLv]);
            levelText.text = "Lv" + currCurseLv;
        }
        if(currentCurse <= -1)
        {
            currentCurse = -1;
        }

        curseBar.SetValue(currentCurse);
    }
}
