public class SumoAnimationTrigger : PenguinAnimationTrigger
{
    private SumoGeneralPenguin Sumo => _penguin as SumoGeneralPenguin;

    private void ThrowTrigger()
    {
        Sumo.Skill.PlaySkill();
    }
}
