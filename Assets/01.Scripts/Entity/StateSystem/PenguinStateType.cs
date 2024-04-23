public enum PenguinStateType
{
    #region 기본

    Idle,
    Chase,
    Move,
    MustMove,
    Attack,
    #endregion

    #region 병사 능력

    Stun,
    AoEAttack,
    #endregion

    #region 장군 능력

    Dash,
    Throw,
    #endregion
}