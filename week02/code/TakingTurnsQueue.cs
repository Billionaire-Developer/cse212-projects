/// <summary>
/// This queue is circular. When people are added via AddPerson, they are added to the 
/// back of the queue (FIFO). When GetNextPerson is called, the next person
/// is removed from the front and returned. They are placed at the back again only if:
/// - They have more than 1 turn (decrement their turns and re-enqueue)
/// - They have 0 or fewer turns (infinite turns, re-enqueue unchanged)
/// If their turns were exactly 1, they are not re-added to the queue.
/// </summary>
public class TakingTurnsQueue
{
    private readonly PersonQueue _people = new();

    /// <summary>
    /// Gets the current number of people in the queue.
    /// </summary>
    public int Length => _people.Length;

    /// <summary>
    /// Add a new person to the queue with a name and a number of turns.
    /// If turns is 0 or less, the person will stay in the queue indefinitely.
    /// </summary>
    /// <param name="name">The name of the person</param>
    /// <param name="turns">The number of turns (0 or less = infinite)</param>
    public void AddPerson(string name, int turns)
    {
        var person = new Person(name, turns);
        _people.Enqueue(person);
    }

    /// <summary>
    /// Returns the next person in the queue.
    /// Re-enqueues the person based on their remaining turns:
    /// - Infinite turns (≤ 0): person is re-added without change
    /// - Finite turns (> 1): decremented and re-added
    /// - Exactly 1 turn: returned without re-adding
    /// Throws an exception if the queue is empty.
    /// </summary>
    /// <returns>The next person in the queue</returns>
    /// <exception cref="InvalidOperationException">If the queue is empty</exception>
    public Person GetNextPerson()
    {
        if (_people.IsEmpty())
        {
            throw new InvalidOperationException("No one in the queue.");
        }

        Person person = _people.Dequeue();

        if (person.Turns <= 0)
        {
            // Infinite turns, re-enqueue as-is
            _people.Enqueue(person);
        }
        else if (person.Turns > 1)
        {
            // Decrement and re-enqueue
            person.Turns -= 1;
            _people.Enqueue(person);
        }
        // If person.Turns == 1, do not re-enqueue

        return person;
    }

    public override string ToString()
    {
        return _people.ToString();
    }
}