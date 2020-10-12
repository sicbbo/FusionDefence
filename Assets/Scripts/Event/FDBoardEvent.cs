using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FDBoardEvent
{
    public delegate void Event_Void();

    public delegate void CallBack_Bool(bool _bool);
    public event CallBack_Bool callBack_ActiveInputCommander;
    public event CallBack_Bool callBack_Pause;
    public event CallBack_Bool callBack_SelectUnit;

    public void ActiveInputCommander(bool _active)
    {
        callBack_ActiveInputCommander(_active);
    }

    public void CallBack_Pause(bool _flag)
    {
        callBack_Pause(_flag);
    }

    public void SelectUnit(bool _isSelect)
    {
        callBack_SelectUnit(_isSelect);
    }

    public delegate void CallBack_ChangeBoard(FDSystem.GameBoard _enumBoard);
    public event CallBack_ChangeBoard callBack_NextBoard;
    public event CallBack_ChangeBoard callBack_GoBoard;

    public void NextBoard(FDSystem.GameBoard _enumBoard)
    {
        callBack_NextBoard(_enumBoard);
    }

    public void GoBoard(FDSystem.GameBoard _enumBoard)
    {
        callBack_GoBoard(_enumBoard);
    }

    public delegate void Event_SetActiveUIBoard(FDSystem.GameBoard _enumBoard, bool _active);
    public event Event_SetActiveUIBoard callBack_SetActiveUIBoard;

    public void SetActiveUIBoard(FDSystem.GameBoard _enumBoard, bool _active)
    {
        callBack_SetActiveUIBoard(_enumBoard, _active);
    }

    public delegate FDUnit Event_CrateUnit(FDField _field, int _unitTypeID, int _unitGrade);
    public event Event_CrateUnit callBack_CreateUnit;

    public FDUnit CreateUnit(FDField _field, int _unitTypeID, int _unitGrade)
    {
        return callBack_CreateUnit(_field, _unitTypeID, _unitGrade);
    }
}
