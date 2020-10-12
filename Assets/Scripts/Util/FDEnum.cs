/*--------------------------------------------
 * Enum
 -------------------------------------------*/
namespace FDSystem
{
    public enum Scene
    {
        Launcher = 0,
        Title,
        Lobby,
        InGame,
        Result,
    }

    public enum GameBoard
    {
        Load = 0,
        Ready,
        //Select,
        Wave,
        Result,
    }

    public enum State
    {
        None = 0,
        Select,
        DeployField,
        DeployStorage,
        Attack,
        Targeting,
        Hit,
        Death,
        MoveToTarget,
        Grade,
    }

    public enum ObjectID
    {
        Unit = 0,
        Enemy = 100,
        Field = 200,
        Storage = 300,
    }

    public enum AnimationType
    {
        None = 0,
        Idle,
        Attack,
    }

    public enum ModelType
    {
        None = 0,
        SelectArrow,
        Star1,
        Star2,
        Star3,

        MaxCount,
    }
}

namespace FDUI
{
    public enum PopupUI
    {
        Popup_Pause = 0,
    }
}