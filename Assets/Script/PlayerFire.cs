using UnityEngine;
using TMPro;
using System.Collections;

public class PlayerFire : MonoBehaviour
{
    public enum WeaponMode { Rifle, MachineGun }
    public WeaponMode currentMode = WeaponMode.Rifle;

    [Header("UI Settings")]
    public TextMeshProUGUI weaponText;

    [Header("Weapon Settings")]
    public int riflePower = 5;
    public int mgPower = 2;
    public float fireRate = 0.1f; // 기관총 연사 속도
    float timer = 0;

    [Header("Effects")]
    public GameObject bulletEffect;
    ParticleSystem ps;

    public GameObject[] eff_Flash;

    Animator anim;
    void Start()
    {
        ps = bulletEffect.GetComponent<ParticleSystem>();
        UpdateUI(); // 시작할 때 UI 초기화
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (GameManager.instance == null || GameManager.instance.isGameStart == false)
        {
            return;
        }
        // 1. 무기 교체 입력
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentMode = WeaponMode.Rifle;
            UpdateUI();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentMode = WeaponMode.MachineGun;
            UpdateUI();
        }

        // 2. 공격 로직 분기
        if (currentMode == WeaponMode.Rifle)
        {
            if (Input.GetMouseButtonDown(0)) Fire(riflePower);
        }
        else // 기관총 모드
        {
            timer += Time.deltaTime;
            if (Input.GetMouseButton(0) && timer > fireRate)
            {
                Fire(mgPower);
                timer = 0;
            }
            StartCoroutine(ShootEffectOn(0.05f));
        }
        if (anim.GetFloat("MoveMotion")==0)
        {
            anim.SetTrigger("Attack");
        }
    }
    IEnumerator ShootEffectOn(float duration)
    {
        int num = Random.Range(0, eff_Flash.Length -1);
        eff_Flash[num].SetActive(true);
        yield return new WaitForSeconds(duration);
        eff_Flash[num].SetActive(false);
    }

    void Fire(int power)
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo))
        {
            if (hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                EnemyFSM eFSM = hitInfo.transform.GetComponent<EnemyFSM>();
                eFSM.HitEnemy(power);
            }
            bulletEffect.transform.position = hitInfo.point;
            bulletEffect.transform.forward = hitInfo.normal;
            ps.Play();
        }
    }

    void UpdateUI()
    {
        if (weaponText != null)
        {
            weaponText.text = (currentMode == WeaponMode.Rifle) ? "현재 무기: 소총" : "현재 무기: 기관총";
        }
    }
}