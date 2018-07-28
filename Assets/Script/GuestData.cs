using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class GuestData : ScriptableObject
{
    #region 我的Data
    public GuestManager.GuestName Guest_Name;

    public List<_talk> mytalks = new List<_talk>();
    public List<_need> myneeds = new List<_need>();

    [System.Serializable]
    public class _talk
    {
        public int level;
        public Question questiom;
        public List<Answer> answers = new List<Answer>(1);
    }

    [System.Serializable]
    public class Question
    {
        public string sentence;
        public List<CardManager.CardName> CardOpens = new List<CardManager.CardName>(1);
    }

    [System.Serializable]
    public class Answer
    {
        public string sentence;
        public int OpenLevel;
        public CardManager.CardName NeedToTrigger;
    }

    [System.Serializable]
    public class _need
    {
        public CardManager.CardName _cardName;
        public int Number;
    }
    #endregion

    #region Editor方便編輯用
    public void SortLevel()
    {
        for (int i = 0; i < mytalks.Count; i++)
        {
            mytalks[i].level = i;
            for (int j = 0; i < mytalks[i].answers.Count; i++)
            {
                if (mytalks[i].answers[j].OpenLevel >= mytalks.Count)
                {
                    mytalks[i].answers[j].OpenLevel = mytalks.Count - 1;
                    Debug.LogErrorFormat("mytalks{0}.answers{1}.OpenLevel 超過mytalks.Count，已修改成0；mytalks.Count數量 : {2}", i, j, mytalks.Count - 1);
                }
            }
        }
    }
    //in
    public int _level = 0;//要找句子的level
    public Question _question = new Question();
    public List<Answer> _answers = new List<Answer>();

    public bool IsRemove = false;
    public void AddSentence()
    {
        SortLevel();

        _talk t = mytalks.Find(x => x.level == _level);
        if (t == null)//沒有此LEVEL的情況
        {
            t = new _talk();
            t.level = mytalks.Count;
            _level = t.level;
            mytalks.Add(t);
            Debug.LogWarning("沒有此LEVEL的數據，將新增一個");
        }
        t.questiom = _question;
        t.answers = _answers;

        /*_level = 0;
        _question = new Question();
        _answers = new List<Answer>();*/
    }

    public void RemoveLevel()
    {
        SortLevel();

        if (mytalks.Contains(mytalks[_level]))
        {
            mytalks.RemoveAt(_level);
        }
        else
        {
            Debug.LogWarning("沒有該數據");
        }
    }
    #endregion

}
