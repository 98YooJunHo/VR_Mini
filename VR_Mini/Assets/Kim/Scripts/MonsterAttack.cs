using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterAttack : MonoBehaviour
{
    public GameObject missile;
    public float spawnTime;

    public float blowX;
    public float blowY;
    public float blowZ;

    public Transform makeMissile;

    private int blowCount = 10;     //������ �̻��� ����

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public IEnumerator Missile()
    {
        yield return new WaitForSeconds(1);
        for (int i = 0; i < blowCount; i++)
        {
            SpawnObject();
            // �����ɶ����� ���� �ְ����
            yield return new WaitForSeconds(0.5f); // 0.5�� ���
        }
    }

    private void SpawnObject()
    {
        Vector3 spawnPosition = GetRandomPositionWithinObject(makeMissile, makeMissile.localScale.x, makeMissile.localScale.y, makeMissile.localScale.z);
        // ������Ʈ ����
        GameObject spawnedObject = Instantiate(missile, spawnPosition, Quaternion.identity);
    }

    private Vector3 GetRandomPositionWithinObject(Transform objectTransform, float objectWidth, float objectHeight, float objectDepth)
    {
        float randomX = Random.Range(-objectWidth / 2f, objectWidth / 2f);
        float randomY = Random.Range(-objectHeight / 2f, objectHeight / 2f);
        float randomZ = Random.Range(-objectDepth / 2f, objectDepth / 2f);

        return objectTransform.position + new Vector3(randomX, randomY, randomZ);
    }
}
