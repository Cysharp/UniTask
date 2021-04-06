using Cysharp.Threading.Tasks;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Channels;
using Cysharp.Threading.Tasks.Linq;
using System.Threading.Tasks;
using Xunit;

namespace NetCoreTests
{
    public class TriggerEventTest
    {
        [Fact]
        public void SimpleAdd()
        {
            var ev = new TriggerEvent<int>();

            // do nothing
            ev.SetResult(0);
            ev.SetError(null);
            ev.SetCompleted();
            ev.SetCanceled(default);

            {
                var one = new TestEvent(1);

                ev.Add(one);

                ev.SetResult(10);
                ev.SetResult(20);
                ev.SetResult(30);

                one.NextCalled.Should().Equal(10, 20, 30);

                ev.SetCompleted();

                one.CompletedCalled.Count.Should().Be(1);

                // do nothing
                ev.SetResult(0);
                ev.SetError(null);
                ev.SetCompleted();
                ev.SetCanceled(default);

                one.NextCalled.Should().Equal(10, 20, 30);
                one.CompletedCalled.Count.Should().Be(1);
            }
            // after removed, onemore
            {
                var one = new TestEvent(1);

                ev.Add(one);

                ev.SetResult(10);
                ev.SetResult(20);
                ev.SetResult(30);

                one.NextCalled.Should().Equal(10, 20, 30);

                ev.SetCompleted();

                one.CompletedCalled.Count.Should().Be(1);

                // do nothing
                ev.SetResult(0);
                ev.SetError(null);
                ev.SetCompleted();
                ev.SetCanceled(default);

                one.NextCalled.Should().Equal(10, 20, 30);
                one.CompletedCalled.Count.Should().Be(1);
            }
        }

        [Fact]
        public void AddFour()
        {
            var ev = new TriggerEvent<int>();

            // do nothing
            ev.SetResult(0);
            ev.SetError(null);
            ev.SetCompleted();
            ev.SetCanceled(default);

            {
                var one = new TestEvent(1);
                var two = new TestEvent(2);
                var three = new TestEvent(3);
                var four = new TestEvent(4);

                ev.Add(one);
                ev.Add(two);
                ev.Add(three);
                ev.Add(four);

                ev.SetResult(10);
                ev.SetResult(20);
                ev.SetResult(30);

                one.NextCalled.Should().Equal(10, 20, 30);
                two.NextCalled.Should().Equal(10, 20, 30);
                three.NextCalled.Should().Equal(10, 20, 30);
                four.NextCalled.Should().Equal(10, 20, 30);

                ev.SetCompleted();

                one.CompletedCalled.Count.Should().Be(1);
                two.CompletedCalled.Count.Should().Be(1);
                three.CompletedCalled.Count.Should().Be(1);


                // do nothing
                ev.SetResult(0);
                ev.SetError(null);
                ev.SetCompleted();
                ev.SetCanceled(default);

                one.NextCalled.Should().Equal(10, 20, 30);
                one.CompletedCalled.Count.Should().Be(1);
                two.NextCalled.Should().Equal(10, 20, 30);
                three.CompletedCalled.Count.Should().Be(1);
                two.NextCalled.Should().Equal(10, 20, 30);
                three.CompletedCalled.Count.Should().Be(1);
            }

            // after removed, onemore.
            {
                var one = new TestEvent(1);
                var two = new TestEvent(2);
                var three = new TestEvent(3);
                var four = new TestEvent(4);

                ev.Add(one);
                ev.Add(two);
                ev.Add(three);
                ev.Add(four);

                ev.SetResult(10);
                ev.SetResult(20);
                ev.SetResult(30);
                ev.Add(four);

                one.NextCalled.Should().Equal(10, 20, 30);
                two.NextCalled.Should().Equal(10, 20, 30);
                three.NextCalled.Should().Equal(10, 20, 30);
                four.NextCalled.Should().Equal(10, 20, 30);

                ev.SetCompleted();

                one.CompletedCalled.Count.Should().Be(1);
                two.CompletedCalled.Count.Should().Be(1);
                three.CompletedCalled.Count.Should().Be(1);


                // do nothing
                ev.SetResult(0);
                ev.SetError(null);
                ev.SetCompleted();
                ev.SetCanceled(default);

                one.NextCalled.Should().Equal(10, 20, 30);
                one.CompletedCalled.Count.Should().Be(1);
                two.NextCalled.Should().Equal(10, 20, 30);
                three.CompletedCalled.Count.Should().Be(1);
                two.NextCalled.Should().Equal(10, 20, 30);
                three.CompletedCalled.Count.Should().Be(1);
            }
        }


        [Fact]
        public void OneRemove()
        {
            var ev = new TriggerEvent<int>();
            {
                var one = new TestEvent(1);
                var two = new TestEvent(2);
                var three = new TestEvent(3);

                ev.Add(one);
                ev.Add(two);
                ev.Add(three);

                ev.SetResult(10);
                ev.SetResult(20);
                ev.SetResult(30);

                one.NextCalled.Should().Equal(10, 20, 30);
                two.NextCalled.Should().Equal(10, 20, 30);
                three.NextCalled.Should().Equal(10, 20, 30);

                ev.Remove(one);

                ev.SetResult(40);
                ev.SetResult(50);
                ev.SetResult(60);

                one.NextCalled.Should().Equal(10, 20, 30);
                two.NextCalled.Should().Equal(10, 20, 30, 40, 50, 60);
                three.NextCalled.Should().Equal(10, 20, 30, 40, 50, 60);
            }
        }
        [Fact]
        public void TwoRemove()
        {
            var ev = new TriggerEvent<int>();
            {
                var one = new TestEvent(1);
                var two = new TestEvent(2);
                var three = new TestEvent(3);

                ev.Add(one);
                ev.Add(two);
                ev.Add(three);

                ev.SetResult(10);
                ev.SetResult(20);
                ev.SetResult(30);

                one.NextCalled.Should().Equal(10, 20, 30);
                two.NextCalled.Should().Equal(10, 20, 30);
                three.NextCalled.Should().Equal(10, 20, 30);

                ev.Remove(two);

                ev.SetResult(40);
                ev.SetResult(50);
                ev.SetResult(60);

                one.NextCalled.Should().Equal(10, 20, 30, 40, 50, 60);
                two.NextCalled.Should().Equal(10, 20, 30);
                three.NextCalled.Should().Equal(10, 20, 30, 40, 50, 60);
            }
        }
        [Fact]
        public void ThreeRemove()
        {
            var ev = new TriggerEvent<int>();
            {
                var one = new TestEvent(1);
                var two = new TestEvent(2);
                var three = new TestEvent(3);

                ev.Add(one);
                ev.Add(two);
                ev.Add(three);

                ev.SetResult(10);
                ev.SetResult(20);
                ev.SetResult(30);

                one.NextCalled.Should().Equal(10, 20, 30);
                two.NextCalled.Should().Equal(10, 20, 30);
                three.NextCalled.Should().Equal(10, 20, 30);

                ev.Remove(three);

                ev.SetResult(40);
                ev.SetResult(50);
                ev.SetResult(60);

                one.NextCalled.Should().Equal(10, 20, 30, 40, 50, 60);
                two.NextCalled.Should().Equal(10, 20, 30, 40, 50, 60);
                three.NextCalled.Should().Equal(10, 20, 30);
            }
        }

        [Fact]
        public void RemoveSelf()
        {
            new RemoveMe().Run1();
            new RemoveMe().Run2();
            new RemoveMe().Run3();
        }

        [Fact]
        public void RemoveNextInIterating()
        {
            new RemoveNext().Run1();
            new RemoveNext().Run2();
            new RemoveNext().Run3();
        }

        [Fact]
        public void RemoveNextNextTest()
        {
            new RemoveNextNext().Run1();
            new RemoveNextNext().Run2();
        }


        [Fact]
        public void AddTest()
        {
            new AddMe().Run1();
            new AddMe().Run2();
        }

        public class RemoveMe
        {
            TriggerEvent<int> ev;

            public void Run1()
            {
                TestEvent one = default;
                one = new TestEvent(1, () => ev.Remove(one));

                var two = new TestEvent(2);
                var three = new TestEvent(3);

                ev.Add(one);
                ev.Add(two);
                ev.Add(three);

                ev.SetResult(10);
                ev.SetResult(20);
                ev.SetResult(30);

                one.NextCalled.Should().Equal(10);
                two.NextCalled.Should().Equal(10, 20, 30);
                three.NextCalled.Should().Equal(10, 20, 30);
            }

            public void Run2()
            {
                TestEvent one = default;
                one = new TestEvent(1, () => ev.Remove(one));

                var two = new TestEvent(2);
                var three = new TestEvent(3);

                ev.Add(two);
                ev.Add(one); // add second.
                ev.Add(three);

                ev.SetResult(10);
                ev.SetResult(20);
                ev.SetResult(30);

                one.NextCalled.Should().Equal(10);
                two.NextCalled.Should().Equal(10, 20, 30);
                three.NextCalled.Should().Equal(10, 20, 30);
            }

            public void Run3()
            {
                TestEvent one = default;
                one = new TestEvent(1, () => ev.Remove(one));

                var two = new TestEvent(2);
                var three = new TestEvent(3);

                ev.Add(two);
                ev.Add(three);
                ev.Add(one); // add thired.

                ev.SetResult(10);
                ev.SetResult(20);
                ev.SetResult(30);

                one.NextCalled.Should().Equal(10);
                two.NextCalled.Should().Equal(10, 20, 30);
                three.NextCalled.Should().Equal(10, 20, 30);
            }
        }

        public class RemoveNext
        {
            TriggerEvent<int> ev;

            public void Run1()
            {
                TestEvent one = default;
                TestEvent two = default;
                TestEvent three = default;
                one = new TestEvent(1, () => ev.Remove(two));
                two = new TestEvent(2);
                three = new TestEvent(3);

                ev.Add(one);
                ev.Add(two);
                ev.Add(three);

                ev.SetResult(10);
                ev.SetResult(20);
                ev.SetResult(30);

                one.NextCalled.Should().Equal(10, 20, 30);
                two.NextCalled.Count.Should().Be(0);
                three.NextCalled.Should().Equal(10, 20, 30);
            }

            public void Run2()
            {
                TestEvent one = default;
                TestEvent two = default;
                TestEvent three = default;
                one = new TestEvent(1, () => ev.Remove(two));
                two = new TestEvent(2);
                three = new TestEvent(3);

                ev.Add(two);
                ev.Add(one); // add second
                ev.Add(three);

                ev.SetResult(10);
                ev.SetResult(20);
                ev.SetResult(30);

                one.NextCalled.Should().Equal(10, 20, 30);
                two.NextCalled.Should().Equal(10);
                three.NextCalled.Should().Equal(10, 20, 30);
            }

            public void Run3()
            {
                TestEvent one = default;
                TestEvent two = default;
                TestEvent three = default;
                one = new TestEvent(1, () => ev.Remove(two));
                two = new TestEvent(2);
                three = new TestEvent(3);

                ev.Add(two);
                ev.Add(three);
                ev.Add(one); // add thired.

                ev.SetResult(10);
                ev.SetResult(20);
                ev.SetResult(30);

                one.NextCalled.Should().Equal(10, 20, 30);
                two.NextCalled.Should().Equal(10);
                three.NextCalled.Should().Equal(10, 20, 30);
            }
        }

        public class RemoveNextNext
        {
            TriggerEvent<int> ev;

            public void Run1()
            {
                TestEvent one = default;
                TestEvent two = default;
                TestEvent three = default;
                TestEvent four = default;
                one = new TestEvent(1, () => { ev.Remove(two); ev.Remove(three); });
                two = new TestEvent(2);
                three = new TestEvent(3);
                four = new TestEvent(4);

                ev.Add(one);
                ev.Add(two);
                ev.Add(three);
                ev.Add(four);

                ev.SetResult(10);
                ev.SetResult(20);
                ev.SetResult(30);

                one.NextCalled.Should().Equal(10, 20, 30);
                two.NextCalled.Count.Should().Be(0);
                three.NextCalled.Count.Should().Be(0);
                four.NextCalled.Should().Equal(10, 20, 30);
            }

            public void Run2()
            {
                TestEvent one = default;
                TestEvent two = default;
                TestEvent three = default;
                TestEvent four = default;
                one = new TestEvent(1, () => { ev.Remove(one); ev.Remove(two); ev.Remove(three); });
                two = new TestEvent(2);
                three = new TestEvent(3);
                four = new TestEvent(4);

                ev.Add(one);
                ev.Add(two);
                ev.Add(three);
                ev.Add(four);

                ev.SetResult(10);
                ev.SetResult(20);
                ev.SetResult(30);

                one.NextCalled.Should().Equal(10);
                two.NextCalled.Count.Should().Be(0);
                three.NextCalled.Count.Should().Be(0);
                four.NextCalled.Should().Equal(10, 20, 30);
            }


        }

        public class AddMe
        {
            TriggerEvent<int> ev;

            public void Run1()
            {
                TestEvent one = default;
                TestEvent two = default;
                TestEvent three = default;
                TestEvent four = default;

                one = new TestEvent(1, () =>
                {
                    if (two == null)
                    {
                        ev.Add(two = new TestEvent(2));
                    }
                    else if (three == null)
                    {
                        ev.Add(three = new TestEvent(3));
                    }
                    else if (four == null)
                    {
                        ev.Add(four = new TestEvent(4));
                    }
                });

                ev.Add(one);

                ev.SetResult(10);
                ev.SetResult(20);
                ev.SetResult(30);
                ev.SetResult(40);

                one.NextCalled.Should().Equal(10, 20, 30, 40);
                two.NextCalled.Should().Equal(20, 30, 40);
                three.NextCalled.Should().Equal(30, 40);
                four.NextCalled.Should().Equal(40);
            }

            public void Run2()
            {
                TestEvent one = default;
                TestEvent two = default;
                TestEvent three = default;
                TestEvent four = default;

                one = new TestEvent(1, () =>
                {
                    if (two == null)
                    {
                        ev.Add(two = new TestEvent(2, () =>
                        {
                            if (three == null)
                            {
                                ev.Add(three = new TestEvent(3, () =>
                                {
                                    if (four == null)
                                    {
                                        ev.Add(four = new TestEvent(4));
                                    }
                                }));
                            }
                        }));
                    }
                });

                ev.Add(one);

                ev.SetResult(10);
                ev.SetResult(20);
                ev.SetResult(30);
                ev.SetResult(40);

                one.NextCalled.Should().Equal(10, 20, 30, 40);
                two.NextCalled.Should().Equal(20, 30, 40);
                three.NextCalled.Should().Equal(30, 40);
                four.NextCalled.Should().Equal(40);
            }
        }
    }

    public class TestEvent : ITriggerHandler<int>
    {
        public readonly int Id;
        readonly Action iteratingEvent;

        public TestEvent(int id)
        {
            this.Id = id;
        }

        public TestEvent(int id, Action iteratingEvent)
        {
            this.Id = id;
            this.iteratingEvent = iteratingEvent;
        }

        public List<int> NextCalled = new List<int>();
        public List<Exception> ErrorCalled = new List<Exception>();
        public List<object> CompletedCalled = new List<object>();
        public List<CancellationToken> CancelCalled = new List<CancellationToken>();

        public ITriggerHandler<int> Prev { get; set; }
        public ITriggerHandler<int> Next { get; set; }

        public void OnCanceled(CancellationToken cancellationToken)
        {
            CancelCalled.Add(cancellationToken);

        }

        public void OnCompleted()
        {
            CompletedCalled.Add(new object());
        }

        public void OnError(Exception ex)
        {
            ErrorCalled.Add(ex);
        }

        public void OnNext(int value)
        {
            NextCalled.Add(value);
            iteratingEvent?.Invoke();
        }

        public override string ToString()
        {
            return Id.ToString();
        }
    }


}
