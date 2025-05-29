## 보간 문자열 예시
```c#
var name = "Mark";
var date = DateTime.Now;

// Composite formatting:
Console.WriteLine("Hello, {0}! Today is {1}, it's {2:HH:mm} now.", name, date.DayOfWeek, date);
// String interpolation:
Console.WriteLine($"Hello, {name}! Today is {date.DayOfWeek}, it's {date:HH:mm} now.");
// Both calls produce the same output that is similar to:
// Hello, Mark! Today is Wednesday, it's 19:40 now.
```

## 보간된 문자열의 구조
문자열 리터럴을 보간된 문자열로 식별하려면 $ 기호를 앞에 붙이십시오. 문자열 리터럴을 시작할 때 $와 " 사이에 공백이 있으면 안 됩니다.
```c#
{<interpolationExpression>[,<width>][:<formatString>]}
```

- interpolationExpression : 서식을 지정할 결과를 생성하는 식. 식이면 `null`출력은 빈 문자열(String.Empty)
- width                   : 값이 식 결과의 문자열 표현에서 최소 문자 수를 정의하는 상수 식. 양수면 문자열 표현이 오른쪽 정렬, 음수면 왼쪽 정렬
- formatString            : 식 결과의 형식에서 지원하는 형식 문자열

예제
```c#
Console.WriteLine($"|{"Left",-7}|{"Right",7}|");

const int FieldWidthRightAligned = 20;
Console.WriteLine($"{Math.PI,FieldWidthRightAligned} - default formatting of the pi number");
Console.WriteLine($"{Math.PI,FieldWidthRightAligned:F3} - display only three decimal digits of the pi number");
// Output is:
// |Left   |  Right|
//     3.14159265358979 - default formatting of the pi number
//                3.142 - display only three decimal digits of the pi number
```

```c#
string message = $"The usage policy for {safetyScore} is {
    safetyScore switch
    {
        > 90 => "Unlimited usage",
        > 80 => "General usage, with daily safety check",
        > 70 => "Issues must be addressed within 1 week",
        > 50 => "Issues must be addressed within 1 day",
        _ => "Issues must be addressed before continued use",
    }
    }";
```

## 보간된 원시 문자열 리터럴 over C# 11
```c#
int X = 2;
int Y = 3;

var pointMessage = $"""The point "{X}, {Y}" is {Math.Sqrt(X * X + Y * Y):F3} from the origin""";

Console.WriteLine(pointMessage);
// Output is:
// The point "2, 3" is 3.606 from the origin
```

결과 문자열에 { 및 } 문자를 포함하려면 여러 $ 문자가 들어 있는 보간된 원시 문자열 리터럴로 시작하십시오. 이렇게 하면 문자 수 {$ 보다 짧은 모든 시퀀스 또는 } 문자가 결과 문자열에 포함됩니다. 해당 문자열 내의 보간 식을 묶으려면 다음 예제와 같이 문자 수와 동일한 개수의 $ 중괄호를 사용해야 합니다.

```c#
int X = 2;
int Y = 3;

var pointMessage = $$"""{The point {{{X}}, {{Y}}} is {{Math.Sqrt(X * X + Y * Y):F3}} from the origin}""";
Console.WriteLine(pointMessage);
// Output is:
// {The point {2, 3} is 3.606 from the origin}
```

## 특수 문자
보간된 문자열로 생성된 텍스트에 중괄호 "{" 또는 "}"를 포함하려면 두 개의 중괄호인 "{{" 또는 "}}"를 사용합니다. 자세한 내용은 복합 서식 문서의이스케이프 중괄호 섹션을 참조하세요.

콜론(":")은 보간 식 항목에 특별한 의미가 있으므로 보간 식에서 조건부 연산 자를 사용합니다. 해당 식을 괄호로 묶습니다.

다음 예제에서는 결과 문자열에 중괄호를 포함하는 방법을 보여줍니다. 또한 조건부 연산자를 사용하는 방법도 보여줍니다.

```c#
string name = "Horace";
int age = 34;
Console.WriteLine($"He asked, \"Is your name {name}?\", but didn't wait for a reply :-{{");
Console.WriteLine($"{name} is {age} year{(age == 1 ? "" : "s")} old.");
// Output is:
// He asked, "Is your name Horace?", but didn't wait for a reply :-{
// Horace is 34 years old.
```

## 문화권별 서식 지정
```c#
double speedOfLight = 299792.458;

System.Globalization.CultureInfo.CurrentCulture = System.Globalization.CultureInfo.GetCultureInfo("nl-NL");
string messageInCurrentCulture = $"The speed of light is {speedOfLight:N3} km/s.";

var specificCulture = System.Globalization.CultureInfo.GetCultureInfo("en-IN");
string messageInSpecificCulture = string.Create(
    specificCulture, $"The speed of light is {speedOfLight:N3} km/s.");

string messageInInvariantCulture = string.Create(
    System.Globalization.CultureInfo.InvariantCulture, $"The speed of light is {speedOfLight:N3} km/s.");

Console.WriteLine($"{System.Globalization.CultureInfo.CurrentCulture,-10} {messageInCurrentCulture}");
Console.WriteLine($"{specificCulture,-10} {messageInSpecificCulture}");
Console.WriteLine($"{"Invariant",-10} {messageInInvariantCulture}");
// Output is:
// nl-NL      The speed of light is 299.792,458 km/s.
// en-IN      The speed of light is 2,99,792.458 km/s.
// Invariant  The speed of light is 299,792.458 km/s.
```