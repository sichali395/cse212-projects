using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Dequeue from empty queue
    // Expected Result: InvalidOperationException with message "The queue is empty."
    // Defect(s) Found: None - this works correctly
    public void TestPriorityQueue_1()
    {
        var priorityQueue = new PriorityQueue();

        // Test that empty queue throws correct exception
        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Should have thrown exception for empty queue");
        }
        catch (InvalidOperationException ex)
        {
            Assert.AreEqual("The queue is empty.", ex.Message);
        }
    }

    [TestMethod]
    // Scenario: Basic priority queue operations with different priorities
    // Expected Result: Items dequeued in order of highest priority first
    // Defect(s) Found: 1. Loop doesn't check last element 2. Items not removed from queue 3. FIFO not maintained for equal priorities
    public void TestPriorityQueue_2()
    {
        var priorityQueue = new PriorityQueue();

        // Add items with different priorities
        priorityQueue.Enqueue("Low", 1);
        priorityQueue.Enqueue("High", 3);
        priorityQueue.Enqueue("Medium", 2);

        // Should dequeue highest priority first (High)
        Assert.AreEqual("High", priorityQueue.Dequeue());

        // Next should be Medium
        Assert.AreEqual("Medium", priorityQueue.Dequeue());

        // Last should be Low
        Assert.AreEqual("Low", priorityQueue.Dequeue());

        // Queue should now be empty
        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Should have thrown exception for empty queue");
        }
        catch (InvalidOperationException)
        {
            // Expected
        }
    }

    [TestMethod]
    // Scenario: Multiple items with same high priority
    // Expected Result: First item with highest priority dequeued first (FIFO for equal priorities)
    // Defect(s) Found: Uses >= comparison which picks last equal priority instead of first
    public void TestPriorityQueue_3()
    {
        var priorityQueue = new PriorityQueue();

        // Add two items with same high priority
        priorityQueue.Enqueue("FirstHigh", 3);
        priorityQueue.Enqueue("Low", 1);
        priorityQueue.Enqueue("SecondHigh", 3);

        // Should dequeue FirstHigh (first of the high priorities)
        Assert.AreEqual("FirstHigh", priorityQueue.Dequeue());

        // Next should be SecondHigh (remaining high priority)
        Assert.AreEqual("SecondHigh", priorityQueue.Dequeue());

        // Last should be Low
        Assert.AreEqual("Low", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Item at last position has highest priority
    // Expected Result: Last item should be found and dequeued
    // Defect(s) Found: Loop condition index < _queue.Count - 1 doesn't check last element
    public void TestPriorityQueue_4()
    {
        var priorityQueue = new PriorityQueue();

        // Add items where last has highest priority
        priorityQueue.Enqueue("First", 1);
        priorityQueue.Enqueue("Last", 5);

        // Should dequeue Last (highest priority at last position)
        Assert.AreEqual("Last", priorityQueue.Dequeue());

        // Next should be First
        Assert.AreEqual("First", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Single item in queue
    // Expected Result: Single item dequeued correctly
    // Defect(s) Found: None for single item case
    public void TestPriorityQueue_5()
    {
        var priorityQueue = new PriorityQueue();

        priorityQueue.Enqueue("Only", 1);

        Assert.AreEqual("Only", priorityQueue.Dequeue());

        // Queue should now be empty
        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Should have thrown exception for empty queue");
        }
        catch (InvalidOperationException)
        {
            // Expected
        }
    }

    [TestMethod]
    // Scenario: Complex mix of priorities with multiple equal priorities
    // Expected Result: Correct priority order with FIFO for equal priorities
    // Defect(s) Found: Multiple defects in original implementation
    public void TestPriorityQueue_6()
    {
        var priorityQueue = new PriorityQueue();

        priorityQueue.Enqueue("A", 1);
        priorityQueue.Enqueue("B", 3);
        priorityQueue.Enqueue("C", 2);
        priorityQueue.Enqueue("D", 3); // Same priority as B
        priorityQueue.Enqueue("E", 1);

        // First should be B (first priority 3)
        Assert.AreEqual("B", priorityQueue.Dequeue());

        // Next should be D (remaining priority 3)
        Assert.AreEqual("D", priorityQueue.Dequeue());

        // Next should be C (priority 2)
        Assert.AreEqual("C", priorityQueue.Dequeue());

        // Next should be A (first priority 1)
        Assert.AreEqual("A", priorityQueue.Dequeue());

        // Last should be E (remaining priority 1)
        Assert.AreEqual("E", priorityQueue.Dequeue());
    }
}