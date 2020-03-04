using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DirectInputTest : MonoBehaviour
{

    // Update is called once per frame
    //void Update()
    //{
    //    //　キーボードのAキーを押した時
    //    if (Keyboard.current.aKey.isPressed)
    //    {
    //        Debug.Log("Aキーが押された");
    //    }
    //    //　物理的なキーボードのレイアウトのBキー
    //    if (Keyboard.current[Key.B].wasPressedThisFrame)
    //    {
    //        Debug.Log("Bキーが押された");
    //    }
    //    //　言語マッピングのCキー
    //    if (((KeyControl)Keyboard.current["C"]).wasPressedThisFrame)
    //    {
    //        Debug.Log("Cキーが押された");
    //    }

    //    //　マウスの左ボタンを押した時この1フレームだけの判定
    //    if (Mouse.current.leftButton.wasPressedThisFrame)
    //    {
    //        Debug.Log("マウスの左ボタンが押された");
    //    }

    //    var gamepad = Gamepad.current;
    //    //　ゲームパッドが接続されていなければこれ以降
    //    if (gamepad == null)
    //    {
    //        Debug.Log("null");
    //        return;
    //    }
    //    //　ゲームパッドの左スティックの入力値が0より大きければその数値を出力
    //    var input = gamepad.leftStick.ReadValue();
    //    if (input.magnitude > 0f)
    //    {
    //        Debug.Log("PS4の左スティックの入力値" + input);
    //    }
    //    //　ゲームパッドの〇ボタンが押された時に出力
    //    if (gamepad.buttonEast.isPressed)
    //    {
    //        Debug.Log("PS4の〇ボタンが押された");
    //    }
    //    //　ゲームパッドの十字キーの上が押されたかどうかこの1フレームだけ判定
    //    if (gamepad.dpad.up.wasPressedThisFrame)
    //    {
    //        Debug.Log("ゲームパッドのDパッドの上が押された");
    //    }
    //}

    ////　ゲームオブジェクトが有効になったら現在のキーボードのonTextInputイベントにリスナーを取り付け
    //public void OnEnable()
    //{
    //    Keyboard.current.onTextInput += OnTextInput;
    //}
    ////　ゲームオブジェクトが無効になったら現在のキーボードのonTextInputイベントからリスナーを削除
    //public void OnDisable()
    //{
    //    Keyboard.current.onTextInput -= OnTextInput;
    //}
    //public void OnTextInput(char c)
    //{
    //    Debug.Log("今打たれた文字" + c);
    //}
}