using MODEL.EasyUIModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL
{
    /// <summary>
    /// 扩展 Ou_Permission
    /// </summary>
    public partial class Ou_Permission
    {
        public Ou_Permission ToPOCO()
        {
            Ou_Permission poco = new Ou_Permission()
            {
                pid = this.pid,
                pParent = this.pParent,
                pName = this.pName,
                pAreaName = this.pAreaName,
                pControllerName = this.pControllerName,
                pActionName = this.pActionName,
                pFormMethod = this.pFormMethod,
                pOperationType = this.pOperationType,
                pIco = this.pIco,
                pOrder = this.pOrder,
                pIsLink = this.pIsLink,
                pLinkUrl = this.pLinkUrl,
                pIsShow = this.pIsShow,
                pRemark = this.pRemark,
                pIsDel = this.pIsDel,
                pAddTime = this.pAddTime
            };

            return poco;
        }

        /// <summary>
        /// 将当前权限 对象 转成 树节点对象
        /// </summary>
        /// <param name="per">前权限 对象</param>
        /// <returns>树节点对象</returns>
        public TreeNode ToTreeNode()
        {
            TreeNode node = new TreeNode()
            {
                id = this.pid,
                text = this.pName,
                state = "open",
                Checked = false,
                attributes = new { url = this.GetUrl() },
                children = new List<TreeNode>()
            };
            return node;
        }

        /// <summary>
        /// 将 当前权限的 区域名+控制器名+Action方法名 合并成一个url返回
        /// </summary>
        /// <returns></returns>
        protected string GetUrl()
        {
            return GetUrlPart(this.pAreaName)
                + GetUrlPart(this.pControllerName)
                + GetUrlPart(this.pActionName);
        }

        /// <summary>
        /// 拼接部分Url
        /// </summary>
        /// <param name="urlPart"></param>
        /// <returns></returns>
        protected string GetUrlPart(string urlPart)
        {
            return string.IsNullOrEmpty(urlPart) ? "" : "/" + urlPart;
        }

        #region 递归权限集合 创建 树节点集合 + static void LoadTreeNode(List<Ou_Permission> listPer, List<TreeNode> listNodes, int pid)
        /// <summary>
        /// 递归权限集合 创建 树节点集合
        /// </summary>
        /// <param name="listPer">权限对象集合</param>
        /// <param name="listNodes">树节点对象集合</param>
        /// <param name="pid">父ID</param>
        public static void LoadTreeNode(List<Ou_Permission> listPer, List<TreeNode> listNodes, int pid)
        {
            foreach (var permission in listPer)
            {
                //如果父Id == 参数
                if (permission.pParent == pid )
                {
                    //将权限对象转换成树节点对象
                    TreeNode node = permission.ToTreeNode();
                    //将节点 加入到 树节点集合
                    listNodes.Add(node);
                    //递归 为这个新创建的 树节点找 子节点
                    LoadTreeNode(listPer, node.children, node.id);
                }
            } 
        }
        #endregion


        #region 2.0 将 权限集合 转成 树节点集合 +List<MODEL.EasyUIModel.TreeNode> ToTreeNodes(List<Ou_Permission> listPer)
        /// <summary>
        /// 将 权限集合 转成 树节点集合
        /// </summary>
        /// <param name="listPer"></param>
        /// <returns></returns>
        public static List<MODEL.EasyUIModel.TreeNode> ToTreeNodes(List<Ou_Permission> listPer)
        {
            List<MODEL.EasyUIModel.TreeNode> listNodes = new List<EasyUIModel.TreeNode>();
            //生成 树节点时，根据 pid=0的根节点 来生成
            LoadTreeNode(listPer, listNodes, 0);
            return listNodes;
        }
        #endregion
    }
}
