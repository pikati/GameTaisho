using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateController : MonoBehaviour
{
    public bool isProgressed { get; set; } = true; //現状は足場を作るときにゲームの進行を一時的に止めるためのやつ
}
