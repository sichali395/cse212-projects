/// <summary>
/// Maintain a Customer Service Queue.  Allows new customers to be 
/// added and allows customers to be serviced.
/// </summary>
public class CustomerService
{
    public static void Run()
    {
        // Example code to see what's in the customer service queue:
        // var cs = new CustomerService(10);
        // Console.WriteLine(cs);

        // Test Cases

        // Test 1: Serve from empty queue
        // Scenario: Try to serve when no customers are in queue
        // Expected Result: Should display error message
        Console.WriteLine("Test 1 - Serve from empty queue");
        var cs1 = new CustomerService(5);
        cs1.ServeCustomer(); // Should handle gracefully

        // Defect(s) Found: Fixed - Added null check in ServeCustomer

        Console.WriteLine("=================");

        // Test 2: Add customers beyond max capacity
        // Scenario: Add customers until queue is full, then try to add one more
        // Expected Result: Should prevent adding beyond max size
        Console.WriteLine("Test 2 - Add beyond capacity");
        var cs2 = new CustomerService(2);

        // We'll test by modifying the class slightly to allow programmatic testing
        // or we could use reflection to test private methods
        // For now, let's create a version with public methods for testing

        TestableCustomerService tcs = new TestableCustomerService(2);
        tcs.AddNewCustomer("John", "123", "Issue 1");
        tcs.AddNewCustomer("Jane", "456", "Issue 2");
        tcs.AddNewCustomer("Bob", "789", "Issue 3"); // Should be rejected

        Console.WriteLine($"Queue size: {tcs.GetQueueCount()} (Expected: 2)");

        // Defect(s) Found: Fixed - Changed > to >= in capacity check

        Console.WriteLine("=================");

        // Test 3: Serve customer correctly
        // Scenario: Add customers and serve them in FIFO order
        // Expected Result: Should serve customers in correct order
        Console.WriteLine("Test 3 - Serve in correct order");
        var cs3 = new TestableCustomerService(3);
        cs3.AddNewCustomer("First", "001", "First issue");
        cs3.AddNewCustomer("Second", "002", "Second issue");
        cs3.AddNewCustomer("Third", "003", "Third issue");

        Console.WriteLine("Serving first customer:");
        cs3.ServeCustomer(); // Should serve "First"

        Console.WriteLine("Serving next customer:");
        cs3.ServeCustomer(); // Should serve "Second"

        // Defect(s) Found: Fixed - ServeCustomer now gets customer before removing

        Console.WriteLine("=================");
    }

    private readonly List<Customer> _queue = new();
    private readonly int _maxSize;

    public CustomerService(int maxSize)
    {
        if (maxSize <= 0)
            _maxSize = 10;
        else
            _maxSize = maxSize;
    }

    /// <summary>
    /// Defines a Customer record for the service queue.
    /// This is an inner class.  Its real name is CustomerService.Customer
    /// </summary>
    private class Customer
    {
        public Customer(string name, string accountId, string problem)
        {
            Name = name;
            AccountId = accountId;
            Problem = problem;
        }

        private string Name { get; }
        private string AccountId { get; }
        private string Problem { get; }

        public override string ToString()
        {
            return $"{Name} ({AccountId})  : {Problem}";
        }
    }

    /// <summary>
    /// Prompt the user for the customer and problem information.  Put the 
    /// new record into the queue.
    /// </summary>
    private void AddNewCustomer()
    {
        // Verify there is room in the service queue
        // FIXED: Changed > to >= to properly check capacity
        if (_queue.Count >= _maxSize)
        {
            Console.WriteLine("Maximum Number of Customers in Queue.");
            return;
        }

        Console.Write("Customer Name: ");
        var name = Console.ReadLine()!.Trim();
        Console.Write("Account Id: ");
        var accountId = Console.ReadLine()!.Trim();
        Console.Write("Problem: ");
        var problem = Console.ReadLine()!.Trim();

        // Create the customer object and add it to the queue
        var customer = new Customer(name, accountId, problem);
        _queue.Add(customer);
    }

    /// <summary>
    /// Dequeue the next customer and display the information.
    /// </summary>
    private void ServeCustomer()
    {
        // FIXED: Check if queue is empty
        if (_queue.Count == 0)
        {
            Console.WriteLine("No customers in the queue.");
            return;
        }

        // FIXED: Get customer BEFORE removing it
        var customer = _queue[0];
        Console.WriteLine(customer);
        _queue.RemoveAt(0);
    }

    /// <summary>
    /// Support the WriteLine function to provide a string representation of the
    /// customer service queue object. This is useful for debugging. If you have a 
    /// CustomerService object called cs, then you run Console.WriteLine(cs) to
    /// see the contents.
    /// </summary>
    /// <returns>A string representation of the queue</returns>
    public override string ToString()
    {
        return $"[size={_queue.Count} max_size={_maxSize} => " + string.Join(", ", _queue) + "]";
    }
}

// Helper class for testing without console input
public class TestableCustomerService
{
    private readonly List<TestableCustomer> _queue = new();
    private readonly int _maxSize;

    public TestableCustomerService(int maxSize)
    {
        if (maxSize <= 0)
            _maxSize = 10;
        else
            _maxSize = maxSize;
    }

    private class TestableCustomer
    {
        public TestableCustomer(string name, string accountId, string problem)
        {
            Name = name;
            AccountId = accountId;
            Problem = problem;
        }

        public string Name { get; }
        public string AccountId { get; }
        public string Problem { get; }

        public override string ToString()
        {
            return $"{Name} ({AccountId})  : {Problem}";
        }
    }

    public void AddNewCustomer(string name, string accountId, string problem)
    {
        if (_queue.Count >= _maxSize)
        {
            Console.WriteLine("Maximum Number of Customers in Queue.");
            return;
        }

        var customer = new TestableCustomer(name, accountId, problem);
        _queue.Add(customer);
    }

    public void ServeCustomer()
    {
        if (_queue.Count == 0)
        {
            Console.WriteLine("No customers in the queue.");
            return;
        }

        var customer = _queue[0];
        Console.WriteLine(customer);
        _queue.RemoveAt(0);
    }

    public int GetQueueCount()
    {
        return _queue.Count;
    }
}