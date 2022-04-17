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

    public float checkCur = -1;
    public int checkLv = -1;

    private Animator anim;
    private GameObject Box;
    private GameObject text;

    // Start is called before the first frame update
    void Start()
    {

        text = GameObject.Find("UICanvas").GetComponent<UIProperties>().getBoxHint();

        Debug.Log("Curse = " + currentCurse);
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
        }
        else
        {
            curseBar.SetValue(currentCurse);
        }
        levelText.text = currCurseLv + 1 + "/" + curseLevel.Length;
    }


    void Update(){
        checkLv = currCurseLv;
        checkCur = currentCurse;
    }
    public void TakeCurseDamage(float curseDamage)
    {

        if(Box.activeSelf == false){
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
                    Mana.playerMana = -1;
                    Mana.playerMaxMana = -1;
                    OptionSkill.skillPlayerGlobal.Clear();
                    Health.playerHealth = -1;
                    Health.playerMaxHealth = -1;
                    GameManager.playerDead = true;
                    GameManager.whyDead = GameManager.CauseOfDeath.Curse;
                    anim.SetTrigger("die");
                    FindObjectOfType<AudioManager>().Play("PlayerDie");       
                    return;
                }
                else{
                    Box.SetActive(true);
                    text.SetActive(true);

                    
                }
                curseBar.SetMaxValue(curseLevel[currCurseLv]);
                levelText.text = currCurseLv + 1 + "/" + curseLevel.Length;
            }
            if(currentCurse <= -1)
            {
                currentCurse = -1;
            }

            curseBar.SetValue(currentCurse);
        }

    }

    public void resetCurse(){
        Debug.Log("Masuk Reset Curse");
        if(currCurseLv > 0){
        currentCurse = -1;
        currCurseLv = currCurseLv-1;
        levelText.text = currCurseLv + 1 + "/" + curseLevel.Length;
        curseBar.SetValue(currentCurse);
        }

    }

    public void ThisIsBox(GameObject box)
    {
        Box = box;
    }




}
