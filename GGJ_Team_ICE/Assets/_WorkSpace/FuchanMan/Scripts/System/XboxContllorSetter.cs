using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/CreateXboxContllorSetter", fileName = "XboxContllorSetter")]
public class XboxContllorSetter : ScriptableObject
{
    [HideInInspector]
    public int playerCnt = 1;   //プレイヤーの数
    [System.Serializable]
    public struct KeyBoradInput
    {
        public string upKeyL;
        public string downKeyL;
        public string leftKeyL;
        public string rightKeyL;
        public string upKeyR;
        public string downKeyR;
        public string leftKeyR;
        public string rightKeyR;
        public string buttonA;
        public string buttonB;
        public string buttonX;
        public string buttonY;
        public string buttonL;
        public string buttonR;
        public string buttonView;
        public string buttonHome;
        public string stickpushL;
        public string stickpushR;
        public string upCross;
        public string downCross;
        public string leftCross;
        public string rightCross;
        public string triggerL;
        public string triggerR;
    }

    public KeyBoradInput keyBoradInput = new KeyBoradInput()
    {
        upKeyL = "w",
        downKeyL = "s",
        leftKeyL = "a",
        rightKeyL = "d",
        upKeyR = "up",
        downKeyR = "down",
        leftKeyR = "left",
        rightKeyR = "right",
        buttonA = "space",
        buttonB = "b",
        buttonX = "x",
        buttonY = "y",
        buttonL = "q",
        buttonR = "e",
        buttonView = "v",
        buttonHome = "h",
        stickpushL = "f",
        stickpushR = "[0]",
        upCross = "[8]",
        downCross = "[2]",
        leftCross = "[4]",
        rightCross = "[6]",
        triggerL = "left shift",
        triggerR = "right shift"
    };

    // インプットマネージャーを設定/再設定する
    public void ResetInputManager()
    {
        Debug.Log("インプットマネージャーの設定を開始します。");
        XboxContllorSetting xboxContllorSetting = new XboxContllorSetting();

        Debug.Log("設定を全てクリアします。");
        xboxContllorSetting.Clear();

        Debug.Log("プレイヤーごとの設定を追加します。");
        for (int i = 1; i < playerCnt + 1; i++)
        {
            AddPlayerInputSettings(xboxContllorSetting, i);
        }

        Debug.Log("グローバル設定を追加します。");
        AddGlobalInputSettings(xboxContllorSetting);

        Debug.Log("インプットマネージャーの設定が完了しました。");
    }

    /// <summary>
    /// グローバル(どのデバイスからでも受け付ける)な入力設定を追加する（OK、キャンセルなど）
    /// </summary>
    /// <param name="XboxContllorSetting">xboxContllorSetting.</param>
    private static void AddGlobalInputSettings(XboxContllorSetting xboxContllorSetting)
    {
        // 横方向
        {
            var name = "Horizontal";
            xboxContllorSetting.AddAxis(InputAxis.CreatePadAxis(name, 0, 1));
            xboxContllorSetting.AddAxis(InputAxis.CreateKeyAxis(name, "a", "d", "left", "right"));
        }
        // 縦方向
        {
            var name = "Vertical";
            xboxContllorSetting.AddAxis(InputAxis.CreatePadAxis(name, 0, 2));
            xboxContllorSetting.AddAxis(InputAxis.CreateKeyAxis(name, "s", "w", "down", "up"));
        }
        // 決定
        {
            var name = "Submit";
            xboxContllorSetting.AddAxis(InputAxis.CreateButton(name, "z", "joystick button 0"));
        }
        // キャンセル
        {
            var name = "Cancel";
            xboxContllorSetting.AddAxis(InputAxis.CreateButton(name, "x", "joystick button 1"));
        }
        // ポーズ
        {
            var name = "Pause";
            xboxContllorSetting.AddAxis(InputAxis.CreateButton(name, "escape", "joystick button 7"));
        }
    }
    /// <summary>
	/// プレイヤーごとの入力設定を追加する
	/// </summary>
	/// <param name="inputManagerGenerator">Input manager generator.</param>
	/// <param name="playerIndex">Player index.</param>
	private void AddPlayerInputSettings(XboxContllorSetting xboxContllorSetting, int playerIndex)
    {
        if (playerIndex < 1) Debug.LogError("プレイヤーインデックスの値が不正です。");

        // Lスティック横方向
        {
            var name = "HorizontalL_P" + playerIndex;
            xboxContllorSetting.AddAxis(InputAxis.CreatePadAxis(name, playerIndex, 1));
            if(playerIndex == 1) xboxContllorSetting.AddAxis(InputAxis.CreateKeyAxis(name, keyBoradInput.leftKeyL, keyBoradInput.rightKeyL, "", ""));
        }
        // Lスティック縦方向
        {
            var name = "VerticalL_P" + playerIndex;
            xboxContllorSetting.AddAxis(InputAxis.CreatePadAxis(name, playerIndex, 2));
            if (playerIndex == 1) xboxContllorSetting.AddAxis(InputAxis.CreateKeyAxis(name, keyBoradInput.downKeyL, keyBoradInput.upKeyL, "", ""));
        }

        // Rスティック横方向
        {
            var name = "HorizontalR_P" + playerIndex;
            xboxContllorSetting.AddAxis(InputAxis.CreatePadAxis(name, playerIndex, 4));
            if (playerIndex == 1) xboxContllorSetting.AddAxis(InputAxis.CreateKeyAxis(name, keyBoradInput.leftKeyR, keyBoradInput.rightKeyR, "", ""));
        }
        // Rスティック縦方向
        {
            var name = "VerticalR_P" + playerIndex;
            xboxContllorSetting.AddAxis(InputAxis.CreatePadAxis(name, playerIndex, 5));
            if (playerIndex == 1) xboxContllorSetting.AddAxis(InputAxis.CreateKeyAxis(name, keyBoradInput.downKeyR, keyBoradInput.upKeyR, "", ""));
        }

        // Aボタン
        {
            var name = "ButtonA_P" + playerIndex;
            var button = string.Format("joystick {0} button 0", playerIndex);
            xboxContllorSetting.AddAxis(InputAxis.CreateButton(name, button, keyBoradInput.buttonA));
        }
        // Bボタン
        {
            var name = "ButtonB_P" + playerIndex;
            var button = string.Format("joystick {0} button 1", playerIndex);
            xboxContllorSetting.AddAxis(InputAxis.CreateButton(name, button, keyBoradInput.buttonB));
        }
        // Xボタン
        {
            var name = "ButtonX_P" + playerIndex ;
            var button = string.Format("joystick {0} button 2", playerIndex);
            xboxContllorSetting.AddAxis(InputAxis.CreateButton(name, button, keyBoradInput.buttonX));
        }
        // Yボタン
        {
            var name = "ButtonY_P" + playerIndex ;
            var button = string.Format("joystick {0} button 3", playerIndex);
            xboxContllorSetting.AddAxis(InputAxis.CreateButton(name, button, keyBoradInput.buttonY));
        }
        // Lボタン
        {
            var name = "ButtonL_P" + playerIndex ;
            var button = string.Format("joystick {0} button 4", playerIndex);
            xboxContllorSetting.AddAxis(InputAxis.CreateButton(name, button, keyBoradInput.buttonL));
        }
        // Rボタン
        {
            var name = "ButtonR_P" + playerIndex ;
            var button = string.Format("joystick {0} button 5", playerIndex);
            xboxContllorSetting.AddAxis(InputAxis.CreateButton(name, button, keyBoradInput.buttonR));
        }
        // Viewボタン
        {
            var name = "ButtonView_P" + playerIndex ;
            var button = string.Format("joystick {0} button 6", playerIndex);
            xboxContllorSetting.AddAxis(InputAxis.CreateButton(name, button, keyBoradInput.buttonView));
        }
        // Menuボタン
        {
            var name = "ButtonMenu_P" + playerIndex ;
            var button = string.Format("joystick {0} button 7", playerIndex);
            xboxContllorSetting.AddAxis(InputAxis.CreateButton(name, button, keyBoradInput.buttonHome));
        }
        // LStick押し込み
        {
            var name = "StickPushL_P" + playerIndex;
            var button = string.Format("joystick {0} button 8", playerIndex);
            xboxContllorSetting.AddAxis(InputAxis.CreateButton(name, button, keyBoradInput.stickpushL));
        }
        // RStick押し込み
        {
            var name = "StickPushR_P" + playerIndex;
            var button = string.Format("joystick {0} button 9", playerIndex);
            xboxContllorSetting.AddAxis(InputAxis.CreateButton(name, button, keyBoradInput.stickpushR));
        }
        // 十字キー横方向
        {
            var name = "HorizontalCross_P" + playerIndex;
            xboxContllorSetting.AddAxis(InputAxis.CreatePadAxis(name, playerIndex, 6));
            if (playerIndex == 1) xboxContllorSetting.AddAxis(InputAxis.CreateKeyAxis(name, keyBoradInput.leftCross, keyBoradInput.rightCross, "", ""));
        }
        // 十字キー縦方向
        {
            var name = "VirticalCross_P" + playerIndex;
            xboxContllorSetting.AddAxis(InputAxis.CreatePadAxis(name, playerIndex, 7));
            if (playerIndex == 1) xboxContllorSetting.AddAxis(InputAxis.CreateKeyAxis(name, keyBoradInput.downCross, keyBoradInput.upCross, "", ""));
        }
        // Lトリガー
        {
            var name = "TriggerL_P" + playerIndex;
            xboxContllorSetting.AddAxis(InputAxis.CreatePadAxis(name, playerIndex, 9));
            if (playerIndex == 1) xboxContllorSetting.AddAxis(InputAxis.CreateKeyAxis(name, "", keyBoradInput.triggerL, "", ""));
        }
        // Rトリガー
        {
            var name = "TriggerR_P" + playerIndex;
            xboxContllorSetting.AddAxis(InputAxis.CreatePadAxis(name, playerIndex, 10));
            if (playerIndex == 1) xboxContllorSetting.AddAxis(InputAxis.CreateKeyAxis(name, "", keyBoradInput.triggerR, "", ""));
        }
    }
}
