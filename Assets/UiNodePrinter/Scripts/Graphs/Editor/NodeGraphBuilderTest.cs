using System;
using NUnit.Framework;
using UnityEngine;

namespace CleverCrow.UiNodeBuilder.Editors {
    public class NodeGraphBuilderTest {
        private Sprite _graphic = Sprite.Create(new Texture2D(1, 1), new Rect(), Vector2.zero);

        public class BuildMethod {
            [Test]
            public void It_should_create_a_NodeGraph () {
                var graph = new NodeGraphBuilder().Build();
                Assert.IsTrue(graph is NodeGraph);
            }
        }
        
        public class AddMethod : NodeGraphBuilderTest {
            [Test]
            public void It_should_add_a_node_with_the_passed_name () {
                var nodeName = "Node Name";
                var graph = new NodeGraphBuilder()
                    .Add(nodeName, _graphic)
                    .Build();
                
                Assert.AreEqual(graph.Root.Children[0].Name, nodeName);
            }
            
            [Test]
            public void It_should_add_a_node_with_the_passed_graphic () {
                var graph = new NodeGraphBuilder()
                    .Add("Lorem Ipsum", _graphic)
                    .Build();
                
                Assert.AreEqual(graph.Root.Children[0].Graphic, _graphic);
            }

            [Test]
            public void It_should_add_the_node_to_the_Nodes_property () {
                var graph = new NodeGraphBuilder()
                    .Add("Node Name", _graphic)
                    .Build();
                
                Assert.IsTrue(graph.Nodes.Contains(graph.Root.Children[0]));
            }
        }

        public class OnClickNodeMethod : NodeGraphBuilderTest {
            [Test]
            public void It_should_add_an_OnClickNode_method_to_the_node_pointer () {
                var callback = new Action<INode>((node) => { });
                var graph = new NodeGraphBuilder()
                    .Add("Node Name", _graphic)
                    .OnClickNode(callback)
                    .Build();
                
                Assert.AreEqual(graph.Root.Children[0].OnClick, callback);
            }
        }

        public class EndMethod : NodeGraphBuilderTest {
            [Test]
            public void If_not_used_should_point_to_the_root () {
                var builder = new NodeGraphBuilder();
                var graph = builder.Build();
                
                Assert.AreEqual(builder.Pointer.Peek(), graph.Root);

            }
            
            [Test]
            public void It_should_move_to_the_previous_pointer () {
                var builder = new NodeGraphBuilder()
                    .Add("Node Name", _graphic)
                    .End();
                var graph = builder.Build();
                
                
                Assert.AreEqual(builder.Pointer.Peek(), graph.Root);
            }
        }
    }
}