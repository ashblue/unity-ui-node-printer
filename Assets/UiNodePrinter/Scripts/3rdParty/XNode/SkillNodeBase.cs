using System;
using System.Collections.Generic;
using UnityEngine;

namespace CleverCrow.UiNodeBuilder.ThirdParty.XNodes {
    public abstract class SkillNodeBase : XNode.Node, ISkillNode {
        [Output]
        public Connection exit;
        
        [Serializable]
        public class Connection {}
        
        public virtual string DisplayName { get; }
        public virtual string Description { get; }
        public virtual Sprite Graphic { get; }
        public virtual NodeType NodeType { get; }
        public virtual bool Purchased { get; }
        public virtual int RequiredLevel { get; }
        public virtual bool HideRequiredLevel { get; }
        
        public List<ISkillNode> Children {
            get {
                var port = GetOutputPort("exit");
                var list = new List<ISkillNode>();
                for (var i = 0; i < port.ConnectionCount; i++) {
                    list.Add(port.GetConnection(i).node as ISkillNode);
                }
    
                return list;
            }
        }
    }
}
