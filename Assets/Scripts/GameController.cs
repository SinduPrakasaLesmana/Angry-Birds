using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public SlingShooter SlingShooter;
    public TrailController TrailController;
    
    public List<Bird> Birds;
    public List<Enemy> Enemies;

    private Bird _shotBird;
    public BoxCollider2D TapCollider;
    
    public GameObject PanelLevelClear;
    public GameObject PanelLevelFailed;

    private bool _isGameEnded = false;
    

    void Start()
    {
        for(int i = 0; i < Birds.Count; i++)
        {
            Birds[i].OnBirdDestroyed += ChangeBird;
            Birds[i].OnBirdShot += AssignTrail;
        }

        for(int i = 0; i < Enemies.Count; i++)
        {
            Enemies[i].OnEnemyDestroyed += CheckGameEnd;
        }

        TapCollider.enabled = false;
        SlingShooter.InitiateBird(Birds[0]);
        _shotBird = Birds[0];
    }

    public void ChangeBird()
    {
        TapCollider.enabled = false;

        if (_isGameEnded)
        {
            return;
        }

        Birds.RemoveAt(0);

        if(Birds.Count > 0)
        {
            SlingShooter.InitiateBird(Birds[0]);
            _shotBird = Birds[0];
        }

        if (Birds.Count <= 0 && Enemies.Count >= 0)
        {
            StartCoroutine(FailedC(2f));
        }
    }

    public void CheckGameEnd(GameObject destroyedEnemy)
    {
        for(int i = 0; i < Enemies.Count; i++)
        {
            if(Enemies[i].gameObject == destroyedEnemy)
            {
                Enemies.RemoveAt(i);
                break;
            }
        }

        if(Enemies.Count == 0 && Birds.Count >= 0)
        {
            _isGameEnded = true;
            StartCoroutine(ClearC(2f));
        }
    }
    
    private IEnumerator FailedC(float second)
    {
        yield return new WaitForSeconds(second);
        PanelLevelFailed.SetActive(true);
    }

    private IEnumerator ClearC(float second)
    {
        yield return new WaitForSeconds(second);
        PanelLevelFailed.SetActive(false);
        PanelLevelClear.SetActive(true);
    }

    public void AssignTrail(Bird bird)
    {
        TrailController.SetBird(bird);
        StartCoroutine(TrailController.SpawnTrail());
        TapCollider.enabled = true;
    }

    void OnMouseUp()
    {
        if(_shotBird != null)
        {
            _shotBird.OnTap();
        }
    }
   
}