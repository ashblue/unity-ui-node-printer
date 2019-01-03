using System;
using NUnit.Framework;

namespace CleverCrow.UiNodeBuilder.Editors {
    public class NodeGraphBuilderTest {
        public class BuildMethod {
            [Test]
            public void It_should_create_a_NodeGraph () {
                var graph = new NodeGraphBuilder().Build();
                Assert.IsTrue(graph is NodeGraph);
            }
        }
        
        public class AddMethod {
            [Test]
            public void It_should_add_a_node_with_the_passed_name () {
                var nodeName = "Node Name";
                var graph = new NodeGraphBuilder()
                    .Add(nodeName, status => { })
                    .Build();
                
                Assert.AreEqual(graph.Root.Children[0].Name, nodeName);
            }

            [Test]
            public void It_should_add_a_node_with_the_passed_callback () {
                var callback = new Action<bool>(status => { });
                var graph = new NodeGraphBuilder()
                    .Add("Example", callback)
                    .Build();
                
                Assert.AreEqual(graph.Root.Children[0].OnClick, callback);
            }
        }
    }
}