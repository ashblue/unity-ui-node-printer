using NUnit.Framework;
using UnityEngine;

namespace CleverCrow.UiNodeBuilder.Editors {
    public class NodeGraphBuilderTest {
        private Sprite _graphic = Sprite.Create(new Texture2D(1, 1), new Rect(), Vector2.zero);
        private NodeGraphBuilder _builder;

        [SetUp]
        public void BeforeEach () {
            _builder = new NodeGraphBuilder();
        }

        public class BuildMethod : NodeGraphBuilderTest {
            [Test]
            public void It_should_create_a_NodeGraph () {
                var graph = _builder.Build();
                Assert.IsTrue(graph is NodeGraph);
            }
        }
        
        public class AddMethod : NodeGraphBuilderTest {
            public class AddingNodes : NodeGraphBuilderTest {
                [Test]
                public void It_should_add_a_node_with_the_passed_name () {
                    var nodeName = "Node Name";
                    var graph = _builder
                        .Add(nodeName, _graphic)
                        .Build();
                
                    Assert.AreEqual(graph.Root.Children[0].Name, nodeName);
                }
            
                [Test]
                public void It_should_add_a_node_with_the_passed_graphic () {
                    var graph = _builder
                        .Add("Lorem Ipsum", _graphic)
                        .Build();
                
                    Assert.AreEqual(graph.Root.Children[0].Graphic, _graphic);
                }

                [Test]
                public void It_should_add_the_node_to_the_Nodes_property () {
                    var graph = _builder
                        .Add("Node Name", _graphic)
                        .Build();
                
                    Assert.IsTrue(graph.Nodes.Contains(graph.Root.Children[0]));
                }

                [Test]
                public void It_should_add_a_child_if_Add_is_called_again () {
                    var graph = _builder
                        .Add("A", _graphic)
                        .Add("B", _graphic)
                        .Build();
                
                    Assert.AreEqual(graph.Root.Children[0].Children[0].Name, "B");
                }
            }

            public class EnabledStatus : NodeGraphBuilderTest {
                [Test]
                public void First_added_node_is_enabled () {
                    var graph = _builder
                        .Add("A", _graphic)
                        .Build();
                    
                    Assert.IsTrue(graph.Root.Children[0].IsEnabled);
                }
                
                [Test]
                public void Child_node_is_disabled () {
                    var graph = _builder
                        .Add("A", _graphic)
                        .Add("B", _graphic)
                        .Build();
                    
                    Assert.IsFalse(graph.Root.Children[0].Children[0].IsEnabled);
                }

                [Test]
                public void Child_node_is_enabled_if_parent_is_Purchased () {
                    var graph = _builder
                        .Add("A", _graphic)
                            .Purchased(true)
                            .Add("B", _graphic)
                        .Build();
                    
                    Assert.IsTrue(graph.Root.Children[0].Children[0].IsEnabled);
                }
            }
        }

        public class IsPurchasable : NodeGraphBuilderTest {
            [Test]
            public void It_should_default_to_true_if_IsPurchasable_is_not_set () {
                var graph = _builder
                    .Add("Node Name", _graphic)
                    .Build();
                
                Assert.AreEqual(graph.Root.Children[0].IsPurchasable, true);
            }
            
            [Test]
            public void It_should_set_IsPurchasable_to_false () {
                var graph = _builder
                    .Add("Node Name", _graphic)
                    .IsPurchasable(() => false)
                    .Build();
                
                Assert.AreEqual(graph.Root.Children[0].IsPurchasable, false);
            }
            
            [Test]
            public void It_should_set_IsPurchasable_to_true () {
                var graph = _builder
                    .Add("Node Name", _graphic)
                    .IsPurchasable(() => true)
                    .Build();
                
                Assert.AreEqual(graph.Root.Children[0].IsPurchasable, true);
            }
        }

        public class EndMethod : NodeGraphBuilderTest {
            [Test]
            public void If_not_used_should_point_to_the_root () {
                var builder = _builder;
                var graph = builder.Build();
                
                Assert.AreEqual(builder.Pointer.Peek(), graph.Root);

            }
            
            [Test]
            public void It_should_move_to_the_previous_pointer () {
                var builder = _builder
                    .Add("Node Name", _graphic)
                    .End();
                var graph = builder.Build();
                
                
                Assert.AreEqual(builder.Pointer.Peek(), graph.Root);
            }
        }
        
        public class IsLockedMethod : NodeGraphBuilderTest {
            [Test]
            public void It_should_override_the_OnIsLocked_method () {
                var graph = _builder
                    .Add("Node Name", _graphic)
                    .IsLocked(() => true)
                    .Build();
                
                Assert.AreEqual(graph.Root.Children[0].IsLocked, true);
            }
        }

        public class LockedDescription : NodeGraphBuilderTest {
            [Test]
            public void It_should_set_the_locked_description_according_to_the_callback () {
                const string text = "Lorem ipsum";
                
                var graph = _builder
                    .Add("Node", _graphic)
                    .LockedDescription(() => text)
                    .Build();
                
                Assert.AreEqual(graph.Root.Children[0].GetLockedDescription(), text);
            }
        }

        public class AddGroupMethod : NodeGraphBuilderTest {
            [Test]
            public void It_should_add_a_Group () {
                var graph = _builder
                    .AddGroup()
                    .Build();
                
                Assert.IsTrue(graph.Root.Children[0].IsGroup);
            }

            [Test]
            public void It_should_add_children_to_the_group () {
                var graph = _builder
                    .AddGroup()
                        .Add("a", _graphic)
                        .End()
                    .End()
                    .Build();
                
                Assert.AreEqual(graph.Root.Children[0].Children[0].Name, "a");
            }

            [Test]
            public void It_should_add_the_passed_node_to_all_children_when_EndGroup_is_called_on_a_group () {
                var graph = _builder
                    .AddGroup()
                        .Add("a", _graphic)
                        .End()
                        .Add("b", _graphic)
                        .End()
                    .EndGroup("exit", _graphic)
                    .Build();
                
                Assert.AreEqual(graph.Root.Children[0].Children[0].Children[0].Name, "exit");
                Assert.AreEqual(graph.Root.Children[0].Children[0].Children[0].Name, "exit");
            }

            [Test]
            public void EndGroup_node_becomes_the_current_pointer () {
                _builder
                    .AddGroup()
                        .Add("a", _graphic)
                        .End()
                        .Add("b", _graphic)
                        .End()
                    .EndGroup("exit", _graphic)
                    .Build();
                
                Assert.AreEqual(_builder.Current.Name, "exit");
            }

            [Test]
            public void Nested_group_exit_should_have_the_correct_number_of_children () {
                _builder
                    .AddGroup()
                        .Add("a", _graphic)
                            .Add("c", _graphic)
                                .Add("d", _graphic)
                                .End()
                            .End()
                        .End()
                        .AddGroup()
                            .Add("b", _graphic)
                            .End()
                            .Add("z", _graphic)
                            .End()
                        .EndGroup("dr", _graphic)
                            .Add("e", _graphic)
                            .End()
                        .End()
                    .EndGroup("exit", _graphic)
                    .Build();
                
                Assert.AreEqual(0, _builder.Current.Children.Count);
            }
            
            [Test]
            public void Nested_group_exit_should_have_the_correct_number_of_children_at_three_nesting () {
                _builder
                    .AddGroup()
                        .Add("a", _graphic)
                            .Add("c", _graphic)
                                .Add("d", _graphic)
                                .End()
                            .End()
                        .End()
                        .AddGroup()
                            .Add("b", _graphic)
                            .End()
                            .Add("z", _graphic)
                            .End()
                        .EndGroup("dr", _graphic)
                            .Add("e", _graphic)
                            .End()
                        .End()
                        .AddGroup()
                            .Add("b", _graphic)
                            .End()
                            .Add("z", _graphic)
                            .End()
                        .EndGroup("dr", _graphic)
                            .Add("e", _graphic)
                            .End()
                        .End()
                        .AddGroup()
                            .Add("b", _graphic)
                            .End()
                            .Add("z", _graphic)
                            .End()
                        .EndGroup("dr", _graphic)
                            .Add("e", _graphic)
                            .End()
                        .End()
                    .EndGroup("exit", _graphic)
                    .Build();
                
                Assert.AreEqual(0, _builder.Current.Children.Count);
            }
        }
    }
}