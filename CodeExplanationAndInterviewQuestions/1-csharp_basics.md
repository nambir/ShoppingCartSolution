
# Difference Between `const` and `readonly` in C#


## Code Examples

### `const`
```csharp
    public class Maths
    {
        public const double pi = 3.14; //// must be assigned here.
        public readonly int maxtimeout = 100;
        public Maths()
        {
            maxtimeout = 100;
            //pi = 3.14;   cannot be set.
        }
    }


```



## Summary

- Use `const` when the value is truly constant and known at compile time.
- Use `readonly` when the value is only known at runtime but should not change after initialization.

| Feature                  | `const`                                  | `readonly`                             |
|--------------------------|------------------------------------------|----------------------------------------|
| **When assigned**        | At **compile-time**                      | At **runtime** (in constructor)        |
| **Default modifier**     | **Static by default**                    | **Instance level** by default          |
| **Can be changed in**    | Never changes after compilation          | Can only be set in constructor         |
| **Allowed data types**   | Only **primitive** types or strings      | Any data type (objects, structs, etc.) |
| **Use with expressions** | Must use **constant expressions**        | Can use non-constant values            |
| **Example declaration**  | `const int Max = 10;`                    | `readonly int Max;` (set in constructor) |
