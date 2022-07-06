using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Counter : MonoBehaviour, ICounter
{
    [SerializeField] private TMP_Text tmpText;

    private float _score;

    public void IncreaseScore()
    {
        _score++;
        tmpText.text = _score.ToString();
    }
}
