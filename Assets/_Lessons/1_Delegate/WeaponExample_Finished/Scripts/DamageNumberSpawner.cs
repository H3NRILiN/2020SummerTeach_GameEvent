using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ISU.Lesson.Delegate.WeaponExample
{
    public class DamageNumberSpawner : MonoBehaviour
    {
        [SerializeField] WeaponTest m_WeaponTest;
        [SerializeField] DamageNumber m_DamageNumber;

        Camera m_MainCam;

        private void OnEnable()
        {
            Damagable.m_OnTakeDamage += SpawnNumber;
        }
        private void OnDisable()
        {
            Damagable.m_OnTakeDamage -= SpawnNumber;
        }
        private void Awake()
        {
            if (!m_WeaponTest)
                m_WeaponTest = FindObjectOfType<WeaponTest>();

        }
        private void Start()
        {
            m_MainCam = FindObjectOfType<Camera>();
        }
        void SpawnNumber(Damagable target, float amount)
        {
            var spawnPos = m_MainCam.WorldToScreenPoint(target.transform.position);
            spawnPos.x += Random.Range(0, 20);
            spawnPos.y += Random.Range(0, 20);
            spawnPos.z += Random.Range(0, 20);
            var number = Instantiate(m_DamageNumber, spawnPos, Quaternion.identity, transform);

            number.OnSpawn(amount);
        }
    }
}