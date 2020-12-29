using System;
using System.Collections;
using System.Collections.Generic;
//using Sirenix.OdinInspector;
using UnityEngine;
using Axis = UnityEngine.UI.GridLayoutGroup.Axis;
//间隙
[Serializable]
public class Padding
{
    public float Left;
    public float Right;
    public float Top;
    public float Bottom;

    public Padding()
    {
        Left = 0;
        Right = 0;
        Top = 0;
        Bottom = 0;
    }
}
//水平排列
public enum HorizontalSortSequence
{
    LeftToRight,//左到右排列
    RightToLeft,//右到左排列
}
//垂直排列顺序
public enum VerticalSortSequence
{
    TopToBottom,//上到下
    BottomToTop,//底到上
}


public delegate void OnUpdateDataItemHandle(object data, UIComponet com);//更新数据物体时触发的回调
/// <summary>
/// 无限循环列表
/// </summary>
public class UILoopList : MonoBehaviour
{

    public const int EXPAND_SIZE = 2;//单侧拓展数量
    //视窗
   // [ValidateInput("ViewPortIsVaild", "ViewPort con't be null", InfoMessageType.Error)]
    public RectTransform ViewPort;
    //四周间隙
    public Padding Padding = new Padding();
    //组件大小
    public Vector2 CellSize = new Vector2(100, 100);
    //组件间隔
    public Vector2 Spacing = Vector2.zero;
    //默认横向排列
    public Axis StartAxis = Axis.Horizontal;
   // [ShowIf("IsHorizontal")]
    //水平排列
    public HorizontalSortSequence HorizontalSortSequence = HorizontalSortSequence.LeftToRight;
   // [ShowIf("IsVertical")]
    //竖直排列
    public VerticalSortSequence VerticalSortSequence = VerticalSortSequence.TopToBottom;
    //约束数量
  //  [ValidateInput("ConstraintCountIsVaild", "ConstraintCount Must >0")]
    public int ConstraintCount = 1;
    //数据列表
    public IList DataList;
    //rectTran组件
    private RectTransform _rectTransform;
    //容器大小
    private Vector2 _contentSize;
    //生成的数量
    private int _createNum;
    //组件列表
    [SerializeField]
    private List<UIComponet> _listCom = new List<UIComponet>();
    [SerializeField]
    // 未使用的池
    private Stack<UIComponet> _unUsedPool = new Stack<UIComponet>();

    private float _lastMoveX, _lastMoveY;//上次移动的X坐标和Y坐标

    private bool _openSync;//开启滑动同步

    private OnUpdateDataItemHandle _overrideUpdateDataItemHandle = null;//更新数据对象覆盖回调

  ///  [LabelText("编辑模式下预览排列")]
    //编辑器模式下预览
    public bool PreviewInEditor = true;

    #region 属性
    //RectTransform获取
    public RectTransform RectTransform
    {
        get
        {
            if (_rectTransform == null)
            {
                _rectTransform = transform as RectTransform;
            }
            return _rectTransform;
        }
    }

    //视窗大小
    public Vector2 ViewPortSize
    {
        get { return ViewPort.rect.size; }
    }


    //元素数量
    public int CellCount
    {
        get
        {
            if (DataList == null)
            {
                return 0;
            }
            //数据大小
            return DataList.Count;

        }
    }

    //每行的最大数量,向上取整
    public int CellCountPerRow
    {
        get { return Mathf.CeilToInt((float)CellCount / ConstraintCount); }
    }

    //每列个最大数,向上取整
    public int CellCountPerColumn
    {
        get { return Mathf.CeilToInt((float)CellCount / ConstraintCount); }
    }

    //行总间隙
    public float RowTotalSpace
    {
        get
        {
            if (Spacing.x == 0)
            {
                return 0;
            }
            int spaceNum = 0;
            switch (StartAxis)
            {
                case Axis.Horizontal:
                    spaceNum = CellCountPerRow - 1;//横向排列，空隙数量为行个数-1
                    break;
                case Axis.Vertical://纵向排列
                    //取约束计算
                    spaceNum = ConstraintCount - 1;
                    break;
            }
            if (spaceNum > 0)
            {
                return Spacing.x * spaceNum;
            }
            else
            {
                return 0;
            }
        }

    }
    //列总间隙
    public float ColumnTotalSpace
    {
        get
        {
            if (Spacing.y == 0)
            {
                return 0;
            }
            int spaceNum = 0;
            switch (StartAxis)
            {
                case Axis.Horizontal://横向排列
                    //取约束计算
                    spaceNum = ConstraintCount - 1;
                    break;
                case Axis.Vertical://纵向排列
                    //取列最大元素计算空隙
                    spaceNum = CellCountPerColumn - 1;
                    break;
            }
            if (spaceNum > 0)
            {
                return Spacing.y * spaceNum;
            }
            else
            {
                return 0;
            }

        }
    }
    //元素总宽度
    public float TotalCellWdith
    {
        get
        {
            switch (StartAxis)
            {
                case Axis.Horizontal://横向排列
                    //取行最大个数计算
                    return CellCountPerRow * CellSize.x;
                case Axis.Vertical:
                    //取约束
                    return ConstraintCount * CellSize.x;
            }
            return 0;
        }
    }
    //元素总高度
    public float TotalCellHeight
    {
        get
        {
            switch (StartAxis)
            {
                case Axis.Horizontal://横向排列
                    //取行最大个数计算
                    return ConstraintCount * CellSize.y;
                case Axis.Vertical:
                    //取约束
                    return CellCountPerColumn * CellSize.y;
            }
            return 0;
        }
    }


    //符号系数，当为横向移动，返回-1，
    private int SignCoff
    {
        get
        {
            switch (StartAxis)
            {
                case Axis.Horizontal:
                    return -1;
                case Axis.Vertical:
                    return 1;
            }
            return 1;
        }
    }

    //当前索引
    public int CurIndex
    {
        get
        {
            int index = 0;
            switch (StartAxis)
            {
                case Axis.Horizontal://横向排列
                                     //减去间隙
                    index = (int)((SignCoff * ContentMovePosX - Padding.Left) / (CellSize.x + Spacing.x)) * ConstraintCount;
                    break;
                case Axis.Vertical://纵向排列
                                   //减去间隙
                    index = (int)((SignCoff * ContentMovePosY - Padding.Top) / (CellSize.y + Spacing.y)) * ConstraintCount;
                    break;
            }
            return index;
        }
    }

    //内容框X轴位置
    private float ContentMovePosX
    {
        get { return RectTransform.anchoredPosition.x; }
    }


    //内容框Y轴位置
    private float ContentMovePosY
    {
        get { return RectTransform.anchoredPosition.y; }
    }




    #endregion

    #region 校验设置
    //校验约束
    private bool ConstraintCountIsVaild(int value)
    {
        return value > 0;
    }
    //校验视窗
    private bool ViewPortIsVaild(RectTransform rect)
    {
        return rect != null;
    }
    //水平循环列表
    private bool IsHorizontal
    {
        get { return StartAxis == Axis.Horizontal; }
    }
    //竖直循环列表
    private bool IsVertical
    {
        get { return StartAxis == Axis.Vertical; }
    }

    #endregion

    //内容框大小
    public Vector2 CalculContentSize()
    {
        float width = 0, height = 0;
        switch (StartAxis)
        {
            case Axis.Horizontal:
                if (CellCountPerRow > 0)
                {
                    //获取约束               
                    //外围间隙+元素总宽度+行总间隙
                    width = Padding.Left + Padding.Right + TotalCellWdith + RowTotalSpace;
                    //外围间隙+元素总高度+列总间隙
                    height = Padding.Top + Padding.Bottom + TotalCellHeight + ColumnTotalSpace;
                }
                break;
            case Axis.Vertical:
                if (CellCountPerColumn > 0)
                {
                    //获取约束               
                    //外围间隙+元素总宽度+行总间隙
                    width = Padding.Left + Padding.Right + TotalCellWdith + RowTotalSpace;
                    //外围间隙+元素总高度+列总间隙
                    height = Padding.Top + Padding.Bottom + TotalCellHeight + ColumnTotalSpace;
                }
                break;
        }
        return new Vector2(width, height);
    }

    //计算需要创建的元素数量
    public int CalculNeedCreatCellNum()
    {
        switch (StartAxis)
        {
            case Axis.Horizontal:
                return (Mathf.CeilToInt((ViewPortSize.x - Padding.Left) / (CellSize.x + Spacing.x)) + EXPAND_SIZE * 2) * ConstraintCount;
            case Axis.Vertical:
                return (Mathf.CeilToInt((ViewPortSize.y - Padding.Top) / (CellSize.y + Spacing.y)) + EXPAND_SIZE * 2) * ConstraintCount;
        }
        return 0;
    }

    //更新逻辑
    public void Update()
    {
        if (!_openSync)
        {
            return;
        }
        switch (StartAxis)
        {
            case Axis.Horizontal://横向排列
                //如果X轴移动
                if (_lastMoveX != ContentMovePosX)
                {
                    RefreshDataItem();
                }
                break;
            case Axis.Vertical://纵向排列
                               //如果Y轴发生移动               
                if (_lastMoveY != ContentMovePosY)
                {
                    RefreshDataItem();
                }
                break;
        }


    }

    //通过索引定位元素位置
    public Vector2 GetCellPosition(int dataIndex)
    {
        float x = 0, y = 0;
        int row, column;//行号和列号
        switch (StartAxis)
        {
            case Axis.Horizontal:
                column = dataIndex / ConstraintCount;//在第几列，影响X
                row = dataIndex % ConstraintCount;//在第几行,影响Y                
                x = Spacing.x * column + Padding.Left + CellSize.x * (column + 0.5f);
                y = -Spacing.y * row - Padding.Top - CellSize.y * (row + 0.5f);
                break;
            case Axis.Vertical:
                row = dataIndex / ConstraintCount;//在第几行，影响Y                
                column = dataIndex % ConstraintCount;//在第几行,影响X     
                x = Spacing.x * column + Padding.Left + CellSize.x * (column + 0.5f);
                y = -Spacing.y * row - Padding.Top - CellSize.y * (row + 0.5f);
                //TODO
                break;
        }
        return new Vector2(x, y);
    }

    //放进未使用的池中
    private void PushToUnUsedPool(UIComponet com)
    {
        //放入池
        _unUsedPool.Push(com);

    }
    //池有组件
    private bool PoolHaveCom
    {
        get { return _unUsedPool.Count > 0; }
    }
    //有效组件
    public List<UIComponet> ValidListCom
    {
        get
        {
            List<UIComponet> result = new List<UIComponet>();
            for (int i = 0; i < _listCom.Count; i++)
            {
                if (_listCom[i].IsValidInLoopList)
                {
                    result.Add(_listCom[i]);
                }
            }
            return result;
        }
    }

    //有效索引区间
    private void ValidIndexRange(int curIndex, out int min, out int max)
    {
        //最小索引
        min = curIndex - EXPAND_SIZE * ConstraintCount;
        //最大索引=最小+总数-1
        max = min + _createNum - 1;
    }

    //回收超范围的组件,返回需要创建的新索引列表
    private List<int> RecyleOutScopeCom(int minIndex, int maxIndex)
    {
        //新数据索引
        List<int> newIndexs = new List<int>();
        for (int i = minIndex; i <= maxIndex; i++)
        {
            newIndexs.Add(i);
        }
        for (int i = _listCom.Count - 1; i >= 0; i--)
        {
            //如果组件索引不在有效区间
            if (!(_listCom[i].DataIndex >= minIndex && _listCom[i].DataIndex <= maxIndex))
            {
                //回收到未使用的池中
                PushToUnUsedPool(_listCom[i]);
                //从列表中去除此组件
                _listCom.RemoveAt(i);
            }
            else
            {
                //存在，剔除
                newIndexs.Remove(_listCom[i].DataIndex);
            }
        }
        return newIndexs;
    }

    //刷新数据
    private void RefreshDataItem()
    {
        // Debug.Log("当前索引："+CurIndex);
        //更新数据
        UpdateDataItem(CurIndex);
        //记录坐标
        _lastMoveX = ContentMovePosX;
        _lastMoveY = ContentMovePosY;
    }

    //更新数据
    public void UpdateDataItem(int curIndex)
    {
        int minIndex, maxIndex;
        ValidIndexRange(curIndex, out minIndex, out maxIndex);
        //   Debug.Log("索引范围："+ minIndex+"    "+ maxIndex);
        //回收超范围的组件
        List<int> newIndexs = RecyleOutScopeCom(minIndex, maxIndex);
        //更新新索引对应的组件
        for (int i = 0; i < newIndexs.Count; i++)
        {
            if (PoolHaveCom)
            {
                UIComponet com = _unUsedPool.Pop();
                _listCom.Add(com);
                //定位元素
                if (com.SycnValidAndPositionInUILoopList(newIndexs[i], this))
                {
                    //触发覆盖回调
                    if (_overrideUpdateDataItemHandle != null)
                    {
                        _overrideUpdateDataItemHandle(DataList[newIndexs[i]], com);
                    }
                    else
                    {
                        com.UpdateDataItem(DataList[newIndexs[i]]);
                    }
                }
            }
        }

    }
    //逆序
    private void ReverseDataList()
    {
        //一共有几对
        int dui = DataList.Count / 2;
        for (int i = 0; i < dui; i++)
        {
            var tem = DataList[i];
            DataList[i] = DataList[DataList.Count - i - 1];
            DataList[DataList.Count - i - 1] = tem;
        }
    }

    //内容栏重置大小
    private void ResetContentSize()
    {
        //设置contetn大小
        RectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 0);
        RectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0);
    }

    #region 外部调用

    //绑定数据列表并显示
    public void BIndDataList<T>(IList iList, UIComType comType, OnUpdateDataItemHandle overrideUpdateDataItemHandle = null) where T : UIComponet
    {
        //清理当前的列表
        RecyleAndClearList();
        //设置数据列表
        DataList = iList;
        _overrideUpdateDataItemHandle = overrideUpdateDataItemHandle;
        //逆序排列
        bool needReverse = false;
        if (StartAxis == Axis.Horizontal && HorizontalSortSequence == HorizontalSortSequence.RightToLeft)
        {
            needReverse = true;
            ReverseDataList();
        }
        else if (StartAxis == Axis.Vertical && VerticalSortSequence == VerticalSortSequence.BottomToTop)
        {
            needReverse = true;
            ReverseDataList();
        }
        //初始化content大小
        _contentSize = CalculContentSize();
        //设置contetn大小
        RectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _contentSize.x);
        RectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, _contentSize.y);
        //计算动态组件需要创建的数量
        _createNum = CalculNeedCreatCellNum();
        //动态生成组件                
        for (int i = 0; i < _createNum; i++)
        {
            //暂时用元素组件测试
            T t = UIComponet.CreateInstance<T>(comType, RectTransform);
            //锚点为左上角
            RectTransformExtensions.SetAnchor(t.RectTran, AnchorPresets.TopLeft);
            //设置大小
            t.RectTran.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, CellSize.x);
            t.RectTran.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, CellSize.y);
            //加入列表
            _listCom.Add(t);
            //重新定位
            bool isSuccess = t.SycnValidAndPositionInUILoopList(i, this);
            if (isSuccess)
            {
                //如果更新数据成功,且有更新回调
                if (_overrideUpdateDataItemHandle != null)
                {
                    //执行回调
                    _overrideUpdateDataItemHandle(DataList[i], t);
                }
                else
                {
                    //更新数据
                    t.UpdateDataItem(DataList[i]);
                }
            }

        }
        //从新定位
        if (needReverse)
        {
            switch (StartAxis)
            {
                case Axis.Horizontal:
                    RectTransform.anchoredPosition = new Vector2(SignCoff * (_contentSize.x - ViewPortSize.x),
                        RectTransform.anchoredPosition.y);
                    break;
                case Axis.Vertical:
                    RectTransform.anchoredPosition = new Vector2(RectTransform.anchoredPosition.x,
                        SignCoff * (_contentSize.y - ViewPortSize.y));
                    break;
            }

        }
        else
        {
            switch (StartAxis)
            {
                case Axis.Horizontal:
                    RectTransform.anchoredPosition = new Vector2(0,
                        RectTransform.anchoredPosition.y);
                    break;
                case Axis.Vertical:
                    RectTransform.anchoredPosition = new Vector2(RectTransform.anchoredPosition.x, 0);
                    break;
            }
        }

        //更新数据
        RefreshDataItem();
        //开启更新同步
        _openSync = true;
    }

    //回收并清空列表
    public void RecyleAndClearList()
    {
        for (int i = 0; i < _listCom.Count; i++)
        {
            _listCom[i].Recycle();
        }
        _listCom.Clear();
        while (PoolHaveCom)
        {
            _unUsedPool.Pop().Recycle();
        }
        //关闭更新同步
        _openSync = false;
        ResetContentSize();
        _overrideUpdateDataItemHandle = null;
    }

    #endregion

    #region 编辑模式下使用

    void OnDrawGizmos()
    {
        if (!PreviewInEditor)
        {
            return;
        }
        if (Application.isPlaying)
        {
            return;
        }
        List<RectTransform> childs = new List<RectTransform>();
        //定位预览
        foreach (Transform child in transform)
        {
            if (child.gameObject.activeSelf == true)
            {
                childs.Add(child as RectTransform);
            }
        }
        if (childs.Count <= 0)
        {
            return;
        }
        //逆序排列
        bool needReverse = false;
        if (StartAxis == Axis.Horizontal && HorizontalSortSequence == HorizontalSortSequence.RightToLeft)
        {
            //逆序排列
            childs.Reverse();
        }
        else if (StartAxis == Axis.Vertical && VerticalSortSequence == VerticalSortSequence.BottomToTop)
        {
            //逆序排列
            childs.Reverse();
        }
        //同步位置
        for (int i = 0; i < childs.Count; i++)
        {
            //锚点为左上角
            RectTransformExtensions.SetAnchor(childs[i], AnchorPresets.TopLeft);
            //设置大小
            childs[i].SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, CellSize.x);
            childs[i].SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, CellSize.y);
            //定位
            childs[i].localPosition = GetCellPosition(i);
        }
    }

    #endregion







}
