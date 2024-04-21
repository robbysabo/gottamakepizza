using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateRatingText : MonoBehaviour
{
    private Vector3 startPos = new Vector3(-31f, 43f);
    private Vector3 endPos = new Vector3(-31f, 63f);
    private bool isStarted = false;
    private float waitUntilEnd = 3f;
    private float timeLeft = 1f;

    private Color greenColor = new Color(0.2471f, 0.6f, 0.2471f, 1f);
    private Color redColor = new Color(0.7686f, 0.149f, 0.1569f, 1f);

    private TextMeshProUGUI txt;

    void Awake()
    {
        txt = GetComponent<TextMeshProUGUI>();
    }

    private void Animate()
    {
        if (timeLeft <= 0.0f)
        {
            timeLeft = 0.0f;
            Destroy(gameObject);
        }
        transform.localPosition = Vector3.Lerp(endPos, startPos, timeLeft / waitUntilEnd);
        Color clr = txt.color;
        clr.a = timeLeft / waitUntilEnd;
        txt.color = clr;
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (isStarted)
        {
            Animate();
        }
        else
        {
            if (timeLeft <= 0.0f)
            {
                timeLeft = waitUntilEnd;
                isStarted = true;
            }
        }
    }

    public void SetupMaxText()
    {
        txt.color = greenColor;
        txt.text = "MAX";
    }

    public void SetupText(float amount)
    {
        // color
        if (amount <= 0.0f)
        {
            txt.color = redColor;
        }
        else
        {
            txt.color = greenColor;
        }

        float snappedAmount = Snapping.Snap(amount, 0.01f);
        string fixedAmount = GameObject.Find("main").GetComponent<Main>().FixDecimal(snappedAmount.ToString());
        string newTxt = "";
        if (snappedAmount > 0.0f)
        {
            newTxt += "+";
        }
        newTxt += fixedAmount;
        txt.text = newTxt;
    }
}
