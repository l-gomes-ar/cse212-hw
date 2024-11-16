using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Items are enqueued as per FIFO rule
    // Expected Result: items should be enqueued by inserting to the back: "[fourth (Pri:1), third (Pri:3), second (Pri:2), first (Pri:4)]" 
    // Defect(s) Found: Items were not being added to the back, but to the front of queue
    public void TestPriorityQueue_Enqueue()
    {
        string testResults = "[fourth (Pri:1), third (Pri:3), second (Pri:2), first (Pri:4)]";

        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("first", 4);
        priorityQueue.Enqueue("second", 2);
        priorityQueue.Enqueue("third", 3);
        priorityQueue.Enqueue("fourth", 1);

        Assert.AreEqual(testResults, priorityQueue.ToString(), message:"Items are not being enqueued correctly!");

    }

    [TestMethod]
    // Scenario: Multiple items with the same priority follow the FIFO (First in, first out) rule 
    // Expected Result: items should be dequeued in the order they were added ["first", "second", "third", "fourth"]
    // Defect(s) Found: Dequeue function's for loop was not accounting for the whole queue, and was not removing items from queue
    public void TestPriorityQueue_DequeueSamePriority()
    {
        string[] testResults = new string[] { "first", "second", "third", "fourth" };

        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("first", 2);
        priorityQueue.Enqueue("second", 2);
        priorityQueue.Enqueue("third", 2);
        priorityQueue.Enqueue("fourth", 2);

        for (int i = 0; i < 4; i++) {
            Assert.AreEqual(testResults[i], priorityQueue.Dequeue(), message:"Items with the same priority are not being dequeued in the right order!");
        }
    }

    [TestMethod]
    // Scenario: Items with different priorities should be dequeued in order of highest to lowest priority
    // Expected Result: "first", "second", "third", "fourth", "fifth" (highest to lowest priority, same priority follows FIFO rule)
    // Defect(s) Found: None
    public void TestPriorityQueue_DequeueHighestPriority()
    {
        string[] testResults = new string[] { "first", "second", "third", "fourth", "fifth"};

        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("fourth", 1);
        priorityQueue.Enqueue("fifth", 1);
        priorityQueue.Enqueue("first", 4);
        priorityQueue.Enqueue("third", 2);
        priorityQueue.Enqueue("second", 3);

        for (int i = 0; i < 4; i++) {
            Assert.AreEqual(testResults[i], priorityQueue.Dequeue(), message:"Items with different priorities are not being dequeued in the right order!");
        }
    }

    [TestMethod]
    // Scenario: If queue is empty, an error should be thrown
    // Expected Result: When trying to dequeue an empty queue, an error should be thrown
    // Defect(s) Found: None
    public void TestPriorityQueue_DequeueEmptyQueue()
    {
        var priorityQueue = new PriorityQueue();
        Assert.ThrowsException<InvalidOperationException>(() => priorityQueue.Dequeue());
    }
}