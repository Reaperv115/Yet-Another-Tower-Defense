using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : EnemyBase
{
    GameObject tower;

    GameObject track;
    List<Transform> pathwayMarkers;
    // Start is called before the first frame update
    void Start()
    {
        Health = 65;
        switch (WaveManager.instance.GetRound())
        {
            case 2: Health *= 2; break;
            case 3: Health *= 3; break;
            case 4: Health *= 4; break;
            case 5: Health *= 5; break;
        }
        print("basic enemy health: " + Health);
        speed = 20f * Time.deltaTime;
        attackTower = false;
        pathIndex = 0;
        pathwayMarkers = new List<Transform>();
        track = GameManager.instance.GetTrack();
        GetPathMarkers();
    }

    // Update is called once per frame
    void Update()
    {
        if (Health <= 0f)
        {
            ScoreManager.instance.amount += 1;
            EnemyManager.instance.enemies.Remove(gameObject);
            Destroy(gameObject);
        }
        // attacking the tower if
        // at the end of the track
        if (attackTower)
        {
            tower = GameManager.instance.FindTower(GameManager.instance.GetTrack());
            transform.position = Vector3.MoveTowards(transform.position, tower.transform.position, speed);
        }
        // else make the enemy go
        else { Go(speed); }
    }

    // guiding the enemy through the track
    public override void Go(float _speed)
    {
        
        if (transform.position.Equals(pathwayMarkers[pathIndex].transform.position))
        {
            if (pathIndex.Equals(pathwayMarkers.Count - 1)) attackTower = true;
            else
            {
                ++pathIndex;
                transform.position = Vector3.MoveTowards(transform.position, pathwayMarkers[pathIndex].transform.position, _speed);
            }

        }
        else transform.position = Vector3.MoveTowards(transform.position, pathwayMarkers[pathIndex].transform.position, _speed);
    }

    // getting track info
    void GetPathMarkers() { 
        for (int i = 0; i < track.transform.GetChild(0).transform.childCount; ++i) pathwayMarkers.Add(track.transform.GetChild(0).transform.GetChild(i));
    }



    public int GetHealth() { return Health; }
}
