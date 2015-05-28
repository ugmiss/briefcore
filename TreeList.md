## 回调类 ##
```
    /// <summary>
    /// 这个类用于树的回调，获取叶子选中状态的节点集
    /// GetAll为真时，取得半选和全选(默认)
    /// 否则返回只有打钩的项
    /// </summary>
    public class XOperationCheckStateEmun : TreeListOperation
    {
        List<TreeListNode> xList = new List<TreeListNode>();
        public bool GetAll = true;
        public override void Execute(TreeListNode node)
        {
           if (GetAll)
           {
                if (node.CheckState != CheckState.Unchecked)
                {
                    xList.Add(node);
                }
            }
            else
            {
                if (node.CheckState == CheckState.Checked)
                {
                    xList.Add(node);
                }
            }
        }
        public List<TreeListNode> XList
        {
            get { return xList; }
        }
    }
```
## 具体使用方法 ##
```
 /// <summary>

        /// 取得树型控件多选项的数目 ;

        /// </summary>

        /// <param name="treelist">树型控件</param>

        /// <returns>返回选中状态的列表</returns>

        public static List<TreeListNode> GetSelectNodes(TreeList treelist)

        {

            XOperationCheckStateEmun CheckStateEmun = new XOperationCheckStateEmun();

            treelist.NodesIterator.DoOperation(CheckStateEmun);

            return CheckStateEmun.XList;

        }
```