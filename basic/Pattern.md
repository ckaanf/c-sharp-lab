### null 검사
```c#
int? maybe = 12;

if (maybe is int value)
{
  // 'value' is an int here
  Console.WriteLine(value);
}
else
{
  // 'value' is not defined here
  Console.WriteLine("No value");
}

string? message = "Hello, World!";

if (message is not null) {
  Console.WriteLine(message);
}
```

### 형식 테스트
```c#
static T MidPoint<T>(IEnumerable<T> sequence)
{
  if (sequence is IList<T> list)
  {
    return list[list.Count / 2];
  }
  else if (sequence is null)
  {
    throw new ArgumentNullException(nameof(sequence), "Sequence can't be null.");
  }
  else
  {
    int halfLength = sequence.Count() / 2 - 1;
    if (halfLength < 0) halfLength = 0;
    return sequence.Skip(halfLength).First();
  }
}
```

### 불연속 값 비교
```c#
public State PerformOperation(Operation command) =>
   command switch
   {
       Operation.SystemTest => RunDiagnostics(),
       Operation.Start => StartSystem(),
       Operation.Stop => StopSystem(),
       Operation.Reset => ResetToReady(),
       _ => throw new ArgumentException("Invalid enum value for command", nameof(command)),
   };
```

### 관계형 패턴
```c#
string WaterState(int tempInFahrenheit) =>
    tempInFahrenheit switch
    {
        < 32 => "solid",
        32 => "solid/liquid transition",
        (> 32) and (< 212) => "liquid",
        212 => "liquid / gas transition",
        > 212 => "gas",
    };
```

### 여러 입력
```c#
public record Order(int Items, decimal Cost);

public decimal CalculateDiscount(Order order) =>
    order switch
    {
        { Items: > 10, Cost: > 1000.00m } => 0.10m,
        { Items: > 5, Cost: > 500.00m } => 0.05m,
        { Cost: > 250.00m } => 0.02m,
        null => throw new ArgumentNullException(nameof(order), "Can't calculate discount on null order"),
        var someObject => 0m,
    };
```

### 목록 패턴
```c#
decimal balance = 0m;
foreach (string[] transaction in ReadRecords())
{
    balance += transaction switch
    {
        [_, "DEPOSIT", _, var amount]     => decimal.Parse(amount),
        [_, "WITHDRAWAL", .., var amount] => -decimal.Parse(amount),
        [_, "INTEREST", var amount]       => decimal.Parse(amount),
        [_, "FEE", var fee]               => -decimal.Parse(fee),
        _                                 => throw new InvalidOperationException($"Record {string.Join(", ", transaction)} is not in the expected format!"),
    };
    Console.WriteLine($"Record: {string.Join(", ", transaction)}, New balance: {balance:C}");
}
```