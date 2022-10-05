
using Unity.Burst.CompilerServices;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    GlobalGame global;
    TurnHandler turnHandler;

    public AnimationCurve spawnRate;

    public GameObject[] Pickups;

    private void Start()
    {
        global = GetComponent<GlobalGame>();

        turnHandler = GetComponent<TurnHandler>();

        turnHandler.StartTurn += NewTurn;
    }

    private void NewTurn(int turn)
    {
        if (turn != 0) return;

        SpawnPowerup((int)(spawnRate.Evaluate(Random.Range(0, 10) / 10.0f) * 10));
    }

    public void SpawnPowerup(int num)
    {
        if (Pickups.Length == 0) return;
        Debug.Log($"Spawning {num} pickup{(num == 1 ? "" : "s")}");
        for (int i = 0; i < num; i++)
        {
            RaycastHit hit;
            Vector2 spawn;
            spawn = new Vector2(
            Random.Range(-16.5f, 16.5f),
            Random.Range(1, 14)
        );

            while (Physics.SphereCast(spawn, .2f, Vector3.up, out hit))
            {
                spawn += Vector2.up * .5f;
            }

            Physics.Raycast(spawn, Vector3.down, out hit);

            spawn = hit.point + (Vector3.up * 0.7f);

            Instantiate(Pickups[Random.Range(0, Pickups.Length)], spawn, Quaternion.identity);

        }



    }

}
