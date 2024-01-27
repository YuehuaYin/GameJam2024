using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timerBar : MonoBehaviour
{

    public void updateTimerBar(float current, float max)
    {
        float fillAmount = 1 - ((float)current / (float)max);
        GetComponent<Image>().fillAmount = fillAmount;

        Gradient g = new Gradient();
        GradientColorKey[] gck = new GradientColorKey[2];
        GradientAlphaKey[] gak = new GradientAlphaKey[2];
        gck[1].color = Color.green;
        gck[1].time = 1.0F;
        gck[0].color = Color.red;
        gck[0].time = -1.0F;
        gak[1].alpha = 1.0F;
        gak[1].time = 1.0F;
        gak[0].alpha = 1.0F;
        gak[0].time = -1.0F;
        g.SetKeys(gck, gak);
        GetComponent<Image>().color = g.Evaluate(fillAmount);
    }
}
