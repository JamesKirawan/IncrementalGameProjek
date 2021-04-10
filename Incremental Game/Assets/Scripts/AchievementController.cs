using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementController : MonoBehaviour
{
    private static AchievementController _instance = null;
    public static AchievementController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<AchievementController>();
            }
            return _instance;
        }
    }
    [SerializeField] private Transform _popUpTransform;
    [SerializeField] private Text _popUpText;
    [SerializeField] private float _popUpShowDuration = 3f;
    [SerializeField] private List<AchievementData> _achievementList;

    private float _popUpShadowDurationCounter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (_popUpShadowDurationCounter > 0)
        {
            // Kurangi durasi ketika pop up durasi lebih dari 0
            _popUpShadowDurationCounter -= Time.unscaledDeltaTime;
            // Lerp adalah fungsi linear interpolation, digunakan untuk mengubah value secara perlahan
            _popUpTransform.localScale = Vector3.LerpUnclamped(_popUpTransform.localScale, Vector3.one, 0.5f);

        }
        else
        {
            _popUpTransform.localScale = Vector2.LerpUnclamped(_popUpTransform.localScale, Vector3.right, 0.5f);
        }
    }
    public void UnlockAchievement(AchievementType type,string value)
    {
        // Mencari data achievement
        AchievementData achievement = _achievementList.Find(a => a.Type == type && a.Value == value);
        if (achievement != null && !achievement.IsUnlocked)
        {
            achievement.IsUnlocked = true;
            ShowAchievementPopUp(achievement);
        }
    }
    public void UnlockAchievement1(AchievementType type, double value)
    {
        AchievementData achievement = _achievementList.Find(a => a.Type == type && a.gold <= value);
        if (achievement != null && !achievement.IsUnlocked)
        {
            achievement.IsUnlocked = true;
            ShowAchievementPopUp(achievement);
        }
    }
    private void ShowAchievementPopUp(AchievementData achievement)
    {
        _popUpText.text = achievement.Title;
        _popUpShadowDurationCounter = _popUpShowDuration;
        _popUpTransform.localScale = Vector2.right;
    }
    // System.Serializable digunakan agar object dari script bisa di-serialize
    // dan bisa di-inputkan dari Inspector, jika tidak terdapat ini, maka variable tidak akan muncul di inspector
    [System.Serializable]
    public class AchievementData
    {
        public string Title;
        public AchievementType Type;
        public string Value;
        public bool IsUnlocked;
        public double gold;
    }
    public enum AchievementType
    {
        UnlockResource,
        UnlockGoal
    }

}
