using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Newtonsoft.Json;
namespace DataStructureBasic
{
    public static class TreeBaseContextExtension
    {
        //public static Func<TreeBase, bool> showNormalData(this Func<TreeBase, bool> predicate)
        //{


        //}
    }
    public class TreeBaseContext
    {
        private static int currentNewTreeId = 0;


        private int getTreeId
        {
            get
            {
                currentNewTreeId++;
                return currentNewTreeId;
            }
        }

        public static List<TreeBase> AccountTree { get; set; } = new List<TreeBase>();

        public TreeBase AddNewNode(string name)
        {
            if (AccountTree.Where(x => x.ParentId != null).Count() > 0)
            {
                throw new Exception("只允许一个根节点");
            }
            int thisTreeId = getTreeId;
            TreeBase data = new TreeBase();
            data.Id = thisTreeId;
            data.Name = name;
            data.Depth = 1;
            data.LValue = 1;
            data.RValue = 2;
            data.ObjId = Guid.NewGuid().ToString();
            AccountTree.Add(data);
            return data;
        }


        public TreeBase AddChildNode(int parentId, string nodeName)
        {
            var parentNode = AccountTree.Where(x => x.Id == parentId).ToList()[0];
            if (parentNode != null)
            {
                int thisTreeId = getTreeId;
                TreeBase data = new TreeBase();
                //depth calc : GetAncestorNode method calc result + 1(parent) +1(this)
                data.Depth = GetAncestorNode(parentId).Count() + 1 + 1;
                data.Id = thisTreeId;
                data.Name = nodeName;
                data.ObjId = Guid.NewGuid().ToString();
                data.ParentId ??= parentId;
                //新节点的左值等于上级节点的右值，新节点右值等于上级节点右值+1（占位）   每个节点最小占两数字
                data.LValue = parentNode.RValue;
                data.RValue = data.LValue + 1;
                var refNodes = AccountTree.Where(x => x.RValue >= parentNode.RValue).ToList();//（大于Rvalue=上级节点）&& （在前面的同级节点不需要修复）在后面的同级节点
                for (int idx = refNodes.Count - 1; idx >= 0; idx--)
                {
                    var cur = refNodes[idx];
                    //支线上只改变右值
                    if (cur.LValue <= parentNode.LValue)//<= is 包含（上级节点）
                    {
                        cur.RValue += 2;
                    }
                    else//后面的节点
                    {
                        cur.LValue += 2;
                        cur.RValue += 2;
                    }
                    AccountTree[AccountTree.FindIndex(x => x.Id == cur.Id)] = cur;
                }
                //添加新节点
                AccountTree.Add(data);
                return data;
            }
            return null;
        }

        /// <summary>
        /// 获取上级节点
        /// </summary>
        /// <param name="node"></param>
        /// <param name="nodeTypes"></param>
        /// <param name="includeCurrentNode"></param>
        /// <returns></returns>
        public IEnumerable<TreeBase> GetAncestorNode(int Id)
        {
            var node = AccountTree.Single((x => x.Id == Id));
            var ids = new List<int>();
            var lastNode = node;
            while (true)
            {
                if (!string.IsNullOrWhiteSpace(lastNode.ParentId.ToString()))
                {
                    var parentNode = AccountTree.Single(x => x.Id == lastNode.ParentId);
                    if (parentNode != null)
                    {
                        ids.Add(parentNode.Id);
                        lastNode = parentNode;
                    }
                    else
                        break;
                }
                else
                {

                    if (lastNode.Id != node.Id)
                    {
                        {
                            ids.Add(lastNode.Id);
                        }
                    }

                    break;
                }
            }

            return from it in AccountTree
                   where ids.Contains(it.Id)
                   select it;

        }

        /// <summary>
        /// 获取下级节点
        /// </summary>
        /// <param name="node"></param>
        /// <param name="nodeTypes"></param>
        /// <param name="includeCurrentNode"></param>
        /// <returns></returns>
        public IEnumerable<TreeBase> GetDescendantNode(int Id)
        {
            var node = AccountTree.Single(x => x.Id == Id);
            return from it in AccountTree
                   where it.LValue > node.LValue && it.RValue < node.RValue
                   select it;
        }


        public void Show()
        {
            Console.WriteLine(JsonConvert.SerializeObject(AccountTree.Select(x => new { Id = x.Id, Name = x.Name, LValue = x.LValue, RValue = x.RValue })));
        }




    }
}
