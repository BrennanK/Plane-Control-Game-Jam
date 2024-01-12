using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CommentInFolder : ScriptableObject
{
    [TextArea(2, 10)]
    public string _comment;
}
