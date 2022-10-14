using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class FadeOutText : MonoBehaviour
{
    [SerializeField] private float timer = 0.3f;
    [SerializeField] private Color defaultColor;
    public int damage;
    private Transform player;
    private TextMeshPro tmp;
    private void Start()
    {
        player = GameObject.Find("Player").transform;
        tmp = GetComponentInChildren<TextMeshPro>();
        tmp.text = damage.ToString();
        defaultColor = tmp.color;

        if (damage > 6) transform.localScale += Vector3.one * Mathf.Clamp01(damage * 0.05f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player);
        timer -= Time.deltaTime;
        if (timer >= 0)
        {
            // tmp.material.SetColor("Color", Color.Lerp(Color.clear, Color.white, timer));
            tmp.color = Color.Lerp(Color.clear, defaultColor, timer);
            transform.position += 5 * Time.deltaTime * Vector3.up;
        }
    }
}
