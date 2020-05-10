using System.Collections;
using UnityEngine;
interface IReaction
{
    void ReactFirst();
    IEnumerator ReactSecond();

}

class DamageEnemyAndSleep:Reaction
{
}
public class CombatController : MonoBehaviour
{
    public TacticUser tactics;
    public TacticUser tactics;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
