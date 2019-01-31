using NSubstitute;
using NUnit.Framework;

namespace CleverCrow.UiNodeBuilder.Editors {
    public class NodeGraphTest {
        public class RootProperty {
            [Test]
            public void It_should_be_populated_by_default () {
                var graph = new NodeGraph();
                Assert.NotNull(graph.Root);
            }
        }

        public class AddNodeMethod {
            [Test]
            public void It_should_add_a_node_to_the_root () {
                var graph = new NodeGraph();
                var node = Substitute.For<INode>();
                
                graph.AddNode(graph.Root, node);

                Assert.IsTrue(graph.Root.Children.Contains(node));
            }
        }
    }
}
