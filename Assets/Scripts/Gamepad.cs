//using Boo.Lang;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

//emmagatzema l'estat d'un únic gamepad
public struct xButton
{
    public ButtonState prev_state;
    public ButtonState state;
}
//emmagatzema l'estat dun únic trigger del gamepad
public struct TriggerState
{
    public float prev_value;
    public float current_value;
}
//vibracio
class xRumble
{
    public float timer;
    public float fadeTime; //en segons
    public Vector2 power;

    public void Update()
    {
        this.timer -= Time.deltaTime;
    }
}

public class Gamepad {

    private GamePadState prev_state;
    private GamePadState state;

    private int gamePadIndex;
    private PlayerIndex playerIndex;
    private List<xRumble> rumbleEvents;
    private Dictionary<string, xButton> inputMap;

    private xButton A, B, X, Y;
    private xButton DPad_Up, DPad_Down, DPad_Left, DPad_Right;

    private xButton Guide; //Xbox button logo
    private xButton Back, Start;
    private xButton L3, R3;
    private xButton LB, RB;
    private TriggerState LT, RT;

    public Gamepad(int index)
    {
        gamePadIndex = index - 1;
        playerIndex = (PlayerIndex)gamePadIndex;

        rumbleEvents = new List<xRumble>();
        inputMap = new Dictionary<string, xButton>();
    }

	//// Use this for initialization
	//void Start () {
		
	//}
	
	// Update is called once per frame
	public void Update () {
        state = GamePad.GetState(playerIndex);
        if (state.IsConnected)
        {
            A.state = state.Buttons.A;
            B.state = state.Buttons.B;
            X.state = state.Buttons.X;
            Y.state = state.Buttons.Y;

            DPad_Up.state = state.DPad.Up;
            DPad_Down.state = state.DPad.Down;
            DPad_Left.state = state.DPad.Left;
            DPad_Right.state = state.DPad.Right;
            
            Guide.state = state.Buttons.Guide;
            Back.state = state.Buttons.Back;
            Start.state = state.Buttons.Start;
            L3.state = state.Buttons.LeftStick;
            R3.state = state.Buttons.RightStick;
            LB.state = state.Buttons.LeftShoulder;
            RB.state = state.Buttons.RightShoulder;

            LT.current_value = state.Triggers.Left;
            RT.current_value = state.Triggers.Right;

            UpdateInputMap();
            HandleRumble();
        }
	}
    public void Refresh()
    {
        prev_state = state;
        if (state.IsConnected)
        {
            A.prev_state = prev_state.Buttons.A;
            B.prev_state = prev_state.Buttons.B;
            X.prev_state = prev_state.Buttons.X;
            Y.prev_state = prev_state.Buttons.Y;

            DPad_Up.prev_state = prev_state.DPad.Up;
            DPad_Down.prev_state = prev_state.DPad.Down;
            DPad_Left.prev_state = prev_state.DPad.Left;
            DPad_Right.prev_state = prev_state.DPad.Right;

            Guide.prev_state = prev_state.Buttons.Guide;
            Back.prev_state = prev_state.Buttons.Back;
            Start.prev_state = prev_state.Buttons.Start;
            L3.prev_state = prev_state.Buttons.LeftStick;
            R3.prev_state = prev_state.Buttons.RightStick;
            LB.prev_state = prev_state.Buttons.LeftShoulder;
            RB.prev_state = prev_state.Buttons.RightShoulder;

            LT.prev_value = prev_state.Triggers.Left;
            RT.prev_value = prev_state.Triggers.Right;

            UpdateInputMap();
        }
    }

    public bool GetButton(string button)
    {
        return inputMap[button].state == ButtonState.Pressed ? true : false;
    }
    public bool GetButtonDown(string button)
    {
        return (inputMap[button].prev_state == ButtonState.Released && inputMap[button].state == ButtonState.Pressed) ? true : false;
    }

    public bool GetButtonUp(string button)
    {
        return (inputMap[button].prev_state == ButtonState.Pressed && inputMap[button].state == ButtonState.Released) ? true : false;
    }

    public int Index { get { return gamePadIndex; } }
    public bool IsConnected { get { return state.IsConnected; } }

    void UpdateInputMap()
    {
        inputMap["A"] = A;
        inputMap["B"] = B;
        inputMap["X"] = X;
        inputMap["Y"] = Y;

        inputMap["DPad_Up"] = DPad_Up;
        inputMap["DPad_Down"] = DPad_Down;
        inputMap["DPad_Left"] = DPad_Left;
        inputMap["DPad_Right"] = DPad_Right;

        inputMap["Guide"] = Guide;
        inputMap["Back"] = Back;
        inputMap["Start"] = Start;

        inputMap["L3"] = L3;
        inputMap["R3"] = R3;

        inputMap["LB"] = LB;
        inputMap["RB"] = RB;
    }

    void HandleRumble()
    {
        if(rumbleEvents.Count > 0)
        {
            Vector2 currentPower = new Vector2(0, 0);
            for (int i = 0; i < rumbleEvents.Count; i++)
            {
                if (rumbleEvents[i].timer > 0) {
                    float timeLeft = Mathf.Clamp(rumbleEvents[i].timer / rumbleEvents[i].fadeTime, 0f, 1f);
                    currentPower = new Vector2(Mathf.Max(rumbleEvents[i].power.x * timeLeft, currentPower.x), Mathf.Max(rumbleEvents[i].power.y * timeLeft, currentPower.y));
                    GamePad.SetVibration(playerIndex, currentPower.x, currentPower.y);

                    //rumbleEvents[i].Update();
                } else {
                    rumbleEvents.Remove(rumbleEvents[i]);
                }
            }
        }
    }

    public void AddRumble(float timer, Vector2 power, float fadeTime)
    {
        xRumble rumble = new xRumble();
        rumble.timer = timer;
        rumble.power = power;
        rumble.fadeTime = fadeTime;
        rumbleEvents.Add(rumble);
    }

    public GamePadThumbSticks.StickValue GetStick_L()
    {
        return state.ThumbSticks.Left;
    }
    public GamePadThumbSticks.StickValue GetStick_R()
    {
        return state.ThumbSticks.Right;
    }

    public float GetTrigger_L() { return state.Triggers.Left; }
    public float GetTrigger_R() { return state.Triggers.Right; }
    public bool GetTriggerTap_L()
    {
        return (LT.prev_value == 0f && LT.current_value >= 0.1f) ? true : false;
    }
    public bool GetTriggerTap_R()
    {
        return (RT.prev_value == 0f && RT.current_value >= 0.1f) ? true : false;
    }
}
