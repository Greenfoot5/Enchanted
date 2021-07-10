using UnityEngine;

public class GameManager : MonoBehaviour
{
    public void Died(Player player)
    {
        Debug.Log(player.name + " died.");
    }
}
