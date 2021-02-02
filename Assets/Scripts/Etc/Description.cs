using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 주의사항 표기용
/// </summary>
public class Description : MonoBehaviour
{
    [Header("주의사항")]

    [TextArea(3,10)]
    public string NOTE;
}
