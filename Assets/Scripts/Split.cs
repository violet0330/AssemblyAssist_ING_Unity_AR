using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Split : MonoBehaviour
{
    public Transform m_ChildPointParent;//要移动的子对象的父物体
    private List<GameObject> m_Child;//所有子对象
    private List<Vector3> m_InitPoint = new List<Vector3>();//初始位置
    private Vector3 m_InitScale = new Vector3();//初始大小

    public Transform m_TargetPointParent;//目标点对象的父物体
    private List<GameObject> m_TargetChild;//目标点所有子对象
    private List<Vector3> m_TargetPoint = new List<Vector3>();//要移动的位置
    private Vector3 m_TargetScale = new Vector3();//要移动的大小

    private void Start()
    {
        Initial(m_ChildPointParent, m_TargetPointParent);
    }

    internal void Initial(Transform Child, Transform Target)
    {
        m_Child = GetChild(Child);//获取所有子对象
        for (int i = 0; i < m_Child.Count; i++)
        {
            m_InitPoint.Add(m_Child[i].transform.position);
        }
        m_InitScale = Child.transform.localScale;

        m_TargetChild = GetChild(Target);//获取所有目标点子对象
        for (int i = 0; i < m_TargetChild.Count; i++)
        {
            m_TargetPoint.Add(m_TargetChild[i].transform.position);
        }
        m_TargetScale = Target.transform.localScale;
    }

    //获取所有子对象
    public List<GameObject> GetChild(Transform obj)
    {
        List<GameObject> tempArrayobj = new List<GameObject>();
        foreach (Transform child in obj)
        {
            tempArrayobj.Add(child.gameObject);
        }
        return tempArrayobj;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            //拆分
            SplitObject();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            //合并
            MergeObject();
        }
    }

    internal void SplitObject()
    {
        Debug.Log("split");
        for (int i = 0; i < m_Child.Count; i++)
        {
            m_Child[i].transform.DOMove(m_TargetPoint[i], 2f, false);

        }
        m_ChildPointParent.transform.DOScale(m_TargetScale, 2f);
    }

    internal void MergeObject()
    {
        Debug.Log("merge");
        for (int i = 0; i < m_InitPoint.Count; i++)
        {
            m_Child[i].transform.DOMove(m_InitPoint[i], 2f, false);
        }
        m_ChildPointParent.transform.DOScale(m_InitScale, 2f);

    }
}
