public enum PenguinStateType
{
    #region 기본

    Idle,
    Chase,
    Move,
    MustMove,
    Attack,
    #endregion

    #region 특수 능력

    Stun,
    AoEAttack,
    #endregion

    #region 장군 스킬

    Dash,
    Throw,
    SpinAttack,
    KatanaSkill,
    LanceSkill,


    KatanaUltimate,
    LanceUltimate,
    #endregion

    #region 그 외

    Impact,
    Block,
    #endregion
}