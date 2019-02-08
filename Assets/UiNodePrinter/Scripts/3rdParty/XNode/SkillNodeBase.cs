using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CleverCrow.UiNodeBuilder.ThirdParty.XNodes {
    public abstract class SkillNodeBase : XNode.Node, ISkillNode {
        [Serializable]
        public class Connection {}
        
        public virtual string DisplayName { get; }
        public virtual string Description { get; }
        public virtual Sprite Graphic { get; }
        public virtual int Priority { get; }
        public virtual NodeType NodeType { get; }
        public virtual bool IsPurchased { get; }
        public virtual bool IsGroup { get; }
        public virtual int RequiredLevel { get; }
        public virtual bool Hide { get; }

        public ISkillNode GroupEnd {
            get {
                var port = GetOutputPort("exit");
                if (port.ConnectionCount == 0) return null;
                
                return port.GetConnection(0).node as ISkillNode;
            }
        }

        public List<ISkillNode> Children {
            get {
                var port = GetOutputPort("children");
                var list = new List<ISkillNode>();
                for (var i = 0; i < port.ConnectionCount; i++) {
                    list.Add(port.GetConnection(i).node as ISkillNode);
                }
    
                return list;
            }
        }
        
        public List<ISkillNode> GetSortedChildren () {
            return Children.OrderByDescending(child => child.Priority).ToList();
        }
    }
}
