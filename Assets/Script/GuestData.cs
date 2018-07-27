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
        public List<_Sentence> mysentences = new List<_Sentence>();
    }

    [System.Serializable]
    public class _Sentence
    {
        public string ID;
        public string sentence;
    }

    [System.Serializable]
    public class _need
    {
        public CardManager.CardName _cardName;
        public int Number;
    }
    #endregion

    #region Editor方便編輯用
    public void SortID()
    {
        for (int i = 0; i < mytalks.Count; i++)
        {
            mytalks[i].level = i;
            for (int j = 0; j < mytalks[i].mysentences.Count; j++)
            {
                mytalks[i].mysentences[j].ID = string.Format("{0}{1}", mytalks[i].level, j);
            }
        }
    }
	//in
    public int _level = 0;//要新增句子的level
    public string _sec = string.Empty;//要新增的句子

	//out
    public string _id = string.Empty;//拿來看的
    public bool IsRemove = false;
    public void AddSentence()
    {
        SortID();

        _talk t = mytalks.Find(x => x.level == _level);
        if (t == null)//沒有此LEVEL的情況
        {
            t = new _talk();
            t.level = mytalks.Count;
            _level = t.level;
            mytalks.Add(t);
            Debug.LogWarning("沒有此LEVEL的數據，將新增一個");
        }
        _Sentence s = new _Sentence();
        s.ID = string.Format("{0}{1}", t.level, t.mysentences.Count);
        _id = s.ID;
        s.sentence = _sec;
        t.mysentences.Add(s);
    }

	public void RemoveSentence()
    {
        SortID();

        foreach (var item in mytalks)
        {
            _Sentence s = item.mysentences.Find(x => x.ID == _id);
            if (s != null)
            {
                item.mysentences.Remove(s);
                return;
            }
			else
			{
                Debug.LogWarning("沒有該數據");
            }
        }
    }
    #endregion

}
