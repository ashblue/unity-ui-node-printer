using System;
using NUnit.Framework;
using UnityEngine;

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
            private Sprite _graphic = Sprite.Create(new Texture2D(1, 1), new Rect(), Vector2.zero);
            
            [Test]
            public void It_should_add_a_node_with_the_passed_name () {
                var nodeName = "Node Name";
                var graph = new NodeGraphBuilder()
                    .Add(nodeName, _graphic, () => { })
                    .Build();
                
                Assert.AreEqual(graph.Root.Children[0].Name, nodeName);
            }
            
            [Test]
            public void It_should_add_a_node_with_the_passed_graphic () {
                var graph = new NodeGraphBuilder()
                    .Add("Lorem Ipsum", _graphic, () => { })
                    .Build();
                
                Assert.AreEqual(graph.Root.Children[0].Graphic, _graphic);
            }

            [Test]
            public void It_should_add_a_node_with_the_passed_callback () {
                var callback = new Action(() => { });
                var graph = new NodeGraphBuilder()
                    .Add("Example", _graphic, callback)
                    .Build();
                
                Assert.AreEqual(graph.Root.Children[0].OnClick, callback);
            }
        }
    }
}