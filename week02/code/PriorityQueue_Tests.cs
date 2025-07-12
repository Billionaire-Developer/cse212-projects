using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Single highest priority item should be dequeued first.
    // Expected Result: "A" is returned as it has the highest priority.
    // Defect(s) Found: None
    public void TestPriorityQueue_SingleHighestPriority()
    {
        var queue = new PriorityQueue();
        queue.Enqueue("A", 5);
        queue.Enqueue("B", 2);
        queue.Enqueue("C", 3);
        Assert.AreEqual("A", queue.Dequeue());
    }

    [TestMethod]
    // Scenario: Multiple items with the same highest priority.
    // Expected Result: "X" is returned first due to FIFO rule.
    // Defect(s) Found: None
    public void TestPriorityQueue_MultipleSamePriority()
    {
        var queue = new PriorityQueue();
        queue.Enqueue("X", 10);
        queue.Enqueue("Y", 10);
        queue.Enqueue("Z", 5);
        Assert.AreEqual("X", queue.Dequeue());
        Assert.AreEqual("Y", queue.Dequeue());
    }

    [TestMethod]
    // Scenario: Dequeue all items in correct order of priority.
    // Expected Result: "High", then "Mid", then "Low".
    // Defect(s) Found: None
    public void TestPriorityQueue_DequeueAllInOrder()
    {
        var queue = new PriorityQueue();
        queue.Enqueue("Low", 1);
        queue.Enqueue("Mid", 5);
        queue.Enqueue("High", 10);
        Assert.AreEqual("High", queue.Dequeue());
        Assert.AreEqual("Mid", queue.Dequeue());
        Assert.AreEqual("Low", queue.Dequeue());
    }

    [TestMethod]
    // Scenario: Attempt to dequeue from an empty queue.
    // Expected Result: Throws InvalidOperationException.
    // Defect(s) Found: None
    public void TestPriorityQueue_EmptyDequeueThrows()
    {
        var queue = new PriorityQueue();
        Assert.ThrowsException<InvalidOperationException>(() => queue.Dequeue());
    }

    [TestMethod]
    // Scenario: Items with descending priorities should be dequeued in priority order.
    // Expected Result: "Four", "Three", "Two", "One".
    // Defect(s) Found: None
    public void TestPriorityQueue_DescendingPriorities()
    {
        var queue = new PriorityQueue();
        queue.Enqueue("One", 1);
        queue.Enqueue("Two", 2);
        queue.Enqueue("Three", 3);
        queue.Enqueue("Four", 4);

        Assert.AreEqual("Four", queue.Dequeue());
        Assert.AreEqual("Three", queue.Dequeue());
        Assert.AreEqual("Two", queue.Dequeue());
        Assert.AreEqual("One", queue.Dequeue());
    }

    [TestMethod]
    // Scenario: All items have the same priority.
    // Expected Result: Items dequeued in the same order they were enqueued (FIFO).
    // Defect(s) Found: None
    public void TestPriorityQueue_AllSamePriorityFIFO()
    {
        var queue = new PriorityQueue();
        queue.Enqueue("A", 3);
        queue.Enqueue("B", 3);
        queue.Enqueue("C", 3);
        Assert.AreEqual("A", queue.Dequeue());
        Assert.AreEqual("B", queue.Dequeue());
        Assert.AreEqual("C", queue.Dequeue());
    }
}
