using NSubstitute;
using NUnit.Framework;

namespace CleverCrow.UiNodeBuilder.Editors {
    public class NodeTest {
        private Node _node;

        [SetUp]
        public void BeforeEach () {
            _node = new Node();
        }
        
        public class PurchasedProperty : NodeTest {
            [Test]
            public void Invokes_OnPurchase_if_true_is_set () {
                var result = false;
                _node.OnPurchase.AddListener(() => result = true);

                _node.Purchase();
                
                Assert.IsTrue(result);
            }
            
            [Test]
            public void It_does_not_trigger_OnPurchase_again_if_already_purchased () {
                _node.Purchase();
                
                var result = false;
                _node.OnPurchase.AddListener(() => result = true);
                _node.Purchase();

                Assert.IsFalse(result);
            }
            
            [Test]
            public void Calling_purchase_triggers_Enable_on_all_children_who_are_purchasable () {
                var child = Substitute.For<INode>();
                child.IsPurchasable.Returns(true); 
                _node.Children.Add(child);

                _node.Purchase();

                child.Received(1).Enable();
            }
            
            [Test]
            public void Calling_purchase_does_not_triggers_children_who_are_not_purchasable () {
                var child = Substitute.For<INode>();
                child.IsPurchasable.Returns(false); 
                _node.Children.Add(child);

                _node.Purchase();

                child.Received(0).Enable();
            }
        }

        public class IsPurchasableProperty : NodeTest {
            [Test]
            public void Returns_true_if_Purchased_is_false () {                
                Assert.IsTrue(_node.IsPurchasable);
            }
            
            [Test]
            public void Returns_false_if_Purchased_is_true () {
                _node.Purchase();

                Assert.IsFalse(_node.IsPurchasable);
            }

            [Test]
            public void Returns_false_if_Purchased_is_false_and_OnIsPurchasable_is_false () {
                _node.OnIsPurchasable = () => false;

                Assert.IsFalse(_node.IsPurchasable);
            }
            
            [Test]
            public void Returns_true_if_Purchased_is_false_and_OnIsPurchasable_is_true () {
                _node.OnIsPurchasable = () => true;

                Assert.IsTrue(_node.IsPurchasable);
            }
        }

        public class IsLockedProperty : NodeTest {
            [Test]
            public void Should_return_false_by_default () {
                Assert.IsFalse(_node.IsLocked);
            }
            
            [Test]
            public void Returns_the_value_of_OnIsLocked () {
                _node.OnIsLocked = () => true;
                
                Assert.IsTrue(_node.IsLocked);
            }
            
            [Test]
            public void Causes_IsPurchasable_to_return_false_if_true () {
                _node.OnIsLocked = () => true;
                
                Assert.IsFalse(_node.IsPurchasable);
            }     
            
            [Test]
            public void Causes_IsPurchasable_to_return_true_if_false () {
                Assert.IsTrue(_node.IsPurchasable);
            }  
        }

        public class EnableMethod : NodeTest {
            [Test]
            public void It_should_set_IsEnabled () {
                _node.Enable();

                Assert.IsTrue(_node.IsEnabled);
            }
            
            [Test]
            public void It_should_not_set_IsEnabled_if_IsLocked_is_set () {
                _node.OnIsLocked = () => false;
                _node.Enable();

                Assert.IsFalse(_node.IsEnabled);
            }
        }

        public class RefundMethod : NodeTest {
            [Test]
            public void It_should_trigger_the_OnRefund_event () {
                var result = false;
                _node.Purchase();
                _node.OnRefund.AddListener(() => result = true);
                
                _node.Refund();
                
                Assert.IsTrue(result);
            }
            
            [Test]
            public void It_should_not_trigger_if_IsPurchased_is_false () {
                var result = false;
                _node.OnRefund.AddListener(() => result = true);
                
                _node.Refund();
                
                Assert.IsFalse(result);
            }

            [Test]
            public void It_should_trigger_Refund_on_Purchased_children () {
                var child = Substitute.For<INode>();
                
                _node.Purchase();
                _node.AddChild(child);
                child.IsPurchased.Returns(true);
                
                _node.Refund();
                
                child.Received(1).Refund();
            }
            
            [Test]
            public void It_should_trigger_Disable_on_Purchased_children () {
                var child = Substitute.For<INode>();
                
                _node.Purchase();
                _node.AddChild(child);
                child.IsPurchased.Returns(true);
                
                _node.Refund();
                
                child.Received(1).Refund();
            }

            [Test]
            public void It_should_set_Purchased_to_false () {
                _node.Purchase();
                
                _node.Refund();
                
                Assert.IsFalse(_node.IsPurchased);
            }
        }
    }
}