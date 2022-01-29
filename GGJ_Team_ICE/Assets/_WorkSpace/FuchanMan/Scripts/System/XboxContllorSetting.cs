//#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.Collections;

//軸のタイプの種類
public enum AxisType
{
    KeyOrMouseButton = 0,   //キーかマウスクリックによるボタン入力
    MouseMovement = 1,      //マウスのデルタとスクロールホイールによる入力
	JoystickAxis = 2        //ジョイスティックによる入力
};

//設定する軸の情報をまとめたクラス(InputManagerの情報の写し)
public class InputAxis
{
    public string name = "";    //軸名,スクリプトからアクセスする際はここの名前を使用する

	public string descriptiveName = "";         //廃止予定の値、設定する必要なし
	public string descriptiveNegativeName = "";	//同上

    //floatの値(-1 ～ +1)をキーで取得するならマイナス要素のボタンにも振り当てる 例：negative(「S」: -1),positive(「W」: +1) 
    //boolの値(True or False)の取得ならpositiveの設定だけでOK
    public string negativeButton = "";  //マイナス要素(negative)のボタン
    public string positiveButton = "";  //プラス要素(positive)のボタン
    public string altNegativeButton = "";   //追加のマイナス要素のボタン
    public string altPositiveButton = "";   //追加のプラス要素のボタン

    public float gravity = 0;   //何も入力がない場合に軸がニュートラルになるための速さ
	public float dead = 0;      //アプリケーションが動きを認識するために、 ユーザーがアナログスティックを動かす必要がある距離
	public float sensitivity = 0;   //軸がターゲット値に向かう速さ (ユニット/秒) ※デジタルデバイス専用

	public bool snap = false;   //有効にすると、逆方向に対応するボタンを押すと軸の値が 0 にリセットされる
	public bool invert = false; //有効にすると、正のボタンが負の値を軸に送信、また、その逆を行う
	
	//軸のタイプ
	public AxisType type = AxisType.KeyOrMouseButton;

	//デバイスからの入力軸の番号 (デフォルトは 1(X軸))
	public int axis = 1;
    //対応するパッドの番号 (0なら全てのゲームパッドから取得される。1以降なら対応したゲームパッド)
    public int joyNum = 0;

	/// <summary>
	/// 押すと1(True)になるキー(boolの値)の設定データを作成する
	/// </summary>
	/// <returns>ボタン軸設定</returns>
	/// <param name="name">軸名.</param>
	/// <param name="positiveButton">対応するボタン.</param>
	/// <param name="altPositiveButton">追加の対応するボタン.</param>
	public static InputAxis CreateButton(string name, string positiveButton, string altPositiveButton)
	{
		var axis = new InputAxis();
		axis.name = name;
		axis.positiveButton = positiveButton;
		axis.altPositiveButton = altPositiveButton;
		axis.gravity = 1000;
		axis.dead = 0.001f;
		axis.sensitivity = 1000;
		axis.type = AxisType.KeyOrMouseButton;

		return axis;
	}

	/// <summary>
	/// ゲームパッド用の軸(floatの値)の設定データを作成する
	/// </summary>
	/// <returns>axis軸設定</returns>
	/// <param name="name">軸英.</param>
	/// <param name="joystickNum">ゲームパッドのどこの軸か.</param>
	/// <param name="axisNum">デバイスからの軸情報の番号.</param>
	public static InputAxis CreatePadAxis(string name, int joystickNum, int axisNum)
	{
		var axis = new InputAxis();
		axis.name = name;
		axis.dead = 0.2f;
		axis.sensitivity = 1;
		axis.type = AxisType.JoystickAxis;
		axis.axis = axisNum;
		axis.joyNum = joystickNum;

		return axis;
	}

	/// <summary>
	/// キーボード用の軸の設定データを作成する
	/// </summary>
	/// <returns>キーボード用軸設定.</returns>
	/// <param name="name">軸名.</param>
	/// <param name="negativeButton">対応するマイナス要素ボタン.</param>
	/// <param name="positiveButton">対応するプラス要素ボタン.</param>
	/// <param name="axisNum">デバイスからの軸情報の番号.</param>
	public static InputAxis CreateKeyAxis(string name, string negativeButton, string positiveButton, string altNegativeButton, string altPositiveButton)
	{
		var axis = new InputAxis();
		axis.name = name;
		axis.negativeButton = negativeButton;
		axis.positiveButton = positiveButton;
		axis.altNegativeButton = altNegativeButton;
		axis.altPositiveButton = altPositiveButton;
		axis.gravity = 3;
		axis.dead = 0.001f;
		axis.sensitivity = 3;
		axis.snap = true;
		axis.type = AxisType.KeyOrMouseButton;

		return axis;
	}
}

// InputManagerを設定するためのクラス
public class XboxContllorSetting
{
	private SerializedObject serializedObject;  //InputManager.assetのシリアライズオブジェクト格納用
	private SerializedProperty axesProperty;	//axisのシリアライズプロパティ格納用(axes = axisの複数形)
	//コンストラクタ
	public XboxContllorSetting()
    {
		// InputManager.assetをシリアライズされたオブジェクトとして読み込む
		serializedObject = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/InputManager.asset")[0]);
		//axisの要素を取得
		axesProperty = serializedObject.FindProperty("m_Axes");
	}

	/// <summary>
	/// 子要素のプロパティを取得します。
	/// </summary>
	/// <returns>The child property.</returns>
	/// <param name="parent">Parent.</param>
	/// <param name="name">Name.</param>
	private SerializedProperty GetChildProperty(SerializedProperty parent, string name)
	{
		SerializedProperty child = parent.Copy();
		child.Next(true);
		do
		{
			if (child.name == name)
				return child;
		}
		while (child.Next(false));	

		return null;
	}

	/// <summary>
	/// 軸の追加
	/// </summary>
	/// <param name="serializedObject">シリアライズオブジェクト.</param>
	/// <param name="axis">軸情報.</param>
	public void AddAxis(InputAxis axis)
    {
		if(axis.axis < 1) Debug.LogError("Axisは1以上に設定してください。");	//デバイスから0以下が入力されることはない
		SerializedProperty axesProperty = serializedObject.FindProperty("m_Axes");

		axesProperty.arraySize++;
		serializedObject.ApplyModifiedProperties();

		SerializedProperty axisProperty = axesProperty.GetArrayElementAtIndex(axesProperty.arraySize - 1);

		GetChildProperty(axisProperty, "m_Name").stringValue = axis.name;
		GetChildProperty(axisProperty, "descriptiveName").stringValue = axis.descriptiveName;
		GetChildProperty(axisProperty, "descriptiveNegativeName").stringValue = axis.descriptiveNegativeName;
		GetChildProperty(axisProperty, "negativeButton").stringValue = axis.negativeButton;
		GetChildProperty(axisProperty, "positiveButton").stringValue = axis.positiveButton;
		GetChildProperty(axisProperty, "altNegativeButton").stringValue = axis.altNegativeButton;
		GetChildProperty(axisProperty, "altPositiveButton").stringValue = axis.altPositiveButton;
		GetChildProperty(axisProperty, "gravity").floatValue = axis.gravity;
		GetChildProperty(axisProperty, "dead").floatValue = axis.dead;
		GetChildProperty(axisProperty, "sensitivity").floatValue = axis.sensitivity;
		GetChildProperty(axisProperty, "snap").boolValue = axis.snap;
		GetChildProperty(axisProperty, "invert").boolValue = axis.invert;
		GetChildProperty(axisProperty, "type").intValue = (int)axis.type;
		GetChildProperty(axisProperty, "axis").intValue = axis.axis - 1;
		GetChildProperty(axisProperty, "joyNum").intValue = axis.joyNum;

		serializedObject.ApplyModifiedProperties();
	}

	/// <summary>
	/// 設定を全てクリアします。
	/// </summary>
	public void Clear()
	{
		axesProperty.ClearArray();
		serializedObject.ApplyModifiedProperties();
	}
}

[CustomEditor(typeof(XboxContllorSetter))]
public class XboxContllorEditor : Editor
{
	XboxContllorSetter setter;
	private bool _foldout = false;
	private void OnEnable()
    {
		setter = target as XboxContllorSetter;
    }
	//OnInspectorGUIでカスタマイズのGUIに変更する
	public override void OnInspectorGUI()
	{
		setter.playerCnt = EditorGUILayout.IntField("プレイヤーの数", setter.playerCnt);
		EditorGUILayout.LabelField("キーボード入力の値");
		base.OnInspectorGUI();
		//元のInspector部分の下にボタンを表示
		if (GUILayout.Button("InputManager Set"))
		{
			setter.ResetInputManager();
		}
	}
}

//#endif
