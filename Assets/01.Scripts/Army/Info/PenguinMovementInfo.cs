
namespace ArmySystem
{
    [System.Serializable]
    public class PenguinMovementInfo
    {
        public PenguinMovementInfo(bool isCheck, Penguin obj)
        {
            IsCheck = isCheck;
            Obj = obj;
        }

        public bool IsCheck;
        public Penguin Obj;
    }
}