using NSubstitute;
using NUnit.Framework;

namespace CleverCrow.UiNodeBuilder.Editors {
    public class NodeTest {
        public class PurchasedProperty {
            [Test]
            public void Calling_purchase_triggers_Enable_on_all_children () {
                var node = new Node();
                var child = Substitute.For<INode>();
                node.Children.Add(child);

                node.Purchased = true;

                child.Received(1).Enable();
            }
        }
    }
}