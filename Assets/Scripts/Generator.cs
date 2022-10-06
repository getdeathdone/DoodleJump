using UnityEngine;
using Random = UnityEngine.Random;

public class Generator : MonoBehaviour
{
    [SerializeField] private GameObject _generateObject;
    [SerializeField] private float _startOffset;
    [SerializeField] private float _offsetEverySpawn;
    [SerializeField] private int _startAmount = 10;
    //[SerializeField] [Range(0,100)] private int _spawnRate;
    [SerializeField] private float _offsetDefaultMinY=1f;
    [SerializeField] private float _offsetDefaultMaxY= 1.5f;

    private void Start()
    {
        Generate(_offsetDefaultMinY,_offsetDefaultMaxY,_startOffset,_offsetEverySpawn,_startAmount);
    }

    public void ReGenerate(float offsetMinY,float offsetMaxY,float height=0,float offset=0,int amount=10)
    {
        Generate(offsetMinY+_offsetDefaultMinY,offsetMaxY+_offsetDefaultMaxY,height,offset,amount);
    }
    public void ReGenerateDefault(float height)
    {
        Generate(_offsetDefaultMinY,_offsetDefaultMaxY,height+_startOffset,_offsetEverySpawn,_startAmount);
    }

    private void Generate(float offsetMinY,float offsetMaxY,float height,float offset,int amount)
    {   
        Vector3 SpawnerPosition = new Vector3();
        SpawnerPosition.y += height;
        for (int i = 0; i < amount; i++)
        {
            SpawnerPosition.x = Random.Range(-2.75f, 2.75f);
            SpawnerPosition.y += Random.Range(offsetMinY, offsetMaxY)+offset;

            Instantiate(_generateObject, SpawnerPosition, Quaternion.identity);
        }
    }
}
